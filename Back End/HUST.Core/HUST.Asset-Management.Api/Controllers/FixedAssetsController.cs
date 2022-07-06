using HUST.Core.Entities;
using HUST.Core.HUSTAttribute;
using HUST.Core.Interfaces.Respositories;
using HUST.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace HUST.Asset_Management.Api.Controllers
{

    public class FixedAssetsController : HUSTBaseController<FixedAsset>
    {
        IFixedAssetRepository _fixedAssetRepository;
        IFixedAssetService _fixedAssetService;

        public FixedAssetsController(IFixedAssetRepository fixedAssetRepository, IFixedAssetService fixedAssetService) : base(fixedAssetService, fixedAssetRepository)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _fixedAssetService = fixedAssetService;
        }

        /// <summary>
        /// Save dữ liệu (tài sản) từ file excel xuống CSDL
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản</param>
        /// <returns></returns>
        [HttpPost("SavingFile")]
        public IActionResult SaveFromExcels(List<FixedAsset> fixedAssets)
        {
            try
            {
                var res = _fixedAssetService.InsertDatasFromFile(fixedAssets);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Nhập khẩu tài sản từ file excel
        /// </summary>
        /// <param name="formFile">File Excel chứa dữ liệu nhập khẩu tài sản</param>
        /// <returns>Danh sách nhập khẩu tài sản từ file excel</returns>
        [HttpPost("ImportingExcel")]
        public IActionResult ImportFromExcels(IFormFile formFile)
        {
            try
            {
                var res = _fixedAssetService.Import(formFile);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xuất khẩu ra file excel
        /// </summary>
        /// <returns>Số bản ghi được xuất khẩu ra file excel</returns>
        [HttpPost("Excel")]
        public dynamic ExportExcel([FromBody] List<FixedAsset>? fixedAssets)
        {
            try
            {
                //var list = new List<FixedAsset>();
                ////Kiểm tra điều kiện danh sách Nguyên vật liệu muốn xuất file Excel(khác null)
                //if (fixedAssets.Count() > 0)
                //{
                //    list = fixedAssets.ToList();
                //}
                //else
                //{
                //    // query data from database  
                //    Task.Yield();
                //    //Lấy danh sách nguyên vật liệu dưới DB
                //    list = (List<FixedAsset>)_fixedAssetRepository.Get();
                //}

                ////Setup những trường sẽ được sẽ được xuất khẩu: 
                //var _objectForExport = new ObjectForExport();

                //var stream = new MemoryStream();

                ////Khai báo thông tin file excel
                //string excelName = $"Tai-san-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                ////config file excel
                //_objectForExport = _fixedAssetService.ConfigFileToExport(list, stream, excelName);

                ////trả về file excel
                //return File(stream, "application/octet-stream", _objectForExport.excelName);

                var stream = new MemoryStream();
                var fixedAsset = _fixedAssetRepository.Get();

                var properties = typeof(FixedAsset).GetProperties();
                using (var package = new ExcelPackage(stream))
                {

                    var workSheet = package.Workbook.Worksheets.Add("Danh Sách Tài Sản");

                    // Chình tiêu đề trong bảng.

                    // STT
                    workSheet.Cells[3, 1].Value = "STT";
                    workSheet.Cells[3, 1].Style.Font.Bold = true;
                    workSheet.Cells[3, 1].Style.Fill.SetBackground(Color.LightGray);
                    workSheet.Cells[3, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);


                    var column = 2;

                    foreach (var prop in properties)
                    {
                        var propHUSTExport = prop.GetCustomAttributes(typeof(HUSTPropExport), true);

                        //Xét các trường có được export không?
                        if (propHUSTExport.Length == 1)
                        {

                            // định dạng ngày/tháng/năm
                            if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                            {
                                workSheet.Column(column).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                            else
                            {
                                workSheet.Column(column).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }

                            workSheet.Cells[3, column].Value = (propHUSTExport[0] as HUSTPropExport).Name;
                            workSheet.Cells[3, column].Style.Font.Bold = true;
                            workSheet.Cells[3, column].Style.Fill.SetBackground(Color.LightGray);
                            workSheet.Cells[3, column].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);

                            column++;
                        }
                    }

                    // Chỉnh bản ghi vào hàng, cell
                    for (int i = 0; i < fixedAsset.Count; i++)
                    {
                        workSheet.Cells[i + 4, 1].Value = i + 1;
                        workSheet.Cells[i + 4, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);

                        int col = 2;

                        foreach (var prop in properties)
                        {
                            var propHUSTExport = prop.GetCustomAttributes(typeof(HUSTPropExport), true);

                            //Xét các trường có được export không?
                            if (propHUSTExport.Length == 1)
                            {

                                if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                                {
                                    var tmp = fixedAsset[i].GetType().GetProperty(prop.Name).GetValue(fixedAsset[i], null);
                                    workSheet.Cells[i + 4, col].Value = tmp == null ? "" : Convert.ToDateTime(tmp).ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    workSheet.Cells[i + 4, col].Value = fixedAsset[i].GetType().GetProperty(prop.Name).GetValue(fixedAsset[i], null);
                                }

                                workSheet.Cells.AutoFitColumns();
                                workSheet.Cells[i + 4, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);

                                col++;
                            }
                        }
                    }

                    // Chỉnh tiêu đề cho workSheet
                    workSheet.Cells["A1:O1"].Merge = true;
                    workSheet.Cells["A2:O2"].Merge = true;
                    workSheet.Cells[1, 1].Value = "DANH SÁCH TÀI SẢN";
                    workSheet.Cells[1, 1].Style.Font.Size = 16;
                    workSheet.Cells[1, 1].Style.Font.Bold = true;
                    workSheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    package.Save();
                }

                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí tạo mã tài sản mới
        /// </summary>
        /// <returns></returns>
        [HttpGet("NewFixedAssetCode")]
        public IActionResult GetNewAssetCode()
        {
            try
            {
                var newFixedAssetCode = _fixedAssetRepository.GetNewFixedAssetCode();
                return Ok(newFixedAssetCode);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí xóa nhiều
        /// </summary>
        /// <param name="fixedAssetIdList"></param>
        /// <returns></returns>
        [HttpDelete("DeleteMulti")]
        public IActionResult DeleteMulti([FromBody] Guid[] fixedAssetIdList)

        {
            try
            {
                var res = _fixedAssetRepository.DeleteMulti(fixedAssetIdList);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí phân trang và filter
        /// </summary>
        /// <param name="FixedAssetCategoryName"></param>
        /// <param name="DepartmentName"></param>
        /// <param name="FixedAssetFilter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("Filter")]
        public IActionResult Filter(string? FixedAssetCategoryName, string? DepartmentName, string? FixedAssetFilter = "", int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                // List data đã phân trang:
                var FilterList = _fixedAssetRepository.Filter(FixedAssetCategoryName, DepartmentName, FixedAssetFilter, pageIndex, pageSize);
                // Tổng số bản ghi khi chưa phân trang: 
                var FilterCount = _fixedAssetRepository.GetFixedAssetCount(FixedAssetCategoryName, DepartmentName, FixedAssetFilter);
                // trả về sữ liệu
                var res = new
                {
                    FilterList = FilterList,
                    FilterCount = FilterCount
                };

                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}

