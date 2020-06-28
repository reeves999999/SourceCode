import 'bootstrap-3-typeahead';
import 'bootstrap';

const uri = 'api/v1/ClientApi/';
let clients = [];


$(function () {
    $('#clientSearchForm input[name=search]').typeahead({
        hint: true,
        highlight: true,
        minLength: 2,
        source: function (query, process) {
            fetch(`${uri}Search/${query}`)
                .then(response => response.json())
                .then((data) => {
                    var parsed = data.map(function (x) {
                        var mapped = { ...x }
                        return `${mapped.name}`
                    });

                    return process(parsed);
                });
        }
    });
});


function setEvents() {
    $(document).on("click", "#add-client-init", function (e) {
        e.stopImmediatePropagation();
        addClient();
    });

    $(document).on("click", ".edit-button", function (e) {
        e.stopImmediatePropagation();
        displayEditForm($(this).data('id'));
    });

    $(document).on("click", ".delete-button", function (e) {
        e.stopImmediatePropagation();
        displayDeleteConfirm($(this).data('id'));
    });

    $(document).on("click", "#edit-client-save", function (e) {
        e.stopImmediatePropagation();
        editClient();
    });

    $(document).on("click", "#delete-client-confirm", function (e) {
        e.stopImmediatePropagation();
        deleteClient();
    });
}

async function getClients() {
    clients = [];

    const urlParams = new URLSearchParams(window.location.search);
    const searchParam = urlParams.get('search');

    let response = null;

    if (searchParam) {
        response = await fetch(`${uri}Search/${searchParam}`);
    }
    else {
response = await fetch(uri);
    }
    
    

    if (response.ok) {
        await response.json()
            .then(data => _displayClients(data))
            .catch(error => {
                console.error('Unable to get clients.', error)
            });
    };
}

async function addClient() {
    clearModalErrors();

    const addClientNameTextbox = document.querySelector('[name=add-client-name]');
    const addWebSiteTextbox = document.querySelector('[name=add-client-website]');
    const addDirectorNameTextbox = document.querySelector('[name=add-client-director-name]');
    const addClientEmailAddressTextbox = document.querySelector('[name=add-client-email-address]');

    const item = {
        name: addClientNameTextbox.value.trim(),
        webSite: addWebSiteTextbox.value.trim(),
        directorName: addDirectorNameTextbox.value.trim(),
        emailAddress: addClientEmailAddressTextbox.value.trim()
    };

    let response = await fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    });

    if (response.ok) {
        let json = await response.json()
            .then(() => {
                addClientNameTextbox.value = '';
                addWebSiteTextbox.value = '';
                addDirectorNameTextbox.value = '';
                addClientEmailAddressTextbox.value = '';
                hideAllModals();
                getClients(true);
            })
            .catch(error => {
                console.error('Unable to add client.', error)
            });
    }
    else {
        let errors = await response.json();
        const errorMessage = document.querySelector("#add-client-error-message");
        errors.errors.forEach((error) => {
            errorMessage.innerHTML += `<li>${error.message}</li>`;
        });

    }
}

async function editClient() {
    clearModalErrors();

    const editClientId = document.querySelector('[name=edit-client-id]');
    const editClientNameTextbox = document.querySelector('[name=edit-client-name]');
    const editWebSiteTextbox = document.querySelector('[name=edit-client-website]');
    const editDirectorNameTextbox = document.querySelector('[name=edit-client-director-name]');
    const editClientEmailAddressTextbox = document.querySelector('[name=edit-client-email-address]');

    const item = {
        id: editClientId.value.trim(),
        name: editClientNameTextbox.value.trim(),
        webSite: editWebSiteTextbox.value.trim(),
        directorName: editDirectorNameTextbox.value.trim(),
        emailAddress: editClientEmailAddressTextbox.value.trim()
    };

    let response = await fetch(`${uri}${item.id}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    });

    if (response.ok) {
        let json = await response.json()
            .then(() => {
                editClientNameTextbox.value = '';
                editWebSiteTextbox.value = '';
                editDirectorNameTextbox.value = '';
                editClientEmailAddressTextbox.value = '';
                hideAllModals();
                getClients();
            })
            .catch(error => {
                console.error('Unable to update client.', error)
            });
    }
    else {
        let errors = await response.json();
        const errorMessage = document.querySelector("#edit-client-error-message");
        errors.errors.forEach((error) => {
            errorMessage.innerHTML += `<li>${error.message}</li>`;
        });

    }
}

async function deleteClient() {
    const clientId = document.querySelector('[name=delete-client-id]');

    const item = {
        id: clientId.value.trim(),
    };

    let response = await fetch(`${uri}${item.id}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    });

    if (response.ok) {
        hideAllModals();
        getClients();
    }
    else {
        let errors = await response.json();
        const errorMessage = document.querySelector("#delete-client-error-message");
        errors.errors.forEach((error) => {
            errorMessage.innerHTML += `<li>${error.message}</li>`;
        });

    }
}

function displayDeleteConfirm(id) {
    //getClients(false);
    const item = clients.find(item => item.id === id);

    document.querySelector('[name=delete-client-id]').value = id;
    document.querySelector('#delete-client-name').textContent = item.name;

    $("#deleteClientModal").modal('show');
}

function hideAllModals() {
    $("div[id$=Modal]").modal('hide');
    $('.modal-backdrop').hide();
    $("body").removeClass("modal-open");
}

async function displayEditForm(id) {
    await getClients()
        .then(() => {
            const item = clients.find(item => item.id === id);

            document.querySelector('[name=edit-client-id]').value = id;
            document.querySelector('[name=edit-client-name]').value = item.name;
            document.querySelector('[name=edit-client-website]').value = item.webSite;
            document.querySelector('[name=edit-client-director-name]').value = item.directorName;
            document.querySelector('[name=edit-client-email-address]').value = item.emailAddress;
            $("#editClientModal").modal()

        });

}

function _displayClients(data) {

    const tBody = document.querySelector('#clients-table-body');
    tBody.innerHTML = '';

    const linkElement = document.createElement('a');
    const buttonElement = document.createElement('button');

    data.forEach(item => {

        let editButton = buttonElement.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('type', 'button');
        editButton.setAttribute('class', 'btn btn-warning btn-xs edit-button');
        editButton.setAttribute('data-id', item.id);

        let detailsLink = linkElement.cloneNode(false);
        detailsLink.innerText = 'Details';
        detailsLink.setAttribute('class', 'btn btn-primary btn-xs');
        detailsLink.setAttribute('href', `/Home/Details/(${item.id})`);

        let deleteButton = buttonElement.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('type', 'button');
        deleteButton.setAttribute('class', 'btn btn-danger btn-xs delete-button');
        deleteButton.setAttribute('data-id', item.id);

        let tr = tBody.insertRow();
        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(item.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        textNode = document.createTextNode(item.webSite);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        textNode = document.createTextNode(item.directorName);
        td3.appendChild(textNode);

        let td4 = tr.insertCell(3);
        textNode = document.createTextNode(item.emailAddress);
        td4.appendChild(textNode);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);
        td5.appendChild(detailsLink);
        td5.appendChild(deleteButton);
    });

    setEvents();

    clients = data;
}

function clearModalErrors() {
    $("ul[id$=-client-error-message]").empty();
}

getClients();

//setEvents();
