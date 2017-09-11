using System.Net;

namespace BusBoard.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var busBoard = new BusFinder();
            //busBoard.Setup()/*;*/
        }

        
    }
}
