using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using AutoLotto.Models;
using Microsoft.Owin.Security.Facebook;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace AutoLotto
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            FacebookAuthenticationOptions options = new FacebookAuthenticationOptions()
            {
                AppId = "141863326584652",
                AppSecret = "be80b504b6b59091b159872e5f948520",
                BackchannelHttpHandler = new FacebookBackChannelHandler(),
                UserInformationEndpoint = "https://graph.facebook.com/v2.8/me?fields=id,name,email,first_name,last_name",
                Scope = { "email", "public_profile" }
            };

            options.Provider = new FacebookAuthenticationProvider()
            {
                OnAuthenticated = (context) =>
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        var d = db.fbDetails.FirstOrDefault(x => x.FbId == context.Id);
                        if (d == null)
                            d = new FbDetails();
                        db.fbDetails.Add(d);

                        d.FbId = context.Id;
                        d.DetailsJson = JsonConvert.SerializeObject(new
                        {
                            Id = context.Id,
                            Name = context.Name,
                            AccessToken = context.AccessToken,
                            UserDetails = context.User
                        });
                        db.SaveChanges();
                    }
                    return Task.FromResult(0);
                }
            };

            options.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseFacebookAuthentication(options);

            app.UseGoogleAuthentication(
                 clientId: "607301369685-h4g7bq7jstcrniogl0erfgitucrf9ug6.apps.googleusercontent.com",
                 clientSecret: "sC1VmUset66kuMH5EZTlTD0m");
        }

        public class FacebookBackChannelHandler : HttpClientHandler
        {
            protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                System.Threading.CancellationToken cancellationToken)
            {
                // Replace the RequestUri so it's not malformed
                if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
                {
                    request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}