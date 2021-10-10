namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Препараты
    {
        [Key]
        [Column("Код препарата")]
        public int Код_препарата { get; set; }

        [Required]
        [StringLength(20)]
        public string Название { get; set; }

        [Column("Срок годности")]
        public int Срок_годности { get; set; }

        public decimal Стоимость { get; set; }
    }
}
