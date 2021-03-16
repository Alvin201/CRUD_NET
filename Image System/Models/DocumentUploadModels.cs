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
    public class DocumentUploadModels
    {
        public IEnumerable<DTO.DocumentUploadDTO> GetList
        {
            get
            {
                List<DTO.DocumentUploadDTO> abc = new List<DTO.DocumentUploadDTO>();
                using (var con = new SqlConnection(GlobalConnection.objcon))
                {
                    SqlCommand cmd = new SqlCommand("select * from DocumentUpload order by ID", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rd = cmd.ExecuteReader();


                    while (rd.Read())
                    {
                        DTO.DocumentUploadDTO a = new DTO.DocumentUploadDTO();
                        a.ID = rd["ID"].ToString();
                        a.FileName = rd["FileName"].ToString();
                        a.CurrentStatus = Convert.ToString(rd["CurrentStatus"]);

                        abc.Add(a);
                    }
                   

                    DataTable dt = new DataTable();
                    dt.Load(rd);
                    rd.Close();

                    return abc;
                }
            }
        }


        public void Insert(DTO.DocumentUploadDTO a)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("[InsertDocument]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileName", a.FileName);
                cmd.Parameters.AddWithValue("@CurrentStatus", a.CurrentStatus);
                cmd.Parameters.AddWithValue("@NIK", Convert.ToString(System.Web.HttpContext.Current.Session["NIK"] as String));
                cmd.Parameters.AddWithValue("@Action", "INSERT");

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(DTO.DocumentUploadDTO a)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("[UpdateDocument]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", a.ID);
                cmd.Parameters.AddWithValue("@FileName", a.FileName);
                cmd.Parameters.AddWithValue("@CurrentStatus", a.CurrentStatus);
                cmd.Parameters.AddWithValue("@NIK", Convert.ToString(System.Web.HttpContext.Current.Session["NIK"] as String));
                cmd.Parameters.AddWithValue("@Action", "UPDATE");

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string ID)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM DocumentUpload WHERE ID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertLogDelete(string ID)
        {
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [LogDocumentUpload] SELECT  @NIK,'Document ID : ' + CONVERT(varchar,@ID) +' has been deleted'", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@NIK", Convert.ToString(System.Web.HttpContext.Current.Session["NIK"] as String));

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int Upload(List<DTO.DocumentUploadDTO> documents)
        {
            int iUpd = 0;
            using (var con = new SqlConnection(GlobalConnection.objcon))
            {
                con.Open();
                foreach (DTO.DocumentUploadDTO document in documents)
                {
                    string q = "UploadMasterDocumentUpd";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", document.ID);
                    cmd.Parameters.AddWithValue("@FileName", document.FileName);
                    cmd.Parameters.AddWithValue("@CurrentStatus", document.CurrentStatus);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 0)
                    {   
                        q = "sp_MasterDocumentUpd";
                        cmd.CommandText = q;
                        i = cmd.ExecuteNonQuery();
                    }
                    iUpd = iUpd + i;
                }
            }
            return iUpd;
        }



    }
}