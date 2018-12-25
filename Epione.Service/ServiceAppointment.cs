
using Epione.Data;
using Epione.Data.Infrastructure;
using Epione.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;

namespace Epione.Service
{
    public class ServiceAppointment : Service<Appointment>, IService<Appointment>
    {
        EpioneContext ct = new EpioneContext();
        private static DatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork uof = new UnitOfWork(dbf);
        public ServiceAppointment() : base(uof)
        {


        }



        //appoinments accepted ; 

        public async Task<List<Appointment>> getAppointmentByIdPatientAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
           "http://localhost:8089/Epione-web/appointments/myApp/" + id.ToString());
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JArray>(result);
            var list = jsonObjects.Value<JArray>().ToObject<List<Appointment>>();
            // regler le problem d'object 
            return list;
        }

        //state : ranning 

        public async Task<List<Appointment>> getRequestsByIdPatientAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
          "http://localhost:8089/Epione-web/appointments/req/" + id.ToString());
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JArray>(result);
            var list = jsonObjects.Value<JArray>().ToObject<List<Appointment>>();
            return list;

        }

        //state : refused
        public async Task<List<Appointment>> getRefusedByIdPatientAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
          "http://localhost:8089/Epione-web/appointments/refusedApp/" + id.ToString());
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JArray>(result);
            var list = jsonObjects.Value<JArray>().ToObject<List<Appointment>>();
            return list;

        }

        public async Task<Appointment> getAppointmentById(int idApp)
        {

            var request = new HttpRequestMessage(HttpMethod.Get,
         "http://localhost:8089/Epione-web/appointments/" + idApp.ToString());
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JObject>(result);
            var appointment = jsonObjects.Value<JObject>().ToObject<Appointment>();
            appointment._object = jsonObjects["object"].Value<string>();

            string state = jsonObjects["state"].Value<string>();

            appointment.state = state;
            return appointment;

        }

        public async Task<bool> UpdateAppointment(Appointment app)
        {

            var request = new HttpRequestMessage(HttpMethod.Put,
        "http://localhost:8089/Epione-web/appointments/update");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(app);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> RegisterAppointment(Appointment app)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
            "http://localhost:8089/Epione-web/appointments");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(app);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteAppointment(int idApp)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
           "http://localhost:8089/Epione-web/appointments/" + idApp.ToString());
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(idApp);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;

        }





        public int getAppointmentGratuit(int? idUser, Appointment b)
        {
            int test = b.date_appointment.Value.Month;
            int test2 = b.date_appointment.Value.Year;
            //date
            IEnumerable<Appointment> ListAppointments = ct.appointments.ToList();
            var appointment = from a in ListAppointments
                              where a.date_appointment.Value.Month == test && a.date_appointment.Value.Year == test2 && idUser == a.patient.id
                              select a;
            int taille = appointment.Count();
            return taille;
        }

        public async Task<List<Appointment>> searshByTitle(String title)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
         "http://localhost:8089/Epione-web/appointments/searsh?title=" + title);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JArray>(result);
            var list = jsonObjects.Value<JArray>().ToObject<List<Appointment>>();
            // regler le problem d'object 
            return list;

        }

        public async Task<List<Appointment>> searshByDate(String date)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:8089/Epione-web/appointments/apps?date=" + date);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JArray>(result);
            var list = jsonObjects.Value<JArray>().ToObject<List<Appointment>>();
            // regler le problem d'object 
            return list;

        }
        public async Task<List<user>> getDoctorsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
           "http://localhost:8089/Epione-web/appointments/doctors");
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JArray>(result);
            var list = jsonObjects.Value<JArray>().ToObject<List<user>>();
            // motif null et speciality
            return list;

        }

        public async Task<List<user>> searshByCity(String ville)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:8089/Epione-web/appointments/searshVille?ville=" + ville);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JArray>(result);
            var list = jsonObjects.Value<JArray>().ToObject<List<user>>();
            // regler le problem d'object 
            return list;

        }

        public async Task<user> getDocById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:8089/Epione-web/appointments/doc/" + id.ToString());
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JObject>(result);
            var appointment = jsonObjects.Value<JObject>().ToObject<user>();
            return appointment;

        }

        public bool smsSend(String phoneNumber)
        {
            const string accountSid = "ACa795b92bbc965f514ef1caebb0998eba";
            const string authToken = "6dd740c1982d7495743557811be08023";

            TwilioClient.Init(accountSid, authToken);

            try
            {
                var message = MessageResource.Create(
                    body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                    from: new Twilio.Types.PhoneNumber("+15017122661"),
                    to: new Twilio.Types.PhoneNumber("+21694453981")
                );

                Console.WriteLine(message.Sid);
            }
            catch (ApiException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"Twilio Error {e.Code} - {e.MoreInfo}");
            }

            Console.Write("Press any key to continue.");
            Console.ReadKey();
            return true;


        }

        public async Task<bool> sendMail(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
           "http://localhost:8089/Epione-web/appointments/mail/" + id.ToString());
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            return true;

        }

    }
}
