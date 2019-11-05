//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/Line/ListAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.FactoryID + '</td>';
                html += '<td>' + item.LineID + '</td>';
                html += '<td>' + item.LineName + '</td>';                
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.LineID + '\')">Edit</a> | <a href="#" onclick="return DeleleLineByID(\'' + item.LineID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            if ($.fn.dataTable.isDataTable('.myTable')) {
                $(".myTable").dataTable().fnDestroy();
            }
            $('.tbody').html(html);
            
            $('.myTable').dataTable(
            {
                "pageLength": 15,
                "language": {
                    "lengthMenu": "",
                    "zeroRecords": "Không có dữ liệu.",
                    "info": "",
                    "infoEmpty": "Không có bản ghi nào.",
                    "infoFiltered": "",
                    "paginate": {
                        "next": ">>",
                        "previous": "<<"
                    }
                }
            });
        },
        error: function (errormessage) {
            //alert('Error' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#LineID').val("");
    $('#LineName').val("");
    $('#FactoryID').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New Line ");
    $('#LineID').prop('disabled', false);
    $('#LineID').css('border-color', 'lightgrey');
    $('#LineName').css('border-color', 'lightgrey');
    $('#FactoryID').css('border-color', 'lightgrey');
}

function GetbyID(LineID) {
    $('#LineID').css('border-color', 'lightgrey');   
    $('#LineName').css('border-color', 'lightgrey');
    $('#FactoryID').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Line/GetbyID/",
        data: { 'LineID': LineID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#LineID').val(result.LineID);
            $('#LineID').prop('disabled', true);
            $('#LineName').val(result.LineName);
            $('#FactoryID').val(result.FactoryID);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Menu ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#LineID').val().trim() == "") {
        $('#LineID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LineID').css('border-color', 'lightgrey');
    }
    if ($('#LineName').val().trim() == "") {
        $('#LineName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LineName').css('border-color', 'lightgrey');
    }
    var _factoryID = $('#FactoryID').val();
    if (_factoryID == "" || _factoryID == null) {
        $('#FactoryID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FactoryID').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertLine() {
    var res = validate();
    if (res == false) {
        return false;
    }
    //check exist.
    $.ajax({
        url: "/Line/CountbyID/",
        data: { 'LineID': $('#LineID').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (resultCheck) {
            if (resultCheck == 0) {
                var LineObj = {
                    LineID: $('#LineID').val(),
                    LineName: $('#LineName').val(),
                    FactoryID: $('#FactoryID').val(),
                    isActive: 1
                };
                $.ajax({
                    url: "/Line/Insert/",
                    data: JSON.stringify(LineObj),
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
                alert("Line already exists. Cannot insert.");
                $('#LineID').css('border-color', 'Red');
            }
        },
    });    
}

function UpdateLine() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var LineObj = {
        LineID: $('#LineID').val(),
        LineName: $('#LineName').val(),
        FactoryID: $('#FactoryID').val(),
        isActive: 1
    };
    $.ajax({
        url: "/Line/Update/",
        data: JSON.stringify(LineObj),
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

function DeleleLineByID(LineID) {
    var ans = confirm("Are you sure you want to delete ? " + LineID);
    if (ans) {
        $.ajax({ //check exists MachineID in table MachineMtn.
            url: "/Machine/CountbyLineID/",
            data: { 'LineID': LineID },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (resultCheck) {
                if ((resultCheck.Code == "00" || resultCheck.Code == "01") && resultCheck.Total == 0) {
                    $.ajax({
                        url: "/Line/Delete/" + LineID,
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
                else
                    alert("Line đang được gắn với MachineID. Không thể xóa!/Line is in use at Machine List. Cannot delete.");
            }
        });
    }

}



