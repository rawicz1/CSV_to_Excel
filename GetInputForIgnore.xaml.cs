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

namespace Bank_statement_to_excel
{
    /// <summary>
    /// Interaction logic for GetInputForIgnore.xaml
    /// </summary>
    public partial class GetInputForIgnore : Window
    {
        public string UserInput { get; private set; }
        public GetInputForIgnore()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            UserInput = txtInputToIgnore.Text;
            Close();
        }
    }
}
