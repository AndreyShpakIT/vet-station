namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Клиенты
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Клиенты()
        {
            Питомцы = new HashSet<Питомцы>();
        }

        [Key]
        [Column("Код клиента")]
        public int Код_клиента { get; set; }

        [Required]
        [StringLength(40)]
        public string ФИО { get; set; }

        [Column("Дата рождения", TypeName = "date")]
        public DateTime Дата_рождения { get; set; }

        [StringLength(30)]
        public string Адрес { get; set; }

        [Column("Номер телефона")]
        [StringLength(15)]
        public string Номер_телефона { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Питомцы> Питомцы { get; set; }
    }
}
