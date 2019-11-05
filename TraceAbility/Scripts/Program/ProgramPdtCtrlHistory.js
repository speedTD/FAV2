//Load Data in Table when documents is ready                        
$(document).ready(function () {
    //loadDataBySelectedPage(1);
    //load_ProgramName();
    var loginUserID = 0;
    var loginUserName = "";
    PdtCtrlHistoryDataTableVM.init();
    ProgramPdtCtrlHistory_SearchTools(1);
});
function clearTextBox() {
    $(".data-content").html("");
    $('#ProgramName').val("");
    $('#Part').val("");
    $('#ControlItem').val("");
    $('#ColumnName').val("");
    $('#SpecDisplay').val("");
    $('#Unit').val("");
    $('#LowerLimit').val("");
    $('#UpperLimit').val("");
    $('#OperatorID').val("");
}


function ClearSearchItem() {
    $('#SearchProgramName').val("");
    $('#SearchPart').val("");
    $('#SearchControlItem').val("");
    $('#SearchMaker').val("");
}
//function GetbyID(id) {
//    var ProgramName = id.split(",")[0];
//    var Parameter = id.split(",")[1];
//    var ControlItem = id.split(",")[2];
//    $("#OperatorID").placeholder = "HistoryOperaterID";
//    $(".OperatorID").text = "HistoryOperaterID";
//    $('#ProgramName').css('border-color', 'lightgrey');
//    $('#Parameter').css('border-color', 'lightgrey');
//    $('#ControlItem').css('border-color', 'lightgrey');
//    $('#ControlItem').css('border-color', 'lightgrey');
//    $('#ColumnName').css('border-color', 'lightgrey');
//    $('#SpecDisplay').css('border-color', 'lightgrey');
//    $('#Unit').css('border-color', 'lightgrey');
//    $('#LowerLimit').css('border-color', 'lightgrey');
//    $('#UpperLimit').css('border-color', 'lightgrey');
//    $('#OperatorID').css('border-color', 'lightgrey');
//    var obj = {
//        'ProgramName': ProgramName,
//        'Parameter': Parameter,
//        'ControlItem': ControlItem
//    };
//    $.ajax({
//        url: "/ProgramPdtCtrlHistory/GetbyKey",
//        data: obj,
//        type: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            if (result.Code == "00") {
//                $.each(result.lstProgramPdtCtrlHistory, function (key, item) {
//                    console.log(item.ProgramName)
//                    $('#ProgramName').val(item.ProgramName);
//                    $('#Parameter').val(item.Parameter);
//                    $('#ControlItem').val(item.ControlItem);

//                    $('#ProgramName').prop('disabled', true);
//                    $('#Parameter').prop('disabled', true);
//                    $('#ControlItem').prop('disabled', true);

//                    $('#ColumnName').val(item.ColumnName);
//                    $('#Unit').val(item.Unit);
//                    $('#LowerLimit').val(item.LowerLimit);
//                    $('#UpperLimit').val(item.UpperLimit);
//                    $('#SpecDisplay').val(item.SpecDisplay);
                    
//                    $('#OperatorID').val(item.OperatorID);
//                    $('#ItemName').val(item.ItemName);
//                    $('#myModal').modal('show');
//                    $('#btnUpdate').show();
//                    $('#btnSave').hide();
//                    $('#myModalLabel').text('Update ProgramPdt Control ');
//                    $('#OperatorID').placeholder = "HistoryOperatorID";
//                    $('.OperatorID').text("HistoryOperatorID");
//                });
//            }

//        },
//        error: function (errormessage) {
//            alert('Error    ' + errormessage.responseText);
//        }
//    });

//    return false;
//}

//function ProgramPdtCtrl_Update() {
//    var res = validate();
//    if (res == false) {
//        return false;
//    }
//    var ProgramPdtCtrlHistoryObj = {
//        ProgramName: $('#ProgramName').val(),
//        Parameter: $('#Parameter').val(),
//        ControlItem: $('#ControlItem').val(),
//        ColumnName: $('#ColumnName').val(),
//        SpecDisplay: $('#SpecDisplay').val(),
//        Unit: $('#Unit').val(),
//        LowerLimit: $('#LowerLimit').val(),
//        UpperLimit: $('#UpperLimit').val(),
//        HistoryOperatorID: $('#OperatorID').val(),
//        ItemName: $('#ItemName').val(),

