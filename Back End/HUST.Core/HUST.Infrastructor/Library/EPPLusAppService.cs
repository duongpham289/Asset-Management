using HUST.Core.Entities;
using HUST.Core.Interfaces.Respositories;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Infrastructor.Library
{
    public class EPPLusAppService : IEPPLusAppService
    {
        public async Task<List<FixedAsset>> ReadFileExcelToGetMaterials(IFormFile formFile)
        {

            var fixedAssets = new List<FixedAsset>();
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    //2. Thực hiện build thành danh sách nguyên vật liệu với các dữ liệu đã đọc được trong tệp
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var fixedAsset = new FixedAsset();
                        //Xử lý thông tin các trường dữ liệu

                        //Thêm Id mới cho từng đối tượng
                        fixedAsset.FixedAssetId = Guid.NewGuid();
                        ////Gán mã ĐVT
                        //material.UnitId = Guid.Parse("153105e0-6702-29ed-7632-03c92da59c43");

                        //Duyệt từng đối tượng(row) trong file excel
                        fixedAsset.FixedAssetCode = worksheet.Cells[row, 2].Value?.ToString();
                        fixedAsset.FixedAssetName = worksheet.Cells[row, 3].Value?.ToString();
                        fixedAsset.DepartmentCode = worksheet.Cells[row, 4].Value?.ToString();
                        fixedAsset.DepartmentName = worksheet.Cells[row, 5].Value?.ToString();
                        fixedAsset.FixedAssetCategoryCode = worksheet.Cells[row, 6].Value?.ToString();
                        fixedAsset.FixedAssetCategoryName = worksheet.Cells[row, 7].Value?.ToString();

                        fixedAssets.Add(fixedAsset);
                    }
                }
                //Danh sách  trong file excel
                return fixedAssets;
            }
        }
    }
}
