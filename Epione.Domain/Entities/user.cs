namespace Epione.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Claims;

    [Table("epione.user")]
    public partial class user 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            appointments = new HashSet<Appointment>();
            appointments1 = new HashSet<Appointment>();
            courses = new HashSet<course>();
            discussions = new HashSet<discussion>();
            discussions1 = new HashSet<discussion>();
            evaluations = new HashSet<evaluation>();
            notifications = new HashSet<notification>();
            notifications1 = new HashSet<notification>();
            soins = new HashSet<soin>();
        }

        [Required]
        [StringLength(31)]
        public string user_type { get; set; }

        public int id { get; set; }

        [Required]
        public Adresse adress { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthDate { get; set; }
        [JsonIgnore]
        public DateTime? creationDate { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string firstName { get; set; }

        [JsonIgnore]
        public DateTime? lastLogin { get; set; }

        [StringLength(255)]
        public string lastName { get; set; }

        [StringLength(255)]
        public string login { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        [StringLength(255)]
        public string phone { get; set; }

        [StringLength(255)]
        public string photo { get; set; }

        [StringLength(255)]
        public string role { get; set; }

        [StringLength(255)]
        public string sexe { get; set; }

        [StringLength(255)]
        public string token { get; set; }

        [StringLength(255)]
        public string numberSocialSecurity { get; set; }

        public int? treated { get; set; }

        [StringLength(255)]
        public string codeDoctor { get; set; }

        [JsonIgnore]
        public int? flag { get; set; }

        [StringLength(255)]
        [JsonIgnore]
        public string motif_s { get; set; }

        [StringLength(255)]
        [JsonIgnore]
        public string speciality_s { get; set; }

        public int? course_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> appointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> appointments1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<course> courses { get; set; }

        public virtual course course { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<discussion> discussions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<discussion> discussions1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<evaluation> evaluations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notification> notifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notification> notifications1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<soin> soins { get; set; }

        public bool ShouldSerializesoins()
        {
            return false;
        }

        public bool ShouldSerializecodeDoctor()
        {
            return role.Equals("doctor");
        }

        public bool ShouldSerializeactivation_account()
        {
            return role.Equals("doctor");
        }

        public bool ShouldSerializemotif_s()
        {
            return false;
        }

        public bool ShouldSerializespeciality_s()
        {
            return false;
        }

        public bool ShouldSerializenumberSocialSecurity()
        {
            return role.Equals("patient");
        }

        public bool ShouldSerializetreated()
        {
            return role.Equals("patient");
        }

        public bool ShouldSerializenotifications()
        {
            return false;
        }
        public bool ShouldSerializenotifications1()
        {
            return false;
        }

        public bool ShouldSerializeevaluations()
        {
            return false;
        }

        public bool ShouldSerializediscussions()
        {
            return false;
        }

        public bool ShouldSerializediscussions1()
        {
            return false;
        }

        public bool ShouldSerializecourses()
        {
            return false;
        }
        public bool ShouldSerializeappointments1()
        {
            return false;
        }
        public bool ShouldSerializeappointments()
        {
            return false;
        }
        public bool ShouldSerializecourse()
        {
            return false;
        }
        public bool ShouldSerializecourse_id()
        {
            return false;
        }
        public bool ShouldSerializetoken()
        {
            return false;
        }
        public bool ShouldDeserializetoken()
        {
            return false;
        }
        public bool ShouldSerializeuser_type()
        {
            return false;
        }

        public class Adresse
        {
           public string rue;
           public string ville;
        }
    }
}
