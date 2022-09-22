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
        private List<Department> allDepartments = DataWorker.GetDepartments();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value;
                NotifyPropertyChanged(nameof(AllDepartments));
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

        public string PositionName { get; set; }
        public decimal PositionSalary { get; set; }
        public int PositionMaxNumber { get; set; }
        public Department PositionDepartment { get; set; }

        public string StaffName { get; set; }
        public string StaffLastName { get; set; }
        public string StaffPhone { get; set; }
        public Position StaffPosition { get; set; }

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
                        SetRedBlockControl(window, "NameBlock");
                    }
                    else
                    {
                        SetGreenBlockControl(window, "NameBlock");
                        result = DataWorker.CreateDepartment(DepartmentName);
                        ShowMessage(result);
                        UpdateAllDataView();
                        SetNullValues();
                        window.Close();
                    }
                });
            }
        }

        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string result = "";
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Выберите отдел", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "NameBlock");
                    }
                    if (PositionSalary == 0)
                    {
                        SetRedBlockControl(window, "SalaryBlock");
                    }
                    if (PositionMaxNumber == 0)
                    {
                        SetRedBlockControl(window, "MaxNumberBlock");
                    }
                    else
                    {
                        SetGreenBlockControl(window, "NameBlock");
                        result = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionDepartment);
                        ShowMessage(result);
                        UpdateAllDataView();
                        SetNullValues();
                        window.Close();
                    }
                });
            }
        }

        private RelayCommand addNewStaff;
        public RelayCommand AddNewStaff
        {
            get
            {
                return addNewStaff ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string result = "";

                    if (StaffName == null || StaffName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "NameBlock");
                    }
                    if (StaffLastName == null || StaffLastName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "LastNameBlock");
                    }
                    if (StaffPhone == null || StaffPhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "PhoneBlock");
                    }
                    if (StaffPosition == null)
                    {
                        MessageBox.Show("Выберите должность", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        SetGreenBlockControl(window, "NameBlock");
                        result = DataWorker.CreateStaff(StaffName, StaffLastName, StaffPhone, StaffPosition);
                        ShowMessage(result);
                        UpdateAllDataView();
                        SetNullValues();
                        window.Close();
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
            Control? block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void SetGreenBlockControl(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.SpringGreen;
        }

        #region UPDATE VIEWS

        private void SetNullValues()
        {
            DepartmentName = null;

            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;

            StaffName = null;
            StaffLastName = null;
            StaffPhone = null;
            StaffPosition = null;
        }

        private void UpdateAllDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllPositionsView();
            UpdateAllStaffsView();
        }

        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetDepartments();
            MainWindow.AllDepartmentsList.ItemsSource = null;
            MainWindow.AllDepartmentsList.Items.Clear();
            MainWindow.AllDepartmentsList.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsList.Items.Refresh();
        }

        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetPositions();
            MainWindow.AllPositionsList.ItemsSource = null;
            MainWindow.AllPositionsList.Items.Clear();
            MainWindow.AllPositionsList.ItemsSource = AllPositions;
            MainWindow.AllPositionsList.Items.Refresh();
        }

        private void UpdateAllStaffsView()
        {
            AllStaffs = DataWorker.GetStaffs();
            MainWindow.AllStaffsList.ItemsSource = null;
            MainWindow.AllStaffsList.Items.Clear();
            MainWindow.AllStaffsList.ItemsSource = AllStaffs;
            MainWindow.AllStaffsList.Items.Refresh();
        }

        #endregion

        private void ShowMessage(string message)
        {
            MessageView messageView = new(message);
            SetCenterPosition(messageView);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
