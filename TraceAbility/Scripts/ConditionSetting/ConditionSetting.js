//Load Data in Table when documents is ready  
$(document).ready(function () {  
    loadData();
    load_Line("");
    load_MachineType("");
    $('#ReceiveDate').datepicker();
});

function loadData() {
    $.ajax({
        url: "/ConditionSetting/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",              
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                //html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.LineID + '</td>';
                html += '<td>' + item.MachineTypeName + '</td>';
                html += '<td>' + item.PatternCode + '</td>';
                html += '<td>' + item.ControlItem + '</td>';
                html += '<td>' + item.SpecDisplay + '</td>';
                html += '<td>' + item.Unit + '</td>';
                html += '<td>' + item.LowerLimit + '</td>';
                html += '<td>' + item.UpperLimit + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
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
    load_Line("");
    load_MachineType("");
    $('#ID').val("");
    $('#LineID').val("");
    $('#MachineTypeID').val("");
    $('#PatternCode').val("");
    $('#ControlItem').val("");
    $('#SpecDisplay').val("");
    $('#Unit').val("");
    $('#LowerLimit').val("");
    $('#UpperLimit').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Condition Setting ");

    $('#ID').css('border-color', 'lightgrey');
    $('#LineID').css('border-color', 'lightgrey');
    $('#MachineID').css('border-color', 'lightgrey');
    $('#PatternCode').css('border-color', 'lightgrey');
    $('#ControlItem').css('border-color', 'lightgrey');
    $('#SpecDisplay').css('border-color', 'lightgrey');
    $('#Unit').css('border-color', 'lightgrey');
    $('#LowerLimit').css('border-color', 'lightgrey');
    $('#UpperLimit').css('border-color', 'lightgrey');
}

function GetbyID(ID) {
    $('#ID').css('border-color', 'lightgrey');
    $('#LineID').css('border-color', 'lightgrey');
    $('#MachineID').css('border-color', 'lightgrey');
    $('#PatternCode').css('border-color', 'lightgrey');
    $('#ControlItem').css('border-color', 'lightgrey');
    $('#SpecDisplay').css('border-color', 'lightgrey');
    $('#Unit').css('border-color', 'lightgrey');
    $('#LowerLimit').css('border-color', 'lightgrey');
    $('#UpperLimit').css('border-color', 'lightgrey');

    $.ajax({
        url: "/ConditionSetting/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ID').val(result.ID);
            $('#ID').prop('disabled', true);
            $('#LineID').val(result.LineID);
            $('#MachineTypeID').val(result.MachineTypeName);
            $('#PatternCode').val(result.PatternCode);
            $('#ControlItem').val(result.ControlItem);
            $('#SpecDisplay').val(result.SpecDisplay);
            $('#Unit').val(result.Unit);
            $('#LowerLimit').val(result.LowerLimit);
            $('#UpperLimit').val(result.UpperLimit);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Condition Setting ');
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
    if ($('#LineID').val().trim() == "") {
        $('#LineID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LineID').css('border-color', 'lightgrey');
    }
    if ($('#MachineTypeID').val().trim() == "") {
        $('#MachineTypeID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineTypeID').css('border-color', 'lightgrey');
    }
    if ($('#PatternCode').val().trim() == "") {
        $('#PatternCode').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PatternCode').css('border-color', 'lightgrey');
    }

    if ($('#ControlItem').val().trim() == "") {
        $('#ControlItem').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ControlItem').css('border-color', 'lightgrey');
    }
    if ($('#SpecDisplay').val().trim() == "") {
        $('#SpecDisplay').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SpecDisplay').css('border-color', 'lightgrey');
    }
    if ($('#Unit').val().trim() == "") {
        $('#Unit').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Unit').css('border-color', 'lightgrey');
    }
    if ($('#LowerLimit').val().trim() == "") {
        $('#LowerLimit').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LowerLimit').css('border-color', 'lightgrey');
    }
    if ($('#UpperLimit').val().trim() == "") {
        $('#UpperLimit').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UpperLimit').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertConditionSetting() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ConditionSettingObj = {
        //ID: $('#ID').val(),
        LineID: $('#LineID').val(),
        MachineTypeID: $('#MachineTypeID').val(),
        PatternCode: $('#PatternCode').val(),
        ControlItem: $('#ControlItem').val(),
        SpecDisplay: $('#SpecDisplay').val(),
        Unit: $('#Unit').val(),
        LowerLimit: $('#LowerLimit').val(),
        UpperLimit: $('#UpperLimit').val()
    };
    $.ajax({
        url: "/ConditionSetting/Insert/",
        data: JSON.stringify(ConditionSettingObj),
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

function UpdateConditionSetting() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ConditionSettingObj = {
        ID: $('#ID').val(),
        LineID: $('#LineID').val(),
        MachineTypeID: $('#MachineTypeID').val(),
        PatternCode: $('#PatternCode').val(),
        ControlItem: $('#ControlItem').val(),
        SpecDisplay: $('#SpecDisplay').val(),
        Unit: $('#Unit').val(),
        LowerLimit: $('#LowerLimit').val(),
        UpperLimit: $('#UpperLimit').val()
    };
    $.ajax({
        url: "/ConditionSetting/Insert/",
        data: JSON.stringify(ConditionSettingObj),
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
            url: "/ConditionSetting/Delete/" + ID,
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
function load_Line(selectObject) {
    $.ajax({
        url: "/Line/ListAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<option value="' + item.LineID + '">' + item.LineID + '</option>';
            });
            $('#LineID').html(html);
        },
        error: function (errormessage) {
            alert('Error function load_Line(selectObject) :' + errormessage.responseText);
        }
    });
}
function load_MachineType(selectObject) {
    $.ajax({
        url: "/MachineType/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<option value="' + item.ID + '">' + item.TypeName + '</option>';
            });
            $('#MachineTypeID').html(html);
        },
        error: function (errormessage) {
            alert('Error function load_MachineType(selectObject) :' + errormessage.responseText);
        }
    });
}

