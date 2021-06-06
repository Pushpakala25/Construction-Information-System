using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectAK.Models
{
    public class CrClass
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required..!!!")]
        public string CrName { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is required..!!!")]
        [DataType(DataType.EmailAddress)]
        public string CrEmail { get; set; }

        [Display(Name = "Resume")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Upload your Resume...!!!")]
        public string CrFile { get; set; }

        [Display(Name = "Message")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Type Your Message Here...!!!")]
        public string CrMsg { get; set; }

    }
}