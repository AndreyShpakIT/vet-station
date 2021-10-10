using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels.Service
{
    class ServiceCreatingViewModel : NotifyObjectBase
    {
        public event Action<Оказанные_услуги> done;

        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-5);
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(15);

        private DateTime date = DateTime.Now;
        private string time;
        private Питомцы pet;
        private Услуги serv;

        public DateTime Date
        {
            get => date;
            set => SetValue(ref date, value);
        }
        public string Time
        {
            get => time;
            set => SetValue(ref time, value);
        }
        public Питомцы Pet
        {
            get => pet;
            set => SetValue(ref pet, value);
        }
        public Услуги Service
        {
            get => serv;
            set => SetValue(ref serv, value);
        }

        private ObservableCollection<Услуги> services;
        public ObservableCollection<Услуги> Services
        {
            get => services;
            set => SetValue(ref services, value);
        }
        public ObservableCollection<Питомцы> Pets { get; set; }


        private ApplicationDb db;

        public ServiceCreatingViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.Услуги.Load();
                db.Питомцы.Load();

                Services = db.Услуги.Local;
                Pets = db.Питомцы.Local;
            }
            catch (Exception) { }

            SaveCommand = new RelayCommand((param) =>
            {
                try
                {
                    Оказанные_услуги s = new Оказанные_услуги();
                    s.Время_оказания = Time;
                    s.Дата_оказания = Date;
                    s.Код_услуги = Service.Код_услуги;
                    s.Код_питомца = Pet.Код_питомца;
                    done?.Invoke(s);
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
