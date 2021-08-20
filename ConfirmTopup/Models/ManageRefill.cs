using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using static ConfirmTopup.Controllers.HomeController;

namespace ConfirmTopup.Models
{
    public class ManageRefill
    {
        public static bool InsertRecharge(double MemberID, string Msisdn, double amount)
        {
            bool result = false;
            try
            {
                OracleConnection conn = new OracleConnection();
                OracleCommand cmd = new OracleCommand();
                ConnectionString.OpenDB(conn);
                cmd = new OracleCommand("STP_RECHARGE_2", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("P_MEMBER_ID", OracleDbType.Int64).Value = Convert.ToInt64(MemberID);
                cmd.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = Msisdn.Trim();
                cmd.Parameters.Add("P_RECHARGE_AMT", OracleDbType.Int64).Value = Convert.ToDouble(amount);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                string err = "ERR: " + ex.Message;
            }
            return result;
        }
        public static string InsertRechargeLog(double MemberID, string Msisdn, double Amount)
        {
            string result = "0";
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            double _amount = Convert.ToInt64(Amount);
            try
            {
                //webrefill@123
                ConnectionString.OpenDB(conn);
                cmd = new OracleCommand("STP_INSERT_RECHAGE_LOG", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("p_member_id", OracleDbType.Int64).Value = Convert.ToInt64(MemberID);
                cmd.Parameters.Add("p_telephone", OracleDbType.NVarchar2, 10).Value = Msisdn.ToString().Trim();
                cmd.Parameters.Add("p_recharge_amt", OracleDbType.Int64).Value = _amount;
                cmd.Parameters.Add("p_user_id", OracleDbType.NVarchar2, 10).Value = "WEB";
                OracleParameter P_ID = new OracleParameter("p_id", OracleDbType.Int64) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(P_ID);
                cmd.ExecuteNonQuery();
                result = P_ID.Value.ToString();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return result;
        }
        public static bool UpdateRechargeLog(int Status, double RechargeID)
        {
            bool result = false;
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            try
            {
                ConnectionString.OpenDB(conn);
                cmd = new OracleCommand("STP_UPDATE_RECHARGE_LOG", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("P_ID", OracleDbType.Int64).Value = Convert.ToDouble(RechargeID);
                cmd.Parameters.Add("P_STS", OracleDbType.Int64).Value = Convert.ToDouble(Status);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return result;
        }
        public static bool UpdateRefillLog(double ID, string userid, string type, string userIT, string userFN)
        {
            bool result = false;
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            try
            {
                ConnectionString.OpenDB(conn);
                cmd = new OracleCommand("STP_UPDATE_REFILL", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = Convert.ToDouble(ID);
                cmd.Parameters.Add("p_userid", OracleDbType.NVarchar2, 20).Value = userid.Trim();
                cmd.Parameters.Add("p_type", OracleDbType.NVarchar2, 30).Value = type.Trim();
                cmd.Parameters.Add("p_it_user", OracleDbType.NVarchar2, 50).Value = userIT.Trim();
                cmd.Parameters.Add("p_fn_user", OracleDbType.NVarchar2, 50).Value = userFN.Trim();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return result;
        }
        public static string GetUsername(string Telephone)
        {
            string result = "None";
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr;
            try
            {
                ConnectionString.OpenDB(conn);
                string sql = "select name from tbl_user_approve where msisdn='" + Telephone.Trim() + "'";
                cmd = new OracleCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    result = dr[0].ToString();
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        public static bool CheckingStatus(string ID, string Type, string tel)
        {
            bool result = false;
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr;
            int _cnt = 0;
            try
            {
                string sql = "";
                ConnectionString.OpenDB(conn);
                if (Type.ToUpper() == "IT")
                {
                    sql = "select count(*) as rec from tbl_process_refill where id = " + ID + " and MSISDNAPPROVEIT = '" + tel + "' and ITSTATUS = 1";
                }
                else
                {
                    sql = "select count(*) as rec from tbl_process_refill where id = " + ID + " and MSISDNAPPROVEFN = '" + tel + "' and FINANCESTATUS = 1";
                }
                cmd = new OracleCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _cnt = Convert.ToInt32(dr[0]);
                    if (_cnt > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public static bool CheckStatusCancel(string id,string tel,string type)
        {
            bool result = false;
            OracleConnection conn = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr;
            int _cnt = 0;
            try
            {
                string sql = "";
                ConnectionString.OpenDB(conn);
                sql = "select count (*)  from tbl_cancel_log where id='"+id+"' and msisdn_approve='"+tel+"' and type='"+type+"'";
                cmd = new OracleCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _cnt = Convert.ToInt32(dr[0]);
                    if (_cnt > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public static ResultFileLog GetImageUrl(string id)
        {
            ResultFileLog result = new ResultFileLog();
            OracleConnection conn = new OracleConnection();
            OracleDataAdapter da = new OracleDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                ConnectionString.OpenDB(conn);
                string sql = "select image_path,doc_no FROM tbl_file_log where recharge_id ='" + id.Trim() + "'";
                ds.Clear();
                da = new OracleDataAdapter(sql, conn);
                da.Fill(ds, "table");
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Columns.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            //your code here

                            result.ImagePath = dr["image_path"].ToString();
                            result.DocumentNo = dr["doc_no"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = "ERR: " + ex.Message;
            }
            finally
            {
                conn.Dispose();
                da.Dispose();
            }

            return result;
        }
        public static ResultUserApproveDetail GetApproveInfo(string id, string type)
        {
            ResultUserApproveDetail result = new ResultUserApproveDetail();
            OracleConnection conn = new OracleConnection();
            OracleDataAdapter da = new OracleDataAdapter();
            DataSet ds = new DataSet();
            result.ResultCode = "100";
            result.ResultDesc = "System Error";
            try
            {
                ConnectionString.OpenDB(conn);
                string sql = "SELECT * FROM tbl_member_refill where refill_id = " + id.Trim() + " and USER_TYPE ='" + type.Trim() + "'";
                ds.Clear();
                da = new OracleDataAdapter(sql, conn);
                da.Fill(ds, "table");
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Columns.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            //your code here

                            result.RefillID = dr["REFILL_ID"].ToString();
                            result.Msisdn = dr["USER_APPROVE"].ToString();
                            result.UserType = dr["USER_TYPE"].ToString();
                            result.ResultCode = "200";
                            result.ResultDesc = "Operation Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = "101";
                result.ResultDesc = "ERR: " + ex.Message;
            }
            finally
            {
                conn.Dispose();
                da.Dispose();
            }

            return result;
        }
        public static bool CheckRefillType(string Msisdn)
        {
            bool result = false;
            try
            {
                OracleConnection conn = new OracleConnection();
                OracleCommand cmd = new OracleCommand();
                OracleDataReader dr;
                ConnectionString.OpenDBNew(conn);
                string sql = "select mtopup.fn_member_credit('" + Msisdn.Trim() + "') from dual";
                //string sql = "select mtopup.FN_CHECKING_MEMBER('" + Msisdn.Trim() + "') from dual";
                cmd = new OracleCommand(sql, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    double value = Convert.ToDouble(dr[0]);
                    if(value > 0)
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
        public static bool InsertRechargeNew(string Msisdn,double Amount)
        {
            bool result = false;
            string trans = DateTime.Now.ToString("ddMMYYHHmmss") + Msisdn.Trim();
            try
            {
                OracleConnection conn = new OracleConnection();
                OracleCommand cmd = new OracleCommand();
                ConnectionString.OpenDBNew(conn);
                cmd = new OracleCommand("mtopup.STP_RECHARGE_TOPUP", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("p_trans_id", OracleDbType.NVarchar2, 35).Value = trans.Trim();
                cmd.Parameters.Add("p_telephone", OracleDbType.NVarchar2, 10).Value = Msisdn.Trim();
                cmd.Parameters.Add("p_credit_amt", OracleDbType.Int64).Value = Convert.ToDouble(Amount);
                cmd.Parameters.Add("p_data_amt", OracleDbType.Int64).Value = 0;
                cmd.Parameters.Add("p_user_id", OracleDbType.NVarchar2, 10).Value = "WebCall";
                cmd.Parameters.Add("p_recharge_type", OracleDbType.NVarchar2, 5).Value = "AMT";
                OracleParameter P_ID = new OracleParameter("p_recharge_id", OracleDbType.Int64) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(P_ID);
                cmd.ExecuteNonQuery();
                double _PID =Convert.ToDouble(P_ID.Value.ToString());
                if(_PID > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                string err = "ERR: " + ex.Message;
            }
            return result;
        }
        public static bool InsertRechargeDetail(ModelInput model)
        {
            bool result = false;
            try
            {
                OracleConnection conn = new OracleConnection();
                OracleCommand cmd = new OracleCommand();
                ConnectionString.OpenDB(conn);
                cmd = new OracleCommand("STP_INSERT_RECHARGE_DETAIL", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("P_MEMBER_ID", OracleDbType.Int64).Value = Convert.ToInt64(model.MemberID);
                cmd.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = model.Msisdn;
                cmd.Parameters.Add("P_AMT_BEFORE", OracleDbType.Int64).Value = Convert.ToInt64(model.OldAMT);
                cmd.Parameters.Add("P_AMT", OracleDbType.Int64).Value = Convert.ToInt64(model.Balance);
                cmd.Parameters.Add("P_AMT_AFTER", OracleDbType.Int64).Value = Convert.ToInt64(model.NewAMT);
                cmd.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 10).Value = "WebCall";
                cmd.Parameters.Add("P_MEMBER_NAME", OracleDbType.NVarchar2, 50).Value = model.Name;
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                string err = "ERR: " + ex.Message;
            }
            return result;
        }
        
    }

    public class ResultUserApproveDetail
    {
        public string RefillID { get; set; }
        public string Msisdn { get; set; }
        public string UserType { get; set; }
        public string ResultCode { get; set; }
        public string ResultDesc { get; set; }
    }
    public class ResultFileLog
    {
        public string ImagePath { get; set; }
        public string DocumentNo { get; set; }
    }
}