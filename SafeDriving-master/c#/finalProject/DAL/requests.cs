//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class requests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public requests()
        {
            this.travels = new HashSet<travels>();
        }
    
        public int id { get; set; }
        public int id_person { get; set; }
        public string resourse_city { get; set; }
        public string resourse { get; set; }
        public string destination { get; set; }
        public string destination_city { get; set; }
        public int seats { get; set; }
        public string remarks { get; set; }
        public System.DateTime date_time { get; set; }
        public bool active { get; set; }
        public string ignore_offers { get; set; }
        public string email_offers { get; set; }
    
        public virtual persons persons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<travels> travels { get; set; }
    }
}
