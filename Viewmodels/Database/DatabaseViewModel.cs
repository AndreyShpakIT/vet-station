using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using VetStation.Database;
using VetStation.Views.Database;

namespace VetStation.Viewmodels.Database
{
    class DatabaseViewModel : NotifyObjectBase
    {
        private Клиенты selectedItem;
        private ObservableCollection<Клиенты> users;
        private ObservableCollection<ПитомцыПредставление> pets;

        public Клиенты SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value == null) return;
                try
                {
                    Pets = new ObservableCollection<ПитомцыПредставление>(
                        db.ПитомцыПредставление.Local.Where(p => p.Код_хозяина == value.Код_клиента)
                        );
                    SetValue(ref selectedItem, value);
                }
                catch { }
            }
        }
        public ObservableCollection<Клиенты> Users
        {
            get => users;
            set => SetValue(ref users, value);
        }
        public ObservableCollection<ПитомцыПредставление> Pets
        {
            get => pets;
            set => SetValue(ref pets, value);
        }
        private ApplicationDb db;

        public DatabaseViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.Клиенты.Load();
                db.ПитомцыПредставление.Load();

                Users = db.Клиенты.Local;
            }
            catch (Exception) { }

            PetAdding = new RelayCommand((parma) =>
            {
                if (selectedItem == null) return;

                PetCreatingWindow window = new PetCreatingWindow();
                PetCreatingViewModel vm = new PetCreatingViewModel(selectedItem);
                vm.petAdded += (pet) =>
                {
                    Pets = new ObservableCollection<ПитомцыПредставление>(
                        db.ПитомцыПредставление.Where(p => p.Код_хозяина == selectedItem.Код_клиента)
                    );
                    window.Close();
                };
                window.DataContext = vm;
                window.Show();
            });
            SaveCommand = new RelayCommand((parma) =>
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
        public RelayCommand PetAdding { get; set; }
    }
}
