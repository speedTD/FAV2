//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});
var loginUserID = 0;
var loginUserName = "";

function loadData() {
    $.ajax({
        url: "/ProgramName/ListAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            //loginUserID = result.UserID;
            //loginUserName = result.UserName;
            $.each(result.lstProgramName, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.programName + '</td>';
                html += '<td>' + item.ProgramType + '</td>';
                html += '<td>' + item.FactoryID + '</td>';
                html += '<td>' + item.OperatorName + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.programName + '\')">Edit</a> | <a href="#" onclick="return DeleleLineByID(\'' + item.programName + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('#OperatorID').val($('#UserLogin').data('id'));
            if ($.fn.dataTable.isDataTable('.myTable'))
                $(".myTable").dataTable().fnDestroy();
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
            });
        },
        error: function (errormessage) {
            //alert('Error' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#OperatorID').val($('#UserLogin').data('username'));
    $('#OperatorID').prop('disabled', true);
    $('#ProgramName').val("");
    $('#ProgramType').val("");
    $('#FactoryID').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Thêm tên chương trình/Add New Program Name ");
   
    $('#ProgramName').css('border-color', 'lightgrey');
    $('#ProgramName').prop('disabled', false);
    $('#ProgramType').css('border-color', 'lightgrey');
    $('#FactoryID').css('border-color', 'lightgrey');
}

function GetbyID(programName) {
    $('#OperatorID').val($('#UserLogin').data('username'));
    $('#ProgramName').css('border-color', 'lightgrey');
    $('#ProgramType').css('border-color', 'lightgrey');
    $('#FactoryID').css('border-color', 'lightgrey');
    $('#OperatorID').css('border-color', 'lightgrey');
    $.ajax({
        url: "/ProgramName/GetbyID/",
        data: { 'nameProgram': programName },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ProgramName').val(result.programName);
            $('#ProgramName').prop('disabled', true);
            $('#ProgramType').val(result.ProgramType);
            $('#FactoryID').val(result.FactoryID);
            $('#OperatorID').val($('#UserLogin').data('username'));
            $('#OperatorID').prop('disabled', true);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Menu ');
        },
        error: function (errormessage) {
            alert('Lỗi/Error function GetbyID(programName) ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#ProgramName').val().trim() == "") {
        $('#ProgramName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProgramName').css('border-color', 'lightgrey');
    }
    if ($('#ProgramType').val().trim() == "") {
        $('#ProgramType').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProgramType').css('border-color', 'lightgrey');
    }
    if ($('#FactoryID').val().trim() == "") {
        $('#FactoryID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FactoryID').css('border-color', 'lightgrey');
    }
    if ($('#OperatorID').val().trim() == "") {
        $('#OperatorID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OperatorID').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertProgramName() {
    var res = validate();
    if (res == false) {
        return false;
    }
    $.ajax({
        url: "/ProgramName/CountbyID/",
        data: { 'nameProgram': $('#ProgramName').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result1) {
            if (result1 == 0) {
                var programname = {
                    programName: $('#ProgramName').val(),
                    ProgramType: $('#ProgramType').val(),
                    FactoryID: $('#FactoryID').val(),
                    OperatorID: $('#UserLogin').data('id')
                };
                $.ajax({
                    url: "/ProgramName/Insert/",
                    data: JSON.stringify(programname),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        loadData();
                        alert("Thêm mới thành công!/Insert successfully!");
                        $('#myModal').modal('hide');
                    },
                    error: function (errormessage) {
                        alert('Lỗi/Error ' +errormessage.responseText);
                    }
                });
            }
            else {
                alert("Tên chương trình đã tồn tại/ProgramName already exists.");
                $('#ProgramName').css('border-color', 'Red');
            }
        },
    });
}

function UpdateProgramName() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var programname = {
        programName: $('#ProgramName').val(),
        ProgramType: $('#ProgramType').val(),
        FactoryID: $('#FactoryID').val(),
        OperatorID: $('#UserLogin').data('id')
    };
    $.ajax({
        url: "/ProgramName/Insert/",
        data: JSON.stringify(programname),
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

function DeleleLineByID(id) {
    var ans = confirm("Có chắc chắn muốn xóa không!/Are you sure you want to delete " + id + " ?");
    if (ans) {
        $.ajax({
            url: "/ProgramName/Delete?id=" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result.Code == "00") {
                    loadData();
                    alert("Xóa thành công!/Deleted successfully!");
                }
            },
            error: function (errormessage) {
                alert('Lỗi/Error ' +errormessage.responseText);
            }
        });
    }
}



