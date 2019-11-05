//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    $('#FPBCheckingDate').datepicker();
});
var idFPB = 0;
function loadData() {
    $.ajax({
        url: "/FPBCheckingItem/GetFPBCheckingItemName",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.FPBCheckingItem + '</td>';
                html += '<td><input type="checkbox" name="OK" id="firstOK' + item.IDFPBCheckingItem + '" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.IDFPBCheckingItem + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="firstNG' + item.IDFPBCheckingItem + '" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.IDFPBCheckingItem + '\')" >NG</input></td>';
                html += '<td><input type="checkbox" name="OK" id="middleOK' + item.IDFPBCheckingItem + '" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.IDFPBCheckingItem + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="middleNG' + item.IDFPBCheckingItem + '" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.IDFPBCheckingItem + '\')" >NG</input></td>';
                html += '<td><input type="checkbox" name="OK" id="endOK' + item.IDFPBCheckingItem + '" AutoPostBack = "true" onchange="check_CheckedChangedOK(\'' + item.IDFPBCheckingItem + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="endNG' + item.IDFPBCheckingItem + '" AutoPostBack="true" onchange ="check_CheckedChangedNG(\'' + item.IDFPBCheckingItem + '\')" >NG</input></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    //$('#ID').val("");
    $('#FPBCheckingDate').val("");
    $('#IndicationNo').css('border-color', 'lightgrey');
    $('#OperatorID').css('border-color', 'lightgrey');
    $('#SeqNo').css('border-color', 'lightgrey');
    $('#MachineID').css('border-color', 'lightgrey');
    $('#ItemName').css('border-color', 'lightgrey');
    $('#ItemCode').css('border-color', 'lightgrey');
    $('#BatchNo').css('border-color', 'lightgrey');
    $('#MachineName').css('border-color', 'lightgrey');
    $('#OperatorID').css('border-color', 'lightgrey');
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New FPB Checking");
}

//function GetbyID(ID) {
//    idFPB = ID;
//    //$('#ID').css('border-color', 'lightgrey');
//    $('#IDFPBCheckingItem').css('border-color', 'lightgrey');
//    $('#FPBDate').css('border-color', 'lightgrey');
//    $('#IndicationNo').css('border-color', 'lightgrey');
//    $('#OperatorID').css('border-color', 'lightgrey');
//    $('#SeqNo').css('border-color', 'lightgrey');
//    $('#BlockID').css('border-color', 'lightgrey');
//    $('#Result').css('border-color', 'lightgrey');
//    $('#ResultContent').css('border-color', 'lightgrey');

//    $.ajax({
//        url: "/FPB/GetbyID/",
//        data: { 'ID': ID },
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            //$('#ID').val(result.ID);
//            //$('#ID').prop('disabled', true);
//            $('#IDFPBCheckingItem').val(result.IDFPBCheckingItem);
//            $('#FPBDate').val(SM_ConvertMsJsonDate_ToString(result.FPBDate));
//            $('#IndicationNo').val(result.IndicationNo);
//            $('#OperatorID').val(result.OperatorID);
//            $('#SeqNo').val(result.SeqNo);
//            $('#BlockID').val(result.BlockID);
//            $('#Result').val(result.Result);
//            $('#ResultContent').val(result.ResultContent);

//            $('#myModal').modal('show');
//            $('#btnUpdate').show();
//            $('#btnAdd').hide();
//            $('#myModalLabel').text('Update Tool ');
//        },
//        error: function (errormessage) {
//            alert('Error    ' + errormessage.responseText);
//        }
//    });
//    return false;
//}

function validate() {
    var isValid = true;
    if ($('#FPBCheckingDate').val().trim() == "") {
        $('#FPBCheckingDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FPBCheckingDate').css('border-color', 'lightgrey');
    }
    if ($('#IndicationNo').val().trim() == "") {
        $('#IndicationNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#IndicationNo').css('border-color', 'lightgrey');
    }
    if ($('#OperatorID').val().trim() == "") {
        $('#OperatorID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OperatorID').css('border-color', 'lightgrey');
    }
    if ($('#SeqNo').val().trim() == "") {
        $('#SeqNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SeqNo').css('border-color', 'lightgrey');
    }
    if ($('#MachineID').val().trim() == "") {
        $('#MachineID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineID').css('border-color', 'lightgrey');
    }
    if ($('#MachineName').val().trim() == "") {
        $('#MachineName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineName').css('border-color', 'lightgrey');
    }
    if ($('#ItemName').val().trim() == "") {
        $('#ItemName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ItemName').css('border-color', 'lightgrey');
    }
    if ($('#ItemCode').val().trim() == "") {
        $('#ItemCode').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ItemCode').css('border-color', 'lightgrey');
    }
    if ($('#BatchNo').val().trim() == "") {
        $('#BatchNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#BatchNo').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertFPBChecking() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var FPBObj = {
        ID: 0,
        FPBCheckingDate: $('#FPBCheckingDate').val(),
        IndicationNo: $('#IndicationNo').val(),
        OperatorID: $('#OperatorID').val(),
        SeqNo: $('#SeqNo').val(),
        MachineID: $('#MachineID').val(),
        MachineName: $('#MachineName').val(),
        ItemName: $('#ItemName').val(),
        ItemCode: $('#ItemCode').val(),
        BatchNo: $('#BatchNo').val(),
    };
    $.ajax({
        url: "/FPB/Insert/",
        data: JSON.stringify(FPBObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function UpdateFPBChecking() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var FPBObj = {
        ID: idFPB,
        IDFPBCheckingItem: $('#IDFPBCheckingItem').val(),
        FPBDate: $('#FPBDate').val(),
        IndicationNo: $('#IndicationNo').val(),
        OperatorID: $('#OperatorID').val(),
        SeqNo: $('#SeqNo').val(),
        BlockID: $('#BlockID').val(),
        Result: $('#Result').val(),
        ResultContent: $('#ResultContent').val()
    };
    $.ajax({
        url: "/FPB/Insert/",
        data: JSON.stringify(FPBObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}



