using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDAL;
using UserDTO;

namespace UserBL
{
    public class BL
    {
        DAL dalObj;
        public BL()
        {
            dalObj = new DAL();
        }
       
        public List<MyProfileDTO> FetchAllProfile()
        {
            List<MyProfileDTO> lstProfile = dalObj.FetchProfile();
            return lstProfile;
        }
        public int signup(MyProfileDTO newLogin)
        {
            return dalObj.SignupPage(newLogin);

        }

        public int signup(SignUpDTO newLogin)
        {
            throw new NotImplementedException();
        }

        public List<MyProfileDTO> login(MyProfileDTO loginUser)
        {
            return dalObj.LoginValidation(loginUser);
        }
        public int profile(MyProfileDTO profileUser)
        {
            return dalObj.MyProfilePage(profileUser);
        }
    }
}
