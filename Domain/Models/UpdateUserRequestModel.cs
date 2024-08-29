using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UpdateUserRequestModel  : CreateUserRequestModel 
    {
        public int Id { get; set; }

    }
}
