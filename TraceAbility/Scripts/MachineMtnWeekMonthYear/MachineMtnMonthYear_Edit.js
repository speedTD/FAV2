//Get ID from EDIT button.
var MachineMtnID = 0;
var url_String = document.URL;
var url_HTML = new URL(url_String);
MachineMtnID = url_HTML.searchParams.get("id");
var checkOK = "OK";
//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#MaintenanceDate').datepicker();
    GetMachineMtnbyID(MachineMtnID);
    loadDataMachineMtnDailyDetail(MachineMtnID);
});

function GetMachineMtnbyID(MachineMtnID) {
    $.ajax({
        url: "/MachineMtnMonthYear/GetMachineMtnbyID/",
        data: { 'MachineMtnID': MachineMtnID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#OperatorID').val(result.lstMachineMtn[0].OperatorID);
            $('#OperatorName').val(result.lstMachineMtn[0].OperatorName);
            $('#Frequency').val(result.lstMachineMtn[0].FrequencyID);
            $('#Frequency').prop('disabled', true);
            $('#Shift').val(result.lstMachineMtn[0].Shift);
            $('#Shift').prop('disabled', true);
            $('#MachineID').val(result.lstMachineMtn[0].MachineID);
            $('#MachineID').prop('disabled', true);
            $('#MachineName').val(result.lstMachineMtn[0].MachineName);
            $('#MachineName').prop('disabled', true);
            $('#CheckerResult').val(result.lstMachineMtn[0].CheckerResult);
            $('#MaintenanceDate').val(SM_ConvertMsJsonDate_ToString(result.lstMachineMtn[0].MaintenanceDate));
            $('#MaintenanceDate').prop('disabled', true);

            $('#Week').val(result.lstMachineMtn[0].Week);
            $('#Month').val(result.lstMachineMtn[0].Month);
            $('#Year').val(result.lstMachineMtn[0].Year);

            $('#TotalResult').val(result.lstMachineMtn[0].Result);
            $('#TotalResult').prop('disabled', true);

            displayWeekMonthYearDate_ByFrequency(document.getElementById("Frequency").value);
        },
        error: function (errormessage) {
            alert('Error loadDataMachineMtn' + errormessage.responseText);
        }
    });
}
function displayWeekMonthYearDate_ByFrequency(frequencyID) {
    var selectedValue = frequencyID;
    if (selectedValue == 1) {  // ngay
        $("#div_MaintenanceDate").show();
        $("#div_Shift").show();
        $("#div_Month").hide();
        $("#div_Week").hide();
        $("#div_Month").hide();
        $("#div_Year").hide();
    }
    if (selectedValue == 2) {  // tuan
        $("#div_MaintenanceDate").hide();
        $("#div_Shift").hide();
        $("#div_Week").show();
        $("#div_Month").hide();
        $("#div_Year").show();
    }
    if (selectedValue == 3 || selectedValue == 4 || selectedValue == 5) {  //cac loai thang.
        $("#div_MaintenanceDate").hide();
        $("#div_Shift").hide();
        $("#div_Week").hide();
        $("#div_Month").show();
        $("#div_Year").show();
    }
    if (selectedValue == 6) {  // year
        $("#div_MaintenanceDate").hide();
        $("#div_Shift").hide();
        $("#div_Week").hide();
        $("#div_Month").hide();
        $("#div_Year").show();
    }
}

