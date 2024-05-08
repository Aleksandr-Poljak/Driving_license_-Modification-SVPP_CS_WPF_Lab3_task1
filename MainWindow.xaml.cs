using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SVPP_CS_WPF_Lab3_task1_Driving_license__Modification_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal Driver driver;

        public MainWindow()
        {
            InitializeComponent();  
            driver = new Driver();
            Grid_Main.DataContext = driver;
        }
              
        /// <summary>
        /// Загружает новое изображение
        /// </summary>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Photo (.png,jpg)|*.png;*jpg";
            dlg.FileName = "";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                driver.PhotoPath = dlg.FileName;
                //Image_UserPhoto.Source = new BitmapImage(new Uri(dlg.FileName, UriKind.RelativeOrAbsolute));
            }
            else { return; }

        }
        /// <summary>
        /// Обработчик кнопки Save. Сохранение введной иноформации во объект
        /// </summary>
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(driver.ToString(), "SAVE");
           
        }

        /// <summary>
        ///  Обработчик кнопки Load. Извлечение инофрмации из объекта и заполение форм.
        /// </summary>
        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            driver = Driver.GetNewRandomDriver();
            Grid_Main.DataContext = driver;
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            driver = new Driver();
            Grid_Main.DataContext= driver;
        }
    }
}