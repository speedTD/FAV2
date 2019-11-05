//Get id from edit button.
var MachineMtnID = 0;

var url_String = document.URL;
var url_HTML = new URL(url_String);
//var MachineMtnDailyID = url_HTML.searchParams.get("id");
MachineMtnID = url_HTML.searchParams.get("id");
var checkOK = "OK";
//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#MaintenanceDate').datepicker();
    $('#MachineMtnID').val(MachineMtnID);  // Luu gia tri MachineMtnID
    GetMachineMtnbyID(MachineMtnID);
    loadDataMachineMtnDailyDetail(MachineMtnID);
});

function GetMachineMtnbyID(MachineMtnID) {
    $.ajax({
        url: "/MachineMtnDaily/GetMachineMtnbyID/",
        data: { 'MachineMtnID': MachineMtnID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //loginUserID = result.UserID;
            //$('#MachineMtnID').val(MachineMtnID);
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
            $('#TotalResult').val(result.lstMachineMtn[0].Result);
            $('#TotalResult').prop('disabled', true);
        },
        error: function (errormessage) {
            alert('Error loadDataMachineMtn' + errormessage.responseText);
        }
    });
}
function loadDataMachineMtnDailyDetail(MachineMtnID) {
    
    $.ajax({
        url: "/MachineMtnDaily/GetMachineMtnDetail/",
        data: { 'MachineMtnID': MachineMtnID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            
            $.each(result, function (key, item) {
                html += '<tr> class=\">' + item.ID + '';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.MachinePartName + '</td>';
                html += '<td>' + item.ContentMtn + '</td>';
                html += '<td>' + item.ToolMtn + '</td>';
                html += '<td>' + item.MethodMtn + '</td>';
                html += '<td>' + item.Standard + '</td>';
                //html += '<td>' + item.FrequencyID + '</td>';
                if (item.Result == "OK") {
                    html += '<td><input type="checkbox" name="OK" id="OK' + item.ID + '" checked="true" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="NG' + item.ID + '" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                }
                if (item.Result == "NG") {
                    html += '<td><input    type="checkbox" name="OK" id="OK' + item.ID + '" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="NG' + item.ID + '" checked="true" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                }
                if (item.Result == "") {
                    html += '<td><input type="checkbox" name="OK" id="OK'  + item.ID + '" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="NG' + item.ID + '" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                }
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            
            alert('Error loadDataMachineMtnDailyDetail' + errormessage.responseText);
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
                html += '<td>' + item.MachineMtnContentID + '</td>';
                html += '<td>' + item.MachinePart + '</td>';
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
    $('#Operator').val("");
    $("#OperatorName").val("");
    $("#Line").val("");
    $("#Machine").val("");


    //$('#btnUpdate').hide();
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
    //DeleleByID(MachineMtnDailyID);
    //InsertMachineMtn();
    UpdateMachineMtn();
}
// **** Region CRUD ***************************************************** 
function UpdateMachineMtn() {
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
        ID:  $('#MachineMtnID').val(),
        OperatorID: $('#UserLogin').data('id'),
        MachineID: $('#MachineID').val(),
        MaintenanceDate: $('#MaintenanceDate').val(),
        Shift: $('#Shift').val(),
        FrequencyID: $('#Frequency').val(),
        Result: checkOK,
        Month: 0,
        Year: 0
    };
    $.ajax({
        url: "/MachineMtnDaily/UpdateMachineMtn/",
        data: JSON.stringify(MachineMtnObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            UpdateMachineMtnDetail();
            //loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//insert machineMtnDaily List
function UpdateMachineMtnDetail() {
    var t = document.getElementById('tblMachineMtnContentList');

    //loops through rows    
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var IDMachineMtnDetail = t.rows[j].cells[0].firstChild.nodeValue;
        var machineMtnContentID = t.rows[j].cells[1].firstChild.nodeValue;
        if (inputs[0].checked == true) {
            checkResult = "OK";
        }
        if (inputs[1].checked == true) {
            checkResult = "NG";
        }
        var MachineMtnDetail = {
            ID: IDMachineMtnDetail,
            MachineMtnID: $('#MachineMtnID').val(),
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
                $('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    alert("Update machineMtnDetail");
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

// Region Checker result.
function CheckerUser_confirmResult(typeOfCheck) {
    if (typeOfCheck == "Add") {
        if (MachineMtnID == 0) {
            alert('Chưa lưu kết quả bảo trì!');
            return;
        }
        CheckerInputResult_In_AddNewForm();
    }
    if (typeOfCheck == "List") {

    }
}

function CheckerInputResult_In_AddNewForm() {
    var CheckerUserName = $('#CheckerUserName').val();
    var CheckerPassword = $('#CheckerPassword').val();
    var CheckerPermission = 'Check';
    var CheckerResult = $("input[name='CheckerResult_RadioOptions']:checked").val();
    console.log(CheckerUserName);
    console.log(CheckerPassword);
    console.log(CheckerResult);

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
                    url: "/MachineMtnDaily/UpdateCheckerResult_ByMachineMtnID/",
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
            }
        },
    });
}

