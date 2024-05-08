using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVPP_CS_WPF_Lab3_task1_Driving_license__Modification_
{
    /// <summary>
    /// Класс содержащий данные для случайного экземпляра Driver.
    /// </summary>
    public class DriverFakeData
    {
        //static public string PhotosDir = "pack://application:,,,/images/";
        static public string PhotosDir = "D:\\Learning\\programming\\c#projects\\SVPP_CS_WPF_Lab3_task1_Driving_license_(Modification)\\images\\";
   
        static public string DefaultPhoto = PhotosDir + "default.jpg";
        static public List<string> MaleNames = new() { "Alex", "Max", "Jhon" };
        static public List<string> FemaleNames = new() {"Alice", "Ann", "Liz" };
        static public List<string> AllNames = MaleNames.Concat(FemaleNames).ToList();
        static public List<string> Surnames = new() { "Merser", "Smith", "Falei", "Asher", "Spilberg" };
        static public List<string> Adresses = new() { "Misnk", "London", "Paris", "San Jose", "Amsterdam"};

        static public List<string>  AllPhotos = Directory.GetFiles(PhotosDir).ToList();
        static public List<string> MalePhotos = AllPhotos.FindAll(f => new FileInfo(f).Name.StartsWith("m"));
        static public List<string> FemalePhotos = AllPhotos.FindAll(f => new FileInfo(f).Name.StartsWith("f"));
    }
}
