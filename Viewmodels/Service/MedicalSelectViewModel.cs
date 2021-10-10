using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels.Service
{
    class MedicalSelectViewModel : NotifyObjectBase
    {
        public event Action<ПрепаратыПредставление, int> SelectedMed;

        private ПрепаратыПредставление selected;
        public ПрепаратыПредставление Selected
        {
            get => selected;
            set => SetValue(ref selected, value);
        }

        private int amount;
        public int Amount
        {
            get => amount;
            set => SetValue(ref amount, value);
        }

        public ObservableCollection<ПрепаратыПредставление> Medicine { get; set; }
        private ApplicationDb db;

        public MedicalSelectViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.ПрепаратыПредставление.Load();
                Medicine = db.ПрепаратыПредставление.Local;
            }
            catch (Exception) { }

            SelectCommand = new RelayCommand((param) =>
            {
                if (selected == null)
                {
                    MessageBox.Show("Выберите препарат!");
                    return;
                }
                if (amount < 1)
                {
                    MessageBox.Show("Введите корректное количество!");
                    return;
                }
                SelectedMed?.Invoke(Selected, amount);
            });
        }

        public RelayCommand SelectCommand { get; set; }
    }
}
