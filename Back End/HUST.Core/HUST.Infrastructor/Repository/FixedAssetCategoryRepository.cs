using HUST.Core.Entities;
using HUST.Core.Interfaces.Respositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Infrastructor.Repository
{
    public class FixedAssetCategoryRepository : BaseRepository<FixedAssetCategory>, IFixedAssetCategoryRepository
    {
        public FixedAssetCategoryRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }


        public int Insert(FixedAssetCategory entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid id, FixedAssetCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
