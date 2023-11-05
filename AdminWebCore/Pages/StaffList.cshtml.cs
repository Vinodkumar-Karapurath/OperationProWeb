using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AdminWebCore.Pages
{
    public class StaffListModel : PageModel
    {
        private IConfiguration Config { get; set; }

        public List<StaffModel> staffModel = null;

        public int Type =0;
        public string condtion = "";

        public StaffListModel(IConfiguration configuration)
        {
            Config = configuration;

        }

        public void OnGet()
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            staffModel = db.StaffList(Type, condtion);
        }
    }
}
