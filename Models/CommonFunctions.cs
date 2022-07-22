using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace WebApplication1.Models
{
    public class CommonFunctions
    {
        #region Constants
        static string PasswordHash = "P@@Sw0rd";
        static string SaltKey = "S@LT&KEY";
        static string VIKey = "@1B2c3D4e5F6g7H8";
#endregion
        public string Decrypt(string cipherText)
        {

            //SaveLog("Decrypt", cipherText, EventLogEntryType.Information);
            cipherText = cipherText.Replace(" ", "%20");
            string EncryptionKey = getPrivateKey(); //"MAKقصV2ضSPBعهخحNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText); //Encoding.Unicode.GetBytes(cipherText); 
            using (Rijndael encryptor = Rijndael.Create())
            {
                //encryptor.Padding = PaddingMode.PKCS7;

                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        //cs.Close();
                        cs.FlushFinalBlock();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private String getPrivateKey()
        {
            String privateKey = "";
            //try
            {
                String connectStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
                using (SqlConnection connect = new SqlConnection(connectStr))
                {
                    using (SqlCommand command = new SqlCommand("getPrivateKey", connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@privateKey", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;

                        connect.Open();

                        command.ExecuteNonQuery();


                        if (command.Parameters["@privateKey"].Value != null)
                        {
                            privateKey = command.Parameters["@privateKey"].Value.ToString();
                        }

                    }
                }
            }
            //catch (Exception ex)
            //{
            //    SaveLog("privateKeyGCS", " Error => " + ex.ToString(), EventLogEntryType.Error);
            //}

            return privateKey;
        }
        public static string CsUploadEncrypt(string plainText)
        {
            //if (plainText == null)
            //{
            //    return "";
            //}
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();

                    cryptoStream.Close();
                }
                memoryStream.Close();
            }


            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string CsUploadDecrypt(string encryptedText)
        {
            string x = "";
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                x = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            catch (Exception ex)
            {
                String error = ex.ToString();
                // WriteToLogFile(ex);
                //  WriteToLogFile(e);
            }
            return x;
        }

        public static void WriteToLogFile(string inputvalue, string sessionDetails)
        {

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(sessionDetails + "Inpu" +
                    "tValue>" + inputvalue, EventLogEntryType.Information, 101, 1);
            }

        }

        public static string GetLocalIPAddress()
        {
            string userip = "";
         // string userName = "";
            if (HttpContext.Current.Request.UserHostAddress != null)
            {
                 userip = HttpContext.Current.Request.UserHostAddress;
                //   userName = System.Net.Dns.GetHostName();
              //  userName = Dns.GetHostName();
              //  userName = Environment.UserName;
            }
            else
            {
                userip = "UnabletotraceTheIpforLoggedInUser";
            }
          //     return userip+" "+userName;
    return userip;
        }


//will work later 
        public static DataSet GetBrokerAffairsDocsCheck(BrokerUpdateModel frdata)
        {
            String connectionStr = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            DataSet Ds = new DataSet();
            try
            {
                using (var sCon = new SqlConnection(connectionStr))
                {
                    using (var sCmd = new SqlCommand("etrade.Sp_checkDocsUploaddedForBrsEservices", sCon))
                    {
                        sCmd.CommandType = CommandType.StoredProcedure;

                        if (String.IsNullOrEmpty(frdata.Eservicerequestid))
                            sCmd.Parameters.Add("@EServiceRequestid", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@EServiceRequestid", SqlDbType.VarChar).Value = frdata.Eservicerequestid;

                        if (String.IsNullOrEmpty(frdata.Referenceprofile))
                            sCmd.Parameters.Add("@EServiceReferenceProfile", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@EServiceReferenceProfile", SqlDbType.VarChar).Value = frdata.Referenceprofile;



                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(Ds);
                        // frdata.NewFileName = sCmd.Parameters["@NewName"].Value.ToString();
                        for (int i = 0; i < Ds.Tables.Count; i++)
                        {
                            if (Ds.Tables[i].Columns.Contains("TableName"))
                            {
                                if (Ds.Tables[i].Rows.Count > 0)
                                    Ds.Tables[i].TableName = Ds.Tables[i].Rows[0]["TableName"].ToString();
                            }
                            else
                            {
                                Ds.Tables.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return Ds;
            return Ds;
        }
     
        public static string LogUserActivity(string ActivityPerformed, string SignInSignOut, string LoginTime, string logoutdatetime, string serviceid, string OtherAdditionalInfo)
        {
            try
            {
                DataSet ds = new DataSet();
                LogUserActivity ObjLogUserActivity = new LogUserActivity();
                ObjLogUserActivity.LoginUserid = HttpContext.Current.Session["UserId"] != null ? HttpContext.Current.Session["UserId"].ToString() : "";
                ObjLogUserActivity.LoginTime = LoginTime;
                ObjLogUserActivity.sessionId = HttpContext.Current.Session.SessionID;
                ObjLogUserActivity.IPAddress = GetLocalIPAddress();
                ObjLogUserActivity.McUserOrgId = HttpContext.Current.Session["UserOrgID"] != null ? HttpContext.Current.Session["UserOrgID"].ToString() : "" ;
                ObjLogUserActivity.McUserName = HttpContext.Current.Session["MicroClearUsername"] != null ? HttpContext.Current.Session["MicroClearUsername"].ToString() : "" ;
                ObjLogUserActivity.legalentity = HttpContext.Current.Session["LegalEntity"] != null ? HttpContext.Current.Session["LegalEntity"].ToString() : "";
                ObjLogUserActivity.ClearingAgentservice = Convert.ToBoolean(HttpContext.Current.Session["ClearingAgentServices"]);
                ObjLogUserActivity.OrganizationService = Convert.ToBoolean(HttpContext.Current.Session["OrganizationServices"]); ;
                ObjLogUserActivity.ActivityPerformed = ActivityPerformed;
                ObjLogUserActivity.LogOutTime = logoutdatetime;
                ObjLogUserActivity.Serviceid = serviceid;
                ObjLogUserActivity.OtherAdditionalInfo = OtherAdditionalInfo;
                Dataclass ObjDataclass = new Dataclass();
                ds = ObjDataclass.UserActivitypost(ObjLogUserActivity);
            }
            catch (Exception ex)
            {
                WriteToLogFile(ex.StackTrace + ex.Message, HttpContext.Current.Session["UserId"] !=null? HttpContext.Current.Session["UserId"].ToString():"");
            }
            return serviceid;
        }
    }




}