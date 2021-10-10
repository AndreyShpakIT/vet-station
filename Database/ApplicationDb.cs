using System.Data.Entity;

namespace VetStation.Database
{
    public partial class ApplicationDb : DbContext
    {
        public ApplicationDb()
            : base("name=ApplicationDb")
        {
        }

        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Вакцины> Вакцины { get; set; }
        public virtual DbSet<Виды_животных> Виды_животных { get; set; }
        public virtual DbSet<Клиенты> Клиенты { get; set; }
        public virtual DbSet<Оказанные_услуги> Оказанные_услуги { get; set; }
        public virtual DbSet<Питомцы> Питомцы { get; set; }
        public virtual DbSet<Препараты> Препараты { get; set; }
        public virtual DbSet<Прививки> Прививки { get; set; }
        public virtual DbSet<Услуги> Услуги { get; set; }
        public virtual DbSet<ПитомцыПредставление> ПитомцыПредставление { get; set; }
        public virtual DbSet<ОстатокПрепаратов> ОстатокПрепаратов { get; set; }
        public virtual DbSet<ПрепаратыПредставление> ПрепаратыПредставление { get; set; }
        public virtual DbSet<ПрививкиПредставление> ПрививкиПредставление { get; set; }
        public virtual DbSet<ОказанныеУслуги_ОстатокПрепаратов> ОказанныеУслуги_ОстатокПрепаратов { get; set; }
        public virtual DbSet<ОказанныеУслугиПредставление> ОказанныеУслугиПредставление { get; set; }
        public virtual DbSet<ИспользуемыеПрепаратыПредставление> ИспользуемыеПрепаратыПредставление { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Вакцины>()
                .HasMany(e => e.Прививки)
                .WithRequired(e => e.Вакцины)
                .HasForeignKey(e => e.Вид_вакцины)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Виды_животных>()
                .HasMany(e => e.Питомцы)
                .WithOptional(e => e.Виды_животных)
                .HasForeignKey(e => e.Вид_животного);

            modelBuilder.Entity<Клиенты>()
                .Property(e => e.Номер_телефона)
                .IsFixedLength();

            modelBuilder.Entity<Клиенты>()
                .HasMany(e => e.Питомцы)
                .WithRequired(e => e.Клиенты)
                .HasForeignKey(e => e.Код_хозяина)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Питомцы>()
                .HasMany(e => e.Оказанные_услуги)
                .WithRequired(e => e.Питомцы)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Питомцы>()
                .HasMany(e => e.Прививки)
                .WithRequired(e => e.Питомцы)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Услуги>()
                .HasMany(e => e.Оказанные_услуги)
                .WithRequired(e => e.Услуги)
                .WillCascadeOnDelete(false);
        }
    }
}
