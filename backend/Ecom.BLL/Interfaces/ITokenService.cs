using Ecom.BLL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.BLL.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
