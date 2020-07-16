using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreOnline.Common
{
    [Serializable]
    public class UserLogin
    {
        public String UserID { get; set; }
        public String UserName { get; set; }
        public String Role { get; set; }
    }
}