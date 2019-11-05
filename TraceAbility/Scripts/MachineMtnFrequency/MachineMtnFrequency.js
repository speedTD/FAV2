//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    $('#ReceiveDate').datepicker();
});
var _MachineID;
function loadData() {
    $.ajax({
        url: "/MachineMtnFrequency/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MachineID + '</td>';
                //html += '<td>' + item.MachineName + '</td>';
                html += '<td>' + item.Daily + '</td>';
                html += '<td>' + item.Weekly + '</td>';
                html += '<td>' + item.Monthly + '</td>';
                html += '<td>' + item.ThreeMonths + '</td>';
                html += '<td>' + item.SixMonths + '</td>';
                html += '<td>' + item.Yearly + '</td>';
                html += '<td><a href="#" onclick="return GetbyMachineID(\'' + item.MachineID + '\')">Edit</a> | <a href="#" onclick="return DeleleByMachineID(\'' + item.MachineID + '\')">Delete</a></td>';
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
    $('#MachineId').val("");
    $("#Daily").prop("checked", false);
    $("#3Months").prop("checked", false);
    $("#Weekly").prop("checked", false);
    $("#6Months").prop("checked", false);
    $("#Monthly").prop("checked", false);
    $("#Yearly").prop("checked", false);

    //$('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Machine Maintenance Frequency");

    $('#MachineID').css('border-color', 'lightgrey');
}

function GetbyMachineID(MachineID) {
    $('#MachineID').css('border-color', 'lightgrey');
    $.ajax({
        url: "/MachineMtnFrequency/GetbyMachineID/",
        data: { 'MachineID': MachineID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                $('#MachineId').val(result.MachineID);
                _MachineID = result.MachineID;
                if (result.FrequencyID == 1) {
                    document.getElementById("Daily").checked = true;
                }
                if (result.FrequencyID == 2) {
                    document.getElementById("Weekly").checked = true;
                }
                if (result.FrequencyID == 3) {
                    document.getElementById("Monthly").checked = true;
                }
                if (result.FrequencyID == 4) {
                    document.getElementById("3Months").checked = true;
                }
                if (result.FrequencyID == 5) {
                    document.getElementById("6Months").checked = true;
                }
                if (result.FrequencyID == 6) {
                    document.getElementById("Yearly").checked = true;
                }
            });
            $('#myModal').modal('show');
            //$('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Machine Maintenance Content List ');
        },
        error: function (errormessage) {
            alert('Error GetbyMachineID():   ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#MachineId').val().trim() == "") {
        $('#MachineId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineId').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertMachineMtnFrequencyList() {
    var res = validate();
    //if (res == false) {
    //    return false;
    //}
    if (document.getElementById("Daily").checked == true) {
        var MachineMtnContentListObj = {
            MachineID: $('#MachineId').val(),
            FrequencyID: 1
        };
        InsertMachineMtnFrequencyImportFile(MachineMtnContentListObj);
    }
    if (document.getElementById("Weekly").checked == true) {
        var MachineMtnContentListObj = {
            MachineID: $('#MachineId').val(),
            FrequencyID: 2
        };
        InsertMachineMtnFrequencyImportFile(MachineMtnContentListObj);
    }
    if (document.getElementById("Monthly").checked == true) {
        var MachineMtnContentListObj = {
            MachineID: $('#MachineId').val(),
            FrequencyID: 3
        };
        InsertMachineMtnFrequencyImportFile(MachineMtnContentListObj);
    }
    if (document.getElementById("3Months").checked == true) {
        var MachineMtnContentListObj = {
            MachineID: $('#MachineId').val(),
            FrequencyID: 4
        };
        InsertMachineMtnFrequencyImportFile(MachineMtnContentListObj);
    }
    if (document.getElementById("6Months").checked == true) {
        var MachineMtnContentListObj = {
            MachineID: $('#MachineId').val(),
            FrequencyID: 5
        };
        InsertMachineMtnFrequencyImportFile(MachineMtnContentListObj);
    }
    if (document.getElementById("Yearly").checked == true) {
        var MachineMtnContentListObj = {
            MachineID: $('#MachineId').val(),
            FrequencyID: 6
        };
        InsertMachineMtnFrequencyImportFile(MachineMtnContentListObj);
    }
    loadData();
}


/*
function InsertMachineMtnFrequency(MachineMtnContentListObj) {
    $.ajax({
        url: "/MachineMtnFrequency/Insert/",
        data: JSON.stringify(MachineMtnContentListObj),
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
    InsertMachineMtnContentList(MachineMtnContentListObj);
}
*/
/*
function InsertMachineMtnFrequencyImportFile(MachineMtnContentListObj) {
    var formData = new FormData();
    var fileImport = document.getElementById("ChooseFile").files[0];
    formData.append("fileImport", fileImport);
    formData.append("MachineMtnContentListObj", JSON.stringify(MachineMtnContentListObj));
    
    $.ajax({
        url: "/MachineMtnFrequency/InsertWithFileImport/",
        data: formData,
        type: "POST",
        contentType: false,
        processData: false,
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
*/


function ExecuteImportExcel_MachineMtnContentListByType(MachineMtnContentListObj) {
    var formData = new FormData();
    var fileImport = document.getElementById("ChooseFile").files[0];
    formData.append("file", fileImport);
    formData.append("MachineMtnContentListObj", JSON.stringify(MachineMtnContentListObj));

    $.ajax({
        url: "/MachineMtnFrequency/InsertWithFileImport/",
        data: formData,
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "json",
        success: function (result) {
            //loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert('InsertMachineMtnFrequencyImportFile: ' + errormessage.responseText);
        }
    });
}


function updatemachinemtncontentlist() {
    var res = validate();
    if (res == false) {
        return false;
    }
    DeleleByMachineID(_MachineID);
    InsertMachineMtnFrequencyList();
}

function DeleleByMachineID(MachineID) {
    var ans = confirm("Are you sure you want to delete ?" + MachineID);
    if (ans) {
        $.ajax({
            url: "/MachineMtnFrequency/Delete/" + MachineID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
                alert("Deleted successful!");
            },
            error: function (errormessage) {
                alert('Error DeleleByMachineID' + errormessage.responseText);
            }
        });
    }
}
function InsertMachineMtnContentList(MachineMtnContentListObj) {
    var res = validate();
    if (res == false) {
        return false;
    }
    var filePath = $('#ChooseFile').val();
    $.ajax({
        url: "/MachineMtnContentList/ImportFileMachineMtnContent/",
        data: JSON.stringify(MachineMtnContentListObj, filePath),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert('Error InsertMachineMtnContentList' + errormessage.responseText);
        }
    });
}


