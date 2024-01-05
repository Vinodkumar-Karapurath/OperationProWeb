using System;

namespace AdminWebCore.Models
{
    public class VehicleEditModels
    {
        public int ID { get; set; }
        public string Vehicle { get; set; }
        public DateTime? VehicleExpDate { get; set; }
        public DateTime? InsuranceExpDate { get; set; }
        public DateTime? AuthorizationExpDate { get; set; }
        public string Model { get; set; }
        public string plateNo { get; set; }
        public byte[] PDFFile { get; set; }
        public string ContentType { get; set; }

        public string Type { get; set; }
        public string EMPID { get; set; }

        public bool isUpdate { get; set; }
    }
}
