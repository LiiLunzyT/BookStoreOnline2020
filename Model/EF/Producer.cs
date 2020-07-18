namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Producer")]
    public partial class Producer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producer()
        {
            Books = new HashSet<Book>();
        }

        [StringLength(10)]
        public string ProducerID { get; set; }

        [Required(ErrorMessage = "Chưa Nhập Tên Nhà Xuất Bản")]
        [DisplayName("Tên Nhà Xuất Bản")]
        [StringLength(100)]
        public string ProducerName { get; set; }

        [Required(ErrorMessage = "Chưa Nhập Đỉa Chỉ")]
        [DisplayName("Địa Chỉ")]
        [StringLength(200)]
        public string ProducerAddress { get; set; }

        [Required(ErrorMessage = "Chưa Nhập Số Điện Thoại")]
        [DisplayName("Số Điện Thoại")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string FAX { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [Required(ErrorMessage = "Chưa Nhập URL")]
        [DisplayName("Đường Dẫn")]
        [StringLength(50)]
        public string Url { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
