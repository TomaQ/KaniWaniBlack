using KaniWaniBlack.Data.DAL.Interfaces;
using KaniWaniBlack.Services.Models.KaniWani;
using KaniWaniBlack.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Services
{
    public class KaniWaniService : IKaniWaniService
    {
        private readonly IKaniWaniRepository _kaniWaniRepo;

        public KaniWaniService(IKaniWaniRepository kaniwaniRepo)
        {
            _kaniWaniRepo = kaniwaniRepo;
        }

        public List<UserKaniWaniList> TestGetKaniWaniVocabList(int userId)
        {
            return _kaniWaniRepo.TestGetWaniKaniVocabList(userId);
        }
    }
}