function loadDataMachineMtnDailyDetail(MachineMtnID) {
    $.ajax({
        url: "/MachineMtnMonthYear/GetMachineMtnDetail/",
        data: { 'MachineMtnID': MachineMtnID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                var html = '';
                $.each(result.lstMachineMtnContentDetail, function (key, item) {
                    html += '<tr class=\"package-row\">';
                    html += '<td style="display:none" class="myID">' + item.ID + '</td>';
                    html += '<td>' + item.MachineMtnContentID + '</td>';
                    html += '<td>' + item.MachinePartName + '</td>';
                    html += '<td>' + item.ContentMtn + '</td>';
                    html += '<td>' + item.ToolMtn + '</td>';
                    html += '<td>' + item.MethodMtn + '</td>';
                    html += '<td>' + item.Standard + '</td>';
                    html += '<td id=\"td_Min\">' + item.Min + '</td>';
                    html += '<td id=\"td_Max\">' + item.Max + '</td>';
                    html += '<td><input id="td_ActualValue" type="text" class="myRowActualValue" onkeyup="return calculate_ActualResult(this,' + '\'' + item.ID + '\'' + ')" class="form-control" value='+ item.ActualValue +'></input></td>';
                   
                    //checkpermissionVM
                    var isDisableEdit = '';
                    if (result.permisionControllerVM.isAllow_View && !result.permisionControllerVM.isAllow_Edit)  //if user permision is View and not Edit.
                        isDisableEdit = 'disabled';

                    // checkbox OK NG.
                    html += '<td>';
                    html += '<div class="checkbox">';
                    if (item.Result == "OK") {
                        html += '<label class="checkbox-inline"><input type="checkbox" ' + isDisableEdit + ' class="myRowCheckbok" name="OK" id="OK' + item.ID + '" checked="true" onchange="check_CheckedChangedOK(\'' + item.ID + '\')">OK</label>';
                        html += '<label class="checkbox-inline"><input type="checkbox" ' + isDisableEdit + '  class="myRowCheckbok" name="NG" id="NG' + item.ID + '"  onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</label>';
                        //html += '<td><input ' + isDisableEdit + ' type="checkbox" name="OK" id="OK' + item.ID + '" checked="true" AutoPostBack = "false" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input ' + isDisableEdit + ' type="checkbox" name="NG" id="NG' + item.ID + '" AutoPostBack="false" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                    }
                    if (item.Result == "NG") {
                        html += '<label class="checkbox-inline"><input type="checkbox" ' + isDisableEdit + ' class="myRowCheckbok" name="OK" id="OK' + item.ID + '"  onchange="check_CheckedChangedOK(\'' + item.ID + '\')">OK</label>';
                        html += '<label class="checkbox-inline"><input type="checkbox" ' + isDisableEdit + '  class="myRowCheckbok" name="NG" id="NG' + item.ID + '" checked="true"   onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</label>';
                        //html += '<td><input ' + isDisableEdit + ' type="checkbox" name="OK" id="OK' + item.ID + '" AutoPostBack = "false" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input ' + isDisableEdit + ' type="checkbox" name="NG" id="NG' + item.ID + '" checked="true" AutoPostBack="false" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                    }
                    if (item.Result == "") {
                        html += '<label class="checkbox-inline"><input type="checkbox" ' + isDisableEdit + ' class="myRowCheckbok" name="OK" id="OK' + item.ID + '"  onchange="check_CheckedChangedOK(\'' + item.ID + '\')">OK</label>';
                        html += '<label class="checkbox-inline"><input type="checkbox" ' + isDisableEdit + '  class="myRowCheckbok" name="NG" id="NG' + item.ID + '" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')">NG</label>';
                        //html += '<td><input ' + isDisableEdit + ' type="checkbox" name="OK" id="OK' + item.ID + '" AutoPostBack = "false" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input ' + isDisableEdit + ' type="checkbox" name="NG" id="NG' + item.ID + '" AutoPostBack="false" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                    }
                    html += '</div></td>';

                    html += '<td><input class="myResultContents" type="text" class="form-control" value="'+ item.ResultContents+'"></input></td>'
                    html += '</tr>';
                });
                $('.tbody').html(html);
                Refresh_Status_OKNG();
                
            }
            else if (result.Code == "")
                alert('Lỗi/Error loadDataMachineMtnDailyDetail: ' + result.Message);
        },
        error: function (errormessage) {
            alert('Lỗi/Error loadDataMachineMtnDailyDetail' + errormessage.responseText);
        }
    });
}
//function check_CheckedChangedOK(ID) {
//    document.getElementById("NG" + ID).checked = false;
//    cal_TotalResultMachineMtn();
//}
//function check_CheckedChangedNG(ID) {
//    document.getElementById("OK" + ID).checked = false;
//    cal_TotalResultMachineMtn();
//}

function check_CheckedChangedOK(ID) {  // if choosing OK.
    if (document.getElementById("OK" + ID).checked) {
        document.getElementById("OK" + ID).parentNode.style["color"] = "blue";
        document.getElementById("NG" + ID).checked = false;
        check_CheckedChangedNG(ID);
    }
    else
        document.getElementById("OK" + ID).parentNode.style["color"] = "black";

    cal_TotalResultMachineMtn();
}

function check_CheckedChangedNG(ID) {   // if choosing NG.

    if (document.getElementById("NG" + ID).checked) {
        document.getElementById("NG" + ID).parentNode.style["color"] = "red";
        document.getElementById("OK" + ID).checked = false;
        check_CheckedChangedOK(ID);
    }
    else
        document.getElementById("NG" + ID).parentNode.style["color"] = "black";

    cal_TotalResultMachineMtn();
}

function clearTextBox() {
    $('#Operator').val("");
    $("#OperatorName").val("");
    $("#Line").val("");
    $("#Machine").val("");

    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Machine Maintenance Frequency");

    $('#Operator').css('border-color', 'lightgrey');
    $('#OperatorName').css('border-color', 'lightgrey');
    $('#Line').css('border-color', 'lightgrey');
    $('#Machine').css('border-color', 'lightgrey');
}

function validate() {
    var isValid = true;
    if ($('#Operator').val().trim() == "") {
        $('#Operator').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Operator').css('border-color', 'lightgrey');
    }
    if ($('#OperatorName').val().trim() == "") {
        $('#OperatorName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OperatorName').css('border-color', 'lightgrey');
    }
    if ($('#Line').val().trim() == "") {
        $('#Line').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Line').css('border-color', 'lightgrey');
    }
    if ($('#Machine').val().trim() == "") {
        $('#Machine').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Machine').css('border-color', 'lightgrey');
    }
    return isValid;
}

function InsertMachineMtnDaily() {
    UpdateMachineMtn();
}
// **** Region CRUD ***************************************************** 
//function cal_TotalResultMachineMtn() {
//    /* Check Result Machine OK or NG*/
//    checkOK = "OK";
//    var t = document.getElementById('tblMachineMtnContentList');
//    for (var j = 1; j < t.rows.length; j++) {
//        var r = t.rows[j];
//        var inputs = r.getElementsByTagName("input");
//        if (inputs[1].checked == true) {
//            checkOK = "NG";
//        }
//        else if (inputs[0].checked == false && inputs[1].checked == false) {
//            checkOK = "NG";
//        }
//    }
//    $('#TotalResult').val(checkOK);
//}
function cal_TotalResultMachineMtn() {
    /* Check Result Machine OK or NG*/
    checkOK = "OK";
    var t = document.getElementById('tblMachineMtnContentList');
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByClassName("myRowCheckbok");
        if (inputs[1].checked == true) {
            checkOK = "NG";
        }
        else if (inputs[0].checked == false && inputs[1].checked == false) {
            checkOK = "NG";
        }
    }
    $('#TotalResult').val(checkOK);
    if (checkOK == "OK")
        $('#TotalResult').css('background-color', 'blue');
    else if (checkOK == "NG")
        $('#TotalResult').css('background-color', 'red');
}
function UpdateMachineMtn() {
    var MachineMtnObj = {
        ID: MachineMtnID,
        OperatorID: $('#UserLogin').data('id'),
        MachineID: $('#MachineID').val(),
        MaintenanceDate: $('#MaintenanceDate').val(),
        Shift: $('#Shift').val(),
        Week: $('#Week').val(),
        Month: $('#Month').val(),
        Year: $('#Year').val(),
        FrequencyID: $('#Frequency').val(),
        Result: checkOK
    };
    $.ajax({
        url: "/MachineMtnMonthYear/UpdateMachineMtn/",
        data: JSON.stringify(MachineMtnObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                UpdateMachineMtnDetail();
                $('#myModal').modal('hide');
                alert('Cập nhật thành công!/Update successfully!');
            } else if (result.Code == "99") {
                alert('Lỗi cập nhật/Error while updating: ' + result.Message);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//update machineMtnDaily List
function UpdateMachineMtnDetail() {
    var t = document.getElementById('tblMachineMtnContentList');
    //loops through rows    
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];

        //var inputs = r.getElementsByTagName("input");
        var checkBoxs = r.getElementsByClassName("myRowCheckbok");

        var inputs = r.getElementsByTagName("input");
        var machineMtnDetailID = t.rows[j].cells[0].firstChild.nodeValue;
        var machineMtnContentID = t.rows[j].cells[1].firstChild.nodeValue; //content.
        var contentMtn = t.rows[j].cells[3].firstChild != null ? t.rows[j].cells[3].firstChild.nodeValue : '';
        var toolMtn = t.rows[j].cells[4].firstChild != null ? t.rows[j].cells[4].firstChild.nodeValue : '';
        var methodMtn = t.rows[j].cells[5].firstChild != null ? t.rows[j].cells[5].firstChild.nodeValue : '';
        var standard = t.rows[j].cells[6].firstChild != null ? t.rows[j].cells[6].firstChild.nodeValue : '';
        var min = t.rows[j].cells[7].firstChild != null ? t.rows[j].cells[7].firstChild.nodeValue : '';
        var max = t.rows[j].cells[8].firstChild != null ? t.rows[j].cells[8].firstChild.nodeValue : '';
        var actualValue = (r.getElementsByClassName("myRowActualValue"))[0].value;
        var checkResult = "";
        if (checkBoxs[0].checked == true) {  // choose OK
            checkResult = "OK";
        }
        if (checkBoxs[1].checked == true) {  // choose NG
            checkResult = "NG";
        }
        var resultContents = (r.getElementsByClassName("myResultContents"))[0].value;
        var MachineMtnDetail = {
            ID: machineMtnDetailID,
            MachineMtnID: MachineMtnID, //$('#MachineMtnID').val(), 
            MachineMtnContentID: machineMtnContentID,
            Result: checkResult,
            ContentMtn: contentMtn,
            ToolMtn: toolMtn,
            MethodMtn: methodMtn,
            Standard: standard,
            Min: min,
            Max: max,
            ActualValue:actualValue,
            Result: checkResult,
            ResultContents: resultContents.trim()
        };
        $.ajax({
            url: "/MachineMtnMonthYear/UpdateMachineMtnDetail/",
            data: JSON.stringify(MachineMtnDetail),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert(' Lỗi/Error UpdateMachineMtnDetail' + errormessage.responseText);
            }
        });
    }
}
//function DeleleByID(ID) {
//    $.ajax({
//        url: "/MachineMtnMonthYear/Delete/" + ID,
//        type: "POST",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

function CheckerUser_confirmResult(typeOfCheck) {
    if (typeOfCheck == "Add") {
        if (MachineMtnID == 0) {
            alert('Hãy lưu kết quả bảo trì, trước khi Checker xác nhận!/Save machine maintenance before checking!');
            return;
        }
        CheckerInputResult_In_AddNewForm();
    }
    if (typeOfCheck == "List") {

    }
    if (typeOfCheck == "Edit") {
        CheckerInputResult_In_AddNewForm();
    }
}
function CheckerInputResult_In_AddNewForm() {
    var CheckerUserName = $('#CheckerUserName').val();
    var CheckerPassword = $('#CheckerPassword').val();
    var CheckerPermission = 'Check';
    var CheckerResult = $("input[name='CheckerResult_RadioOptions']:checked").val();

    $.ajax({
        url: "/User/CheckPermission_ByUserNameAndPassword/",
        data: { 'userName': CheckerUserName, 'password': CheckerPassword, 'permission': CheckerPermission },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {   //neu user pass dung, va co quyen.
                console.log('user co quyen.');

                var MachineMtnObjCheck = {
                    ID: MachineMtnID,
                    CheckerID: result.lstUser[0].ID,
                    CheckerResult: CheckerResult,
                };
                $.ajax({
                    url: "/MachineMtnMonthYear/UpdateCheckerResult_ByMachineMtnID/",
                    data: JSON.stringify(MachineMtnObjCheck),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result1) {
                        if (result1.Code == '00') {
                            $('#myModal').modal('hide');
                            $('#CheckerResult').val(CheckerResult);
                            alert("Insert check result success!");
                        }
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
            }
            else if (result.Code == '01') {
                alert(" Not permission!");
            } else if (result.Code == '99') {

            }
        },
        error: function (errormessage) {
            alert('Error CheckerInputResult_In_AddNewForm' + errormessage.responseText);
        }
    });
}
function Refresh_Status_OKNG()
{
    var rows = document.querySelectorAll("tr.package-row");
    
        rows.forEach(function (currentRow) {
            var inputs = currentRow.getElementsByClassName("myRowCheckbok");
            var myID = currentRow.getElementsByClassName("myID")[0].innerHTML;
            
            if (inputs[0].checked == true) {
                check_CheckedChangedOK(myID);
            }
            else if (inputs[1].checked == true) {
                check_CheckedChangedNG(myID);
            }
        });
}
// Calculate.
function calculate_ActualResult(thisTDtag, ID) {
    var currentRow = thisTDtag.parentNode.parentNode;

    var td_Min = currentRow.querySelector('#td_Min').innerHTML;
    var td_Max = currentRow.querySelector('#td_Max').innerHTML;
    var td_ActualValue = currentRow.querySelector('#td_ActualValue').value;
    if (!isNaN(parseFloat(td_Min)) && !isNaN(parseFloat(td_Max)) && !isNaN(parseFloat(td_ActualValue))) {  //neu la kieu so.
        {
            var Min;
            var Max;
            var ActualValue;
            Min = parseFloat(td_Min);
            Max = parseFloat(td_Max);
            ActualValue = parseFloat(td_ActualValue);
            //calculate.

            currentRow.querySelector('#td_ActualValue').style["border-color"] = "lightgrey";
            if (ActualValue >= Min & ActualValue <= Max) {
                document.getElementById("OK" + ID).checked = true;
                check_CheckedChangedOK(ID);
            }
            else {
                document.getElementById("NG" + ID).checked = true;
                check_CheckedChangedNG(ID);
            }
        }
    }
}





