$(document).ready(function () {
    //Search();
    $('#PdtCtrlDateTime').datepicker();
    $('#FromDate').datepicker();
    $('#FromDate').datepicker("setDate", new Date());
    $('#ToDate').datepicker();
    $('#ToDate').datepicker("setDate", new Date());
    
    myDataTableVM.init();
});
var ProductionControlIDLast = "";
var productionControlID = 0;
var loginUserID = 0;
var loginUserName = "";

function LoadData() {
    $.ajax({
        url: "/ProductionCondition/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log('/ProductionCondition/List');
            console.log(result);
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td style=\"display:none\">' + item.ID + '</td>';
                html += '<td>' + item.IndicationID + '</td>';
                html += '<td>' + item.MachineID + '</td>';
                html += '<td>' + SM_ConvertMsJson_DateTimePicker_ToString(item.PdtCtrlDateTime) + '</td>';
                html += '<td>' + item.UserID + '</td>';
                html += '<td>' + item.ItemName + '</td>';
                html += '<td>' + item.ItemCode + '</td>';
                html += '<td>' + item.BatchNo + '</td>';
                html += '<td>' + item.SeqNo + '</td>';
                html += '<td>' + item.Result + '</td>';
                html += '<td>' + item.ProgramName + '</td>';
                html += '<td><a href=\'/ProductionCondition/Edit?id=' + item.ID + '\' >Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}
function PdtCtrlChecksheet_Search() {
    myDataTableVM.refresh();

    //var MachineIDSearch = $('#MachineIDSearch').val();
    //var IndicationIDSearch = $('#IndicationIDSearch').val();
    //var ResultSearch = $('#ResultSearch').val();
    //var FromDate = $('#FromDate').val();
    //var ToDate = $('#ToDate').val();
    //var where = "1=1";
    //if (MachineIDSearch.length > 0) {
    //    where += " AND tProductionControl.MachineID = '" + MachineIDSearch + "' ";
    //}
    //if (IndicationIDSearch.length > 0) {
    //    where += " AND tProductionControl.IndicationID = '" + IndicationIDSearch + "' ";
    //}
    //if (ResultSearch.length > 0) {
    //    where += " AND tProductionControl.Result = '" + ResultSearch + "' ";
    //}
    //if (FromDate.length > 0) {
    //    where += " AND tProductionControl.PdtCtrlDateTime >= '" + FromDate + "' ";
    //}
    //if (ToDate.length > 0) {
    //    where += " AND tProductionControl.PdtCtrlDateTime <= '" + ToDate + "' ";
    //}
    //$.ajax({
    //    url: "/ProductionCondition/SearchProductionControl",
    //    type: "GET",
    //    data: { 'where': where },
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //    success: function (result1) {
    //        var html = '';
    //        loginUserID = result1.userID;
    //        loginUserName = result1.UserName;
    //        $.each(result1.lstProductionControl, function (key, item) {
    //            html += '<tr>';
    //            html += '<td style=\"display:none\">' + item.ID + '</td>';
    //            html += '<td>' + item.IndicationID + '</td>';
    //            html += '<td>' + item.MachineID + '</td>';
    //            html += '<td>' + SM_ConvertMsJson_DateTimePicker_ToString(item.PdtCtrlDateTime) + '</td>';
    //            html += '<td>' + item.UserID + '</td>';
    //            html += '<td>' + item.ItemName + '</td>';
    //            html += '<td>' + item.ItemCode + '</td>';
    //            html += '<td>' + item.BatchNo + '</td>';
    //            html += '<td>' + item.SeqNo + '</td>';
    //            html += '<td>' + item.Result + '</td>';
    //            html += '<td>' + item.ProgramName + '</td>';
    //            html += '<td><a href=\'/ProductionCondition/Edit?id=' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
    //            html += '</tr>';
    //        });
    //        $('.tbody').html(html);
    //    },
    //    error: function (errormessage) {
    //        alert('Error' + errormessage.responseText);
    //    }
    //});
}
function ClearSearchItem() {
    $('#MachineIDSearch').val("");
    $('#IndicationIDSearch').val("");
    //$('#ResultSearch').val("");
    $('#FromDate').val("");
    $('#ToDate').val("");
    $("input[name='rdo_ResultSearch']").val([""]);  // phai de [""]
}
$(document).ready(function () {
    $('#MachineID').keyup(function () {
        var machineID = $('#MachineID').val();
        var batchNo = $('#BatchNo').val();
        $.ajax({
            url: "/Machine/GetbyID/",
            data: { 'machineID': machineID },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#MachineName').val(result.MachineName);
            },
        });
        loadProductionConditionListByMachineTypeID(machineID, batchNo);
    });
});
$(document).ready(function () {
    $('#BatchNo').keyup(function () {
        var machineID = $('#MachineID').val();
        var batchNo = $('#BatchNo').val();
        loadProductionConditionListByMachineTypeID(machineID, batchNo);
    });
});
$(document).ready(function () {
    $('#ItemCode').keyup(function () {
        var itemCode = $('#ItemCode').val();
        $.ajax({
            url: "/ProductionList/GetbyItemCode/",
            data: { 'ItemCode': itemCode },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#ItemName').val(result.ItemName);
            },
        });
    });
});
function loadProductionConditionListByMachineTypeID(MachineID, BatchNo) {
    $.ajax({
        url: "/ProductionCondition/GetbyMachineTypeID",
        type: "GET",
        data: { 'MachineID': MachineID, 'BatchNo': BatchNo },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.lstConditionSetting, function (key, item) {
                html += '<tr class=\"package-row\">';
                html += '<td style=\"display:none\">' + item.ID + '</td>';
                html += '<td>' + item.ControlItem + '</td>';
                html += '<td>' + item.SpecDisplay + '</td>';
                html += '<td>' + item.Unit + '</td>';
                html += '<td id=\"LowerLimit\">' + item.LowerLimit + '</td>';
                html += '<td id=\"UpperLimit\">' + item.UpperLimit + '</td>';
                html += '<td><input id=\"ActualValue\" type=\"text\" onkeyup=\"return calculateActualResult_PdtCdt(this)\" class=\"form-control\" /></td>';
                html += '<td id=\"Result\"></td>';
                html += '<td style=\"display:none\">0</td>';
                html += '</tr>';

            });
            //alert(html);
            $('.tProductionControl').html(html);
        },
        error: function (errormessage) {
           // alert('Error ' + errormessage.responseText);
        }
    });
}
function GetbyID(ID) {
    productionControlID = ID;
    $('#IndicatorID').css('border-color', 'lightgrey');
    $('#PdtCtrlDateTime').css('border-color', 'lightgrey');
    $('#ItemName').css('border-color', 'lightgrey');
    $('#ItemCode').css('border-color', 'lightgrey');
    $('#BatchNo').css('border-color', 'lightgrey');
    $('#MachineID').css('border-color', 'lightgrey');
    $('#MachineName').css('border-color', 'lightgrey');
    $('#ProgramName').css('border-color', 'lightgrey');
    $('#ControlItem').css('border-color', 'lightgrey');
    $('#SeqNo').css('border-color', 'lightgrey');

    $.ajax({
        url: "/ProductionCondition/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#IndicationID').val(result.IndicationID);
            $('#IndicationID').prop('disabled', true);
            $('#PdtCtrlDateTime').val(SM_ConvertMsJsonDate_ToString(result.PdtCtrlDateTime));
            $('#PdtCtrlDateTime').prop('disabled', true);
            $('#MachineID').val(result.MachineID);
            $('#MachineID').prop('disabled', true);
            $('#ItemName').val(result.ItemName);
            $('#ItemCode').val(result.ItemCode);
            $('#BatchNo').val(result.BatchNo);
            $('#ProgramName').val(result.ProgramName);
            $('#UserID').val(result.UserID);
            $('#MachineName').val(result.MachineName);
            $('#SeqNo').val(result.SeqNo);

            $.ajax({
                url: "/ProductionCondition/GetbyProductionControlID",
                type: "GET",
                data: { 'ProductionControlID': productionControlID},
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result1) {
                    var html = '';
                    $.each(result1.lstProductionControlDetail, function (key, item) {
                        html += '<tr class=\"package-row\">';
                        html += '<td style=\"display:none\">' + item.ProductionControlListID + '</td>';
                        html += '<td>' + item.ControlItem + '</td>';
                        html += '<td>' + item.SpecDisplay + '</td>';
                        html += '<td>' + item.Unit + '</td>';
                        html += '<td id=\"LowerLimit\">' + item.LowerLimit + '</td>';
                        html += '<td id=\"UpperLimit\">' + item.UpperLimit + '</td>';
                        html += '<td><input id=\"ActualValue\" type=\"text\" value= "' + item.ActualValue + '"onkeyup=\"return calculateActualResult_PdtCdt(this)\" class=\"form-control\"></td>';
                        html += '<td id=\"Result\">' + item.Result + '</td>';
                        html += '<td style=\"display:none\">' + item.ID + '</td>';
                        html += '</tr>';
                        $('#ControlItem').val(item.ControlItem);
                    });
                    $('.tProductionControl').html(html);
                },
            });
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#myModalLabel').text('Update Production Control List ');
        },
        error: function (errormessage) {
            alert('Lỗi/Error    ' + errormessage.responseText);
        }
    });
    return false;
}
function clearTextBox() {
    $('#IndicationID').val("");
    $('#PdtCtrlDateTime').val("");
    $('#ItemName').val("");
    $('#ItemCode').val("");
    $('#BatchNo').val("");
    $('#MachineID').val("");
    $('#MachineName').val("");
    $('#ProgramName').val("");
    $('#ControlItem').val("");
    $('#UserID').val(loginUserName);
    $('#SeqNo').val("");
    loadProductionConditionListByMachineTypeID("0", "0");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Add New ProductionControl ");

    $('#Name').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#MachineType').css('border-color', 'lightgrey');
}

