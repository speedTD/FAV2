//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#MaintenanceDate').datepicker();
    $('#MaintenanceDate').datepicker("setDate", new Date());
    $('#TotalResult').prop('disabled', true);
    $('#OperatorName').val($('#UserLogin').data('username'));
    $('#Week').val(moment().week());
    $('#Month').val((new Date()).getMonth() + 1);
    $('#Year').val((new Date()).getFullYear());
});
// **** Event ****
//Lay toan bo thong tin cua Machineid 
$(document).ready(function () {
    //init form.
    $('#Frequency').val(1).trigger('change');
});
//--------- event ----------
$('#MachineID').keyup(function (event) {
    if ((event.keyCode ? event.keyCode : event.which) == 13) {
        //load content list
        Load_MachineMtnDetailBy_MachineID_and_Frequency();
    }
});
$('#Frequency').change(function () {
    displayWeekMonthYearDate_ByFrequency(document.getElementById("Frequency").value);
    //load content list
    Load_MachineMtnDetailBy_MachineID_and_Frequency();
});
function Load_MachineMtnDetailBy_MachineID_and_Frequency() {
    var MachineID = $('#MachineID').val();
    var FrequencyID = $('#Frequency').val();
    if (MachineID != "" && FrequencyID != "") {
        Load_MachineInfo();
        loadMachineMtnDetailNew(MachineID.trim(), FrequencyID);
    }
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
            if (result.Code == "00") {
                var html = '';
                $.each(result.lstMachineMtnContentList, function (key, item) {
                    html += '<tr class=\"package-row\">' + item.ID + '';
                    html += '<td>' + item.ID + '</td>';
                    html += '<td>' + item.MachinePartName  + '</td>';
                    html += '<td>' + item.ContentMtn + '</td>';
                    html += '<td>' + item.ToolMtn + '</td>';
                    html += '<td>' + item.MethodMtn + '</td>';
                    html += '<td>' + item.Standard + '</td>';
                    html += '<td id=\"td_Min\">' + item.Min + '</td>';
                    html += '<td id=\"td_Max\">' + item.Max + '</td>';
                                       
                    html += '<td><input id="td_ActualValue" type="text" class="myRowActualValue" onkeyup="return calculate_ActualResult(this,' + '\'' + item.ID + '\'' + ')" class="form-control" /></td>';
                    //html += '<td><input type="checkbox" name="OK" id="OK' + item.ID + '"  onchange="check_CheckedChangedOK(\'' + item.ID + '\')" >OK</input>|<input type="checkbox" name="NG" id="NG' + item.ID + '"  onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</input></td>';
                    

                    html += '<td>';
                    html += '<div class="checkbox">';
                    html += '<label class="checkbox-inline"><input type="checkbox" class="myRowCheckbok" name="OK" id="OK' + item.ID + '"  onchange="check_CheckedChangedOK(\'' + item.ID + '\')">OK</label>';
                    html += '<label class="checkbox-inline"><input type="checkbox" class="myRowCheckbok" name="NG" id="NG' + item.ID + '"  onchange ="check_CheckedChangedNG(\'' + item.ID + '\')" >NG</label>';
                    html += '</div></td>';



                    html += '<td><input class="myResultContents" type="text" class="form-control"></input></td>'
                    html += '</tr>';
                });
                $('.tbody').html(html);
            }
            if (result.Code == "01")
                $('.tbody').html('');
            else if (result.Code == "99") {
                alert('Lỗi tìm kiếm nội dung bảo trì/Error while loading maintenance content list: ' + result.Message);
                $('.tbody').html('');
            }
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}
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
    $("#Line").val("");
    $("#Machine").val("");
    $('#OperatorName').val($('#UserLogin').data('username'));
    //$('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#Line').css('border-color', 'lightgrey');
    $('#MachineID').css('border-color', 'lightgrey');
}

// **** Region CRUD ****
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
    if ($('#MachineName').val().trim() == "") {
        $('#MachineName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineName').css('border-color', 'lightgrey');
    }

    var countRow = $("#tblMachineMtnContentList .tbody tr").length;
    //var t = document.getElementById('tblMachineMtnContentList').getElementsByTagName('tbody');
    console.log(countRow);
    if (countRow == 0) {
        alert('Không có nội dung bảo trì./Doesn\'t exist Content maintenance!');
        isValid = false;
    }
    return isValid;
}
//insert machineMtnDaily List 1
function BeginInsert_MachineMtMonthYear() {
    var res = isValidateToInsert();
    if (res == false) {
        alert('Không thể thêm mới. Hãy kiểm tra thông tin nhập vào./Cannot insert. Check input information.');
        return false;
    }
    CheckMachineMtnData();
}

//insert machineMtnDaily List
/* Check Result Machine Content OK or NG*/
//function Calulate_TotalResultMachineContentList() {
//    var toTalResult = "OK";
//    var t = document.getElementById('tblMachineMtnContentList');
//    for (var j = 1; j < t.rows.length; j++) {
//        var r = t.rows[j];
//        var inputs = r.getElementsByTagName("input");
//        if (inputs[1].checked == true) {
//            toTalResult = "NG";
//        }
//    }
//    return toTalResult;
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

