using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;
using VetStation.Views.Medicine;

namespace VetStation.Viewmodels.Medicine
{
    class MedicineCreatingViewModel : NotifyObjectBase
    {
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-5);
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(15);


        public event Action medicineAdded;
        private ApplicationDb db;

        private Препараты kind;
        private DateTime date = DateTime.Now;
        private int amount;

        private ObservableCollection<Препараты> kinds;


        public Препараты Kind
        {
            get => kind;
            set => SetValue(ref kind, value);
        }
        public DateTime Date
        {
            get => date;
            set => SetValue(ref date, value);
        }
        public int Amount
        {
            get => amount;
            set => SetValue(ref amount, value);
        }
        public ObservableCollection<Препараты> Kinds
        {
            get => kinds;
            set => SetValue(ref kinds, value);
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand AddMedicineCommand { get; set; }

        public MedicineCreatingViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.Препараты.Load();

                Kinds = new ObservableCollection<Препараты>(db.Препараты);
            }
            catch(Exception) { }

            AddCommand = new RelayCommand((param) =>
            {
                try
                {
                    if (amount < 1)
                    {
                        MessageBox.Show("Введите корректное количество!");
                        return;
                    } 
                    ОстатокПрепаратов med = new ОстатокПрепаратов();
                    med.Дата_привоза = date;
                    med.Код_препарата = kind.Код_препарата;
                    med.Количество = amount;

                    db.ОстатокПрепаратов.Add(med);
                    db.SaveChanges();
                    medicineAdded?.Invoke();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });

            AddMedicineCommand = new RelayCommand((param) =>
            {
                try
                {
                    NewMedicineCreatingWindow window = new NewMedicineCreatingWindow();
                    NewMedicineCreatingViewModel vm = new NewMedicineCreatingViewModel();
                    vm.medicineAdded += (med) =>
                    {
                        Kinds.Add(med);
                        window.Close();
                    };
                    window.DataContext = vm;
                    window.Show();
                } 
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
