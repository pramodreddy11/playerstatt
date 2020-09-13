using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services.Protocols;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Web.Script.Services;

namespace Theplayerstats.com
{
    public partial class DataEntry : System.Web.UI.Page
    { 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
           
            if(!IsPostBack)
            {
                BindDropDown();
            }


        }
        protected void Add_stadium(object sender, EventArgs e)
        {
            string connetionString = null;

            var value = DropDownList1.SelectedValue;
            SqlConnection cnn;
             connetionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query = "insert into StadiumNames(StadiumName,CountryID) values ('" + Convert.ToString(StadiumName.Text) + "',"+Convert.ToInt32(DropDownList1.SelectedValue)+")";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            sqlCommand.ExecuteNonQuery();
              b 
            cnn.Close();
        }



        protected void Add_Player(object sender, EventArgs e)
        {

            string connetionString = null;

            var value = DropDownList2.SelectedValue;
            if (value !="")
            {
                SqlConnection cnn;
                connetionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                string query = "insert into PlayersDetails(PlayerName,CountryID) values ('" + Convert.ToString(TextBox1.Text) + "'," + Convert.ToInt32(DropDownList2.SelectedValue) + ")";
                var sqlCommand = new SqlCommand(query);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = cnn;
                sqlCommand.ExecuteNonQuery();

                cnn.Close();
            }
         


        }
        protected void Add_country(object sender, EventArgs e)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string query = "insert into CountryNames(CountryName) values ('"+Convert.ToString(CountryName.Text)+"')";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            sqlCommand.ExecuteNonQuery();
            
            cnn.Close();
            
        }
        protected void BindDropDown()
        {
            string str = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            SqlConnection con = new SqlConnection(str);
            string com = "Select * from CountryNames";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "CountryName";
            DropDownList1.DataValueField = "CountryID";
            DropDownList1.DataBind();
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "CountryName";
            DropDownList2.DataValueField = "CountryID";
            DropDownList2.DataBind();
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string GetDetails()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sqlCommand = new SqlCommand("Select * From CountryNames");
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(ds);
            string jsondata = string.Empty;
            jsondata = JsonConvert.SerializeObject(ds.Tables[0]);
            return jsondata;



        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string GetPlayerDetailss(string index)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sqlCommand = new SqlCommand("Select * From PlayersDetails where CountryID=" + Convert.ToInt32(index));
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(ds);
            string jsondata = string.Empty;
            jsondata = JsonConvert.SerializeObject(ds.Tables[0]);
            return jsondata;



        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string GetSDetails(string index)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sqlCommand = new SqlCommand("select * from StadiumNames  where CountryID="+ Convert.ToInt32(index));
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(ds);
            string jsondata = string.Empty;
            jsondata = JsonConvert.SerializeObject(ds.Tables[0]);
            return jsondata;


        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string DeleteCountry( string countryId)
        {
            int playerID = Convert.ToInt32(countryId);
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sqlCommand = new SqlCommand("Delete from CountryNames where CountryID= "+playerID);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            sqlCommand.ExecuteNonQuery();

            cnn.Close();
            return "Deleted Successfully";



        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static string DeleteStadium(string StadiumId)
        {
            int playerID = Convert.ToInt32(StadiumId);
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sqlCommand = new SqlCommand("Delete from StadiumNames where StadiumID= " + StadiumId);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            sqlCommand.ExecuteNonQuery();

            cnn.Close();
            return "Deleted Successfully";



        }
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string PlayersDelete(string PlayerName)
        {
            //int playerID = Convert.ToInt32(StadiumId);
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sqlCommand = new SqlCommand("Delete from PlayersDetails where PlayerName = '" + PlayerName+"'");
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            sqlCommand.ExecuteNonQuery();

            cnn.Close();
            return "Deleted Successfully";



        }
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SavePlayerDetails(string PlayerName,string PlayerID,string CountryID)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=DESKTOP-JKBF32A;Initial Catalog=playerstats;User ID=ManojLogin;Password=Pramod@11";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var sqlCommand = new SqlCommand("Update PlayersDetails set PlayerName = '" + PlayerName+ "' ,CountryID="+Convert.ToInt32(CountryID) + "where PlayerID = "+Convert.ToInt32(PlayerID));
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = cnn;
            sqlCommand.ExecuteNonQuery();

            cnn.Close();



            return "success";
        }
    }
}