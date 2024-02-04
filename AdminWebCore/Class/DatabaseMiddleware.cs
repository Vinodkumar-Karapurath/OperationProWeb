using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using AdminWebCore.Models;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace AdminWebCore.Class
{
    public class DatabaseMiddleware : IDisposable
    {
        public SqlConnection Connection { get; }
        public string _Filepath { get; set; }

        private string key = "ce455678huyu4e44566gthy56aswq997";

        public DatabaseMiddleware(string connectionString)
        {
            string connectionStringDec = connectionString; //DecryptString(key, connectionString);
            Connection = new SqlConnection(connectionStringDec);
        }

        //CustomerList Function
        public List<WarningModel> WarningList()
        {
            List<WarningModel> warningModel = new List<WarningModel>();
           
            try
            {


                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_Warning";
                reader = cmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();

               

                if (dt.Rows.Count > 0)
                {

                    warningModel = dt.AsEnumerable()
                        .Select(row => new WarningModel

                        {
                            id = row.Field<Int32>("id"),
                            EMPID = row.Field<string>("EMPID"),
                            EmpName = row.Field<string>("EmpName"),
                            Mobile = row.Field<string>("Mobile"),
                            TypeId = row.Field<string>("TypeId"),
                            ExpDate = row.Field<DateTime?>("ExpDate"),
                            trantypes = row.Field<string>("trantypes"),
                            VehicleID = row.Field<Int32>("VehicleID"),
                           
                        }).ToList();

                }
                             
               
            }
            catch (System.Data.SqlClient.SqlException ex)
            { 
                LogMiddleware.LogData(_Filepath, ex.Message);
                return warningModel;
            }
            finally { Connection.Close(); }

            return warningModel;

        }

        //CustomerList Function
        public List<MasterModel> MasterList(string Type)
        {
            List<MasterModel> masterModel = new List<MasterModel>();

            try
            {


                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.CommandText = "SP_Masters";
                reader = cmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();



                if (dt.Rows.Count > 0)
                {

                    masterModel = dt.AsEnumerable()
                        .Select(row => new MasterModel

                        {
                            id = row.Field<Int32>("id"),
                            MasterName = row.Field<string>("MasterName")
                         
                        }).ToList();

                }


            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                LogMiddleware.LogData(_Filepath, ex.Message);
                return masterModel;
            }
            finally { Connection.Close(); }


            return masterModel;

        }


        public string MastersSave(MastersAddModel master)
        {

            string result = string.Empty;
            try
            {

                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_SaveMaster";
                cmd.Parameters.AddWithValue("@MasterName", master.MasterName);
                cmd.Parameters.AddWithValue("@Type", master.Type);
                

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0) + " " + reader.GetString(1);
    
                }
                reader.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
               
                LogMiddleware.LogData(_Filepath, ex.Message);
                return result;
            }
            finally { Connection.Close(); }

            return result;

        }

        public string MastersUpdate(MastersAddModel master)
        {

            string result = string.Empty;
            try
            {

                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UpdateMaster";
                cmd.Parameters.AddWithValue("@ID", master.IDS);
                cmd.Parameters.AddWithValue("@MasterName", master.MasterName);
                cmd.Parameters.AddWithValue("@Type", master.Type);


                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0) + " " + reader.GetString(1);

                }
                reader.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                LogMiddleware.LogData(_Filepath, ex.Message);
                return result;
            }
            finally { Connection.Close(); }

            return result;

        }

        public List<StaffModel> StaffList(int Type = 0, string condtion = "")
        {
            List<StaffModel> staffModel = new List<StaffModel>();

            try
            {


                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type", Type);
                cmd.Parameters.AddWithValue("@condtion", condtion);
                cmd.CommandText = "SP_GETALLStaff";
                reader = cmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();



                if (dt.Rows.Count > 0)
                {

                    staffModel = dt.AsEnumerable()
                        .Select(row => new StaffModel

                        {
                            ID = row.Field<Int32>("id"),
                            EmpName = row.Field<string>("EmpName"),
                            EMPID = row.Field<string>("EMPID"),
                            SARID = row.Field<string>("SARID"),
                            SARExpDate = row.Field<DateTime?>("SARExpDate")?.ToShortDateString(),
                            TGID = row.Field<string>("TGID"),
                            Company = row.Field<string>("Company"),
                            Department = row.Field<string>("Department"),
                            Email = row.Field<string>("Email"),
                            IqamaExpDate = row.Field<Nullable<DateTime>>("IqamaExpDate")?.ToShortDateString(),
                            IqamaID = row.Field<string>("IqamaID"),
                            JobTitle = row.Field<string>("JobTitle"),
                            Mobile = row.Field<string>("Mobile"),
                            Location = row.Field<string>("Location"),
                            Passport = row.Field<string>("Passport"),
                            PassportExpDate = row.Field<DateTime?>("PassportExpDate")?.ToShortDateString(),


                        }).ToList();

                }


            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                LogMiddleware.LogData(_Filepath, ex.Message);
                return staffModel;
            }
            finally { Connection.Close(); }

            return staffModel;

        }

        public string StaffSave(StaffModel staff)
        {

            string result = string.Empty;
            try
            {

                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SaveStaff";
                cmd.Parameters.AddWithValue("@EmpName", staff.EmpName);
                cmd.Parameters.AddWithValue("@EMPID", staff.EMPID);
                cmd.Parameters.AddWithValue("@TGID", staff.TGID);
                cmd.Parameters.AddWithValue("@Email", staff.Email);
                cmd.Parameters.AddWithValue("@Mobile", staff.Mobile);
                cmd.Parameters.AddWithValue("@IqamaID", staff.IqamaID);
                cmd.Parameters.AddWithValue("@IqamaExpDate", staff.IqamaExpDate);
                cmd.Parameters.AddWithValue("@SARID", staff.SARID);
                cmd.Parameters.AddWithValue("@SARExpDate", staff.SARExpDate);
                cmd.Parameters.AddWithValue("@Passport", staff.Passport);
                cmd.Parameters.AddWithValue("@PassportExpDate", staff.PassportExpDate);
                cmd.Parameters.AddWithValue("@Company", staff.Company);
                cmd.Parameters.AddWithValue("@Department", staff.Department);
                cmd.Parameters.AddWithValue("@JobTitle", staff.JobTitle);
                cmd.Parameters.AddWithValue("@Location", staff.Location);
                cmd.Parameters.AddWithValue("@IqamacontentType", staff.IqamacontentType);
                cmd.Parameters.AddWithValue("@iqamaData", staff.iqamaData);
                cmd.Parameters.AddWithValue("@SARcontentType", staff.SARcontentType);
                cmd.Parameters.AddWithValue("@SARData", staff.SARData);
                cmd.Parameters.AddWithValue("@PassportcontentType", staff.PassportcontentType);
                cmd.Parameters.AddWithValue("@PassportData", staff.PassportData);
                cmd.Parameters.AddWithValue("@PhotoData", staff.PhotoData);


                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0) + " " + reader.GetString(1);

                }
                reader.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                LogMiddleware.LogData(_Filepath, ex.Message);
                return result;
            }
            finally { Connection.Close(); }

            return result;

        }

        public StaffEditModels GetStaffEdit(int Id = 0)
        {
            StaffEditModels staffModel = new StaffEditModels();

            try
            {


                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandText = "SP_GETStaff";
                reader = cmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();



                if (dt.Rows.Count > 0)
                {

                    staffModel = dt.AsEnumerable()
                        .Select(row => new StaffEditModels

                        {
                            ID = row.Field<Int32>("id"),
                            EmpName = row.Field<string>("EmpName"),
                            EMPID = row.Field<string>("EMPID"),
                            SARID = row.Field<string>("SARID"),
                            SARExpDate = row.Field<DateTime?>("SARExpDate"),
                            TGID = row.Field<string>("TGID"),
                            Company = row.Field<int>("CompanyID").ToString(),
                            Department = row.Field<int>("Depart").ToString(),
                            Email = row.Field<string>("Email"),
                            IqamaExpDate = row.Field<DateTime?>("IqamaExpDate"),
                            IqamaID = row.Field<string>("IqamaID"),
                            JobTitle = row.Field<int>("JobTitle").ToString(),
                            Mobile = row.Field<string>("Mobile"),
                            Location = row.Field<int>("Location").ToString(),
                            Passport = row.Field<string>("Passport"),
                            PassportExpDate = row.Field<DateTime?>("PassportExpDate"),
                            IqamacontentType =  row.Field<string>("IqamacontentType"),
                            iqamaData = row.Field<byte[]>("iqamaData"),
                            SARcontentType = row.Field<string>("SARcontentType"),
                            SARData = row.Field<byte[]>("SARData"),
                            PassportcontentType = row.Field<string>("PassportcontentType"),
                            PassportData = row.Field<byte[]>("PassportData"),
                            PhotoData = row.Field<byte[]>("PhotoData"),

                        }).FirstOrDefault();

                }


            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                LogMiddleware.LogData(_Filepath, ex.Message);
                return staffModel;
            }
            finally { Connection.Close(); }

            return staffModel;

        }

        public JsonResult GetPDF(int DOCID = 0,int  MasterID =0 )
        {
            byte[] bytes;
            string fileName, contentType;

            try
            {


                Connection.Open();

              

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DOCID", DOCID);
                cmd.Parameters.AddWithValue("@MasterID", MasterID);

                cmd.CommandText = "SP_GetPdf";

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["Name"].ToString();
                }


               
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                LogMiddleware.LogData(_Filepath, ex.Message);
                return null;
            }
            finally { Connection.Close(); }

            return new JsonResult(new { FileName = fileName, ContentType = contentType, Data = bytes });
        }

        public string StaffUpdate(StaffEditModels staff)
        {

            string result = string.Empty;
            try
            {

                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_UpdateStaff";
                cmd.Parameters.AddWithValue("@ID", staff.ID);
                cmd.Parameters.AddWithValue("@EmpName", staff.EmpName);
                cmd.Parameters.AddWithValue("@EMPID", staff.EMPID);
                cmd.Parameters.AddWithValue("@TGID", staff.TGID);
                cmd.Parameters.AddWithValue("@Email", staff.Email);
                cmd.Parameters.AddWithValue("@Mobile", staff.Mobile);
                cmd.Parameters.AddWithValue("@IqamaID", staff.IqamaID);
                cmd.Parameters.AddWithValue("@IqamaExpDate", staff.IqamaExpDate);
                cmd.Parameters.AddWithValue("@SARID", staff.SARID);
                cmd.Parameters.AddWithValue("@SARExpDate", staff.SARExpDate);
                cmd.Parameters.AddWithValue("@Passport", staff.Passport);
                cmd.Parameters.AddWithValue("@PassportExpDate", staff.PassportExpDate);
                cmd.Parameters.AddWithValue("@Company", staff.Company);
                cmd.Parameters.AddWithValue("@Department", staff.Department);
                cmd.Parameters.AddWithValue("@JobTitle", staff.JobTitle);
                cmd.Parameters.AddWithValue("@Location", staff.Location);
                cmd.Parameters.AddWithValue("@IqamacontentType", staff.IqamacontentType);
                cmd.Parameters.AddWithValue("@iqamaData", staff.iqamaData);
                cmd.Parameters.AddWithValue("@SARcontentType", staff.SARcontentType);
                cmd.Parameters.AddWithValue("@SARData", staff.SARData);
                cmd.Parameters.AddWithValue("@PassportcontentType", staff.PassportcontentType);
                cmd.Parameters.AddWithValue("@PassportData", staff.PassportData);
                cmd.Parameters.AddWithValue("@PhotoData", staff.PhotoData);

                cmd.Parameters.AddWithValue("@isIqamaUpdate", staff.isIqamaUpdate);
                cmd.Parameters.AddWithValue("@isSarUpdate", staff.isSarUpdate);
                cmd.Parameters.AddWithValue("@isPassUpdate", staff.isPassUpdate);
                cmd.Parameters.AddWithValue("@isPhotoUpdate", staff.isPhotoUpdate);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0) + " " + reader.GetString(1);

                }
                reader.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                LogMiddleware.LogData(_Filepath, ex.Message);
                return result;
            }
            finally { Connection.Close(); }

            return result;

        }

        public List<VehicleModel> VehicleList(int Type = 0, string condtion = "")
        {
            List<VehicleModel> vehicleModel = new List<VehicleModel>();

            try
            {


                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@type", Type);
                cmd.Parameters.AddWithValue("@condtion", condtion);
                cmd.CommandText = "SP_GETALLVehicle";
                reader = cmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();



                if (dt.Rows.Count > 0)
                {

                    vehicleModel = dt.AsEnumerable()
                        .Select(row => new VehicleModel

                        {
                            ID = row.Field<Int32>("id"),
                            EmpName = row.Field<string>("EmpName"),
                            EMPID = row.Field<string>("EMPID"),
                            Vehicle = row.Field<string>("Vehicle"),
                            VehicleExpDate = row.Field<DateTime?>("VehicleExpDate")?.ToShortDateString(),
                            InsuranceExpDate = row.Field<DateTime?>("InsuranceExpDate")?.ToShortDateString(),
                            AuthorizationExpDate = row.Field<DateTime?>("AuthorizationExpDate")?.ToShortDateString(),

                            Model = row.Field<string>("Model"),
                            plateNo = row.Field<string>("plateNo"),
                            Type = row.Field<string>("Type"),
                           


                        }).ToList();

                }


            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                LogMiddleware.LogData(_Filepath, ex.Message);
                return vehicleModel;
            }
            finally { Connection.Close(); }

            return vehicleModel;

        }

        public string VehicleSave(VehicleModel vehicle)
        {

            string result = string.Empty;
            try
            {

                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SaveVehicle";
                cmd.Parameters.AddWithValue("@Vehicle", vehicle.Vehicle);
                cmd.Parameters.AddWithValue("@VehicleExpDate", vehicle.VehicleExpDate);
                cmd.Parameters.AddWithValue("@InsuranceExpDate", vehicle.InsuranceExpDate);
                cmd.Parameters.AddWithValue("@AuthorizationExpDate", vehicle.AuthorizationExpDate);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@plateNo", vehicle.plateNo);
                cmd.Parameters.AddWithValue("@AuthorizationID", vehicle.EMPID);
                cmd.Parameters.AddWithValue("@ContentType", vehicle.ContentType);
                cmd.Parameters.AddWithValue("@PDFFile", vehicle.PDFFile);   

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0) + " " + reader.GetString(1);

                }
                reader.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                LogMiddleware.LogData(_Filepath, ex.Message);
                return result;
            }
            finally { Connection.Close(); }

            return result;

        }

        public VehicleEditModels GetVehicleEdit(int IDS)
        {
            VehicleEditModels VehiclefModel = new VehicleEditModels();

            try
            {


                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", IDS);
                cmd.CommandText = "SP_GETVehicle";
                reader = cmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();



                if (dt.Rows.Count > 0)
                {

                    VehiclefModel = dt.AsEnumerable()
                        .Select(row => new VehicleEditModels

                        {
                            ID = row.Field<Int32>("ID"),
                            EMPID = row.Field<int?>("AuthorizationID").ToString(),
                            AuthorizationExpDate = row.Field<DateTime?>("AuthorizationExpDate"),
                            plateNo = row.Field<string>("plateNo"),
                            Vehicle = row.Field<string>("Vehicle"),
                            VehicleExpDate = row.Field<DateTime?>("VehicleExpDate"),
                            Model = row.Field<string>("Model"),
                            Type = row.Field<int?>("Type").ToString(),
                            InsuranceExpDate = row.Field<DateTime?>("InsuranceExpDate"),
                            ContentType = row.Field<string>("ContentType"),                           
                            PDFFile = row.Field<byte[]>("PDFFile"),
                          

                        }).FirstOrDefault();

                }


            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                LogMiddleware.LogData(_Filepath, ex.Message);
                return VehiclefModel;
            }
            finally { Connection.Close(); }

            return VehiclefModel;
        }

        public JsonResult GetVehiclePDF(int DOCID = 0)
        {
            byte[] bytes;
            string fileName, contentType;

            try
            {


                Connection.Open();



                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DOCID", DOCID);

                cmd.CommandText = "SP_GetVehiclePDF";

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"] ;
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["Name"].ToString();
                }



            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                LogMiddleware.LogData(_Filepath, ex.Message);
                return null;
            }
            finally { Connection.Close(); }

            return new JsonResult(new { FileName = fileName, ContentType = contentType, Data = bytes });
        }

        public string VehicleUpdate(VehicleEditModels vehicle)
        {

            string result = string.Empty;
            try
            {

                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_UpdateVehicle";
                cmd.Parameters.AddWithValue("@ID", vehicle.ID);
                cmd.Parameters.AddWithValue("@Vehicle", vehicle.Vehicle);
                cmd.Parameters.AddWithValue("@VehicleExpDate", vehicle.VehicleExpDate);
                cmd.Parameters.AddWithValue("@InsuranceExpDate", vehicle.InsuranceExpDate);
                cmd.Parameters.AddWithValue("@AuthorizationExpDate", vehicle.AuthorizationExpDate);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@plateNo", vehicle.plateNo);
                cmd.Parameters.AddWithValue("@AuthorizationID", vehicle.EMPID);
                cmd.Parameters.AddWithValue("@ContentType", vehicle.ContentType);
                cmd.Parameters.AddWithValue("@PDFFile", vehicle.PDFFile);
                cmd.Parameters.AddWithValue("@isUpdate", vehicle.isUpdate);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0) + " " + reader.GetString(1);

                }
                reader.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                LogMiddleware.LogData(_Filepath, ex.Message);
                return result;
            }
            finally { Connection.Close(); }

            return result;

        }


        public List<PaymentListModel> PaymentList(int Slno = 0, string condtion = "")
        {
            List<PaymentListModel> PaymentModelobj = new List<PaymentListModel>();

            try
            {


                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@slno", Slno);
                cmd.Parameters.AddWithValue("@condtion", condtion);
                cmd.CommandText = "SP_GETPayment";
                reader = cmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();



                if (dt.Rows.Count > 0)
                {

                    PaymentModelobj = dt.AsEnumerable()
                        .Select(row => new PaymentListModel

                        {
                            ID = row.Field<Int64>("id"),
                            Details = row.Field<string>("Details"),
                            Trandate = row.Field<string?>("Trandate"),
                            SIte = row.Field<string>("SIte"),
                            SAPNumber = row.Field<string>("SAPNumber"),
                            AdvancePayment = row.Field<decimal?>("AdvancePayment"),
                            credit = row.Field<decimal?>("PaymentSite_credit"),
                            Debit = row.Field<decimal?>("Debit"),
                            Cleared = row.Field<string>("Cleared"),

                        }).ToList();

                }


            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                LogMiddleware.LogData(_Filepath, ex.Message);
                return PaymentModelobj;
            }
            finally { Connection.Close(); }

            return PaymentModelobj;

        }
        //save payment data to database
        public string PaymentSave(PaymentAddModel payment)
        {

            string result = string.Empty;
            try
            {

                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "SP_SavePayment";
                cmd.Parameters.AddWithValue("@TranscationDate", payment.TranscationDate);
                cmd.Parameters.AddWithValue("@Note", payment.Note);
                cmd.Parameters.AddWithValue("@Site", payment.Site);
                cmd.Parameters.AddWithValue("@Sapinput", payment.Sapinput);
                cmd.Parameters.AddWithValue("@amount", payment.amount);
                cmd.Parameters.AddWithValue("@PDFFile", payment.PDFFile);
                cmd.Parameters.AddWithValue("@ContentType", payment.ContentType);
                cmd.Parameters.AddWithValue("@opttype", payment.opttype);
                cmd.Parameters.AddWithValue("@isUpdate", payment.isUpdate);
                cmd.Parameters.AddWithValue("@ID", payment.ID);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0) + " " + reader.GetString(1);

                }
                reader.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                LogMiddleware.LogData(_Filepath, ex.Message);
                return result;
            }
            finally { Connection.Close(); }

            return result;

        }

        //get how many pages in the database for payment
        public int PaymentPageCount(int slno, string condtion)
        {
            int result = 0;
            try
            {

                Connection.Open();

                IDataReader reader = null;

                using var cmd = Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "SP_GetPaymentPageCount";
                cmd.Parameters.AddWithValue("@Slno", slno);
                cmd.Parameters.AddWithValue("@condtion", condtion);


                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0);

                }
                reader.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                LogMiddleware.LogData(_Filepath, ex.Message);
                return result;
            }
            finally { Connection.Close(); }

            return result;

        }
            

        //public string DecryptString(string key, string cipherText)
        //{
        //    byte[] iv = new byte[16];
        //    byte[] buffer = Convert.FromBase64String(cipherText);

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes(key);
        //        aes.IV = iv;
        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        //        using (MemoryStream memoryStream = new MemoryStream(buffer))
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
        //                {
        //                    return streamReader.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}

        //public string EncryptString(string key, string plainText)
        //{
        //    byte[] iv = new byte[16];
        //    byte[] array;

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes(key);
        //        aes.IV = iv;

        //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
        //                {
        //                    streamWriter.Write(plainText);
        //                }

        //                array = memoryStream.ToArray();
        //            }
        //        }
        //    }

        //    return Convert.ToBase64String(array);
        //}

        ////Login Function
        //public LoginResponds Login(LoginRequest loginRequest)
        //{
        //    LoginResponds loginResponds = new LoginResponds();
        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetLogin";
        //        cmd.Parameters.AddWithValue("@salesMan_code", loginRequest.salesMan_code);
        //        cmd.Parameters.AddWithValue("@salesman_pass", loginRequest.salesman_pass);
        //        cmd.Parameters.AddWithValue("@DeviceSerial", loginRequest.DeviceSerial);
        //        reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            loginResponds.salesMan_id = reader.GetInt32(0);
        //            loginResponds.resultMsg = reader.GetString(1);
        //            loginResponds.result = reader.GetInt32(2);
        //        }
        //        reader.Close();

        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        loginResponds.result = 0;
        //        loginResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);

        //        return loginResponds;
        //    }
        //    return loginResponds;

        //}

        ////Logout Function
        //public LogoutModelResponds Logout(LogoutModelRequest logoutModelRequest)
        //{
        //    LogoutModelResponds logoutModelResponds = new LogoutModelResponds();
        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetLogout";
        //        cmd.Parameters.AddWithValue("@salesMan_id", logoutModelRequest.salesMan_id);
        //        cmd.Parameters.AddWithValue("@UnRegister", logoutModelRequest.UnRegister);
        //        reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {

        //            logoutModelResponds.resultMsg = reader.GetString(0);
        //            logoutModelResponds.result = reader.GetInt32(1);
        //        }
        //        reader.Close();

        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        logoutModelResponds.result = 0;
        //        logoutModelResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);

        //        return logoutModelResponds;
        //    }
        //    return logoutModelResponds;

        //}

        ////CustomerList Function
        //public CustomerModelRootResponds CustomerList(CustomerModelRequest customerModelRequest)
        //{
        //    CustomerModelRootResponds customerModelRootResponds = new CustomerModelRootResponds();
        //    customerModelRootResponds.customerModelResponds = new List<CustomerModelResponds>();
        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetCustomers";
        //        cmd.Parameters.AddWithValue("@salesMan_id", customerModelRequest.salesMan_id);
        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            customerModelRootResponds.customerModelResponds.Add(new CustomerModelResponds()
        //            {
        //                customer_id = reader.GetInt32(0),
        //                customer_name = reader.GetString(1),
        //                VAT_No = reader.GetString(2),
        //                Customer_Type = reader.GetByte(3),
        //                contact_person = reader.GetString(4),
        //                Area_id = reader.GetInt32(5),
        //                Salesman_id = reader.GetInt32(6),
        //                Additional_No = reader.GetString(7),
        //                Building_unit = reader.GetString(8),
        //                Building_No = reader.GetString(9),
        //                street = reader.GetString(10),
        //                District = reader.GetString(11),
        //                City = reader.GetString(12),
        //                zip = reader.GetString(13),
        //                box_no = reader.GetString(14),
        //                phone = reader.GetString(15),
        //                mobile = reader.GetString(16),
        //                email = reader.GetString(17),
        //                opening_credit = reader.GetDouble(18),
        //                credit_limit = reader.GetDouble(19),
        //                finyear = reader.GetString(20),
        //                remarks = reader.GetString(21),
        //                Ac_Head_id = reader.GetInt32(22),
        //                vat_perc = reader.GetDouble(23),
        //                EnablePriceEdit = reader.GetInt32(24)
        //            });

        //        }
        //        reader.Close();
        //        customerModelRootResponds.result = 1;
        //        customerModelRootResponds.resultMsg = "";
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        customerModelRootResponds.result = 0;
        //        customerModelRootResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return customerModelRootResponds;
        //    }

        //    return customerModelRootResponds;

        //}

        ////Customer saving Function
        //public NewCustomerModelResponds CustomerSave(NewCustomerModelRequest newCustomerModelRequest)
        //{
        //    NewCustomerModelResponds newCustomerModelResponds = new NewCustomerModelResponds();

        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_SaveCustomerMaster";
        //        cmd.Parameters.AddWithValue("@customer_name", newCustomerModelRequest.customer_name);
        //        cmd.Parameters.AddWithValue("@VAT_No", newCustomerModelRequest.VAT_No);
        //        cmd.Parameters.AddWithValue("@Customer_Type", newCustomerModelRequest.Customer_Type);
        //        cmd.Parameters.AddWithValue("@contact_person", newCustomerModelRequest.contact_person);
        //        cmd.Parameters.AddWithValue("@Area_id", newCustomerModelRequest.Area_id);
        //        cmd.Parameters.AddWithValue("@Salesman_id", newCustomerModelRequest.Salesman_id);
        //        cmd.Parameters.AddWithValue("@Additional_No", newCustomerModelRequest.Additional_No);
        //        cmd.Parameters.AddWithValue("@Building_unit", newCustomerModelRequest.Building_unit);
        //        cmd.Parameters.AddWithValue("@Building_No", newCustomerModelRequest.Building_No);
        //        cmd.Parameters.AddWithValue("@street", newCustomerModelRequest.street);
        //        cmd.Parameters.AddWithValue("@District", newCustomerModelRequest.District);
        //        cmd.Parameters.AddWithValue("@City", newCustomerModelRequest.City);
        //        cmd.Parameters.AddWithValue("@zip", newCustomerModelRequest.zip);
        //        cmd.Parameters.AddWithValue("@box_no", newCustomerModelRequest.box_no);
        //        cmd.Parameters.AddWithValue("@phone", newCustomerModelRequest.phone);
        //        cmd.Parameters.AddWithValue("@mobile", newCustomerModelRequest.mobile);
        //        cmd.Parameters.AddWithValue("@email", newCustomerModelRequest.email);
        //        cmd.Parameters.AddWithValue("@opening_credit", newCustomerModelRequest.opening_credit);
        //        cmd.Parameters.AddWithValue("@credit_limit", newCustomerModelRequest.credit_limit);
        //        cmd.Parameters.AddWithValue("@finyear", newCustomerModelRequest.finyear);
        //        cmd.Parameters.AddWithValue("@remarks", newCustomerModelRequest.remarks);
        //        cmd.Parameters.AddWithValue("@Ac_Head_id", newCustomerModelRequest.Ac_Head_id);
        //        cmd.Parameters.AddWithValue("@vat_perc", newCustomerModelRequest.vat_perc);
        //        cmd.Parameters.AddWithValue("@newUpdate_flag", newCustomerModelRequest.newUpdate_flag);

        //        reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            newCustomerModelResponds.resultMsg = reader.GetString(0);
        //            newCustomerModelResponds.result = reader.GetInt32(1);                       
        //            newCustomerModelResponds.customer_id = reader.GetInt32(2);

        //        }
        //        reader.Close();
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        newCustomerModelResponds.result = 0;
        //        newCustomerModelResponds.resultMsg = ex.Message;
        //        newCustomerModelResponds.customer_id = 0;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return newCustomerModelResponds;
        //    }

        //    return newCustomerModelResponds;

        //}


        ////Itemmaster list Function
        //public ItemMasterModelRootResponds ItemMasterList()
        //{
        //    ItemMasterModelRootResponds itemMasterModelRootResponds = new ItemMasterModelRootResponds();
        //    itemMasterModelRootResponds.itemMasterModels = new List<ItemMasterModel>();
        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetItemMaster";
        //        reader = cmd.ExecuteReader();


        //        DataTable dt = new DataTable();
        //        dt.Load(reader);
        //        reader.Close();


        //        if (dt.Rows.Count > 0)
        //        {

        //            itemMasterModelRootResponds.itemMasterModels = dt.AsEnumerable()
        //                .Select(row => new ItemMasterModel

        //                {
        //                    item_id = row.Field<Int32>("item_id"),
        //                    item_code = row.Field<string>("item_code"),
        //                    item_name = row.Field<string>("item_name"),
        //                    unit_mesure = row.Field<string>("unit_mesure"),
        //                    item_type = row.Field<Int32>("item_type"),
        //                    unit_price_purchase = row.Field<double>("unit_price_purchase"),
        //                    unit_price_sales = row.Field<double>("unit_price_sales"),
        //                    item_Category_id = row.Field<Int32>("item_Category_id"),
        //                    opening_quantity =  row.Field<double>("opening_quantity"),
        //                    opening_stock_value = row.Field<double>("opening_stock_value"),
        //                    rack_no = row.Field<string>("rack_no"),
        //                    Stock = row.Field<Int32>("Stock"),
        //                    Stock_val = row.Field<double>("Stock_val"),
        //                    this_sales = row.Field<Int32>("this_sales"),
        //                    average_item_value = row.Field<double>("average_item_value"),
        //                    EnablePriceEdit = row.Field<Int32>("EnablePriceEdit")
        //                }).ToList();

        //        }


        //        itemMasterModelRootResponds.result = 1;
        //        itemMasterModelRootResponds.resultMsg = "";
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        itemMasterModelRootResponds.result = 0;
        //        itemMasterModelRootResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return itemMasterModelRootResponds;
        //    }

        //    return itemMasterModelRootResponds;

        //}

        ////ItemDetails list Function
        //public ItemDetailsModelRootResponds ItemDetailsList()
        //{
        //    ItemDetailsModelRootResponds itemDetailsModelRootResponds = new ItemDetailsModelRootResponds();
        //    itemDetailsModelRootResponds.itemDetailsModels = new List<ItemDetailsModel>();
        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetItemDetails";
        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            itemDetailsModelRootResponds.itemDetailsModels.Add(new ItemDetailsModel()
        //            {
        //                Unit_id = reader.GetInt32(0),
        //                item_id = reader.GetInt32(1),
        //                Item_Code = reader.GetString(2),
        //                Unit_Mes = reader.GetString(3),
        //                Inv_Qty = reader.GetDouble(4),
        //                Inv_Mes = reader.GetString(5),
        //                purchase_Rate = reader.GetDouble(6),
        //                Sales_Rate = reader.GetDouble(7),
        //                IsDefault = reader.GetBoolean(8) ,
        //                inv_id = reader.GetInt32(9)
        //            });

        //        }
        //        reader.Close();
        //        itemDetailsModelRootResponds.result = 1;
        //        itemDetailsModelRootResponds.resultMsg = "";
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        itemDetailsModelRootResponds.result = 0;
        //        itemDetailsModelRootResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return itemDetailsModelRootResponds;
        //    }

        //    return itemDetailsModelRootResponds;

        //}

        ////CustomerList Function
        //public WHStockModelRootResponds WHStockList(WHStockModelRequest wHStockModelRequest)
        //{
        //    WHStockModelRootResponds wHStockModelRootResponds = new WHStockModelRootResponds();
        //    wHStockModelRootResponds.wHStockModelResponds = new List<WHStockModelResponds>();
        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetWHStock";
        //        cmd.Parameters.AddWithValue("@salesMan_id", wHStockModelRequest.salesMan_id);
        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            wHStockModelRootResponds.wHStockModelResponds.Add(new WHStockModelResponds()
        //            {
        //                Entry_Id = reader.GetInt32(0),
        //                WH_Id = reader.GetInt32(1),
        //                item_id = reader.GetInt32(2),
        //                opening_quantity = reader.GetDouble(3),
        //                opening_stock_value = reader.GetDouble(4),
        //                rack_no = reader.GetString(5),
        //                Stock = reader.GetInt32(6),
        //                average_item_value = reader.GetDouble(7),
        //                Price_List = reader.GetDouble(8),
        //                reOrder_level = reader.GetInt32(9),

        //            });

        //        }
        //        reader.Close();
        //        wHStockModelRootResponds.result = 1;
        //        wHStockModelRootResponds.resultMsg = "";
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        wHStockModelRootResponds.result = 0;
        //        wHStockModelRootResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return wHStockModelRootResponds;
        //    }

        //    return wHStockModelRootResponds;

        //}


        ////Customer outstanding List Function
        //public CustomerOutstandingModelResponds OutStandingList(CustomerOutstandingModelRequest customerOutstandingModelRequest)
        //{
        //    CustomerOutstandingModelResponds customerOutstandingModelResponds = new CustomerOutstandingModelResponds();

        //    customerOutstandingModelResponds.customerOutstanding = new List<CustomerOutstandingModel>();
        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetCustomerOutstanding";
        //        cmd.Parameters.AddWithValue("@salesman_id", customerOutstandingModelRequest.salesman_id);
        //        cmd.Parameters.AddWithValue("@customer_id", customerOutstandingModelRequest.customer_id);
        //        reader = cmd.ExecuteReader();

        //        DataTable dt = new DataTable();
        //        dt.Load(reader);
        //        reader.Close();

        //        if (dt.Rows.Count > 0)
        //        {

        //            customerOutstandingModelResponds.customerOutstanding = dt.AsEnumerable()
        //                                                                    .Select(row => new CustomerOutstandingModel
        //                                                                    {
        //                                                                       customer_id = row.Field<Int32>("customer_id"),
        //                                                                       OutstandingType = row.Field<Int32>("OutstandingType"),
        //                                                                       salesorReturn_id = row.Field<Int32>("salesorReturn_id"),
        //                                                                        Bill_no = row.Field<Int32>("Bill_no"),
        //                                                                        Bill_date = row.Field<string>("Bill_date"),
        //                                                                        net_amount =(float) row.Field<double>("net_amount"),
        //                                                                        total_payment = (float)row.Field<double>("total_payment"),
        //                                                                        Due_amount = (float)row.Field<double>("Due_amount")

        //                                                                    }).ToList();

        //        }

        //        customerOutstandingModelResponds.result = 1;
        //        customerOutstandingModelResponds.resultMsg = "";
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        customerOutstandingModelResponds.result = 0;
        //        customerOutstandingModelResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return customerOutstandingModelResponds;
        //    }

        //    return customerOutstandingModelResponds;

        //}

        ////Customer Return reference List Function
        //public ReturnReferenceModelResponds ReturnReferenceList(ReturnReferenceModelRequest returnReferenceModelRequest)
        //{
        //    ReturnReferenceModelResponds returnReferenceModelResponds = new ReturnReferenceModelResponds();
        //    returnReferenceModelResponds.returnReferenceModels = new List<ReturnReferenceModel>();
        //    returnReferenceModelResponds.returnReferenceItemModels = new List<ReturnReferenceItemModel>();

        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetReturnReferance";
        //        cmd.Parameters.AddWithValue("@customer_id", returnReferenceModelRequest.customer_id);
        //        reader = cmd.ExecuteReader();

        //        DataTable dt = new DataTable();
        //        dt.Load(reader);
        //        DataTable dtDetail = new DataTable();
        //        dtDetail.Load(reader);

        //        reader.Close();

        //        if (dt.Rows.Count > 0)
        //        {

        //            returnReferenceModelResponds.returnReferenceModels = dt.AsEnumerable()
        //                                                                    .Select(row => new ReturnReferenceModel
        //                                                                    {
        //                                                                        customer_id = row.Field<Int32>("customer_id"),
        //                                                                        sales_id = row.Field<Int32>("sales_id"),
        //                                                                        Bill_no = row.Field<Int32>("Bill_no"),
        //                                                                        Bill_date = row.Field<string>("Bill_date"),
        //                                                                        net_amount = (float)row.Field<double>("net_amount"),
        //                                                                        discount = (float)row.Field<double>("discount")
        //                                                                    }).ToList();

        //        }
        //        if (dtDetail.Rows.Count > 0)
        //        {

        //            returnReferenceModelResponds.returnReferenceItemModels = dtDetail.AsEnumerable()
        //                                                                    .Select(row => new ReturnReferenceItemModel
        //                                                                    {
        //                                                                        sales_id = row.Field<Int32>("sales_id"),
        //                                                                        item_id = row.Field<Int32>("item_id"),
        //                                                                        item_quantity = (float) row.Field<double>("item_quantity"),
        //                                                                        unit_mes =row.Field<string>("unit_mes"),
        //                                                                        total_line = (float)row.Field<double>("total_line"),
        //                                                                        total_price = (float)row.Field<double>("total_price"),
        //                                                                        Unit_Disc = (float)row.Field<double>("Unit_Disc"),
        //                                                                        Unit_Disc_Perc = (float)row.Field<double>("Unit_Disc_Perc"),
        //                                                                        Unit_price = (float)row.Field<double>("Unit_price"),
        //                                                                        vat_amt = (float)row.Field<double>("vat_amt"),
        //                                                                        vat_perc = (float)row.Field<double>("vat_perc"),
        //                                                                        inv_id = row.Field<Int32>("inv_id"),
        //                                                                        item_code = row.Field<string>("item_code"),
        //                                                                        item_name = row.Field<string>("item_name"),
        //                                                                    }).ToList();

        //        }

        //        returnReferenceModelResponds.result = 1;
        //        returnReferenceModelResponds.resultMsg = "";
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        returnReferenceModelResponds.result = 0;
        //        returnReferenceModelResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return returnReferenceModelResponds;
        //    }

        //    return returnReferenceModelResponds;

        //}

        ////Company Function
        //public CompanyModel CompanyList()
        //{
        //    CompanyModel companyModelRespond = new CompanyModel();

        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetCompany";

        //        reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {

        //            companyModelRespond.comapny_id = reader.GetInt32(0);
        //            companyModelRespond.comapny_name = reader.GetString(1);
        //            companyModelRespond.VAT_No = reader.GetString(2);
        //            companyModelRespond.contact_person = reader.GetString(3);
        //            companyModelRespond.Area_id = reader.GetInt32(4);
        //            companyModelRespond.Additional_No = reader.GetString(5);
        //            companyModelRespond.Building_unit = reader.GetString(6);
        //            companyModelRespond.Building_No = reader.GetString(7);
        //            companyModelRespond.street = reader.GetString(8);
        //            companyModelRespond.District = reader.GetString(9);
        //            companyModelRespond.City = reader.GetString(10);
        //            companyModelRespond.zip = reader.GetString(11);
        //            companyModelRespond.box_no = reader.GetString(12);
        //            companyModelRespond.phone = reader.GetString(13);
        //            companyModelRespond.mobile = reader.GetString(14);
        //            companyModelRespond.email = reader.GetString(15);
        //            companyModelRespond.Company_nameQR = reader.GetString(16);


        //        }
        //        reader.Close();
        //        companyModelRespond.result = 1;
        //        companyModelRespond.resultMsg = "";
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        companyModelRespond.result = 0;
        //        companyModelRespond.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return companyModelRespond;
        //    }

        //    return companyModelRespond;

        //}

        ////Customer Invoice List Function
        //public InvoiceListModelResponds GetInvoiceList(InvoiceListModelRequest invoiceListModelRequest)
        //{
        //    InvoiceListModelResponds invoiceListModelResponds = new InvoiceListModelResponds();
        //    invoiceListModelResponds.invoiceListModels = new List<InvoiceListModel>();
        //    invoiceListModelResponds.invoiceListItemModels = new List<InvoiceListItemModel>();

        //    try
        //    {

        //        Connection.Open();

        //        IDataReader reader = null;

        //        using var cmd = Connection.CreateCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "sp_GetInvoiceList";
        //        cmd.Parameters.AddWithValue("@customer_id", invoiceListModelRequest.customer_id);
        //        cmd.Parameters.AddWithValue("@FromDate", invoiceListModelRequest.FromDate);
        //        cmd.Parameters.AddWithValue("@ToDate", invoiceListModelRequest.ToDate);
        //        reader = cmd.ExecuteReader();

        //        DataTable dt = new DataTable();
        //        dt.Load(reader);
        //        DataTable dtDetail = new DataTable();
        //        dtDetail.Load(reader);

        //        reader.Close();

        //        if (dt.Rows.Count > 0)
        //        {

        //            invoiceListModelResponds.invoiceListModels = dt.AsEnumerable()
        //                                                                    .Select(row => new InvoiceListModel
        //                                                                    {
        //                                                                        Type = row.Field<Int32>("Type"),
        //                                                                        customer_id = row.Field<Int32>("customer_id"),
        //                                                                        sales_id = row.Field<Int32>("sales_id"),
        //                                                                        Bill_no = row.Field<Int32>("Bill_no"),
        //                                                                        Bill_date = row.Field<string>("Bill_date"),
        //                                                                        net_amount = (float)row.Field<double>("net_amount"),
        //                                                                        discount = (float)row.Field<double>("discount")
        //                                                                    }).ToList();

        //        }
        //        if (dtDetail.Rows.Count > 0)
        //        {

        //            invoiceListModelResponds.invoiceListItemModels = dtDetail.AsEnumerable()
        //                                                                    .Select(row => new InvoiceListItemModel
        //                                                                    {
        //                                                                        Type = row.Field<Int32>("Type"),
        //                                                                        sales_id = row.Field<Int32>("sales_id"),
        //                                                                        item_id = row.Field<Int32>("item_id"),
        //                                                                        item_quantity = (float)row.Field<double>("item_quantity"),
        //                                                                        unit_mes = row.Field<string>("unit_mes"),
        //                                                                        total_line = (float)row.Field<double>("total_line"),
        //                                                                        total_price = (float)row.Field<double>("total_price"),
        //                                                                        Unit_Disc = (float)row.Field<double>("Unit_Disc"),
        //                                                                        Unit_Disc_Perc = (float)row.Field<double>("Unit_Disc_Perc"),
        //                                                                        Unit_price = (float)row.Field<double>("Unit_price"),
        //                                                                        vat_amt = (float)row.Field<double>("vat_amt"),
        //                                                                        vat_perc = (float)row.Field<double>("vat_perc"),
        //                                                                        inv_id = row.Field<Int32>("inv_id"),
        //                                                                        item_code   = row.Field<string>("item_code"),
        //                                                                        item_name = row.Field<string>("item_name"),
        //                                                                    }).ToList();

        //        }

        //        invoiceListModelResponds.result = 1;
        //        invoiceListModelResponds.resultMsg = "";
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        invoiceListModelResponds.result = 0;
        //        invoiceListModelResponds.resultMsg = ex.Message;
        //        LogMiddleware.LogData(_Filepath, ex.Message);
        //        return invoiceListModelResponds;
        //    }

        //    return invoiceListModelResponds;

        //}

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
