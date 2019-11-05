//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    $('#FPBCheckingDate').datepicker();
});
var idFPB = 0;
var loginUserID = $('#UserLogin').data('id');
var loginUserName = $('#UserLogin').data('username');

function loadData() {
    $.ajax({
        url: "/FPBChecking/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            loginUserID = result.UserID;
            loginUserName = result.UserName;
            $.each(result.LstFPBChecking, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.IndicationNo + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.FPBCheckingDate) + '</td>';
                html += '<td>' + item.ItemName + '</td>';
                html += '<td>' + item.ItemCode + '</td>';
                html += '<td>' + item.BatchNo + '</td>';
                html += '<td>' + item.MachineID + '</td>';
                html += '<td>' + item.MachineName + '</td>';
                html += '<td>' + item.UserID + '</td>';
                html += '<td>' + item.SeqNo + '</td>';
                html += '<td>' + item.BlockID + '</td>';
                html += '<td>' + item.Result + '</td>';
                html += '<td>' + item.ResultContent + '</td>';
                html += '<td><a href=\'\' target="_blank"  onclick="this.href =\'/FPBChecking/Edit?id=' + item.ID + '\'">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
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
    $('#IDFPBCheckingItem').val("");
    $('#FPBDate').val("");
    $('#IndicationNo').val("");
    $('#UserID').val(loginUserName);
    $('#SeqNo').val("");
    $('#BlockID').val("");
    $('#Result').val("");
    $('#ResultContent').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New FPB ");

    $('#IDFPBCheckingItem').css('border-color', 'lightgrey');
    $('#FPBDate').css('border-color', 'lightgrey');
    $('#IndicationNo').css('border-color', 'lightgrey');
    $('#UserID').css('border-color', 'lightgrey');
    $('#SeqNo').css('border-color', 'lightgrey');
    $('#BlockID').css('border-color', 'lightgrey');
    $('#Result').css('border-color', 'lightgrey');
    $('#ResultContent').css('border-color', 'lightgrey');
}

function GetbyID(ID) {
    idFPB = ID;
    $('#IDFPBCheckingItem').css('border-color', 'lightgrey');
    $('#FPBDate').css('border-color', 'lightgrey');
    $('#IndicationNo').css('border-color', 'lightgrey');
    $('#UserID').css('border-color', 'lightgrey');
    $('#SeqNo').css('border-color', 'lightgrey');
    $('#BlockID').css('border-color', 'lightgrey');
    $('#Result').css('border-color', 'lightgrey');
    $('#ResultContent').css('border-color', 'lightgrey');

    $.ajax({
        url: "/FPB/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //$('#ID').val(result.ID);
            //$('#ID').prop('disabled', true);
            $('#IDFPBCheckingItem').val(result.IDFPBCheckingItem);
            $('#FPBDate').val(SM_ConvertMsJsonDate_ToString(result.FPBDate));
            $('#IndicationNo').val(result.IndicationNo);
            $('#UserID').val(result.UserID);
            $('#SeqNo').val(result.SeqNo);
            $('#BlockID').val(result.BlockID);
            $('#Result').val(result.Result);
            $('#ResultContent').val(result.ResultContent);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update FPB ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    //if ($('#ID').val().trim() == "") {
    //    $('#ID').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#ID').css('border-color', 'lightgrey');
    //}
    if ($('#IDFPBCheckingItem').val().trim() == "") {
        $('#IDFPBCheckingItem').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#IDFPBCheckingItem').css('border-color', 'lightgrey');
    }
    if ($('#FPBDate').val().trim() == "") {
        $('#FPBDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FPBDate').css('border-color', 'lightgrey');
    }
    if ($('#IndicationNo').val().trim() == "") {
        $('#IndicationNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#IndicationNo').css('border-color', 'lightgrey');
    }
    if ($('#UserID').val().trim() == "") {
        $('#UserID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UserID').css('border-color', 'lightgrey');
    }
    if ($('#SeqNo').val().trim() == "") {
        $('#SeqNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SeqNo').css('border-color', 'lightgrey');
    }
    if ($('#BlockID').val().trim() == "") {
        $('#BlockID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#BlockID').css('border-color', 'lightgrey');
    }
    if ($('#Result').val().trim() == "") {
        $('#Result').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Result').css('border-color', 'lightgrey');
    }
    if ($('#ResultContent').val().trim() == "") {
        $('#ResultContent').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ResultContent').css('border-color', 'lightgrey');
    }
    return isValid;
}
// **** Region CRUD ***************************************************** 
function InsertFPB() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var FPBObj = {
        ID: 0,
        IDFPBCheckingItem: $('#IDFPBCheckingItem').val(),
        FPBDate: $('#FPBDate').val(),
        IndicationNo: $('#IndicationNo').val(),
        UserID: loginUserID,
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
function UpdateFPB() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var FPBObj = {
        ID: idFPB,
        IDFPBCheckingItem: $('#IDFPBCheckingItem').val(),
        FPBDate: $('#FPBDate').val(),
        IndicationNo: $('#IndicationNo').val(),
        UserID: $('#UserID').val(),
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
function DeleleByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/FPBChecking/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
                alert("Deleted successful!");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}


