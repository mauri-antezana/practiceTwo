using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPB.BusinessLogic.Exceptions
{
    internal class PatientNotFoundException : Exception
    {
        public PatientNotFoundException() { }
        public PatientNotFoundException(string message) : base(message){ }

        public string GetMessage()
        {
            return "PatientNotFoundException: "+Message;
        }
    }
}
