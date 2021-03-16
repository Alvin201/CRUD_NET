using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_System.Models
{
    public class AccessLogin
    {   
        public DTO.LoginResult CheckUserLogin(DTO.LoginDTO login)
        {
            if (string.IsNullOrWhiteSpace(login.NIK))
                throw new ArgumentNullException("nik");

            //set the password to empty if it is null
            login.Password = login.Password ?? "";

            

            //create the connection
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                var result = new DTO.LoginResult
                {
                    AttemptsRemaining = 5,
                    Status = DTO.LoginStatus.InvalidCredentials
                };
                try
                {
                    using (var command = new SqlCommand("EXEC CheckLogin @nik, @password", con))
                    {
                        command.Parameters.AddWithValue("@nik", login.NIK);

                        Image_System.Encryption encr = new Image_System.Encryption();
                        string pwd = encr.Encrypt(login.Password, "A0000");
                        command.Parameters.AddWithValue("@password", pwd);


                        command.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Status = ((DTO.LoginStatus)(int)reader["Result"]);
                                result.AttemptsRemaining = (int)reader["AttemptsRemaining"];
                                break;
                            }
                            reader.Close();
                        }con.Close();
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    if (con.State != System.Data.ConnectionState.Closed)
                        con.Close();

                    Debug.WriteLine("Error on sql query:" + ex.Message);
                    return result;
                }
            }
        }

        public DTO.LoginDTO GetData(string NIK)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                string select_where = "SELECT us.NIK, * FROM UserSetup us  INNER JOIN RoleSetup rol ON us.Role_ID  = rol.ID_Role INNER JOIN RoleMenuSetup rol_m ON us.Role_ID = rol_m.RoleID INNER JOIN Menu men ON men.IDMenu = rol_m.MenuID WHERE us.NIK = ";

                string q = select_where + "@NIK";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("NIK", NIK);
                con.Open();
                DTO.LoginDTO user = new DTO.LoginDTO();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    user.NIK = dt.Rows[0]["NIK"].ToString();
                    user.Username = Convert.ToString(dt.Rows[0]["Username"]).ToString();
                    user.Password = dt.Rows[0]["Password"].ToString();
                    user.LastPasswordChangedDate = Convert.ToDateTime(dt.Rows[0]["LastPasswordChangedDate"]).ToLocalTime();
                    user.RoleID = Convert.ToInt32(dt.Rows[0]["RoleID"]);
                    user.MenuID = Convert.ToInt32(dt.Rows[0]["MenuID"]);
                    return user;
                }
            }
        }

        public IEnumerable<DTO.ChangePasswordExpiredDTO> FindUsers
        {
            get
            {
                List<DTO.ChangePasswordExpiredDTO> Users = new List<DTO.ChangePasswordExpiredDTO>();
                using (var con = new SqlConnection(GlobalConnection.objcon))
                {
                    string q = "Select * from UserSetup";
                    SqlCommand cmd = new SqlCommand(q, con);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    Image_System.Encryption encr = new Image_System.Encryption();
                    while (rd.Read())
                    {
                        DTO.ChangePasswordExpiredDTO login = new DTO.ChangePasswordExpiredDTO();
                        login.NIK = rd["NIK"].ToString();
                        login.Password = rd["Password"].ToString();
                        string pwd = encr.Decrypt(login.Password, "A0000");
                        login.Password = pwd;
                        Users.Add(login);
                    }
                    return Users;
                }
            }
        }

        public void Update_day(DTO.ChangePasswordExpiredDTO login)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("[ChangePassword_day]", con);
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIK", Convert.ToString(System.Web.HttpContext.Current.Session["NIK"] as String));
                Image_System.Encryption encr = new Encryption();
                string pwd = encr.Encrypt(login.Password, "A0000");
                cmd.Parameters.AddWithValue("Password", pwd);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }




    }
}