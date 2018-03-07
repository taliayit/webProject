using AutoLotto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoLotto.App_Start
{
    public class ExercisesConfig
    {
        public static void Register()
        {
            var exercisesData = System.IO.File.ReadAllLines(HttpContext.Current.Server.MapPath(@"App_Data/exercises.csv"));
            var workoutsData = System.IO.File.ReadAllLines(HttpContext.Current.Server.MapPath(@"App_Data/workouts.csv"));
            //var musclesData = System.IO.File.ReadAllLines(HttpContext.Current.Server.MapPath(@"App_Data/muscles.csv"));
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //musclessss
                db.Muscles.RemoveRange(db.Muscles);
                db.SaveChanges();

                Muscle m = new Muscle()
                {
                    Id = 1,
                    Name = "Butt"
                };
                db.Muscles.Add(m);
                db.SaveChanges();

                ///exercisessssss
                db.Exercises.RemoveRange(db.Exercises);
                db.SaveChanges();

                int i = 0;
                foreach (var row in exercisesData)
                {
                    if (i++ == 0) continue;
                    var values = row.Split(',');
                    Exercise e = new Exercise()
                    {
                        Id = values[0].AsInt(),
                        Name = values[1],
                        Description = values[2],
                        Time = values[3].AsInt(),
                        Level = values[4],
                        ImagePath = values[5]
                    };
                    string[] muscles = values[6].Split('-');
                    //todo: think about this disguisting shit
                    foreach (var ms in muscles)
                    {
                        Muscle mm = db.Muscles.FirstOrDefault(x => x.Id.ToString() == ms);
                        if (mm == null) continue;
                        e.Muscels.Add(mm);
                    }
                    db.Exercises.Add(e);

                }
                db.SaveChanges();

                //workouts
                db.Workouts.RemoveRange(db.Workouts);
                db.SaveChanges();

                i = 0;
                foreach (var row in workoutsData)
                {
                    if (i++ == 0) continue;
                    var values = row.Split(',');
                    Workout w = new Workout()
                    {
                        Id = values[0].AsInt(),
                        ImageName = values[1],
                        Description = values[2],
                        Time = values[3].AsInt(),
                    };
                    string[] exercises = values[4].Split('-');
                    //todo: think about this disguisting shit
                    foreach (var e in exercises)
                    {
                        Exercise ee = db.Exercises.FirstOrDefault(x => x.Id.ToString() == e);
                        if (ee == null) continue;
                        w.Exercises.Add(ee);
                    }
                    db.Workouts.Add(w);

                }
                db.SaveChanges();
            }
        }
    }
}