//    };
//    $.ajax({
//        url: "/ProgramPdtCtrlHistory/Insert/",
//        data: JSON.stringify(ProgramPdtCtrlHistoryObj),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
//            }
//            else {
//                loadDataBySelectedPage(1);
//                setTimeout(function () {
//                    alert("Thêm thành công");
//                }, 250)
//                $('#myModal').modal('hide');
//            }
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}
//function validate() {
//    var isValid = true;

//    if ($('#ProgramName').val().trim() == "") {
//        $('#ProgramName').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#ProgramName').css('border-color', 'lightgrey');
//    }
//    if ($('#Parameter').val().trim() == "") {
//        $('#Parameter').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Parameter').css('border-color', 'lightgrey');
//    }
//    if ($('#ControlItem').val().trim() == "") {
//        $('#ControlItem').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#ControlItem').css('border-color', 'lightgrey');
//    }
//    if ($('#ColumnName').val().trim() == "") {
//        $('#ColumnName').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#ColumnName').css('border-color', 'lightgrey');
//    }
//    if ($('#SpecDisplay').val().trim() == "") {
//        $('#SpecDisplay').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#SpecDisplay').css('border-color', 'lightgrey');
//    }

//    if ($('#Unit').val().trim() == "") {
//        $('#Unit').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Unit').css('border-color', 'lightgrey');
//    }
//    if ($('#LowerLimit').val().trim() == "") {
//        $('#LowerLimit').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#LowerLimit').css('border-color', 'lightgrey');
//    }
//    if ($('#UpperLimit').val().trim() == "") {
//        $('#UpperLimit').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#UpperLimit').css('border-color', 'lightgrey');
//    }
//    if ($('#OperatorID').val().trim() == "") {
//        $('#OperatorID').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#OperatorID').css('border-color', 'lightgrey');
//    }
//    return isValid;
//}

