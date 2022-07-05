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
        IDepartmentRepository _iDepartmentRepository;
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository, IDepartmentRepository iDepartmentRepository, IEPPLusAppService iEPPLusAppService) : base(fixedAssetRepository)
        {
            _fixedAssettRepository = fixedAssetRepository;
            _iEPPLusAppService = iEPPLusAppService;
            _iDepartmentRepository = iDepartmentRepository;
        }

        public List<FixedAsset> InsertDatasFromFile(List<FixedAsset> fixedAssetsFromClient)
        {
            var fixedAssetListIsValid = new List<FixedAsset>();
            var fixedAssetListIsNotValid = new List<FixedAsset>();

            foreach (var fixedAsset in fixedAssetsFromClient)
            {
                if (_fixedAssettRepository.GetByName(fixedAsset.FixedAssetName) != null && _iDepartmentRepository.GetByName(fixedAsset.DepartmentName) != null)
                {
                    _fixedAssettRepository.GetByName(fixedAsset.FixedAssetName).DepartmentId = fixedAsset.DepartmentId;
                }
                else if (fixedAsset.DepartmentName != null)
                {
                    Department newDepartment = new Department();
                    newDepartment.DepartmentName = fixedAsset.DepartmentName;
                    _iDepartmentRepository.Insert(newDepartment);
                }
                ValidateAfterInsertToDB(fixedAsset, fixedAssetsFromClient);
                if(fixedAsset.IsValid == true)
                {
                    fixedAssetListIsValid.Add(fixedAsset);
                }
                else
                {
                    fixedAssetListIsNotValid.Add(fixedAsset);
                }
            }
            //Thêm những danh sách nguyên vật thỏa mãn yêu cầu
            foreach (var fixedAssetIsValid in fixedAssetListIsValid)
            {
                _fixedAssettRepository.Insert(fixedAssetIsValid);
            }
            return fixedAssetListIsNotValid;
        }

        private FixedAsset ValidateAfterInsertToDB(FixedAsset fixedAssetToValidate, List<FixedAsset> fixedAssetsFile)
        {
            //validate dữ liệu
            _error = new Dictionary<string, string>();
            //Validate thông tin file excel

            base.ValidationObject(fixedAssetToValidate);

            if (_error.Count() > 0)
            {
                fixedAssetToValidate.IsValid = false;
                fixedAssetToValidate.ErrorValidateNotValid = _error;
            }
            if (fixedAssetToValidate.FixedAssetCode != null && CheckCodeExistInFile(fixedAssetToValidate.FixedAssetCode, fixedAssetToValidate.FixedAssetId, fixedAssetsFile))
            {
                fixedAssetToValidate.IsValid = false;
                _error.Add("FixedAssetCode", $"Mã tài sản không được phép trùng lặp trong file");
                fixedAssetToValidate.ErrorValidateNotValid = _error;
            }
            if (_fixedAssettRepository.CheckCodeExist(fixedAssetToValidate.FixedAssetCode) == true)
            {
                fixedAssetToValidate.IsValid = false;
                _error.Add("FixedAssetCode", $"Mã tài sản đã tồn tại");
                fixedAssetToValidate.ErrorValidateNotValid = _error;
            }

            return fixedAssetToValidate;
        }

        public bool CheckCodeExistInFile(string fixedAssetCodeToCheck, Guid fixedAssetIdToCheck, List<FixedAsset> fixedAssetsFile)
        {
            //return materialsInFile.Select(item => item.MaterialCode).Contains(materialCodeToCheck);
            foreach (var fixedAsset in fixedAssetsFile)
            {
                if (fixedAsset.FixedAssetCode == fixedAssetCodeToCheck && fixedAsset.FixedAssetId != fixedAssetIdToCheck)
                {
                    return true;
                }
            }
            return false;
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
