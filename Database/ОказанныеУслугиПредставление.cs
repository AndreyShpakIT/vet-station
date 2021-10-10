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
        [Column("Код записи", Order = 0)]
        public int Код_записи { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string ФИО { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string Кличка { get; set; }

        [Key]
        [Column("Название услуги", Order = 3)]
        [StringLength(20)]
        public string Название_услуги { get; set; }

        [Key]
        [Column("Дата оказания", Order = 4, TypeName = "date")]
        public DateTime Дата_оказания { get; set; }
    }
}
