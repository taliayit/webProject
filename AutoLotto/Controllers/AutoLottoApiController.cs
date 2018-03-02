using AutoLotto.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
    public class AutoLottoApiController : ApiController
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
        public IHttpActionResult setDataForUser([FromBody] customChoice data)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return Ok();
            }
        }

    }

    public class customChoice
    {
        public int time { get; set; }
        public string diff { get; set; }
        public string[] areas { get; set; }
    }
}
