namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Виды животных")]
    public partial class Виды_животных
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Виды_животных()
        {
            Питомцы = new HashSet<Питомцы>();
        }

        [Key]
        [Column("Код вида")]
        public int Код_вида { get; set; }

        [Column("Название вида")]
        [Required]
        [StringLength(20)]
        public string Название_вида { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Питомцы> Питомцы { get; set; }
    }
}
