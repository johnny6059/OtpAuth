using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace OtpAuth {

    public class MyUserManager : UserManager<MyUser> {

        public MyUserManager(IUserStore<MyUser> store)
            : base(store) {
        }

        public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context) {
            var manager = new MyUserManager(new UserStore<MyUser>(context.Get<MyDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MyUser>(manager) {
                AllowOnlyAlphanumericUserNames = false,
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator {
                RequiredLength = 6,
                // RequireNonLetterOrDigit = true,
                // RequireDigit = true,
                // RequireLowercase = true,
                // RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Configure TOTP support
            manager.RegisterTwoFactorProvider("TOTP", new OtpTokenProvider());

            return manager;
        }
    }
}