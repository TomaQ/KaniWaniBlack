using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Services.Interfaces
{
    public interface IWaniKaniService
    {
        bool UpdateWaniKaniVocabList(string apiKey);

        bool GetUserWaniKaniData(int userId, string apiKey);
    }
}