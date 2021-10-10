namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ИспользуемыеПрепаратыПредставление
    {
        [Key]
        [Column("Код оказанной услуги", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Код_оказанной_услуги { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Название { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Количество { get; set; }


        [Key]
        [Column("Стоимость препарата", Order = 3)]
        public decimal Стоимость_препарата { get; set; }
    }
}
