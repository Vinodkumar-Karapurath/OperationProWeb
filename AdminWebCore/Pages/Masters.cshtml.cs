using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AdminWebCore.Pages
{
    public class MastersModel : PageModel
    {
        private IConfiguration Config { get; set; }

        public List<MasterModel> masterModel = null;
        public string Type;
        
        public MastersModel(IConfiguration configuration)
        {
            Config = configuration;

        }


        public void OnGet(string Types)
        {
            Type= Types;
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            masterModel = db.MasterList(Type);
        }
    }
}
