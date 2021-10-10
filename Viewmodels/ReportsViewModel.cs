using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using VetStation.Database;

namespace VetStation.Viewmodels
{
    class ReportsViewModel : NotifyObjectBase
    {
        private Питомцы pet;

        public Питомцы Pet
        {
            get => pet;
            set => SetValue(ref pet, value);
        }
        
        public List<Питомцы> Pets { get; set; }
        private ApplicationDb db;
        public ReportsViewModel()
        {
            InitCommands();
            db = new ApplicationDb();
            db.Питомцы.Load();
            db.ОказанныеУслугиПредставление.Load();
            db.ПрививкиПредставление.Load();
            db.Питомцы.Load();

            Pets = db.Питомцы.Local.ToList();
        }

        public RelayCommand Report1Command { get; set; }
        public RelayCommand Report2Command { get; set; }

        private void InitCommands()
        {
            Report1Command = new RelayCommand((param) => CreateRep1(Pet));
        }

        public bool CreateRep1(Питомцы pet)
        {
            if (pet == null)
            {
                MessageBox.Show("Выберите питомца!");
                return false;
            }
            return ReportManager.CreateNewReport(sheet =>
            {
                sheet.Cells[1, 1].Value = "Отчет по питомцу: ";
                sheet.Cells[1, 1].Style.Font.Bold = true;
                sheet.Cells[1, 1].Style.Font.Size = 13;
                                           
                sheet.Cells[1, 2].Value = pet.Кличка;
                sheet.Cells[1, 2].Style.Font.Size = 13;
                                           
                sheet.Cells[3, 1].Value = "Информация по вакцинации:";
                sheet.Cells[5, 1].Value = "Дата";
                sheet.Cells[5, 2].Value = "Вакцина";
                sheet.Cells[5, 3].Value = "Срок действия";

                int row = 6;
                var list = db.ПрививкиПредставление.Local.Where(o => o.Код_питомца == pet.Код_питомца).ToList();
                int i = 0;
                for (i = 0; i < list.Count; i++)
                {
                    sheet.Cells[i + row, 1].Value = list[i].Дата_вакцинации;
                    sheet.Cells[i + row, 1].Style.Numberformat.Format = "yyyy-mm-dd";
                    sheet.Cells[i + row, 2].Value = list[i].Название_вакцины;
                    sheet.Cells[i + row, 3].Value = list[i].Срок_действия;
                }

                // Table 2
                row = i + 2 + row;
                sheet.Cells[row, 1].Value = "Оказываемые услуги:";

                row++;
                sheet.Cells[row, 1].Value = "Дата";
                sheet.Cells[row, 2].Value = "Услуга";

                row++;
                var list2 = db.ОказанныеУслугиПредставление.Local.Where(o => o.Кличка == pet.Кличка).ToList();
                for (i = 0; i < list2.Count; i++)
                {
                    sheet.Cells[i + row, 1].Value = list2[i].Дата_оказания;
                    sheet.Cells[i + row, 1].Style.Numberformat.Format = "yyyy-mm-dd";
                    sheet.Cells[i + row, 2].Value = list2[i].Название_услуги;
                }
            }, "report1.xlsx");
        }
    }
}
