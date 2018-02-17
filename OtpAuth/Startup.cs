using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(OtpAuth.Startup))]

namespace OtpAuth {

    public class Startup {

        public void Configuration(IAppBuilder app) {
            // Add per-request objects
            app.CreatePerOwinContext<MyDbContext>(MyDbContext.Create);
            app.CreatePerOwinContext<MyUserManager>(MyUserManager.Create);

            // Enable cookie authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login.aspx"),
                LogoutPath = new PathString("/Logout.aspx")
            });

            // Use 2FA cookie
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
        }
    }
}