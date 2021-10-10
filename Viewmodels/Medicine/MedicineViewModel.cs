using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;
using VetStation.Views.Medicine;

namespace VetStation.Viewmodels.Medicine
{
    class MedicineViewModel : NotifyObjectBase
    {
        private ObservableCollection<ПрепаратыПредставление> medicine;
        public ObservableCollection<ПрепаратыПредставление> Medicine
        {
            get => medicine;
            set => SetValue(ref medicine, value);
        }
        private ApplicationDb db;

        public MedicineViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.ПрепаратыПредставление.Load();

                Medicine = db.ПрепаратыПредставление.Local;
            }
            catch (Exception) { }

            MedicineAdding = new RelayCommand((param) =>
            {
                try
                {
                    MedicineCreatingView window = new MedicineCreatingView();
                    MedicineCreatingViewModel vm = new MedicineCreatingViewModel();
                    vm.medicineAdded += () =>
                    {
                        Medicine = new ObservableCollection<ПрепаратыПредставление>(
                            db.ПрепаратыПредставление
                        );
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

        public RelayCommand MedicineAdding { get; set; }
    }
}
