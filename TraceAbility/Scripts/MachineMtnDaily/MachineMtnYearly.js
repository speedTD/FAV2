//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#MaintenanceDate').datepicker();
    //checkData();
    LoadDataLine();
    $('#Frequency').prop('disabled', true);
});
var LoginUserID = 0;
var LoginUserName = "";
var _MachineID = $('#Machine').val();
var MachineMtnIDLast = 0;
var ID = 0;
var checkOK = "OK";
$(document).ready(function () {
    $('#Machine').keyup(function () {
        var MachineID = $('#Machine').val();
        var FrequencyID = $('#Frequency').val();
        loadMachineMtnDetailNew(MachineID, FrequencyID);
    });
});
function loadMachineMtnDetailNew(MachineID, FrequencyID) {
    $.ajax({
        url: "/MachineMtnContentList/GetByMachineID/",
        data: { 'MachineID': MachineID, 'FrequencyID': FrequencyID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            LoginUserID = result.UserID;
            LoginUserName = result.UserName;
            var html = '';
            $.each(result.lstMachineMtnContentList, function (key, item) {
                html += '<tr> class=\">' + item.ID + '';
                html += '<td>' + item.ID + '</td>';
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


    //$('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Machine Maintenance Yearly");

    $('#Operator').css('border-color', 'lightgrey');
    $('#OperatorName').css('border-color', 'lightgrey');
    $('#Line').css('border-color', 'lightgrey');
    $('#Machine').css('border-color', 'lightgrey');
}


function validate() {
    var isValid = true;
    if ($('#Machine').val().trim() == "") {
        $('#Machine').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Machine').css('border-color', 'lightgrey');
    }
    if ($('#Year').val().trim() == "") {
        $('#Year').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Year').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertMachineMtnYearly() {
    CheckMachineMtnData();
    //if (ID == 0) {
    //    InsertMachineMtn();
    //}
    //else alert("Machine maintenance already exist");
}

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
        OperatorID: LoginUserID,
        MachineID: $('#Machine').val(),
        MaintenanceDate: $('#MaintenanceDate').val(),
        Shift: 0,
        FrequencyID: $('#Frequency').val(),
        Result: checkOK,
        Month: 0,
        Year: $('#Year').val()
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
        var machineMtnContentID = t.rows[j].cells[0].firstChild.nodeValue;
        var checkResult = " ";
        if (inputs[0].checked == true) {
            checkResult = "OK";
        }
        if (inputs[1].checked == true) {
            checkResult = "NG";
        }
        var MachineMtnDetail = {
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
    alert("Insert machineMtnDetail");
}

function CheckMachineMtnData() {
    $.ajax({
        url: "/MachineMtnDaily/CheckMachineMtnData/",
        data: { 'MachineID': $('#Machine').val(), 'FrequencyID': $('#Frequency').val(), 'MaintenanceDate': $('#MaintenanceDate').val(), 'Shift': $('#Shift').val(), 'OperatorID': $('#Operator').val(), 'Month': 0, 'Year': $('#Year').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "01") {
                InsertMachineMtn();
            }
            if (result.Code == "00") {
                alert("Cannot insert. Machine maintenance already exists.");
            }
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}
function LoadDataLine() {
    $.ajax({
        url: "/Line/List/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var options;
            LoginUserID = result.UserID;
            LoginUserName = result.UserName;
            $("#OperatorName").val(LoginUserName);
            $.each(result.LstLine, function (key, item) {
                options += '<option value="' + item.LineID + '">' + item.LineName + '</option>';
            });
            $('#Line').html(options);
        },
    });
}