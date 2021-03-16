using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Web;
//using Oracle.ManagedDataAccess.Client;

namespace Image_System.Models
{
    public class MenuModels
    {
        public IEnumerable<DTO.MenuDTO> GetList(string NIK)
        {
            List<DTO.MenuDTO> abc = new List<DTO.MenuDTO>();
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                string select_where = "SELECT us.NIK, * FROM UserSetup us  INNER JOIN RoleSetup rol ON us.Role_ID  = rol.ID_Role INNER JOIN RoleMenuSetup rol_m ON us.Role_ID = rol_m.RoleID INNER JOIN Menu men ON men.IDMenu = rol_m.MenuID WHERE us.NIK = ";
                SqlCommand cmd = new SqlCommand(select_where + "@NIK", con);
                cmd.Parameters.AddWithValue("NIK", NIK);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rd = cmd.ExecuteReader();


                while (rd.Read())
                {
                    DTO.MenuDTO a = new DTO.MenuDTO();
                    a.IDMenu = Convert.ToInt32(rd["IDMenu"]);
                    a.Description = rd["Description"].ToString();
                    a.URL = Convert.ToString(rd["URL"]);
                    a.Fa_Awesome = Convert.ToString(rd["Fa_Awesome"]);
                    abc.Add(a);
                }

                DataTable dt = new DataTable();
                dt.Load(rd);
                rd.Close();

                return abc;
            }
        }

        //public IEnumerable<DTO.MenuDTO> ListMenu(string NIK)
        //{
        //    List<DTO.MenuDTO> Menus = new List<DTO.MenuDTO>();
        //    using (var con = new SqlConnection(GlobalConnection.objcon))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_UserMenu", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("NIK", NIK);
        //        con.Open();
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            DTO.MenuDTO Menu = new DTO.MenuDTO();
        //            Menu.IDMenu = rd["IDMenu"].ToString();
        //            Menu.Description = rd["Description"].ToString();
        //            Menu.URL = rd["URL"].ToString();
        //            Menu.Fa_Awesome = rd["Fa_Awesome"].ToString();
        //            Menus.Add(Menu);
        //        }
        //        return Menus;
        //    }
        //}

        //public IEnumerable<DTO.MenuDTO> GroupMenu(string NIK)
        //{
        //    List<DTO.MenuDTO> Menus = new List<DTO.MenuDTO>();
        //    using (var con = new SqlConnection(GlobalConnection.objcon))
        //    {
        //        string q = "Select * from Menu";
        //        SqlCommand cmd = new SqlCommand(q, con);
        //        cmd.Parameters.AddWithValue("NIK", NIK);
        //        con.Open();
        //        SqlDataReader rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            DTO.MenuDTO Menu = new DTO.MenuDTO();
        //            Menu.IDMenu = "";
        //            Menu.Description = "";
        //            Menu.URL = "";
        //            Menu.Fa_Awesome = rd["Fa_Awesome"].ToString();
        //            Menus.Add(Menu);
        //        }
        //        return Menus;
        //    }
        //}


    }
}