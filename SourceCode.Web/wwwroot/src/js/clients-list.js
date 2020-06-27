import 'bootstrap-3-typeahead';
import 'bootstrap';

const uri = 'api/v1/ClientApi/';

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

    $("#add-client-init").on("click", function () {
        addClient();
    });
});


let clients = [];

function getClients() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayClients(data))
        .catch(error => console.error('Unable to get clients.', error));
}

function addClient() {
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

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            addClientNameTextbox.value = '';
            addWebSiteTextbox.value = '';
            addDirectorNameTextbox.value = '';
            addClientEmailAddressTextbox.value = '';
            document.querySelector('[name=add-client-name]');
            hideAllModals();
            getClients();
        })
        .catch(error => console.error('Unable to add client.', error));
}

function hideAllModals() {
    $("div[id$=Modal]").hide();
    $('.modal-backdrop').hide();
    $("body").removeClass("modal-open");
}
//function deleteItem(id) {
//    fetch(`${uri}/${id}`, {
//        method: 'DELETE'
//    })
//        .then(() => getItems())
//        .catch(error => console.error('Unable to delete item.', error));
//}

//function displayEditForm(id) {
//    const item = todos.find(item => item.id === id);

//    document.getElementById('edit-name').value = item.name;
//    document.getElementById('edit-id').value = item.id;
//    document.getElementById('edit-isComplete').checked = item.isComplete;
//    document.getElementById('editForm').style.display = 'block';
//}

//function updateItem() {
//    const itemId = document.getElementById('edit-id').value;
//    const item = {
//        id: parseInt(itemId, 10),
//        isComplete: document.getElementById('edit-isComplete').checked,
//        name: document.getElementById('edit-name').value.trim()
//    };

//    fetch(`${uri}/${itemId}`, {
//        method: 'PUT',
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(item)
//    })
//        .then(() => getItems())
//        .catch(error => console.error('Unable to update item.', error));

//    closeInput();

//    return false;
//}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

//function _displayCount(clientCount) {
//    const name = (clientCount === 1) ? 'to-do' : 'to-dos';

//    document.getElementById('counter').innerText = `${itemCount} ${name}`;
//}

function _displayClients(data) {
    const tBody = document.querySelector('#clients-table-body');
    tBody.innerHTML = '';

    //_displayCount(data.length);

    const linkElement = document.createElement('a');

    data.forEach(item => {

        let editLink = linkElement.cloneNode(false);
        editLink.innerText = 'Edit';
        editLink.setAttribute('onclick', `displayEditForm(${item.id})`);

        let detailsLink = linkElement.cloneNode(false);
        detailsLink.innerText = 'Details';
        detailsLink.setAttribute('href', `/Home/Details/(${item.id})`);

        let deleteLink = linkElement.cloneNode(false);
        deleteLink.innerText = 'Delete';
        deleteLink.setAttribute('href', `/Home/Delete/(${item.id})`);

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
        td5.appendChild(editLink);
        td5.appendChild(detailsLink);
        td5.appendChild(deleteLink);
    });

    clients = data;
}

