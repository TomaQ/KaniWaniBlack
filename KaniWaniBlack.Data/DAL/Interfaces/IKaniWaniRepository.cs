using KaniWaniBlack.Services.Models.KaniWani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Data.DAL.Interfaces
{
    public interface IKaniWaniRepository
    {
        List<UserKaniWaniList> TestGetWaniKaniVocabList(int userId);
    }
}