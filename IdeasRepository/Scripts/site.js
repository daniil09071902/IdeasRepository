
function updateRecords() {
    var currentUrl = $('#currentUrl').val();
    $.ajax({
        url: currentUrl,
        success: function (data) {
            $('#records').html(data);
        },
        error: function (xhr) {
            printError(xhr);
        }
    });
}

function deleteRecord(event) {
    var fieldset = $(event.target).parent();
    var id = fieldset.children().filter('#recordId').first().val();
    $.ajax({
        url: '/Record/Delete',
        dataType: "json",
        type: "DELETE",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ recordId: id }),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            updateRecords();
        },
        error: function (xhr) {
            printError(xhr);
        }
    });
}

function confirmRecordDeletion(event) {
    var fieldset = $(event.target).parent();
    var id = fieldset.children().filter('#recordId').first().val();
    $.ajax({
        url: '/Record/ConfirmDeletion',
        dataType: "json",
        type: "DELETE",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ recordId: id }),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            updateRecords();
        },
        error: function (xhr) {
            printError(xhr);
        }
    });
}

function restoreRecord(event) {
    var fieldset = $(event.target).parent();
    var id = fieldset.children().filter('#recordId').first().val();
    $.ajax({
        url: '/Record/Restore',
        dataType: "json",
        type: "PUT",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ recordId: id }),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            updateRecords();
        },
        error: function (xhr) {
            printError(xhr);
        }
    });
}

function updateRecord(event) {
    var fieldset = $(event.target).parent();
    var note = fieldset.children().filter('#edit_note').first().val();
    if (note.trim().length == 0) {
        return;
    }
    var id = fieldset.children().filter('#recordId').first().val();
    var authorId = fieldset.children().filter('#authorId').first().val();
    $.ajax({
        url: '/Record/Update',
        dataType: "json",
        type: "PUT",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ record: { id: id, text: note, authorId: authorId } }),
        async: true,
        processData: true,
        cache: false,
        success: function (data) {
            updateRecords();
        },
        error: function (xhr) {
            printError(xhr);
        }
    });
}

function saveRecord() {
    if ($('#new_note').val().trim().length == 0) {
        return;
    }
    var note = $('#new_note').val();
    $.ajax({
        url: '/Record/Save',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ record: { text: note } }),
        async: true,
        processData: true,
        cache: false,
        success: function (data) {
            updateRecords();
            $('#new_note').val('');
        },
        error: function (xhr) {
            printError(xhr);
        }
    });
}

function printError(xhr) {
    try {
        var json = $.parseJSON(xhr.responseText);
        alert(json.errorMessage);
    } catch (e) {
        alert('Error happened');
    }
}