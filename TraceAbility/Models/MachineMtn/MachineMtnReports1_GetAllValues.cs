using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;
using System.Drawing;

namespace TestABC.Models.Data
{
    public partial class MachineMtnReportDatas
    {
        private static readonly CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");
        private void MachineMtnReportDatas_GetExcel_Daily(MachineMtnReportDataSearch machineMtnReportDataSearch, IEnumerable<MachineMtnReportData> data, ExcelPackage ep, List<BuildArgs> buildArgs)
        {
            //init value.
            //fix cứng số cột: số thự tự tên nước, saleperson, Year và danh sách 12 tháng (orderMonthFromIndex --> orderMonthToIndex)
            string prefixTotalColumn = "_*_";
            int startRowIdx = 8; //  title table index.
            int machinePartName_ColIdx = 2;
            int contentMtn_ColIdx = 3;
            int toolMtn_ColIdx = 4;
            int methodMtn_ColIdx = 5;
            int standard_ColIdx = 6;
            int runningFrom_ColIdx = 7;
            int DayAndShiftOrder_FromColIdx = runningFrom_ColIdx;
            int DayAndShiftOrder_ToColIdx = 0;
            int totalColumns_ColIdx = 0; // tổng số cột.
            int rowIndex = startRowIdx;
            //total Result index.
            int totalResult_RowIdx = 0;
            int OperatorRowIdx;
            int CheckerRowIdx;
            // workbook
            var wb = ep.Workbook;
            // new worksheet
            var ws = wb.Worksheets.Add("Sheet01");

            #region Title.
            ws.Cells[2, 7].Value = ("Bảng kiểm tra bảo trì máy/Machine maintenance checksheet").ToUpper();
            ws.Cells[2, 7].Style.Font.Bold = true;
            ws.Cells[2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[2, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // merge cells 
            buildArgs.Add(new BuildArgs()
            {
                Action = BuildAction.Merge,
                Worksheet = ws,
                FromRow = 2,
                FromColumn = 7,
                ToRow = 2,
                ToColumn = 14
            });

            using (ExcelRange Rng = ws.Cells[3, 7]) //machineid.
            {
                Rng.Style.Font.Size = 11;
                ExcelRichTextCollection RichTxtCollection = Rng.RichText;
                ExcelRichText RichText = RichTxtCollection.Add("MachineID: ");
                RichText.Bold = false;
                RichText = RichTxtCollection.Add(machineMtnReportDataSearch.MachineID);
                RichText.Bold = true;
                RichText = RichTxtCollection.Add("  Machine Name: ");
                RichText.Bold = false;
                RichText = RichTxtCollection.Add(machineMtnReportDataSearch.MachineName);
                RichText.Bold = true;
            }
            ws.Cells[3, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[3, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // merge cells 
            buildArgs.Add(new BuildArgs()
            {
                Action = BuildAction.Merge,
                Worksheet = ws,
                FromRow = 3,
                FromColumn = 7,
                ToRow = 3,
                ToColumn = 14
            });

            using (ExcelRange Rng = ws.Cells[4, 7]) //FromDate ToDate
            {
                Rng.Style.Font.Size = 11;
                ExcelRichTextCollection RichTxtCollection = Rng.RichText;
                ExcelRichText RichText = RichTxtCollection.Add("Từ ngày/FromDate: ");
                RichText = RichTxtCollection.Add(machineMtnReportDataSearch.FromDate.ToString("MM/dd/yyyy"));
                RichText.Bold = true;
                RichText = RichTxtCollection.Add("    Đến ngày/ToDate: ");
                RichText.Bold = false;
                RichText = RichTxtCollection.Add(machineMtnReportDataSearch.ToDate.ToString("MM/dd/yyyy"));
                RichText.Bold = true;
            }
            ws.Cells[4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[4, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // merge cells 
            buildArgs.Add(new BuildArgs()
            {
                Action = BuildAction.Merge,
                Worksheet = ws,
                FromRow = 4,
                FromColumn = 7,
                ToRow = 4,
                ToColumn = 14
            });
            #endregion

            #region Header.
            // Header Value.
            ws.Cells[rowIndex, machinePartName_ColIdx].Value = "Part Name";
            ws.Cells[rowIndex, contentMtn_ColIdx].Value = "Content";
            ws.Cells[rowIndex, toolMtn_ColIdx].Value = "Tool";
            ws.Cells[rowIndex, methodMtn_ColIdx].Value = "Method";
            ws.Cells[rowIndex, standard_ColIdx].Value = "Standard";

            //-- MaintenanceDate headers
            var dict_DayOrder = new Dictionary<string, int>();
            int countDayAndShiftOrderColumn = 0;

            var dict_DayAndShiftOrder = new Dictionary<string, int>();
            for (var day = machineMtnReportDataSearch.FromDate; day <= machineMtnReportDataSearch.ToDate; day = day.AddDays(1))
            {
                int oneDay_FullShift_FromColIndex = DayAndShiftOrder_FromColIdx + countDayAndShiftOrderColumn;
                for (int i = 0; i <= 2; i++) // i = order number column in excel, i++ = shift number.
                {
                    //date header.
                    ws.Cells[rowIndex, (countDayAndShiftOrderColumn) + runningFrom_ColIdx].Value = day.ToString("MM/dd", enUS);
                    //shift header.
                    ws.Cells[rowIndex + 1, countDayAndShiftOrderColumn + runningFrom_ColIdx].Value = (i + 1).ToString();
                    dict_DayOrder.Add(day.ToString("MM/dd", enUS) + "_" + (i + 1).ToString(), countDayAndShiftOrderColumn); // sample "15/3_1" = date is 15/3 and shift = 1
                    dict_DayAndShiftOrder.Add(day.ToString("MM/dd", enUS) + (i + 1).ToString(), countDayAndShiftOrderColumn);
                    countDayAndShiftOrderColumn++;
                }
                int oneDay_FullShift_ToColIndex = oneDay_FullShift_FromColIndex + 2;
                //merge cell MaintenanceDate.
                buildArgs.Add(new BuildArgs()
                {
                    Action = BuildAction.Merge,
                    Worksheet = ws,
                    FromRow = rowIndex,
                    FromColumn = oneDay_FullShift_FromColIndex,
                    ToRow = rowIndex,
                    ToColumn = oneDay_FullShift_ToColIndex
                });
                //intDayOrder++;
            }
            DayAndShiftOrder_ToColIdx = DayAndShiftOrder_FromColIdx + countDayAndShiftOrderColumn - 1;
            totalColumns_ColIdx = DayAndShiftOrder_ToColIdx;
            // Headers style
            using (var cell = ws.Cells[rowIndex, machinePartName_ColIdx, rowIndex + 1, totalColumns_ColIdx])
            {
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 11;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            rowIndex++;
            rowIndex++;
            #endregion


            //checking data.
            if (data == null)
            {
                return;
            }
            #region Add TotalResult.

            var maintenanceDateGroups = data.GroupBy(d => d.MaintenanceDate);
                foreach (var maintenanceDateGroup in maintenanceDateGroups)
                {
                    MachineMtnReportData tMachineMtn = maintenanceDateGroup.FirstOrDefault();
                    MachineMtnReportData TotalResult_Row = new MachineMtnReportData();
                    TotalResult_Row.MaintenanceDate = tMachineMtn.MaintenanceDate;
                    TotalResult_Row.Shift = tMachineMtn.Shift;
                    TotalResult_Row.FrequencyID = tMachineMtn.FrequencyID;
                    TotalResult_Row.MachinePartID = int.MaxValue;
                    TotalResult_Row.MachinePartName = "zzzzz01" + prefixTotalColumn + "Total Result";
                    TotalResult_Row.MtnDetailResult = tMachineMtn.TotalResult;
                    TotalResult_Row.MtnDetailResultContents = tMachineMtn.TotalResultContents;

                    MachineMtnReportData Operator_Row = new MachineMtnReportData();
                    Operator_Row.MaintenanceDate = tMachineMtn.MaintenanceDate;
                    Operator_Row.Shift = tMachineMtn.Shift;
                    Operator_Row.MachinePartID = int.MaxValue;
                    Operator_Row.MachinePartName = "zzzzz02" + prefixTotalColumn + "Operator";
                    Operator_Row.MtnDetailResult = tMachineMtn.OperatorName;

                    MachineMtnReportData Checker_Row = new MachineMtnReportData();
                    Checker_Row.MaintenanceDate = tMachineMtn.MaintenanceDate;
                    Checker_Row.Shift = tMachineMtn.Shift;
                    Checker_Row.MachinePartID = int.MaxValue;
                    Checker_Row.MachinePartName = "zzzzz03" + prefixTotalColumn + "Checker";
                    Checker_Row.MtnDetailResult = tMachineMtn.CheckerName ;

                    IEnumerable<MachineMtnReportData> x = new MachineMtnReportData[] { TotalResult_Row, Operator_Row, Checker_Row };
                    data = data.Concat(x);
                }
            
            #endregion


            //***************** Data *************
            #region MachinePartName.
            var machinePartNameGroups = data.GroupBy(d => new { d.MachinePartID, d.MachinePartName }).OrderBy(g => g.Key.MachinePartName);
            #endregion
            //var Start_RunningFromRowIndex = rowIndex;
            foreach (var machinePartNameGroup in machinePartNameGroups)
            {
                int machinePartNameGroup_FromRowIndex = rowIndex;
                #region ContentMtn.
                int contentMtnFromRowIndex = rowIndex;
                var contentMtnGroups = machinePartNameGroup.GroupBy(d => d.ContentMtn).OrderBy(g => g.Key);
                #endregion
                foreach (var contentMtnGroup in contentMtnGroups)
                {
                    int contentMtnGroup_FromRowIndex = rowIndex;
                    #region ToolMtn.
                    int toolMtnFromRowIndex = rowIndex;
                    var toolMtnGroups = contentMtnGroup.GroupBy(d => d.ToolMtn).OrderBy(g => g.Key);
                    #endregion
                    foreach (var toolMtnGroup in toolMtnGroups)
                    {
                        int toolMtnGroup_FromRowIndex = rowIndex;
                        #region MethodMtn.
                        int methodMtnFromRowIndex = rowIndex;
                        var methodMtnGroups = toolMtnGroup.GroupBy(d => d.MethodMtn).OrderBy(g => g.Key);
                        #endregion
                        foreach (var methodMtnGroup in methodMtnGroups)
                        {
                            #region Standard.
                            int standard_FromRowIndex = rowIndex;
                            var standardGroups = methodMtnGroup.GroupBy(d => d.Standard).OrderBy(g => g.Key);

                            foreach (var standardGroup in standardGroups)
                            {
                                //insert MachinePartName, Tool, Content, ......
                                string _machinePartName = machinePartNameGroup.Key.MachinePartName;
                                if (_machinePartName.Contains(prefixTotalColumn)) //totalResult.
                                {
                                    if (_machinePartName.Contains("zzzzz01")) //get totalResult Row index.
                                        totalResult_RowIdx = rowIndex;
                                    _machinePartName = _machinePartName.Remove(0, _machinePartName.IndexOf(prefixTotalColumn) + prefixTotalColumn.Length);
                                }
                                ws.Cells[rowIndex, machinePartName_ColIdx].Value = _machinePartName;
                                ws.Cells[rowIndex, contentMtn_ColIdx].Value = contentMtnGroup.Key;
                                ws.Cells[rowIndex, toolMtn_ColIdx].Value = toolMtnGroup.Key;
                                ws.Cells[rowIndex, methodMtn_ColIdx].Value = methodMtnGroup.Key;
                                ws.Cells[rowIndex, standard_ColIdx].Value = standardGroup.Key;
                                                           
                                #region Maintenance Days.
                                int maintenanceDayFromRowIndex = rowIndex;
                                var maintenanceDayGroups = standardGroup.GroupBy(d => d.MaintenanceDate.ToString("MM/dd")).OrderBy(g => g.Key);
                                #endregion
                                int orderDay = 1;  // ***************   checking aaaa : orderday: phai lay theo thu tu cua methodMtnGroup

                                ws.Cells[rowIndex, DayAndShiftOrder_FromColIdx, rowIndex, DayAndShiftOrder_ToColIdx].Value = "";
                                foreach (var maintenanceDayGroup in maintenanceDayGroups)
                                {
                                    var mtnDayShiftGroups = maintenanceDayGroup.GroupBy(d => d.Shift);
                                    #region one day.

                                    foreach (var mtnDayShiftGroup in mtnDayShiftGroups)
                                    {
                                        #region one day and one shift.
                                        // ******************* Gán cụ thể vào Excel.
                                        DateTime currentMtnDate = mtnDayShiftGroup.FirstOrDefault().MaintenanceDate; //.ToString("MM/dd", enUS);
                                        MachineMtnReportData currentMtnReportData = mtnDayShiftGroup.FirstOrDefault();
                                        int currentOrderDayShift = dict_DayOrder[currentMtnDate.ToString("MM/dd", enUS) + "_" + currentMtnReportData.Shift];

                                        var currentCells = ws.Cells[rowIndex, currentOrderDayShift - 1 + runningFrom_ColIdx];
                                        //set value for cell.   
                                        currentCells.Value = currentMtnReportData.MtnDetailResult;

                                        //set color for OK, NG.                                       
                                        if (currentMtnReportData.MtnDetailResult == SMCommon.MachineMtnResult_OK)
                                        {
                                            currentCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            currentCells.Style.Fill.BackgroundColor.SetColor(SMCommon.ReportMachineMtn_Result_OK_Color);
                                        }
                                        else if (currentMtnReportData.MtnDetailResult == SMCommon.MachineMtnResult_NG)
                                        {
                                            currentCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            currentCells.Style.Fill.BackgroundColor.SetColor(SMCommon.ReportMachineMtn_Result_NG_Color);
                                        }
                                        orderDay++;
                                        #endregion
                                    }
                                    #endregion
                                }
                                rowIndex++;
                                //Borders.
                            }
                            int standard_ToRowIndex = rowIndex - 1;
                            #endregion
                        }
                        int toolMtnGroup_ToRowIndex = rowIndex - 1;
                        // merge cells 
                        buildArgs.Add(new BuildArgs()
                        {
                            Action = BuildAction.Merge,
                            Worksheet = ws,
                            FromRow = toolMtnGroup_FromRowIndex,
                            FromColumn = toolMtn_ColIdx,
                            ToRow = rowIndex - 1,
                            ToColumn = toolMtn_ColIdx
                        });
                    }
                    int contentMtnGroup_ToRowIndex = rowIndex - 1;
                    // merge cells 
                    buildArgs.Add(new BuildArgs()
                    {
                        Action = BuildAction.Merge,
                        Worksheet = ws,
                        FromRow = contentMtnGroup_FromRowIndex,
                        FromColumn = contentMtn_ColIdx,
                        ToRow = rowIndex - 1,
                        ToColumn = contentMtn_ColIdx
                    });
                }
                int machinePartNameGroup_ToRowIndex = rowIndex - 1;
                // merge cells 
                buildArgs.Add(new BuildArgs()
                {
                    Action = BuildAction.Merge,
                    Worksheet = ws,
                    FromRow = machinePartNameGroup_FromRowIndex,
                    FromColumn = machinePartName_ColIdx,
                    ToRow = rowIndex - 1,
                    ToColumn = machinePartName_ColIdx
                });
            }
            #region Footer
            #endregion

            #region Border Style.
            int toRowIndex = rowIndex - 1;
            // cells borders
            ws.View.ShowGridLines = false;
            foreach (var cell in ws.Cells[startRowIdx - 1, machinePartName_ColIdx, toRowIndex, totalColumns_ColIdx])
            {
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Top.Color.SetColor(Color.Black);
                cell.Style.Border.Bottom.Color.SetColor(Color.Black);
                cell.Style.Border.Left.Color.SetColor(Color.Black);
                cell.Style.Border.Right.Color.SetColor(Color.Black);
            }
            #endregion

            #region Manipulate Cells (merge).
            // merge cells
            foreach (var args in buildArgs.Where(ba => ba.Action == BuildAction.Merge))
            {
                using (var cells = args.Worksheet.Cells[args.FromRow, args.FromColumn, args.ToRow, args.ToColumn])
                {
                    cells.Merge = true;
                }
            }
            // Vertical Alignment body table.
            ws.Cells[startRowIdx - 1, machinePartName_ColIdx, toRowIndex, totalColumns_ColIdx].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // new line Cell.
            foreach (var cell in ws.Cells[startRowIdx, machinePartName_ColIdx, toRowIndex, standard_ColIdx])
            {
                if (cell.Value != null)
                    cell.Value = cell.Value.ToString().Replace("/", Environment.NewLine);
            }
            
            // wordwrap entire table.
            using (var AllCellsTables = ws.Cells[startRowIdx - 1, machinePartName_ColIdx, toRowIndex, totalColumns_ColIdx])
            {
                AllCellsTables.Style.WrapText = true;
                //AllCellsTables.AutoFitColumns();
            }
            //set height Row.
            for (int intRow = startRowIdx; intRow < toRowIndex; intRow++)
            {
                if (ws.Cells[intRow, machinePartName_ColIdx] != null && ws.Cells[intRow, machinePartName_ColIdx].Value != null)
                {
                    ws.Row(intRow).Height = SMCommon.MeasureTextHeight(ws.Cells[intRow, machinePartName_ColIdx].Value.ToString(), ws.Cells[intRow, machinePartName_ColIdx].Style.Font, 1);
                }
            }
            //set width column.
            ws.Column(machinePartName_ColIdx).Width = 25;
            ws.Column(contentMtn_ColIdx).Width = 25;
            ws.Column(toolMtn_ColIdx).Width = 25;
            ws.Column(methodMtn_ColIdx).Width = 20;
            ws.Column(standard_ColIdx).Width = 20;

            #endregion
        }
    }
}