using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Data.Models
{
    [Table("Share", Schema="USER")]
    public class Share
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ShareID { get; set; }

        [ForeignKey("Document")]
        public Guid DocumentID { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public DateTime SharedTS { get; set; }

        public bool IsActive { get; set; }

        public virtual Document Document { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Changeset> Changesets { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }
    }
}
