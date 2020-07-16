namespace Model.EF
{
    using System;
    using System.Collections.Generic;
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

        [StringLength(10)]
        public string OrderID { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OrderByDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }

        [Required]
        [StringLength(10)]
        public string CustomerID { get; set; }

        [Required]
        [StringLength(10)]
        public string PaymentID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Payment Payment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
