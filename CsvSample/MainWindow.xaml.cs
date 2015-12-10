using System;
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

using System.Data;
using CsvHelper;
using System.IO;
using CsvSample.Models;
using CommonUtilities;

using CsvSample.ViewModels;

namespace CsvSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private MailWindowViewModel mainindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.mainindowViewModel = new MailWindowViewModel();
            this.DataContext = this.mainindowViewModel;
        }

    }
}
