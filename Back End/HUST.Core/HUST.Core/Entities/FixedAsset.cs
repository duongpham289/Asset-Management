using HUST.Core.HUSTAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.Entities
{
    public class FixedAsset : BaseEntity
    {
        /// <summary>
        /// Id tài sản 
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        [IsNotNullOrEmpty]
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Mã tài sản")]
        [MaxLength(50)]
        [HUSTPropExport(("Mã tài sản"))]
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Tên tài sản")]
        [MaxLength(255)]
        [HUSTPropExport(("Tên tài sản"))]
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Mã bộ phận sử dụng
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Mã bộ phận sử dụng")]
        [MaxLength(50)]
        public string DepartmentCode { get; set; }


        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Tên bộ phận sử dụng")]
        [MaxLength(255)]
        [HUSTPropExport(("Tên bộ phận sử dụng"))]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Id Loại tài sản
        /// </summary>
        public Guid? FixedAssetCategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Mã loại tài sản")]
        [MaxLength(50)]
        public string FixedAssetCategoryCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Tên loại tài sản")]
        [MaxLength(255)]
        [HUSTPropExport(("Tên loại tài sản"))]
        public string FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Ngày mua")]
        [MaxLength(255)]
        [HUSTPropExport(("Ngày mua"))]
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Ngày bắt đầu sử dụng")]
        [MaxLength(255)]
        [HUSTPropExport(("Ngày bắt đầu sử dụng"))]
        public DateTime? UseDate { get; set; }

        /// <summary>
        /// Lũy kế
        /// </summary>
        [FriendlyName("Lũy kế")]
        [MaxLength(255)]
        [HUSTPropExport(("Lũy kế"))]
        public decimal Accumulated { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Nguyên giá")]
        [MaxLength(255)]
        [HUSTPropExport(("Nguyên giá"))]
        public decimal Cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Số lượng")]
        [MaxLength(255)]
        [HUSTPropExport(("Số lượng"))]
        public int Quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Tỷ lệ hao mòn")]
        [MaxLength(255)]
        [HUSTPropExport(("Tỷ lệ hao mòn"))]
        public float DepreciationRate { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Giá trị hao mòn năm")]
        [MaxLength(255)]
        [HUSTPropExport(("Giá trị hao mòn năm"))]
        public float DepreciationValue { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi tài sản
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Năm bắt đầu theo dõi tài sản")]
        [MaxLength(255)]
        [HUSTPropExport(("Năm bắt đầu theo dõi tài sản"))]
        public int? TrackedYear { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Số năm sử dụng")]
        [MaxLength(255)]
        [HUSTPropExport(("Số năm sử dụng"))]
        public int LifeTime { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        [FriendlyName("Năm sử dụng")]
        [HUSTPropExport(("Năm sử dụng"))]
        public int? ProductionYear { get; set; }

        [NotMap]
        public bool? IsValid { get; set; } = true;

        [NotMap]
        public Dictionary<string, string>? ErrorValidateNotValid { get; set; } = new Dictionary<string, string>();

    }
}
