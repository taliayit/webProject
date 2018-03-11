using AutoLotto.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AutoLotto.Controllers
{
    public class WeTrainController : ApiController
    {

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        [Route("api/getUserLoginDetails")]
        public IHttpActionResult getUserLoginDetails()
        {
            string userId = "";
            if (HttpContext.Current != null && HttpContext.Current.User != null
                    && HttpContext.Current.User.Identity.Name != null)
            {
                userId = HttpContext.Current.User.Identity.GetUserId();
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null) {
                    FbDetails d = db.fbDetails.FirstOrDefault(x => x.FbId == user.FbId);
                    return Ok(d);
                }
            }
                
            return Ok("can't find it :O");
            
        }

        [HttpGet]
        [Route("api/getUserLoginDetailsWithFbId/{FBId}")]
        public IHttpActionResult getUserLoginDetailsWithFbId(string FBId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.FbId == FBId);
                if (user != null)
                {
                    FbDetails d = db.fbDetails.FirstOrDefault(x => x.FbId == user.FbId);
                    return Ok(d);
                }
            }

            return Ok("can't find it :O");

        }

        [HttpPost]
        [Route("api/setDataForUser")]
        public IHttpActionResult setDataForUser([FromBody] UserDataObject data)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string userId = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(x => x.Id.ToString() == userId);
                user.UserDataJson = JsonConvert.SerializeObject(data);
                db.SaveChanges();
                
                var bestMatch = BestMatchForUser(db, data);

                return Ok(new {
                    bestMatch.Id,
                    bestMatch.VideoUrl,
                    bestMatch.Time
                });
            }
        }

        public Workout BestMatchForUser(ApplicationDbContext db, UserDataObject data)
        {
            Workout bestMatch = null;
            int bestScore = 0;
            List<Workout> workoutList = db.Workouts.ToList();
            List<Workout> copyList = workoutList.ToList();
            int diff=0;
            switch (data.diff)
            {
                case "difficultyEasy": diff = 1; break;
                case "difficultyMedium": diff = 2; break;
                case "difficultyLarge": diff = 3; break;

            }
            foreach (var w in workoutList)
            {
                if (w.Difficulty != diff || w.Time > data.time * 1.33 || w.Time < data.time * 0.66)
                {
                    copyList.Remove(w);
                }
            }
            foreach (var w in copyList)
            { 
                int score = w.Muscles.Sum( x=> data.areas.Contains(x.Id.ToString()) ? 1:0 );
                if (score >= bestScore)
                {
                    bestScore = score;
                    bestMatch = w;
                }
            }
            return bestMatch;
        }

    }

}
