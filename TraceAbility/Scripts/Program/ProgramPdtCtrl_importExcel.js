var table_title_checkingData = "Bảng Kiểm Tra Dữ Liệu Trước Import/Checking Data Table";
var table_title_AfterImportData = "Bảng Dữ Liệu khi Import/Importing Data Table";
$(document).ready(function () {
    //init.
    $('#btnImportExcel').prop('disabled', true);

    if ($.fn.dataTable.isDataTable('.tbl_ImportExecl_ProgramPdtCtrl'))
        $('.tbl_ImportExecl_ProgramPdtCtrl').dataTable().fnDestroy();
    $('.tbl_ImportExecl_ProgramPdtCtrl').DataTable(
        {
            "pageLength": 15,
            "searching": false,
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
    //var table_title_checkingData = "Bảng Kiểm Tra Dữ Liệu Trước Khi Import/Checking Data Table";
    $('.tbl_ImportExecl_ProgramPdtCtrl').append('<caption style="caption-side: top; text-align:center;"><div class="box-header with-border"><h3 class="box-title myDataTable-title">' + table_title_checkingData + '</h3></div></caption>');
});
/*
function ProgramPdtCtrl_ExecuteImportExcel() {
    var formData = new FormData();
    var fileImport = document.getElementById("ChooseFileUpload").files[0];
    formData.append("fileImport", fileImport);

    $.ajax({
        url: "/ProgramPdtCtrl/ImportExcel_ProgramPdtCtrl/",
        data: formData,
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "Json",
        success: function (result) {
            if (result.Code == "00") {
                //$('#str_viewImportStatusTable_ToHTML').html(result.Message);
                if ($.fn.dataTable.isDataTable('.tbl_ImportExecl_ProgramPdtCtrl'))
                    $('.tbl_ImportExecl_ProgramPdtCtrl').dataTable().fnDestroy();
                $('.tbody').html(result.Message);
                $('.tbl_ImportExecl_ProgramPdtCtrl').DataTable(
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
                alert('Cập nhật dữ liệu thành công/Import successfully!');
            }
            else if (result.Code == "99")
                alert('Lỗi thao tác dữ liệu/Error: ' + result.Message);
        },
        error: function (errormessage) {
            alert('Lỗi/Error: ' + errormessage.responseText);
        }
    });
}
*/

function ProgramPdtCtrl_CheckImportExcelFile(isImportToDB) {
    isImportToDB = !!isImportToDB;    //if isImportToDB = true, import to DB. If isImportToDB = false, only checking data excel file.
    var formData = new FormData();
    var fileImport = document.getElementById("ChooseFileUpload").files[0];
    formData.append("fileImport", fileImport);
    formData.append("isImportToDB", isImportToDB);
    $.ajax({
        url: "/ProgramPdtCtrl/CheckFile_ImportExcel_ProgramPdtCtrl/",
        data: formData,
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "Json",
        success: function (result) {
            if (result.Code == "00") {
                if ($.fn.dataTable.isDataTable('.tbl_ImportExecl_ProgramPdtCtrl'))
                    $('.tbl_ImportExecl_ProgramPdtCtrl').dataTable().fnDestroy();
                $('.tbody').html(result.Message);
                $('.tbl_ImportExecl_ProgramPdtCtrl').DataTable(
                    {
                        "pageLength": 15,
                        "searching": false,
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
                $('.tbl_ImportExecl_ProgramPdtCtrl').DataTable().columns.adjust();
                //alert after checking data.
                var checkAllRowsOK = "Success";
                var t = document.getElementsByClassName('tbl_ImportExecl_ProgramPdtCtrl');
                var list = t[0].getElementsByClassName("statusRowImport");
                var i;
                for (i = 0; i < list.length; i++) {
                    if (list[i].innerHTML.includes("Fail"))
                    {
                        alert("Có lỗi, kiểm tra lại file trước khi import./Error data, checking data before importing.");
                        $('#btnImportExcel').prop('disabled', true);
                        checkAllRowsOK = "Fail";
                        break;
                    }
                }
                if (checkAllRowsOK == "Success")
                    $('#btnImportExcel').prop('disabled', false);
                else if (checkAllRowsOK == "Fail")
                    $('#btnImportExcel').prop('disabled', true);

                if (isImportToDB) {
                    if (checkAllRowsOK == "Success") {
                        alert('Nhập liệu file thành công/Import file successfully!');
                        $('.myDataTable-title').html(table_title_AfterImportData);
                    } else if (checkAllRowsOK == "Fail")
                    {
                        alert("Có lỗi, kiểm tra lại file trước khi import./Error data, checking data before importing.");
                    }
                }
                else
                    $('.myDataTable-title').html(table_title_checkingData);
            }
            else if (result.Code == "99")
                alert('Lỗi thao tác dữ liệu/Error: ' + result.Message);
        },
        error: function (errormessage) {
            $('#btnImportExcel').prop('disabled', true);
            alert('Lỗi/Error: ' + errormessage.responseText);
        }
    });
}





