namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Вакцины
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Вакцины()
        {
            Прививки = new HashSet<Прививки>();
        }

        [Key]
        [Column("Код вида")]
        public int Код_вида { get; set; }

        [Column("Название вакцины")]
        [Required]
        [StringLength(20)]
        public string Название_вакцины { get; set; }

        [Column("Срок действия")]
        public int Срок_действия { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Прививки> Прививки { get; set; }
    }
}
