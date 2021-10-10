using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels.Vaccine
{
    class VaccineListViewModel : NotifyObjectBase
    {
        private ObservableCollection<Вакцины> list;
        public ObservableCollection<Вакцины> List
        {
            get => list;
            set => SetValue(ref list, value);
        }
        private ApplicationDb db;

        public VaccineListViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.Вакцины.Load();

                List = db.Вакцины.Local;
            }
            catch (Exception) { }

            SaveCommand = new RelayCommand((param) =>
            {
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }

        public RelayCommand SaveCommand { get; set; }
    }
}
