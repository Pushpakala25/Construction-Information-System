using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;  

namespace ProjectAK.Models
{
    public class UserClass
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required..!!!")]
        public string Cname { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is required..!!!")]
        [DataType(DataType.EmailAddress)]
        public string Cemail { get; set; }

        [Display(Name = "Mobile")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile No is required..!!!")]
        [MinLength(10,ErrorMessage ="Minimum 10 digits")]
        [MaxLength(10,ErrorMessage ="Maximum 10 digits")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Entered phone format is not valid.")]
        [DataType(DataType.PhoneNumber)]

        public string Cmobile { get; set; }

        [Display(Name = "Message")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tye Your Message Here...!!!")]
        public string Cmsg { get; set; }
    }
}