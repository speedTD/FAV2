//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    load_Line("");
    load_MachineType("");
    $('#ReceiveDate').datepicker();
});

function loadData() {
    $.ajax({
        url: "/ProductionList/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.IndicatorID + '</td>';
                html += '<td>' + item.ItemName + '</td>';
                html += '<td>' + item.ItemCode + '</td>';
                html += '<td>' + item.BatchNo + '</td>';
                html += '<td>' + item.ProgramName + '</td>';
                html += '<td>' + item.LineID + '</td>';
                html += '<td>' + item.MachineTypeName + '</td>';
                html += '<td>' + item.PatternCode + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.IndicatorID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.IndicatorID + '\')">Delete</a></td>';
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
    $('#IndicatorID').val("");
    $('#ItemName').val("");
    $('#ItemCode').val("");
    $('#BatchNo').val("");
    $('#ProgramName').val("");
    $('#LineID').val(1);
    $('#MachineTypeID').val("");
    $('#PatternCode').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Production List ");
    $('#IndicatorID').prop('disabled', false);
    $('#IndicatorID').css('border-color', 'lightgrey');
    $('#ItemName').css('border-color', 'lightgrey');
    $('#ItemCode').css('border-color', 'lightgrey');
    $('#BatchNo').css('border-color', 'lightgrey');
    $('#ProgramName').css('border-color', 'lightgrey');
    $('#LineID').css('border-color', 'lightgrey');
    $('#MachineTypeID').css('border-color', 'lightgrey');
    $('#PatternCode').css('border-color', 'lightgrey');
}

function GetbyID(IndicatorID) {
    $('#IndicatorID').css('border-color', 'lightgrey');
    $('#ItemName').css('border-color', 'lightgrey');
    $('#ItemCode').css('border-color', 'lightgrey');
    $('#BatchNo').css('border-color', 'lightgrey');
    $('#ProgramName').css('border-color', 'lightgrey');
    $('#Line').css('border-color', 'lightgrey');
    $('#MachineTypeID').css('border-color', 'lightgrey');
    $('#PatternCode').css('border-color', 'lightgrey');

    $.ajax({
        url: "/ProductionList/GetbyID/",
        data: { 'ID': IndicatorID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#IndicatorID').val(result.IndicatorID);
            $('#IndicatorID').prop('disabled', true);
            $('#ItemName').val(result.ItemName);
            $('#ItemCode').val(result.ItemCode);
            $('#BatchNo').val(result.BatchNo);
            $('#ProgramName').val(result.ProgramName);
            $('#Line').val(result.LineID);
            $('#MachineTypeID').val(result.MachineTypeName);
            $('#PatternCode').val(result.PatternCode);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Production List ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#IndicatorID').val().trim() == "") {
        $('#IndicatorID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#IndicatorID').css('border-color', 'lightgrey');
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

    if ($('#ProgramName').val().trim() == "") {
        $('#ProgramName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProgramName').css('border-color', 'lightgrey');
    }
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
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertProductionList() {
    var res = validate();
    if (res == false) {
        return false;
    }
    $.ajax({
        url: "/ProductionList/CountbyID/",
        data: { 'ID': $('#IndicatorID').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result1) {
            if (result1 == 0) {
                var ProductionListObj = {
                    IndicatorID: $('#IndicatorID').val(),
                    ItemName: $('#ItemName').val(),
                    ItemCode: $('#ItemCode').val(),
                    BatchNo: $('#BatchNo').val(),
                    ProgramName: $('#ProgramName').val(),
                    LineID: $('#LineID').val(),
                    MachineTypeID: $('#MachineTypeID').val(),
                    PatternCode: $('#PatternCode').val()
                };
                $.ajax({
                    url: "/ProductionList/Insert/",
                    data: JSON.stringify(ProductionListObj),
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
            else {
                alert("Production already exists");
                $('#IndicatorID').css('border-color', 'Red');
            }
        },
    });
    
}

function UpdateProductionList() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ProductionListObj = {
        IndicatorID: $('#IndicatorID').val(),
        ItemName: $('#ItemName').val(),
        ItemCode: $('#ItemCode').val(),
        BatchNo: $('#BatchNo').val(),
        ProgramName: $('#ProgramName').val(),
        LineID: $('#LineID').val(),
        MachineTypeID: $('#MachineTypeID').val(),
        PatternCode: $('#PatternCode').val()
    };
    $.ajax({
        url: "/ProductionList/Insert/",
        data: JSON.stringify(ProductionListObj),
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

function DeleleByID(IndicatorID) {
    var ans = confirm("Are you sure you want to delete ?" + IndicatorID);
    if (ans) {
        $.ajax({
            url: "/ProductionList/Delete/" + IndicatorID,
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
            alert('Error function load_LineUsing(selectObject) :' + errormessage.responseText);
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


