using Ski_Service_2.Orders;
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

namespace Ski_Service_2
{
    /// <summary>
    /// Interaktionslogik für BestellungEdit.xaml
    /// </summary>
    public partial class BestellungEdit : Window
    {
        public BestellungEdit()
        {
            InitializeComponent();
            DataContext = new BestellungEditViewModel();
        }
    }
}
