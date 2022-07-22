using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class DataAccess
    {
        private static String connectionStr = String.Empty;
        public DataAccess()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public DataSet CreateCustomsVisitAppointment(CustomsVisit R)
        {
            connectionStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataSet Ds = new DataSet();
            try
            {
               
                using (var sCon = new SqlConnection(connectionStr))
                {
                    using (var sCmd = new SqlCommand("etrade.CraeteSchedule", sCon))
                    {
                        sCmd.CommandType = CommandType.StoredProcedure;

                        if (String.IsNullOrEmpty(R.VisitorType))
                            sCmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = R.VisitorType;
                        if (String.IsNullOrEmpty(R.VisitorName))
                            sCmd.Parameters.Add("@OrgId", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@OrgId", SqlDbType.VarChar).Value = R.VisitorName;
                        if (String.IsNullOrEmpty(R.CivilId))
                            sCmd.Parameters.Add("@PortId", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@PortId", SqlDbType.VarChar).Value = R.CivilId;
                        if (String.IsNullOrEmpty(R.MobileNum))
                            sCmd.Parameters.Add("@DONumber", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@DONumber", SqlDbType.VarChar).Value = R.MobileNum;
                        if (String.IsNullOrEmpty(R.Email))
                            sCmd.Parameters.Add("@SecurityCode", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@SecurityCode", SqlDbType.VarChar).Value = R.Email;
                        if (String.IsNullOrEmpty(R.Department))
                            sCmd.Parameters.Add("@SelectedVehicleList", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@SelectedVehicleList", SqlDbType.VarChar).Value = R.Department;

                        string year = DateTime.ParseExact(R.AppointmentDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Year.ToString();
                        string month = DateTime.ParseExact(R.AppointmentDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Month.ToString();
                        string day = DateTime.ParseExact(R.AppointmentDate, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")).Day.ToString();

                        R.AppointmentDate = year + "-" + month + "-" + day;

                        if (String.IsNullOrEmpty(R.VisitPurpose))
                            sCmd.Parameters.Add("@InspectionDate", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@InspectionDate", SqlDbType.VarChar).Value = R.VisitPurpose;
                  

                        if (String.IsNullOrEmpty(R.AppointmentDate))
                            sCmd.Parameters.Add("@DeclarationType", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@DeclarationType", SqlDbType.VarChar).Value = R.AppointmentDate;
                        if (String.IsNullOrEmpty(R.AppointmentTimeSlot))
                            sCmd.Parameters.Add("@TempDeclarationId", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@TempDeclarationId", SqlDbType.VarChar).Value = R.AppointmentTimeSlot;

                        if (String.IsNullOrEmpty(R.RequestNote))
                            sCmd.Parameters.Add("@DeclarationId", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@DeclarationId", SqlDbType.VarChar).Value = R.RequestNote;
                        
                       

                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(Ds);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.LogUserActivity("CreateInspectionAppointment", "", "", "", "", ex.Message.ToString());
                throw ex;
            }
            return Ds;
        }
    }
}