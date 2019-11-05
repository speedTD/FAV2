
// **** Region Load *****************************************************
var productionControlID = 0;
$(document).ready(function () {
    InitForm();
});
function InitForm() {
    $('#PdtCtrlDateTime').datetimepicker({
        format: 'MM/DD/YYYY HH:mm', // format you want to show on datetimepicker
        useCurrent: false, // default this is set to true
        defaultDate: new Date()
    });
    //nen dung: alert($('#PdtCtrlDateTime').data("date"));
    //ko nen dung: alert($('#PdtCtrlDateTime').datepicker( "getDate" ));
    $('#OperatorName').val($('#UserLogin').data('username'));

    //event.
    $('#MachineID').keyup(function (event) {
        if ((event.keyCode ? event.keyCode : event.which) == 13) {
            var MachineID = $('#MachineID').val();
            SM_GetMachineName_ByID('#MachineID', '#MachineName', '#LineID','#MachineType');
        }
    });
    //event.
    $('#ProgramName').keyup(function (event) {
        if ((event.keyCode ? event.keyCode : event.which) == 13) {
            GetProgramPdtCtrl_ByProgramName(-1);
        }
    });
}
// TagID_ofMachineID = '#MachineID', TagID_ofMachineName = '#MachineName'
function SM_GetMachineName_ByID(TagID_ofMachineID, TagID_ofMachineName, TagID_ofLineID = "", TagID_ofMachineType = "" ) {
    var machineID = $(TagID_ofMachineID).val();
        
    if (machineID == "") {
        $(TagID_ofMachineName).val("");
        return;
    }
    $.ajax({
        url: "/Machine/GetbyID_ReturnMachineResult/",
        data: { 'machineID': machineID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                $(TagID_ofMachineName).val(result.lstMachine[0].MachineName);
                //$('#LineID').val(result.lstMachine[0].LineID);
                if(TagID_ofLineID != "")
                    $(TagID_ofLineID).val(result.lstMachine[0].LineID);
                if(TagID_ofLineID != "")
                    $(TagID_ofMachineType).val(result.lstMachine[0].MachineTypeName);
            }
            else if (result.Code == "01") {
                $(TagID_ofMachineName).val("");
                $(TagID_ofLineID).val("");
                $(TagID_ofMachineType).val("");
                alert("Không tồn tại MachineID./Not exist MachineID!");
            }
            else if (result.Code == "99") {
                alert('Lỗi/Error: ' + result.Message);
            }
        },
        error: function (errormessage) {
            alert('Lỗi/Error GetMachineName_ByID(TagID_ofMachineID, TagID_ofMachineName): ' + errormessage.responseText);
        }
    });
}
    // **** Region Load *****************************************************
    // Tuong tu file ProgramPdtCtrl.js
    function GetProgramPdtCtrl_ByProgramName(_pageNumber) {
        var ProgramPdtCtrlObj = {
            ProgramName: $('#ProgramName').val(),
            Part: "",
            ControlItem: "",
            ColumnName: "",
            SpecDisplay: "",
            Unit: "",
            LowerLimit: "",
            UpperLimit: "",
            OperatorID: "",
        };
        var returnObj = {
            aProgramPdtCtrl: ProgramPdtCtrlObj,
            PageNumber: _pageNumber
        }
        $.ajax({
            url: "/ProgramPdtCtrl/GetProgramPdtCtrl_ByProgramName",
            data: JSON.stringify(returnObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                var html = '';
                $.each(result.lstProgramPdtCtrl, function (key, item) {
                    html += '<tr class=\"package-row\">';     // dung de querySelectorAll(package-row)
                    html += '<td style="display:none">' + item.ProgramPdtCtrlID + '</td>';
                    html += '<td>' + item.Part + '</td>';
                    html += '<td>' + item.ControlItem + '</td>';
                    html += '<td>' + item.ColumnName + '</td>';
                    html += '<td>' + item.SpecDisplay + '</td>';
                    html += '<td>' + item.Unit + '</td>';
                    html += '<td id=\"td_LowerLimit\">' + item.LowerLimit.trim() + '</td>';
                    html += '<td id=\"td_UpperLimit\">' + item.UpperLimit.trim() + '</td>';

                    html += GetHTML_ActualResult_SelectOrInputSelector(item.LowerLimit.trim());

                    //html += '<td><input id="td_ActualValue" type="text" onkeyup="return calculate_ActualResult_TotalResult(this)" class="form-control" </td>';
                    html += '<td id=\"td_Result\"></td>';
                    html += '<td style="display:none">' + 0 + '</td>'; // item.id =  tProductionControlDetail.ID trong sql.
                    //html += '<td>' + item.OperatorID + '</td>';
                    //html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
                    //html += '<td><a href="#" onclick="return GetbyID(\'' + item.ProgramName + ',' + item.Part + ',' + item.ControlItem + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ProgramName + ',' + item.Part + ',' + item.ControlItem + '\')">Delete</a></td>';
                    html += '</tr>';
                });
                $('.tbody').html(html);
                //Paging 
                //if (result.pageNumber == 1) {
                //    reloadPageTool(result.TotalPage);
                //}
            },
            error: function (errormessage) {
                alert('Lỗi/Error: ' + errormessage.responseText);
            }
        });
    }

    // **** Region CRUD *****************************************************
    function isValiateToInsert() {
        var isValid = true;
        if ($('#PdtCtrlDateTime').data("date") == "") {
            $('#PdtCtrlDateTime').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#PdtCtrlDateTime').css('border-color', 'lightgrey');
        }
        //if ($('#ItemName').val().trim() == "") {
        //    $('#ItemName').css('border-color', 'Red');
        //    isValid = false;
        //}
        //else {
        //    $('#ItemName').css('border-color', 'lightgrey');
        //}
        //if ($('#ItemCode').val().trim() == "") {
        //    $('#ItemCode').css('border-color', 'Red');
        //    isValid = false;
        //}
        //else {
        //    $('#ItemCode').css('border-color', 'lightgrey');
        //}
        //if ($('#BatchNo').val().trim() == "") {
        //    $('#BatchNo').css('border-color', 'Red');
        //    isValid = false;
        //}
        //else {
        //    $('#BatchNo').css('border-color', 'lightgrey');
        //}
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
        //if ($('#UserID').val().trim() == "") {
        //    $('#UserID').css('border-color', 'Red');
        //    isValid = false;
        //}
        //else {
        //    $('#UserID').css('border-color', 'lightgrey');
        //}
        //if ($('#SeqNo').val().trim() == "") {
        //    $('#SeqNo').css('border-color', 'Red');
        //    isValid = false;
        //}
        //else {
        //    $('#SeqNo').css('border-color', 'lightgrey');
        //}

        //kiem tra danh sach dieu kien san xuat.
        var t = document.getElementById('tblProductionControlDetail');
        if (t.rows.length == 1) {
            alert('Không thể thêm mới. Không có danh mục điều kiện sản xuất./Cannot insert. Not exists production Condition list.');
            isValid = false;
        }
        else {
            for (var j = 1; j < t.rows.length; j++) {
                var r = t.rows[j];
                var actualValue = t.rows[j].querySelector('#td_ActualValue').value;
                if (actualValue == "") {
                    t.rows[j].querySelector('#td_ActualValue').style["border-color"] = "Red";
                    isValid = false;
                }
            }
        }
        return isValid;
    }

    function BeginInsert_ProductionControl() {
        //check validate.
        var res = isValiateToInsert();
        if (res == false) {
            alert('Không thêm được, hãy kiểm tra giá trị nhập vào./Cannot insert. Check input value!');
            return false;
        }
        CheckProductionControlData();
    }
    function CheckProductionControlData() {
        var ProductionControlObject = {
            ID: 0,
            IndicationID: $('#IndicationID').val(),
            MachineID: $('#MachineID').val().trim(),
            PdtCtrlDateTime: $('#PdtCtrlDateTime').data("date"),
            UserID: $('#UserLogin').data('id'),    // dung cho Insert.
            ItemName: $('#ItemName').val(),
            ItemCode: $('#ItemCode').val(),
            BatchNo: $('#BatchNo').val(),
            SeqNo: $('#SeqNo').val(),
            //Result: totalresult,
            ProgramName: $('#ProgramName').val().trim()
        };
        $.ajax({
            url: "/ProductionCondition/CountBySelection/",
            data: JSON.stringify(ProductionControlObject),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if ((result.Code == "00" || result.Code == "01") && result.Total == 0) { //not exists.
                    InsertProductionControl();
                }
                else if (result.Code == "99")
                    alert('Lỗi/Error 99 CheckProductionControlData()' + result.Message);
                else
                    alert("Đã tồn tại bảng kiểm tra này. Không thể thêm mới./Checksheet already exists! Cannot insert.");
            },
            error: function (errormessage) {
                alert('Lỗi/Error CheckProductionControlData()' + errormessage.responseText);
            }
        });
    }
    function InsertProductionControl() {
        var totalresult = calculateTotalResult();
        // insert.
        var ProductionControlObject = {
            ID: 0,
            IndicationID: $('#IndicationID').val(),
            MachineID: $('#MachineID').val(),
            PdtCtrlDateTime: $('#PdtCtrlDateTime').data("date"),
            UserID: $('#UserLogin').data('id'),    // dung cho Insert.
            ItemName: $('#ItemName').val(),
            ItemCode: $('#ItemCode').val(),
            BatchNo: $('#BatchNo').val(),
            SeqNo: $('#SeqNo').val(),
            Result: totalresult,
            ProgramName: $('#ProgramName').val()
        };
        $.ajax({
            url: "/ProductionCondition/Insert/",
            data: JSON.stringify(ProductionControlObject),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.Code == "00") {
                    productionControlID = result.LastID;
                    InsertProductionControlDetail();
                    alert("Thêm mới thành công./Insert success!");
                    $('#myModal').modal('hide');
                }
                else if (result.Code == "99")
                    alert("Lỗi phần kiểm tra dữ liệu ProductionCondition-Insert / Error : " + resultCheck.Message);
            },
            error: function (errormessage) {
                alert("Lỗi/Error: " + errormessage.responseText);
            }
        });

    }

    function InsertProductionControlDetail() {
        var t = document.getElementById('tblProductionControlDetail');
        for (var j = 1; j < t.rows.length; j++) {
            var r = t.rows[j];
            var ProgramPdtCtrlID = t.rows[j].cells[0].firstChild.data;
            var Part = t.rows[j].cells[1].firstChild != null ? t.rows[j].cells[1].firstChild.data : "";
            var controlItem = t.rows[j].cells[2].firstChild != null ? t.rows[j].cells[2].firstChild.data : "";
            var ColumnName = t.rows[j].cells[3].firstChild != null ? t.rows[j].cells[3].firstChild.data : "";
            var SpecDisplay = t.rows[j].cells[4].firstChild != null ? t.rows[j].cells[4].firstChild.data : "";
            var Unit = t.rows[j].cells[5].firstChild != null ? t.rows[j].cells[5].firstChild.data : "";
            var lowerLimit = t.rows[j].cells[6].firstChild != null ? t.rows[j].cells[6].firstChild.data : "";
            var upperLimit = t.rows[j].cells[7].firstChild != null ? t.rows[j].cells[7].firstChild.data : "";
            
            var actualValue;
            var inputs = r.getElementsByTagName("input");
            if(inputs.length >= 1) //if exists.
                actualValue = inputs[0].value;
            else 
            {
                inputs = r.getElementsByTagName("select");
                actualValue = inputs[0].selectedOptions[0].value;
            }
            var result = t.rows[j].cells[9].firstChild.data;
            var productionControlDetailID = t.rows[j].cells[10].firstChild.data;

            var ProductionControlDetailObj = {
                ID: productionControlDetailID,
                ProductionControlID: productionControlID,
                ProgramName: $('#ProgramName').val(),
                ProgramPdtCtrlID: ProgramPdtCtrlID,
                ControlItem: controlItem,
                ColumnName: ColumnName,
                SpecDisplay: SpecDisplay,
                Unit: Unit,
                LowerLimit: lowerLimit,
                UpperLimit: upperLimit,
                ActualValue: actualValue,
                Result: result,
                ResultContent: ""
            }
            
            $.ajax({
                url: "/ProductionCondition/InsertProductionControlDetail/",
                data: JSON.stringify(ProductionControlDetailObj),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if(result.Code == "00")
                    {}
                    else if(result.Code == "99")
                    {
                        alert('Lỗi cập nhật chi tiết ĐKSX/Error ' + result.Message);
                    }
                    $('#myModal').modal('hide');
                },
                error: function (errormessage) {
                    alert('Lỗi/Error: ' + errormessage.responseText);
                }
            });
        }
    }

    // **** Region Event *****************************************************
    function calculate_ActualResult_TotalResult() {
        calculateActualResult_PdtCdt();
        var totalResult = calculateTotalResult();
        document.getElementById('TotalResult').value = totalResult;
        setColor_OK_NG(document.getElementById('TotalResult'), totalResult);
    }

    function GetHTML_ActualResult_SelectOrInputSelector(value)
    {
        var returnHTML = "";
        if(isNaN(value)) //not number
        {
            var isFirstOption = true;
            var array = value.split("/");
            returnHTML =   '<td><select id="td_ActualValue" class="form-control">';

            for(var i=0; i < array.length; i++)
            {
                if(array[i] != "" & isFirstOption)            
                    returnHTML +='<option selected value="' + array[i]+'">'+ array[i]+'</option>';
                else
                    returnHTML +='<option value="' + array[i]+'">'+ array[i]+'</option>';
            }
            returnHTML +=   '<select></td>';
        }
        else{
            returnHTML = '<td><input id="td_ActualValue" type="text" onkeyup="return calculate_ActualResult_TotalResult(this)" class="form-control" </td>';
        }
        return returnHTML;
    }

    function setColor_OK_NG(object,valueOKNG)
    {
        if(valueOKNG == "NG")
            object.style["color"] = "Red";
        else if(valueOKNG == "OK")
            object.style["color"] = "BLue";
    }

    function calculateActualResult_PdtCdt() {
        var rows = document.querySelectorAll("tr.package-row");
        rows.forEach(function (currentRow) {
            //check isvalidate
            var ResultCell = currentRow.querySelector('#td_Result');
            var td_ActualValue_selector = currentRow.querySelector('#td_ActualValue'); //.value = currentRow.querySelector('#td_ActualValue').value.trim();
            var td_ActualValue ;
            if(td_ActualValue_selector.tagName == "INPUT")
                td_ActualValue = currentRow.querySelector('#td_ActualValue').value;
            else if(td_ActualValue_selector.tagName = "SELECT")
                td_ActualValue = currentRow.querySelector('#td_ActualValue').selectedOptions[0].value;
            if (td_ActualValue == "") {
                currentRow.querySelector('#td_ActualValue').style["border-color"] = "Red";
                ResultCell.innerHTML = "NG";
                ResultCell.style["color"] = "Red";
                return;
            }

            var td_LowerLimit = currentRow.querySelector('#td_LowerLimit').innerHTML;
            var td_UpperLimit = currentRow.querySelector('#td_UpperLimit').innerHTML;

            var LowerLimit;
            var UpperLimit;
            var ActualValue;

            if (!isNaN(parseFloat(td_LowerLimit)) && !isNaN(parseFloat(td_UpperLimit)) && !isNaN(parseFloat(td_ActualValue))) {  //neu la kieu so.
                LowerLimit = parseFloat(td_LowerLimit);
                UpperLimit = parseFloat(td_UpperLimit);
                ActualValue = parseFloat(td_ActualValue);
                //calculate.
                currentRow.querySelector('#td_ActualValue').style["border-color"] = "lightgrey";
                if (ActualValue >= LowerLimit & ActualValue <= UpperLimit) {
                    ResultCell.innerHTML = "OK";
                    ResultCell.style["color"] = "Blue";
                }
                else {
                    ResultCell.innerHTML = "NG";
                    ResultCell.style["color"] = "Red";
                }
            }
            else { //neu la kieu chu.
                LowerLimit = td_LowerLimit;
                UpperLimit = td_UpperLimit;
                ActualValue = td_ActualValue;
                //calculate by compare string value.
                currentRow.querySelector('#td_ActualValue').style["border-color"] = "lightgrey";
                if (LowerLimit.includes(ActualValue)) {
                    ResultCell.innerHTML = "OK";
                    ResultCell.style["color"] = "Blue";
                }
                else {
                    ResultCell.innerHTML = "NG";
                    ResultCell.style["color"] = "Red";
                }
            }

        });
    }
    function calculateTotalResult() {
        // Tinh Total Result.
        var totalresult = "OK";
        var t = document.getElementById('tblProductionControlDetail');
        for (var j = 1; j < t.rows.length; j++) {
            var resultDetail = t.rows[j].querySelector('#td_Result').innerHTML;  //t.rows[j].cells[7].firstChild.data;
            if (resultDetail == "NG" || resultDetail == "") {
                totalresult = "NG";
                break;
            }
        }
        return totalresult;
    }