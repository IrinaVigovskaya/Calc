using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.CodeDom;
using System.Reflection;
using System.Data.SQLite;

namespace Calc
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IHistory RAM = new RAM_History();
            IHistory DB = new DataBase();
            IHistory File = new FileHistory();

            var mainViewModel = new MainViewModel(DB);
            DataContext = mainViewModel;
        }
    }
}

