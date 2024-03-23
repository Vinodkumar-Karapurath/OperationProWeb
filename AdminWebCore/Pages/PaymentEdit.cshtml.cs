using AdminWebCore.Class;
using AdminWebCore.Models;
using CoreUI_Free_Bootstrap_Admin.Pages.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace AdminWebCore.Pages
{
    public class PaymentEditModel : PageModel
    {
        private IConfiguration Config { get; set; }

        public PaymentEditModels paymentModels = null;

        public List<MasterModel> Note = null;
        public List<MasterModel> Site = null;


        [BindProperty]
        public IFormFile Pdffile { get; set; }

        public PaymentEditModel(IConfiguration configuration)//, IHostingEnvironment environment)
        {
            Config = configuration;
            

        }
        

        public void OnGet(int IDS)
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            Note = db.MasterList("note");
            Site = db.MasterList("site");
            paymentModels = db.PaymentDetails(IDS);
        }

        public void OnPostSave(AdminWebCore.Models.PaymentAddModel payment)
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));

            MemoryStream photoms = new MemoryStream();
                     
            if( Pdffile != null)
            {
               Pdffile.CopyTo(photoms);
                payment.PDFFile = photoms.ToArray();
                payment.ContentType = Pdffile.ContentType;
            }

            string result = db.PaymentSave(payment);

            photoms.Dispose();

            Note = db.MasterList("note");
            Site = db.MasterList("site");
            paymentModels = db.PaymentDetails(payment.ID);
        }

      
    }
}
