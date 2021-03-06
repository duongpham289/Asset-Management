using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.HUSTAttribute
{
   
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class NotMap : Attribute
    {
    }

    /// <summary>
    /// Cờ Khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class PrimaryKey : Attribute
    {
    }

    /// <summary>
    /// Cờ check trốnh
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class IsNotNullOrEmpty : Attribute
    {
    }

    /// <summary>
    /// Cờ tên để hiển thị lên cảnh báo
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class FriendlyName : Attribute
    {
        public string Name;

        public FriendlyName(string name)
        {
            Name = name;
        }
    }


    /// <summary>
    /// Cờ độ dài kí tự
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class MaxLength : Attribute
    {
        public int Length;
        public MaxLength(int length)
        {
            Length = length;
        }
    }

    /// <summary>
    /// Thuộc tính xuất file excel
    /// </summary>
    /// CreatedBy: PHDUONG (03/09/2021)
    public class HUSTPropExport : Attribute
    {
        public readonly string Name;
        public HUSTPropExport(string name)
        {
            this.Name = name;
        }
    }
}
