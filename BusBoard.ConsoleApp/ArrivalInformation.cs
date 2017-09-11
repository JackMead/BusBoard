using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class ArrivalInformation
    {
        public string lineId;
        public string lineName;
        public int timeToStation;

        public ArrivalInformation(string id, string name, int timeToArrive)
        {
            lineId = id;
            lineName = name;
            timeToStation = timeToArrive;
        }


    }
}
