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
    public class VehicleEditModel : PageModel
    {
        private IConfiguration Config { get; set; }

        public VehicleEditModels vehicleModels = null;
        public int id = 0;

        public List<MasterModel> Type = null;
        public List<MasterModel> EMPID = null;

        [BindProperty]
        public IFormFile PDFFile { get; set; }

        public VehicleEditModel(IConfiguration configuration)
        {
            Config = configuration;
            

        }

        public void OnGet(int IDS)
        {
            id = IDS;
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            vehicleModels = db.GetVehicleEdit(IDS);
            Type = db.MasterList("vehicle");
            EMPID = db.MasterList("staff");
        }
        public JsonResult OnPostGetPDF(int fileId, int doc)
        {
            //, int MasterID
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));

            JsonResult result = db.GetVehiclePDF(fileId);
            vehicleModels = db.GetVehicleEdit(fileId);
            Type = db.MasterList("vehicle");
            EMPID = db.MasterList("staff");

            return result;
        }

        public void OnPostSubmit(AdminWebCore.Models.VehicleEditModels vehicles)
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));

            MemoryStream photoms = new MemoryStream();

     
            if (PDFFile != null)
            {
                PDFFile.CopyTo(photoms);
                vehicles.ContentType = PDFFile.ContentType;
                vehicles.PDFFile = photoms.ToArray();
            }

           // string result = db.VehicleUpdate(vehicles);

            photoms.Dispose();

            vehicleModels = db.GetVehicleEdit(vehicles.ID);
            Type = db.MasterList("vehicle");
            EMPID = db.MasterList("staff");

        }
    }
}
