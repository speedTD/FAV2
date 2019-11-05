//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});
var _MachineTypeID;
function loadData() {
    $.ajax({
        url: "/MachineTypeFrequency/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MachineTypeID + '</td>';
                html += '<td>' + item.Daily + '</td>';
                html += '<td>' + item.Weekly + '</td>';
                html += '<td>' + item.Monthly + '</td>';
                html += '<td>' + item.ThreeMonths + '</td>';
                html += '<td>' + item.SixMonths + '</td>';
                html += '<td>' + item.Yearly + '</td>';
                html += '<td><a href="#" onclick="return GetbyMachineTypeID(\'' + item.MachineTypeID + '\')">Edit</a> | <a href="#" onclick="return DeleleByMachineTypeID(\'' + item.MachineTypeID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert('Error loadData(): ' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#MachineTypeID').val("");
    $("#Daily").prop("checked", false);
    $("#3Months").prop("checked", false);
    $("#Weekly").prop("checked", false);
    $("#6Months").prop("checked", false);
    $("#Monthly").prop("checked", false);
    $("#Yearly").prop("checked", false);

    //$('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Machine Type Frequency");

    $('#MachineTypeID').css('border-color', 'lightgrey');
}

function GetbyMachineTypeID(MachineTypeID) {
    $('#MachineTypeID').css('border-color', 'lightgrey');
    $.ajax({
        url: "/MachineTypeFrequency/GetbyMachineTypeID/",
        data: { 'MachineTypeID': MachineTypeID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                $('#MachineTypeID').val(item.MachineTypeID);
                _MachineTypeID = item.MachineTypeID;
                if (item.FrequencyID == 1) {
                    document.getElementById("Daily").checked = true;
                }
                if (item.FrequencyID == 2) {
                    document.getElementById("Weekly").checked = true;
                }
                if (item.FrequencyID == 3) {
                    document.getElementById("Monthly").checked = true;
                }
                if (item.FrequencyID == 4) {
                    document.getElementById("3Months").checked = true;
                }
                if (item.FrequencyID == 5) {
                    document.getElementById("6Months").checked = true;
                }
                if (item.FrequencyID == 6) {
                    document.getElementById("Yearly").checked = true;
                }
            });
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Machine Type Frequency ');
        },
        error: function (errormessage) {
            alert('Error GetbyMachineTypeID():   ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#MachineTypeID').val().trim() == "") {
        $('#MachineTypeID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineTypeID').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertMachineTypeFrequencyList() {
    var res = validate();
    //if (res == false) {
    //    return false;
    //}
    $.ajax({
        url: "/MachineTypeFrequency/CountbyMachineTypeID/",
        data: { 'MachineTypeID': $('#MachineTypeID').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result > 0) {
                alert("MachineTypeID already exists");
                $('#MachineTypeID').css('border-color', 'Red');
            }
            else {
                if (document.getElementById("Daily").checked == true) {
                    var MachineTypeFrequency = {
                        MachineTypeID: $('#MachineTypeID').val(),
                        FrequencyID: 1
                    };
                    InsertMachineTypeFrequency(MachineTypeFrequency);
                }
                if (document.getElementById("Weekly").checked == true) {
                    var MachineTypeFrequency = {
                        MachineTypeID: $('#MachineTypeID').val(),
                        FrequencyID: 2
                    };
                    InsertMachineTypeFrequency(MachineTypeFrequency);
                }
                if (document.getElementById("Monthly").checked == true) {
                    var MachineTypeFrequency = {
                        MachineTypeID: $('#MachineTypeID').val(),
                        FrequencyID: 3
                    };
                    InsertMachineTypeFrequency(MachineTypeFrequency);
                }
                if (document.getElementById("3Months").checked == true) {
                    var MachineTypeFrequency = {
                        MachineTypeID: $('#MachineTypeID').val(),
                        FrequencyID: 4
                    };
                    InsertMachineTypeFrequency(MachineTypeFrequency);
                }
                if (document.getElementById("6Months").checked == true) {
                    var MachineTypeFrequency = {
                        MachineTypeID: $('#MachineTypeID').val(),
                        FrequencyID: 5
                    };
                    InsertMachineTypeFrequency(MachineTypeFrequency);
                }
                if (document.getElementById("Yearly").checked == true) {
                    var MachineTypeFrequency = {
                        MachineTypeID: $('#MachineTypeID').val(),
                        FrequencyID: 6
                    };
                    InsertMachineTypeFrequency(MachineTypeFrequency);
                }
                loadData();
            }
        },
        error: function (errormessage) {
        }
    });    
}



function DeleleByMachineTypeID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/MachineTypeFrequency/Delete/" + ID,
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
function InsertMachineTypeFrequency(MachineTypeFrequency) {
    var res = validate();
    if (res == false) {
        return false;
    }
    $.ajax({
        url: "/MachineTypeFrequency/Insert/",
        data: JSON.stringify(MachineTypeFrequency),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert('Error InsertMachineTypeContentList' + errormessage.responseText);
        }
    });
}


