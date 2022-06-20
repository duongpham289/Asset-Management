using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUST.Core.Exceptions
{
    public class HUSTException : Exception
    {
        public string ErrorMsg;
        IDictionary ErrorData;
        public HUSTException(string errorMsg, List<string> validateErrorMsg = null)
        {
            ErrorMsg = errorMsg;
            ErrorData = new Dictionary<string, List<string>>();
            ErrorData.Add("data", validateErrorMsg);
        }
        public override string Message => this.ErrorMsg;
        public override IDictionary Data => this.ErrorData;
    }
}
