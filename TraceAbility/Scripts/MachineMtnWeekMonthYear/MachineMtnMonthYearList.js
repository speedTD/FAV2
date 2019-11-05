//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#MaintenanceDate').datepicker();
    Clear_SearchMachineMtnList();
    //$('#MaintenanceDate').datepicker();
    $('#MaintenanceDate').datepicker("setDate", new Date());
    $('#OperatorID').val($('#UserLogin').data('username'));
    MachineMtnDataTableVM.init();
});
// **** Event ****
//Lay toan bo thong tin cua Machineid
//--------- event ----------
$('#MachineID').keyup(function (event) {
    if ((event.keyCode ? event.keyCode : event.which) == 13) {
        Load_MachineInfo();
    }
});
function SearchMachineMtnList() {
    MachineMtnDataTableVM.refresh();
}
function Clear_SearchMachineMtnList() {
    $("input[name='rdo_Result']").val([""]);  // phai de [""]
    $("#Frequency").val("");
    $("#Shift").val("");
    $("#LineID").val("");
    $("#MachineID").val("");
    $("#MachineName").val("");
    //$("#MaintenanceDate").val("");
    $('#MaintenanceDate').datepicker('setDate', null);
    $("#OperatorName").val("");
}
function clearTextBox() {
    $('#Operator').val("");
    $("#OperatorName").val("");
    $("#LineID").val("");
    $("#MachineID").val("");
    $("#MachineName").val("");
    $("#MaintenanceDate").val("");

    $('#btnAdd').show();
    $('#Operator').css('border-color', 'lightgrey');
    $('#OperatorName').css('border-color', 'lightgrey');
    $('#Line').css('border-color', 'lightgrey');
    $('#Machine').css('border-color', 'lightgrey');
}
function validate() {
    var isValid = true;
    if ($('#Operator').val().trim() == "") {
        $('#Operator').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Operator').css('border-color', 'lightgrey');
    }
    if ($('#OperatorName').val().trim() == "") {
        $('#OperatorName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OperatorName').css('border-color', 'lightgrey');
    }
    if ($('#Line').val().trim() == "") {
        $('#Line').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Line').css('border-color', 'lightgrey');
    }
    if ($('#Machine').val().trim() == "") {
        $('#Machine').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Machine').css('border-color', 'lightgrey');
    }
    return isValid;
}
function DeleleByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/MachineMtnMonthYear/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result.Code == "00") {
                    MachineMtn_Search(1);
                    alert("Xóa thành công./Deleted successful!");
                }
                else if (result.Code == "99") {
                    alert(result.Message);
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
//Load data
function Load_MachineInfo() {
    var machineID = $('#MachineID').val();
    $.ajax({
        url: "/Machine/GetbyID_ReturnMachineResult/",
        data: { 'machineID': machineID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                $('#MachineName').val(result.lstMachine[0].MachineName);
                $('#LineID').val(result.lstMachine[0].LineID);
            }
            else if (result.Code == "01") {
                $('#MachineName').val("");
                $('#LineID').val("");
                alert("Không có mã máy/Not exists MachineID!");
            }
            else if (result.Code == "99") {
                alert('Lỗi lấy thông tin mã máy/Error Load_MachineInfo - ajax /Machine/GetbyID_ReturnMachineResult/ ' + errormessage.responseText);
            }
        },
        error: function (errormessage) {
            alert('Lỗi/Error Load_MachineInfo(e) ' + errormessage.responseText);
        }
    });
}
function LoadDataLine() {
    $.ajax({
        url: "/Line/ListAll/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var options;
            $.each(result, function (key, item) {
                options += '<option value="' + item.LineID + '">' + item.LineName + '</option>';
            });
            $('#Line').html(options);
        },
    });
}
//--DataTables-----------------------------------------------------------------------
var MachineMtnDataTableVM = {
    dt: null,
    init: function () {
        dt = $('#MachineMtn_DataTable').DataTable({
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
                "url": '/MachineMtnMonthYear/SelectByPageDataTable/',
                "type": "POST",
                //"contentType": "application/json;charset=UTF-8",
                //"dataType": "json",
                "data": function (data) {
                    data.MachineID = $('#MachineID').val();
                    data.FrequencyID = $('#Frequency').val();
                    data.MaintenanceDate = $('#MaintenanceDate').val();
                    data.Shift = $('#Shift').val();
                    data.Week = $('#Week').val();
                    data.Month = $('#Month').val();
                    data.Year = $('#Year').val();
                    data.OperatorID = $('#OperatorID').val();
                    data.CheckerResult = $("input[name='rdo_CheckerResult']:checked").val();
                    data.Result = $("input[name='rdo_Result']:checked").val();
                    data.OperatorID = $('#OperatorID').val();

                    return data;  // attention: not return JSON.stringify(data);
                },
                "dataSrc": function (response) {
                    var return_dataSrc = new Array();
                    result = response.data; //chi lay kieu ReturnMachineMtn.
                    if (result.Code == "00") {
                        $.each(result.lstMachineMtn, function (key, item) {
                            var dr = new Object();
                            dr.MachineID = item.MachineID;
                            dr.MachineName = item.MachineName;
                            dr.OperatorID = item.OperatorID;
                            dr.OperatorName = item.OperatorName;
                            dr.FrequencyID = Common_Get_MachineMtnFrequencyName_ById(item.FrequencyID);
                            dr.MaintenanceDate = SM_ConvertMsJsonDate_ToString(item.MaintenanceDate);
                            dr.Shift = Common_GetShiftNameByID(item.Shift);
                            dr.Week = item.Week != 0 ? item.Week : '';
                            dr.Month = item.Month != 0 ? item.Month : '';
                            dr.Year = item.Year != 0 ? item.Year : '';
                            dr.CheckerName = item.CheckerName;
                            dr.CheckerResult = item.CheckerResult;
                            var html_Result = '';
                            if (item.Result == "OK")
                                html_Result = '<span style="color:' + Common_setColorForOKNG(item.Result) + ';">' + item.Result + '</span>';
                            else if (item.Result == "NG")
                                html_Result = '<span style="color:' + Common_setColorForOKNG(item.Result) + ';">' + item.Result + '</span>';
                            dr.Result = html_Result;
                            
                            //dr.Result = item.Result;
                            //permission
                            var html = '';
                            var isAddSeperateChar = false;
                            if (result.permisionControllerVM.isAllow_View) {
                                html += '<a href=\'/MachineMtnMonthYear/Edit?id=' + item.ID + '\'>View</a>';
                                isAddSeperateChar = true;
                            }
                            if (result.permisionControllerVM.isAllow_Edit) {
                                if (isAddSeperateChar)
                                    html += '|';
                                else
                                    isAddSeperateChar = true;
                                html += '<a href=\'/MachineMtnMonthYear/Edit?id=' + item.ID + '\'>Edit</a>';
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
                       { "data": "MachineID" },
                       { "data": "MachineName" },
                       { "data": "OperatorID" },
                       { "data": "OperatorName" },
                       { "data": "FrequencyID" },
                       { "data": "MaintenanceDate" },
                       { "data": "Shift" },
                       { "data": "Week" },
                       { "data": "Month" },
                       { "data": "Year" },
                       { "data": "CheckerName" },
                       { "data": "CheckerResult" },
                       { "data": "Result" },
                       { "data": "Action" }
            ],
            "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]]
        });
        $('#MachineMtn_DataTable').DataTable().columns.adjust();
    },
    refresh: function () {
        dt.ajax.reload();
        $('#MachineMtn_DataTable').DataTable().columns.adjust();
    }
}



