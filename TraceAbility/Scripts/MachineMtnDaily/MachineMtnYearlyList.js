//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#MaintenanceDate').datepicker();
    LoadDataLine();
    loadData();    
});

function SearchMachineMtnList() {
    loadData();
}
function loadData() {
    $.ajax({
        url: "/MachineMtnDaily/SelectByCondition/",
        data: { 'MachineID': $('#Machine').val(), 'FrequencyID': $('#Frequency').val(), 'MaintenanceDate': $('#MaintenanceDate').val(), 'Shift': $('#Shift').val(), 'OperatorID': $('#Operator').val(), 'Month': 0, 'Year': $('#Year').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.lstMachineMtn, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MachineID + '</td>';
                html += '<td>' + item.OperatorID + '</td>';
                html += '<td>' + item.FrequencyID + '</td>';
                html += '<td style="display:none">' + item.Month + '</td>';
                html += '<td>' + item.Year + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.MaintenanceDate) + '</td>';
                html += '<td>' + item.Result + '</td>';
                html += '<td><a href=\'/MachineMtnDaily/EditYear?id=' + item.ID + '\'>Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
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
    $('#Operator').val("");
    $("#OperatorName").val("");
    $("#Line").val("");
    $("#Machine").val("");
    $("#Year").val("");
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
    if ($('#Year').val().trim() == "") {
        $('#Year').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Year').css('border-color', 'lightgrey');
    }
    return isValid;
}

function DeleleByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/MachineMtnDaily/Delete/" + ID,
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