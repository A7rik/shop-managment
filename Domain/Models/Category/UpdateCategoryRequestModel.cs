using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Category
{
    public class UpdateCategoryRequestModel : CreateCategoryRequestModel
    {
        public int Id { get; set; }

    }
}
