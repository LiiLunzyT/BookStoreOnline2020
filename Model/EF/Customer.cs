namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Books = new HashSet<Book>();
        }

        [Required(ErrorMessage = "ID Không Hợp Lệ")]
        [DisplayName("Mã Khánh Hàng")]
        [StringLength(10)]
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "Địa Chỉ Không Hợp Lệ")]
        [DisplayName("Địa Chỉ")]
        [StringLength(200)]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Tên Khách Hàng Không Hợp Lệ")]
        [DisplayName("Tên Khánh Hàng")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Chưa Chọn Giới Tính")]
        [DisplayName("Giới Tính")]
        public bool? Gender { get; set; }

        [Required(ErrorMessage = "Ngày Sinh Không Hợp Lệ")]
        [DisplayName("Ngày Sinh")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birth { get; set; }


        [Required(ErrorMessage = "Số Điện Thoại Không Hợp Lệ")]
        [DisplayName("Số Điện Thoại")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email Không Hợp Lệ")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Chưa Nhập UsersID")]
        [StringLength(10)]
        public string UserID { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
