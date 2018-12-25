using Epione.Domain;
using Epione.Service;
using Epione.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Epione.Web.Controllers
{
    public class AppointmentController : Controller
    {
        ServiceAppointment serviceAppointment = new ServiceAppointment();
        // GET: Appointment
        public async Task<ActionResult> Index(string searchString,string searshDate)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");
          //  serviceAppointment.smsSend("94453981");

            
            var list = await serviceAppointment.getAppointmentByIdPatientAsync(currentUser.id);
            if (!String.IsNullOrEmpty(searchString))
            {
                //listfilms = listfilms.Where(m => m.Titre.Contains(searchString)).ToList();

                list = await serviceAppointment.searshByTitle(searchString);
               // searchString = ""; 

            }

            if (!String.IsNullOrEmpty(searshDate))
            {
                //listfilms = listfilms.Where(m => m.Titre.Contains(searchString)).ToList();

                list = await serviceAppointment.searshByDate(searshDate);
              //  searshDate = "";

            }

            var apps = new List<AppointmentVM>(); 
            foreach(Appointment a in list)
            {
                apps.Add(new AppointmentVM() {
                    title = a.title,
                    message=a.message, 
                    object_appointment=a._object, 
                    start_Hour=a.start_hour, 
                    end_Hour=a.endHour,
                    state = a.state, 
                    date_appointment=a.date_appointment, 
                    id=a.id,
                    id_patient=a.patient.id, 
                    id_doctor=a.doctor.id,
                    doctor=a.doctor,

                });
            }
            return View(apps);
        }

        // GET: Appointment/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");
         Appointment app =   await serviceAppointment.getAppointmentById(id);
            AppointmentVM appVm = new AppointmentVM() {
                id = app.id, 
                title=app.title, 
                message=app.message, 
                object_appointment=app._object, 
                date_appointment=app.date_appointment, 
                start_Hour=app.start_hour, 
                end_Hour=app.start_hour,
                state=app.state, 
                
                doctor = app.doctor,
                patient=app.patient

            };

            return View(appVm);
        }

        // GET: Appointment/Create
        public async Task<ActionResult> Create(int id)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");
            user app = await serviceAppointment.getDocById(id);
            AppointmentVM appp = new AppointmentVM() { doctor=app };
            ViewBag.name = app.firstName;
            ViewBag.name2 = app.lastName;
            return View(appp);
        }

        // POST: Appointment/Create
        [HttpPost]
        public async Task<ActionResult> Create(int id,AppointmentVM appointment)
        {
            //cuurent user and redirection to login 
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");
            // initialisation des valeurs 
            int test = 0;
            int test2 =0;
            int test3 =0;
            DayOfWeek day = DayOfWeek.Monday;
            int compare = 0;

            // var declaration bool 
            bool res = false;
            bool existeDeja = false;
            DayOfWeek today = DateTime.Today.DayOfWeek; // day of week metier 
            bool holiday = false;

            user app = await serviceAppointment.getDocById(id);
            Appointment e1 = new Appointment()
            {
                title = appointment.title,
                message = appointment.message,
                _object = appointment.object_appointment,
                date_appointment = appointment.date_appointment,
                start_hour = appointment.start_Hour,
                state = "running",
                note = 0,
                endHour = "End Hour",
                doctor = app,
                patient = currentUser
            };
            if (e1.date_appointment == null)
            {
                test = 0;
                test2 = 0;
                test3 = 0;

            }
            else
            {
                //services decalaration 
                test = e1.date_appointment.Value.Month;
                test2 = e1.date_appointment.Value.Year;
                test3 = e1.date_appointment.Value.Day;
                day = e1.date_appointment.Value.DayOfWeek;
            }


            //
            var listAppointment = await serviceAppointment.getAppointmentByIdPatientAsync(currentUser.id);// accepted
            var listRequests = await serviceAppointment.getRequestsByIdPatientAsync(currentUser.id);// Requests
            var listRefused = await serviceAppointment.getRefusedByIdPatientAsync(currentUser.id);// Refused

           
           
            
            AppointmentVM appp = new AppointmentVM() { doctor = app };
            ViewBag.name = app.firstName;
            ViewBag.name2 = app.lastName;
            // verification day of week 
            if ((day == DayOfWeek.Sunday) || (day == DayOfWeek.Saturday)) holiday = true;


            //same Date Verification


            foreach (Appointment a in listAppointment)
            {
                if ((a.date_appointment.Value.Month == test) && (a.date_appointment.Value.Year == test2) && (a.date_appointment.Value.Day == test3)) existeDeja = true;

            }

            foreach (Appointment a in listRequests)
            {
                if ((a.date_appointment.Value.Month == test) && (a.date_appointment.Value.Year == test2) && (a.date_appointment.Value.Day == test3)) existeDeja = true;

            }

            foreach (Appointment a in listRefused)
            {
                if ((a.date_appointment.Value.Month == test) && (a.date_appointment.Value.Year == test2) && (a.date_appointment.Value.Day == test3)) existeDeja = true;

            }
            if(holiday)
            {
                TempData["Message3"] = "Sorry , Sunday and saturday are Holiday there is not work... Change date";
            }
            if (existeDeja)
            {
                
                TempData["Message2"] = "Sorry , you cant Enter two Appointments in the same day... Change date";
            }
            if (e1.date_appointment != null)
            {
                compare = DateTime.Compare(e1.date_appointment.Value, DateTime.Today);
            }
             

            if(compare<0)
            {
                TempData["Message4"] = "Sorry , Date infernieur of today date Plz... Change date !";
            }
            if (compare == 0)
            {
                TempData["Message4"] = "Sorry ,  today date ignred date Plz... Change date !";
            }

            if ((!ModelState.IsValid) || (e1.date_appointment==null) || (holiday)||(compare<=0) ||(existeDeja))
            {
                TempData["Message"] = "Verify your Informations";

                return View(appp);
            }

           

            
            TempData["Message"] = " Added Successfully ..";
            // controle de la metier 
           
            //
            
            int count = 0; 
            foreach(Appointment a in listAppointment)
            {
                if ((a.date_appointment.Value.Month == test) && (a.date_appointment.Value.Year == test2)) count++;

            }
            if (count == 2)
            {
                res = true;
                TempData["Message"] = "Added Successfully .., Un autre rendez-vous accepter et vous aurrier un rdv gratuit ";
            }
            if (count >= 3)
            {
                res = true;
                TempData["Message"] = "Félicitations , Un rendez vous gratuit Vous avez Atteint Plus que 3 Rendez...";
            }

             bool added = await serviceAppointment.RegisterAppointment(e1);
          //  if (serviceAppointment.getAppointmentGratuit(currentUser.id, e1) >=3)
            
               return RedirectToAction("Index2");
            
         
              //  return RedirectToAction("Index3");
            
                
            
        }

        // GET: Appointment/Edit/5
        public async  Task<ActionResult> Edit(int id)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");

            var app = await serviceAppointment.getAppointmentById(id);
            user user1 = await serviceAppointment.getDocById(app.doctor.id);
            var appointment = new AppointmentVM() {
                id =app.id,
                title=app.title, 
                message=app.message, 
                object_appointment=app._object,
                date_appointment=app.date_appointment,
                state=app.state, 
                start_Hour=app.start_hour,
                end_Hour=app.endHour,
                doctor = new user() { id = 1, role = "doctor" },
                patient = currentUser,



            }; 
              return View(appointment);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditAsync(int id,AppointmentVM app)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");

            int test = 0;
            int test2 = 0;
            int test3 = 0;
            DayOfWeek day = DayOfWeek.Monday;
            int compare = 0;


            bool res = false;
            bool existeDeja = false;
            DayOfWeek today = DateTime.Today.DayOfWeek; // day of week metier 
            bool holiday = false;


            //DayOfWeek day = app.date_appointment.Value.DayOfWeek;


            //
            var listAppointment = await serviceAppointment.getAppointmentByIdPatientAsync(currentUser.id);// accepted
            var listRequests = await serviceAppointment.getRequestsByIdPatientAsync(currentUser.id);// Requests
            var listRefused = await serviceAppointment.getRefusedByIdPatientAsync(currentUser.id);// Refused




         
            // verification day of week 
            


            var e1 = new Appointment()
            {
                id = id,
                title = app.title,
                message = app.message,
                _object = app.object_appointment,
                
                date_appointment = app.date_appointment,
                start_hour = app.start_Hour,
                endHour = "End Hour",
                state = "running",
                 //note = (int) app.note,
                // appointment.note = note.ToString();
                doctor = new user() { id = 1, role = "doctor" },
                patient = currentUser
            };

            if (e1.date_appointment == null)
            {
                test = 0;
                test2 = 0;
                test3 = 0;

            }
            else
            {
                //services decalaration 
                test = e1.date_appointment.Value.Month;
                test2 = e1.date_appointment.Value.Year;
                test3 = e1.date_appointment.Value.Day;
                day = e1.date_appointment.Value.DayOfWeek;
            }

            if ((day == DayOfWeek.Sunday) || (day == DayOfWeek.Saturday)) holiday = true;
            foreach (Appointment a in listAppointment)
            {
                if ((a.date_appointment.Value.Month == test) && (a.date_appointment.Value.Year == test2) && (a.date_appointment.Value.Day == test3)) existeDeja = true;

            }

            foreach (Appointment a in listRequests)
            {
                if ((a.date_appointment.Value.Month == test) && (a.date_appointment.Value.Year == test2) && (a.date_appointment.Value.Day == test3)) existeDeja = true;

            }

            foreach (Appointment a in listRefused)
            {
                if ((a.date_appointment.Value.Month == test) && (a.date_appointment.Value.Year == test2) && (a.date_appointment.Value.Day == test3)) existeDeja = true;

            }
            if (holiday)
            {
                TempData["Message3"] = "Sorry , Sunday and saturday are Holiday there is not work... Change date";
            }
            if (existeDeja)
            {

                TempData["Message2"] = "Sorry , you cant Enter two Appointments in the same day... Change date";
            }
            if (e1.date_appointment != null)
            {
                compare = DateTime.Compare(e1.date_appointment.Value, DateTime.Today);
            }


            if (compare < 0)
            {
                TempData["Message4"] = "Sorry , Date infernieur of today date Plz... Change date !";
            }
            if (compare == 0)
            {
                TempData["Message4"] = "Sorry ,  today date ignred date Plz... Change date !";
            }

            if ((!ModelState.IsValid) || (e1.date_appointment == null) || (holiday) || (compare <= 0) || (existeDeja))

            { 
                TempData["Message"] = "Undate Cancelled Please , Verify your Informations";

                return RedirectToAction("Index");
            }

            TempData["Message"] = "Appointment Updated Succefully";






            await serviceAppointment.UpdateAppointment(e1);


            return RedirectToAction("Index3");



            // il manque le test 

          //  return RedirectToAction("Index");






            //return RedirectToAction("Index");
        }

        // GET: Appointment/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");

            Appointment app = await serviceAppointment.getAppointmentById(id); 
            AppointmentVM appVm = new AppointmentVM()
            {
                title= app.title,
                message=app.message, 
                object_appointment=app._object, 
                date_appointment=app.date_appointment, 
                start_Hour=app.start_hour, 
                end_Hour=app.start_hour, 
                state=app.state,
                id=app.id,
                doctor=app.doctor,


            };

            return View(appVm);
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");

            
            if (await serviceAppointment.DeleteAppointment(id))
            {
                TempData["Message"] = "Appointment Deleted Successfully";
            }

            return RedirectToAction("Index");
           
        }

        public async Task<ActionResult> Index2(string searshString)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");
            var list = await serviceAppointment.getRefusedByIdPatientAsync(currentUser.id);
            if (!String.IsNullOrEmpty(searshString))
            {
                //listfilms = listfilms.Where(m => m.Titre.Contains(searchString)).ToList();

                list = await serviceAppointment.searshByTitle(searshString);
                searshString = "";

            }
            var apps = new List<AppointmentVM>();
            foreach (Appointment a in list)
            {
                apps.Add(new AppointmentVM()
                {
                    title = a.title,
                    message = a.message,
                    object_appointment = a._object,
                    start_Hour = a.start_hour,
                    end_Hour = a.endHour,
                    state = a.state,
                    date_appointment = a.date_appointment,
                    id = a.id,
                    id_patient = a.patient.id,
                    id_doctor = a.doctor.id,
                    doctor = a.doctor,

                });
            }
            return View(apps);
        }

        public async Task<ActionResult> Index3(string searchString)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");

            var list = await serviceAppointment.getRequestsByIdPatientAsync(currentUser.id);
            if (!String.IsNullOrEmpty(searchString))
            {
                //listfilms = listfilms.Where(m => m.Titre.Contains(searchString)).ToList();

                list = await serviceAppointment.searshByTitle(searchString);
                searchString = "";

            }
            var apps = new List<AppointmentVM>();

            foreach (Appointment a in list)
            {
                apps.Add(new AppointmentVM()
                {
                    title = a.title,
                    message = a.message,
                    object_appointment = a._object,
                    start_Hour = a.start_hour,
                    end_Hour = a.endHour,
                    state = a.state,
                    date_appointment = a.date_appointment,
                    id = a.id,
                    id_patient = a.patient.id,
                    id_doctor = a.doctor.id,
                    doctor = a.doctor,

                });
            }
            return View(apps);
        }

        public async Task<ActionResult> rechDoctorAsync(string searchStringVille, string speciality)
        {
            var currentUser = (user)System.Web.HttpContext.Current.Session["IUser"];
            if (currentUser == null)
                return RedirectToAction("Login", "Home");
            if (!currentUser.role.Equals("patient"))
                return RedirectToAction("Index", "Home");

            var list = await serviceAppointment.getDoctorsAsync();
            if (!String.IsNullOrEmpty(searchStringVille))
            {
                //listfilms = listfilms.Where(m => m.Titre.Contains(searchString)).ToList();

                list = await serviceAppointment.searshByCity(searchStringVille);
                // searchString = ""; 

            }

            TempData["second"] = "Second Step .. ! You have to check the formulaire with valid informations";




            var apps = new List<DocotorAppointmentVm>();
            foreach (user a in list)
            {
                apps.Add(new DocotorAppointmentVm()
                {
                    id = a.id,
                    adress = a.adress,
                    birthDate = a.birthDate,
                    email = a.email,
                    phone = a.phone,
                    firstName = a.firstName,
                    lastName = a.lastName,
                    speciality_s = a.speciality_s,
                    motif_s = a.motif_s,
                    creationDate = a.creationDate,
                    lastLogin=a.lastLogin
                    






                });
            }
            return View(apps);

        }


        [HttpPost]
        public async Task<ActionResult> sendMail(int id)
        {
            
            
            if (await serviceAppointment.sendMail(id))
            {
                TempData["Message5"] = "Email Remember Sended Succeffuly";
            }

            return RedirectToAction("Index");
           //return RedirectToAction("Index");
        }


      







    }
}
