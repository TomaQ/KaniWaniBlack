using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Services.Services.Interfaces
{
    public interface ICryptoService
    {
        string GenerateSaltForUser();
        string GeneratePasswordHashForUser(string salt, string password);
    }
}
