using System.ComponentModel.DataAnnotations;

namespace KP_Soloviev.Models
{
    public partial class User
    {
        [Key]
        public int UserID { get; set; }

        public string UserMail { get; set; }

        public string UserName { get; set; }

        public string UserPass { get; set; }

        public string UserRole { get; set; }

        public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
