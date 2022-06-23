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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public int Insert(Department entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid id, Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
