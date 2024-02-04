

using System;

namespace AdminWebCore.Models
{
    public class PaymentListModel
    {
        public Int64 ID { get; set; }
        public string  Details { get; set; }
        public string Trandate { get; set; }
        public string SIte { get; set; }
        public string SAPNumber { get; set; }

        public decimal? AdvancePayment { get; set; }
        public decimal? credit { get; set; }
        public decimal? Debit { get; set; }
        public string Cleared { get; set; }

    }
}
