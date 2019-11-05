//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});
var idFPB = 0;
function loadData() {
    $.ajax({
        url: "/FPBCheckingItem/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MachineID + '</td>';
                html += '<td>' + item.FrequencyID + '</td>';
                html += '<td><a href="#" onclick="return GetbyMachineID(\'' + item.MachineID + '\',\'' + item.FrequencyID + '\')">Edit</a> | <a href="#" onclick="return DeleleByMachineID(\'' + item.MachineID + '\',\'' + item.FrequencyID + '\')">Delete</a></td>';
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
    $('#MachineID').val("");
    $('#FrequencyID').val(1);
    $('#TableDPBChekingItem').val("");
    $('#btnUpdate').hide();
    $('#btnInsert').show();
    $('#btnAddRow').show();
    $('#myModalLabel').text("Add New FPBCheckingItem ");
    $('#MachineID').prop('disabled', false);
    $('#FrequencyID').prop('disabled', false);
    $('#MachineID').css('border-color', 'lightgrey');
    $('#FrequencyID').css('border-color', 'lightgrey');
}

function GetbyMachineID(MachineID, FrequencyID) {
    $('#MachineID').css('border-color', 'lightgrey');
    $('#FrequencyID').css('border-color', 'lightgrey');

    $.ajax({
        url: "/FPBCheckingItem/GetbyMachineID/",
        data: { 'MachineID': MachineID, 'FrequencyID': FrequencyID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $('#MachineID').val(result[0].MachineID);
            $('#FrequencyID').val(result[0].FrequencyID);
            var i = 1;
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + i + '</td>';
                html += '<td style="display:none"><input type="text" disabled = "true" value= "' + item.IDFPBCheckingItem + '"></input></td>';
                html += '<td><input type="text" value= "' + item.CheckingItemName + '"></input></td>';
                html += '<td><input type="button" id="btnDelRow" value="Delete" onclick="deleteRow(this)"/></td>';
                html += '</tr>';
                i++;
            });
            $('.tbodyFPB').html(html);

            $('#MachineID').prop('disabled', true);
            $('#FrequencyID').prop('disabled', true);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnInsert').hide();
            $('#idFPBCheckingItem').prop('disabled', true);
            $('#btnAddRow').hide();
            $('#myModalLabel').text('Update FPB Cheking Item');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}
function deleteRow(row) {
    var t = document.getElementById('TableDPBChekingItem');
    var i = row.parentNode.parentNode.rowIndex;
    var r = t.rows[i];
    var inputs = r.getElementsByTagName("input");
    var checkingItemID = inputs[0].value;
    if (checkingItemID != "") {
        DeleleByCheckingItemID(checkingItemID);
    }    
    document.getElementById('TableDPBChekingItem').deleteRow(i);
}

function validate() {
    var isValid = true;
    if ($('#MachineID').val().trim() == "") {
        $('#MachineID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineID').css('border-color', 'lightgrey');
    }
    if ($('#FrequencyID').val().trim() == "") {
        $('#FrequencyID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FrequencyID').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertFPBCheckingItem() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var t = document.getElementById('TableDPBChekingItem');

    //loops through rows    
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var result = new Array(inputs.length);
        var checkingItemName = inputs[1].value;
        var FPBCheckingItemObj = {
            IDFPBCheckingItem: 0,
            MachineID: $('#MachineID').val(),
            CheckingItemName: checkingItemName,
            FrequencyID: $('#FrequencyID').val()
        };
        $.ajax({
            url: "/FPBCheckingItem/Insert/",
            data: JSON.stringify(FPBCheckingItemObj),
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
}

function UpdateFPBCheckingItem() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var t = document.getElementById('TableDPBChekingItem');

    //loops through rows    
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var result = new Array(inputs.length);
        var checkingItemName = inputs[1].value;
        var idFPBCheckingItem = inputs[0].value;;
        var FPBCheckingItemObj = {
            IDFPBCheckingItem: idFPBCheckingItem,
            MachineID: $('#MachineID').val(),
            CheckingItemName: checkingItemName,
            FrequencyID: $('#FrequencyID').val()
        };
        $.ajax({
            url: "/FPBCheckingItem/Insert/",
            data: JSON.stringify(FPBCheckingItemObj),
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
    loadData();
}

function DeleleByCheckingItemID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/FPBCheckingItem/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                alert("Deleted successful!");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function DeleleByMachineID(MachineID, FrequencyID) {
    var ans = confirm("Are you sure you want to delete ?");
    if (ans) {
        $.ajax({
            url: "/FPBCheckingItem/DeleteByMachineID/",
            data: { 'MachineID': MachineID, 'FrequencyID': FrequencyID },
            type: "GET",
            contentType: "application/json;charset=utf-8",
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



