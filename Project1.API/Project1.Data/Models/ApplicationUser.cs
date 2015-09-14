using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Data.Models
{
    [Table("ApplicationUser", Schema="USER")]
    public class ApplicationUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserID { get; set; }
        
        [Required(ErrorMessage="{0} is required.")]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string SecurityHash { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(16)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100)]
        public string Password { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public DateTime JoinTS { get; set; }
        
        public bool Active { get; set; }

        public virtual ICollection<Document> Documents{ get; set; }

        public virtual ICollection<Share> Shares { get; set; }

        public virtual UserAccessToken AccessToken { get; set; }

        [Timestamp]
        [ConcurrencyCheck]
        public Byte[] TimeStamp { get; set; }
    }
}
