using HUST.Core.Entities;
using HUST.Core.Interfaces.Respositories;
using HUST.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

