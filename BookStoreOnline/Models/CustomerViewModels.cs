using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreOnline.Models
{
    public class CustomerViewModels
    {
    }

    public class CustomerLoginViewModel
    {
        [Required(ErrorMessage = "Tên tài khoản được bỏ trống")]
        [Display(Name = "Tài khoản")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [StringLength(100, ErrorMessage = "{0} ít nhất phải dài hơn {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }

    public class CustomerRegisterViewModel
    {
        [Required(ErrorMessage = "Tên tài khoản không được bỏ trống")]
        [StringLength(28, ErrorMessage = "{0} ít nhất phải dài hơn {2} ký tự.", MinimumLength = 6)]
        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [StringLength(100, ErrorMessage = "{0} ít nhất phải dài hơn {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được bỏ trống")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu phải trùng với mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được bỏ trống")]
        [StringLength(100)]
        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }
        
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được bỏ trống")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày sinh")]
        public DateTime Birth { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [StringLength(20)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
    }
}