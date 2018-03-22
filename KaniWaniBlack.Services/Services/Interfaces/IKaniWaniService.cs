using KaniWaniBlack.Services.Models.KaniWani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Services.Interfaces
{
    public interface IKaniWaniService
    {
        List<UserKaniWaniList> TestGetKaniWaniVocabList(int userId);
    }
}