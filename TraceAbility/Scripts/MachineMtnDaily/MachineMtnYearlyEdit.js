//Get id from edit button.
var MachineMtnID = 0;
var url_String = document.URL;
var url_HTML = new URL(url_String);
var MachineMtnYearID = url_HTML.searchParams.get("id");
var checkOK = "OK";
var loginUserID = 0;
//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#MaintenanceDate').datepicker();
    LoadDataLine();
    loadDataMachienMtn(MachineMtnYearID);
    loadDataMachineMtnDaily(MachineMtnYearID);
});

function loadDataMachienMtn(MachineMtnID) {
    $.ajax({
        url: "/MachineMtnDaily/GetMachineMtnbyID/",
        data: { 'MachineMtnID': MachineMtnID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loginUserID = result.UserID;
            $('#OperatorName').val(result.UserName);
            $('#Frequency').val(result.lstMachineMtn[0].FrequencyID);
            $('#Frequency').prop('disabled', true);
            $('#Year').val(result.lstMachineMtn[0].Year);
            $('#Year').prop('disabled', true);
            $('#Machine').val(result.lstMachineMtn[0].MachineID);
            $('#Machine').prop('disabled', true);
            $('#MaintenanceDate').val(SM_ConvertMsJsonDate_ToString(result.lstMachineMtn[0].MaintenanceDate));
            $('#MaintenanceDate').prop('disabled', true);
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}
function loadDataMachineMtnDaily(MachineMtnID) {
    $.ajax({
        url: "/MachineMtnDaily/GetMachineMtnDetail/",
        data: { 'MachineMtnID': MachineMtnID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.MachineMtnContentID + '</td>';
                html += '<td>' + item.MachinePartName + '</td>';
                html += '<td>' + item.ContentMtn + '</td>';
                html += '<td>' + item.ToolMtn + '</td>';
                html += '<td>' + item.MethodMtn + '</td>';
                if (item.Result == "OK") {
                    html += '<td><input type="checkbox" name="OK" id="OK' + item.ID + '" checked="true" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="NG' + item.ID + '" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                }
                if (item.Result == "NG") {
                    html += '<td><input type="checkbox" name="OK" id="OK' + item.ID + '" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="NG' + item.ID + '" checked="true" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                }
                if (item.Result == " ") {
                    html += '<td><input type="checkbox" name="OK" id="OK' + item.ID + '" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="NG' + item.ID + '" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                }
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}
function loadMachineMtnDetailNew(MachineID, FrequencyID) {
    $.ajax({
        url: "/MachineMtnContentList/GetByMachineID/",
        data: { 'MachineID': MachineID, 'FrequencyID': FrequencyID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td style="display:none">' + item.ID + '</td>';
                html += '<td>' + item.MachinePartName + '</td>';
                html += '<td>' + item.ContentMtn + '</td>';
                html += '<td>' + item.ToolMtn + '</td>';
                html += '<td>' + item.MethodMtn + '</td>';
                html += '<td><input type="checkbox" name="OK" id="OK' + item.ID + '" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="NG' + item.ID + '" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}
function check_CheckedChangedOK(ID) {
    document.getElementById("NG" + ID).checked = false;
}
function check_CheckedChangedNG(ID) {
    document.getElementById("OK" + ID).checked = false;
}

function clearTextBox() {
    $("#Line").val("");
    $("#Machine").val("");
    $("#Year").val("");
    //$('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Machine Maintenance Yearly");

    $('#Line').css('border-color', 'lightgrey');
    $('#Machine').css('border-color', 'lightgrey');
}


function validate() {
    var isValid = true;

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

function InsertMachineMtnYearly() {
    DeleleByID(MachineMtnYearID);
    InsertMachineMtn();
}
// **** Region CRUD ***************************************************** 
function InsertMachineMtn() {
    var res = validate();
    /* Check Result Machine OK or NG*/
    var t = document.getElementById('tblMachineMtnContentList');
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        if (inputs[1].checked == true) {
            checkOK = "NG";
        }
    }
    var MachineMtnObj = {
        OperatorID: loginUserID,
        MachineID: $('#Machine').val(),
        MaintenanceDate: $('#MaintenanceDate').val(),
        Month: 0,
        Year: $('#Year').val(),
        Result: checkOK,
        FrequencyID: $('#Frequency').val(),
        Shift: 0
    };
    $.ajax({
        url: "/MachineMtnDaily/InsertMachineMtn/",
        data: JSON.stringify(MachineMtnObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            MachineMtnIDLast = result.LastID;
            InsertMachineMtnDetail();
            //loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//insert machineMtnDaily List
function InsertMachineMtnDetail() {
    var res = validate();
    var t = document.getElementById('tblMachineMtnContentList');

    //loops through rows    
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var ID = t.rows[j].cells[0].firstChild.nodeValue;
        var machineMtnContentID = t.rows[j].cells[1].firstChild.nodeValue;
        if (inputs[0].checked == true) {
            checkResult = "OK";
        }
        if (inputs[1].checked == true) {
            checkResult = "NG";
        }
        var MachineMtnDetail = {
            ID: 0,
            MachineMtnID: MachineMtnIDLast,
            MachineMtnContentID: machineMtnContentID,
            Result: checkResult
        };
        $.ajax({
            url: "/MachineMtnDaily/InsertMachineMtnDetail/",
            data: JSON.stringify(MachineMtnDetail),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                //loadData();
                $('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    alert("Update Machine Mtn Detail Year");
}
function DeleleByID(ID) {
    $.ajax({
        url: "/MachineMtnDaily/Delete/" + ID,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function LoadDataLine() {
    $.ajax({
        url: "/Line/ListAll/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var options;
            $.each(result, function (key, item) {
                options += '<option value="' + item.LineID + '">' + item.LineName + '</option>';
            });
            $('#Line').html(options);
        },
    });
}