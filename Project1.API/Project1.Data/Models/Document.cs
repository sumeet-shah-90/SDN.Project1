using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project1.Data.Enumerations;

namespace Project1.Data.Models
{
    [Table("Document", Schema="DOC")]
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DocumentID { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(200)]
        public string FileName { get; set; }

        public FileType FileType { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public DateTime UploadedTS { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public DateTime LastUpdatedTS { get; set; }

        public bool IsActive { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Share> Shares { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }
    }
}
