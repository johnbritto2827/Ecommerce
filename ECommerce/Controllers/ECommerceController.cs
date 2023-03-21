using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/***********************************************************************************************************
 * Created by       : John Britto
 * Created On       : 18th Oct 2022
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 ***********************************************************************************************************/
namespace ECommerce.Controllers
{
    public class ECommerceController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _LoginRepository;
        private readonly IProductRepository _ProductRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailRepository _EmailRepository;
        private readonly IHostingEnvironment hostingEnv;

        public ECommerceController(IConfiguration configuration, ILoginRepository LoginRepository, IProductRepository ProductRepository, IOrderRepository orderRepository, IEmailRepository EmailRepository, IHostingEnvironment env)
        {
            this.hostingEnv = env;
            _configuration = configuration;
            _LoginRepository = LoginRepository;
            _ProductRepository = ProductRepository;
            _EmailRepository = EmailRepository;
            _orderRepository = orderRepository;
        }

        public ActionResult CustomerDashboard()
        {

            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        
        public ActionResult CheckOut()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult ProductView()
        {
            return View();
        }

        public ActionResult ProductAdd()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult TrackOrder(int OrderId)
        {
            ECommerceModel ObjTrack = new ECommerceModel();
            object OrdeStatus = _orderRepository.GetMyOrderStatus(OrderId);
            ViewBag.OrderStatus = Convert.ToInt32(OrdeStatus);
            return View();
        }

        public ActionResult LoginActive(Login objModel)
        {
            
            try
            {
                if (objModel != null && objModel.Email == "Admin@gmail.com" && objModel.Password == "Admin")
                {
                    return RedirectToAction("AdminDashboard");
                }
                else
                {
                    Login Login = new Login();
                    Login = _LoginRepository.CheckLogin(objModel.Email, objModel.Password);
                    if (Login.Email != null && Login.Password != null)
                    {
                        HttpContext.Session.SetInt32("LoginUserId", Login.UserId);

                        return RedirectToAction("CustomerDashboard");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return RedirectToAction("Login");
        }

        public ActionResult AdminLogin(Login objModel)
        {
            try
            {
                if (objModel != null && objModel.Email == "Admin@gmail.com" && objModel.Password == "Admin")
                {
                    return RedirectToAction("AdminDashboard");
                }
                else
                {
                    return RedirectToAction("CustomerDashboard");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      

        public ActionResult ViewProduct()
        {
            ECommerceModel objproduct = new ECommerceModel();

            try
            {
                DataTable dt = new DataTable();
                dt = _ProductRepository.GetProduct();

                objproduct.ListProductModels = (from DataRow dr in dt.Rows
                                                select new ProductAdd()
                                                {
                                                    ProductId = Convert.ToInt32(dr["ProductId"]),
                                                    Brand = dr["Brand"].ToString(),
                                                    Model = dr["Model"].ToString(),
                                                    Price = Convert.ToInt32(dr["Price"]),
                                                    Discount = Convert.ToInt32(dr["Discount"]),
                                                    Specification = dr["Specification"].ToString(),
                                                    Rating = Convert.ToDecimal(dr["Rating"]),
                                                    Colour = dr["Colour"].ToString(),
                                                    Storage = dr["Storage"].ToString(),
                                                    Image = dr["Image"].ToString(),
                                                }).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(objproduct);
        }

     

        public ActionResult OrderProduct(int ProductId)
        {

            ECommerceModel objproduct = new ECommerceModel();
            List<ProductAdd> ProductAdd = new List<ProductAdd>();
            try
            {
                DataTable dt = new DataTable();
                dt = _ProductRepository.GetProduct(ProductId);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductAdd mob = new ProductAdd();
                    mob.ProductId = Convert.ToInt32(dt.Rows[i]["ProductId"]);
                    mob.Brand = dt.Rows[i]["Brand"].ToString();
                    mob.Model = dt.Rows[i]["Model"].ToString();
                    mob.Price = Convert.ToInt32(dt.Rows[i]["Price"]);
                    mob.Discount = Convert.ToInt32(dt.Rows[i]["Discount"]);
                    mob.Specification = dt.Rows[i]["Specification"].ToString();
                    mob.Rating = Convert.ToDecimal(dt.Rows[i]["Rating"]);
                    mob.Colour = dt.Rows[i]["Colour"].ToString();
                    mob.Storage = dt.Rows[i]["Storage"].ToString();
                    mob.Image = dt.Rows[i]["Image"].ToString();
                    ProductAdd.Add(mob);
                }
                objproduct.ListProductModels = ProductAdd;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(objproduct);
        }

    

        #region LoginPage
        public ActionResult ViewLogin()
        {
            try
            {
                ECommerceModel objLogin = new ECommerceModel();
                objLogin.ListLoginModels = _LoginRepository.ViewLogin();
                return View(objLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SaveLogin(Login objModel)
        {
            try
            {

                ECommerceModel objLogin = new ECommerceModel();
                objLogin.ListLoginModels = _LoginRepository.SaveLogin(objModel);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

      

        public ActionResult GetProduct()
        {
            
            ECommerceModel objproduct = new ECommerceModel();
            try
            {
                DataTable dt = new DataTable();
                dt = _ProductRepository.GetProduct();

                objproduct.ListProductModels = (from DataRow dr in dt.Rows
                                                select new ProductAdd()
                                                {
                                                    ProductId = Convert.ToInt32(dr["ProductId"]),
                                                    Brand = dr["Brand"].ToString(),
                                                    Model = dr["Model"].ToString(),
                                                    Price = Convert.ToInt32(dr["Price"]),
                                                    Discount = Convert.ToInt32(dr["Discount"]),
                                                    Specification = dr["Specification"].ToString(),
                                                    QtyinStock = Convert.ToInt32(dr["QtyinStock"]),
                                                    Rating = Convert.ToDecimal(dr["Rating"]),
                                                    Colour = dr["Colour"].ToString(),
                                                    Storage = dr["Storage"].ToString(),
                                                    Image = dr["Image"].ToString(),
                                                }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objproduct);
        }

        public ActionResult SaveProduct(ProductAdd objModel,int UserId,int ProductId)
        {
            ECommerceModel objproduct = new ECommerceModel();
          
            try
            {
                _ProductRepository.SaveProduct(objModel,  UserId,  ProductId);
                TempData["issuccess"] = "1";
            }
            catch (Exception ex)
            {
                TempData["issuccess"] = "0";
                throw ex;
            }
            return RedirectToAction("GetProduct");
        }

        public ActionResult EditProduct(int ProductId)
        {
            DataTable dataTable = _ProductRepository.EditProduct(ProductId);
            ECommerceModel product = new ECommerceModel();
            try
            {
                ProductAdd ProductModel = new ProductAdd();
                if (dataTable.Rows.Count > 0)
                {
                    ProductModel.ProductId = Convert.ToInt32(dataTable.Rows[0]["ProductId"].ToString());
                    ProductModel.Brand = dataTable.Rows[0]["Brand"].ToString();
                    ProductModel.Model = dataTable.Rows[0]["Model"].ToString();
                    ProductModel.Price = Convert.ToInt32(dataTable.Rows[0]["Price"].ToString());
                    ProductModel.Discount = Convert.ToInt32(dataTable.Rows[0]["Discount"].ToString());
                    ProductModel.Specification = dataTable.Rows[0]["Specification"].ToString();
                    ProductModel.QtyinStock = Convert.ToInt32(dataTable.Rows[0]["QtyinStock"].ToString());
                    ProductModel.Rating = Convert.ToDecimal(dataTable.Rows[0]["Rating"].ToString());
                    ProductModel.Colour = dataTable.Rows[0]["Colour"].ToString();
                    ProductModel.Storage = dataTable.Rows[0]["Storage"].ToString();
                    ProductModel.Image = dataTable.Rows[0]["Image"].ToString();
                    product.ProductDetails = ProductModel;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View("ProductAdd", product);

        }

        public IActionResult DeleteProduct(int ProductId)
        {
            try
            {

                _ProductRepository.DeleteProduct(ProductId);
                return RedirectToAction("GetProduct");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public JsonResult UploadFiles()
        {
            string FNames = string.Empty;
            try
            {
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    if (Request.Form.Files[i].FileName != string.Empty)
                    {
                        FNames = SaveUniqueFile(Request.Form.Files[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(FNames);
        }


        public string SaveUniqueFile(IFormFile file)
        {
            var filename = string.Empty;
            try
            {
                filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                if (!Directory.Exists(hostingEnv.WebRootPath + @"\Images\"))
                {
                    Directory.CreateDirectory(hostingEnv.WebRootPath + @"\Images");
                    filename = hostingEnv.WebRootPath + $@"\{ @"\Images\" + file.FileName }";
                }
                else
                {
                    filename = hostingEnv.WebRootPath + $@"\{ @"\Images\" + file.FileName}";
                }

                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return file.FileName;
        }


        public ActionResult GetOrder(int ProductId)
        {
            ECommerceModel objOrder = new ECommerceModel();
            try
            {
                DataTable dt = new DataTable();
                dt = _orderRepository.GetOrder( ProductId);

                objOrder.ListOrderModels = (from DataRow dr in dt.Rows
                                            select new Order()
                                            {
                                                OrderId = Convert.ToInt32(dr["OrderId"]),
                                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                                FullName = dr["FullName"].ToString(),
                                                MobileNumber = dr["MobileNumber"].ToString(),
                                                Address = dr["Address"].ToString(),
                                                City = dr["City"].ToString(),
                                                State = dr["State"].ToString(),
                                                PinCode = Convert.ToInt64(dr["PinCode"]),
                                                AddressType = dr["AddressType"].ToString(),
                                                Status = Convert.ToInt16(dr["Status"]),
                                            }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objOrder);
        }

        public ActionResult SaveOrder(Order objModel)
        {
            ECommerceModel objproduct = new ECommerceModel();
            int UserId = Convert.ToInt16(HttpContext.Session.GetInt32("LoginUserId"));
            int ProductId = Convert.ToInt16(HttpContext.Session.GetInt32("ProductId"));
            try
            {
                _orderRepository.SaveOrder(objModel, UserId, ProductId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Success");
        }
        [HttpGet]
        public JsonResult UpdateStatus(int OrderId, string Status)
        {
            string Result = string.Empty;
            try
            {
                _EmailRepository.UpdateStatus(OrderId, Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(Result);
        }
        public ActionResult MyOrder()
        {
            ECommerceModel objOrder = new ECommerceModel();
            try
            {
                int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("LoginUserId"));
                DataTable dt = new DataTable();
                dt = _orderRepository.GetMyOrder(UserId);
                objOrder.ListOrderModels = (from DataRow dr in dt.Rows
                                            select new Order()
                                            {
                                                OrderId = Convert.ToInt32(dr["OrderId"]),
                                                Brand=dr["Brand"].ToString(),
                                                Model=dr["Model"].ToString(),
                                                Price = Convert.ToInt32(dr["Price"]),
                                                FullName = dr["FullName"].ToString(),
                                                MobileNumber = dr["MobileNumber"].ToString(),
                                                Address = dr["Address"].ToString(),
                                                City = dr["City"].ToString(),
                                                State = dr["State"].ToString(),
                                                PinCode = Convert.ToInt64(dr["PinCode"]),
                                                AddressType = dr["AddressType"].ToString(),
                                                Status = Convert.ToInt16(dr["Status"]),
                                                Image = dr["Image"].ToString(),
                                            }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return View(objOrder);
        }


        public ActionResult GetEmail()
        {
            ECommerceModel objEmail = new ECommerceModel();
            try
            {
                DataTable dt = new DataTable();
                dt = _EmailRepository.GetEmail();

                objEmail.ListEmailModels = (from DataRow dr in dt.Rows
                                            select new EmailInfo()
                                            {
                                                Id = Convert.ToInt32(dr["Id"]),
                                                Email = dr["Email"].ToString(),

                                            }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objEmail);
        }

        public ActionResult SaveEmail(EmailInfo objModel)
        {
            ECommerceModel objproduct = new ECommerceModel();

            try
            {
                _EmailRepository.SaveEmail(objModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("CustomerDashboard");
        }

    }

}