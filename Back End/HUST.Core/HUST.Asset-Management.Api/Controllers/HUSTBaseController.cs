using HUST.Core.Exceptions;
using HUST.Core.Interfaces.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUST.Asset_Management.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class MISABaseController<T> : ControllerBase
    {
        #region Fiels
        IBaseRepository<T> _baseRepository;
        #endregion

        #region Contructtor
        public MISABaseController( IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lí lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        // GET: api/<MISABaseController>
        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var entitties = _baseRepository.Get();
                return Ok(entitties);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lí lấy dữ liệu về theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        // GET api/<MISABaseController>/5
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            try
            {
                var entitties = _baseRepository.GetById(entityId);
                return Ok(entitties);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

 

        /// <summary>
        /// Xử lí xóa đối tượng theo Id
        /// </summary>
        /// <param name="entityId"> Id của đối tượng </param>
        /// <returns></returns>
        [HttpDelete("{entityId}")]
        public virtual IActionResult Delete(Guid entityId)
        {
            try
            {
                var res = _baseRepository.Delete(entityId);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #endregion 
        /// <summary>
        /// Xử lí lỗi Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns> Thông tin lỗi Exception </returns>
        /// CreatedBy: LTTUAN (09/05/2022)
        protected IActionResult HandleException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xấy ra vui lòng liên hệ MISA để được hỗ trợ",
                errorCode = "001",
                data = ex.Data
            };
            if (ex is HUSTException)
                return StatusCode(400, res); //Lỗi từ server trả về 500
            else
                return StatusCode(500, res);
        }
    }
}

