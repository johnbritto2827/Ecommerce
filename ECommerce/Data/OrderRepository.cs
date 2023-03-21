using Ecommerce.DBEngine;
using ECommerce.Common;
using ECommerce.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ECommerce.Data
{
    public interface IOrderRepository
    {
        DataTable GetOrder(int ProductId);
        DataTable GetMyOrder(int UserId);
        object GetMyOrderStatus(int OrderId);
        void SaveOrder(Order objModel,int UserId,int ProductId);
    }
    public class OrderRepository : IOrderRepository
    {
       
        private readonly ISqlDBConnection _sqlDBConnection;
        public OrderRepository(ISqlDBConnection sqlDBConnection)
        {
            _sqlDBConnection = sqlDBConnection;
        }

        public DataTable GetOrder(int ProductId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@ActionId",1),
                     };
                dt = _sqlDBConnection.ExecuteTable(StoredProc.SP_Order, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;

        }
        public DataTable GetMyOrder(int UserId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@ActionId",4),
                new SqlParameter("@UserId",UserId),
                     };
                dt = _sqlDBConnection.ExecuteTable(StoredProc.SP_Order, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;

        }
        public object GetMyOrderStatus(int OrderId)
        {
            DataTable dt = new DataTable();
            object Status;
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@ActionId",5),
                new SqlParameter("@OrderId",OrderId),
                     };
                Status=  _sqlDBConnection.ExecuteScalar(StoredProc.SP_Order, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Status;

        }
        public void SaveOrder(Order objModel,int UserId,int ProductId)
        {
           
            try
            {
                  SqlParameter[] param = {
                                    new SqlParameter("ActionId",2 ),
                                    new SqlParameter("@OrderId", objModel.OrderId),
                                    new SqlParameter("@UserId", UserId ),
                                    new SqlParameter("@ProductId",objModel.ProductId),
                                    new SqlParameter("@FullName", objModel.FullName),
                                    new SqlParameter("@MobileNumber", objModel.MobileNumber),
                                    new SqlParameter("@Address", objModel.Address),
                                    new SqlParameter("@City", objModel.City),
                                    new SqlParameter("@State", objModel.State),
                                    new SqlParameter("@PinCode", objModel.PinCode),
                                    new SqlParameter("@Email", objModel.Email),
                                    new SqlParameter("@AddressType", objModel.AddressType),
                                    new SqlParameter("@Status", objModel.Status),
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
