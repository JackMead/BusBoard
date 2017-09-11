using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class PostCodeInformation
    {
        public string postCode;

        public decimal longitude;
        public decimal latitude;

        public PostCodeInformation(string postCode, decimal longitude, decimal latitude)
        {
            this.postCode = postCode;
            this.longitude = longitude;
            this.latitude = latitude;
        }
    }
}
