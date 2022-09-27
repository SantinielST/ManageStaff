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
        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value;
                NotifyPropertyChanged(nameof(AllDepartments));
            }
        }

        private List<Position> allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get { return allPositions; }
            set
            {
                allPositions = value;
                NotifyPropertyChanged(nameof(AllPositions));
            }
        }

        private List<Staff> allStaffs = DataWorker.GetAllStaffs();
        public List<Staff> AllStaffs
        {
            get { return allStaffs; }
            set
            {
                allStaffs = value;
                NotifyPropertyChanged(nameof(AllStaffs));
            }
        }

        public TabItem SelectedTabItem { get; set; }
        public static Staff SelectedStaff { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }

        #region COMMANDS TO ADD

        public static string DepartmentName { get; set; }

        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public static Department PositionDepartment { get; set; }

        public static string StaffName { get; set; }
        public static string StaffLastName { get; set; }
        public static string StaffPhone { get; set; }
        public static Position StaffPosition { get; set; }

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

        #region COMMAND TO DELETE

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string result = "Выберите объект";

                    if (SelectedTabItem.Name == "StaffTab" && SelectedStaff != null)
                    {
                        result = DataWorker.DeleteStaff(SelectedStaff);
                        UpdateAllDataView();
                    }

                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        result = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }

                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        result = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }
                    SetNullValues();
                    ShowMessage(result);
                });
            }
        }

        #endregion

        #region COMMANDS TO EDIT

        private RelayCommand editStaff;
        public RelayCommand EditStaff
        {
            get
            {
                return editStaff ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string result = "Не выбран сотрудник";
                    string noPisition = "Не выбрана должность";

                    if (SelectedStaff != null)
                    {
                        if (StaffPosition != null)
                        {
                            result = DataWorker.EditStaff(SelectedStaff, StaffName, StaffLastName, StaffPhone, StaffPosition);

                            UpdateAllDataView();
                            SetNullValues();
                            ShowMessage(result);
                            window.Close();
                        }
                        else ShowMessage(noPisition);
                    }
                    else ShowMessage(result);
                });
            }
        }

        private RelayCommand editPosition;
        public RelayCommand EditPosition
        {
            get
            {
                return editPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string result = "Не выбрана позиция";
                    string noDepaartment = "Не выбран отдел";

                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            result = DataWorker.EditPosition(SelectedPosition, PositionName, PositionSalary, PositionMaxNumber, PositionDepartment);

                            UpdateAllDataView();
                            SetNullValues();
                            ShowMessage(result);
                            window.Close();
                        }
                        else ShowMessage(noDepaartment);
                    }
                    else ShowMessage(result);
                });
            }
        }

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    var window = obj as Window;
                    string result = "Отдел не выбран";

                    if (SelectedDepartment != null)
                    {
                        result = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);

                        UpdateAllDataView();
                        SetNullValues();
                        ShowMessage(result);
                        window.Close();
                    }
                    else ShowMessage(result);
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

        private RelayCommand openEditWindow;
        public RelayCommand OpenEditWindow
        {
            get
            {
                return openEditWindow ?? new RelayCommand(obj =>
                {
                    if (SelectedTabItem.Name == "StaffTab" && SelectedStaff != null)
                    {
                        StaffName = SelectedStaff.Name;
                        StaffLastName = SelectedStaff.LastName;
                        StaffPhone = SelectedStaff.Phone;

                        OpenEditStaffWindowMethod();
                    }

                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        PositionName = SelectedPosition.Name;
                        PositionSalary = SelectedPosition.Salary;
                        PositionMaxNumber = SelectedPosition.MaxNumber;

                        OpenEditPositionWindowMethod();
                    }

                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        DepartmentName = SelectedDepartment.Name;

                        OpenEditDepartmentWindowMethod();
                    }
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
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsList.ItemsSource = null;
            MainWindow.AllDepartmentsList.Items.Clear();
            MainWindow.AllDepartmentsList.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsList.Items.Refresh();
        }

        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsList.ItemsSource = null;
            MainWindow.AllPositionsList.Items.Clear();
            MainWindow.AllPositionsList.ItemsSource = AllPositions;
            MainWindow.AllPositionsList.Items.Refresh();
        }

        private void UpdateAllStaffsView()
        {
            AllStaffs = DataWorker.GetAllStaffs();
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
