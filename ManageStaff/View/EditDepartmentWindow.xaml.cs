using ManageStaff.ViewModel;
using System.Windows;

namespace ManageStaff.View
{
    /// <summary>
    /// Логика взаимодействия для EditDepartmentWindow.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        public EditDepartmentWindow()
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
        }
    }
}
