using HUST.Core.Entities;
using HUST.Core.Exceptions;
using HUST.Core.Interfaces.Respositories;
using HUST.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.Services
{
    public class FixedAssetService : BaseService<FixedAsset>, IFixedAssetService
    {
        IEPPLusAppService _iEPPLusAppService;
        IFixedAssetRepository _fixedAssettRepository;
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository, IEPPLusAppService iEPPLusAppService) : base(fixedAssetRepository)
        {
            _fixedAssettRepository = fixedAssetRepository;
            _iEPPLusAppService = iEPPLusAppService;
        }

        /// <summary>
        /// Nhập khẩu tài sản từ file excel
        /// </summary>
        /// <param name="formFile">File excel</param>
        /// <returns>Thông tin danh sách tài sản từ file excel</returns>
        public async Task<List<FixedAsset>> Import(IFormFile formFile)
        {
            //Thực hiện validate dữ liệu 
            if (formFile == null || formFile.Length <= 0)
            {
                throw new HUSTException("Tệp dữ liệu trống! Vui lòng kiểm tra lại");
            }
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new HUSTException("Tệp dữ liệu phải có định dạng là .xls hoặc .xlsx");
            }
            var fixedAssets = new List<FixedAsset>();

            //Thực hiện đọc file excel và trả về danh sách nguyên vật liệu
            List<FixedAsset> fixedAssetFile = await _iEPPLusAppService.ReadFileExcelToGetMaterials(formFile);

            return fixedAssetFile;
        }

        /// <summary>
        /// Xử lý sự kiện config thông tin file Excel
        /// </summary>
        /// <param name="list">Danh sách nguyên vật liệu</param>
        /// <param name="stream">Stream</param>
        /// <param name="excelName">Tên file Excel</param>
        /// <returns>thông tin config file</returns>
        public ObjectForExport ConfigFileToExport(List<FixedAsset> list, MemoryStream stream, string excelName)
        {
            var _objectForExport = new ObjectForExport();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;

            _objectForExport.excelName = excelName;

            return _objectForExport;
        }
    }
}
