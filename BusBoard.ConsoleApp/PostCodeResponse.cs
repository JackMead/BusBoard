using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class PostCodeResponse
    {
        public int status;
        public PostCodeInformation result;

        public PostCodeResponse(int status, PostCodeInformation result)
        {
            this.status = status;
            this.result = result;
        }
    }
}
