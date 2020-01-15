// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addTaskButton() {
    reload('/Home/Add', 'add task');
}

function deleteTaskButton(id, userId) {
    reloadAndRefresh({ url: 'api/todo/remove/' + id + '/' + userId, actionType: 'Delete' });
    sleepFor(100);
    location.reload();
}

function editTaskButton(id, userId) {
    reload('/Home/Update/' + id + '/' + userId, 'update task');
}

function SendForm(parameters) {
    var $form = $('#formToSend');
    var data = getFormData($form);
    console.log(data);
    console.log(JSON.stringify(data));
    var result = false;
    if (parameters.id == null) {
        var addUrl = '/api/todo/add/' + parameters.userId;
        result = reloadAndRefresh({ url: addUrl, actionType: "post", data: data });
    }
    else
        result = reloadAndRefresh({
            url: '/api/todo/update/' + parameters.id + '/' + parameters.userId,
            actionType: 'PUT',
            data: data
        });

    sleepFor(100);
    location.reload();
    return false;
}

function sleepFor(sleepDuration) {
    var now = new Date().getTime();
    while (new Date().getTime() < now + sleepDuration) { /* do nothing */ }
}

function reload(url, headerTitle) {
    setPageTitle(headerTitle);

    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            $('#partialView').html(data);
            $('#partialView').show();
        }
    });
}

function reloadAndRefresh(parameters) {
    var url = parameters.url;
    var actionType = parameters.actionType;
    var data = parameters.data;

    console.log(data);
    $.ajax({
        type: actionType,
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            location.reload();
            return true;
        }
    });
    return false;
}

function setPageTitle(title) {
    if (title !== '')
        document.title = title;
}

function closePopoutWindow() {
    $('#popout-background')[0].style.display = "none";
}

function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}