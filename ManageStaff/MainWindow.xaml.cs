using ManageStaff.ViewModel;
using System.Windows;
using System.Windows.Controls;

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

            ManagerTabControl.SelectedValue = StaffTab;
        }
    }
}
