using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.Entities
{
    public class Department
    {

        /// <summary>
        /// Id bộ phận sử dụng
        /// Khóa chính
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        //public string? OrganizationId { get; set; }
    }
}
