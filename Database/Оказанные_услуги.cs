namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Оказанные услуги")]
    public partial class Оказанные_услуги
    {
        [Key]
        [Column("Код записи")]
        public int Код_записи { get; set; }

        [Column("Код питомца")]
        public int Код_питомца { get; set; }

        [Column("Код услуги")]
        public int Код_услуги { get; set; }

        [Column("Дата оказания", TypeName = "date")]
        public DateTime Дата_оказания { get; set; }

        [Column("Время оказания")]
        [StringLength(10)]
        public string Время_оказания { get; set; }

        public virtual Питомцы Питомцы { get; set; }

        public virtual Услуги Услуги { get; set; }
    }
}
