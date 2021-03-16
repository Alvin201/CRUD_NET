using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Image_System.Models
{
    public class ChangePasswordModels
    {

        public IEnumerable<DTO.ChangePasswordDTO> Users
        {
            get
            {
                List<DTO.ChangePasswordDTO> Users = new List<DTO.ChangePasswordDTO>();
                using (var con = new SqlConnection(GlobalConnection.objcon))
                {
                    string q = "Select NIK from UserSetup";
                    SqlCommand cmd = new SqlCommand(q, con);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    Image_System.Encryption encr = new Image_System.Encryption();
                    while (rd.Read())
                    {
                        DTO.ChangePasswordDTO User = new DTO.ChangePasswordDTO();
                        User.NIK = Convert.ToInt32(rd["NIK"]);
                        Users.Add(User);
                    }
                    return Users;
                }
            }
        }

        public void Update(DTO.ChangePasswordDTO user)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("[ChangePassword]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NIK", user.NIK);
                Image_System.Encryption encr = new Encryption();
                string pwd = encr.Encrypt(user.Password , "A0000");
                cmd.Parameters.AddWithValue("Password", pwd);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

       
    }
}