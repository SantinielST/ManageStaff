using ManageStaff.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManageStaff.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewStaffWindow.xaml
    /// </summary>
    public partial class AddNewStaffWindow : Window
    {
        public AddNewStaffWindow()
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^0-9+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
