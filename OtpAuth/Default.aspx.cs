using System;
using System.Text;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OtpSharp;

namespace OtpAuth {

    public partial class Default : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (this.User.Identity.IsAuthenticated) {
                this.MultiViewPage.SetActiveView(this.ViewAuthenticated);
                this.LiteralWelcome.Text = string.Format(this.LiteralWelcome.Text, this.User.Identity.GetUserName());

                if (!this.IsPostBack) {
                    // Get current user
                    var currentUserId = this.User.Identity.GetUserId();
                    var manager = this.Context.GetOwinContext().Get<MyUserManager>();
                    var otpEnabled = manager.GetTwoFactorEnabled(currentUserId);
                    if (otpEnabled) {
                        // OTP is currently enabled
                        this.MultiViewOtp.SetActiveView(this.ViewOtpEnabled);
                    }
                    else {
                        // OTP is currently disabled
                        this.MultiViewOtp.SetActiveView(this.ViewOtpDisabled);
                    }
                }
            }
        }

        protected void OtpEnableButton_Click(object sender, EventArgs e) {
            var userName = this.User.Identity.GetUserName();

            // Generate OTP secret and store in hidden field
            var secret = KeyGeneration.GenerateRandomKey(20);
            this.HiddenSecret.Value = Base32.Base32Encoder.Encode(secret);
            this.LiteralSecret.Text = PrettyFormatSecret(this.HiddenSecret.Value);

            // Generate QR code to setup authenticator app
            var setupUrl = KeyUrl.GetTotpUrl(secret, "OTP Demo/" + userName) + "&issuer=ASP.NET%20Identity%20OTP%20demo";
            var qrUrl = "https://chart.googleapis.com/chart?cht=qr&chs=250x250&chld=L|0&chl=" + HttpUtility.UrlEncode(setupUrl);
            this.QrImage.ImageUrl = qrUrl;

            // Show form to enable
            this.MultiViewOtp.SetActiveView(this.ViewOtpSetup);
        }

        protected void OtpDisableButton_Click(object sender, EventArgs e) {
            // Get current user
            var currentUserId = this.User.Identity.GetUserId();
            var manager = this.Context.GetOwinContext().Get<MyUserManager>();

            // Disable OTP
            var result = manager.SetTwoFactorEnabled(currentUserId, enabled: false);
            if (result != IdentityResult.Success) throw new Exception(string.Join(", ", result.Errors));

            // Clear OTP secret
            var user = manager.FindById(currentUserId);
            user.OtpSecret = null;
            result = manager.Update(user);
            if (result != IdentityResult.Success) throw new Exception(string.Join(", ", result.Errors));

            // Redirect to self
            this.Response.Redirect("/Default.aspx");
        }

        protected void OtpSetupButton_Click(object sender, EventArgs e) {
            if (!this.IsValid) return;

            // Read supplied code and secret
            var code = this.CodeTextBox.Text;
            var secret = Base32.Base32Encoder.Decode(this.HiddenSecret.Value);

            // Validate code
            long timeStepMatched;
            var otp = new Totp(secret);
            var verifyResult = otp.VerifyTotp(code, out timeStepMatched, new VerificationWindow(1, 1));
            if (!verifyResult) {
                // Invalid code
                this.MultiViewOtp.SetActiveView(this.ViewOtpSetupFailed);
                return;
            }
            else {
                // Valid code - get and update current user
                var currentUserId = this.User.Identity.GetUserId();
                var manager = this.Context.GetOwinContext().Get<MyUserManager>();

                // Enable OTP
                var result = manager.SetTwoFactorEnabled(currentUserId, enabled: true);
                if (result != IdentityResult.Success) throw new Exception(string.Join(", ", result.Errors));

                // Set OTP secret
                var user = manager.FindById(currentUserId);
                user.OtpSecret = this.HiddenSecret.Value;
                result = manager.Update(user);
                if (result != IdentityResult.Success) throw new Exception(string.Join(", ", result.Errors));

                // Redirect to self
                this.Response.Redirect("/Default.aspx");
            }
        }

        private static string PrettyFormatSecret(string secret) {
            if (secret == null) throw new ArgumentNullException("secret");
            if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "secret");

            var sb = new StringBuilder();
            for (int i = 0; i < secret.Length; i++) {
                sb.Append(secret[i]);
                if ((i + 1) % 4 == 0) sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}