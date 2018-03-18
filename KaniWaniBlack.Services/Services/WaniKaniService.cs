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
        private readonly IGenericRepository<UserVocab> _UserVocabRepo;
        private readonly IHttpClientFactory _httpClientFactory;

        private const short WANIKANI_LEVELS = 60;

        public WaniKaniService(IGenericRepository<WaniKaniVocab> genericWKVRepo, IGenericRepository<UserVocab> genericUVRepo, IHttpClientFactory httpClientFac)
        {
            _WKVRepo = genericWKVRepo;
            _UserVocabRepo = genericUVRepo;
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
                    var vocab = JsonConvert.DeserializeObject<WaniKaniVocabWords>(request);

                    if (vocab.error != null)
                    {
                        Logger.LogError("In WaniKani Services: " + vocab.error.message);
                    }
                    else
                    {
                        didUpdate = ExtractVocabListFromRequest(vocab); //check if all 60 passed not just last one
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: this
            }

            return didUpdate;
        }

        public bool GetUserWaniKaniData(int userId, string apiKey)
        {
            bool didFinish = false;
            try
            {
                var client = _httpClientFactory.CreateClient(apiKey);
                string request = client.GetAsync("").Result.Content.ReadAsStringAsync().Result; //TODO: cleanup
                var vocab = JsonConvert.DeserializeObject<UserWaniKaniVocabWords>(request);

                if (vocab.error != null)
                {
                    Logger.LogError("In WaniKani Services: " + vocab.error.message);
                }
                else
                {
                    //TODO: get users general information here
                    didFinish = ExtractUserVocabDataFromRequest(vocab, userId);
                }
            }
            catch (Exception ex)
            {
                Logger.HandleException(ex);
            }

            return didFinish;
        }

        private bool ExtractUserVocabDataFromRequest(UserWaniKaniVocabWords words, int userId)
        {
            bool didUpdate = false;

            try
            {
                var userVocab = new List<UserVocab>();

                //The request returns all vocabulary for the users level,
                //so there could be some words not unlocked yet for that level
                foreach (var w in words.requested_information.general)
                {
                    if (w.user_specific != null)
                    {
                        int waniKaniVocabId = _WKVRepo.Get(x => x.Character == w.character).Id;

                        var wordToInsert = new UserVocab
                        {
                            UserId = userId,
                            WkvocabId = waniKaniVocabId,
                            IsBurned = w.user_specific.burned,
                            CreatedOn = DateTime.UtcNow,
                            UserSynonyms = w.user_specific.user_synonyms != null ? string.Join(",", w.user_specific.user_synonyms) : null //TODO: separate table
                        };

                        userVocab.Add(wordToInsert);
                    }
                }

                _UserVocabRepo.Add(userVocab.ToArray()); //TODO: not add duplicates
                didUpdate = true;
            }
            catch (Exception ex)
            {
                Logger.HandleException(ex);
            }

            return didUpdate;
        }

        private bool ExtractVocabListFromRequest(WaniKaniVocabWords words)
        {
            bool didUpdate = false;

            try
            {
                List<WaniKaniVocab> vocabList = new List<WaniKaniVocab>();

                vocabList = words.requested_information.Select(x =>
                    new WaniKaniVocab
                    {
                        Level = x.level,
                        Character = x.character,
                        Kana = x.kana,
                        Meaning = x.meaning
                    }).ToList();

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