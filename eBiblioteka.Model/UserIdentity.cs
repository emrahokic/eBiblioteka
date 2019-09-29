using System;
using System.Collections.Generic;
using System.Text;

namespace eBiblioteka.Model
{
    public class UserIdentity
    {

        public int UserID { get; set; }
        public List<string> Roles { get; set; }
        public UserIdentity(string userId, List<string> roles)
        {
            this.UserID = int.Parse(userId);
            this.Roles = roles;
        }
    }
}
