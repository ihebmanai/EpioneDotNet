﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Epione.Domain;
using Epione.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Epione.Web.Extensions;
using Epione.Web.Models;
using static Epione.Domain.user;

namespace Epione.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
           
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetUserById(int Id)
        {

            ServiceUser serviceUser = new ServiceUser();
            return Json(await serviceUser.getUserByIdAsync(Id));
        }

        public async Task<ActionResult> Login()
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if(currentUser!=null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            ServiceUser serviceUser = new ServiceUser();
            user currentUser = await serviceUser.loginUserAsync(model.Email, model.Password);
            if(currentUser!=null)
            {
                System.Web.HttpContext.Current.Session.Add("IUser", currentUser);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Le mot de passe et/ou adresse saisie est/sont incorrectes.");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LogOff(LoginViewModel model)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }
            System.Web.HttpContext.Current.Session.Remove("IUser");
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Register()
        {
            return View();
        }

        public async Task<ActionResult> RegisterDoctor()
        {
            return View();
        }

        public async Task<ActionResult> RegisterPatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterDoctor(RegisterDoctorViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Sexe.Equals("M"))
            {
                model.Sexe = "homme";
            }
            else
            {
                model.Sexe = "femme";
            }
            Adresse adresse = new Adresse
            {
                rue = model.Street,
                ville = model.City
            };
            user user = new user
            {
                firstName = model.FirstName,
                lastName = model.LastName,
                password = model.Password,
                phone = model.Phone,
                sexe = model.Sexe,      
                adress=adresse,
                email = model.Email,
                codeDoctor = model.Code,
                flag = 0,
                user_type = "doctor",
                role ="doctor"
            };
            ServiceUser serviceUser = new ServiceUser();
           if( await serviceUser.RegisterUser(user))
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterPatient(RegisterPatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.Sexe.Equals("M"))
            {
                model.Sexe = "homme";
            }
            else
            {
                model.Sexe = "femme";
            }
            Adresse adresse = new Adresse
            {
                rue = model.Street,
                ville = model.City
            };
            user user = new user
            {
                firstName = model.FirstName,
                lastName = model.LastName,
                password = model.Password,
                phone = model.Phone,
                sexe = model.Sexe,
                adress = adresse,
                email = model.Email,
                user_type = "patient",
                role = "patient"
            };
            ServiceUser serviceUser = new ServiceUser();
            if (await serviceUser.RegisterUser(user))
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}