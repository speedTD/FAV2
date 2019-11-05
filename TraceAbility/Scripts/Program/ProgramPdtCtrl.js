//Load Data in Table when documents is ready                        
$(document).ready(function () {
    //ProgramPdtCtrl_SearchTools(1);
    load_ProgramName();

    //init.
    var loginUserID = $('#UserLogin').data('id');
    var loginUserName = $('#UserLogin').data('username');
    $('#OperatorID').val($('#UserLogin').data('username'));
    
    myDataTableVM.init();
    ProgramPdtCtrl_SearchTools(1);
});
function ClearSearchItem() {
    $('#SearchProgramName').val("");
    $('#SearchPart').val("");
    $('#Part').val("");
}

function clearTextBox() {
    $(".data-content").html("");
    $('#ProgramName').val("");
    $('#ProgramName').prop('disabled', false);
    $('#Part').val("");
    $('#Part').prop('disabled', false);
    $('#ControlItem').val("");
    $('#ControlItem').prop('disabled', false);
    $('#ColumnName').val("");
    $('#SpecDisplay').val("");
    $('#Unit').val("");
    $('#LowerLimit').val("");
    $('#UpperLimit').val("");
    $('#OperatorID').val($('#UserLogin').data('username'));
    $('#OperatorID').prop('disabled', true);

    $('#ProgramName').css('border-color', 'lightgrey');
    $('#Part').css('border-color', 'lightgrey');
    $('#ControlItem').css('border-color', 'lightgrey');
    $('#ColumnName').css('border-color', 'lightgrey');
    $('#SpecDisplay').css('border-color', 'lightgrey');
    $('#Unit').css('border-color', 'lightgrey');
    $('#LowerLimit').css('border-color', 'lightgrey');
    $('#UpperLimit').css('border-color', 'lightgrey');
    $('#OperatorID').css('border-color', 'lightgrey');

    //insert default
    $('#btnUpdate').hide();
    $('#btnSave').show();
    $('#myModalLabel').text('Thêm mới ĐKSX tiêu chuẩn/Add new Program PDT Control');
}

