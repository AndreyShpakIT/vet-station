namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ПрепаратыПредставление
    {
        [Key]
        [Column("Код записи", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Код_записи { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Название { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Количество { get; set; }

        [Key]
        [Column("Дата привоза", Order = 3, TypeName = "date")]
        public DateTime Дата_привоза { get; set; }

        [Key]
        [Column("Срок годности", Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Срок_годности { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal Стоимость { get; set; }
    }
}
