//Load Data in Table when documents is ready  
$(document).ready(function () {
    var now = new Date();
    //console.log('1   :    ' + now.getTime());
    loadData();
    var machineTypeID;
});
function loadData() {
    $.ajax({
        url: "/MachineType/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var now = new Date();
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.TypeName + '</td>';
                html += '<td>' + item.Description + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            if ($.fn.dataTable.isDataTable('.myTable'))
                $('.myTable').dataTable().fnDestroy();
            $('.tbody').html(html);
            $('.myTable').DataTable(
                {
                    "pageLength": 10,
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
function clearTextBox() {
    $('#TypeName').val("");
    $('#Description').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New MachineType ");
    $('#TypeName').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
}
function GetbyID(ID) {
    machineTypeID = ID;
    $('#Name').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#MachineType').css('border-color', 'lightgrey');

    $.ajax({
        url: "/MachineType/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#TypeName').val(result.TypeName);
            $('#Description').val(result.Description);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update MachineType');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}
function validate() {
    var isValid = true;
    if ($('#TypeName').val().trim() == "") {
        $('#TypeName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TypeName').css('border-color', 'lightgrey');
    }
    return isValid;
}
// **** Region CRUD ***
function InsertMachineType() {
    var res = validate();
    if (res == false) {
        return false;
    }
    //check exists.
    $.ajax({
        url: "/MachineType/CountbyTypeName/",
        data: { 'TypeName': $('#TypeName').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (resultCheck) {
            if (resultCheck == 0) {
                var MachineTypeObj = {
                    ID: 0,
                    TypeName: $('#TypeName').val(),
                    Description: $('#Description').val(),
                    isActive: 1
                };
                $.ajax({
                    url: "/MachineType/Insert/",
                    data: JSON.stringify(MachineTypeObj),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        loadData();
                        $('#myModal').modal('hide');
                        alert('Thêm mới dữ liệu thành công');
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
            }
            else {
                alert("MachineType already exists. Cannot delete.");
                $('#MachineID').css('border-color', 'Red');
            }
        },
    });
}
function UpdateMachineType() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var MachineTypeObj = {
        ID: machineTypeID,
        TypeName: $('#TypeName').val(),
        Description: $('#Description').val(),
        isActive: 1
    };
    $.ajax({
        url: "/MachineType/Insert/",
        data: JSON.stringify(MachineTypeObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            alert('Cập nhật dữ liệu thành công');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function DeleleByID(ID) {
    var ans = confirm("Bạn muốn xóa dữ liệu! /Are you sure you want to delete ?" + ID);
    if (ans) {
        //check exist machine type in table MachineList.
        $.ajax({
            url: "/Machine/GetbyMachineTypeID/",
            data: { 'MachineTypeID': ID },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (resultCheck) {
                console.log(resultCheck);
                if ((resultCheck.Code == "00" || resultCheck.Code == "01") && resultCheck.Total == 0) { //not exists in Machine Table.
                    $.ajax({
                        url: "/MachineMtnContentList/GetbyMachineTypeID/",
                        data: { 'MachineTypeID': ID },
                        type: "GET",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (resultCheck) {
                            if ((resultCheck.Code == "00" || resultCheck.Code == "01") && resultCheck.Total == 0) { //not exists.
                                $.ajax({
                                    url: "/MachineType/Delete/" + ID,
                                    type: "POST",
                                    contentType: "application/json;charset=UTF-8",
                                    dataType: "json",
                                    success: function (result) {
                                        loadData();
                                        alert("Xóa thành công!/Deleted successful!");
                                    },
                                    error: function (errormessage) {
                                        alert(errormessage.responseText);
                                    },
                                });
                            }
                            else
                                alert("Chủng loại máy đang được dùng trong danh mục nội dung bảo trì. Không thể xóa!/Machine type is in use at Machine content list. Cannot delete.");
                        },
                        error: function (errormessage) {
                            alert('Error Ajax /MachineMtnContentList/GetbyMachineTypeID/ ' + errormessage.responseText);
                        }
                    });

                }
                else
                    alert("Chủng loại máy đang được dùng trong danh sách Máy. Không thể xóa!/Machine type is in use at Machine List. Cannot delete.");
            },
            error: function (errormessage) {
                alert('Error Ajax /Machine/GetbyMachineTypeID/ ' + errormessage.responseText);
            }
        });
    }
}