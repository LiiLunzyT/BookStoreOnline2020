﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreOnline.Models
{
    [Serializable]
    public class CartItem
    {
        public Book book { get; set; }
        public int Quantity { get; set; }
    }
}