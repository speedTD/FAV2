$(document).ready(function () {
    Role_ListAll();
    Common_LoadAllFactoryToCombobox('#Factory');
    
    $('#Factory').change(function () {
        Common_LoadAllUserByFactoryID_ToCombobox('#UserName', $('#Factory').val());
    });
    $('#UserName').change(function () {
        setCheckboxRoleByUserID($('#UserName').val());
    });
});
function Role_ListAll() {
    $.ajax({
        url: "/Role/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.lstRole, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + '<input class=\"myCheckBox\" type=\"checkbox\" value=\"\">' + '</td>';
                html += '<td>' + item.RoleName + '</td>';
                html += '<td>' + item.Title + '</td>';
                //html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleRoleByID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert('Error Role_ListAll: ' + errormessage.responseText);
        }
    });
}
function SaveUserWithRoles()
{
    var UserRolesObject = {
        UserID : $('#UserName').val(),
        Roles : []
    }
    //loop table.
    var rows = document.getElementById('RoleTable').rows;
    for (var i = 1 ; i < rows.length; i++) {  // ko tinh tieu de.
        var myrow = rows[i];
        var checkBox = myrow.getElementsByClassName('myCheckBox')[0];  //checkbox
        if (checkBox != null) {
            UserRolesObject.Roles.push({ ID: myrow.cells[0].innerHTML, RoleName: myrow.cells[2].innerHTML, Checked: checkBox.checked }); //RoleID column.
        }
    }
    $.ajax({
        url: "/UserRole/SaveUserWithRoles",
        data: JSON.stringify(UserRolesObject),
        type: "POST",        
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00")
            {
                alert("Cập nhật thành công/Save successfully.");
            }
            else if (result.Code == "99")
                alert("Lỗi/Error: " + result.Message);
        },
        error: function (errormessage) {
            alert('Lỗi/Error SaveUserWithRoles: ' + errormessage.responseText);
        }
    });
}
function setCheckboxRoleByUserID(userID) {
    if (userID == null) {
        alert("Lỗi userID/ Error UserID");
        return;
    }
    $.ajax({
        url: "/Role/ListByUserID",
        data: { 'userID': userID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            if (result.Code == "00") {
                var rows = document.getElementById('RoleTable').rows;
                for (var i = 1 ; i < rows.length; i++) {  // ko tinh tieu de.
                    var myrow = rows[i];
                    var checkBox = myrow.getElementsByClassName('myCheckBox')[0];  //checkbox
                    if (checkBox != null) {
                        checkBox.checked = false; //clear checkbox.
                        var RoleID_InRow = myrow.cells[0].innerHTML;
                        if (result.lstRole.find(o => o.ID == RoleID_InRow) != null)   // kiem tra RoleID
                            checkBox.checked = true;
                    }
                }
                //Cach 2.
                //for (var i = 1 ; i < rows.length; i++) {  // ko tinh tieu de.
                //    var myrow = rows[i];
                //    var checkBox = myrow.cells[1].firstChild;  //checkbox
                //    checkBox.checked = false; //clear checkbox.
                //    var RoleID_InRow = myrow.cells[0].innerHTML;  
                //    if (result.lstRole.find(o => o.ID == RoleID_InRow) != null)   // kiem tra RoleID
                //        checkBox.checked = true;
                //}
                console.log('4. setCheckboxRoleByUserID');
            }
            if (result.Code == "99") {
                alert('Error 99 setCheckboxRoleByUserID : ' + result.Message);
            }
        },
        error: function (errormessage) {
            alert('Errormessage setCheckboxRoleByUserID: ' + errormessage.responseText);
        }
    });
}
