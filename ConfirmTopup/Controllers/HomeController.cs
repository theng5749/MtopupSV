using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using ConfirmTopup.Models;
using ConfirmTopup.ltcsrv;
using ConfirmTopup.sms;
using System.Web.Services.Description;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace ConfirmTopup.Controllers
{
    public class HomeController : Controller
    {
        OracleConnection conn = new OracleConnection();
        OracleCommand cmd = new OracleCommand();
        public ActionResult Index(string id, string type)
        {
            //id = "492";
            //type = "IT";
            ViewBag.ResultCode = "001";
            ViewBag.ResultDesc = "Please try again later";
            ViewBag.Result = "ກະລຸນາລອງໃຫມ່ພາຍຫລັງ";
            ViewBag.Msisdn = "20xxxxxxxx";
            ViewBag.Name = "[Empty]";
            ViewBag.MsisdnApprove = "20xxxxxxxx";
            ViewBag.Amount = "";
            ViewBag.MemberID = "";
            ViewBag.Type = "";
            ViewBag.Id = "";
            ViewBag.Balance = "";
            ViewBag.DocNo = "";
            string tel = "";
            if (id != null)
            {
                var getresult = ManageRefill.GetApproveInfo(id,type.ToUpper());
                if(getresult.ResultCode == "200")
                {
                    tel = getresult.Msisdn;
                }

                //image patch
                var imageUrl = ManageRefill.GetImageUrl(id);
                if(imageUrl != null)
                {

                    ViewBag.Image = imageUrl.ImagePath;
                    ViewBag.DocNo = imageUrl.DocumentNo;
                }

                string _name = ManageRefill.GetUsername(tel.Trim());
                ViewBag.ApproveName = _name.ToString().Trim();
                string Msisdn = "";
                string Custname = "";
                string MemberID = "";
                double Amount = 0;
                double OldAmount = 0;
                double TotalAmount = 0;
                ConnectionString.OpenDB(conn);
                OracleDataReader dr;
                string sql = @"select userid,RECORDDATE,amount,OLDAMOUNT,msisdn,name,MEMBERID from tbl_process_refill where id=" + id.Trim() + "";
                cmd = new OracleCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Amount = Convert.ToDouble(dr[2]);
                    OldAmount = Convert.ToDouble(dr[3]);
                    Msisdn = dr[4].ToString().Trim();
                    Custname = dr[5].ToString().Trim();
                    MemberID = dr[6].ToString().Trim();
                    TotalAmount = Amount + OldAmount;
                    ViewBag.Msisdn = Msisdn;
                    ViewBag.Result = OldAmount.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat);
                    ViewBag.AmountTotal = TotalAmount.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat);
                    ViewBag.Name = Custname;
                    ViewBag.MsisdnApprove = tel.Trim();
                    ViewBag.Amount = Amount.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat);
                    ViewBag.MemberID = MemberID.Trim();
                    ViewBag.Type = type.Trim();
                    ViewBag.Id = id.Trim();
                    ViewBag.OldAmount = OldAmount;
                    ViewBag.TotalAmount = TotalAmount;
                    ViewBag.Balance = Convert.ToDouble(dr[2]);
                    ViewBag.OldAMT = Convert.ToDouble(dr[3]);
                    ViewBag.NewAMT = TotalAmount;
                    //Checking already refill

                    if (ManageRefill.CheckStatusCancel(id, tel.Trim(), type.Trim())){

                        ViewBag.ResultCode = "500";
                        ViewBag.ResultDesc = "This transaction alrealy rejected";
                        ViewBag.Result = "ລາຍການຕື່ມເງີນນີ້ທ່ານໄດ້ທຳການ reject ເປັນທີ່ຮຽບຮ້ອຍແລ້ວ";
                    }
                    else
                    {
                        if (ManageRefill.CheckingStatus(id, type.Trim(), tel.Trim()))
                        {
                            ViewBag.ResultCode = "500";
                            ViewBag.ResultDesc = "This transaction alrealy approved";
                            ViewBag.Result = "ລາຍການຕື່ມເງີນນີ້ທ່ານໄດ້ອະນຸມັດໄປແລ້ວ";
                        }
                        else
                        {
                            string _otp = RandomOTP();
                            if (InsertOTP(_otp, tel.Trim()))
                            {
                                smservice _sms = new smservice();

                                var _result = _sms.SubmitSMS(tel.Trim(), "M-Topup OTP:" + _otp, "MTOPUP");
                                if (_result)
                                {
                                    //Check Approve
                                    ViewBag.ResultCode = "200";
                                    ViewBag.ResultDesc = "Operation Succeeded";
                                }
                                else
                                {
                                    ViewBag.ResultCode = "102";
                                    ViewBag.ResultDesc = "Could not send SMS";
                                    ViewBag.Result = "ບໍ່ສາມາດສົ່ງຂໍ້ຄວາມຫາທ່ານໄດ້.";
                                }
                            }
                        }
                    }
                    

                }
                else
                {
                    ViewBag.ResultCode = "101";
                    ViewBag.ResultDesc = "Do not have this transaction in system";
                    ViewBag.Result = "ລາຍການຕື່ມເງີນນີ້ບໍ່ໄດ້ຢູ່ໃນລະບົບ";
                }
                //}
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(ModelInput model)
        {
            ViewBag.ResultCode = "104";
            ViewBag.ResultDesc = "ກະລຸນາລອງໃຫມ່ພາຍຫລັງ";
            string _name = ManageRefill.GetUsername(model.MsisdnApprove.Trim());
            double _amount = 0;

            ViewBag.Msisdn = model.Msisdn;
            ViewBag.Name = model.Name;
            ViewBag.ApproveName = model.nameApprove;
            ViewBag.MsisdnApprove = model.MsisdnApprove.Trim();
            ViewBag.Amount = model.Balance.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat);
            ViewBag.OldAmount = model.OldAmount.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat);
            ViewBag.AmountTotal = model.TotalAmount.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat);
            //Check OTP
            OracleDataReader dr;
            ConnectionString.OpenDB(conn);
            string sql = @"select * from TBL_TOPUP_OTP where msisdn='" + model.MsisdnApprove.Trim() + "' and OTP ='" + model.txtotp.Trim() + "' and status='1' ";
            cmd = new OracleCommand(sql, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                DateTime dateExpire = (DateTime)dr[2];
                DateTime _dateExpire = dateExpire.AddMinutes(3);
                DateTime dateNow = DateTime.Now;

                if(_dateExpire > dateNow)
                {
                    if (UpdateOTP(model.txtotp.Trim(), model.MsisdnApprove.Trim()))
                    {
                        ManageRefill.UpdateRefillLog(Convert.ToDouble(model.Id), model.MemberID, model.Type, _name, _name);
                        ViewBag.ResultCode = "1000";
                        ViewBag.ResultDesc = "Operation Succeeded";
                        if (model.Type == "IT")
                        {
                            ViewBag.Result = "ການອະນຸມັດຂອງທ່ານສຳລັດ ກະລຸນາລໍຖ້າຝ່າຍການເງິນອະນຸມັດ";
                        }
                        else
                        {
                            ViewBag.Result = "ການອະນຸມັດຂອງທ່ານສຳລັດ ກະລຸນາລໍຖ້າຝ່າຍໄອທີອະນຸມັດ";
                        }


                        //Checking all
                        if (CheckRefillProcess(model.Id))
                        {
                            var getOldNew = CheckBankingUser(model.Msisdn);
                            if (!getOldNew)
                            {
                                _amount = 0;
                                var resultRefill = ManageRefill.InsertRechargeNew(model.Msisdn, model.Balance);
                                if (!resultRefill)
                                {
                                    ViewBag.ResultCode = "106";
                                    ViewBag.ResultDesc = "Couldn't Insert Refill";
                                    ViewBag.Result = "ບໍ່ສາມາດຕື່ມເງິນໄດ້!";
                                }
                                double _memberID = Convert.ToDouble(model.MemberID);
                                double _logID = Convert.ToDouble(ManageRefill.InsertRechargeLog(_memberID, model.Msisdn.Trim(), _amount));
                                if (_logID > 0)
                                {

                                    ManageRefill.UpdateRechargeLog(1, _logID);
                                    ViewBag.ResultCode = "2000";
                                    ViewBag.ResultDesc = "Operation Succeeded";
                                    ViewBag.Result = "ການຕື່ມເງິນໄດ້ຮັບການອະນຸມັດຈາກທັງສອງຝ່າຍເປັນທີ່ຮຽບຮ້ອຍ";
                                }
                                else
                                {
                                    ViewBag.ResultCode = "105";
                                    ViewBag.ResultDesc = "ບໍ່ສາມາດຕື່ມເງິນໄດ້";
                                }
                            }
                            else
                            {
                                _amount = Convert.ToDouble(model.Balance);
                                double _memberID = Convert.ToDouble(model.MemberID);
                                double _logID = Convert.ToDouble(ManageRefill.InsertRechargeLog(_memberID, model.Msisdn.Trim(), _amount));
                                if (_logID > 0)
                                {
                                    if (ManageRefill.InsertRecharge(_memberID, model.Msisdn.Trim(), _amount))
                                    {
                                        ManageRefill.UpdateRechargeLog(1, _logID);
                                        ViewBag.ResultCode = "2000";
                                        ViewBag.ResultDesc = "Operation Succeeded";
                                        ViewBag.Result = "ການຕື່ມເງິນໄດ້ຮັບການອະນຸມັດຈາກທັງສອງຝ່າຍເປັນທີ່ຮຽບຮ້ອຍ";
                                    }
                                }
                                else
                                {
                                    ViewBag.ResultCode = "105";
                                    ViewBag.ResultDesc = "ບໍ່ສາມາດຕື່ມເງິນໄດ້";
                                }
                            }
                            ///Insert log
                            ManageRefill.InsertRechargeDetail(model);

                        }
                    }
                    else
                    {
                        ViewBag.ResultCode = "106";
                        ViewBag.ResultDesc = "ລະຫັດ OTP ຖືກນຳໃຊ້ແລ້ວ";
                        ViewBag.Result = "ລະຫັດ OTP ຖືກນຳໃຊ້ແລ້ວ";
                    }
                    //_amount = Convert.ToDouble(model.Balance);
                }
                else
                {
                    ViewBag.ResultCode = "107";
                    ViewBag.ResultDesc = "ລະຫັດ OTP ຫມົດເວລາແລ້ວ";
                    ViewBag.Result = "ລະຫັດ OTP ຫມົດເວລາແລ້ວ";
                }
            }
            else
            {
                ViewBag.ResultCode = "105";
                ViewBag.ResultDesc = "ລະຫັດ OTP ຂອງທ່ານບໍ່ຖືກຕ້ອງ";
            }
            return View("index");
        }
        public ActionResult Close()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(string comment, ModelInput model)
        {
            //Insert log
            ViewBag.ResultCode = "300";


            ViewBag.ResultDesc = "ກະລຸນາລອງໃຫມ່ພາຍຫລັງ";

            bool getCheckLog = CheckCommentLog(model);
            if (getCheckLog)
            {
                bool getResult = InsertLog(comment, model);
                if (!getResult)
                {
                    ViewBag.ResultDesc = "ບັນທຶກບໍ່ສຳເລັດ ກະລຸນາລອງໃຫມ່ພາຍຫລັງ";
                }
                else
                {
                    ViewBag.ResultCode = "200";
                    ViewBag.ResultDesc = "ທ່ານໄດ້ທຳການບັນທຶກເຫດຜົນທີ່ບໍ່ອະນຸມັດສຳເລັດ";
                    ViewBag.Result = "ບໍລິສັດ "+model.Name+" ຈຳນວນເງິນ "+model.Balance.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) + "";
                }
            }
            else
            {
                
                ViewBag.ResultDesc = "ທ່ານໄດ້ລະບຸສາເຫດໄປກ່ອນໜ້ານີ້ແລ້ວ";
                ViewBag.Result = "ບໍລິສັດ " + model.Name + " ຈຳນວນເງິນ " + model.Balance.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) + "";
            }
            


            return View();
        }


        private static string RandomOTP()
        {
            string result = "None";
            Random _random = new Random();
            result = Convert.ToString(_random.Next(1, 999999));
            return result;
        }
        private static bool InsertOTP(string OTP, string Msisdn)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            bool result = false;
            ConnectionString.OpenDB(conn);
            string sql = "insert into tbl_topup_otp (MSISDN,OTP,RECORDDATE,Status) values ('" + Msisdn.Trim() + "','" + OTP.Trim() + "',sysdate,1)";
            cmd = new OracleCommand(sql, conn);
            cmd.ExecuteNonQuery();
            result = true;
            return result;
        }

        private static bool UpdateOTP(string OTP, string Msisdn)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            bool result = false;
            ConnectionString.OpenDB(conn);
            string sql = "update tbl_topup_otp set status='0' where msisdn='"+Msisdn.Trim()+"' and otp = '"+OTP.Trim()+"'";
            cmd = new OracleCommand(sql, conn);
            cmd.ExecuteNonQuery();
            result = true;
            return result;
        }
        private static bool InsertLog(string comment, ModelInput model)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            bool result = false;
            try
            {
                ConnectionString.OpenDB(conn);
                string sql = "insert into tbl_cancel_log (id,msisdn,name,msisdn_approve,name_approve,comment_approve,amount_refill,Status,type) values (" + model.Id + ",'" + model.Msisdn + "','" + model.Name + "','" + model.MsisdnApprove + "','" + model.nameApprove + "','" + comment.Trim() + "'," + model.amount + ",1,'" + model.Type + "')";
                cmd = new OracleCommand(sql, conn);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }
            
            return result;
        }
        public static bool CheckCommentLog(ModelInput model)
        {
            bool result = false;
            try
            {
                OracleConnection conn = new OracleConnection();
                OracleCommand cmd = new OracleCommand();
                OracleDataReader dr;
                ConnectionString.OpenDB(conn);
                string sql = "select count(*) as countRow from tbl_cancel_log where id=" + model.Id +" and type = '"+model.Type+"' and status =1";
                cmd = new OracleCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    double value = Convert.ToDouble(dr[0]);
                    if (value == 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {

            }
            return result;
        }
        private static bool CheckRefillProcess(string ID)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr;
            bool result = false;
            int _count = 0;
            ConnectionString.OpenDB(conn);
            string sql = "select count(*) as rec from TBL_PROCESS_REFILL where VASSTATUS=1 and ITSTATUS=1 and FINANCESTATUS=1 and id=" + ID + "";
            cmd = new OracleCommand(sql, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                _count = Convert.ToInt32(dr[0]);
                if (_count > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        private static bool CheckBankingUser (string msisdn)
        {
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr;
            bool result = false;
            int _count = 0;
            ConnectionString.OpenDBBanking(conn);
            string sql = "select count(*) from tbl_topup where record_date  >= sysdate -30 and center_number = '"+msisdn.Trim()+"'";
            cmd = new OracleCommand(sql, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                _count = Convert.ToInt32(dr[0]);
                if (_count > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        //public async System.Threading.Tasks.Task<ActionResult> RenderFileAsync(string id)
        //{
        //    //using (var client = new HttpClient())
        //    //{
        //    //    // Replace path with local domain
        //    //    string path = "http://10.30.6.37:3535/files/Manual-01-20210121102852.jpg";

        //    //    var res = await client.GetAsync(path);

        //    //    var response = Request.CreateResponse(HttpStatusCode.OK);
        //    //    response.Content = new StreamContent(await res.Content.ReadAsStreamAsync());
        //    //    response.Content.Headers.ContentType = res.Content.Headers.ContentType;

        //    //    return response;
        //    //}

        //    try
        //    {
        //        string uri = "http://10.30.6.37:3535/files/Manual-01-20210121102852.jpg";
        //        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
        //        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

        //        StreamReader sr = new StreamReader(resp.GetResponseStream());

        //        sr.Close();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public async Task<ActionResult> RenderImage(string id)
        {
            // var path = $"http://172.28.12.35:2425/Files/{id}";
            var path = id;
            var memory = new MemoryStream();

            var req = System.Net.WebRequest.Create(path);
            using (Stream stream = req.GetResponse().GetResponseStream())
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
               {".txt", "text/plain"},
                {".inc", "text/plain"},
                {".dat", "application/octet-stream"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".exe", "application/octet-stream"},
                {".zip", "application/zip"},
            };
        }
        public class ModelInput
        {
            public string Id { get; set; }
            public string MemberID { get; set; }
            public string Type { get; set; }
            public string Msisdn { get; set; }
            public string MsisdnApprove { get; set; }
            public string Name { get; set; }
            public double amount { get; set; }
            public string Result { get; set; }
            public string txtotp { get; set; }
            public string nameApprove { get; set; }
            public double OldAmount { get; set; }
            public double TotalAmount { get; set; }
            public double Balance { get; set; }
            public double OldAMT { get; set; }
            public double NewAMT { get; set; }
        }
    }
}