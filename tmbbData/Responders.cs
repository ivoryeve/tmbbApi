using System.ComponentModel.DataAnnotations;

namespace tmbbData
{
    public class Responders
    {
        [Key]
        public int pro_uid { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string xname { get; set; }
        public string gender { get; set; }
        public DateTime birthdate { get; set; }
        public DateTime baptism_date { get; set; }
        public string fullname { get; set; }
        public string cp_no { get; set; }
        public string email { get; set; }
        public string fb_acc { get; set; }    

    }
}