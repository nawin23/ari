using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UserDTO
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public class SignUpDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class MyProfileDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string Password { get; set; }
       // public string Img {  get; set; }
        [NotMapped]

        public HttpPostedFileBase ImageUpload { get; set; }

        public String Img = "~/Images/Default.png";
        /* public MyProfileDTO()
         {
             Img = "~/Images/Default.png";
         }*/

    }

    public class UpdatePasswordDto
    {

        
        public string oldPassword { get; set; }

        
        public string newPassword { get; set; }


        public string UserName { get; set; }

    }

    public class editProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }


        
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public String Img = "~/Images/Default.png";
    }
}
