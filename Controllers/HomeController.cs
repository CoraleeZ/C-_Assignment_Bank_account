using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank_Accounts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Bank_Accounts.Controllers
{
    public class HomeController : Controller
    {
      private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        ////
        public string Email {get; set;}
        public string Password { get; set; }

//////////////////////////////////////////////////
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost("register")]
        public IActionResult Register(ForLandR lar)
        {
            // Check initial ModelState
            if(ModelState.IsValid)
            {
                // If a User exists with provided email
                if(dbContext.ForLandRes.Any(u => u.Email == lar.Email))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Email", "Email already in use!");
                    
                    // You may consider returning to the View at this point
                    return View("Index");
                }
                else{
                    HttpContext.Session.SetString("Firstname", lar.Firstname);
                    HttpContext.Session.SetInt32("id",lar.ForLandRId);
                    //////
                    PasswordHasher<ForLandR> Hasher = new PasswordHasher<ForLandR>();
                    lar.Password = Hasher.HashPassword(lar, lar.Password);
                    //////
                    dbContext.Add(lar);
                    dbContext.SaveChanges(); 
                    return RedirectToAction("Success");
                }
            }else{
                return View("Index");
            }
            // other code
        } 

                [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Firstname");
            HttpContext.Session.Remove("id");

            return RedirectToAction("Index");
        }


        [HttpPost("log_in")]
        public IActionResult Log_in(ForL lar)
        {
            if(ModelState.IsValid)
            {
               
                // If inital ModelState is valid, query for a user with provided email
                var userInDb = dbContext.ForLandRes.FirstOrDefault(u => u.Email == lar.Email);
                // If no user exists with provided email          
                if(userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }else{
                    // Initialize hasher object
                    var hasher = new PasswordHasher<ForL>();
                    
                    // varify provided password against hash stored in db
                    var result = hasher.VerifyHashedPassword(lar, userInDb.Password, lar.Password);
                    
                    // result can be compared to 0 for failure    
                    if(result == 0)
                    { 
                      
                        // handle failure (this should be similar to how "existing email" is handled)
                        ModelState.AddModelError("Password", "Invalid Email/Password");
                        return View("Login");               
                    }
                    else{                
                       
                        HttpContext.Session.SetString("Firstname", userInDb.Firstname);
                        HttpContext.Session.SetInt32("id",userInDb.ForLandRId);
                        return RedirectToAction("Success"); 
                    }
                }
            }else{
                return View("Login");
            }

        }
//////////////////////////////////////////////////////////////////////////////

        [HttpGet("success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetString("Firstname")!=null){
                List<Transaction> Alltran=dbContext.Transactiones.Include(x=>x.Customer).OrderByDescending(x=>x.CreatedAt).ToList();
                decimal sum=dbContext.Transactiones.Sum(x=>x.Amount);
                HttpContext.Session.SetString("sum",sum.ToString());
                Info newinfo=new Info{
                sum=sum,
                tran=Alltran,
                Firstname=HttpContext.Session.GetString("Firstname")
                };
                return View(newinfo);
            }else{
                return RedirectToAction("Index");
            }
        }



        [HttpPost("maketran")]
        public IActionResult MakeTran(Transaction tran)
        {
            if(HttpContext.Session.GetString("Firstname")!=null){


                if(ModelState.IsValid)
                {
                    string sum=HttpContext.Session.GetString("sum");
                    if(tran.Amount<0 && -(tran.Amount)>System.Convert.ToDecimal(sum)){
                        List<Transaction> Alltran=dbContext.Transactiones.Include(x=>x.Customer).OrderByDescending(x=>x.CreatedAt).ToList();
                        HttpContext.Session.SetString("sum",sum.ToString());
                        Info newinfo=new Info{
                            sum=System.Convert.ToDecimal(sum),
                            tran=Alltran,
                            Firstname=HttpContext.Session.GetString("Firstname")
                        };
                        ModelState.AddModelError("Amount", "You can not withdraw more than your balance!");
                        return View("Success",newinfo);
                    }
                    else
                    {
                        tran.ForLandRId=(int)HttpContext.Session.GetInt32("id");
                        dbContext.Add(tran);
                        dbContext.SaveChanges();
                        return RedirectToAction("Success");
                    }

                }
                else{
                    List<Transaction> Alltran=dbContext.Transactiones.Include(x=>x.Customer).OrderByDescending(x=>x.CreatedAt).ToList();
                    decimal sum=dbContext.Transactiones.Sum(x=>x.Amount);
                    HttpContext.Session.SetString("sum",sum.ToString());
                    Info newinfo=new Info{
                        sum=sum,
                        tran=Alltran,
                        Firstname=HttpContext.Session.GetString("Firstname")
                    };
                    return View("Success",newinfo);
                }

           
            }else{
                return RedirectToAction("Index");
            }

        }

    }
}
