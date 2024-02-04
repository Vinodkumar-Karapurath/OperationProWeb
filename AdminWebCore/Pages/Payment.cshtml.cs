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
    public class PaymentModel : PageModel
    {
        private IConfiguration Config { get; set; }

        public List<PaymentListModel> paymentModels = null;

        public List<MasterModel> Note = null;
        public List<MasterModel> Site = null;

        public int pagenationcount = 0;

        [BindProperty]
        public IFormFile Pdffile { get; set; }

        public PaymentModel(IConfiguration configuration)//, IHostingEnvironment environment)
        {
            Config = configuration;
            

        }
        

        public void OnGet()
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            Note = db.MasterList("note");
            Site = db.MasterList("site");
            paymentModels = db.PaymentList(10,"");
            pagenationcount = db.PaymentPageCount(10, "");
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
            paymentModels = db.PaymentList(10, "");
            pagenationcount = db.PaymentPageCount(10, "");

        }
    }
}
