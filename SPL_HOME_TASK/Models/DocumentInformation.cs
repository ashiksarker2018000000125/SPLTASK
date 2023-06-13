//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPL_HOME_TASK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DocumentInformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentInformation()
        {
            this.MetaDataInformations = new HashSet<MetaDataInformation>();
            this.FileInformations = new HashSet<FileInformation>();
        }
    
        public long DocumentyIdentity { get; set; }
        [Display(Name = "Document Category *")]
        public Nullable<int> CategoryId { get; set; }
        [Display(Name = "DocumentReferenceName *")]
        [Required]
        [MaxLength(150)]
        [MinLength(3)]
        public string DocumentReferenceName { get; set; }
        [Display(Name = "DocumentDate *")]
        [Required]
        public DateTime DocumentDate { get; set; } = DateTime.Now;
        [Display(Name = "DocumentName *")]
        [Required]
        [MaxLength(250)]
        [MinLength(3)]
        public string DocumentName { get; set; }
        [MaxLength(500)]
        [MinLength(3)]
        public string DocumentNameBangla { get; set; }
        [MaxLength(1500)]
        [MinLength(3)]
        public string Description { get; set; }
        public Nullable<bool> Sttaus { get; set; }
    
        public virtual DocumentCategoryInfo DocumentCategoryInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetaDataInformation> MetaDataInformations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileInformation> FileInformations { get; set; }
    }
}