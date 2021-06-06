using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAK.Models
{
    public class ad_view_model
    {
        public int pro_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required..!!!")]
        public string pro_name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Image is required..!!!")]
        public string pro_img { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required..!!!")]
        public string pro_desc { get; set; }

        public Nullable<int> cat_id_fk { get; set; }
        public Nullable<int> pro_adm_id_fk { get; set; }


        public int cat_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required..!!!")]
        public string cat_name { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required..!!!")]
        public string ad_name { get; set; }       


    }
}