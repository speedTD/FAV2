//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    RoleID = 0;
});

function loadData() {
    $.ajax({
        url: "/Role/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.lstRole, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.RoleName + '</td>';
                html += '<td>' + item.Title + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleRoleByID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            //alert('Error' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#RoleName').val("");
    $('#Title').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Role ");

    $('#RoleName').css('border-color', 'lightgrey');
    $('#Title').css('border-color', 'lightgrey');
}

function GetbyID(ID) {
    $('#RoleName').css('border-color', 'lightgrey');
    $('#Title').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Role/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            RoleID = result.ID;
            $('#RoleName').val(result.RoleName);
            $('#Title').val(result.Title);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Role ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#RoleName').val().trim() == "") {
        $('#RoleName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#RoleName').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertRole() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var RoleObj = {
        ID: 0,
        RoleName: $('#RoleName').val(),
        Title: $('#Title').val()
    };
    $.ajax({
        url: "/Role/Insert/",
        data: JSON.stringify(RoleObj),
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

function UpdateRole() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var RoleObj = {
        ID: RoleID,
        RoleName: $('#RoleName').val(),
        Title: $('#Title').val()
    };
    $.ajax({
        url: "/Role/Insert/",
        data: JSON.stringify(RoleObj),
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

function DeleleRoleByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/Role/Delete/" + ID,
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



