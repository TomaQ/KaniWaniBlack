using KaniWaniBlack.Data.DAL.Interfaces;
using KaniWaniBlack.Data.Models;
using KaniWaniBlack.Helper.Services;
using KaniWaniBlack.Helper.Services.HttpFactory;
using KaniWaniBlack.Services.Models.WaniKani;
using KaniWaniBlack.Services.Services.Interfaces;
using Newtonsoft.Json;
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
        private readonly IHttpClientFactory _httpClientFactory;

        private const short WANIKANI_LEVELS = 60;

        public WaniKaniService(IGenericRepository<WaniKaniVocab> genericWKVRepo, IHttpClientFactory httpClientFac)
        {
            _WKVRepo = genericWKVRepo;
            _httpClientFactory = httpClientFac;
        }

        //TODO: return object not bool
        public bool UpdateWaniKaniVocabList(string apiKey)
        {
            bool didUpdate = false;
            try
            {
                string request;
                var client = _httpClientFactory.CreateClient(apiKey);
                for (int i = 1; i <= WANIKANI_LEVELS; i++)
                {
                    request = client.GetAsync(i.ToString()).Result.Content.ReadAsStringAsync().Result; //TODO: cleanup
                    VocabWord vocab = JsonConvert.DeserializeObject<VocabWord>(request);
                    if (vocab.error != null)
                    {
                        //TODO: this
                    }
                    else
                    {
                        didUpdate = ExtractVocabDataFromRequest(vocab); //check if all 60 passed not just last one
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: this
            }

            return didUpdate;
        }

        private bool ExtractVocabDataFromRequest(VocabWord words)
        {
            bool didUpdate = false;

            try
            {
                List<WaniKaniVocab> vocabList = new List<WaniKaniVocab>();
                foreach (var w in words.requested_information)
                {
                    WaniKaniVocab newWord = new WaniKaniVocab
                    {
                        Level = w.level,
                        Character = w.character,
                        Kana = w.kana,
                        Meaning = w.meaning
                    };

                    vocabList.Add(newWord);
                }

                _WKVRepo.Add(vocabList.ToArray());
                didUpdate = true;
            }
            catch (Exception ex)
            {
                //TODO: log
            }

            return didUpdate;
        }
    }
}