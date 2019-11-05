var url_String = document.URL;
var url_HTML = new URL(url_String);
var productionControlID = url_HTML.searchParams.get("id");

// **** Region Load *****************************************************
$(document).ready(function () {
    InitForm();
    Get_ProductionConditionByID(productionControlID);
});
function InitForm() {
    $('#PdtCtrlDateTime').datetimepicker({
        format: 'MM/DD/YYYY HH:mm', // format you want to show on datetimepicker
        useCurrent: false, // default this is set to true
        defaultDate: new Date()
    });
    $('#ProgramName').prop('disabled', true);
    $('#PdtCtrlDateTime > .form-control').prop('disabled', true);
    //nen dung: alert($('#PdtCtrlDateTime').data("date"));
    //ko nen dung: alert($('#PdtCtrlDateTime').datepicker( "getDate" ));
    //$('#OperatorName').prop();

    //event.
    //$('#MachineID').keyup(function (event) {
    //    if ((event.keyCode ? event.keyCode : event.which) == 13) {
    //        var MachineID = $('#MachineID').val();
    //        SM_GetMachineName_ByID('#MachineID', '#MachineName');
    //    }
    //});
    //event.

    //$('#ProgramName').keyup(function (event) {
    //    if ((event.keyCode ? event.keyCode : event.which) == 13) {
    //        GetProgramPdtCtrl_ByProgramName(-1);
    //    }
    //});
}
function Get_ProductionConditionByID(ID) {
    $('#IndicatorID').css('border-color', 'lightgrey');
    $('#PdtCtrlDateTime').css('border-color', 'lightgrey');
    $('#ItemName').css('border-color', 'lightgrey');
    $('#ItemCode').css('border-color', 'lightgrey');
    $('#BatchNo').css('border-color', 'lightgrey');
    $('#MachineID').css('border-color', 'lightgrey');
    $('#MachineName').css('border-color', 'lightgrey');
    $('#ProgramName').css('border-color', 'lightgrey');
    $('#ControlItem').css('border-color', 'lightgrey');
    $('#TotalResult').css('border-color', 'lightgrey');
    $('#SeqNo').css('border-color', 'lightgrey');
    $.ajax({
        url: "/ProductionCondition/GetbyID/",   // lay production 
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //console.log('--222--------------------------------');
            //console.log(result.PdtCtrlDateTime);
            //console.log(SM_ConvertMsJson_DateTimePicker_ToString(result.PdtCtrlDateTime));

            $('#IndicationID').val(result.IndicationID);
            $('#PdtCtrlDateTime').data("DateTimePicker").date(SM_ConvertMsJson_DateTimePicker_ToString(result.PdtCtrlDateTime));
            //$('#PdtCtrlDateTime').prop('disabled', true);
            $('#MachineID').val(result.MachineID);
            $('#MachineID').prop('disabled', true);
            $('#ItemName').val(result.ItemName);
            $('#ItemCode').val(result.ItemCode);
            $('#BatchNo').val(result.BatchNo);
            $('#ProgramName').val(result.ProgramName);
            $('#ProgramName').prop('disabled', true);
            $('#OperatorID').val(result.UserID);
            $('#OperatorName').val(result.UserName);
            $('#OperatorName').prop('disabled', true);
            $('#MachineName').val(result.MachineName);
            $('#MachineName').prop('disabled', true);
            $('#TotalResult').val(result.Result);
            $('#TotalResult').prop('disabled', true);
            $('#SeqNo').val(result.SeqNo);
            $.ajax({
                url: "/ProductionCondition/GetbyProductionControlID",   // lay production detail
                type: "GET",
                data: { 'ProductionControlID': productionControlID },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result1) {
                    var html = '';
                    $.each(result1.lstProductionControlDetail, function (key, item) {
                        html += '<tr class=\"package-row\">';     // dung de querySelectorAll(package-row)
                        html += '<td style="display:none">' + item.ProgramPdtCtrlID + '</td>';
                        html += '<td>' + item.Part + '</td>';
                        html += '<td>' + item.ControlItem + '</td>';
                        html += '<td>' + item.ColumnName + '</td>';
                        html += '<td>' + item.SpecDisplay + '</td>';
                        html += '<td>' + item.Unit + '</td>';
                        html += '<td id=\"td_LowerLimit\">' + item.LowerLimit + '</td>';
                        html += '<td id=\"td_UpperLimit\">' + item.UpperLimit + '</td>';
                        html += GetHTML_ActualResult_SelectOrInputSelector_WithActualValue(item.LowerLimit.trim(), item.ActualValue);
                        //html += '<td><input id=\"td_ActualValue\" type=\"text\" value=' + item.ActualValue + ' onkeyup=\"return calculate_ActualResult_TotalResult(this)\" class=\"form-control\" </td>';
                        html += '<td id=\"td_Result\">' + item.Result + '</td>';
                        html += '<td style="display:none">' + item.ID + '</td>'; //Dung cho update Detail.  item.id =  tProductionControlDetail.ID trong sql.
                        html += '</tr>'; 
                    });
                    $('.tbody').html(html);
                    calculate_ActualResult_TotalResult();
                    
                },
            });
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

// TagID_ofMachineID = '#MachineID', TagID_ofMachineName = '#MachineName'
function SM_GetMachineName_ByID(TagID_ofMachineID, TagID_ofMachineName) {
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
            }
            else if (result.Code == "01") {
                $(TagID_ofMachineName).val("");
                //$('#LineID').val("");
                alert("Not exist MachineID!");
            }
            else if (result.Code == "99") { }
        },
        error: function (errormessage) {
            alert('Error GetMachineName_ByID(TagID_ofMachineID, TagID_ofMachineName) ' + errormessage.responseText);
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
                html += '<td id=\"td_LowerLimit\">' + item.LowerLimit + '</td>';
                html += '<td id=\"td_UpperLimit\">' + item.UpperLimit + '</td>';
                html += '<td><input id=\"td_ActualValue\" type=\"text\" onkeyup=\"return calculateActualResult_PdtCdt(this)\" class=\"form-control\" </td>';
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
            alert('Error' + errormessage.responseText);
        }
    });
}

