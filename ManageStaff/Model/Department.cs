using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageStaff.Model
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Position> Positions { get; set; }

        [NotMapped]
        public List<Position> DepartmentPosition
        {
            get
            {
                return DataWorker.GetAllPositionsByDepartmentId(Id);
            }
        }
    }
}
