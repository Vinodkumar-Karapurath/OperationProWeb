using AdminWebCore.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace AdminWebCore.Pages
{
    public class MasterEditModel : PageModel
    {
        private IConfiguration Config { get; set; }

        // public List<MasterModel> masterModel = null;
        public AdminWebCore.Models.MastersAddModel Type;

        public MasterEditModel(IConfiguration configuration)
        {
            Config = configuration;

        }

        public void OnGet(AdminWebCore.Models.MastersAddModel Types)
        {
            Type = Types;
        }

        public void OnPostSubmit(AdminWebCore.Models.MastersAddModel masters)
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            string result = db.MastersUpdate(masters);
            Type = masters;
        }
    }
}
