//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JZWineNB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Wine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wine()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int WineID { get; set; }

        //convert first letter to cap in each word
        
        private string _name;
        [Required]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    return _name;
                }
                return _name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_name);

            }

            set { _name = value; }
        }

        [Required]
        [Display(Name="Type")]
        public string WineType { get; set; }

        
        private string _grape;
        [Required]
        public string Grape
        {
            get
            {
                if (string.IsNullOrEmpty(_grape))
                {
                    return _grape;
                }
                return _grape = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_grape);

            }

            set { _grape = value; }

        }

        
        private string _region;
        [Required]
        public string Region
        {
            get
            {
                if (string.IsNullOrEmpty(_region))
                {
                    return _region;
                }
                return _region = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_region);

            }

            set { _region = value; }
        }

        
        private string _country;
        [Required]
        public string Country
        {
            get
            {
                if (string.IsNullOrEmpty(_country))
                {
                    return _country;
                }
                return _country = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_country);

            }
            set { _country = value; }
        }
        
        public string Description { get; set; }
        public string Body { get; set; }
        public Nullable<decimal> AlcoholVol { get; set; }
        public Nullable<decimal> RateOverall { get; set; }
        public string WineImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
