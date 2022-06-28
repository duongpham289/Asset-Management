using HUST.Core.Entities;
using HUST.Core.Interfaces.Respositories;
using HUST.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.Services
{
    public class FixedAssetService : BaseService<FixedAsset>, IFixedAssetService
    {
        IFixedAssetRepository _fixedAssettRepository;
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository) : base(fixedAssetRepository)
        {
            _fixedAssettRepository = fixedAssetRepository;
        }
    }
}
