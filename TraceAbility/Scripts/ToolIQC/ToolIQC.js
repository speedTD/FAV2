//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    load_ToolType("");
    clearTextBox();
    // alert($('#UserLogin').data('username'));   -- phai de chu thuong: data-username
});

function loadData() {
    $.ajax({
        url: "/ToolIQC/ListAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            console.log(result)
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td style="display:none">' + item.ID + '</td>';
                html += '<td>' + item.ToolTypeName + '</td>';
                html += '<td>' + item.PrefixToolID + '</td>';
                html += '<td>' + item.FromToolID + '</td>';
                html += '<td>' + item.ToToolID + '</td>';

                //html += '<td>' + item.FileUrl + '</td>';
                var FileUrl_split = item.FileUrl.split('/');
                var fileName = FileUrl_split[FileUrl_split.length - 1];
                html += '<td>' + '<a href="' + item.FileUrl.replace('~', "..") + '" download>' + fileName + '<a/>' + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
                html += '<td>' + item.OperatorName + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return ToolIQC_DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);

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
            alert('Lỗi hiển thị danh sách/Error while loading data: ' + errormessage.responseText);
        }
    });
}

function load_ToolType(selectObject) {
    $.ajax({
        url: "/ToolTypeList/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<option value="' + item.ToolTypeID + '">' + item.ToolTypeName + '</option>';
            });
            $('#ToolType').html(html);
        },
        error: function (errormessage) {
            alert('Error function load_ToolType(selectObject) :' + errormessage.responseText);
        }
    });
}