function calculateActualResult_PdtCdt()
{
        var rows = document.querySelectorAll("tr.package-row");
        rows.forEach(function (currentRow) {
            var LowerLimit = Number(currentRow.querySelector('#LowerLimit').innerHTML);
            var UpperLimit = Number(currentRow.querySelector('#UpperLimit').innerHTML);
            var ActualValue = Number(currentRow.querySelector('#ActualValue').value);
            var ResultCell = currentRow.querySelector('#Result');
           
            if (isNaN(ActualValue) ||ActualValue == "")
            {
                currentRow.querySelector('#ActualValue').style["border-color"] = "Red";
            }
            else
            {
                currentRow.querySelector('#ActualValue').style["border-color"] = "lightgrey";
                if (ActualValue>=LowerLimit & ActualValue <=UpperLimit)
                {
                    ResultCell.innerHTML = "OK";
                }
                else
                {
                    ResultCell.innerHTML = "NG";
                }
            }
        });
}
function validate() {
    var isValid = true;
    if ($('#IndicationID').val().trim() == "") {
        $('#IndicationID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#IndicationID').css('border-color', 'lightgrey');
    }
    if ($('#PdtCtrlDateTime').val().trim() == "") {
        $('#PdtCtrlDateTime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PdtCtrlDateTime').css('border-color', 'lightgrey');
    }
    if ($('#ItemName').val().trim() == "") {
        $('#ItemName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ItemName').css('border-color', 'lightgrey');
    }
    if ($('#ItemCode').val().trim() == "") {
        $('#ItemCode').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ItemCode').css('border-color', 'lightgrey');
    }
    if ($('#BatchNo').val().trim() == "") {
        $('#BatchNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#BatchNo').css('border-color', 'lightgrey');
    }
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
    if ($('#ProgramName').val().trim() == "") {
        $('#ProgramName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ProgramName').css('border-color', 'lightgrey');
    }
    if ($('#UserID').val().trim() == "") {
        $('#UserID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UserID').css('border-color', 'lightgrey');
    }
    if ($('#SeqNo').val().trim() == "") {
        $('#SeqNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SeqNo').css('border-color', 'lightgrey');
    }
    var t = document.getElementById('tblProductionControlDetail');
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var actualValue = inputs[0].value;
        if (actualValue == "") {
            t.rows[j].querySelector('#ActualValue').style["border-color"] = "Red";
            isValid = false;
        }
    }
    return isValid;
}

