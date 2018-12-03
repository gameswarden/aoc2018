using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace aoc2018.Util
{
    public class Input
    {
        public static async Task<List<string>> GetInput(string url)
        {
            var result = new List<string>();

            var client = new HttpClient();
            var response = await client.GetStreamAsync(url);

            var reader = new StreamReader(response);
            
            while (!reader.EndOfStream)
            {
                result.Add(reader.ReadLine());
            }

            return result;
        }

        public static List<string> GetInputFromFile(string path)
        {
            var result = new List<string>();

            var reader = new StreamReader(path);

            while (!reader.EndOfStream)
            {
                result.Add(reader.ReadLine());
            }

            return result;
        }
    }
}
