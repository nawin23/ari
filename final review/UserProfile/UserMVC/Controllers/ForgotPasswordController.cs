using RegistrationAndLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UserDAL;

namespace UserMVC.Controllers
{
    public class ForgotPasswordController : Controller
    {
        UserProfileContent2 contxtObj;
        public ForgotPasswordController()
        {
            contxtObj = new UserProfileContent2();
        }
        // GET: ForgotPassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

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



        //[NonAction]
        //public void SendVerificationLinkEmail(string Username, string activationCode, string emailFor = "ResetPassword")
        //{
        //    var verifyUrl = "/ForgotPassword" + emailFor + "/" + activationCode;
        //    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

        //    var fromEmail = new MailAddress("hariharan18.12.1999@gmail.com", "Dotnet Awesome");
        //    var toEmail = new MailAddress(Username );
        //    var fromEmailPassword = "Stepup@54321"; // Replace with actual password

        //    string subject = "";
        //    string body = "";
        //    if (emailFor == "VerifyAccount")
        //    {
        //        subject = "Your account is successfully created!";
        //        body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
        //            " successfully created. Please click on the below link to verify your account" +
        //            " <br/><br/><a href='" + link + "'>" + link + "</a> ";
        //    }
        //    else if (emailFor == "ResetPassword")
        //    {
        //        subject = "Reset Password";
        //        body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
        //            "<br/><br/><a href=" + link + ">Reset Password link</a>";
        //    }


        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
        //    };

        //    using (var message = new MailMessage(fromEmail, toEmail)
        //    {
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true
        //    })
        //        smtp.Send(message);
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

        //    using (UserProfileContext2 dc = new UserProfileContext2())
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