//insert 
function InsertMachineMtn() {
    cal_TotalResultMachineMtn();
    var MachineMtnObj;
    if ($('#Frequency').val() == "1") { //daily.
        MachineMtnObj = {
            OperatorID: LoginUserID,
            MachineID: $('#MachineID').val(),
            MaintenanceDate: $('#MaintenanceDate').val(),
            Shift: $('#Shift').val(),
            //Week: $('#Week').val(),
            //Month: $('#Month').val(),
            //Year: $('#Year').val(),
            FrequencyID: $('#Frequency').val(),
            Result: checkOK
        }
    }
    else if ($('#Frequency').val() == "2") //week
    {
        MachineMtnObj = {
            OperatorID: LoginUserID,
            MachineID: $('#MachineID').val(),
            MaintenanceDate: $('#MaintenanceDate').val(), // lay ngay hien tai.
            //Shift: 0,
            Week: $('#Week').val(),
            //Month: 0,
            Year: $('#Year').val(),
            FrequencyID: $('#Frequency').val(),
            Result: checkOK
        }
    }
    else if ($('#Frequency').val() == "3" || $('#Frequency').val() == "4" || $('#Frequency').val() == "5") //month,3months, 6months, year.
    {
        MachineMtnObj = {
            OperatorID: LoginUserID,
            MachineID: $('#MachineID').val(),
            MaintenanceDate: $('#MaintenanceDate').val(),  // lay ngay hien tai.
            //Shift: 0,
            //Week: 0,
            Month: $('#Week').val(),
            Year: $('#Year').val(),
            FrequencyID: $('#Frequency').val(),
            Result: checkOK
        }
    }
    else if ($('#Frequency').val() == "6") {
        MachineMtnObj = {
            OperatorID: LoginUserID,
            MachineID: $('#MachineID').val(),
            MaintenanceDate: $('#MaintenanceDate').val(),  // lay ngay hien tai.
            //Shift: 0,
            //Week: 0,
            //Month: $('#Week').val(),
            Year: $('#Year').val(),
            FrequencyID: $('#Frequency').val(),
            Result: checkOK
        }
    }
    $.ajax({
        url: "/MachineMtnMonthYear/InsertMachineMtn/",
        data: JSON.stringify(MachineMtnObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                MachineMtnIDLast = result.LastID;
                InsertMachineMtnDetail();
                alert('Thêm mới thành công!/Insert successful!');
            } else if (result.Code == "99") {
                alert('Lỗi thêm mới!/Error while inserting: ' + result.Message);
            }
        },
        error: function (errormessage) {
            alert('Lỗi/Error: InsertMachineMtn ' + errormessage.responseText);
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

        //var inputs = r.getElementsByTagName("input");
        var checkBoxs = r.getElementsByClassName("myRowCheckbok");
        
        var machineMtnContentID = t.rows[j].cells[0].firstChild.nodeValue;
        var contentMtn          = t.rows[j].cells[2].firstChild != null ? t.rows[j].cells[2].firstChild.nodeValue : '';
        var toolMtn             = t.rows[j].cells[3].firstChild != null ? t.rows[j].cells[3].firstChild.nodeValue : '';
        var methodMtn           = t.rows[j].cells[4].firstChild != null ? t.rows[j].cells[4].firstChild.nodeValue : '';
        var standard            = t.rows[j].cells[5].firstChild != null ? t.rows[j].cells[5].firstChild.nodeValue : '';
        var min                 = t.rows[j].cells[6].firstChild != null ? t.rows[j].cells[6].firstChild.nodeValue : '';
        var max                 = t.rows[j].cells[7].firstChild != null ? t.rows[j].cells[7].firstChild.nodeValue : '';
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
            MachineMtnID: MachineMtnIDLast,
            MachineMtnContentID: machineMtnContentID,
            ContentMtn: contentMtn,
            ToolMtn: toolMtn,
            MethodMtn: methodMtn,
            Standard: standard,
            Min: min,
            Max: max,
            ActualValue:actualValue,
            Result: checkResult,
            ResultContents: resultContents
        };

        $.ajax({
            url: "/MachineMtnMonthYear/InsertMachineMtnDetail/",
            data: JSON.stringify(MachineMtnDetail),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.Code == "00")
                { }
                if (result.Code == "99")
                {
                    
                }
                //loadData();
                //$('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert('Lỗi/Error ' + errormessage.responseText);
            }
        });
    }
}
//insert machineMtnDaily List 2
function CheckMachineMtnData() {
    $.ajax({
        url: "/MachineMtnMonthYear/CheckMachineMtnData/",
        data: { 'MachineID': $('#MachineID').val(), 'FrequencyID': $('#Frequency').val(), 'MaintenanceDate': $('#MaintenanceDate').val(), 'Shift': $('#Shift').val(), 'Week': $('#Week').val(), 'Month': $('#Month').val(), 'Year': $('#Year').val(), 'OperatorID': $('#Operator').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                alert("Đã tồn tại bảng kiểm tra này. Không thể thêm mới./Machine maintenance checksheet already exists! Cannot insert.");
            }
            if (result.Code == "01") {  // neu chua co.
                InsertMachineMtn();
            }
            else if (result.Code == "99")
            {
                alert('Lỗi kiểm tra checksheet/Error while checking checksheet' + errormessage.responseText);
            }
        },
        error: function (errormessage) {
            alert('Lỗi/Error CheckMachineMtnData()' + errormessage.responseText);
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
        data: { 'machineID': machineID.trim() },
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
// Region Add New.
// Region Checker result.
function CheckerUser_confirmResult(typeOfCheck) {
    if (typeOfCheck == "Add") {
        if (MachineMtnIDLast == 0) {
            alert('Hãy lưu kết quả bảo trì trước!/ Please, save checksheet before Checker confirm!');
            return;
        }
        else
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
                var MachineMtnObjCheck = {
                    ID: MachineMtnIDLast,
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
            }
        },
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


