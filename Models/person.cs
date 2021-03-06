//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectAK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public person()
        {
            this.bookings = new HashSet<booking>();
        }
    
        public int pro_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Designer Name is required...!!!")]
        public string pro_name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Designer Image is required...!!!")]
        public string pro_img { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Designer Description is required...!!!")]
        public string pro_desc { get; set; }
        public Nullable<int> cat_id_fk { get; set; }
        public Nullable<int> pro_adm_id_fk { get; set; }
    
        public virtual admin admin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }
        public virtual category category { get; set; }
    }
}
