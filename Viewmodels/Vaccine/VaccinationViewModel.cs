using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using VetStation.Database;
using VetStation.Views.Vaccine;

namespace VetStation.Viewmodels.Vaccine
{
    class VaccinationViewModel : NotifyObjectBase
    {
        private Клиенты selectedUser;
        private Питомцы selectedPet;
        private ObservableCollection<Клиенты> users;
        private ObservableCollection<Питомцы> pets;
        private ObservableCollection<ПрививкиПредставление> vaccines;

        public Клиенты SelectedUser
        {
            get => selectedUser;
            set
            {
                try
                {
                    Vaccines?.Clear();
                    Pets = new ObservableCollection<Питомцы>(
                        db.Питомцы.Where(p => p.Код_хозяина == value.Код_клиента)
                    );
                    SetValue(ref selectedUser, value);
                }
                catch(Exception) { }
            }
        }
        public Питомцы SelectedPet
        {
            get => selectedPet;
            set
            {
                try
                {
                    Vaccines = new ObservableCollection<ПрививкиПредставление>(
                        db.ПрививкиПредставление.Where(p => p.Код_питомца == value.Код_питомца)
                    );
                    SetValue(ref selectedPet, value);
                }
                catch (Exception) { }
            }
        }
        public ObservableCollection<Клиенты> Users
        {
            get => users;
            set => SetValue(ref users, value);
        }
        public ObservableCollection<Питомцы> Pets
        {
            get => pets;
            set => SetValue(ref pets, value);
        }
        public ObservableCollection<ПрививкиПредставление> Vaccines
        {
            get => vaccines;
            set => SetValue(ref vaccines, value);
        }
        private ApplicationDb db;

        public VaccinationViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.Клиенты.Load();
                db.Питомцы.Load();
                db.Прививки.Load();

                Users = GetUsers();
                if (Users.Count > 0)
                {
                    SelectedUser = Users[0];
                    if (Pets.Count > 0)
                    {
                        SelectedPet = Pets[0];
                    }
                }
            }
            catch (Exception) { }


            VaccineAdding = new RelayCommand((param) =>
            {
                if (selectedPet == null) return;

                VaccineCreatingWindow window = new VaccineCreatingWindow();
                VaccineCreatingViewModel vm = new VaccineCreatingViewModel(selectedPet);
                vm.vaccineAdded += (vac) =>
                {
                    try
                    {
                        Vaccines = new ObservableCollection<ПрививкиПредставление>(
                            db.ПрививкиПредставление.Where(p => p.Код_питомца == selectedPet.Код_питомца)
                        );
                        window.Close();
                    } catch { }
                };
                window.DataContext = vm;
                window.Show();
            });
            RefreshCommand = new RelayCommand((param) => 
            {
                Users = GetUsers();
            });
        }

        public RelayCommand VaccineAdding { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        public ObservableCollection<Клиенты> GetUsers()
        {
            try
            {
                return new ObservableCollection<Клиенты>(db.Клиенты.Local.Where(c => db.Питомцы.Any(p => c.Код_клиента == p.Код_хозяина)));
            } 
            catch { return null; }
        }
    }
}
