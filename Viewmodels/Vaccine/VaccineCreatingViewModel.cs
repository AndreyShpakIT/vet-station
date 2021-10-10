using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels.Vaccine
{
    class VaccineCreatingViewModel : NotifyObjectBase
    {
        public event Action<Прививки> vaccineAdded;
        private ApplicationDb db;

        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-365);
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(15);

        private Питомцы pet;
        private Вакцины kind;
        private DateTime vaccinationDate = DateTime.Now;

        private ObservableCollection<Вакцины> kinds;

        public Питомцы Pet 
        {
            get => pet;
            set => SetValue(ref pet, value);
        }

        public Вакцины Kind
        {
            get => kind;
            set => SetValue(ref kind, value);
        }
        public DateTime VaccinationDate
        {
            get => vaccinationDate;
            set => SetValue(ref vaccinationDate, value);
        }

        public ObservableCollection<Вакцины> Kinds
        {
            get => kinds;
            set => SetValue(ref kinds, value);
        }


        public RelayCommand AddCommand { get; set; }

        public VaccineCreatingViewModel() : this(null)
        {

        }
        public VaccineCreatingViewModel(Питомцы pet)
        {
            try
            {
                db = new ApplicationDb();
                db.Вакцины.Load();
                db.Прививки.Load();
            } 
            catch(Exception) { }

            Pet = pet;

            Kinds = new ObservableCollection<Вакцины>(db.Вакцины);

            AddCommand = new RelayCommand((param) =>
            {
                try
                {
                    Прививки vac = new Прививки();
                    vac.Дата_вакцинации = vaccinationDate;
                    vac.Вид_вакцины = kind.Код_вида;
                    vac.Код_питомца = pet.Код_питомца;

                    db.Прививки.Add(vac);
                    db.SaveChanges();
                    vaccineAdded?.Invoke(vac);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
