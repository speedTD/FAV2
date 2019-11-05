//Load Data in Table when documents is ready  
$(document).ready(function () {
    Search();
    //loadData();
    loadDataPermission1();
    ID = 0;
    userID = 0;
});

function loadData() {
    $.ajax({
        url: "/User/SelectDistinctUserID",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.UserName + '</td>';
                html += '<td><a href="#" onclick="return GetbyUserID(\'' + item.ID + '\',\'' + item.UserName + '\')">Edit</a> | <a href="#" onclick="return DeleleByUserID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
        }
    });
}
function Search() {
    var UserNameSearch = $('#UserNameSearch').val();
    var where = "1=1";
    if (UserNameSearch.length > 0) {
        where += " AND tUser.UserName= '" + UserNameSearch + "' ";
    }
    $.ajax({
        url: "/User/SearchByUserName",
        data: { 'where': where },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.UserName + '</td>';
                html += '<td><a href="#" onclick="return GetbyUserID(\'' + item.ID + '\',\'' + item.UserName + '\')">Edit</a> | <a href="#" onclick="return DeleleByUserID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
        }
    });
}
//function loadDataPermission() {
//    LoadDataUserName();
//    $('#UserName').val("");
//    $('#UserName').prop('disabled', false);
//    $('#btnUpdate').hide();
//    $('#btnAdd').show();
//    $('#myModalLabel').text("Add New UserMenu ");
//    $.ajax({
//        url: "/Menu/SelectMenuLevel2/",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var html = '';
//            $.each(result, function (key, item) {
//                html += '<tr>';
//                html += '<td style="display:none">' + '0' + '</td>';
//                html += '<td>' + item.ID + '</td>';
//                html += '<td> - ' + item.MenuName + '</td>';
//                html += '<td><input type="checkbox"  name="View" id="View" AutoPostBack = "true"></input></td>';
//                html += '<td><input type="checkbox"  name="Add" id="Add" AutoPostBack = "true"></input></td>';
//                html += '<td><input type="checkbox"  name="Edit" id="Edit" AutoPostBack = "true"></input></td>';
//                html += '<td><input type="checkbox"  name="Delete" id="Delete" AutoPostBack = "true"></input></td>';
//                html += '</tr>';
//            });
//            $('.tPermission').html(html);
//        },
//    });
//}

function GetbyUserID(UserID, UserName) {
    userID = UserID;
    var options = '<option value="' + UserID + '">' + UserName + '</option>';
    $('#UserName').html(options);
    $('#UserName').prop('disabled', true);
    $.ajax({
        url: "/UserMenu/GetbyUserID/",
        data: { 'UserID': UserID },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                ID = item.ID;
                html += '<tr>';
                html += '<td style="display:none">' + item.ID + '</td>';
                html += '<td>' + item.MenuID + '</td>';
                html += '<td>' + item.MenuName + '</td>';
                if (item.Pemission.search("View") >= 0) {
                    html += '<td><input type="checkbox"  name="View" id="View" AutoPostBack = "true" checked = "true"></input></td>';
                }
                else {
                    html += '<td><input type="checkbox"  name="View" id="View" AutoPostBack = "true" ></input></td>';
                }
                if (item.Pemission.search("Add") >= 0) {
                    html += '<td><input type="checkbox"  name="Add" id="Add" AutoPostBack = "true" checked = "true"></input></td>';
                }
                else {
                    html += '<td><input type="checkbox"  name="Add" id="Add" AutoPostBack = "true" ></input></td>';
                }
                if (item.Pemission.search("Edit") >= 0) {
                    html += '<td><input type="checkbox"  name="Edit" id="Edit" AutoPostBack = "true" checked = "true"></input></td>';
                }
                else {
                    html += '<td><input type="checkbox"  name="Edit" id="Edit" AutoPostBack = "true" ></input></td>';
                }
                if (item.Pemission.search("Delete") >= 0) {
                    html += '<td><input type="checkbox"  name="Delete" id="Delete" AutoPostBack = "true" checked = "true"></input></td>';
                }
                else {
                    html += '<td><input type="checkbox"  name="Delete" id="Delete" AutoPostBack = "true" ></input></td>';
                }
                html += '</tr>';
            });
            $('.tPermission').html(html);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update UserMenu ');
        },
        error: function (errormessage) {
            //alert('Error' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertUserMenu() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var t = document.getElementById('tblMenu');  
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var menuID = t.rows[j].cells[1].firstChild.nodeValue;
        var permission = "";
        if (inputs[0].checked == true) {
            permission += "View,";
        }
        if (inputs[1].checked == true) {
            permission += "Add,";
        }
        if (inputs[2].checked == true) {
            permission += "Edit,";
        }
        if (inputs[3].checked == true) {
            permission += "Delete,";
        }
        var UserMenuObj = {
            ID: 0,
            UserID: $('#UserName').val(),
            MenuID: menuID,
            Pemission: permission
        };
        $.ajax({
            url: "/UserMenu/Insert/",
            data: JSON.stringify(UserMenuObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    alert("Insert success!");
    LoadDataUserName();
    loadData();
}

function UpdateUserMenu() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var t = document.getElementById('tblMenu');
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var id = t.rows[j].cells[0].firstChild.nodeValue;
        var menuID = t.rows[j].cells[1].firstChild.nodeValue;
        var permission = "";
        if (inputs[0].checked == true) {
            permission += "View,";
        }
        if (inputs[1].checked == true) {
            permission += "Add,";
        }
        if (inputs[2].checked == true) {
            permission += "Edit,";
        }
        if (inputs[3].checked == true) {
            permission += "Delete,";
        }
        var UserMenuObj = {
            ID: id,
            UserID: userID,
            MenuID: menuID,
            Pemission: permission
        };
        $.ajax({
            url: "/UserMenu/Insert/",
            data: JSON.stringify(UserMenuObj),
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
}

function DeleleUserMenuByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/UserMenu/Delete/" + ID,
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
function DeleleByUserID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/UserMenu/DeleteByUserID/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
                LoadDataUserName();
                alert("Deleted successful!");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function LoadDataUserName() {
    $.ajax({
        url: "/User/UserExceptUserMenu/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var options;
            $.each(result, function (key, item) {
                options += '<option value="' + item.ID + '">' + item.UserName + '</option>';
            });
            $('#UserName').html(options);
        },
    });
}
function loadDataPermission1() {
    LoadDataUserName();
    $('#UserName').val("");
    $('#UserName').prop('disabled', false);
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Permission! ");
    $.ajax({
        url: "/Menu/SelectTempSorted/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td style="display:none">' + '0' + '</td>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.MenuName + '</td>';
                html += '<td><input type="checkbox"  name="View" id="View" AutoPostBack = "true"></input></td>';
                html += '<td><input type="checkbox"  name="Add" id="Add" AutoPostBack = "true"></input></td>';
                html += '<td><input type="checkbox"  name="Edit" id="Edit" AutoPostBack = "true"></input></td>';
                html += '<td><input type="checkbox"  name="Delete" id="Delete" AutoPostBack = "true"></input></td>';
                html += '</tr>';
            });
            $('.tPermission').html(html);
        },
    });
}

