namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Author")]
    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            Books = new HashSet<Book>();
        }
        [DisplayName("Mã Tác Giả")]
        [StringLength(10)]
        public string AuthorID { get; set; }

        [Required(ErrorMessage = "Tên Tác Giả Không Hợp Lệ")]
        [DisplayName("Tên Tác Giả")]
        [StringLength(100)]
        public string AuthorName { get; set; }

        [DisplayName("Số Điện Thoại")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Đường Dẫn Không Hớp Lệ")]
        [DisplayName("Đường Dẫn")]
        [StringLength(50)]
        public string Url { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
