//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MunicipalityAccess.Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class AmenityArea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AmenityArea()
        {
            this.MunicipalityAmenities = new HashSet<MunicipalityAmenity>();
        }
    
        public int Id { get; set; }
        public int AmenityId { get; set; }
        public int AreaCode { get; set; }
        public string AreaAdress { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual Amenity Amenity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MunicipalityAmenity> MunicipalityAmenities { get; set; }
    }
}
