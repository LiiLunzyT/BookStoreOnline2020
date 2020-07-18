namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Required(ErrorMessage = "Chưa Nhập Mã Đơn Hàng")]
        [DisplayName("Mã Đơn Hàng")]
        [StringLength(10)]
        public string OrderID { get; set; }


        [Required(ErrorMessage = "Chưa Nhập Địa Chỉ Giao Hàng")]
        [DisplayName("Địa Chỉ Giao Hàng")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Chưa Có Ngày Đặt Hàng")]
        [DisplayName("Ngày Đặt Hàng")]
        [Column(TypeName = "date")]
        public DateTime? OrderByDate { get; set; }


        [DisplayName("Tính Trạng")]
        [StringLength(50)]
        public string Status { get; set; }

        [DisplayName("Ghi Chú")]
        [Column(TypeName = "text")]
        public string Notes { get; set; }

        [DisplayName("Tổng tiền")]
        public int Total { get; set; }

        [Required(ErrorMessage = "Chưa Chọn Mã Khách Hàng")]
        [DisplayName("Mã Khách Hàng")]
        [StringLength(10)]
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "Chưa Chọn Hình Thức Thanh Toán")]
        [DisplayName("Hình Thức Thanh Toán")]
        [StringLength(10)]
        public string PaymentID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Payment Payment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
