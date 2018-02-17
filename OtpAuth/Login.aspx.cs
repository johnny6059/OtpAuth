using System;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace OtpAuth {

    public partial class Login : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void ButtonFormSubmit_Click(object sender, EventArgs e) {
            if (!this.IsValid) return;

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            var userManager = this.Context.GetOwinContext().Get<MyUserManager>();
            var helper = new SignInHelper(userManager, authenticationManager);

            var result = helper.PasswordSignIn(this.UserNameTextBox.Text, this.PasswordTextBox.Text, isPersistent: false, shouldLockout: false).Result;
            switch (result) {
                case SignInStatus.Success:
                    this.Response.Redirect("~/Default.aspx");
                    break;

                case SignInStatus.RequiresTwoFactorAuthentication:
                    this.MultiViewPage.SetActiveView(this.ViewOtp);
                    break;

                default:
                    this.ModelState.AddModelError("", "Login failed. Invalid user name, password or account locked out.");
                    break;
            }
        }

        protected void ButtonOtpFormSubmit_Click(object sender, EventArgs e) {
            if (!this.IsValid) return;

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            var userManager = this.Context.GetOwinContext().Get<MyUserManager>();
            var helper = new SignInHelper(userManager, authenticationManager);

            var result = helper.TwoFactorSignIn("TOTP", this.CodeTextBox.Text, isPersistent: false, rememberBrowser: false).Result;
            if (result == SignInStatus.Success) {
                this.Response.Redirect("~/Default.aspx");
            }
            else {
                this.ModelState.AddModelError("", "Login failed. Invalid OTP or account locked out.");
            }
        }
    }
}