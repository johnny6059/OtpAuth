using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace OtpAuth {

    public partial class Register : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void ButtonFormSubmit_Click(object sender, EventArgs e) {
            if (!this.IsValid) return;

            var manager = this.Context.GetOwinContext().Get<MyUserManager>();
            var user = new MyUser() { UserName = this.UserNameTextBox.Text };
            var result = manager.Create(user, this.PasswordTextBox.Text);

            if (result.Succeeded) {
                this.LiteralMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
            }
            else {
                this.LiteralMessage.Text = result.Errors.FirstOrDefault();
            }
            this.MultiViewPage.SetActiveView(this.ViewResult);
        }
    }
}