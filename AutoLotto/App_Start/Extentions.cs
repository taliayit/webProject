using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using AutoLotto.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace AutoLotto.App_Start
{
    public static class Extentions
    {
        public static double AsDouble(this string s) {
            try
            {
                return double.Parse(s);
            }
            catch (Exception) { return 0; }
        }

        public static int AsInt(this string s)
        {
            try
            {
                return int.Parse(s);
            }
            catch (Exception) { return 0; }
        }

        public static string GetNameFromFB(this IIdentity i)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string id = i.GetUserId();
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    FbDetails d = db.fbDetails.FirstOrDefault(x => x.FbId == user.FbId);
                    var json = JsonConvert.DeserializeObject<dynamic>(d.DetailsJson);
                    return json.Name;
                }
            }
            return "";
        }

        public static string GetUserNameWhichIsEiran(this IIdentity i)
        {
            return "Eiran";
        }
    }
}