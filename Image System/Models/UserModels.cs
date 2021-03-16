using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using Oracle.ManagedDataAccess.Client;

namespace Image_System.Models
{
    public class UserModels
    {

        #region ALL
        //public List<DTO.UserDTO> All()

        //{

        //    List<DTO.UserDTO> lst = new List<DTO.UserDTO>();

        //    using (var con = new SqlConnection(GlobalConnection.objcon))

        //    {

        //        con.Open();

        //        SqlCommand com = new SqlCommand("select * from dbo.UserSetup", con);

        //        com.CommandType = CommandType.Text;

        //        SqlDataReader rdr = com.ExecuteReader();

        //        while (rdr.Read())

        //        {

        //            lst.Add(new DTO.UserDTO
        //            {
        //                NIK = Convert.ToInt32(rdr["NIK"]),
        //                Username = rdr["Username"].ToString(),
        //                Password = Convert.ToString(rdr["Password"]),
        //                LastLoginDate = Convert.ToString(rdr["LastLoginDate"]),

        //        });
        //        }
        //        return lst;
        //    }
        //}
        #endregion ALL

        #region GetID
        //public List<DTO.UserDTO> GetID(int NIK)
        //{
        //    List<DTO.UserDTO> details = new List<DTO.UserDTO>();
        //    using (var con = new SqlConnection(GlobalConnection.objcon))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("select * from UserSetup WHERE NIK = @NIK", con);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Parameters.AddWithValue("@NIK", NIK);
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            DTO.UserDTO detail = new DTO.UserDTO();
        //            detail.NIK = Convert.ToInt32(rd["NIK"]);
        //            detail.Username = rd["Username"].ToString();
        //            detail.Password = rd["Password"].ToString();
        //            details.Add(detail);
        //        }
        //        rd.Close();
        //        return details;
        //    }
        //}
        #endregion GetID

        public IEnumerable<DTO.UserDTO> GetList
        {
            get
            {
                List<DTO.UserDTO> abc = new List<DTO.UserDTO>();
                using (var con = new SqlConnection(GlobalConnection.objcon))
                {
                    SqlCommand cmd = new SqlCommand("select * from UserSetup order by NIK", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();


                    while (rd.Read())
                    {
                        DTO.UserDTO a = new DTO.UserDTO();
                        a.NIK = Convert.ToInt32(rd["NIK"]);
                        a.Username = rd["Username"].ToString();
                        a.Password = Convert.ToString(rd["Password"]);

                        if (rd["LastLoginDate"] != null)
                        {
                            a.LastLoginDate = Convert.ToString(rd["LastLoginDate"]);
                        }

                        abc.Add(a);
                    }

                    DataTable dt = new DataTable();
                    dt.Load(rd);
                    rd.Close();

                    return abc;
                }
            }
        }


        public void Insert(DTO.UserDTO a)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("[InsertUser]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", a.Username);
                
                Image_System.Encryption encr = new Image_System.Encryption();
                string pwd = encr.Encrypt(a.Password, "A0000");
                cmd.Parameters.AddWithValue("@password", pwd);
                
                cmd.Parameters.AddWithValue("@Action", "INSERT");
               
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(DTO.UserDTO a)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("[UpdateUser]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIK", a.NIK);
                cmd.Parameters.AddWithValue("@Username", a.Username);

                Image_System.Encryption encr = new Image_System.Encryption();
                string pwd = encr.Encrypt(a.Password, "A0000");
                cmd.Parameters.AddWithValue("@password", pwd);
                cmd.Parameters.AddWithValue("@Action", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int NIK)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.UserSetup WHERE NIK = @NIK", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@NIK", NIK);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}