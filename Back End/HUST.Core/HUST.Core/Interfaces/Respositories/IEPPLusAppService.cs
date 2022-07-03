using HUST.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.Interfaces.Respositories
{
    public interface IEPPLusAppService
    {
        /// <summary>
        /// Thực hiện đọc file excel và trả về danh sách nguyên vật liệu
        /// </summary>
        /// <param name="formFile">File Excel truyền vào</para
        Task<List<FixedAsset>> ReadFileExcelToGetMaterials(IFormFile formFile);
    }
}
