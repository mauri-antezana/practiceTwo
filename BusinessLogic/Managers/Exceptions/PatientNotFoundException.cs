using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPB.BusinessLogic.Managers.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException() { }
        public PatientNotFoundException(string message) : base(message) { }

        public string GetMessageForLogs(string methodName)
        {
            return methodName + "(Patient not found) method error: " + Message;
        }
    }
}
