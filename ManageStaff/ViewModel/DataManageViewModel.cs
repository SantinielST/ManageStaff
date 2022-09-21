using ManageStaff.Model;
using ManageStaff.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManageStaff.ViewModel
{
    public class DataManageViewModel : INotifyPropertyChanged
    {
        private List<Department> allDepartmets = DataWorker.GetDepartments();
        public List<Department> AllDepartmets
        {
            get { return allDepartmets; }
            set
            {
                allDepartmets = value;
                NotifyPropertyChanged(nameof(AllDepartmets));
            }
        }

        private List<Position> allPositions = DataWorker.GetPositions();
        public List<Position> AllPositions
        {
            get { return allPositions; }
            set
            {
                allPositions = value;
                NotifyPropertyChanged(nameof(AllPositions));
            }
        }

        private List<Staff> allStaffs = DataWorker.GetStaffs();
        public List<Staff> AllStaffs
        {
            get { return allStaffs; }
            set
            {
                allStaffs = value;
                NotifyPropertyChanged(nameof(AllStaffs));
            }
        }

        #region COMMANDS TO ADD

        public string DepartmentName { get; set; }

        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string result = "";

                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        //SetRedBlockControl(window, "NameBlock");
                    }
                    else
                    {
                        result = DataWorker.CreateDepartment(DepartmentName);
                    }
                });
            }
        }

        #endregion

        #region COMMANDS TO OPEN WINDOWS

        private RelayCommand openAddNewDepartment;
        public RelayCommand OpenAddNewDepartment
        {
            get
            {
                return openAddNewDepartment ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentWindowMethod();
                });
            }
        }

        private RelayCommand openAddNewPosition;
        public RelayCommand OpenAddNewPosition
        {
            get
            {
                return openAddNewPosition ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                });
            }
        }

        private RelayCommand openAddNewStaff;
        public RelayCommand OpenAddNewStaff
        {
            get
            {
                return openAddNewStaff ?? new RelayCommand(obj =>
                {
                    OpenAddStaffWindowMethod();
                });
            }
        }

        private RelayCommand openEditDepartment;
        public RelayCommand OpenEditDepartment
        {
            get
            {
                return openEditDepartment ?? new RelayCommand(obj =>
                {
                    OpenEditDepartmentWindowMethod();
                });
            }
        }

        private RelayCommand openEditPosition;
        public RelayCommand OpenEditPosition
        {
            get
            {
                return openEditPosition ?? new RelayCommand(obj =>
                {
                    OpenEditPositionWindowMethod();
                });
            }
        }

        private RelayCommand openEditStaff;
        public RelayCommand OpenEditStaff
        {
            get
            {
                return openEditStaff ?? new RelayCommand(obj =>
                {
                    OpenEditStaffWindowMethod();
                });
            }
        }

        #endregion

        #region METHODS TO OPEN WINDOWS
        private void OpenAddDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPosition(newDepartmentWindow);
        }

        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterPosition(newPositionWindow);
        }

        private void OpenAddStaffWindowMethod()
        {
            AddNewStaffWindow newStaffWindow = new AddNewStaffWindow();
            SetCenterPosition(newStaffWindow);
        }

        private void OpenEditDepartmentWindowMethod()
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow();
            SetCenterPosition(editDepartmentWindow);
        }

        private void OpenEditPositionWindowMethod()
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow();
            SetCenterPosition(editPositionWindow);
        }

        private void OpenEditStaffWindowMethod()
        {
            EditStaffWindow editStaffWindow = new EditStaffWindow();
            SetCenterPosition(editStaffWindow);
        }

        private void SetCenterPosition(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        private void SetRedBlockControl(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void SetGreenBlockControl(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.SpringGreen;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
