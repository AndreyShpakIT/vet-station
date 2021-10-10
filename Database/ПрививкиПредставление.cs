namespace VetStation.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ПрививкиПредставление
    {
        [Key]
        [Column("Код питомца", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Код_питомца { get; set; }

        [Key]
        [Column("Название вакцины", Order = 1)]
        [StringLength(20)]
        public string Название_вакцины { get; set; }

        [Key]
        [Column("Дата вакцинации", Order = 2, TypeName = "date")]
        public DateTime Дата_вакцинации { get; set; }

        [Key]
        [Column("Срок действия", Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Срок_действия { get; set; }
    }
}
