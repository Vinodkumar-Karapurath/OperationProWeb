using System;

namespace AdminWebCore.Models
{
    public class PaymentEditModels
    {
        public Int64 ID { get; set; }
        public DateTime? TranscationDate { get; set; }
        public string Note { get; set; }
        public string Site { get; set; }
        public string Sapinput { get; set; }
       
        public decimal amount { get; set; }

        public int opttype { get; set; }
        public int isUpdate { get; set; }
        public int status { get; set; }
    }
}
