namespace Epione.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Epione.Domain;
    public partial class EpioneContext : DbContext
    {
        public EpioneContext()
            : base("name=EpioneContext")
        {
        }

        public virtual DbSet<Appointment> appointments { get; set; }
        public virtual DbSet<availibility> availibilities { get; set; }
        public virtual DbSet<course> courses { get; set; }
        public virtual DbSet<discussion> discussions { get; set; }
        public virtual DbSet<evaluation> evaluations { get; set; }
        public virtual DbSet<message> messages { get; set; }
        public virtual DbSet<notification> notifications { get; set; }
        public virtual DbSet<report> reports { get; set; }
        public virtual DbSet<soin> soins { get; set; }
        public virtual DbSet<treatment> treatments { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.endHour)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e._object)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.start_hour)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.evaluations)
                .WithOptional(e => e.appointment)
                .HasForeignKey(e => e.appointment_id);

            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.notifications)
                .WithOptional(e => e.appointment)
                .HasForeignKey(e => e.appointment_id);

            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.reports)
                .WithOptional(e => e.appointment)
                .HasForeignKey(e => e.appointment_id);

            modelBuilder.Entity<course>()
                .HasMany(e => e.reports)
                .WithOptional(e => e.course)
                .HasForeignKey(e => e.id_course);

            modelBuilder.Entity<course>()
                .HasMany(e => e.users)
                .WithOptional(e => e.course)
                .HasForeignKey(e => e.course_id);

            modelBuilder.Entity<discussion>()
                .HasMany(e => e.messages)
                .WithOptional(e => e.discussion)
                .HasForeignKey(e => e.discussion_id);

            modelBuilder.Entity<evaluation>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<message>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.checkAppointmentType)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e._object)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<report>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<report>()
                .Property(e => e.desease)
                .IsUnicode(false);

            modelBuilder.Entity<report>()
                .Property(e => e.objet)
                .IsUnicode(false);

            modelBuilder.Entity<report>()
                .HasMany(e => e.treatments)
                .WithOptional(e => e.report)
                .HasForeignKey(e => e.report_id);

            modelBuilder.Entity<soin>()
                .Property(e => e.typeSoins)
                .IsUnicode(false);

            modelBuilder.Entity<treatment>()
                .Property(e => e.dateTreatments)
                .IsUnicode(false);

            modelBuilder.Entity<treatment>()
                .Property(e => e.doctorForRecommandation)
                .IsUnicode(false);

            modelBuilder.Entity<treatment>()
                .Property(e => e.justification)
                .IsUnicode(false);

            modelBuilder.Entity<treatment>()
                .Property(e => e.nameTreatment)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.user_type)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.adress.rue)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.adress.ville)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.photo)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.sexe)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.token)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.numberSocialSecurity)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.codeDoctor)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.motif_s)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.speciality_s)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.appointments)
                .WithOptional(e => e.patient)
                .HasForeignKey(e => e.patient.id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.appointments1)
                .WithOptional(e => e.doctor)
                .HasForeignKey(e => e.doctor.id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.courses)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.patient_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.discussions)
                .WithOptional(e => e.doctor)
                .HasForeignKey(e => e.patient_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.discussions1)
                .WithOptional(e => e.patient)
                .HasForeignKey(e => e.doctor_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.evaluations)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.id_patient);

            modelBuilder.Entity<user>()
                .HasMany(e => e.notifications)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.patient_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.notifications1)
                .WithOptional(e => e.user1)
                .HasForeignKey(e => e.doctor_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.soins)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.id_doctor);
        }
    }
}
