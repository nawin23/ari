using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserMVC.Models
{
    public class LoginModelDTO 
        {
        [Required(ErrorMessage = "Please enter your name.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class SignUpModelDTO
        {

        [Required(ErrorMessage = "Please enter your name..")]
        public string Name { get; set; }
            

        [Required(ErrorMessage = "Please enter your age.")]
        public int Age { get; set; }

        [Display(Name = "Username")]

        [Required(ErrorMessage = "Please enter your username.")]
        public string UserName { get; set; }
           
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
      // [RegularExpression(@"^.* (?=.{8,20})(?=.*\d)(?=.*[a - z])(?=.*[A - Z])", ErrorMessage = "Password must have only special Character")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password  should be between 5 and 20 characters")]

        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Password  should not be empty.")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        [DataType(DataType.Password)]
        
        public string ConfirmPassword { get; set; }

    }
    public class editProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }


        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public String Img = "~/Images/Default.png";
    }
    public class MyProfileModelDTO
        {
           // public String Img = "~/Images/Default.png";
            [Required(ErrorMessage = "This field is required.")]

        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter your name..")]
        [Display(Name = "Username")]

        public string UserName { get; set; }
            public string Designation { get; set; }
            public string Location { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("ProfilePic")]
          public string Img { get; set; }
            [NotMapped]
            public HttpPostedFileBase ImageUpload { get; set; }
        public MyProfileModelDTO()
        {
            Img = "~/Images/Default.png";
        }

        //public bool IsEmailVerified { get; set; }
        //public System.Guid ActivationCode { get; set; }
    }
    public class UpadatePasswordModel
    {

        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "Please enter your old password.")]
        [DataType(DataType.Password)]
        public string oldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Please enter your new password.")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("newPassword")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

    }

}