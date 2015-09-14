using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Data.Models
{
    [Table("Changeset", Schema="USER")]
    public class Changeset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order=1)]
        public Guid ChangesetID { get; set; }

        [ForeignKey("Share")]
        public Guid ShareID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 2)]
        public int ChangesetNumber { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public DateTime PushedAt { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public DateTime PulledAt { get; set; }

        public virtual Share Share { get; set; }
        
        [ConcurrencyCheck]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }
    }
}
