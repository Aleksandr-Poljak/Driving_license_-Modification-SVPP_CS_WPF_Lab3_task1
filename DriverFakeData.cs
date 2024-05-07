using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVPP_CS_WPF_Lab3_task1_Driving_license__Modification_
{
    // Класс содержащий фековый данные для случайного экземпляра Driver.
    public class DriverFakeData
    {
        static public string PhotosDir = "D:\\Learning\\programming\\c#projects\\SVPP_CS_WPF_Lab3_task1_Driving_license_(Modification)\\photo\\";

        static public string DefaultPhoto = PhotosDir + "Default_photo.jpg";
        static public List<string> MaleNames = new() { "Alex", "Max", "Jhon" };
        static public List<string> FemaleNames = new() {"Alice", "Ann", "Liz" };
        static public List<string> AllNames = MaleNames.Concat(FemaleNames).ToList();
        static public List<string> Surnames = new() { "Merser", "Smith", "Falei", "Asher", "Spilberg" };
        static public List<string> Adresses = new() { "Misnk", "London", "Paris", "San Jose", "Amsterdam"};
        
        static public List<FileInfo> AllPhotos = (new DirectoryInfo(PhotosDir)).GetFiles().ToList();
        static public List<FileInfo> MalePhotos = AllPhotos.FindAll(f => f.Name.StartsWith("m"));
        static public List<FileInfo> FemalePhotos = AllPhotos.FindAll(f => f.Name.StartsWith("f"));
    }
}
