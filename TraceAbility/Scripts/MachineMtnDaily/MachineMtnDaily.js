//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#MaintenanceDate').datepicker();
    $('#Frequency').prop('disabled', true);
    $('#TotalResult').prop('disabled', true);
    $('#OperatorName').val($('#UserLogin').data('username'));
    LoadDataLine();
});
//Lay toan bo thong tin cua Machineid
$(document).ready(function () {
    $('#MachineID').keyup(function (event) {
        if ((event.keyCode ? event.keyCode : event.which) == 13)
        {
            var MachineID = $('#MachineID').val();
            var FrequencyID = $('#Frequency').val();
            Load_MachineInfo();
            loadMachineMtnDetailNew(MachineID, FrequencyID);
        }
    });
});

var LoginUserID = $('#UserLogin').data('id');
var LoginUserName = $('#UserLogin').data('username');
var _MachineID = $('#MachineID').val();
var MachineMtnIDLast = 0;
var ID = 0;
var checkOK = "OK";
function SearchMtnContentList() {
   loadMachineMtnDetailNew($('#MachineID').val(), $('#Frequency').val());
}

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
                html += '<td>' + item.Standard + '</td>';
                html += '<td>' + item.FrequencyID + '</td>';
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
    $('#OperatorName').val($('#UserLogin').data('username'));
    //$('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Machine Maintenance Daily");

    $('#Operator').css('border-color', 'lightgrey');
    $('#OperatorName').css('border-color', 'lightgrey');
    $('#Line').css('border-color', 'lightgrey');
    $('#MachineID').css('border-color', 'lightgrey');
}

// **** Region CRUD ***************************************************** 
//function Validate() {
//    var isValid = true;
//    if ($('#MaintenanceDate').val().trim() == "") {
//        $('#MaintenanceDate').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#MaintenanceDate').css('border-color', 'lightgrey');
//    }
//    return isValid;
//}
function isValidateToInsert() {
    var isValid = true;
    if ($('#MaintenanceDate').val().trim() == "") {
        $('#MaintenanceDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MaintenanceDate').css('border-color', 'lightgrey');
    }
    if ($('#MachineName').val().trim() == "") {
        $('#MachineName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineName').css('border-color', 'lightgrey');
    }
    var t = document.getElementById('tblMachineMtnContentList');
    if (t.rows.length == 0)
    {
        alert('Doesn\'t exist Content maintenance!');
        isValid = false;
    }
    return isValid;
}
//insert machineMtnDaily List 1
function InsertMachineMtnDaily() {
    var res = isValidateToInsert();
    if (res == false) {
        alert('Cannot insert. Check input information.');
        return false;
    }
    CheckMachineMtnData();
}


//insert machineMtnDaily List
/* Check Result Machine Content OK or NG*/
function Calulate_TotalResultMachineContentList() {
    var toTalResult = "OK";
    var t = document.getElementById('tblMachineMtnContentList');
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        if (inputs[1].checked == true) {
            toTalResult = "NG";
        }
    }
    return toTalResult;
}

//insert machineMtnDaily List 3
function InsertMachineMtn() {
    checkOK = Calulate_TotalResultMachineContentList();
    $('#TotalResult').val(checkOK);
    ///* Check Result Machine OK or NG*/
    //var t = document.getElementById('tblMachineMtnContentList');
    //for (var j = 1; j < t.rows.length; j++) {
    //    var r = t.rows[j];
    //    var inputs = r.getElementsByTagName("input");
    //    if (inputs[1].checked == true) {
    //        checkOK = "NG";
    //    }
    //}
    var MachineMtnObj = {
        OperatorID: LoginUserID,
        MachineID: $('#MachineID').val(),
        MaintenanceDate: $('#MaintenanceDate').val(),
        Shift: $('#Shift').val(),
        FrequencyID: $('#Frequency').val(),
        Result: checkOK,
        Month: 0,
        Year: 0
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
            alert('Insert successful!');
            //loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//insert machineMtnDaily List
function InsertMachineMtnDetail() {
    //var res = validate();
    var t = document.getElementById('tblMachineMtnContentList');
    
    //loops through rows    
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var machineMtnContentID = t.rows[j].cells[0].firstChild.nodeValue;
        var contentMtn = t.rows[j].cells[2].firstChild.nodeValue;
        var toolMtn = t.rows[j].cells[3].firstChild.nodeValue;
        var methodMtn = t.rows[j].cells[4].firstChild.nodeValue;
        var standard = t.rows[j].cells[5].firstChild.nodeValue;

        var checkResult = "";
        if (inputs[0].checked == true) {
            checkResult = "OK";
        }
        if (inputs[1].checked == true) {
            checkResult = "NG";
            checkOK = "NG";
        }
        var MachineMtnDetail = {
            MachineMtnID: MachineMtnIDLast,
            MachineMtnContentID: machineMtnContentID,
            MontentMtn : contentMtn,
            ToolMtn: toolMtn,
            MethodMtn: methodMtn,
            Standard : standard,
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
}
//insert machineMtnDaily List 2
function CheckMachineMtnData() {
    $.ajax({
        url: "/MachineMtnDaily/CheckMachineMtnData/",
        data: { 'MachineID': $('#MachineID').val(), 'FrequencyID': $('#Frequency').val(), 'MaintenanceDate': $('#MaintenanceDate').val(), 'Shift': $('#Shift').val(), 'OperatorID': $('#Operator').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "01") {
                InsertMachineMtn();
            }
            if (result.Code == "00") {
                alert("Machine maintenance already exist! Cannot insert." );
            }
             
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
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

function Load_MachineInfo() {
        var machineID = $('#MachineID').val();
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
                else if (result.Code == "99") { }
            },
            error: function (errormessage) {
                alert('Error Load_MachineInfo(e) ' + errormessage.responseText);
            }
        });
}
// Region Add New.
// Region Checker result.
function CheckerUser_confirmResult(typeOfCheck)
{
    if (typeOfCheck == "Add") {
        if (MachineMtnIDLast == 0)
        {
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
                    ID: MachineMtnIDLast,
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

