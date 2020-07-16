namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryGroup")]
    public partial class CategoryGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoryGroup()
        {
            Categories = new HashSet<Category>();
        }

        [Key]
        [Required(ErrorMessage = "Mã không hợp lệ")]
        [DisplayName("Mã nhóm")]
        [StringLength(10)]
        public string GroupID { get; set; }

        [Required(ErrorMessage = "Tên nhóm không hợp lệ")]
        [DisplayName("Tên nhóm")]
        [StringLength(100)]
        public string GroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }
    }
}
