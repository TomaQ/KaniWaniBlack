using KaniWaniBlack.Data.DAL.Interfaces;
using KaniWaniBlack.Data.Models;
using KaniWaniBlack.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks; //TODO: clean up usings

namespace KaniWaniBlack.Services.Services
{
    public class WaniKaniService : IWaniKaniService
    {
        private static HttpClient _client = new HttpClient();
        private readonly IGenericRepository<WaniKaniVocab> _WKVRepo;

        public WaniKaniService(IGenericRepository<WaniKaniVocab> genericWKVRepo)
        {
            _WKVRepo = genericWKVRepo;
        }

        public void Test(string apiKey)
        {
            string test = "";
            _client.BaseAddress = (new Uri(@"https://www.wanikani.com/api/v1.4/user/" + apiKey + "/vocabulary/")); //TODO: static string
            test = _client.GetAsync("55").Result.Content.ReadAsStringAsync().Result; //TODO: httphelper class

            if (test != "")
            {
                System.Diagnostics.Debug.WriteLine(test);
            }
        }
    }
}