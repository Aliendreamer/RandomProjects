namespace IRunes.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {

        public User()
        {
            this.UserAlbums=new HashSet<UserAlbum>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public ICollection<UserAlbum> UserAlbums { get; set; }

    }
}
