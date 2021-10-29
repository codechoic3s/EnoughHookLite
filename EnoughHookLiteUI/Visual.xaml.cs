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
using System.Windows.Shapes;

namespace EnoughHookLiteUI
{
    /// <summary>
    /// Interaction logic for Visual.xaml
    /// </summary>
    public partial class Visual : Window
    {
        public bool isLoaded;
        public Visual()
        {
            InitializeComponent();
            base.Loaded += Visual_Loaded;
        }

        private void Visual_Loaded(object sender, RoutedEventArgs e)
        {
            isLoaded = true;
        }
    }
}
