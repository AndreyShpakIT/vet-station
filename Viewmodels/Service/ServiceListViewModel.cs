using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels.Service
{
    class ServiceListViewModel : NotifyObjectBase
    {
        private ObservableCollection<Услуги> services;
        public ObservableCollection<Услуги> Services
        {
            get => services;
            set => SetValue(ref services, value);
        }
        private ApplicationDb db;

        public ServiceListViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.Услуги.Load();

                Services = db.Услуги.Local;
            }
            catch (Exception) { }

            SaveCommand = new RelayCommand((param) =>
            {
                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }

        public RelayCommand SaveCommand { get; set; }
    }
}
