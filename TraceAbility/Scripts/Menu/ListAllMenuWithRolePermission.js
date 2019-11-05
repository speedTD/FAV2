$(document).ready(function () {
    $('#Role').val("");
    LoadAllRoleToCombobox('#Role');
    $('#Role').change(function () {
        $('.checkAll').checked = false;
        GetFullMenuIncludeRolePermission($('#Role').val());
    });
});
function LoadAllRoleToCombobox(objectName) {
    $.ajax({
        url: "/Role/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                var html = '';
                $.each(result.lstRole, function (key, item) {
                    html += '<option value="' + item.ID + '">' + item.RoleName + '</option>';
                });
                $(objectName).html(html);
                $(objectName).val(""); //Set default value to null.
            }
            if (result.Code == "99")
                alert('Error Load Role ' + result.Message);
        },
        error: function (errormessage) {
            alert('Error LoadAllRoleToCombobox: ' + errormessage.responseText);
        }
    });
}
    
function SaveRoleWithPermissionMenus()
{
    var RoleMenus = {
        lstMenuRole: []
    }
    //loop table.
    var rows = document.getElementById('RoleTable').rows;
    for (var i = 1 ; i < rows.length; i++) {  // ko tinh tieu de
        var strPermssion = "";
        var myrow = rows[i];
        var myCheckBoxView = myrow.getElementsByClassName('myCheckBoxView')[0];  //checkbox
        if (myCheckBoxView != null) {
            if (myCheckBoxView.checked)
                strPermssion = "View";
        }
        var myCheckBoxAdd = myrow.getElementsByClassName('myCheckBoxAdd')[0];  //checkbox
        if (myCheckBoxAdd != null) {
            if (myCheckBoxAdd.checked)
                strPermssion = strPermssion + ".Add";
        }
        var myCheckBoxEdit = myrow.getElementsByClassName('myCheckBoxEdit')[0];  //checkbox
        if (myCheckBoxEdit != null) {
            if (myCheckBoxEdit.checked)
                strPermssion = strPermssion + ".Edit";
        }
        var myCheckBoxDelete = myrow.getElementsByClassName('myCheckBoxDelete')[0];  //checkbox
        if (myCheckBoxDelete != null) {
            if (myCheckBoxDelete.checked)
                strPermssion = strPermssion + ".Delete";
        }
        var myCheckBoxCheck = myrow.getElementsByClassName('myCheckBoxCheck')[0];  //checkbox
        if (myCheckBoxCheck != null) {
            if (myCheckBoxCheck.checked)
                strPermssion = strPermssion + ".Check";
        }
        RoleMenus.lstMenuRole.push({ ID: myrow.cells[0].innerHTML, RoleID: $('#Role').val(), Permission: strPermssion });
    }    
    console.log(JSON.stringify(RoleMenus));
    //save permission to db.
    $.ajax({
        url: "/Role/SaveRoleWithPermissionMenus",
        data: JSON.stringify(RoleMenus),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                alert("Cập nhật thành công/Save successfully.");
            }
            else if (result.Code == "99")
                alert("Lỗi/Error: " + result.Message);
        },
        error: function (errormessage) {
            alert('Lỗi/Error function SaveRoleWithPermissionMenus(): ' + errormessage.responseText);
        }
    });
}
//---- Event ------
function GetFullMenuIncludeRolePermission(roleID) {
    if (roleID == null) {
        return;
    }
    $.ajax({
        url: "/Role/GetFullMenuIncludeRolePermission",
        data: { 'RoleID': roleID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                var html = '';
                var isChecked = '';
                $.each(result.lstMenuRole, function (key, item) {

                    if (item.ParentMenuID == 0) {
                        //html += (item.ParentMenuID == 0) ? '<tr class=\"bg-yellow\">' : '<tr>';
                        html += '<tr class=\"bg-yellow\">';
                        html += '<td style="display:none;">' + item.ID + '</td>';
                        html += '<td>' + item.DisplayName + '</td>';
                        html += '<td>'   + '</td>';
                        html += '<td>'   + '</td>';
                        html += '<td>'   + '</td>';
                        html += '<td>'   + '</td>';
                        html += '<td>' + '</td>';
                        html += '</tr>';
                    }
                    else
                    {
                        html += '<tr>';
                        html += '<td style="display:none;">' + item.ID + '</td>';
                        html += '<td>' + item.DisplayName + '</td>';
                        isChecked = (item.Permission.includes("View")) ? "checked" : "";
                        html += '<td>' + '<input class=\"myCheckBoxView\" type=\"checkbox\" value=\"\" ' + isChecked + '>' + '</td>';
                        isChecked = (item.Permission.includes("Add")) ? "checked" : "";
                        html += '<td>' + '<input class=\"myCheckBoxAdd\" type=\"checkbox\" value=\"\" ' + isChecked + '>' + '</td>';
                        isChecked = (item.Permission.includes("Edit")) ? "checked" : "";
                        html += '<td>' + '<input class=\"myCheckBoxEdit\" type=\"checkbox\" value=\"\" ' + isChecked + '>' + '</td>';
                        isChecked = (item.Permission.includes("Delete")) ? "checked" : "";
                        html += '<td>' + '<input class=\"myCheckBoxDelete\" type=\"checkbox\" value=\"\" ' + isChecked + '>' + '</td>';
                        isChecked = (item.Permission.includes("Check")) ? "checked" : "";
                        html += '<td>' + '<input class=\"myCheckBoxCheck\" type=\"checkbox\" value=\"\" ' + isChecked + '>' + '</td>';
                        html += '</tr>';
                    }
                });
                $('.tbody').html(html);
            }
            if (result.Code == "99") {
                alert('Error 99 GetFullMenuIncludeRolePermission : ' + result.Message);
            }
        },
        error: function (errormessage) {
            alert('Errormessage GetFullMenuIncludeRolePermission: ' + errormessage.responseText);
        }
    });
}

function toggleCheckAll(e, checkboxClassName) {
    //var t = e.srcElement || e.target;
    //var className = $(t).attr('class'); //get checkbox by classname: myCheckBoxView myCheckBoxAdd myCheckBoxEdit myCheckBoxDelete myCheckBoxCheck
    var checkboxes = document.getElementsByClassName(checkboxClassName);
    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = e.checked; 
    }
}