using Microsoft.EntityFrameworkCore;
using SaLes__APIs.Entity;
using SaLes__APIs.Serviecs.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SaLes__APIs.Services.Security
{
    public class RUser
    {
        private readonly SelasContext _context;

        public RUser(SelasContext context)
        {
            _context = context;
        }

        public async Task<PermissionPolicyUser> Get(UserLogin login)
        {
            var user = await _context.PermissionPolicyUser.FirstOrDefaultAsync(o => o.UserName == login.UserName);
            if (user == null)
            {
                return null; 
            }          
            PasswordCryptographerNew cryptographerNew = new PasswordCryptographerNew();
       

            var newpass = cryptographerNew.GenerateSaltedPassword(login.StoredPassword);

            var flag = cryptographerNew.AreEqual(user.StoredPassword ,  newpass);
            //var flag = cryptographerNew.AreEqual(user.StoredPassword, login.StoredPassword);
            if (flag == false)
            {
                return null;
            }

            return user; 
        }

    }
}
