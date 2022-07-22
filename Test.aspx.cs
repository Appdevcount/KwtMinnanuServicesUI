using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace WebApplication1
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionStr = "";
            string AtmId = "";

            image1.ImageUrl = "https://bprassets.s3.amazonaws.com/blogfiles/assets/media/plant_tree.jpg";


            connectionStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataSet Ds = new DataSet();
            try
            {
                using (var connectDB = new SqlConnection(connectionStr))
                using (var commandDB = new SqlCommand("sp_getLink", connectDB))
                using (var dataAdapter = new SqlDataAdapter(commandDB))
                {
                    commandDB.CommandType = CommandType.StoredProcedure;
                    commandDB.Parameters.Add("@AtmId", SqlDbType.NVarChar).Value = AtmId;
                    dataAdapter.Fill(Ds);
                    for (int i = 0; i < Ds.Tables.Count; i++)
                    {
                        if (Ds != null && Ds.Tables[0].Rows.Count > 0)
                        {

                            image1.ImageUrl = "https://bprassets.s3.amazonaws.com/blogfiles/assets/media/plant_tree.jpg";
                            image2.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfLZWxzsOkpVrj4BhT9maj8JApOmrAZ-3-5xsTZtIkWopf3FHj";

                            image3.ImageUrl = "";
                            image4.ImageUrl = "";

                        }
                        }
                        connectDB.Close();
                }
            }

            catch(Exception ex)
            { 
            
            }
        }
    }
}