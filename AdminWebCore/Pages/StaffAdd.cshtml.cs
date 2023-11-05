using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AdminWebCore.Pages
{
    public class StaffAddModel : PageModel
    {
        private IConfiguration Config { get; set; }

        public List<StaffModel> staffModel = null;

        public List<MasterModel> company = null;
        public List<MasterModel> department = null;
        public List<MasterModel> job = null;
        public List<MasterModel> location = null;
        

        // private IHostingEnvironment _environment;

        [BindProperty]
        public IFormFile StaffPhotofile { get; set; }

        [BindProperty]
        public IFormFile Iqamafile { get; set; }

        [BindProperty]
        public IFormFile SARfile { get; set; }
        [BindProperty]
        public IFormFile Passportfile { get; set; }

        public StaffAddModel(IConfiguration configuration)//, IHostingEnvironment environment)
        {
            Config = configuration;
          //  _environment = environment;

        }
        public void OnGet()
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            company = db.MasterList("company");
            department = db.MasterList("department");
            job = db.MasterList("job");
            location = db.MasterList("location");

        }

        public void OnPostSubmit(AdminWebCore.Models.StaffModel staff)
        { 
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));

            
            
           

            MemoryStream Iqms = new MemoryStream();
            MemoryStream sarms = new MemoryStream();
            MemoryStream passms = new MemoryStream();
            MemoryStream photoms = new MemoryStream();

            if (Iqamafile != null)
            {
                Iqamafile.CopyTo(Iqms);
                staff.IqamacontentType = Iqamafile.ContentType;
                staff.iqamaData = Iqms.ToArray();
            }
            if (SARfile != null)
            {
                SARfile.CopyTo(sarms);
                staff.SARcontentType = SARfile.ContentType;
                staff.SARData = sarms.ToArray();

            }
            if (Passportfile != null)
            {
                Passportfile.CopyTo(passms);
                staff.PassportcontentType = Passportfile.ContentType;
                staff.PassportData = passms.ToArray();
            }
            if (StaffPhotofile != null)
            {
                StaffPhotofile.CopyTo(photoms);
                staff.PhotoData = photoms.ToArray();
            }

            string result = db.StaffSave(staff);

            Iqms.Dispose();
            sarms.Dispose();
            passms.Dispose();
            photoms.Dispose();

            company = db.MasterList("company");
            department = db.MasterList("department");
            job = db.MasterList("job");
            location = db.MasterList("location");

        }
    }
}
