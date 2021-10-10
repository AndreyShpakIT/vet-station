using System;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels.Medicine
{
    class NewMedicineCreatingViewModel : NotifyObjectBase
    {
        public event Action<Препараты> medicineAdded;
        private ApplicationDb db;

        private decimal cost;
        private int shelfLife;
        private string name;

        public decimal Cost
        {
            get => cost;
            set => SetValue(ref cost, value);
        }
        public int ShelfLife
        {
            get => shelfLife;
            set => SetValue(ref shelfLife, value);
        }
        public string Name
        {
            get => name;
            set => SetValue(ref name, value);
        }

        public RelayCommand AddCommand { get; set; }

        public NewMedicineCreatingViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.Препараты.Load();
            }
            catch (Exception) { }

            AddCommand = new RelayCommand((param) =>
            {
                try
                {
                    if (cost < 1)
                    {
                        MessageBox.Show("Введите корректную стоимость!");
                        return;
                    }
                    if (shelfLife < 1)
                    {
                        MessageBox.Show("Введите корректный срок годности!");
                        return;
                    }
                    Препараты med = new Препараты();
                    med.Название = name;
                    med.Срок_годности = shelfLife;
                    med.Стоимость = cost;

                    db.Препараты.Add(med);
                    db.SaveChanges();
                    medicineAdded?.Invoke(med);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
