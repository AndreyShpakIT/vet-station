namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Питомцы
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Питомцы()
        {
            Оказанные_услуги = new HashSet<Оказанные_услуги>();
            Прививки = new HashSet<Прививки>();
        }

        [Key]
        [Column("Код питомца")]
        public int Код_питомца { get; set; }

        [Required]
        [StringLength(20)]
        public string Порода { get; set; }

        [Required]
        [StringLength(20)]
        public string Кличка { get; set; }

        public int? Возраст { get; set; }

        [Column("Код хозяина")]
        public int Код_хозяина { get; set; }

        [Column("Вид животного")]
        public int? Вид_животного { get; set; }

        public virtual Виды_животных Виды_животных { get; set; }

        public virtual Клиенты Клиенты { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Оказанные_услуги> Оказанные_услуги { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Прививки> Прививки { get; set; }
    }
}
