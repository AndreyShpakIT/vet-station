namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ОказанныеУслуги_ОстатокПрепаратов
    {
        [Key]
        [Column("Код записи")]
        public int Код_записи { get; set; }

        [Column("Код препарата")]
        public int Код_препарата { get; set; }

        public int Количество { get; set; }

        [Column("Код оказанной услуги")]
        public int Код_оказанной_услуги { get; set; }
    }
}
