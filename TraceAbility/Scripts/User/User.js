//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    UserID = 0;
});
var passWordOld = "";
function loadData() {
    $.ajax({
        url: "/User/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                var html = '';
                $.each(result.lstUser, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.FullName + '</td>';
                    html += '<td>' + item.UserName + '</td>';
                    html += '<td>' + item.MobileNumber + '</td>';
                    html += '<td>' + item.FactoryID + '</td>';
                    html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleUserByID(\'' + item.ID + '\')">Delete</a></td>';
                    html += '</tr>';
                });
                $('.tbody').html(html);
            }
            if (result.Code == "99")
                alert('Error Load User ' + result.Message);
        },
        error: function (errormessage) {
            alert('Error loadData()' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#FullName').val("");
    $('#UserName').val("");
    $('#PassWord').val("");
    $('#MobileNumber').val("");
    $('#FactoryID').val("");
    //$('#RoleID').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New User ");

    $('#FullName').css('border-color', 'lightgrey');
    $('#UserName').css('border-color', 'lightgrey');
    $('#PassWord').css('border-color', 'lightgrey');
    $('#MobileNumber').css('border-color', 'lightgrey');
    $('#FactoryID').css('border-color', 'lightgrey');
    //$('#RoleID').css('border-color', 'lightgrey');
}

function GetbyID(ID) {
    $('#FullName').css('border-color', 'lightgrey');
    $('#UserName').css('border-color', 'lightgrey');
    $('#PassWord').css('border-color', 'lightgrey');
    $('#MobileNumber').css('border-color', 'lightgrey');
    $('#FactoryID').css('border-color', 'lightgrey');
    //$('#RoleID').css('border-color', 'lightgrey');

    $.ajax({
        url: "/User/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            UserID = result.ID;
            $('#FullName').val(result.FullName);
            $('#UserName').val(result.UserName);
            //$('#PassWord').val(result.PassWord);
            passWordOld = result.PassWord;
            $('#MobileNumber').val(result.MobileNumber);
            $('#FactoryID').val(result.FactoryID);
            //$('#RoleID').val(result.RoleID);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update User ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#FullName').val().trim() == "") {
        $('#FullName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FullName').css('border-color', 'lightgrey');
    }
    if ($('#UserName').val().trim() == "") {
        $('#UserName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UserName').css('border-color', 'lightgrey');
    }
    if ($('#PassWord').val().trim() == "") {
        $('#PassWord').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PassWord').css('border-color', 'lightgrey');
    }
    var _factoryid = $('#FactoryID').val();
    if (_factoryid == "" || _factoryid==null) {
        $('#FactoryID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FactoryID').css('border-color', 'lightgrey');
    }
    //if ($('#RoleID').val().trim() == "") {
    //    $('#RoleID').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#RoleID').css('border-color', 'lightgrey');
    //}
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertUser() {
    var res = validate();
    if (res == false) {
        return false;
    }
    $.ajax({
        url: "/User/SelectByUserName/",
        data: { 'userName': $('#UserName').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.lstUser != null) {
                alert("User name already exists");
                $('#UserName').css('border-color', 'Red');
            }
            else {
                var UserObj = {
                    ID: 0,
                    FullName: $('#FullName').val(),
                    UserName: $('#UserName').val(),
                    PassWord: $('#PassWord').val(),
                    MobileNumber: $('#MobileNumber').val(),
                    FactoryID: $('#FactoryID').val(),
                    //RoleID: $('#RoleID').val(),
                    isActive: 1
                };
                $.ajax({
                    url: "/User/Insert/",
                    data: JSON.stringify(UserObj),
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
        },
    });    
}

function UpdateUser() {
    var res = validate();
    //if (res == false) {
    //    return false;
    //}
    var UserObj = {
        ID: UserID,
        FullName: $('#FullName').val(),
        UserName: $('#UserName').val(),
        PassWord: passWordOld,
        PassWordNew : $('#PassWord').val(),
        MobileNumber: $('#MobileNumber').val(),
        FactoryID: $('#FactoryID').val(),
        //RoleID: $('#RoleID').val(),
        isActive: 1
    };
    $.ajax({
        url: "/User/Update/",
        data: JSON.stringify(UserObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#PassWord').val("");
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleleUserByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/User/Delete/" + ID,
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



