using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreOnline.Common
{
    [Serializable]
    public class CustomerLogin
    {
        public String UserID { get; set; }
        public String UserName { get; set; }
    }
}