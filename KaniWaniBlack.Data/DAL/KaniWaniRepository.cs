using KaniWaniBlack.Data.DAL.Interfaces;
using KaniWaniBlack.Data.Models;
using KaniWaniBlack.Helper.Services;
using KaniWaniBlack.Services.Models.KaniWani;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Data.DAL
{
    public class KaniWaniRepository : IKaniWaniRepository
    {
        internal KaniWaniBlackContext _context;

        public KaniWaniRepository(KaniWaniBlackContext aContext)
        {
            _context = aContext;
        }

        public List<UserKaniWaniList> TestGetWaniKaniVocabList(int userId)
        {
            var list = new List<UserKaniWaniList>();
            try
            {
                list = _context.Set<UserKaniWaniList>().FromSql("SELECT TOP 10 Level, Character, Kana, Meaning FROM vwUserKaniWaniVocabList WHERE UserId = " + userId).ToList();
            }
            catch (Exception ex)
            {
                Logger.HandleException(ex);
            }

            return list;
        }
    }
}