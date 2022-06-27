using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UserMVC.Models;

namespace UserMVC.Database_Access
{
    public class newedit
    {
        public string UpdateData(MyProfileModelDTO objcust)
        {
            SqlConnection con = null;
            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserProfileEntitiesstr"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", objcust.Id);
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Age", objcust.Age);
                cmd.Parameters.AddWithValue("@Username", objcust.UserName);
                cmd.Parameters.AddWithValue("@Designation", objcust.Designation);
                cmd.Parameters.AddWithValue("@Location", objcust.Location);
                cmd.Parameters.AddWithValue("@Img", objcust.Img);
                cmd.Parameters.AddWithValue("@Query", 2);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public MyProfileModelDTO SelectDatabyID(string ID)
        {
            SqlConnection con = null;
            DataSet ds = null;
            MyProfileModelDTO cobj = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserProfileEntitiesstr"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Age", "");
                cmd.Parameters.AddWithValue("@Username", "");
                cmd.Parameters.AddWithValue("@Designation", "");
                cmd.Parameters.AddWithValue("@Location", "");
                cmd.Parameters.AddWithValue("@Img", "");
                cmd.Parameters.AddWithValue("@Query", 5);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cobj = new MyProfileModelDTO();
                    cobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.UserName = ds.Tables[0].Rows[i]["Username"].ToString();
                    cobj.Age = Convert.ToInt32(ds.Tables[0].Rows[i]["Age"].ToString());
                    cobj.Designation = ds.Tables[0].Rows[i]["Designation"].ToString();
                    cobj.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                    cobj.Img = ds.Tables[0].Rows[i]["Img"].ToString();

                }
                return cobj;
            }
            catch
            {
                return cobj;
            }
            finally
            {
                con.Close();
            }
        }
    }
}