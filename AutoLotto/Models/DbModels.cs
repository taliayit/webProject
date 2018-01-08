using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoLotto.Models
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }

        public string ImageName { get; set; }
        public string Description { get; set; }
    }
}
