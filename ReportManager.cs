using System;
using VetStation.Database;
using OfficeOpenXml;
using System.IO;
using System.Windows;

namespace VetStation
{
    public delegate void Generation(ExcelWorksheet package);
    static class ReportManager
    {
        public static string Path = "Reports/";

        private static ApplicationDb db;

        static ReportManager()
        {
            db = new ApplicationDb();
        }

        public static bool SaveReport(ExcelPackage package, string path)
        {
            try
            {
                File.WriteAllBytes(path, package.GetAsByteArray());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            MessageBox.Show("Отчет успешно создан по следующему пути: " + path);
            return true;
        }

        public static bool CreateNewReport(Generation generate, string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage();

            ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Лист1");

            generate(sheet);

            return SaveReport(package, Path + fileName);
        }

    }
}