// **** Region CRUD *****************************************************
function isValiateToUpdate() {
    var isValid = true;
    //if ($('#IndicationID').val().trim() == "") {
    //    $('#IndicationID').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#IndicationID').css('border-color', 'lightgrey');
    //}
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
        console.log('2');
    }
    else {
        $('#MachineID').css('border-color', 'lightgrey');
    }
    if ($('#MachineName').val().trim() == "") {
        $('#MachineName').css('border-color', 'Red');
        isValid = false;
        console.log('3');
    }
    else {
        $('#MachineName').css('border-color', 'lightgrey');
    }
    if ($('#ProgramName').val().trim() == "") {
        $('#ProgramName').css('border-color', 'Red');
        isValid = false;
        console.log('4');
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

    var t = document.getElementById('tblProductionControlDetail');
    if (t.rows.length == 1) {
        alert('Không có danh sách điều kiện sản xuất.'/'Cannot update. Not exists production Condition list.');
        isValid = false;
        console.log('5');
    }
    else {
        for (var j = 1; j < t.rows.length; j++) {
            var r = t.rows[j];
            var actualValue = t.rows[j].querySelector('#td_ActualValue').value;
            if (actualValue == "") {
                t.rows[j].querySelector('#td_ActualValue').style["border-color"] = "Red";
                isValid = false;
                console.log('6');
            }
        }
    }
    return isValid;
}

