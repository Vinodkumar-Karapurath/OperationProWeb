namespace AdminWebCore.Models
{
    public class PaymentAddModel
    {
        public int ID { get; set; }
        public string TranscationDate { get; set; }
        public string Note { get; set; }
        public string Site { get; set; }
        public string Sapinput { get; set; }
        public byte[] PDFFile { get; set; }
        public string ContentType { get; set; }
        public decimal amount { get; set; }

        public int opttype { get; set; }
        public int isUpdate { get; set; }

        public int status { get; set; }





    }
}
