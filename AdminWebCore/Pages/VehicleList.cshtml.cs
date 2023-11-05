using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AdminWebCore.Pages
{
    public class VehicleListModel : PageModel
    {
        private IConfiguration Config { get; set; }

        public List<VehicleModel> vehicleModel = null;

        public int Type = 0;
        public string condtion = "";

        public VehicleListModel(IConfiguration configuration)
        {
            Config = configuration;

        }

        public void OnGet()
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            vehicleModel = db.VehicleList(Type, condtion);
        }
    }
}
