﻿using Microsoft.AspNetCore.Http;

namespace AdminWebCore.Models
{
    public class StaffModel
    {
        
        public int  ID {  get; set; }
        public string EmpName { get; set; }
        public string EMPID { get; set; }
        public string TGID { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string IqamaID { get; set; }
        public string IqamaExpDate { get; set; }
        public string SARID { get; set; }
        public string SARExpDate { get; set; }
        public string Passport { get; set; }
        public string PassportExpDate { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }

        public string IqamacontentType { get; set; }
        public byte[] iqamaData { get; set; }

        public string SARcontentType { get; set; }
        public byte[] SARData { get; set; }

        public string PassportcontentType { get; set; }
        public byte[] PassportData { get; set; }

        public byte[] PhotoData { get; set; }
    }
}
