using Ecommerce.DBEngine;
using ECommerce.Common;
using ECommerce.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ECommerce.Data
{
    public interface IEmailRepository
    {
        DataTable GetEmail();
        void SaveEmail(EmailInfo objModel);
        void UpdateStatus(int OrderId, string Status);

    }
    public class EmailRepository : IEmailRepository
    {
        private readonly ISqlDBConnection _sqlDBConnection;
        public EmailRepository(ISqlDBConnection sqlDBConnection)
        {
            _sqlDBConnection = sqlDBConnection;
        }

        public DataTable GetEmail()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@ActionId",1),
                     };
                dt = _sqlDBConnection.ExecuteTable(StoredProc.SP_Email, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public void SaveEmail(EmailInfo objModel)
        {
            try
            {
                SqlParameter[] param = {
                                    new SqlParameter("ActionId",2 ),
                                    new SqlParameter("@Id", objModel.Id),
                                    new SqlParameter("@Email", objModel.Email),
                                    };
                _sqlDBConnection.ExecuteNonQuery(StoredProc.SP_Email, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateStatus(int OrderId, string Status)
        {
            try
            {
                SqlParameter[] param = {
                                    new SqlParameter("@ActionId", 3),
                                    new SqlParameter("@OrderId",OrderId),
                                    new SqlParameter("@Status", Status),
                                    };
                _sqlDBConnection.ExecuteNonQuery(StoredProc.SP_Order, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
