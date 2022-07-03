using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.Entities
{
    public class ObjectForExport
    {
        /// <summary>
        /// Danh sách nguyên vật liệu truyền vào
        /// </summary>
        public List<FixedAsset> listFixedAssets { get; set; }

        /// <summary>
        /// stream
        /// </summary>
        public MemoryStream stream { get; set; }

        /// <summary>
        /// Tên file excel để download
        /// </summary>
        public string excelName { get; set; }
    }
}
