using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdminWebCore.Pages
{
    public class StaffEditModel : PageModel
    {

        private IConfiguration Config { get; set; }

        public StaffEditModels staffModel = null;

        public List<MasterModel> company = null;
        public List<MasterModel> department = null;
        public List<MasterModel> job = null;
        public List<MasterModel> location = null;
        public int id = 0;

        // private IHostingEnvironment _environment;

        [BindProperty]
        public IFormFile StaffPhotofile { get; set; }

        [BindProperty]
        public IFormFile Iqamafile { get; set; }

        [BindProperty]
        public IFormFile SARfile { get; set; }
        [BindProperty]
        public IFormFile Passportfile { get; set; }

        

        public StaffEditModel(IConfiguration configuration)//, IHostingEnvironment environment)
        {
            Config = configuration;
            //  _environment = environment;

        }
        public void OnGet(int IDS)
        {
            id = IDS;
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            staffModel  = db.GetStaffEdit(IDS);
            company = db.MasterList("company");
            department = db.MasterList("department");
            job = db.MasterList("job");
            location = db.MasterList("location");
        }

        public JsonResult OnPostGetPDF(int fileId, int DOCID )
        {
            //, int MasterID
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));

            JsonResult result = db.GetPDF(DOCID, fileId);

            return result;
        }
    }
}
