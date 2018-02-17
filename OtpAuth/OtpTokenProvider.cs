using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OtpSharp;

namespace OtpAuth {

    public class OtpTokenProvider : IUserTokenProvider<MyUser, string> {

        public Task<string> GenerateAsync(string purpose, UserManager<MyUser, string> manager, MyUser user) {
            // This provider does not actually generate anything
            return Task.FromResult<string>(null);
        }

        public Task NotifyAsync(string token, UserManager<MyUser, string> manager, MyUser user) {
            // This provider does not actually generate anything, so there is nothing to send to user
            return Task.FromResult(true);
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<MyUser, string> manager, MyUser user) {
            if (string.IsNullOrEmpty(user.OtpSecret)) throw new InvalidOperationException("OTP authentication not setup for this user");

            // Create instance of TOTP generator with given secret
            var otp = new Totp(Base32.Base32Encoder.Decode(user.OtpSecret));

            // Validate supplied OTP (allow 1 code before and 1 after to accommodate time skew)
            long timeStepMatched;
            var result = otp.VerifyTotp(token, out timeStepMatched, new VerificationWindow(1, 1));
            return Task.FromResult(result);
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<MyUser, string> manager, MyUser user) {
            // Check if this user has enabled OTP auth
            var result = !string.IsNullOrEmpty(user.OtpSecret);
            return Task.FromResult(result);
        }
    }
}