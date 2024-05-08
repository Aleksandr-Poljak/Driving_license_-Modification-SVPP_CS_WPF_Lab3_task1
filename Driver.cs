using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xaml;
using System.Xml.Linq;

namespace SVPP_CS_WPF_Lab3_task1_Driving_license__Modification_
{
    enum GenderEnum { male, female, other };
    enum EyesEnum { Blue, Brown, Green, Amber, Red, Black };

    /// <summary>
    /// Driver class.
    /// </summary>
    class Driver : INotifyPropertyChanged, IDataErrorInfo
    {

        private string nameSurname = string.Empty;
        private string number = "A0000001";
        private string adress = string.Empty;
        private DateTime dob = new DateTime(1940, 1, 1);
        private char classLicense = 'A';
        private DateTime iss = new DateTime(1960, 1, 1);
        private DateTime exp = DateTime.Now.AddYears(1);
        private bool organDonor = false;
        private string photoPath = DriverFakeData.DefaultPhoto;
        private GenderEnum gender = GenderEnum.other;
        private EyesEnum eyes = EyesEnum.Amber;
        private int hgt = 120;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public string NameSurname {
            get { return nameSurname; }
            set
            {
                nameSurname = value;
                OnPropertyChanged(nameof(NameSurname));
            }
        }
        public string Number {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public string Adress {
            get => adress;
            set
            {
                adress = value;
                OnPropertyChanged(nameof(Adress));
            }
        }
        public DateTime DOB {
            get => dob;
            set
            {
                dob = value;
                OnPropertyChanged(nameof(DOB));
            }
        }
        public char ClassLicense {
            get => classLicense;
            set
            {
                classLicense = value;
                OnPropertyChanged(nameof(ClassLicense));
            }
        }
        public DateTime ISS {
            get => iss;
            set
            {
                iss = value;
                OnPropertyChanged(nameof(ISS));
            }
        }
        public DateTime EXP {
            get => exp;
            set
            {
                exp = value;
                OnPropertyChanged(nameof(EXP));
            }
        }
        public bool OrganDonor {
            get => organDonor;
            set
            {
                organDonor = value;
                OnPropertyChanged(nameof(OrganDonor));
            }
        }
        public string PhotoPath
        {
            get => photoPath;
            set
            {
                photoPath = value;
                OnPropertyChanged(nameof(PhotoPath));
            }
        }
        public GenderEnum Gender {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public EyesEnum Eyes {
            get => eyes;
            set
            {
                eyes = value;
                OnPropertyChanged(nameof(Eyes));
            }
        }
        public int HGT {
            get => hgt;
            set
            {
                hgt = value;
                OnPropertyChanged(nameof(HGT));
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] 
        { 
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "ClassLicense":
                        if (classLicense < 'A' || classLicense > 'E')
                            error = "Недопустимый символ";
                            break;
                    case "DOB":
                        if ((DOB < new DateTime(1900, 1, 1)) || (DOB.AddYears(16) > DateTime.Now))
                            error = "Некорректная дата рождения";
                        break;
                    case "ISS":                       
                        if (ISS < DOB.AddYears(16))
                            error += "Пользователю нет 16 лет.";
                        if (ISS > DateTime.Now)
                            error += " Некорректная дата выдачи";
                        break;
                    case "EXP":
                        if (EXP < DateTime.Now)
                            error = "Некорректная дата окончания";
                        break;
                    case "Number":
                        Regex rg = new Regex("[^A-Za-z0-9]");
                        if (rg.IsMatch(Number))
                            error = "Некорректный номер лицензии";
                        break;

                } 
                return error;
            }

        }

        public Driver() { }

        public Driver(string nameSurname, string number, string adress, DateTime dOB,
            char class_License, DateTime iSS, DateTime eXP, bool organDonor,
            string photoPath, GenderEnum gender, EyesEnum eyes, int hGT)
        {
            NameSurname = nameSurname;
            Number = number;
            Adress = adress;
            DOB = dOB;
            ClassLicense = class_License;
            ISS = iSS;
            EXP = eXP;
            OrganDonor = organDonor;
            PhotoPath = photoPath;
            Gender = gender;
            Eyes = eyes;
            HGT = hGT;
        }

        public override string ToString()
        {
            string info = $"FIO: {NameSurname}\nLicense Number: {Number}\n" +
                $"License class {ClassLicense}\nAdress: {Adress}\n" +
                $"DOB: {DOB.ToShortDateString()}\nOrgan donor: {OrganDonor}\n" +
                $"Photo: {PhotoPath}\nGender: {Gender.ToString()}\nEyes: {Eyes.ToString()}\n" +
                $"HGT: {HGT}\n ISS: {ISS.ToShortDateString()}\nEXP: {EXP.ToShortDateString()}";
            return info;
        }

        /// <summary>
        /// Создает экзмепляр класса Driver c случайными данными
        /// </summary>
        /// <returns>Driver obj</returns>
        public static Driver GetNewRandomDriver()
        {
            Random rd = new Random();
            
            GenderEnum r_Gender = (GenderEnum)rd.Next(Enum.GetValues(typeof(GenderEnum)).Length);
            EyesEnum r_Eyes = (EyesEnum)rd.Next(Enum.GetValues(typeof(EyesEnum)).Length);

            // Выбор имени согласно гендеру
            string name;
            if (r_Gender == GenderEnum.male) name = DriverFakeData.MaleNames[rd.Next(DriverFakeData.MaleNames.Count)];
            else if (r_Gender == GenderEnum.female) name = DriverFakeData.FemaleNames[rd.Next(DriverFakeData.FemaleNames.Count)];
            else  { name = DriverFakeData.AllNames[rd.Next(DriverFakeData.AllNames.Count)]; };

            string surname = DriverFakeData.Surnames[rd.Next(DriverFakeData.Surnames.Count)]; // Выбор фамилии
            string r_nameAndSurname = name + " " + surname; // Имя и фамилия
            int r_Hgt = rd.Next(120, 211); // рост
            bool r_Donor = rd.Next(2) == 0 ? true : false;

            string r_Adress = DriverFakeData.Adresses[rd.Next(DriverFakeData.Adresses.Count)];

            char r_Class_License = (char)('A' + rd.Next(5)); // Символ лицензии
            string r_number = rd.Next(1, 10000000).ToString(); // Номер лицензии

            DateTime r_Dob = new DateTime(rd.Next(1900, DateTime.Now.AddYears(-15).Year),
                rd.Next(1, 12), rd.Next(1,28)); // Случайная дата рождения.Возраст не менее 16 лет          
            DateTime r_Iss = new DateTime(rd.Next(r_Dob.AddYears(16).Year, DateTime.Now.Year),
                rd.Next(1,12), rd.Next(1,28)); // Случайная дата выдачи лицензии, с 16 лет от даты рождения
            DateTime r_Exp = DateTime.Now.AddYears(rd.Next(1,5)); // Дата окончания.

            // Случайный выбор фото согласно гендеру.
            string r_PhotoPath = string.Empty;
            if (r_Gender == GenderEnum.male) 
                r_PhotoPath = DriverFakeData.MalePhotos[rd.Next(DriverFakeData.MalePhotos.Count)];
            if (r_Gender == GenderEnum.female)
                r_PhotoPath = DriverFakeData.FemalePhotos[rd.Next(DriverFakeData.FemalePhotos.Count)];
            if (r_Gender == GenderEnum.other)
                r_PhotoPath = DriverFakeData.AllPhotos[rd.Next(DriverFakeData.AllPhotos.Count)];

            // Экземпляр с набором случайных данных.
            Driver newDriver = new Driver(r_nameAndSurname, r_number, r_Adress, r_Dob, r_Class_License,
                r_Iss, r_Exp, r_Donor, r_PhotoPath, r_Gender, r_Eyes, r_Hgt);

            return newDriver;            
        }
    }
}
