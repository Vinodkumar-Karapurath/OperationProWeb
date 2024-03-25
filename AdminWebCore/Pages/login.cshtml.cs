using AdminWebCore.Class;
using AdminWebCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdminWebCore.Pages
{
    
    public class loginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }

        private IConfiguration Config { get; set; }

        public loginModel(IConfiguration configuration)
        {
            Config = configuration;

            //DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));

        }

        public async Task<IActionResult> OnPost()
        {
            DatabaseMiddleware db = new DatabaseMiddleware((Config["ConnectionStrings:DefaultConnection"]));
            
            if(db.Login(UserName) == Password)
            { 
                //var passwordHasher = new PasswordHasher<string>();
               // if (passwordHasher.VerifyHashedPassword(null, "1235", Password) == PasswordVerificationResult.Success)
               
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("/index");
                
            }
            Message = "Invalid attempt";
            return Page();
        }
    }
}
