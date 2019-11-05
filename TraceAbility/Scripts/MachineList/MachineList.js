//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
    ////$('#ReceiveDate').datepicker({ format: 'dd/mm/yyyy' });
    
    ////$('#ReceiveDate').datepicker.setDefaults($.datepicker.regional.vi);
    //// Reset language
    ////$.datepicker.setDefaults($.datepicker.regional[""]);
    //console.log('ccc');
    //$.datepicker.setDefaults($.datepicker.regional['vi']);
    //$('#ReceiveDate').datepicker('option', $.datepicker.regional['vi']);
    ////$('#ReceiveDate').datepicker($.datepicker.regional.vi);
    //console.log('ddd');
    //// Initialize
    ////$('#ReceiveDate').datepicker();

    load_MachineType('#MachineType');
    load_Line('#LineID');
    Common_LoadAllFactoryToCombobox('#Area');
    //$('#ReceiveDate').datepicker();
    //$('#ReceiveDate').datepicker("setDate", new Date());
    
});

function loadData() {
    $.ajax({
        url: "/Machine/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Area + '</td>';
                html += '<td>' + item.LineID + '</td>';
                html += '<td>' + item.MachineID + '</td>';
                html += '<td>' + item.MachineName + '</td>';
                html += '<td>' + item.MachineTypeName + '</td>';
                html += '<td>' + item.MachineNumber + '</td>';
                
                html += '<td>' + item.Section + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.ReceiveDate) + '</td>';
                html += '<td>' + item.Maker + '</td>';
                html += '<td>' + item.SerialNumber + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.MachineID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.MachineID + '\')">Delete</a></td>';
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

            //if ($.fn.dataTable.isDataTable('.myTable')) {
            //    //$('.myTable').dataTable().clear();
            //    $('.myTable').dataTable().fnDestroy();
            //}
            //$('.tbody').html(html);
            //$('.myTable').dataTable(
            //    {
            //        //"searching": false,
            //        "scrollX": true,  // attention: assign style="width:100%;" to table tag.
            //        //"serverSide": true,
            //        //"processing": true,
            //        //"deferLoading": 0, //prevent from DataTable automatically  call ajax in initialisation .
            //        "pageLength": 15,
            //        //"bDestroy": true, //avoid from error "Cannot reinitialise DataTable"
            //        "language": {
            //            "lengthMenu": "",
            //            "zeroRecords": "Không có dữ liệu.",
            //            "info": "",
            //            "infoEmpty": "Không có bản ghi nào.",
            //            "infoFiltered": "",
            //            "paginate": {
            //                "next": ">>",
            //                "previous": "<<"
            //            }
            //        }
            //    });
           
        },
        error: function (errormessage) {
            alert('Lỗi hiển thị danh sách máy/Error loadData()' + errormessage.responseText);
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
            $(selectObject).html(html);
        },
        error: function (errormessage) {
            alert('Lỗi/Error function load_MachineType(selectObject) :' + errormessage.responseText);
        }
    });
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
            $(selectObject).html(html);
        },
        error: function (errormessage) {
            alert('Lỗi/Error function load_Line(selectObject) :' + errormessage.responseText);
        }
    });
}

function clearTextBox() {
    $('#MachineID').val("");
    $('#MachineName').val("");
    $('#MachineNumber').val("");
    $('#Area').val("");
    $('#Section').val("");
    $('#ReceiveDate').val("");
    $('#Maker').val("");
    $('#SerialNumber').val("");
    $('#MachineID').prop('disabled', false);
    $('#LineID').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Thêm máy/Add New Machine ");

    $('#MachineID').css('border-color', 'lightgrey');
    $('#MachineName').css('border-color', 'lightgrey');
    $('#MachineNumber').css('border-color', 'lightgrey');
    $('#Area').css('border-color', 'lightgrey');
    $('#Section').css('border-color', 'lightgrey');
    $('#ReceiveDate').css('border-color', 'lightgrey');
    $('#Maker').css('border-color', 'lightgrey');
    $('#SerialNumber').css('border-color', 'lightgrey');
    $('#LineID').css('border-color', 'lightgrey');
}

