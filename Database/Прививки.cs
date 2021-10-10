namespace VetStation.Database
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Прививки
    {
        [Key]
        [Column("Код записи")]
        public int Код_записи { get; set; }

        [Column("Дата вакцинации", TypeName = "date")]
        public DateTime Дата_вакцинации { get; set; }

        [Column("Вид вакцины")]
        public int Вид_вакцины { get; set; }

        [Column("Код питомца")]
        public int Код_питомца { get; set; }

        public virtual Вакцины Вакцины { get; set; }

        public virtual Питомцы Питомцы { get; set; }
    }
}
