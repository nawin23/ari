using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserBL;
using UserDTO;

namespace UserAPI.Controllers
{
    public class UserController : ApiController
    {
        BL blObj;
        public UserController()
        {
            blObj = new BL();
        }
        public HttpResponseMessage SaveSignUp(SignUpDTO newLogin)
        {
            try
            {
                int res = blObj.signup(newLogin);
                if (res == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Signup  sucessfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Something went wrong");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }



        }
    }
}
