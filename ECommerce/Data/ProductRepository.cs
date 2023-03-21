using Ecommerce.DBEngine;
using ECommerce.Common;
using ECommerce.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ECommerce.Data
{
    public interface IProductRepository
    {
        DataTable GetProduct(int ProductId=0);
        void SaveProduct(ProductAdd objModel, int UserId, int ProductId);
        void DeleteProduct(int ProductId);
        DataTable EditProduct(int ProductId);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ISqlDBConnection _sqlDBConnection;
        public ProductRepository(ISqlDBConnection sqlDBConnection)
        {
            _sqlDBConnection = sqlDBConnection;
        }



        public DataTable GetProduct(int ProductId=0)
        {
            DataTable dt = new DataTable();
                try
            {
                SqlParameter[] param = {
                new SqlParameter("@ActionId", 3),
                new SqlParameter("@ProductId", ProductId)
                     };
                dt = _sqlDBConnection.ExecuteTable(StoredProc.SP_Product, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;

        }

        public void SaveProduct(ProductAdd objModel,int UserId, int ProductId)
        {
            try
            {
                   SqlParameter[] param = {
                                    new SqlParameter("ActionId",1 ),
                                    new SqlParameter("@ProductId", objModel.ProductId),
                                    new SqlParameter("@UserId", UserId ),
                                    new SqlParameter("@Brand", objModel.Brand),
                                    new SqlParameter("@Model", objModel.Model),
                                    new SqlParameter("@Price", objModel.Price),
                                    new SqlParameter("@Discount", objModel.Discount),
                                    new SqlParameter("@Specification", objModel.Specification),
                                    new SqlParameter("@QtyinStock", objModel.QtyinStock),
                                    new SqlParameter("@Rating", objModel.Rating),
                                    new SqlParameter("@Colour", objModel.Colour),
                                    new SqlParameter("@Storage", objModel.Storage),
                                    new SqlParameter("@Image", objModel.Image),
                                    };
                _sqlDBConnection.ExecuteNonQuery(StoredProc.SP_Product, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteProduct(int ProductId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ProductId", ProductId),
                    new SqlParameter("@ActionId",4)
                };
                _sqlDBConnection.ExecuteNonQuery(StoredProc.SP_Product, CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {

                throw ex;
 
           }
        }

       

        public DataTable EditProduct(int ProductId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@ProductId", ProductId),
                new SqlParameter("@ActionId",3)
            };
                dt = _sqlDBConnection.ExecuteTable(StoredProc.SP_Product, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}