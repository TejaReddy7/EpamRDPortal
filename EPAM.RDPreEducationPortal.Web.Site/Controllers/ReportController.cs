using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Utilities;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;
using EPAM.RDPreEducationPortal.Web.Site.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Table.PivotTable;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class ReportController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        // GET: Report
        public ActionResult Attendance()
        {
            return View();
        }
        // GET: Report
        public ActionResult Recruitment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Recruitment(int TestId)
        {
            var list = new List<RecruitmentChartsReportModel>();
            var model = new RecruitmentChartsReportModel();
            model.Category = "Technical";
            model.Selected = db.TechnicalAssessments.Where(x => x.EPAMFit == 1 && x.TestId == TestId).Count();
            model.Rejected = db.TechnicalAssessments.Where(x => x.EPAMFit == 2 && x.TestId == TestId).Count();
            model.NeedMoreEvaluation = db.TechnicalAssessments.Where(x => x.EPAMFit == 3 && x.TestId == TestId).Count();
            list.Add(model);
            model = new RecruitmentChartsReportModel();
            model.Category = "HR";
            model.Selected = db.HRAssessments.Where(x => x.Recommendation == true && x.TestId == TestId).Count();
            model.Rejected = db.HRAssessments.Where(x => x.Recommendation == false && x.TestId == TestId).Count();
            list.Add(model);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RecruitmentMaster()
        {
            return View();
        }
        public ActionResult RecruitmentExcelExport(int recruitmentId, string roundType)
        {
            try
            {
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@RecruitmentId", recruitmentId);
                parameters[1] = new SqlParameter("@RoundType", roundType);
                DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_RecruitmentReport", parameters);

                var memoryStream = new MemoryStream();
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Hiring Master Report_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year);
                    worksheet.Cells["A1"].LoadFromDataTable(ds.Tables[0], true, TableStyles.None);
                    worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(10).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(11).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(12).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(13).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(14).Style.WrapText = true;
                    worksheet.Column(15).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(16).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(17).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(19).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(20).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(21).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(22).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(23).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(24).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(25).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(26).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(27).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(28).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(29).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(30).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(31).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(33).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    //worksheet.Column(31).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkGreen);
                    worksheet.DefaultColWidth = 15;
                    worksheet.DefaultRowHeight = 15;
                    worksheet.Cells.AutoFitColumns(15, 15);
                    //return File(excelPackage.GetAsByteArray(), "application/octet-stream", "FileManager.xlsx");
                    /////
                    //ExcelWorksheets wsData = excelPackage.Workbook.Worksheets["Data"];
                    //ExcelTable tblData = worksheet.Tables[0];
                    //ExcelRange dataCells = worksheet.Cells[tblData.Address.Address];

                    //// pivot table
                    //ExcelRange pvtLocation = worksheet.Cells["B1"];
                    //string pvtName = "pvtSalesBySalesperson";
                    //ExcelPivotTable pivotTable = worksheet.PivotTables.Add(pvtLocation, dataCells, pvtName);

                    //// headers
                    //pivotTable.ShowHeaders = true;
                    //pivotTable.RowHeaderCaption = "Salesperson";

                    //// grand total
                    //pivotTable.ColumGrandTotals = true;
                    //pivotTable.GrandTotalCaption = "Total";

                    //// data fields are placed in columns
                    //pivotTable.DataOnRows = false;

                    //// style
                    //pivotTable.TableStyle = OfficeOpenXml.Table.TableStyles.Medium9;
                    ///////
                    Session["DownloadExcel_FileManager"] = excelPackage.GetAsByteArray();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ActionResult GenerateReport(int recruitmentId, string roundType)
        {
            List<ReportModel> list=new List<ReportModel>();
            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@RecruitmentId", recruitmentId);
            parameters[1] = new SqlParameter("@RoundType", roundType);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_RecruitmentReport", parameters);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        var model = new ReportModel();
                        model.CandidateName = dr["Candidate Name"].ToString();
                        model.EmailAddress = dr["Email Address"].ToString();
                        model.GraduationCollegeName = dr["Graduation College Name"].ToString();
                        model.TotalWeightage = dr["TotalWeightage"].ToString();
                        model.SelectionStatus = dr["Selection Status"].ToString();
                        model.TestLocation = dr["Test Location"].ToString();
                        model.TestName = dr["Test Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return View("_recruitmentReport", list);
        }
        public ActionResult Download()
        {

            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                return File(data, "application/octet-stream", "Hiring MasterSheet Results_v1_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".xlsx");
            }
            else
            {
                return new EmptyResult();
            }
        }
        public ActionResult RecruitmentWeightagesList(int recruitmentId)
        {
            ReportModel model = new ReportModel();
            return View("_recruitmentWeightagesList", model.RecruitmentWeightagesList(recruitmentId));
        }
    }
}