using ManageStaff.Data;
using ManageStaff.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace ManageStaff.Model
{
    public class DataWorker
    {
        public static List<Department> GetDepartments()
        {
            using (ApplContext applContext = new ApplContext())
            {
                var result = applContext.Departments.ToList();
                return result;
            }
        }

        public static List<Position> GetPositions()
        {
            using (ApplContext applContext = new ApplContext())
            {
                var result = applContext.Positions.ToList();
                return result;
            }
        }

        public static List<Staff> GetStaffs()
        {
            using (ApplContext applContext = new ApplContext())
            {
                var result = applContext.Staffs.ToList();
                return result;
            }
        }

        public static string CreateDepartment(string name)
        {
            string result = "Уже существует";

            using (ApplContext applContext = new ApplContext())
            {
                bool checkIsExist = applContext.Departments.Any(element => element.Name == name);

                if (!checkIsExist)
                {
                    Department department = new Department { Name = name };
                    applContext.Departments.Add(department);
                    applContext.SaveChanges();
                    result = "Добавлено";
                }
            }
            return result;
        }

        public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Уже существует";

            using (ApplContext applContext = new ApplContext())
            {
                bool checkIsExist = applContext.Positions.Any(element => element.Name == name && element.Salary == salary);

                if (!checkIsExist)
                {
                    Position position = new Position
                    {
                        Name = name,
                        Salary = salary,
                        MaxNumber = maxNumber,
                        DepartmentId = department.Id
                    };
                    applContext.Positions.Add(position);
                    applContext.SaveChanges();
                    result = "Добавлено";
                }
            }
            return result;
        }

        public static string CreateStaff(string name, string lastName, string phone, Position position)
        {
            string result = "Уже существует";

            using (ApplContext applContext = new ApplContext())
            {
                bool checkIsExist = applContext.Staffs.Any(element => element.Name == name && element.LastName == lastName && element.Position == position);

                if (!checkIsExist)
                {
                    Staff staff = new Staff
                    {
                        Name = name,
                        LastName = lastName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    applContext.Staffs.Add(staff);
                    applContext.SaveChanges();
                    result = "Добавлено";
                }
            }
            return result;
        }

        public static string DeleteDepartment(Department department)
        {
            string result = "Отдела не существует";

            using (ApplContext applContext = new ApplContext())
            {
                applContext.Departments.Remove(department);
                applContext.SaveChanges();
                result = "Отдел удален";
            }
            return result;
        }

        public static string DeletePosition(Position position)
        {
            string result = "Должность не существует";

            using (ApplContext applContext = new ApplContext())
            {
                applContext.Positions.Remove(position);
                applContext.SaveChanges();
                result = "Должность удалена";
            }
            return result;
        }

        public static string DeleteStaff(Staff staff)
        {
            string result = "Сотрудник не существует";

            using (ApplContext applContext = new ApplContext())
            {
                applContext.Staffs.Remove(staff);
                applContext.SaveChanges();
                result = "Сотрудник удален";
            }
            return result;
        }

        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Отдела не существует";

            using (ApplContext applContext = new ApplContext())
            {
                Department department = applContext.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                applContext.SaveChanges();
                result = "Отдел изменен";
            }
            return result;
        }

        public static string EditPosition(Position oldPosition, string newName, decimal newSalary, int newMaxNumber, Department newDepartment)
        {
            string result = "Должность не существует";

            using (ApplContext applContext = new ApplContext())
            {
                Position position = applContext.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxNumber = newMaxNumber;
                position.Dapartment.Id = newDepartment.Id;
                applContext.SaveChanges();
                result = "Должность изменена";
            }
            return result;
        }

        public static string EditStaff(Staff oldStaff, string newName, string newLastName, string newPhone, Position newPosition)
        {
            string result = "Сотрудника не существует";

            using (ApplContext applContext = new ApplContext())
            {
                Staff staff = applContext.Staffs.FirstOrDefault(s => s.Id == oldStaff.Id);
                staff.Name = newName;
                staff.LastName = newLastName;
                staff.Phone = newPhone;
                staff.Position.Id = newPosition.Id;
                applContext.SaveChanges();
                result = "Сотрудник изменен";
            }
            return result;
        }
    }
}

