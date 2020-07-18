namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [Required(ErrorMessage = "Mã không hợp lệ")]
        [DisplayName("Mã")]
        [StringLength(10)]
        public string CategoryID { get; set; }

        [Required(ErrorMessage = "Tên không hợp lệ")]
        [DisplayName("Tên Thể Loại")]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Mô tả")]
        public string CategoryDescription { get; set; }

        [Required(ErrorMessage = "url không hợp lệ")]
        [DisplayName("Url")]
        [StringLength(50)]
        public string Url { get; set; }

        [Required(ErrorMessage = "Nhóm không hợp lệ")]
        [DisplayName("Nhóm")]
        [StringLength(10)]
        public string CateGroupID { get; set; }

        public virtual CategoryGroup CategoryGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
