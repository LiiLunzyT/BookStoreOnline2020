namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
            Categories = new HashSet<Category>();
            Readers = new HashSet<Reader>();
            Customers = new HashSet<Customer>();
        }

        [StringLength(10)]
        public string BookID { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Tên")]
        [StringLength(200)]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Giá")]
        [DefaultValue(20000)]
        public int Price { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Giảm giá")]
        public int DiscountPercent { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Đã bán")]
        public int TotalSell { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Avatar")]
        [StringLength(50)]
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Ngày đăng")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateByDate { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Url")]
        [StringLength(50)]
        public string Url { get; set; }

        [DisplayName("Nhà XB")]
        [StringLength(100)]
        public string Publisher { get; set; }

        [DisplayName("Ngày XB")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublicByDate { get; set; }

        [DisplayName("Bìa")]
        [StringLength(50)]
        public string BookCover { get; set; }

        [DisplayName("Số trang")]
        public int? Pages { get; set; }

        [DisplayName("Mô tả")]
        [Column(TypeName = "text")]
        public string BookDescription { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Tác giả")]
        [StringLength(10)]
        public string AuthorID { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Nhà SX")]
        [StringLength(10)]
        public string ProducerID { get; set; }

        public virtual Author Author { get; set; }

        public virtual Producer Producer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reader> Readers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