function GetbyID(machineID) {
    $('#MachineID').css('border-color', 'lightgrey');
    $('#MachineName').css('border-color', 'lightgrey');
    $('#MachineNumber').css('border-color', 'lightgrey');
    $('#Area').css('border-color', 'lightgrey');
    $('#Section').css('border-color', 'lightgrey');
    $('#ReceiveDate').css('border-color', 'lightgrey');
    $('#Maker').css('border-color', 'lightgrey');
    $('#SerialNumber').css('border-color', 'lightgrey');
    $('#LineID').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Machine/GetbyID/",
        data: { 'machineID': machineID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#MachineID').val(result.MachineID);
            $('#MachineID').prop('disabled', true);
            $('#MachineName').val(result.MachineName);
            $('#MachineNumber').val(result.MachineNumber);
            $('#Area').val(result.Area);
            $('#Section').val(result.Section);
            $('#ReceiveDate').val(SM_ConvertMsJsonDate_ToString(result.ReceiveDate));
            $('#Maker').val(result.Maker);
            $('#SerialNumber').val(result.SerialNumber);
            $('#MachineType').val(result.MachineTypeID);
            $('#LineID').val(result.LineID);
           // $('#isActive').val(result.isActive);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Cập nhật máy/Update Machine ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
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
    if ($('#MachineName').val().trim() == "") {
        $('#MachineName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineName').css('border-color', 'lightgrey');
    }
    if ($('#MachineNumber').val().trim() == "") {
        $('#MachineNumber').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineNumber').css('border-color', 'lightgrey');
    }
    //$('#Area').val() = $('#Area').val() || "";
    //if ($('#Area').val().trim() == "") {
    if (!$('#Area').val()) {
        $('#Area').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Area').css('border-color', 'lightgrey');
    }
    if (!$('#LineID').val()) {
        $('#LineID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LineID').css('border-color', 'lightgrey');
    }
    //if ($('#Section').val().trim() == "") {
    //    $('#Section').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Section').css('border-color', 'lightgrey');
    //}
    if ($('#ReceiveDate').val().trim() == "") {
        $('#ReceiveDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ReceiveDate').css('border-color', 'lightgrey');
    }
    //if ($('#Maker').val().trim() == "") {
    //    $('#Maker').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Maker').css('border-color', 'lightgrey');
    //}
    //if ($('#SerialNumber').val().trim() == "") {
    //    $('#SerialNumber').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#SerialNumber').css('border-color', 'lightgrey');
    //}
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertMachine() {
    var res = validate();
    if (res == false) {
        return false;
    }
    $.ajax({
        url: "/Machine/CountbyID/",
        data: { 'machineID': $('#MachineID').val() },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result1) {
            if (result1 == 0) {
                var MachineObj = {
                    MachineID: $('#MachineID').val(),
                    MachineName: $('#MachineName').val(),
                    MachineNumber: $('#MachineNumber').val(),
                    Area: $('#Area').val(),
                    Section: $('#Section').val(),
                    ReceiveDate: $('#ReceiveDate').val(),
                    Maker: $('#Maker').val(),
                    SerialNumber: $('#SerialNumber').val(),
                    MachineTypeID: $('#MachineType').val(),
                    LineID: $('#LineID').val()
                };
                $.ajax({
                    url: "/Machine/Insert/",
                    data: JSON.stringify(MachineObj),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        loadData();
                        $('#myModal').modal('hide');
                        alert("Thêm mới dữ liệu thành công / Insert successful! ");
                    },
                    error: function (errormessage) {
                        alert("Lỗi thêm mới máy/Error while inserting new machine: " + errormessage.responseText);
                    }
                });
            }
            else {
                alert("Máy đã tồn tại không thể thêm mới./Machine already exists. Cannot insert.");
                $('#MachineID').css('border-color', 'Red');
            }
        },
    });
    
}

function UpdateMachine() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var MachineObj = {
        MachineID: $('#MachineID').val(),
        MachineName: $('#MachineName').val(),
        MachineNumber: $('#MachineNumber').val(),
        Area: $('#Area').val(),       
        Section: $('#Section').val(),
        ReceiveDate: $('#ReceiveDate').val(),
        Maker: $('#Maker').val(),
        SerialNumber: $('#SerialNumber').val(),
        MachineTypeID: $('#MachineType').val(),
        LineID: $('#LineID').val()
    };
    $.ajax({
        url: "/Machine/Insert/",
        data: JSON.stringify(MachineObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            alert("Cập nhật dữ liệu thành công / Update successful! ");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleleByID(ID) {
    var ans = confirm("Bạn có chắc chắn muốn xóa không/Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({ //check exists MachineID in table MachineMtn.
            url: "/MachineMtnMonthYear/CountMachineMtByMachineID/",
            data: { 'MachineID': ID },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (resultCheck) {
                if ((resultCheck.Code == "00" || resultCheck.Code == "01") && resultCheck.Total == 0) {
                    $.ajax({
                        url: "/Machine/Delete/" + ID,
                        type: "POST",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (result) {
                            loadData();
                            alert("Xóa thành công!/Deleted successful!");
                        },
                        error: function (errormessage) {
                            alert(errormessage.responseText);
                        }
                    });
                }
                else
                    alert("Máy đang được dùng trong mục bảo trì máy. Không thể xóa!/MachineID is in use at Machine Maintenance. Cannot delete.");
            }
        });
    }
}



// **** Region Event ***************************************************** 
