﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentMigratorExample.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public string Category { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
