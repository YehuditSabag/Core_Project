using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using user.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(cfg =>
{
    //    cfg.RequireHttpsMetadata = false;
    cfg.TokenValidationParameters = TokenService.GetTokenValidationParameters();
});
builder.Services.AddEndpointsApiExplorer();
// 
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="Bearer"}
            },
            new string[] {}
        }
    });
});
builder.Services.AddSingleton<TodoList.Interfaces.ITodoService, TodoList.Services.TodoService>();
builder.Services.AddSingleton<user.Interfaces.IuserService, user.Services.userService>();
// builder.Services.AddSingleton<user.Interfaces.ITokenServices, user.Services.TokenService>();
builder.Services.AddAuthorization(cfg =>
             {
                 cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
                 cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));

             });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();
// 
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


// --
namespace OrderManagement
{
    static class Extention
    {
        public static IServiceCollection AddOrders(this IServiceCollection services)
        {
            services.AddScoped<TodoList.Interfaces.ITodoService, TodoList.Services.TodoService>();
            return services;
        }
    }
}