function getToolTypeID_ByToolTypeName(toolTypeName) {
    $.ajax({
        url: "/ToolTypeList/GetbyToolTypeName",
        data: { 'ToolTypeName': toolTypeName },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "01") //ko ton tai ban ghi
            {
                $('#ToolType').val(0);
            }
            else {
                $('#ToolType').val(result.ToolTypeID);
            }

        },
        error: function (errormessage) {
            alert('Error function get_ToolTypeByToolTypeName(selectObject) :' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#ID').val("0");
    $('#ToolType').val("");
    $('#PrefixToolID').val("");
    $('#FromToolID').val("");
    $('#ToToolID').val("");
    $('#FileUrl').val("");
    $("#FileUpload").val(null);

    $('#OperatorID').val($('#UserLogin').data('username'));
    $('#OperatorID').prop('disabled', true);
    $("#CreatedDate").datepicker("setDate", new Date());
    $('#FactoryID').css('border-color', 'lightgrey');
    $('#FileUrl_DownloadLink').html('');
    $('#myModalLabel').text("Add New ");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

function GetbyID(id) {
    clearTextBox();
    $('#ToolType').css('border-color', 'lightgrey');
    $('#PrefixToolID').css('border-color', 'lightgrey');
    $('#FromToolID').css('border-color', 'lightgrey');
    $('#ToToolID').css('border-color', 'lightgrey');
    $('#FileUrl').css('border-color', 'lightgrey');
    $('#CreatedDate').css('border-color', 'lightgrey');
    $('#FactoryID').css('border-color', 'lightgrey');

    $.ajax({
        url: "/ToolIQC/GetbyID/",
        data: { 'id': id },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ID').val(result.ID);
            $('#ToolType').val(result.ToolTypeID);
            $('#PrefixToolID').val(result.PrefixToolID);
            $('#FromToolID').val(result.FromToolID);
            $('#ToToolID').val(result.ToToolID);
            $('#CreatedDate').val(SM_ConvertMsJsonDate_ToString(result.CreatedDate));
            $('#FileUrl').val(result.FileUrl);
            $('#OperatorID').val(result.OperatorName);

            var FileUrl_split = result.FileUrl.split('/');
            var fileName = FileUrl_split[FileUrl_split.length - 1];
            $('#FileUrl_DownloadLink').html('<a href="' + result.FileUrl.replace('~', "..") + '" download>' + fileName + '<a/>');

            $('#FactoryID').val(result.FactoryID);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    //if ($('#FileUrl').val().trim() == "") {
    //    $('#FileUrl').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#FileUrl').css('border-color', 'lightgrey');
    //}
    var _tooltype = $('#ToolType').val();
    if (_tooltype == "" || _tooltype == null) {
        $('#ToolType').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ToolType').css('border-color', 'lightgrey');
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
function ToolIQC_Insert() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var _file = document.getElementById("FileUpload").files;
    var _filename = '';
    if (_file.length > 0) {
        _filename = _file[0].name;
    }

    $.ajax({
        url: "/ToolIQC/CountbyFileName/",
        data: { 'FileName': _filename },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result1) {
            // check if not exists.
            if (result1 == 0) {
                //get values to transfer.
                var dataTransfer = new FormData();
                var fileUpload = document.getElementById("FileUpload").files;
                if (fileUpload.length > 0) {
                    for (var i = 0; i < fileUpload.length; i++) {
                        dataTransfer.append("fileInput", fileUpload[i]);
                    }
                }
                var toolIQCObj = {
                    ID: $('#ID').val(),
                    ToolTypeID: $('#ToolType').val(),
                    PrefixToolID: $('#PrefixToolID').val(),
                    FromToolID: $('#FromToolID').val(),
                    ToToolID: $('#ToToolID').val(),
                    OperatorID: $('#UserLogin').data('id'),
                    FileUrl: $('#FileUrl').val(),
                    FactoryID: $('#FactoryID').val()
                };
                dataTransfer.append('toolIQCObj', JSON.stringify(toolIQCObj));

                $.ajax({
                    url: "/ToolIQC/Insert/",
                    data: dataTransfer,
                    type: "POST",
                    contentType: false,
                    processData: false,
                    dataType: "json",
                    success: function (result) {
                        loadData();
                        $('#myModal').modal('hide');
                        clearTextBox();
                        alert("Insert successful! ");
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
            }
            else {
                alert("File already exists! Cannot insert.");
            }
        },
    });
}
function ToolIQC_Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var toolIQC = {
        ID: $('#ID').val(),
        ToolTypeID: $('#ToolType').val(),
        PrefixToolID: $('#PrefixToolID').val(),
        FromToolID: $('#FromToolID').val(),
        ToToolID: $('#ToToolID').val(),
        OperatorID: $('#UserLogin').data('id'),
        FileUrl: $('#FileUrl').val(),
        FactoryID: $('#FactoryID').val()
    };
    $.ajax({
        url: "/ToolIQC/Update/",
        data: JSON.stringify(toolIQC),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            alert("Update successful!");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ToolIQC_DeleleByID(id) {
    var ans = confirm("Are you sure you want to delete ?" + id);
    if (ans) {
        $.ajax({
            url: "/ToolIQC/Delete/" + id,
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

// **** Region Event ***************************************************** 
//Trigger now when you have selected any file 
$("#FileUpload").change(function (e) {
    var filename = e.target.files[0].name;
    alert('FileUpload  ' + filename);
    if (filename.trim() != "") {
        debugger;
        var OnlyFileName = filename.substring(0,(filename.lastIndexOf(".") ));
        debugger;
        var arr_FileName = OnlyFileName.split('_');
        if (arr_FileName.length != 4)
        {
            alert("Định dạng file không hợp lệ. Kiểm tra lại định dạng file gồm có 4 thành phần: ToolType_PrefixToolID_FromToolID_ToToolID / File template is not valid.");
            return;
        }

        var ToolTypeName = arr_FileName[0];
        var PrefixToolID = arr_FileName[1];
        var FromToolID = arr_FileName[2];
        var ToToolID = arr_FileName[3];

        getToolTypeID_ByToolTypeName(ToolTypeName);  // gan gia tri cho $('#ToolType')
        var ToolTypeID = $('#ToolType');

        if (ToolTypeID == 0) {
            alert("Không tồn tại ToolType: " + ToolTypeName  + " /Tooltype doesn't exist!");
            return;
        }
        $('#ToolType').val(ToolTypeID);
        $('#PrefixToolID').val(PrefixToolID);
        $('#FromToolID').val(FromToolID);
        $('#ToToolID').val(ToToolID);
    }
});