function GetbyID(id) {

    $('#ProgramName').css('border-color', 'lightgrey');
    $('#Part').css('border-color', 'lightgrey');
    $('#ControlItem').css('border-color', 'lightgrey');
    $('#ColumnName').css('border-color', 'lightgrey');
    $('#SpecDisplay').css('border-color', 'lightgrey');
    $('#Unit').css('border-color', 'lightgrey');
    $('#LowerLimit').css('border-color', 'lightgrey');
    $('#UpperLimit').css('border-color', 'lightgrey');
    $('#OperatorID').css('border-color', 'lightgrey');
    $('#OperatorID').prop('disabled', true);
    $.ajax({
        url: "/ProgramPdtCtrl/GetbyID",
        data: { "ID": id },
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                $.each(result.lstProgramPdtCtrl, function (key, item) {

                    $('.ProgramName').val(item.ProgramName);
                    $('#Part').val(item.Part);
                    $('#ControlItem').val(item.ControlItem);

                    $('#ProgramName').prop('disabled', true);
                    $('#Part').prop('disabled', true);
                    $('#ControlItem').prop('disabled', true);

                    $('#ColumnName').val(item.ColumnName);
                    $('#Unit').val(item.Unit);
                    $('#LowerLimit').val(item.LowerLimit);
                    $('#UpperLimit').val(item.UpperLimit);
                    $('#SpecDisplay').val(item.SpecDisplay);
                    $('#OperatorID').val($('#UserLogin').data('username'));

                    $('#myModal').modal('show');
                    $('#btnUpdate').show();
                    $('#btnSave').hide();
                    $('#myModalLabel').text('Cập nhật ĐKSX tiêu chuẩn/Update Program PDT Control ');
                });
            }
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

//function GetbyID(id) {
//    var ProgramName = id.split(",")[0];
//    var Part = id.split(",")[1];
//    var ControlItem = id.split(",")[2];

//    $('#ProgramName').css('border-color', 'lightgrey');
//    $('#Part').css('border-color', 'lightgrey');
//    $('#ControlItem').css('border-color', 'lightgrey');
//    $('#ColumnName').css('border-color', 'lightgrey');
//    $('#SpecDisplay').css('border-color', 'lightgrey');
//    $('#Unit').css('border-color', 'lightgrey');
//    $('#LowerLimit').css('border-color', 'lightgrey');
//    $('#UpperLimit').css('border-color', 'lightgrey');
//    $('#OperatorID').css('border-color', 'lightgrey');
//    var obj = {
//        'ProgramName': ProgramName,
//        'Part': Part,
//        'ControlItem': ControlItem
//    };
//    $.ajax({
//        url: "/ProgramPdtCtrl/GetbyKey",
//        data: obj,
//        type: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            if (result.Code == "00") {
//                $.each(result.lstProgramPdtCtrl, function (key, item) {
//                    console.log(item.ProgramName)
//                    $('.ProgramName').val(item.ProgramName);
//                    $('#Part').val(item.Part);
//                    $('#ControlItem').val(item.ControlItem);

//                    $('#ProgramName1').prop('disabled', true);
//                    $('#Part').prop('disabled', true);
//                    $('#ControlItem').prop('disabled', true);

//                    $('#ColumnName').val(item.ColumnName);
//                    $('#Unit').val(item.Unit);
//                    $('#LowerLimit').val(item.LowerLimit);
//                    $('#UpperLimit').val(item.UpperLimit);
//                    $('#SpecDisplay').val(item.SpecDisplay);
//                    $('#OperatorID').val(item.OperatorID);

//                    $('#myModal').modal('show');
//                    $('#btnUpdate').show();
//                    $('#btnSave').hide();
//                });
//            }

//        },
//        error: function (errormessage) {
//            alert('Error    ' + errormessage.responseText);
//        }
//    });
//    return false;
//}

function ProgramPdtCtrl_Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ProgramPdtCtrlObj = {
        ProgramName: $('#ProgramName').val(),
        Part: $('#Part').val(),
        ControlItem: $('#ControlItem').val(),
        ColumnName: $('#ColumnName').val(),
        SpecDisplay: $('#SpecDisplay').val(),
        Unit: $('#Unit').val(),
        LowerLimit: $('#LowerLimit').val(),
        UpperLimit: $('#UpperLimit').val(),
        OperatorID: $('#UserLogin').data('id'),
    };
    $.ajax({
        url: "/ProgramPdtCtrl/Update/",
        data: JSON.stringify(ProgramPdtCtrlObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
                }
                else {
                    //loadDataBySelectedPage(1);
                    ProgramPdtCtrl_SearchTools(1);
                    $('#myModal').modal('hide');
                }
            }
            else if (result.Code == "99") {
                alert("Lỗi thêm mới/Insert error: " + result.Message);
            }
        },
        error: function (errormessage) {
            alert("errormessage ProgramPdtCtrl_Update() " + errormessage.responseText);
        }
    });
}
function validate() {
    var isValid = true;
    if ($.trim($('#ProgramName').val()) == '') {
        $('#ProgramName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProgramName').css('border-color', 'lightgrey');
    }

    //if ($('#ProgramName').val().trim() == "") {
    //    $('#ProgramName').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#ProgramName').css('border-color', 'lightgrey');
    //}

    if ($.trim($('#ControlItem').val()) == '') {
        $('#ControlItem').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ControlItem').css('border-color', 'lightgrey');
    }
    //if ($('#ColumnName').val().trim() == "") {
    //    $('#ColumnName').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#ColumnName').css('border-color', 'lightgrey');
    //}
    //if ($('#SpecDisplay').val().trim() == "") {
    //    $('#SpecDisplay').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#SpecDisplay').css('border-color', 'lightgrey');
    //}

    //if ($('#Unit').val().trim() == "") {
    //    $('#Unit').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#Unit').css('border-color', 'lightgrey');
    //}
    if ($('#LowerLimit').val().trim() == "") {
        $('#LowerLimit').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LowerLimit').css('border-color', 'lightgrey');
    }
    if ($('#UpperLimit').val().trim() == "") {
        $('#UpperLimit').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UpperLimit').css('border-color', 'lightgrey');
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
function ProgramPdtCtrl_Insert() {
    var res = validate();
    if (res == false) {
        return false;
    }
    $.ajax({
        url: "/ProgramPdtCtrl/CountbyKey/",
        data: {
            ProgramName: $('#ProgramName').val().trim(),
            Part: $('#Part').val().trim(),
            ControlItem: $('#ControlItem').val().trim()
        },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if ((result.Code == "00" || result.Code == "01") && result.Total == 0) { //not exists.
                var ProgramPdtCtrlObj = {
                    ProgramName: $('#ProgramName').val(),
                    Part: $('#Part').val(),
                    ControlItem: $('#ControlItem').val(),
                    ColumnName: $('#ColumnName').val(),
                    SpecDisplay: $('#SpecDisplay').val(),
                    Unit: $('#Unit').val(),
                    LowerLimit: $('#LowerLimit').val(),
                    UpperLimit: $('#UpperLimit').val(),
                    OperatorID: $('#UserLogin').data('id'),
                };
                $.ajax({
                    url: "/ProgramPdtCtrl/Insert/",
                    data: JSON.stringify(ProgramPdtCtrlObj),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.Code == "00") {
                            //loadDataBySelectedPage(1);
                            ProgramPdtCtrl_SearchTools(1);
                            $('#myModal').modal('hide');
                        }
                        else if (result.Code == "99")
                            alert("Lỗi thêm mới/Error insert : " + result.Message);
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
            }
            else if (result.Code == "99")
                alert("Lỗi phần kiểm tra dữ liệu ProgramPdtCtrl/CountbyKey / Error : " + resultCheck.Message);
            else
                alert("Đã tồn tại ProgramName, Part, ControlItem. Không thể thêm mới/ProgramName, Part and ControlItem already exists. Cannot insert.");
        },
    });
}
function DeleleByID(id) {
    //var ProgramName = id.split(",")[0];
    //var Part = id.split(",")[1];
    //var ControlItem = id.split(",")[2];
    //var obj = {
    //    'ProgramName': ProgramName,
    //    'Part': Part,
    //    'ControlItem': ControlItem
    //}
    var ans = confirm("Có chắc chắn muốn xóa?/Are you sure you want to delete ? ");
    if (ans) {
        $.ajax({
            url: "/ProgramPdtCtrl/DeleteByID",
            data: { "ID": id },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.Code == "00") {
                    alert("Xóa thành công/Deleted successful!");
                    //loadDataBySelectedPage(1);
                    ProgramPdtCtrl_SearchTools(1);
                }
                else if (result.Code == "99")
                    alert("Xóa bị lỗi/Delete error: " + result.Message);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

// **** End of region CRUD *****************************************************
function ProgramPdtCtrl_SearchTools(_pageNumber) {
    myDataTableVM.refresh();

    //var ProgramPdtCtrlObj = {
    //    ProgramName: $('#SearchProgramName').val().trim(),
    //    Part: $('#SearchPart').val().trim(),
    //    ControlItem: $('#SearchControlItem').val().trim(),
    //    ColumnName: "",
    //    SpecDisplay: "",
    //    Unit: "",
    //    LowerLimit: "",
    //    UpperLimit: "",
    //    OperatorID: "",
    //};
    //var returnObj = {
    //    aProgramPdtCtrl: ProgramPdtCtrlObj,
    //    PageNumber: _pageNumber
    //}
    //$.ajax({
    //    url: "/ProgramPdtCtrl/Search",
    //    data: JSON.stringify(returnObj),
    //    type: "POST",
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //    success: function (result) {
    //        if (result.Code == "00") {
    //            var html = '';
    //            //loginUserID = result.UserID;
    //            //loginUserName = result.UserName;
    //            //$('#UserID').val(loginUserName);
    //            $.each(result.lstProgramPdtCtrl, function (key, item) {
    //                html += '<tr>';
    //                html += '<td>' + item.ProgramName + '</td>';
    //                html += '<td>' + item.Part + '</td>';
    //                html += '<td>' + item.ControlItem + '</td>';
    //                html += '<td>' + item.ColumnName + '</td>';
    //                html += '<td>' + item.SpecDisplay + '</td>';
    //                html += '<td>' + item.Unit + '</td>';
    //                html += '<td>' + item.LowerLimit + '</td>';
    //                html += '<td>' + item.UpperLimit + '</td>';
    //                html += '<td>' + item.OperatorName + '</td>';
    //                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
    //                //html += '<td><a href="#" onclick="return GetbyID(\'' + item.ProgramName + ',' + item.Part + ',' + item.ControlItem + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ProgramName + ',' + item.Part + ',' + item.ControlItem + '\')">Delete</a></td>';
    //                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
    //                html += '</tr>';
    //            });
    //            $('.tbody').html(html);
    //        }
    //        else if (result.Code == "99")
    //            alert("Lỗi phần kiểm tra dữ liệu /ProgramPdtCtrl/Search / Error : " + result.Message);
    //        //Paging 
    //        if (_pageNumber == 1) {
    //            reloadPageTool(result.TotalPage);
    //        }
    //        console.log(result);
    //    },
    //    error: function (errormessage) {
    //        alert('Error' + errormessage.responseText);
    //    }
    //});
}

// **** Region of Paging.
//function loadDataBySelectedPage(pageNumber) {
//    $.ajax({
//        url: "/ProgramPdtCtrl/ListByPage",
//        data: { 'pageNumber': pageNumber },
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var html = '';
//            //loginUserID = result.UserID;
//            //loginUserName = result.UserName;
//            //$('#UserID').val(loginUserName);
//            $.each(result.lstProgramPdtCtrl, function (key, item) {
//                html += '<tr>';
//                html += '<td style=\"display:none\">' + item.ID + '</td>';
//                html += '<td>' + item.ProgramName + '</td>';
//                html += '<td>' + item.Part + '</td>';
//                html += '<td>' + item.ControlItem + '</td>';
//                html += '<td>' + item.ColumnName + '</td>';
//                html += '<td>' + item.SpecDisplay + '</td>';
//                html += '<td>' + item.Unit + '</td>';
//                html += '<td>' + item.LowerLimit + '</td>';
//                html += '<td>' + item.UpperLimit + '</td>';
//                html += '<td>' + item.OperatorName + '</td>';

//                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
//                //html += '<td><a href="#" onclick="return GetbyID(\'' + item.ProgramName + ',' + item.Part + ',' + item.ControlItem + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ProgramName + ',' + item.Part + ',' + item.ControlItem + '\')">Delete</a></td>';
//                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
//                html += '</tr>';
//            });
//            $('.tbody').html(html);
//            //Paging 
//            if (pageNumber == 1) {
//                reloadPageTool(result.TotalPage);
//            }
//            console.log(result);
//        },
//        error: function (errormessage) {
//            alert('Error' + errormessage.responseText);
//        }
//    });
//}
//paging.
function reloadPageTool(p_totalPage) {
    $('#page-selection').bootpag({
        total: p_totalPage,          // total pages
        page: 1,            // default page
        maxVisible: 10,     // visible pagination
        leaps: true         // next/prev leaps through maxVisible
    }).on("page", function (event, num) {
        ProgramPdtCtrl_SearchTools(num);
    });
}
function load_ProgramName() {
    $.ajax({
        url: "/ProgramName/ListAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                var html = '';
                $.each(result.lstProgramName, function (key, item) {
                    html += '<option value="' + item.programName + '">' + item.programName + '</option>';
                });
                $('.ProgramName').html(html);
            }
        },
        error: function (errormessage) {
            alert('Error function load_LineUsing(selectObject) :' + errormessage.responseText);
        }
    });
}
function checkValue(obj, error) {
    var i = 0;
    if (obj.length > 1) {
        for (y = 0; y < obj.length; y++) {
            for (z = 0; z < obj.length; z++) {
                if (y != z && obj[y].Part == obj[z].Part && obj[y].ControlItem == obj[z].ControlItem) {
                    error[error.length] = (obj[y]);
                }
            }
        }
    }

}
// insert datatable
function Add_ProgramName() {
    var row = document.getElementById('myTableID').rows.length;
    var myTab = document.getElementById('myTableID');
    var obj1 = new Array();
    for (i = 1; i < row; i++) {
        var a = myTab.rows[i].getElementsByTagName("input");
        var ProgramPdtCtrlObj = {
            ProgramName: $("#ProgramName").val(),
            Part: a[0].value,
            ControlItem: a[1].value,
            ColumnName: a[2].value,
            SpecDisplay: a[3].value,
            Unit: a[4].value,
            LowerLimit: a[5].value,
            UpperLimit: a[6].value,
            OperatorID: a[7].value,
        };
        obj1.push(ProgramPdtCtrlObj);
    }
    var listError = new Array();
    checkValue(obj1, listError);
    for (j = 0; j < listError.length; j++) {
        for (i = 0; i < obj1.length; i++) {
            if (listError[j].Part == obj1[i].Part && listError[j].ControlItem == obj1[i].ControlItem && listError[j].ColumnName == obj1[i].ColumnName
                && listError[j].SpecDisplay == obj1[i].SpecDisplay && listError[j].Unit == obj1[i].Unit && listError[j].LowerLimit == obj1[i].LowerLimit
                && listError[j].UpperLimit == obj1[i].UpperLimit && listError[j].OperatorID == obj1[i].OperatorID)
                obj1.splice(obj1[i], 1);
        }
    }
    for (i = 0; i < obj1.length; i++) {
        var b = {
            ProgramName: obj1[i].ProgramName,
            Part: obj1[i].Part,
            ControlItem: obj1[i].ControlItem,
            ColumnName: obj1[i].ColumnName,
            SpecDisplay: obj1[i].SpecDisplay,
            Unit: obj1[i].Unit,
            LowerLimit: obj1[i].LowerLimit,
            UpperLimit: obj1[i].UpperLimit,
            OperatorID: obj1[i].OperatorID,
        };

        $.ajax({
            url: "/ProgramPdtCtrl/GetbyKey/",
            data: {
                ProgramName: obj1[i].ProgramName,
                Part: obj1[i].Part,
                ControlItem: obj1[i].ControlItem
            },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result1) {
                if (result1.Code == "01") {
                    $.ajax({
                        url: "/ProgramPdtCtrl/Insert/",
                        data: JSON.stringify(b),
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            ProgramPdtCtrl_SearchTools(1);
                            //loadDataBySelectedPage(1);
                        },
                        error: function (errormessage) {
                            alert(errormessage.responseText);
                        }
                    });
                } else if (result1.Code == "00") {
                    for (i = 1; i < row; i++) {
                        var a = myTab.rows[i].getElementsByTagName("input");

                        if (a[0].value == result1.lstProgramPdtCtrl[0].Part && a[1].value == result1.lstProgramPdtCtrl[0].Part) {
                            myTab.rows[i].classList.add("bg-danger");
                        }
                    }
                    listError[listError.length] = (result1.lstProgramPdtCtrl[0]);


                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    var Lengthcc;
    setTimeout(function () {
        if (listError.length > 0) {
            for (i = 1; i < row; i++) {
                var a = myTab.rows[i].getElementsByTagName("input");

                for (j = 0; j < listError.length; j++) {
                    if (a[0].value == listError[j].Part && a[1].value == listError[j].ControlItem) {
                        myTab.rows[i].classList.add("bg-danger");
                        Lengthcc++;
                    }
                }
            }
            alert("Dữ liệu trùng lặp");
        } else {
            alert("Thêm dữ liệu thành công/Insert successful!");
            //loadDataBySelectedPage(1);
            ProgramPdtCtrl_SearchTools(1);
            setTimeout(function () {
                $('#myModal1').modal('hide');

            }, 500);
        }
    }, 1000);
    console.log(listError.length);
    if (Lengthcc == 0) {
        loadDataBySelectedPage(1);
        setTimeout(function () {
            $('#myModal1').modal('hide');
        }, 500);
    }
}
function AddRow() {
    var row = document.getElementById('myTableID').rows.length;

    var html = '<tr><th scope="row">' + row + '</th>' +
        '<td><input type="text" class="form-control Part" placeholder="Part"></td>' +
        '<td><input type="text" class="form-control ControlItem" placeholder="ControlItem"></td>' +
        '<td><input type="text" class="form-control ColumnName" placeholder="ColumnName"></td>' +
        '<td><input type="text" class="form-control SpecDisplay" placeholder="SpecDisplay"></td>' +
        '<td><input type="text" class="form-control Unit" placeholder="Unit"></td>' +
        '<td><input type="text" class="form-control LowerLimit" placeholder="LowerLimit"></td>' +
        '<td><input type="text" class="form-control UpperLimit" placeholder="UpperLimit"></td>' +
        '<td><input type="text" class="form-control OperatorID" placeholder="OperatorID"></td>' +
        '<td><button type="button" class="btn btn-danger" id="btnSave" onclick="DeleteRow(this);">Delete</button></td>' +
        '</tr>'
    $(".data-content").append(html);

}

function DeleteRow(e) {
    $(e).parent().parent().remove();
    var myTab = document.getElementById('myTableID');
    var row = document.getElementById('myTableID').rows.length;
    for (i = 1; i < row; i++) {
        var a = myTab.rows[i].getElementsByTagName("th");
        a[0].textContent = i;
    }
}

//--DataTables-----------------------------------------------------------------------
var myDataTableVM = {
    dt: null,
    init: function () {
        dt = $('#ProgramPdtCtrl_DataTable').DataTable({
            "searching": false,
            "scrollX": true,  // attention: assign style="width:100%;" to table tag.
            "serverSide": true,
            "processing": true,
            "deferLoading": 0, //prevent from DataTable automatically  call ajax in initialisation .
            "language": {
                "lengthMenu": "",
                "zeroRecords": "Không có dữ liệu./No records.",
                "info": "",
                "infoEmpty": "Không có bản ghi nào./No records.",
                "infoFiltered": "",
                "paginate": {
                    "next": ">>",
                    "previous": "<<"
                }
            },
            "ajax": {
                "url": '/ProgramPdtCtrl/Search',
                "type": "POST",
                //"contentType": "application/json;charset=UTF-8",
                //"dataType": "json",
                "data": function (data) {
                    data.ProgramName = $('#SearchProgramName').val();
                    data.Part = $('#SearchPart').val();
                    data.ControlItem = $('#SearchControlItem').val();
                    data.ColumnName = "",
                    data.SpecDisplay = "",
                    data.Unit = "",
                    data.LowerLimit = "",
                    data.UpperLimit = "",
                    data.OperatorID = "";
                    return data;  // attention: not return JSON.stringify(data);
                },
                "dataSrc": function (response) {
                    var return_dataSrc = new Array();
                    result = response.data; //chi lay kieu ReturnMachineMtn.
                    if (result.Code == "00") {
                        $.each(result.lstProgramPdtCtrl, function (key, item) {
                            var dr = new Object();
                            dr.ProgramName = item.ProgramName;
                            dr.Part = item.Part;
                            dr.ControlItem = item.ControlItem;
                            dr.ColumnName = item.ColumnName;
                            dr.SpecDisplay = item.SpecDisplay;
                            dr.Unit = item.Unit;
                            dr.LowerLimit = item.LowerLimit;
                            dr.UpperLimit = item.UpperLimit;
                            dr.OperatorName = item.OperatorName;
                            dr.CreatedDate = SM_ConvertMsJsonDate_ToString(item.CreatedDate);
                            //permission
                            var html = '';
                            var isAddSeperateChar = false;

                            //html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';

                            if (result.permisionControllerVM.isAllow_Edit) {
                                if (isAddSeperateChar)
                                    html += '|';
                                else
                                    isAddSeperateChar = true;
                                html += '<a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a>';
                            }
                            if (result.permisionControllerVM.isAllow_Delete) {
                                if (isAddSeperateChar)
                                    html += '|';
                                else
                                    isAddSeperateChar = true;
                                html += '<a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a>';
                            }
                            dr.Action = html;
                            return_dataSrc.push(dr);
                        });
                    }
                    else if (result.Code == "99")
                        alert("Lỗi tìm kiếm dữ liệu MachineMtn_Search / Error while searching : " + result.Message);
                    return return_dataSrc;
                }
            }, //end of ajax.
            "columns": [
                       { "data": "ProgramName" },
                       { "data": "Part" },
                       { "data": "ControlItem" },
                       { "data": "ColumnName" },
                       { "data": "SpecDisplay" },
                       { "data": "Unit" },
                       { "data": "LowerLimit" },
                       { "data": "UpperLimit" },
                       { "data": "OperatorName" },
                       { "data": "CreatedDate" },
                       { "data": "Action" }
            ],
            "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        });
        $('#ProgramPdtCtrl_DataTable').DataTable().columns.adjust();
    },
    refresh: function () {
        dt.ajax.reload();
        $('#ProgramPdtCtrl_DataTable').DataTable().columns.adjust();
    }
}
