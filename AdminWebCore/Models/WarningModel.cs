using System;

namespace AdminWebCore.Models
{
    public class WarningModel
    {
        public Nullable<int> id { get; set; }
        public string EMPID { get; set; }
        public string EmpName { get; set; }
        public string Mobile { get; set; }
        public string TypeId { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public string trantypes { get; set; }
        public int VehicleID { get; set; }
    }
}
