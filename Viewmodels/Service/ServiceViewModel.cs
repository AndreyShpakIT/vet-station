using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using VetStation.Database;
using VetStation.Views.Service;

namespace VetStation.Viewmodels.Service
{
    class ServiceViewModel : NotifyObjectBase
    {
        // info
        private DateTime date;
        private string time;
        private Питомцы pet;
        private decimal sum;
        public decimal Sum
        {
            get => sum;
            set => SetValue(ref sum, value);
        }


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

        public ObservableCollection<Питомцы> Pets { get; set; }

        // Combo
        public ObservableCollection<Услуги> Servs { get; set; }
        private Услуги selectedServ;
        public Услуги SelectedServ
        {
            get => selectedServ;
            set => SetValue(ref selectedServ, value);
        }


        // Table        
        private ObservableCollection<ИспользуемыеПрепаратыПредставление> usingMeds;
        public ObservableCollection<ИспользуемыеПрепаратыПредставление> UsingMeds
        {
            get => usingMeds;
            set => SetValue(ref usingMeds, value);
        }

        private ObservableCollection<ОказанныеУслугиПредставление> services;
        public ObservableCollection<ОказанныеУслугиПредставление> Services
        {
            get => services;
            set => SetValue(ref services, value);
        }
        private ОказанныеУслугиПредставление selectedService;
        public ОказанныеУслугиПредставление SelectedService
        {
            get => selectedService;
            set
            {
                SetValue(ref selectedService, value);
                SetValues();
            }
        }

        private ApplicationDb db;

        public ServiceViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.ОказанныеУслугиПредставление.Load();
                db.Оказанные_услуги.Load();
                db.Услуги.Load();
                db.Питомцы.Load();
                db.ИспользуемыеПрепаратыПредставление.Load();

                Servs = db.Услуги.Local;
                Pets = db.Питомцы.Local;
               

                Services = db.ОказанныеУслугиПредставление.Local;
            }
            catch (Exception) { }

            ShowListCommand = new RelayCommand((param) => 
            {
                ServiceListWindow window = new ServiceListWindow();
                window.Show();
            });
            RefCommand = new RelayCommand((param) => 
            {
                try
                {
                    Services = new ObservableCollection<ОказанныеУслугиПредставление>(db.ОказанныеУслугиПредставление);
                    Servs = new ObservableCollection<Услуги>(db.Услуги);
                }
                catch { }
            });
            AddMedCommand = new RelayCommand((param) => 
            {
                MedicalSelectWindow window = new MedicalSelectWindow();
                MedicalSelectViewModel vm = new MedicalSelectViewModel();
                window.DataContext = vm;
                vm.SelectedMed += (m, a) => 
                {
                    window.Close();

                    Услуги serv = null;
                    try
                    {
                        serv = db.Услуги.Where(i => i.Название_услуги == selectedService.Название_услуги).FirstOrDefault();
                    }
                    catch { }

                    try
                    {
                        Препараты p = new Препараты();
                        p = db.Препараты.Where(i => i.Название == m.Название).FirstOrDefault();

                        ОказанныеУслуги_ОстатокПрепаратов row = new ОказанныеУслуги_ОстатокПрепаратов();
                        row.Код_оказанной_услуги = selectedService.Код_записи;
                        row.Код_препарата = p.Код_препарата;
                        row.Количество = a;

                        if (m.Количество - a < 0)
                        {
                            MessageBox.Show("Не достаточно количества!");
                            return;
                        }
                        if (m.Дата_привоза.AddDays(30 * p.Срок_годности) < DateTime.Now)
                        {
                            MessageBox.Show("Срок годности данного препарата истек!");
                            return;
                        }

                        db.ОказанныеУслуги_ОстатокПрепаратов.Add(row);
                        db.ОстатокПрепаратов.Where(r => r.Код_записи == m.Код_записи).FirstOrDefault().Количество -= a;
                        db.SaveChanges();
                        UsingMeds = GetMeds();
                        Calculate();
                        return;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                };
                window.Show();
            });
            ServAdding = new RelayCommand((param) => 
            {
                ServiceCreatingWindow window = new ServiceCreatingWindow();
                ServiceCreatingViewModel vm = new ServiceCreatingViewModel();
                window.DataContext = vm;
                vm.done += (m) =>
                {
                    try
                    {
                        window.Close();
                        db.Оказанные_услуги.Add(m);
                        db.SaveChanges();
                        Services = new ObservableCollection<ОказанныеУслугиПредставление>(db.ОказанныеУслугиПредставление);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                };
                window.Show();
            });
        }

        public RelayCommand ShowListCommand { get; set; }
        public RelayCommand RefCommand { get; set; }
        public RelayCommand AddMedCommand { get; set; }
        public RelayCommand ServAdding { get; set; }
        private void SetValues()
        {
            Оказанные_услуги s = GetServ();
            try
            {
                SelectedServ = db.Услуги.Where(i => i.Название_услуги == selectedService.Название_услуги).FirstOrDefault();
            }
            catch { }

            if (s != null)
            {
                Date = selectedService.Дата_оказания;
                Pet = db.Питомцы.Where(i => i.Код_питомца == s.Код_питомца).FirstOrDefault();
                Time = s.Время_оказания;
            }

            try
            {
                UsingMeds = GetMeds();
                Calculate();
            }
            catch { }
        }

        private ObservableCollection<ИспользуемыеПрепаратыПредставление> GetMeds() => new ObservableCollection<ИспользуемыеПрепаратыПредставление>(db.ИспользуемыеПрепаратыПредставление.Where(i => i.Код_оказанной_услуги == selectedService.Код_записи));
        private Оказанные_услуги GetServ()
        {
            try
            {
                return db.Оказанные_услуги.Where(i => i.Код_записи == selectedService.Код_записи).ToList().FirstOrDefault();
            }
            catch { }
            return null;
        }

        private void Calculate()
        {
            decimal sum = 0;
            foreach(var i in UsingMeds)
            {
                sum += i.Стоимость_препарата * i.Количество;
            }
            sum += SelectedServ?.Стоимость ?? 0;
            Sum = sum;
        }
    }
}
