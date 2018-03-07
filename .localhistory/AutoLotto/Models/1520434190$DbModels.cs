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
        public Workout()
        {
            Exercises = new HashSet<Exercise>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public int Difficulty { get; set; }
        public string VideoUrl { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }

    }

    public class Exercise
    {
        public Exercise()
        {
            Muscels = new HashSet<Muscle>();
            Workouts = new HashSet<Workout>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Muscle> Muscels { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
    }

    public class Muscle
    {
        public Muscle()
        {
            Exercises = new HashSet<Exercise>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