function InsertProductionControl() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var result = "OK";
    var t = document.getElementById('tblProductionControlDetail');
    for (var j = 1; j < t.rows.length; j++) {
        var resultDetail = t.rows[j].cells[7].firstChild.data;
        if (resultDetail == "NG") {
            result = "NG";
            break;
        }
    }
    var ProductionControl = {
        ID: 0,
        IndicationID: $('#IndicationID').val(),
        MachineID: $('#MachineID').val(),
        PdtCtrlDateTime: $('#PdtCtrlDateTime').val(),
        UserID: loginUserID,
        ItemName: $('#ItemName').val(),
        ItemCode: $('#ItemCode').val(),
        BatchNo: $('#BatchNo').val(),
        SeqNo: $('#SeqNo').val(),
        Result: result,
        ProgramName: $('#ProgramName').val(),
    };
    $.ajax({
        url: "/ProductionCondition/Insert/",
        data: JSON.stringify(ProductionControl),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            productionControlID = result.LastID;
            if (productionControlID == -1) {
                alert("Đã tồn tại, không thể thêm mới!/Already Exists. Cannot insert!");
            }
            else {
                InsertProductionControlDetail();
                alert("Thêm mới thành công!/Insert successfully!");
                LoadData();
            }
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function UpdateProductionControl() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var result = "OK";
    var t = document.getElementById('tblProductionControlDetail');
    for (var j = 1; j < t.rows.length; j++) {
        var resultDetail = t.rows[j].cells[7].firstChild.data;
        if (resultDetail == "NG") {
            result = "NG";
            break;
        }
    }
    var ProductionControl = {
        ID: productionControlID,
        IndicationID: $('#IndicationID').val(),
        MachineID: $('#MachineID').val(),
        PdtCtrlDateTime: $('#PdtCtrlDateTime').val(),
        UserID: $('#UserID').val(),
        ItemName: $('#ItemName').val(),
        ItemCode: $('#ItemCode').val(),
        BatchNo: $('#BatchNo').val(),
        SeqNo: $('#SeqNo').val(),
        Result: result,
        ProgramName: $('#ProgramName').val(),
    };
    $.ajax({
        url: "/ProductionCondition/Insert/",
        data: JSON.stringify(ProductionControl),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            InsertProductionControlDetail();
            alert("Update success!");
            productionControlID = 0;
            LoadData();

            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function InsertProductionControlDetail() {
    var t = document.getElementById('tblProductionControlDetail');
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var productionControlListID = t.rows[j].cells[0].firstChild.data;
        var controlItem = t.rows[j].cells[1].firstChild.data;
        var lowerLimit = t.rows[j].cells[4].firstChild.data;
        var upperLimit = t.rows[j].cells[5].firstChild.data;
        var actualValue = inputs[0].value;
        var result = t.rows[j].cells[7].firstChild.data;
        var productionControlDetailID = t.rows[j].cells[8].firstChild.data;
        var ProductionControlDetailObj = {
            ID: productionControlDetailID,
            ProductionControlID: productionControlID,
            ProgramName: $('#ProgramName').val(),
            ProductionControlListID: productionControlListID,
            ControlItem: controlItem,
            LowerLimit: lowerLimit,
            UpperLimit: upperLimit,
            ActualValue: actualValue,
            Result: result,
            ResultContent: "OK"
        }
        $.ajax({
            url: "/ProductionCondition/InsertProductionControlDetail/",
            data: JSON.stringify(ProductionControlDetailObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('#myModal').modal('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function DeleleByID(ID) {
    var ans = confirm("Bạn có chắc chắn muốn xóa?/Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/ProductionCondition/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                LoadData();
                alert("Xóa thành công!/Deleted successful!");
            },
            error: function (errormessage) {
                alert('Lỗi/Error ' + errormessage.responseText);
            }
        });
    }
}
//--DataTables-----------------------------------------------------------------------
var myDataTableVM = {
    dt: null,
    init: function () {
        dt = $('#PdtCtrlCheckSheet_DataTable').DataTable({
            "searching": false,
            "scrollX": true,  // attention: assign style="width:100%;" to table tag.
            "serverSide": true,
            "processing": true,
            "deferLoading": 0, //prevent from DataTable automatically  call ajax in initialisation .
            "language": {
                "lengthMenu": "",
                "zeroRecords": "Không có dữ liệu./No records.",
                "info": "",
                //"infoEmpty": "Không có bản ghi nào./No records.",
                "infoEmpty": "",
                "infoFiltered": "",
                "paginate": {
                    "next": ">>",
                    "previous": "<<"
                }
            },
            "ajax": {
                "url": '/ProductionCondition/SearchProductionControl_UsingDataTables',
                "type": "POST",
                "data": function (data) {
                    data.MachineID = $('#MachineIDSearch').val();
                    data.IndicationID = $('#IndicationIDSearch').val();
                    //data.ProgramNameID = $('#ProgramNameSearch').val();
                    data.FromDate = $('#FromDate').val();
                    data.ToDate = $('#ToDate').val();
                    data.Result = $("input[name='rdo_ResultSearch']:checked").val();
                    return data;  // attention: not return JSON.stringify(data);
                },
                "dataSrc": function (response) {
                    var return_dataSrc = new Array();
                    result = response.data; //chi lay kieu Return.
                    if (result.Code == "00") {
                        $.each(result.lstProductionControl, function (key, item) {
                            var dr = new Object();
                            dr.IndicationID = item.IndicationID;
                            dr.MachineID = item.MachineID;

                            dr.PdtCtrlDateTime = SM_ConvertMsJson_DateTimePicker_ToString(item.PdtCtrlDateTime);

                            dr.UserID = item.UserID;
                            dr.ItemName = item.ItemName;
                            dr.ItemCode = item.ItemCode;

                            dr.ItemCode = item.ItemCode;
                            dr.BatchNo = item.BatchNo;
                            dr.SeqNo = item.SeqNo;
                            dr.Result = item.Result;
                            dr.UserName = item.UserName;
                            dr.ProgramName = item.ProgramName;

                            //permission
                            var html = '';
                            var isAddSeperateChar = false;
                            if (result.permisionControllerVM.isAllow_View) {
                                html += '<a href=\'/ProductionCondition/Edit?id=' + item.ID + '\'>View</a>';
                                isAddSeperateChar = true;
                            }
                            if (result.permisionControllerVM.isAllow_Edit) {
                                if (isAddSeperateChar)
                                    html += '|';
                                else
                                    isAddSeperateChar = true;
                                html += '<a href=\'/ProductionCondition/Edit?id=' + item.ID + '\'>Edit</a>';
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
                        alert("Lỗi tìm kiếm dữ liệu  / Error while searching : " + result.Message);
                    return return_dataSrc;
                }
            }, //end of ajax.
            "columns": [
                       
                       { "data": "MachineID" },
                       { "data": "PdtCtrlDateTime" },
                       { "data": "IndicationID" },
                       { "data": "UserName" },
                       { "data": "ItemName" },
                       { "data": "ItemCode" },
                       { "data": "BatchNo" },
                       { "data": "SeqNo" },
                       { "data": "Result" },
                       { "data": "ProgramName" },
                       { "data": "Action" }
            ],
            "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        });
        $('#PdtCtrlCheckSheet_DataTable').DataTable().columns.adjust();
    },
    refresh: function () {
        dt.ajax.reload();
        $('#PdtCtrlCheckSheet_DataTable').DataTable().columns.adjust();
    }
}