function UpdateProductionControl() {
    var res = isValiateToUpdate();
    if (res == false) {
        alert('Không cập nhật được. Hãy kiểm tra lại thông tin còn thiếu/Cannot insert! Check input value.');
        return false;
    }
    var totalresult = calculateTotalResult();
 
    var ProductionControlObject = {
        ID: productionControlID,  // danh cho update.
        IndicationID: $('#IndicationID').val(),
        MachineID: $('#MachineID').val(),
        PdtCtrlDateTime: $('#PdtCtrlDateTime').data("date"),
        UserID: $('#UserLogin').data('id'),    // update operator new with update permission.
        ItemName: $('#ItemName').val(),
        ItemCode: $('#ItemCode').val(),
        BatchNo: $('#BatchNo').val(),
        SeqNo: $('#SeqNo').val(),
        Result: totalresult,
        ProgramName: $('#ProgramName').val()
    };
    $.ajax({
        url: "/ProductionCondition/Update/",
        data: JSON.stringify(ProductionControlObject),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") { //not exists.
                productionControlID = result.LastID;
                // // phai co phan kiem tra xem dieu kien san xuat da ton tai chua. test check
                UpdateProductionControlDetail();
                alert("Update success!");
                $('#myModal').modal('hide');
            }
            else if (result.Code == "99")
                alert("Lỗi cập nhật bảng kiểm tra/ Error : " + resultCheck.Message);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function UpdateProductionControlDetail() {
    var t = document.getElementById('tblProductionControlDetail');
    for (var j = 1; j < t.rows.length; j++) {
        var r = t.rows[j];
        var inputs = r.getElementsByTagName("input");
        var ProgramPdtCtrlID = t.rows[j].cells[0].firstChild.data;
        var Part = t.rows[j].cells[1].firstChild != null ? t.rows[j].cells[1].firstChild.data : '' ;
        var controlItem = t.rows[j].cells[2].firstChild != null ? t.rows[j].cells[2].firstChild.data : '';
        var ColumnName = t.rows[j].cells[3].firstChild != null ? t.rows[j].cells[3].firstChild.data : "";
        var SpecDisplay = t.rows[j].cells[4].firstChild != null ? t.rows[j].cells[4].firstChild.data : "";
        var Unit = t.rows[j].cells[5].firstChild != null ? t.rows[j].cells[5].firstChild.data : "";
        var lowerLimit = t.rows[j].cells[6].firstChild.data;
        var upperLimit = t.rows[j].cells[7].firstChild.data;

        var actualValue;
        var inputs = r.getElementsByTagName("input");
        if (inputs.length >= 1) //if exists.
            actualValue = inputs[0].value;
        else {
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
            url: "/ProductionCondition/UpdateProductionControlDetail/",
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
function setColor_OK_NG(object, valueOKNG) {
    if (valueOKNG == "NG")
        object.style["color"] = "Red";
    else if (valueOKNG == "OK")
        object.style["color"] = "BLue";
}
// **** Region Event *****************************************************
function calculate_ActualResult_TotalResult() {
    calculateActualResult_PdtCdt();
    var TotalResult = calculateTotalResult();
    document.getElementById('TotalResult').value = TotalResult;
    setColor_OK_NG(document.getElementById('TotalResult'), TotalResult);
}
function GetHTML_ActualResult_SelectOrInputSelector_WithActualValue(myLowerLimit, myActualValue)
    {
        var returnHTML = "";
        if (isNaN(myLowerLimit)) //not number
        {
            //var isFirstOption = true;
            var array = myLowerLimit.split("/");
            returnHTML =   '<td><select id="td_ActualValue" class="form-control">';

            for(var i=0; i < array.length; i++)
            {
                if (array[i] != "" & array[i] == myActualValue)
                    returnHTML +='<option selected value="' + array[i]+'">'+ array[i]+'</option>';
                else
                    returnHTML +='<option value="' + array[i]+'">'+ array[i]+'</option>';
            }
            returnHTML +=   '<select></td>';
        }
        else { //is Number.
            if (myActualValue == null)
                returnHTML = '<td><input id="td_ActualValue" type="text" onkeyup="return calculate_ActualResult_TotalResult(this)" class="form-control" </td>';
            else
                returnHTML += '<td><input id=\"td_ActualValue\" type=\"text\" value=' + myActualValue + ' onkeyup=\"return calculate_ActualResult_TotalResult(this)\" class=\"form-control\" </td>';
        }
        return returnHTML;
    }
function calculateActualResult_PdtCdt() {
    var rows = document.querySelectorAll("tr.package-row");
    rows.forEach(function (currentRow) {
        //check isvalidate
        var ResultCell = currentRow.querySelector('#td_Result');
        var td_ActualValue_selector = currentRow.querySelector('#td_ActualValue'); //.value = currentRow.querySelector('#td_ActualValue').value.trim();
        var td_ActualValue;
        if (td_ActualValue_selector.tagName == "INPUT")
            td_ActualValue = currentRow.querySelector('#td_ActualValue').value;
        else if (td_ActualValue_selector.tagName = "SELECT")
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
//function calculateActualResult_PdtCdt() {
//    var rows = document.querySelectorAll("tr.package-row");
//    rows.forEach(function (currentRow) {
//        var td_LowerLimit = currentRow.querySelector('#td_LowerLimit').innerHTML;
//        var td_UpperLimit = currentRow.querySelector('#td_UpperLimit').innerHTML;
//        var td_ActualValue = currentRow.querySelector('#td_ActualValue').value;
//        var LowerLimit;
//        var UpperLimit;
//        var ActualValue;
//        var ResultCell = currentRow.querySelector('#td_Result');

//        if (td_ActualValue == "") {
//            currentRow.querySelector('#td_ActualValue').style["border-color"] = "Red";
//        }
//        else {

//            if (!isNaN(parseFloat(td_LowerLimit)) && !isNaN(parseFloat(td_UpperLimit)) && !isNaN(parseFloat(td_ActualValue))) {  //neu la kieu so.
//                LowerLimit = parseFloat(td_LowerLimit);
//                UpperLimit = parseFloat(td_UpperLimit);
//                ActualValue = parseFloat(td_ActualValue);

//                //calculate.
//                currentRow.querySelector('#td_ActualValue').style["border-color"] = "lightgrey";
//                if (ActualValue >= LowerLimit & ActualValue <= UpperLimit) {
//                    ResultCell.innerHTML = "OK";
//                }
//                else {
//                    ResultCell.innerHTML = "NG";
//                }
//            }
//            else { //neu la kieu chu.
//                LowerLimit = td_LowerLimit;
//                UpperLimit = td_UpperLimit;
//                ActualValue = td_ActualValue;
//                //calculate by compare string value.
//                currentRow.querySelector('#td_ActualValue').style["border-color"] = "lightgrey";
//                if (LowerLimit.includes(ActualValue)) {
//                    ResultCell.innerHTML = "OK";
//                }
//                else {
//                    ResultCell.innerHTML = "NG";
//                }
//            }
//        }

//    });
//}


