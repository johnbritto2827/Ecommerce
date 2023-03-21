using ECommerce.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ECommerce.Data
{
    public interface ILoginRepository
    {
        List<Login> ViewLogin();
        Login CheckLogin(string Email, string Password);
        List<Login> SaveLogin(Login objModel);
    }
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _configuration;

        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Login> ViewLogin()
        {
            List<Login> lstlogin = new List<Login>();
            try
            {
                string connString = _configuration.GetValue<string>("SDDemoConnStrings:SDDemoConnetion");
                SqlConnection con = new SqlConnection(connString);//system.Data.Sqllite 
                con.Open();
                string query = "SELECT * FROM [dbo].[tblLogin]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Login LoginDetails = new Login();
                    LoginDetails.UserId = Convert.ToInt32(reader["UserId"]);
                    LoginDetails.FullName = reader["FullName"].ToString();
                    LoginDetails.Email = reader["EmailId"].ToString();
                    LoginDetails.Password = reader["Password"].ToString();

                    lstlogin.Add(LoginDetails);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstlogin;
        }

        public List<Login> SaveLogin(Login objModel)
        {
            List<Login> lstStaff = new List<Login>();
            try
            {           
            string query = string.Empty;
            string connString = _configuration.GetValue<string>("SDDemoConnStrings:SDDemoConnetion");
            SqlConnection con = new SqlConnection(connString);//system.Data.Sqllite 
            if (objModel.UserId > 0)
            {
                query = "UPDATE [dbo].[tblLogin] SET FullName ='" + objModel.FullName + "', Email='" + objModel.Email + "',Password ='" + objModel.Password + "'  WHERE UserId ='" + objModel.UserId + "' ";
            }
            else
            {
                query = "INSERT INTO [dbo].[tblLogin] (FullName,Email,Password) VALUES('" + objModel.FullName + "','" + objModel.Email + "','" + objModel.Password + "')";
            }
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return lstStaff;
        }

        public Login CheckLogin(string Email, string Password)
        {
            Login LoginDetails = new Login();
            try
            {
                string connString = _configuration.GetValue<string>("SDDemoConnStrings:SDDemoConnetion");
                SqlConnection con = new SqlConnection(connString);//system.Data.Sqllite 
                con.Open();
                string query = "SELECT Email,Password,UserId FROM [dbo].[tblLogin] where Email = '" + Email + "' AND Password = '" + Password + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    LoginDetails.Email = reader["Email"].ToString();
                    LoginDetails.Password = reader["Password"].ToString();
                    LoginDetails.UserId = Convert.ToInt32(reader["UserId"]);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LoginDetails;
        }
    }
}

