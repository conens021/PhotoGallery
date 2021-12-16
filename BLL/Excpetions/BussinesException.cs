using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excpetions
{
    public class BussinesException : Exception
    {

        public BussinesException() : base() { }

        public BussinesException(string message,int StatusCode) : base(message) {
           this.StatusCode = StatusCode;
        }

        public int StatusCode { get; set; }

    }
}
