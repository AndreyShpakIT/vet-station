namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Услуги
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Услуги()
        {
            Оказанные_услуги = new HashSet<Оказанные_услуги>();
        }

        [Key]
        [Column("Код услуги")]
        public int Код_услуги { get; set; }

        [Column("Название услуги")]
        [Required]
        [StringLength(20)]
        public string Название_услуги { get; set; }

        public decimal Стоимость { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Оказанные_услуги> Оказанные_услуги { get; set; }
    }
}
