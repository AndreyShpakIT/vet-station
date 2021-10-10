using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using VetStation.Database;
using VetStation.Views.Service;

namespace VetStation.Viewmodels.Service
{
    class ServiceViewModel : NotifyObjectBase
    {
        private ObservableCollection<ОказанныеУслугиПредставление> services;
        public ObservableCollection<ОказанныеУслугиПредставление> Services
        {
            get => services;
            set => SetValue(ref services, value);
        }
        private ApplicationDb db;

        public ServiceViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.ОказанныеУслугиПредставление.Load();

                Services = db.ОказанныеУслугиПредставление.Local;
            }
            catch (Exception) { }
        }
    }
}
