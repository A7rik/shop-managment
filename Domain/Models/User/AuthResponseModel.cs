using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class AuthResponseModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public UserModel User { get; set; }  // Combine with user data if needed
    }

}
