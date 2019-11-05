$(document).ready(function () {
    InitForm();
});
//*** init value ****

function InitForm() {
    $('#FromDate').datetimepicker({
        format: 'MM/DD/YYYY', // format you want to show on datetimepicker
        useCurrent: false, // default this is set to true
        defaultDate: new Date()
    });
    $('#ToDate').datetimepicker({
        format: 'MM/DD/YYYY', // format you want to show on datetimepicker
        useCurrent: false, // default this is set to true
        defaultDate: new Date()
    });
    $('#MachineID').keyup(function (event) {
        if ((event.keyCode ? event.keyCode : event.which) == 13) {
            Load_MachineInfo();
        }
    });
}

// **** Event ****
//function createReport() {
    
//    var MachineMtnDetail = {};
//    $.ajax({
//        url: "/MachineMtnReportData/CreateReport/",
//        data: JSON.stringify(MachineMtnDetail),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            if (result.Code == "00") {
//                alert("OK");
//            }
//            if (result.Code == "01") {  // neu chua co.
                
//            }
//            else if (result.Code == "99") {
//                alert('Lỗi kiểm tra checksheet/Error while checking checksheet' + errormessage.responseText);
//            }
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

function isValidateToExportExcel() {
    var isValid = true;
    if ($('#MachineName').val().trim() == "") {
        $('#MachineName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineName').css('border-color', 'lightgrey');
    }
    return isValid;
}

//function ExportExcel(frequencyID)
//{
//    Load_MachineInfo();
//    var res = isValidateToExportExcel();
//    if (res == false) {
//        alert('Không thể export. Hãy kiểm tra thông tin nhập vào./Cannot export. Check input information.');
//        return false;
//    }

//    if (frequencyID = Common_MachineMtn_FrequencyID.Daily)
//    {
//        var MachineMtnReportDataSearchObj =
//        {
//            MachineID: $('#MachineID').val(),
//            MachineName: $('#MachineName').val(),
//            OperatorID: 0,
//            Shift: 0,
//            FrequencyID: frequencyID,
//            MaintenanceDate: null,
//            FromDate: $('#FromDate').data("date"),
//            ToDate: $('#ToDate').data("date"),
//            FromInt: 0,
//            ToInt: 0
//        };
//    }
//    $.ajax({
//        url: "/MachineMtnReportData/CreateReport_2/",
//        data: JSON.stringify(MachineMtnReportDataSearchObj),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "html",
//        success: function (result) {
//            window.location = '@Url.Action("CreateReport_2", "MachineMtnReportData")';

//        },
//        error: function (errormessage) {
//            alert('Lỗi/Error ExportExcel()' + errormessage.responseText);
//        }
//    });
//};

function Load_MachineInfo() {
    var machineID = $('#MachineID').val().trim();    
    $.ajax({
        url: "/Machine/GetbyID_ReturnMachineResult/",
        data: { 'machineID': machineID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                $('#MachineName').val(result.lstMachine[0].MachineName);
                $('#LineID').val(result.lstMachine[0].LineID);
            }
            else if (result.Code == "01") {
                $('#MachineName').val("");
                $('#LineID').val("");
                alert("Not exist MachineID!");
            }
            else if (result.Code == "99") {
                alert('Error Load_MachineInfo - ajax /Machine/GetbyID_ReturnMachineResult/ ' + errormessage.responseText);
            }
        },
        error: function (errormessage) {
            alert('Error Load_MachineInfo(e) ' + errormessage.responseText);
        }
    });
}