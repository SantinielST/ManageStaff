using ManageStaff.ViewModel;
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

namespace ManageStaff
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllDepartmentsList;
        public static ListView AllPositionsList;
        public static ListView AllStaffsList;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
            AllStaffsList = AllStaffsListView;
            AllPositionsList = AllPositionsListView;
            AllDepartmentsList = AllDepartmentsListView;
        }
    }
}
