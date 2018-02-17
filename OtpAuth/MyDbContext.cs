using Microsoft.AspNet.Identity.EntityFramework;

namespace OtpAuth {

    public class MyDbContext : IdentityDbContext<MyUser> {

        public MyDbContext()
            : base("DefaultConnection") {
        }

        public static MyDbContext Create() {
            return new MyDbContext();
        }
    }
}