var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TodoList.Interfaces.ITodoService, TodoList.Services.TodoService>();
 builder.Services.AddSingleton<user.Interfaces.IuserService, user.Services.userService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
  
}

// 
app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();
// 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

   builder.Services.AddAuthorization(cfg =>
                {
                    cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
                    cfg.AddPolicy("user", policy => policy.RequireClaim("type", "user"));
                   
                });
// --
namespace OrderManagement
{
    static class Extention
    {
        public static IServiceCollection AddOrders(this IServiceCollection services)
        {
            services.AddScoped<TodoList.Interfaces.ITodoService,  TodoList.Services.TodoService>();
            return services;
        }
    }
}
