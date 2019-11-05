//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    loadParentMenu();
    MenuID = 0;
});

function loadData() {
    $.ajax({
        url: "/Menu/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                
                if (item.ParentMenuID == 0)
                    html += '<tr class=\"bg-yellow\">';   //set color for Parent ID>
                else
                    html += '<tr>';
                html += '<td>' + item.MenuName + '</td>';
                html += '<td>' + item.DisplayName + '</td>';
                html += '<td>' + item.ParentMenuID + '</td>';
                html += '<td>' + item.UrlLink + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleMenuByID(\'' + item.ID + '\')">Delete</a></td>';
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
    $('#MenuName').val("");
    $('#DisplayName').val("");
    $('#ParentMenuID').val("");
    $('#UrlLink').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Menu ");

    $('#MenuName').css('border-color', 'lightgrey');
    $('#DisplayName').css('border-color', 'lightgrey');
    $('#ParentMenuID').css('border-color', 'lightgrey');
    $('#UrlLink').css('border-color', 'lightgrey');
}

function GetbyID(ID) {
    $('#MenuName').css('border-color', 'lightgrey');
    $('#DisplayName').css('border-color', 'lightgrey');
    $('#ParentMenuID').css('border-color', 'lightgrey');
    $('#UrlLink').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Menu/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            MenuID = result.ID;
            $('#MenuName').val(result.MenuName);
            $('#DisplayName').val(result.DisplayName);
            $('#ParentMenuID').val(result.ParentMenuID);
            $('#UrlLink').val(result.UrlLink);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Menu ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#MenuName').val().trim() == "") {
        $('#MenuName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MenuName').css('border-color', 'lightgrey');
    }
    if ($('#DisplayName').val().trim() == "") {
        $('#DisplayName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DisplayName').css('border-color', 'lightgrey');
    }
    if ($('#ParentMenuID').val().trim() == "") {
        $('#ParentMenuID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ParentMenuID').css('border-color', 'lightgrey');
    }    
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertMenu() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var MenuObj = {
        ID: 0,
        MenuName: $('#MenuName').val(),
        DisplayName: $('#DisplayName').val(),
        ParentMenuID: $('#ParentMenuID').val(),
        isActive: 1,
        UrlLink: $('#UrlLink').val()
    };
    $.ajax({
        url: "/Menu/Insert/",
        data: JSON.stringify(MenuObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadParentMenu();
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function UpdateMenu() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var MenuObj = {
        ID: MenuID,
        MenuName: $('#MenuName').val(),
        DisplayName: $('#DisplayName').val(),
        ParentMenuID: $('#ParentMenuID').val(),
        isActive: 1,
        UrlLink: $('#UrlLink').val()
    };
    $.ajax({
        url: "/Menu/Insert/",
        data: JSON.stringify(MenuObj),
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

function DeleleMenuByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/Menu/Delete/" + ID,
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
function loadParentMenu() {
    $.ajax({
        url: "/Menu/GetbyParentMenuID/",
        data: { 'ParentMenuID': 0 },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var options = '<option value="0" selected="selected">0</option>';
            $.each(result, function (key, item) {
                options += '<option value="' + item.ID + '">' + item.MenuName + '</option>';
            });
            $('#ParentMenuID').html(options);
        },
    });
}








