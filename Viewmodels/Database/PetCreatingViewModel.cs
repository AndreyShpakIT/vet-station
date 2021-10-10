using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels.Database
{
    class PetCreatingViewModel : NotifyObjectBase
    {
        public event Action<Питомцы> petAdded;
        private ApplicationDb db;

        private string name;
        private Виды_животных kind;
        private string bride;
        private int age;

        private ObservableCollection<Виды_животных> kinds;


        public string Name
        {
            get => name;
            set => SetValue(ref name, value);
        }
        public Виды_животных Kind
        {
            get => kind;
            set => SetValue(ref kind, value);
        }
        public string Bride
        {
            get => bride;
            set => SetValue(ref bride, value);
        }
        public int Age
        {
            get => age;
            set => SetValue(ref age, value);
        }

        public ObservableCollection<Виды_животных> Kinds
        {
            get => kinds;
            set => SetValue(ref kinds, value);
        }


        public RelayCommand AddCommand { get; set; }

        public PetCreatingViewModel() : this(null)
        {

        }
        public PetCreatingViewModel(Клиенты user)
        {
            try
            {
                db = new ApplicationDb();
                db.Питомцы.Load();
                db.Виды_животных.Load();
            }
            catch(Exception) { }

            Kinds = db.Виды_животных.Local;

            AddCommand = new RelayCommand((param) =>
            {
                try
                {
                    Питомцы pet = new Питомцы();
                    if (age > 99 || age < 0)
                    {
                        MessageBox.Show("Не корректный возраст!");
                        return;
                    }
                    pet.Возраст = age;
                    pet.Код_хозяина = user.Код_клиента;
                    pet.Вид_животного = kind.Код_вида;
                    pet.Кличка = name;
                    pet.Порода = bride;

                    db.Питомцы.Add(pet);
                    db.SaveChanges();
                    petAdded?.Invoke(pet);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
