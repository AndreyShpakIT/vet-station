using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels.Database
{
    class ListViewModel : NotifyObjectBase
    {
        private ObservableCollection<Виды_животных> list;
        public ObservableCollection<Виды_животных> List
        {
            get => list;
            set => SetValue(ref list, value);
        }
        private ApplicationDb db;

        public ListViewModel()
        {
            try
            {
                db = new ApplicationDb();
                db.Виды_животных.Load();

                List = db.Виды_животных.Local;
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
