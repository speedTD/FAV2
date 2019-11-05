//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/ToolTypeList/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ToolTypeID + '</td>';
                html += '<td>' + item.ToolTypeName + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ToolTypeID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ToolTypeID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#ToolTypeID').val("");
    $('#ToolTypeName').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Tool ");

    $('#ToolTypeID').css('border-color', 'lightgrey');
    $('#ToolTypeName').css('border-color', 'lightgrey');
}

function GetbyID(toolTypeID) {
    $('#ToolTypeID').css('border-color', 'lightgrey');
    $('#ToolTypeName').css('border-color', 'lightgrey');

    $.ajax({
        url: "/ToolTypeList/GetbyID/",
        data: { 'toolTypeID': toolTypeID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ToolTypeID').val(result.ToolTypeID);
            $('#ToolTypeID').prop('disabled', true);
            $('#ToolTypeName').val(result.ToolTypeName);
            $('#isActive').val(result.isActive);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Tool ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}
function validate() {
    var isValid = true;
    if ($('#ToolTypeID').val().trim() == "") {
        $('#ToolTypeID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ToolTypeID').css('border-color', 'lightgrey');
    }
    if ($('#ToolTypeName').val().trim() == "") {
        $('#ToolTypeName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ToolTypeName').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertToolType() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ToolTypeObj = {
        ToolTypeID: $('#ToolTypeID').val(),
        ToolTypeName: $('#ToolTypeName').val()
    };
    $.ajax({
        url: "/ToolTypeList/Insert/",
        data: JSON.stringify(ToolTypeObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function UpdateToolType() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ToolTypeObj = {
        ToolTypeID: $('#ToolTypeID').val(),
        ToolTypeName: $('#ToolTypeName').val()
    };
    $.ajax({
        url: "/ToolTypeList/Insert/",
        data: JSON.stringify(ToolTypeObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleleByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/ToolTypeList/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
                alert("Deleted successful!");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}


