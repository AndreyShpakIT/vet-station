namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ОказанныеУслугиПредставление
    {
        [Key]
        [Column("ФИО", Order = 0)]
        [StringLength(50)]
        public string ФИО { get; set; }

        [Key]
        [Column("Название вакцины", Order = 1)]
        [StringLength(20)]
        public string Название_вакцины { get; set; }


        [Key]
        [Column("Название услуги", Order = 2)]
        public int Название_услуги { get; set; }

        [Key]
        [Column("Дата оказания", Order = 3, TypeName = "date")]
        public DateTime Дата_оказания { get; set; }
    }
}
