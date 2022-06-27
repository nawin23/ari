using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteJquery.Database_Access
{
    
    public class db
    {
       
        
       public class employee
       {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string UserName { get; set; }
            public string Designation { get; set; }
            public string Location { get; set; }
            public string Password { get; set; }
            public string Img { get; set; }
            [NotMapped]
            public HttpPostedFileBase ImageUpload { get; set; }
            /* public MyProfileDTO()
             {
                 Img = "~/Images/Default.png";
             }*/
        }

        public static List<employee> GetEmployee(int pageindex,int pagesize)
       {
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserProfileEntitiesstr"].ConnectionString);
           SqlCommand com = new SqlCommand("usp_Profile_LazyLoad", con);
           com.CommandType = CommandType.StoredProcedure;
           com.Parameters.AddWithValue("@PageIndex", pageindex);
           com.Parameters.AddWithValue("@PageSize", pagesize);
           List<employee> listemp = new List<employee>();

           SqlDataAdapter da = new SqlDataAdapter(com);
           DataSet ds = new DataSet();
           da.Fill(ds);
           foreach (DataRow dr in ds.Tables[0].Rows)
           {
               listemp.Add(new employee
               {
                   Id = Convert.ToInt32(dr["id"]),
                   Name = dr["Name"].ToString(),
                   Age = Convert.ToInt32(dr["Age"]),
                   UserName = dr["Username"].ToString(),
                   Designation = dr["Designation"].ToString(),
                   Location = dr["Location"].ToString(),
                   Img = dr["img"].ToString()
               });
           }
           return listemp;
       }

    }

}