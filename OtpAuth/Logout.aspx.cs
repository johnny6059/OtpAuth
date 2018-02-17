using System;
using System.Web;

namespace OtpAuth {

    public partial class Logout : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }
    }
}