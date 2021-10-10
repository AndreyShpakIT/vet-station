using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using VetStation.Views.Database;
using VetStation.Views.Medicine;
using VetStation.Views.Service;
using VetStation.Views.Vaccine;

namespace VetStation.Viewmodels
{
    class MainWindowViewModel : NotifyObjectBase
    {
        public ObservableCollection<UserControl> Views { get; set; }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set => SetValue(ref _currentView, value);
        }
        // Commands
        public RelayCommand TabClickCommand { get; set; }
        public RelayCommand OpenHelpCommand { get; set; }

        public MainWindowViewModel()
        {
            Views = new ObservableCollection<UserControl>
            {
                new DatabaseView(),
                new ServiceView(),
                new VaccinationView(),
                new MedicineView(),
            };
            CurrentView = Views[0];

            TabClickCommand = new RelayCommand((parameter) =>
            {
                try
                {
                    int number = Convert.ToInt32(parameter);
                    CurrentView = Views[number];
                }
                catch
                {
                    MessageBox.Show("Еще не реализовано!");
                }
            });
            OpenHelpCommand = new RelayCommand(param =>
            {
                try
                {
                    Process.Start("help.chm");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
