
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDTO;

namespace UserDAL
{
    public class DAL
    {
        SqlConnection conObj;
        SqlCommand cmdObj;
        UserProfileContent2 contxtObj;
        public DAL()
        {
            
            conObj = new SqlConnection(ConfigurationManager.ConnectionStrings["UserProfileEntitiesstr"].ConnectionString);
            cmdObj = new SqlCommand();
            contxtObj = new UserProfileContent2();
            
        }
        public static List<MyProfileDTO> GetEmployee(int pageindex, int pagesize)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserProfileEntitiesstr"].ConnectionString);
            SqlCommand com = new SqlCommand("usp_Profile_LazyLoad", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@PageIndex", pageindex);
            com.Parameters.AddWithValue("@PageSize", pagesize);
            List<MyProfileDTO> listemp = new List<MyProfileDTO>();

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                listemp.Add(new MyProfileDTO()
                {
                    Id = Convert.ToInt32(dr["id"]),
                    Name = dr["Name"].ToString(),
                    Age = Convert.ToInt32(dr["Age"]),
                    UserName = dr["Username"].ToString(),
                    Designation = dr["Designation"].ToString(),
                    Location = dr["Location"].ToString(),
                    Img = dr["img"].ToString()

                    
                }) ;
            }
            return listemp;
        }
        public List<MyProfileDTO> FetchProfile()
        {
            try
            {
                var res = contxtObj.My_Profile.ToList();
                List<MyProfileDTO> lstProfile = new List<MyProfileDTO>();
                foreach (var item in res)
                {
                    lstProfile.Add(new MyProfileDTO()
                    {
                        Name = item.Name,
                        Age = Convert.ToInt32(item.Age),
                        UserName = item.Username,
                        Designation = item.Designation,
                        Location = item.Location,
                        Img = item.Img
                    });

                }
                return lstProfile;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int SignupPage(MyProfileDTO newLogin)
        {
            try
            {
                cmdObj = new SqlCommand();
                cmdObj.CommandText = @"uspSign";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue(@"Name", newLogin.Name);
                cmdObj.Parameters.AddWithValue(@"Age", newLogin.Age);
                cmdObj.Parameters.AddWithValue(@"UserName", newLogin.UserName);
                cmdObj.Parameters.AddWithValue(@"Password", newLogin.Password);
                //cmdObj.Parameters.AddWithValue(@"ActivationCode",newLogin.ActivationCode);
                //cmdObj.Parameters.AddWithValue(@"Designation", newLogin.Designation);
                //cmdObj.Parameters.AddWithValue(@"location", newLogin.Location);
                //cmdObj.Parameters.AddWithValue(@"image", newLogin.Img);
               
                SqlParameter retObj = new SqlParameter();
                retObj.Direction = ParameterDirection.ReturnValue;
                retObj.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(retObj);
                conObj.Open();
                cmdObj.ExecuteNonQuery();
                return Convert.ToInt32(retObj.Value);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MyProfileDTO> LoginValidation(MyProfileDTO loginUser)
        {
            try
            {

                var result = contxtObj.My_Profile.Where(x => x.Username == loginUser.UserName && x.Password == loginUser.Password).ToList();
                List<MyProfileDTO> loginCredentails = new List<MyProfileDTO>();
                foreach (var log in result)
                {
                    loginCredentails.Add(new MyProfileDTO()
                    {
                        UserName = log.Username,
                        Password = log.Password,
                    });
                }
                return loginCredentails;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int MyProfilePage(MyProfileDTO newprofile)
        {
            try
            {
                cmdObj = new SqlCommand();
                cmdObj.CommandText = @"uspMyProfile";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue(@"Name", newprofile.Name);
                cmdObj.Parameters.AddWithValue(@"Age", newprofile.Age);
                cmdObj.Parameters.AddWithValue(@"UserName", newprofile.UserName);
                cmdObj.Parameters.AddWithValue(@"Designation", newprofile.Designation);
                cmdObj.Parameters.AddWithValue(@"Location", newprofile.Location);
                cmdObj.Parameters.AddWithValue(@"Img", newprofile.Img);
                SqlParameter retObj = new SqlParameter();
                retObj.Direction = ParameterDirection.ReturnValue;
                retObj.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(retObj);
                conObj.Open();
                cmdObj.ExecuteNonQuery();
                return Convert.ToInt32(retObj.Value);
                 
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public string UpdateData(MyProfileDTO objcust)
        //{
        //    SqlConnection con = null;
        //    string result = "";
        //    try
        //    {
        //        con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserProfileEntitiesstr"].ToString());
        //        SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", objcust.Id);
        //        cmd.Parameters.AddWithValue("@Name", objcust.Name);
        //        cmd.Parameters.AddWithValue("@Age", objcust.Age);
        //        cmd.Parameters.AddWithValue("@Username", objcust.UserName);
        //        cmd.Parameters.AddWithValue("@Designation", objcust.Designation);
        //        cmd.Parameters.AddWithValue("@Location", objcust.Location);
        //        cmd.Parameters.AddWithValue("@Img", objcust.Img);
        //        cmd.Parameters.AddWithValue("@Query", 2);
        //        con.Open();
        //        result = cmd.ExecuteScalar().ToString();
        //        return result;
        //    }
        //    catch
        //    {
        //        return result = "";
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //public MyProfileModelDTO SelectDatabyID(string Id)
        //{
        //    SqlConnection con = null;
        //    DataSet ds = null;
        //    My_Profile cobj = null;
        //    try
        //    {
        //        con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserProfileEntitiesstr"].ToString());
        //        SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", Id);
        //        cmd.Parameters.AddWithValue("@Name", "");
        //        cmd.Parameters.AddWithValue("@Age", "");
        //        cmd.Parameters.AddWithValue("@Username", "");
        //        cmd.Parameters.AddWithValue("@Designation", "");
        //        cmd.Parameters.AddWithValue("@Location", "");
        //        cmd.Parameters.AddWithValue("@Img", "");
        //        cmd.Parameters.AddWithValue("@Query", 5);
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = cmd;
        //        ds = new DataSet();
        //        da.Fill(ds);
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            cobj = new My_Profile();
        //            cobj.id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
        //            cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
        //            cobj.Username = ds.Tables[0].Rows[i]["Username"].ToString();
        //            cobj.Age = Convert.ToInt32(ds.Tables[0].Rows[i]["Age"].ToString());
        //            cobj.Designation = ds.Tables[0].Rows[i]["Designation"].ToString();
        //            cobj.Location = ds.Tables[0].Rows[i]["Location"].ToString();
        //            cobj.Img = ds.Tables[0].Rows[i]["Img"].ToString();

        //        }
        //        return cobj;
        //    }
        //    catch
        //    {
        //        return cobj;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        public int UpdatePassword(UpdatePasswordDto passObj)
        {
            try
            {
                cmdObj = new SqlCommand();
                cmdObj.CommandText = @"UserChangePassword";
                cmdObj.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObj.Connection = conObj;
                cmdObj.Parameters.AddWithValue("@UserName", passObj.UserName);
                cmdObj.Parameters.AddWithValue("@OldPassword", passObj.oldPassword);
                cmdObj.Parameters.AddWithValue("@NewPassword", passObj.newPassword);

                SqlParameter retObj = new SqlParameter();
                retObj.Direction = ParameterDirection.ReturnValue;
                retObj.SqlDbType = SqlDbType.Int;
                cmdObj.Parameters.Add(retObj);
                conObj.Open();
                cmdObj.ExecuteNonQuery();
                return Convert.ToInt32(retObj.Value);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conObj.Close();
            }
        }
    }
}
