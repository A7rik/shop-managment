﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class UpdateProductRequestModel : CreateProductRequestModel
    {
        public int Id { get; set; }

    }
}
