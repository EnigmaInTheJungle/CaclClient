using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaclClient
{
    public class CalcClient
    {
        HttpClient client = null;
        string url = null;

        public CalcClient(string servAddress)
        {
            client = new HttpClient();
            url = servAddress;             
        }

        public async Task<double> Calculate(double a, double b, char op)
        {
            var param = "a=" + a + "&b=" + b + "&op=" + op;
            string response = await client.GetStringAsync(url + "/?" + param);
            return Convert.ToDouble(response);
        }
    }
}
