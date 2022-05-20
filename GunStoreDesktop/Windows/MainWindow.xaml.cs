using System.Windows;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;

namespace GunStoreDesktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Employee CurrentEmployee { get; set; }
        public bool isAdmin { get; set; }
        
        public MainWindow()
        {
            CurrentEmployee = DataFactory.GetMySqlDataFactory().Employee.getEmployees()[0];
            SettingsUtil.SetStartupSettings(CurrentEmployee.Settings);
            InitializeComponent();
            isAdmin = true;
            DataContext = this;
        }
    }
}