using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AdminWebCore.Pages
{
    public class VehicleAddModel : PageModel
    {
        private IConfiguration Config { get; set; }

        public List<VehicleModel> vehicleModel = null;

        public List<MasterModel> Type = null;
        public List<MasterModel> EMPID = null;

        [BindProperty]
        public IFormFile PDFFile { get; set; }

        public VehicleAddModel(IConfiguration configuration)
        {
            Config = configuration;
            

        }

        public void OnGet()
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            Type = db.MasterList("vehicle");
            EMPID = db.MasterList("staff");
        }

        public void OnPostSubmit(AdminWebCore.Models.VehicleModel vehicles)
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));

            MemoryStream photoms = new MemoryStream();

     
            if (PDFFile != null)
            {
                PDFFile.CopyTo(photoms);
                vehicles.ContentType = PDFFile.ContentType;
                vehicles.PDFFile = photoms.ToArray();
            }

            string result = db.VehicleSave(vehicles);

            photoms.Dispose();

            Type = db.MasterList("vehicle");
            EMPID = db.MasterList("staff");

        }
    }
}
