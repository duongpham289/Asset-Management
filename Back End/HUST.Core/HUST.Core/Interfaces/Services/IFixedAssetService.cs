using HUST.Core.Entities;
using HUST.Core.Interfaces.Respositories;
using HUST.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.Interfaces.Services
{
    public interface IFixedAssetService : IBaseService<FixedAsset>
    {
        ObjectForExport ConfigFileToExport(List<FixedAsset> list, MemoryStream stream, string excelName);
        Task<List<FixedAsset>> Import(IFormFile formFile);
        List<FixedAsset> InsertDatasFromFile(List<FixedAsset> fixedAssetsFromClient);
    }
}
