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

    public partial class category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public category()
        {
            this.bookings = new HashSet<booking>();
            this.people = new HashSet<person>();
        }
    
        public int cat_id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name is required...!!!")]
        public string cat_name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Image is required...!!!")]
        public string cat_img { get; set; }
        public Nullable<int> ad_id_fk { get; set; }
        public int cat_statuss { get; set; }
    
        public virtual admin admin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<person> people { get; set; }
    }
}
