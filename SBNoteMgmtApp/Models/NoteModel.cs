using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBNoteMgmtApp.Models
{
    public class NoteModel
    {
        //Following are the properties to the Note App. Allowing Client to set and get the properties.
        public Guid Id { get; set; }

        //Making the Subject field Required.
        [Required(ErrorMessage ="Please Include the Subject for your Note.")]
        public string Subject { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
