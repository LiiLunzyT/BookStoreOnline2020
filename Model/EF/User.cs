namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Customers = new HashSet<Customer>();
        }

        [StringLength(10)]
        public string UserID { get; set; }

        [Required]
        [StringLength(28)]
        public string UserName { get; set; }

        [Required]
        [StringLength(28)]
        public string Password { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedByDate { get; set; }

        [Required]
        [StringLength(10)]
        public string RoleID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }

        public virtual Role Role { get; set; }
    }
}
