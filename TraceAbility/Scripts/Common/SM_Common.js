// Convert datetime from Microsoft JsonResult to "MM/dd/yyyy" in javascript.
function SM_ConvertMsJsonDate_ToString(MsJsonDate) {
    //var jsDate = new Date(parseInt(MsJsonDate.substr(6)));  
    //return jsDate.toLocaleDateString();
    var myDate = moment(MsJsonDate);
    //return myDate.format('MM/DD/YYYY HH:mm');
    return myDate.format('MM/DD/YYYY');
}
function SM_ConvertMsJson_DateTimePicker_ToString(MsJsonDate) {
    var myDate = moment(MsJsonDate);
    //return myDate.format('MM/DD/YYYY HH:mm');
    return myDate.format('MM/DD/YYYY');
}
function SM_GetOperatorNameByID(_ID) {
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
            passWordOld = result.PassWord;
            $('#MobileNumber').val(result.MobileNumber);
            $('#FactoryID').val(result.FactoryID);
            $('#RoleID').val(result.RoleID);

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
function Common_GetList_MachineMtnFrequency_HTML()
{
    var string = '<option value="0"></option>';  //luu y: de empty value=0, de bat loi trong insert Machine maintenance content list
    string += '<option value="1">Bảo dưỡng hàng ngày</option>';
    string += '<option value="2">Bảo dưỡng hàng tuần</option>';
    string += '<option value="3">Bảo dưỡng hàng tháng</option>';
    string += '<option value="4">Bảo dưỡng 3 tháng</option>';
    string += '<option value="5">Bảo dưỡng 6 tháng</option>';
    string += '<option value="6">Bảo dưỡng hàng năm</option>';
    return string;
}
function Common_Get_MachineMtnFrequencyName_ById(frequencyId) {
    if (frequencyId == 1) {
        return 'Hàng ngày';
    }
    else if (frequencyId == 2) {
        return 'Hàng tuần';
    }
    else if (frequencyId == 3) {
        return 'Hàng tháng';
    }
    else if (frequencyId == 4) {
        return '3 tháng';
    }
    else if (frequencyId == 5) {
        return '6 tháng';
    }
    else if (frequencyId == 6) {
        return 'Hàng năm';
    }
    return '';
}
function Common_GetShiftNameByID(_id) {
    if (_id == 1) {
        return 'Shift 1';
    }
    else if (_id == 2) {
        return 'Shift 2';
    }
    else if (_id == 3) {
        return 'Shift 3';
    }
    return '';
}

function Common_LoadAllUserToCombobox(objectName) {
    $.ajax({
        url: "/User/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                var html = '';
                $.each(result.lstUser, function (key, item) {
                    html += '<option value="' + item.id + '">' + item.UserName + '</option>';
                });
                $(objectName).html(html);
            }
            if (result.Code == "99")
                alert('Error Load User ' + result.Message);
        },
        error: function (errormessage) {
            alert('Error Common_LoadAllUserToCombobox: ' + errormessage.responseText);
        }
    });
}
function Common_LoadAllUserByFactoryID_ToCombobox(objectName, factoryID) {
    $.ajax({
        url: "/User/GetbyFactoryID",
        type: "GET",
        data: { 'factoryID': factoryID },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00" || result.Code == "01") {
                var html = '';
                $.each(result.lstUser, function (key, item) {
                    html += '<option value="' + item.ID + '">' + item.UserName + '</option>';
                });
                $(objectName).html(html);
                $(objectName).val(""); //Set default value to null.
            }
            if (result.Code == "99")
                alert('Error Load User (Common_LoadAllUserByFactoryID_ToCombobox()) ' + result.Message);
            console.log('1. Common_LoadAllUserByFactoryID_ToCombobox');
            console.log('2.  ' + $(objectName).val());
        },
        error: function (errormessage) {
            alert('Error Common_LoadAllUserByFactoryID_ToCombobox: ' + errormessage.responseText);
        }
    });
}
function Common_LoadAllFactoryToCombobox(objectName) {
    var html = '';
    html += '<option value=\"F1\">F1</option>';
    html += '<option value=\"F2\">F2</option>';
    html += '<option value=\"F3\">F3</option>';
    html += '<option value=\"F4\">F4</option>';
    html += '<option value=\"F5\">F5</option>';
    $(objectName).html(html);
}
function Common_LoadIsActiveToCombobox(objectName) {
    var html = '';
    html += '<option value=\"true\">Đang sử dụng/Active</option>';
    html += '<option value=\"false\">Không sử dụng/InActive</option>';
    $(objectName).html(html);
}
function Common_GetActiveNameByID(_id) {
    var returnvalue ='';
    if (_id == true) {
        returnvalue = 'Đang sử dụng/Active';
        //returnvalue = '<input type="checkbox" checked data-toggle="toggle" data-size="small" disabled data-on="On" data-off="Off">';

        //returnvalue  = '<div class="custom-control custom-switch">'
        //returnvalue += '<input type="checkbox" checked disabled class="custom-control-input" id="customSwitches">'
        //returnvalue += '<label class="custom-control-label" for="customSwitches">On</label>'
        //returnvalue += '</div>';
    }
    else if (_id == false) {
        returnvalue = 'Không sử dụng/InActive';

        //returnvalue = '<input type="checkbox" data-toggle="toggle" data-size="small" disabled data-on="On" data-off="Off">';

        //returnvalue = '<div class="custom-control custom-switch">'
        //returnvalue += '<input type="checkbox" disabled class="custom-control-input" id="customSwitches">'
        //returnvalue += '<label class="custom-control-label" for="customSwitches">Off</label>'
        //returnvalue += '</div>';
    }
    return returnvalue;

    //if (_id == true) {
    //    return '<input type="checkbox" checked data-toggle="toggle" data-size="mini" disabled>';
    //}
    //else if (_id == false) {
    //    return '<input type="checkbox" data-toggle="toggle" data-size="mini" disabled>';
    //}
    //return '';
}

//Machine Mtn
var Common_MachineMtn_FrequencyID = {
    Daily: 1,
    Weekly: 2,
    Monthly: 3,
    ThreeMonths: 4,
    SixMonths: 5,
    Yearly: 6
}

function Common_setColorForOKNG(OKNG_value)
{
    if (OKNG_value == 'OK')
        return 'Blue';
    if (OKNG_value == 'NG')
        return 'Red';
}
//Image
function Common_Image_ConvertJsonToCLientUrl(serverLink)
{
    var img = serverLink.replace('~', "..");
}