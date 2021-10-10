namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ОстатокПрепаратов
    {
        [Key]
        [Column("Код записи")]
        public int Код_записи { get; set; }

        [Column("Дата привоза", TypeName = "date")]
        public DateTime Дата_привоза { get; set; }

        public int Количество { get; set; }

        [Column("Код препарата")]
        public int Код_препарата { get; set; }
    }
}
