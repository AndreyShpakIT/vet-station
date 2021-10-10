namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ПитомцыПредставление
    {
        [Key]
        [Column("Код хозяина", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Код_хозяина { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Кличка { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string Порода { get; set; }

        [Key]
        [Column("Название вида", Order = 3)]
        [StringLength(20)]
        public string Название_вида { get; set; }

        public int? Возраст { get; set; }
    }
}
