using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasmartTest.Data.Entities
{
    public class Equipment
    {
        public Guid Id { get; set; }

        [Required()]
        [MaxLength(100)]
        public string TypeOne { get; set; }

        [MaxLength(100)]
        public string TypeTwo { get; set; }

        [MaxLength(100)]
        public string TypeThree { get; set; }

        [Required()]
        public int Width { get; set; }

        [Required()]
        public int Hight { get; set; }

    }
}
