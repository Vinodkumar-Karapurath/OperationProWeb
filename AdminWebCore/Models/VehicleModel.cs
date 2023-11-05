namespace AdminWebCore.Models
{
    public class VehicleModel
    {
        public int ID { get; set; }
        public string Vehicle { get; set; }
        public string VehicleExpDate { get; set; }
        public string InsuranceExpDate { get; set; }
        public string AuthorizationExpDate { get; set; }
        public string Model { get; set; }
        public string plateNo { get; set; }
        public byte[] PDFFile { get; set; }
        public string ContentType { get; set; }
        public string Type { get; set; }
        public string EmpName { get; set; }
        public string EMPID { get; set; }

    }
}
