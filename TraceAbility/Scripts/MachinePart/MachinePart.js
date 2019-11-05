//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    LoadDataMachineType();
    var machinePartID;
});

function loadData() {
    $.ajax({
        url: "/MachinePart/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Description + '</td>';
                html += '<td>' + item.TypeName + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            if ($.fn.dataTable.isDataTable('.myTable'))
                $('.myTable').dataTable().fnDestroy();
            $('.tbody').html(html);
            $('.myTable').DataTable(
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
            })
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}
function LoadDataMachineType() {
    $.ajax({
        url: "/MachineType/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var options;
            $.each(result, function (key, item) {
                options += '<option value="' + item.ID + '">' + item.TypeName + '</option>';
            });
            $('#MachineType').html(options);
        },
    });
}
function clearTextBox() {
    LoadDataMachineType();
    $('#Name').val("");
    $('#Description').val("");
    $('#MachineType').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Thêm mới bộ phận máy/Add New MachinePart. ");

    $('#Name').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#MachineType').css('border-color', 'lightgrey');
}

function GetbyID(ID) {
    machinePartID = ID;
    $('#Name').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#MachineType').css('border-color', 'lightgrey');

    $.ajax({
        url: "/MachinePart/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Name').val(result.Name);
            $('#Description').val(result.Description);
            $('#MachineType').val(result.MachineType);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Cập nhật bộ phận máy/Update MachinePart. ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }   
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertMachinePart() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var MachinePartObj = {
        ID: 0,
        Name: $('#Name').val(),
        Description: $('#Description').val(),
        MachineType: $('#MachineType').val()
    };
    $.ajax({
        url: "/MachinePart/Insert/",
        data: JSON.stringify(MachinePartObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            alert("Thêm mới dữ liệu thành công / Insert successful! ");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function UpdateMachinePart() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var MachinePartObj = {
        ID: machinePartID,
        Name: $('#Name').val(),
        Description: $('#Description').val(),
        MachineType: $('#MachineType').val()
    };
    $.ajax({
        url: "/MachinePart/Insert/",
        data: JSON.stringify(MachinePartObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
             alert("Cập nhật dữ liệu thành công / Insert successful! ");
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
            url: "/MachinePart/Delete/" + ID,
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