using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UserAndPasswordModel : UserModel
    {
        public string HashedPassword { get; set; }

    }
}
