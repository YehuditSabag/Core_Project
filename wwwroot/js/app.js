const uri = '/Todo';
let tasks = [];
// 
//////////////////////
var Token = "";
var getToken;
function Login() {
    const name = document.getElementById('name');
    const password = document.getElementById('password');

    var userVal = JSON.stringify({
        Username: name.value.trim(),
        Password: password.value.trim()
    });

    var request = {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }, body: userVal,

        redirect: "follow"
    }; fetch('user/Login', request)
        .then((response) => response.json())
        .then((result) => {
            if (result.includes("401")) {
                name.value = "";
                password.value = "";
                alert("ERROR")
            } else {
                Token = result;
                name.value = "";
                password.value = "";
                localStorage.setItem("token", result)
                getToken = localStorage.getItem("token")
                location.href = "/task.html"
            }
        }).catch((error) => alert("error", error))

}


function getItems() {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + localStorage.getItem("token"));
    myHeaders.append("Content-Type", "application/json");

    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("https://localhost:7260/Todo", requestOptions)
        .then((response) => response.json())

        .then(result => {
            // alert(result[0].userId),
            _displayItems(result);
        })
        .catch(error => console.log('error', error));
}


function _displayItems(data) {
    // alert(data[0].userId);
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';
    _displayCount(data.length);
    const button = document.createElement('button');
    data.forEach(item => {
        let isDoneCheckbox = document.createElement('input');
        isDoneCheckbox.type = 'checkbox';
        isDoneCheckbox.disabled = true;
        isDoneCheckbox.checked = item.isdone;
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);
        let tr = tBody.insertRow();
        let td1 = tr.insertCell(0);
        td1.appendChild(isDoneCheckbox);
        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);
        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);
        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}

// 
function addItem() {

    const addNameTextbox = document.getElementById('add-name');
    const item = {
        id: 1,
        name: addNameTextbox.value.trim(),
        isdone: false,
        userId: 1

    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Authorization': "Bearer " + localStorage.getItem("token"),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': "Bearer " + localStorage.getItem("token"),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = tasks.find(item => item.id === id);
    document.getElementById('edit-userid').value = item.userId;
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-Isdone').checked = item.isdone;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const userid = document.getElementById('edit-userid').value;
    const item = {
        id: parseInt(itemId, 10),
        name: document.getElementById('edit-name').value.trim(),
        isdone: document.getElementById('edit-Isdone').checked,
        userId: parseInt(userid, 10)

    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Authorization': "Bearer " + localStorage.getItem("token"),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'Mylist' : 'Mylist tasks';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}
// ___________________________________________________users

const uri2="/user";
function getAllUsers() {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + localStorage.getItem("token"));
    myHeaders.append("Content-Type", "application/json");

    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("https://localhost:7260/user", requestOptions)
        .then((response) => response.json())

        .then(result => {
            //  alert(result),
            _displayUsers(result);
        })
        .catch(error => alert('You do not have permission for this action', error));
}


function _displayUsers(data) {
   
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';
    _displayCount(data.length);
    const button = document.createElement('button');
    data.forEach(item => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);
        let tr = tBody.insertRow();
       

        
        let td1 = tr.insertCell( 0);
        let textNode = document.createTextNode(item.id);
        td1.appendChild(textNode);
        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.username);
        td2.appendChild(textNode2);
        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(item.password);
        td3.appendChild(textNode3);
        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    users = data;
}


function getMyUser() {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer " + localStorage.getItem("token"));
    myHeaders.append("Content-Type", "application/json");

    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("https://localhost:7260/user/get", requestOptions)
        .then((response) => response.json())

        .then(result => {
             
            _displayUser(result);
        })
        .catch(error => console.log('error', error));
}

function _displayUser(data){
    const tBody = document.getElementById('myuser'); 
    tBody.innerHTML = '';
 
    let tr = tBody.insertRow();
       
        let td1 = tr.insertCell( 0);
        let textNode = document.createTextNode(data.id);
        td1.appendChild(textNode);
        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(data.username);
        td2.appendChild(textNode2);
        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(data.password);
        td3.appendChild(textNode3);

myuser = data;
}

function _displayUsers(data) {
   
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';
    _displayCount(data.length);
    const button = document.createElement('button');
    data.forEach(item => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteUser(${item.id})`);
        let tr = tBody.insertRow();
       
        let td1 = tr.insertCell( 0);
        let textNode = document.createTextNode(item.id);
        td1.appendChild(textNode);
        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.username);
        td2.appendChild(textNode2);
        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(item.password);
        td3.appendChild(textNode3);
        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    users = data;
}

function addUser() {

    const addNameTextbox = document.getElementById('adduser-name');
    const addPasswordTextbox = document.getElementById('adduser-password');
    const item = {
        id: 1,
        username: addNameTextbox.value.trim(),
        password: addPasswordTextbox.value.trim(),
        isAdmin: false

    }; 

    fetch(`${uri2}/Create`, {
        method: 'POST',
        headers: {
            'Authorization': "Bearer " + localStorage.getItem("token"),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getAllUsers();
            addNameTextbox.value = " ";
            addPasswordTextbox.value = " ";
        })
        .catch(error => alert('You do not have permission for this action', error));
}

function deleteUser(id) {
    fetch(`${uri2}/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': "Bearer " + localStorage.getItem("token"),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(() => getAllUsers())
        .catch(error => alert('You do not have permission for this action', error));
}
