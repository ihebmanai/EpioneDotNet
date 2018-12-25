using Epione.Domain;
using ServicePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epione.Service
{
    public interface IServiceAppointment : IService<Appointment>
    {
        Task<Appointment> getAppointmentById(int idApp);
        Task<user> getDoctors();
        Task<List<Appointment>> getAppointmentByIdPatientAsync(int id);
        Task<List<Appointment>> getRequestsByIdPatientAsync(int id);
        Task<List<Appointment>> getRefusedByIdPatientAsync(int id);
        Task<bool> UpdateAppointment(Appointment app);
        Task<bool> AddAppointment(Appointment app);
        Task<bool> DeleteAppointment(int idApp);
        Task<List<Appointment>> searshByTitle(String title);
        Task<List<Appointment>> searshByDate(String date);
        Task<List<Appointment>> searshByCity(String ville);
        Task<user> getDocById(int id);
        Task<bool> sendMail(int id);
        Task<bool> smsSend(String phoneNumber);
        Task<bool> metierApp(Appointment app);

    }


}
