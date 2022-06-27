using RegistrationAndLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserBL;
using UserDAL;
using UserDTO;
using UserMVC.Models;

namespace UserMVC.Controllers
{
    public class AuthcController : Controller
    {
        BL blObj;
        UserProfileContent2 contxtObj;
        public AuthcController()
        {
            blObj = new BL();
            contxtObj = new UserProfileContent2();

        }
        // GET: Authc
        public ActionResult IndexPage()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Signup()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Signup(SignUpModelDTO saveObj)
        {
            try
            {
                MyProfileDTO dtoObj = new MyProfileDTO();
                dtoObj.Name = saveObj.Name;
                dtoObj.Age = saveObj.Age;
                dtoObj.UserName = saveObj.UserName;

                dtoObj.Password = Crypto.Hash(saveObj.Password);


                int res = blObj.signup(dtoObj);
                if (res == 1)
                {
                    return RedirectToAction("LoginDetails");
                }
                else
                {
                    return View("Error");

                }

            }
            catch (Exception)
            {

                return View("Signup");
            }

        }

        //[HttpPost]
        //public ActionResult Signup(MyProfileModelDTO saveObj)
        //{
        //    try
        //    {
        //        //bool Status = false;
        //        //string message = "";


        //        MyProfileDTO dtoObj = new MyProfileDTO();
        //        dtoObj.Name = saveObj.Name;
        //        dtoObj.Age = saveObj.Age;
        //        dtoObj.UserName = saveObj.UserName;
        //        dtoObj.Password = Crypto.Hash(saveObj.Password);
        //        //dtoObj.ActivationCode = Guid.NewGuid();
                

        //        int res = blObj.signup(dtoObj);
        //        //SendVerificationLinkEmail(saveObj.UserName, saveObj.ActivationCode.ToString());
        //        //message = "Registration successfully done. Account activation link " +
        //        //    " has been sent to your email id:" + saveObj.UserName;
        //        //Status = true;
        //        if (res == 1)
        //        {
        //            return RedirectToAction("LoginDetails");
        //        }
        //        else
        //        {
        //            return View("Error");

        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        [HttpGet]
        public ActionResult LoginDetails()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult LoginDetails(MyProfileModelDTO loginDetails)
        {
            try
            {
                string message = "";
                int result = 0;

                MyProfileDTO loginCreds = new MyProfileDTO();
                loginCreds.UserName = loginDetails.UserName;
                loginCreds.Password = Crypto.Hash(loginDetails.Password);
                List<MyProfileDTO> validation = blObj.login(loginCreds);
                foreach (var validationItem in validation)
                {
                    if (loginDetails.UserName == validationItem.UserName && Crypto.Hash(loginDetails.Password) == validationItem.Password)
                    {

                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
                if (result == 1)
                {
                    MyProfileModelDTO profile = new MyProfileModelDTO();
                    Session["UserName"] = loginDetails.UserName.ToString();
                    message = "Login Successful";
                    FormsAuthentication.SetAuthCookie(loginDetails.UserName, false);
                    return RedirectToAction("IndexProfile", "User");
                }
                else
                {
                    message = "Please check your username or password";
                    ViewBag.Message = message;
                    return View("LoginDetails");
                   
                }

            }
            catch (Exception)
            {

                return View("Error");
            }

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginDetails");
        }


        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Authc/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("gtpolo110@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "oleeolee45"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        //[HttpGet]
        //public ActionResult VerifyAccount(string id)
        //{
        //    bool Status = false;
        //    using (UserProfileEntities1 dc = new UserProfileEntities1())
        //    {
        //        dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
        //                                                        // Confirm password does not match issue on save changes
        //        var v = dc.My_Profile.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
        //        if (v != null)
        //        {
        //            v.IsEmailVerified = true;
        //            dc.SaveChanges();
        //            Status = true;
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Invalid Request";
        //        }
        //    }
        //    ViewBag.Status = Status;
        //    return View();
        //}


        ////Part 3 - Forgot Password

        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ForgotPassword(RegisterModel User)
        //{
        //    //Verify Email ID
        //    //Generate Reset password link 
        //    //Send Email 
        //    string message = "";
        //    bool status = false;

        //    using (UserProfileEntities1 dc = new UserProfileEntities1())
        //    {
        //        var account = dc.My_Profile.Where(a => a.Username == User.UserName).FirstOrDefault();
        //        if (account != null)
        //        {
        //            //Send email for reset password
        //            string resetCode = Guid.NewGuid().ToString();
        //            SendVerificationLinkEmail(account.Username, resetCode, "ResetPassword");
        //            account.ResetPasswordCode = resetCode;
        //            //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
        //            //in our model class in part 1
        //            dc.Configuration.ValidateOnSaveEnabled = false;
        //            dc.SaveChanges();
        //            message = "Reset password link has been sent to your email id.";
        //        }
        //        else
        //        {
        //            message = "Account not found";
        //        }
        //    }
        //    ViewBag.Message = message;
        //    return View();
        //}

        //public ActionResult ResetPassword(string id)
        //{
        //    //Verify the reset password link
        //    //Find account associated with this link
        //    //redirect to reset password page
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        return HttpNotFound();
        //    }

        //    using (UserProfileEntities1 dc = new UserProfileEntities1())
        //    {
        //        var user = dc.My_Profile.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
        //        if (user != null)
        //        {
        //            ResetPasswordModel model = new ResetPasswordModel();
        //            model.ResetCode = id;
        //            return View(model);
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ResetPassword(ResetPasswordModel model)
        //{
        //    var message = "";
        //    if (ModelState.IsValid)
        //    {
        //        using (UserProfileEntities1 dc = new UserProfileEntities1())
        //        {
        //            var user = dc.My_Profile.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
        //            if (user != null)
        //            {
        //                user.Password = Crypto.Hash(model.NewPassword);
        //                user.ResetPasswordCode = "";
        //                dc.Configuration.ValidateOnSaveEnabled = false;
        //                dc.SaveChanges();
        //                message = "New password updated successfully";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        message = "Something invalid";
        //    }
        //    ViewBag.Message = message;
        //    return View(model);
        //}

    }


}