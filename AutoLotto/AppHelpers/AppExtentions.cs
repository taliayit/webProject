using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using AutoLotto.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace AutoLotto.AppHelpers
{
    public static class IdentityExtensions
    {
        public static UserDataObject GetUserData(this IIdentity identity)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string userId = identity.GetUserId();
                var user = db.Users.FirstOrDefault(x => x.Id.ToString() == userId);
                string json = "{}";
                if (user.UserDataJson != null)
                    json = user.UserDataJson;
                return JsonConvert.DeserializeObject<UserDataObject>(json);
            }
        }
    }
}