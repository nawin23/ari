using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserBL;
using UserDAL;
using UserDTO;
using InfiniteJquery.Database_Access;
using UserMVC.Models;
using UserMVC.Database_Access;

namespace UserMVC.Controllers
{
    [Authorize]
    [OutputCache( Duration = 1, VaryByParam = "None")]
   

    public class UserController : Controller
    {
        
        InfiniteJquery.Database_Access.db dblayer = new InfiniteJquery.Database_Access.db();
        BL blObj;
        UserProfileContent2 contxtObj;
        DAL dalObj;
        public UserController()
        {
            blObj = new BL();
            contxtObj = new UserProfileContent2();
            dalObj = new DAL();
            
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FetchProfile()
        {
            try
            {
                List<MyProfileDTO> lstReview = blObj.FetchAllProfile();
                List<MyProfileModelDTO> lstModel = new List<MyProfileModelDTO>();
                foreach (var user in lstReview)
                {
                    MyProfileModelDTO lstMvc = new MyProfileModelDTO();
                    lstMvc.Name = user.Name;
                    lstMvc.Age = user.Age;
                    lstMvc.UserName = user.UserName;
                    lstMvc.Designation = user.Designation;
                    lstMvc.Location = user.Location;
                    lstMvc.Img = user.Img;
                    lstModel.Add(lstMvc);
                }
                return View(lstModel);
            }
            catch (Exception)
            {

                return View("Error");
            }

        }
        
        public ActionResult DashBoard()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult MyProfile()
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
        public ActionResult Welcome()
        {
            string display = (string)Session["UserName"];
            string mainconn = ConfigurationManager.ConnectionStrings["UserProfileEntitiesstr"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from [UserProfile].[dbo].[My_Profile] where UserName='" + display + "'";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("UserName", Session["UserName"].ToString());
            SqlDataReader sdr = sqlcomm.ExecuteReader();

            if (sdr.Read())
            {
                string u = sdr["Username"].ToString();
                string s = sdr["Img"].ToString();
                string l = sdr["Location"].ToString();
                string d = sdr["Designation"].ToString();
                string n = sdr["Name"].ToString();
                var a = sdr["Age"];
                ViewData["Image"] = s;
                TempData["Name"] = s;
                ViewData["loc"] = l;
                ViewData["Design"] = d;
                ViewData["Name"] = n;
                ViewData["Age"] = a;
                TempData["Age"] = a;
                ViewData["UserName"] = u;
                TempData["UserName"] = u;
            }
            sqlconn.Close();
            return View();
        }
        [HttpPost]
        public ActionResult Welcome(MyProfileModelDTO saveObj)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(saveObj.ImageUpload.FileName);
                string extension = Path.GetExtension(saveObj.ImageUpload.FileName);
                fileName = DateTime.Now.ToString("yymmssfff") + extension;
                saveObj.Img = "/Images/" + fileName;

                saveObj.ImageUpload.SaveAs(Path.Combine(Server.MapPath("/Images/"), fileName));

                MyProfileDTO dtoObj = new MyProfileDTO();
                dtoObj.Name = TempData["Name"].ToString();
                dtoObj.Age = Convert.ToInt32(TempData["Age"]);
                dtoObj.UserName = TempData["UserName"].ToString();
                dtoObj.Designation = saveObj.Designation;
                dtoObj.Location = saveObj.Location;
                dtoObj.Img = saveObj.Img;

                int res = blObj.profile(dtoObj);
                if (res == 1)
                {
                    return RedirectToAction("IndexProfile");
                }
                else
                {
                    return View("Error");

                }

            
             }
            catch (Exception)
            {

                throw;
            }
}

        [HttpPost]
        public ActionResult MyProfile(MyProfileModelDTO saveObj)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(saveObj.ImageUpload.FileName);
                string extension = Path.GetExtension(saveObj.ImageUpload.FileName);
                fileName = DateTime.Now.ToString("yymmssfff") + extension;
                saveObj.Img = "/Images/" + fileName;

                saveObj.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/"), fileName));

                MyProfileDTO dtoObj = new MyProfileDTO();
                dtoObj.Name = saveObj.Name;
                dtoObj.Age = saveObj.Age;
                dtoObj.UserName = saveObj.UserName;
                dtoObj.Designation = saveObj.Designation;
                dtoObj.Location = saveObj.Location;
                dtoObj.Img = saveObj.Img;

                int res = blObj.profile(dtoObj);
                if (res == 1)
                {
                    return RedirectToAction("FetchProfile");
                }
                else
                {
                    return View("Error");

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult IndexProfile()
        {
            int pagesize = 10;
            var rowdata = db.GetEmployee(1, pagesize);
            return View(rowdata);
        }

        protected string renderPartialViewtostring(string Viewname, object model)
        {
            if (string.IsNullOrEmpty(Viewname))

                Viewname = ControllerContext.RouteData.GetRequiredString("action");
            ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewresult = ViewEngines.Engines.FindPartialView(ControllerContext, Viewname);
                ViewContext viewcontext = new ViewContext(ControllerContext, viewresult.View, ViewData, TempData, sw);
                viewresult.View.Render(viewcontext, sw);
                return sw.GetStringBuilder().ToString();
            }

        }

        public class JsonModel
        {
            public string HTMLString { get; set; }
            public bool NoMoredata { get; set; }
        }
        [ChildActionOnly]
        public ActionResult table_row(List<db.employee> Model)
        {
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Infinite
            
            (int pageindex)
        {
            System.Threading.Thread.Sleep(1000);
            int pagesize = 5;
            var tbrow = db.GetEmployee(pageindex, pagesize);
            JsonModel jsonmodel = new JsonModel();
            jsonmodel.NoMoredata = tbrow.Count < pagesize;
            jsonmodel.HTMLString = renderPartialViewtostring("table_row", tbrow);
            return Json(jsonmodel);
        }


        [HttpGet]
        public ActionResult Details(string ID)
        {
            MyProfileModelDTO objCustomer = new MyProfileModelDTO();
            newedit objDB = new newedit();   
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            MyProfileModelDTO objCustomer = new MyProfileModelDTO();
            newedit objDB = new newedit(); 
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpPost]
        public ActionResult Edit(MyProfileModelDTO objCustomer)
        {

            newedit objDB = new newedit();  
                string result = objDB.UpdateData(objCustomer);
                //ViewData["result"] = result;    
                TempData["result2"] = result;
                ModelState.Clear(); //clearing model    
                //return View();    
                return RedirectToAction("IndexProfile");
            
            
        }

        
        [HttpGet]
        public ActionResult UpdatePassword()
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
        public ActionResult UpdatePassword(UpdatePasswordDto passObj)
        {
            try
            {
                UpdatePasswordDto dtoObj = new UpdatePasswordDto();
                passObj.UserName = Session["UserName"].ToString();
                dtoObj.UserName = passObj.UserName;
                dtoObj.oldPassword = Crypto.Hash(passObj.oldPassword);
                dtoObj.newPassword = Crypto.Hash(passObj.newPassword);

                int res = dalObj.UpdatePassword(dtoObj);
                if (res == 1)
                {
                    return RedirectToAction("IndexProfile", "User");
                }
                else
                {
                    return View("Error");
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult SearchCustomers(string customerName)
        {
            UserProfileContent2 entities = new UserProfileContent2();
            var customers = from c in entities.My_Profile
                            where c.Name.Contains(customerName)
                            select c;
            return Json(customers.ToList().Take(10));
        }


    }



}
