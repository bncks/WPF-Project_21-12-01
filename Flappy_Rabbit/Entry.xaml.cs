using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Flappy_Rabbit
{
    /// <summary>
    /// Interaction logic for Entry.xaml
    /// </summary>
    public partial class Entry : Window
    {
        public Entry()
        {
            InitializeComponent();
        }

        //Start gomb működtetése 
        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
            
        }

        //Kilépés gomb működtetése
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            
        }
    }
}
