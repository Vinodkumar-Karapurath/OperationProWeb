using Microsoft.AspNetCore.Mvc;

namespace AdminWebCore.Models
{
    public class MastersAddModel
    {
        [BindProperty]
        public string IDS { get; set; }

        [BindProperty]
        public string Type { get; set; }

        [BindProperty]
        public string MasterName { get; set; }
    }
}
