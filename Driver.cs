using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
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
    class Driver
    {
        private const string DEFAULT_NAME_SURNAME = "Default name";
        private const int DEFAULT_NUMBER = 0;
        private const string DEFAULT_ADRESS = "Default Adress";
        private const string DEFAULT_PHOTO_PATH = "photo\\default_photo.jpg";
        private const int DEFAULT_HGT = 120;
        private static readonly DateTime DEFAULT_DOB = new DateTime(1901, 1, 1);
        private static readonly DateTime DEFAULT_ISS = DEFAULT_DOB;
        private static readonly DateTime DEFAULT_EXP = DEFAULT_DOB;
        private const char DEFAULT_CLASS_LICENSE = 'Z';


        public string NameSurname { get; set; }
        public int Number { get; set; }
        public string Adress { get; set; }
        public DateTime DOB { get; set; }
        public char Class_License { get; set; }
        public DateTime ISS { get; set; }
        public DateTime EXP { get; set; }
        public bool OrganDonor { get; set; }
        public string PhotoPath { get; set; }
        public GenderEnum Gender { get; set; }
        public EyesEnum Eyes { get; set; }
        public int HGT { get; set; }


        public Driver()
        {
            NameSurname = DEFAULT_NAME_SURNAME;
            Number = DEFAULT_NUMBER;
            Adress = DEFAULT_ADRESS;
            DOB = DEFAULT_DOB;
            ISS = DEFAULT_ISS;
            EXP = DEFAULT_EXP;
            OrganDonor = false;
            PhotoPath = DEFAULT_PHOTO_PATH;
            Gender = GenderEnum.other;
            Eyes = EyesEnum.Amber;
            HGT = DEFAULT_HGT;
            Class_License = DEFAULT_CLASS_LICENSE;

        }

        public Driver(string nameSurname, int number, string adress, DateTime dOB,
            char class_License, DateTime iSS, DateTime eXP, bool organDonor,
            string photoPath, GenderEnum gender, EyesEnum eyes, int hGT)
        {
            NameSurname = nameSurname;
            Number = number;
            Adress = adress;
            DOB = dOB;
            Class_License = class_License;
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
            string dr = $"FIO: {NameSurname}\nLicense Number: {Number}\n" +
                $"License class {Class_License}\nAdress: {Adress}\n" +
                $"DOB: {DOB.ToShortDateString()}\nOrgan donor: {OrganDonor}\n" +
                $"Photo: {PhotoPath}\n Gender: {Gender.ToString()}\nEyes: {Eyes.ToString()}\n" +
                $"HGT: {HGT}\n ISS: {ISS.ToShortDateString()}\nEXP: {EXP.ToShortDateString()}";
            return dr;
        }
        /// <summary>
        /// Метод возвращает true если профиль заполнен полностью. В ином случае false.
        /// </summary>
        public bool IsFullInfo()
        {

            if (NameSurname == DEFAULT_NAME_SURNAME || Number == DEFAULT_NUMBER ||
                Adress == DEFAULT_ADRESS || DOB == DEFAULT_DOB ||
                PhotoPath == DEFAULT_PHOTO_PATH || HGT == DEFAULT_HGT || ISS == DEFAULT_ISS ||
                EXP == DEFAULT_EXP || Class_License == DEFAULT_CLASS_LICENSE) return false;
            else return true;
            // Данный метод используется для отслеживания заполенения профиля.
        }

        /// <summary>
        /// Создает экзмепляр класса Driver c случайными данными
        /// </summary>
        /// <returns>Driver obj</returns>
        public static Driver GetNewRandomDriver()
        {
            Random rd = new Random(); 

            List<List<string>> names = new List<List<string>>(); // Список списков с именами.
            names.Add(new List<string>() { "Alex", "Max", "Jhon" }); //Мужские имена
            names.Add(new List<string>() { "Alice", "Ann", "Liz" }); // Женские имена
            // Фамилии
            List<string> surnames = new() {"Merser", "Smith", "Falei", "Asher", "Spilberg" };
            List<string> adresses = new() { "Misnk", "London", "Paris", "San Jose", "Amsterdam" };
            // Список доступных фото.Что бы фото доступно для выбора, вторая буква в имени должна 
            // быть m или f. m- на фото мужчина , f-на фото женщина.
            List<string> allPhotos = Directory.GetFiles("photo").ToList<string>();

            // Случайное число для индекса выбора мужских или женских имен и фото. 0- мужские имена и фото
            //1- Женские имена и фото
            int gender_int = rd.Next(0, 2);
            // Выбор имени согласно случайному индексу
            string name = names[gender_int][rd.Next(names[gender_int].Count)];
            string surname = surnames[rd.Next(surnames.Count)]; // Выбор фамилии

            string r_nameAndSurname = name + " " + surname; // Имя и фамилия
            string r_Adress = adresses[rd.Next(adresses.Count)];
            DateTime r_Dob = new DateTime(rd.Next(1940, DateTime.Now.AddYears(-15).Year),
                rd.Next(1, 13), rd.Next(1,29)); // Случайная дата рождения.Возраст не менее 16 лет
            char r_Class_License = (char)('A' + rd.Next(6)); // Символ лицензии
            int r_number = rd.Next(1, 10000000); // Номер лицензии
            DateTime r_Iss = new DateTime(rd.Next(r_Dob.AddYears(17).Year, DateTime.Now.Year),
                rd.Next(1,13), rd.Next(1,29)); // Случайная дата выдачи лицензии, с 16 лет от даты рождения
            DateTime r_Exp = r_Iss.AddYears(rd.Next(1, 11)); // Случайная дата окончания.

            bool r_Donor = rd.Next(1) == 0? true : false;
            // Случайный выбор гендера и цвета глаз
            GenderEnum r_Gender = (GenderEnum) rd.Next(Enum.GetValues(typeof(GenderEnum)).Length);
            EyesEnum r_Eyes = (EyesEnum)rd.Next(Enum.GetValues(typeof(EyesEnum)).Length);
            int r_Hgt = rd.Next(120, 211); // рост

            //Список только мужчких или только женских фото
            List<string> genderPhotos = new List<string>(); 

            foreach(string path in allPhotos)
            {
                if (gender_int == 0) //Отбор всех фотографий только с мужчинами
                {
                    if (path[1] == 'm') genderPhotos.Add(path);       
                }
                ////Отбор всех фотографий только с женщинами
                else if (gender_int == 1) { if (path[2] == 'f') genderPhotos.Add(path); } 
            }
            string r_PhotoPath = genderPhotos[ rd.Next(genderPhotos.Count)]; // Выбор случайного фото

            // Экземпляр с набором случайных данных.
            Driver newDriver = new Driver(r_nameAndSurname, r_number, r_Adress, r_Dob, r_Class_License,
                r_Iss, r_Exp, r_Donor, r_PhotoPath, r_Gender, r_Eyes, r_Hgt);

            return newDriver;
            
        }
    }
}
