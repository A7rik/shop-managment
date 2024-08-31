using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UpdateUserRoleByEmailModel
    {
        public string Email { get; set; }
        public int RoleID { get; set; }
    }
}