// **** Region CRUD ***************************************************** 
//function ProgramPdtCtrl_Insert() {
//    var res = validate();
//    if (res == false) {
//        return false;
//    }
//    $.ajax({
//        url: "/ProgramPdtCtrlHistory/CountbyID/",
//        data: {
//            ProgramName: $('#ProgramName').val(),
//            Parameter: $('#Parameter').val(),
//            ControlItem: $('#ControlItem').val()
//        },
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result1) {
//            if (result1 == 0) {
//                var ProgramPdtCtrlObj = {
//                    ProgramName: $('#ProgramName').val(),
//                    Parameter: $('#Parameter').val(),
//                    ControlItem: $('#ControlItem').val(),
//                    ColumnName: $('#ColumnName').val(),
//                    SpecDisplay: $('#SpecDisplay').val(),
//                    Unit: $('#Unit').val(),
//                    LowerLimit: $('#LowerLimit').val(),
//                    UpperLimit: $('#UpperLimit').val(),
//                    OperatorID: $('#OperatorID').val(),
//                    HistoryOperatorID: $('#OperatorID').val(),
//                    ItemName: $('#ItemName').val(),
//                };
//                $.ajax({
//                    url: "/ProgramPdtCtrlHistory/Insert/",
//                    data: JSON.stringify(ProgramPdtCtrlObj),
//                    type: "POST",
//                    contentType: "application/json;charset=utf-8",
//                    dataType: "json",
//                    success: function (result) {
//                        loadDataBySelectedPage(1);
//                        setTimeout(function () {
//                            alert("Thêm thành công");
//                        },250)
//                        $('#myModal').modal('hide');
//                    },
//                    error: function (errormessage) {
//                        alert(errormessage.responseText);
//                    }
//                });
//            }
//            else {
//                alert("ProgramPdtCtrlHistory already exists");
//                $('#LineID').css('border-color', 'Red');
//            }
//        },
//    });
//}

function DeleleByID(id) {
    var ProgramName = id.split(",")[0];
    var Parameter = id.split(",")[1];
    var ControlItem = id.split(",")[2];
    var obj = {
        'ProgramName': ProgramName,
        'Parameter': Parameter,
        'ControlItem': ControlItem
    }
    var ans = confirm("Are you sure you want to delete ? programName= " + ProgramName + " Part=" + Part + " ControlItem=" + ControlItem);
    if (ans) {
        $.ajax({
            url: "/ProgramPdtCtrlHistory/Delete",
            data: JSON.stringify(obj),
            type: "POST",
            contentType: false,
            processData: false,
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                alert("Deleted successful!");
                PdtCtrlHistoryDataTableVM.refresh();
                //loadDataBySelectedPage(1);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
// **** End of region CRUD *****************************************************

function ProgramPdtCtrlHistory_SearchTools(_pageNumber) {
    PdtCtrlHistoryDataTableVM.refresh();
    //var ProgramPdtCtrlHistoryObj = {
    //    ProgramName: $('#SearchProgramName').val(),
    //    Parameter: $('#SearchPart').val(),
    //    ControlItem: $('#SearchControlItem').val(),
    //    ColumnName: "",
    //    SpecDisplay: "",
    //    Unit: "",
    //    LowerLimit: "",
    //    UpperLimit: "",
    //    OperatorID: "",

    //};

    //var returnObj = {
    //    aProgramPdtCtrlHistory: ProgramPdtCtrlHistoryObj,
    //    PageNumber: _pageNumber
    //}

    //$.ajax({
    //    url: "/ProgramPdtCtrlHistory/Search",
    //    data: JSON.stringify(returnObj),
    //    type: "POST",
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //    success: function (result) {
    //        var html = '';
    //        loginUserID = result.UserID;
    //        loginUserName = result.UserName;
    //        $('#UserID').val(loginUserName);
    //        $.each(result.lstProgramPdtCtrl, function (key, item) {
    //            html += '<tr>';
    //            html += '<td>' + item.ProgramName + '</td>';
    //            html += '<td>' + item.Parameter + '</td>';
    //            html += '<td>' + item.ControlItem + '</td>';
    //            html += '<td>' + item.ColumnName + '</td>';
    //            html += '<td>' + item.SpecDisplay + '</td>';
    //            html += '<td>' + item.Unit + '</td>';
    //            html += '<td>' + item.LowerLimit + '</td>';
    //            html += '<td>' + item.UpperLimit + '</td>';
    //            html += '<td>' + item.OperatorID + '</td>';
    //            html += '<td><a href="#" onclick="return GetbyID(\'' + item.ProgramName + ',' + item.Parameter + ',' + item.ControlItem + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ProgramName + ',' + item.Parameter + ',' + item.ControlItem + '\')">Delete</a></td>';

    //            html += '</tr>';
    //        });
    //        $('.tbody').html(html);
    //        //Paging 
    //        if (result.pageNumber == 1) {
    //            reloadPageTool(result.TotalPage);
    //        }
    //    },
    //    error: function (errormessage) {
    //        alert('Error' + errormessage.responseText);
    //    }
    //});
}

// **** Region of Paging.
//function loadDataBySelectedPage(pageNumber) {
    
//    $.ajax({
//        url: "/ProgramPdtCtrlHistory/ListByPage",
//        data: { 'pageNumber': pageNumber },
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var html = '';
//            loginUserID = result.UserID;
//            loginUserName = result.UserName;
//            $('#UserID').val(loginUserName);
//            $.each(result.lstProgramPdtCtrlHistory, function (key, item) {
//                html += '<tr>';
//                html += '<td>' + item.ProgramName + '</td>';
//                html += '<td>' + item.Part + '</td>';
//                html += '<td>' + item.ControlItem + '</td>';
//                html += '<td>' + item.ColumnName + '</td>';
//                html += '<td>' + item.SpecDisplay + '</td>';
//                html += '<td>' + item.Unit + '</td>';
//                html += '<td>' + item.LowerLimit + '</td>';
//                html += '<td>' + item.UpperLimit + '</td>';
//                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
//                html += '<td>' + item.OperatorName + '</td>';
//                html += '<td>' + item.ItemName + '</td>';
//                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.HistoryDate) + '</td>';
//                html += '<td>' + item.HistoryOperatorName + '</td>';
//                html += '<td>' + item.StatusCRUD + '</td>';
//                //html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
//                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ProgramName + ',' + item.Parameter + ',' + item.ControlItem + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ProgramName + ',' + item.Part + ',' + item.ControlItem + '\')">Delete</a></td>';
//                html += '</tr>';
//            });
//            $('.tbody').html(html);
//            //Paging 
//            if (pageNumber == 1) {
//                reloadPageTool(result.TotalPage);
//            }
//        },
//        error: function (errormessage) {
//            alert('Error' + errormessage.responseText);
//        }
//    });
//}

//paging.
//function reloadPageTool(p_totalPage) {
//    $('#page-selection').bootpag({
//        total: p_totalPage,          // total pages
//        page: 1,            // default page
//        maxVisible: 10,     // visible pagination
//        leaps: true         // next/prev leaps through maxVisible
//    }).on("page", function (event, num) {
//        loadDataBySelectedPage(num);
//    });
//}

function load_ProgramName() {
    $.ajax({
        url: "/ProgramName/ListAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                console.log(result);
                var html = '';
                $.each(result.lstProgramName, function (key, item) {
                    html += '<option value="' + item.programName + '">' + item.programName + '</option>';
                });
                $('.ProgramName').html(html);
            }
        },
        error: function (errormessage) {
            alert('Error function load_ProgramName() :' + errormessage.responseText);
        }
    });
}

//--DataTables-----------------------------------------------------------------------
var PdtCtrlHistoryDataTableVM = {
    dt: null,
    init: function () {
        dt = $('#PdtCtrlHistory_DataTable').DataTable({
            "searching": false,
            "scrollX": true,  // attention: assign style="width:100%;" to table tag.
            "serverSide": true,
            "processing": true,
            "deferLoading": 0, //prevent from DataTable automatically  call ajax in initialisation .
            "language": {
                "lengthMenu": "",
                "zeroRecords": "Không có dữ liệu./No records.",
                "info": "",
                "infoEmpty": "",
                "infoFiltered": "",
                "paginate": {
                    "next": ">>",
                    "previous": "<<"
                }
            },
            "ajax": {
                "url": '/ProgramPdtCtrlHistory/Search/',
                "type": "POST",
                "data": function (data) {
                    data.ProgramName = $('#SearchProgramName').val();
                    data.Part = $('#SearchPart').val();
                    data.ControlItem = $('#SearchControlItem').val();
                    return data;  // attention: not return JSON.stringify(data);
                },
                "dataSrc": function (response) {
                    var return_dataSrc = new Array();
                    result = response.data; //chi lay kieu ReturnMachineMtn.
                    if (result.Code == "00") {
                        $.each(result.lstProgramPdtCtrlHistory, function (key, item) {
                            var dr = new Object();
                            dr.ProgramName = item.ProgramName;
                            dr.Part = item.Part;
                            dr.ControlItem = item.ControlItem;
                            dr.ColumnName = item.ColumnName;
                            dr.SpecDisplay = item.SpecDisplay;
                            dr.Unit = item.Unit;
                            dr.LowerLimit = item.LowerLimit;
                            dr.UpperLimit = item.UpperLimit;
                            dr.CreatedDate = SM_ConvertMsJsonDate_ToString(item.CreatedDate);
                            dr.OperatorName = item.OperatorName;
                            dr.ItemName = item.ItemName;
                            
                            dr.HistoryDate = SM_ConvertMsJsonDate_ToString(item.HistoryDate);
                            dr.HistoryOperatorName = item.HistoryOperatorName;
                            dr.StatusCRUD = item.StatusCRUD;
                            
                            //permission
                            var html = '';
                            var isAddSeperateChar = false;
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
                        alert("Lỗi tìm kiếm dữ liệu ProgramPdtCtrlHistory / Error while searching : " + result.Message);
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
                       { "data": "CreatedDate" },
                       { "data": "OperatorName" },
                       { "data": "ItemName" },
                       { "data": "HistoryDate" },
                       { "data": "HistoryOperatorName" },
                       { "data": "StatusCRUD" },
                       { "data": "Action" }
            ],
            "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        });
        $('#PdtCtrlHistory_DataTable').DataTable().columns.adjust();
    },
    refresh: function () {
        dt.ajax.reload();
        $('#PdtCtrlHistory_DataTable').DataTable().columns.adjust();
    }
}