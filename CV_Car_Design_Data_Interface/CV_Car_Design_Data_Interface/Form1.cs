using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Data.SqlClient;
using System.Xml;

namespace CV_Car_Design_Data_Interface
{
    public partial class Form1 : Form
    {
        //set up logger for error and status logging
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //log application start
            logger.Info("Start");
        }

        private void BtnFileBrowse_Click(object sender, EventArgs e)
        {
            //browse for the file to import to the car database
            OpenFileDialog dlgCSV = new OpenFileDialog();
            dlgCSV.DefaultExt = ".csv";
            if (dlgCSV.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = dlgCSV.FileName;
            }
        }

        private void FileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void BtnLoadCSV_Click(object sender, EventArgs e)
        {
            //initialize the row counter
            int iCount = 0;

            //clear the row count display on the UI
            lblRowsLoaded.Text = "";
            try
            {
                //init the stream reader and the csv interpreter
                StreamReader reader = new StreamReader(this.txtFilePath.Text);
              
                var csv = new CsvReader(reader);
           

                //the default cn format has spaces.  Class members have _ instead
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.Replace(" ", "_");
                csv.Configuration.HasHeaderRecord = true;

                //configuration to handle bad rows
                csv.Configuration.BadDataFound = context =>
                {
                    logger.Info(context.RawRow);
                };

                csv.Configuration.MissingFieldFound = null;

                csv.Configuration.ReadingExceptionOccurred  = null;


                try
                {
                    //get the records from the CSV file
                    List<CN_CSV> records = new List<CN_CSV>();
                    while (csv.Read())
                    {
                        try
                        {
                            var myType = csv.GetRecord<CN_CSV>();
                            records.Add(myType);
                            logger.Info(myType.Number);
                            logger.Info(records.Count);
                        }
                        catch (Exception exParse)
                        {
                            logger.Error(exParse);
                        }
                    }

                    //sql setup
                    string szSQLConnectionInfo;
                    
                    SqlConnection sql_connection;

                    SqlCommand sql_command;
                    szSQLConnectionInfo = GetSQLConnectionInfo();
                    sql_connection = new SqlConnection(szSQLConnectionInfo);
                    sql_connection.Open();

                    //iterate the csv records
                    foreach (CN_CSV item in records)
                    {
                        try
                        {
                            //sql command.  Using an SP which removes the oldest copy of this car re
                            sql_command = new SqlCommand("add_update_car", sql_connection);
                            sql_command.CommandType = CommandType.StoredProcedure;

                            //if any numbers are null, set to 0
                            if(item.Door_Width is null)
                            {
                                item.Door_Width = 0;
                            }
                            if (item.Door_Height is null)
                            {
                                item.Door_Height = 0;
                            }
                            if (item.Inside_Length is null)
                            {
                                item.Inside_Length = 0;
                            }
                            if (item.Inside_Width is null)
                            {
                                item.Inside_Width = 0;
                            }
                            if (item.Inside_Height is null)
                            {
                                item.Inside_Height = 0;
                            }

                            //build the SQL command with the class members
                            sql_command.Parameters.Add("@Init", SqlDbType.NVarChar).Value = item.Initials;
                            sql_command.Parameters.Add("@CarNum", SqlDbType.Int).Value = item.Number;
                            sql_command.Parameters.Add("@Owner", SqlDbType.NVarChar).Value = item.Owner;
                            sql_command.Parameters.Add("@AAR_Type", SqlDbType.NVarChar).Value = item.AAR_Type;
                            sql_command.Parameters.Add("@AAR_Type_Description", SqlDbType.NVarChar).Value = item.AAR_Type_Description;
                            sql_command.Parameters.Add("@CN_Type", SqlDbType.NVarChar).Value = item.CN_Type;
                            sql_command.Parameters.Add("@CN_Type_Description", SqlDbType.NVarChar).Value = item.CN_Type_Description;
                            sql_command.Parameters.Add("@Tare_Weight", SqlDbType.Int).Value = item.Tare_Weight;
                            sql_command.Parameters.Add("@Load_Limit", SqlDbType.Int).Value = item.Load_Limit;
                            sql_command.Parameters.Add("@Outside_Length", SqlDbType.Float).Value = item.Outside_Length;
                            sql_command.Parameters.Add("@Outside_Width", SqlDbType.Float).Value = item.Outside_Width;
                            sql_command.Parameters.Add("@Outside_Height", SqlDbType.Float).Value = item.Outside_Height;
                            sql_command.Parameters.Add("@Inside_Length", SqlDbType.Float).Value = item.Inside_Length;
                            sql_command.Parameters.Add("@Inside_Width", SqlDbType.Float).Value = item.Inside_Width;
                            sql_command.Parameters.Add("@Inside_Height", SqlDbType.Float).Value = item.Inside_Height;
                            sql_command.Parameters.Add("@Capacity", SqlDbType.SmallInt).Value = item.Capacity;
                            sql_command.Parameters.Add("@Capacity_Unit", SqlDbType.NVarChar).Value = item.Capacity_Unit;
                            sql_command.Parameters.Add("@Floor_Type", SqlDbType.NVarChar).Value = item.Floor_Type;
                            sql_command.Parameters.Add("@Floor_Type_Description", SqlDbType.NVarChar).Value = item.Floor_Type_Description;
                            sql_command.Parameters.Add("@Door_Type", SqlDbType.NVarChar).Value = item.Door_Type;
                            sql_command.Parameters.Add("@Door_Type_Description", SqlDbType.NVarChar).Value = item.Door_Type_Description;
                            sql_command.Parameters.Add("@Door_Width", SqlDbType.Float).Value = item.Door_Width;
                            sql_command.Parameters.Add("@Door_Height", SqlDbType.Float).Value = item.Door_Height;
                            sql_command.Parameters.Add("@Clearance_Restriction", SqlDbType.NVarChar).Value = item.Clearance_Restriction;


                            //execute the query.  Add the number of rows to the rolling count
                            sql_command.ExecuteNonQuery();
                            iCount = iCount + 1;






                        }
                        catch (Exception exInstance)
                        {
                            logger.Info("Error on {0}{1} {2}", item.Initials, item.Number, exInstance.Message);
                        }

                    }

                    //display the number of rows loaded
                    string szLoaded = iCount.ToString() + " Rows Loaded";
                    lblRowsLoaded.Text = szLoaded;

                    sql_connection.Close();

                }
                
                catch (Exception exParse)
                {
                    logger.Error("Error parsing file {0}", exParse.Message);
                }
                csv.Dispose();
            }
            catch (Exception exFile)
            {
                logger.Error("Error opening file {0}", exFile.Message);
            }

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //sql server setup
                SqlConnection sql_connection;
                DataTable dt = new DataTable();
                SqlCommand sql_command;
                string szSQLConnectionInfo;

                //build the query
                string szTagQuery = "select distinct CONCAT(Initials,CAST(Number as varchar(50))) from Car_Data";
                szSQLConnectionInfo = GetSQLConnectionInfo();
                sql_connection = new SqlConnection(szSQLConnectionInfo);
                sql_command = new SqlCommand(szTagQuery, sql_connection);
                sql_command.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
                int iCount = dt.Rows.Count;
                DateTime dtNow = DateTime.Now;
                StreamWriter writer = new StreamWriter(this.txtFolderPath.Text + "\\carIDs_" + dtNow.ToString("yyyyMMdd") + ".csv",false);
                var csv = new CsvWriter(writer);

                // Write columns
                foreach (DataColumn column in dt.Columns)       
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                // Write row values
                foreach (DataRow row in dt.Rows)
                {
                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
                csv.Dispose();
            }
            catch (Exception exExport)
            {

                logger.Error("Error opening file {0}", exExport.Message);
            }


        }

        private void BtnBrowseExportFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private string GetSQLConnectionInfo()
        {
            //init default connection
            string szConnectionInfo = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CN_Train_Data;Trusted_Connection=true";
            

            //load the xml document
            XmlDocument doc = new XmlDocument();
            try
            {
                //loading the xml file from the EXE directory
                doc.Load(@"TrainInfo.xml");
                XmlNodeList n = doc.GetElementsByTagName("SqlConnectionInfo");
                if (n != null)
                {
                    szConnectionInfo = n[0].InnerText;
                }
            }
            catch (Exception Ex)
            {
                
                logger.Error("Error Loading Config settings - {0}", Ex.Message);
            }
            return szConnectionInfo;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnQueryCar_Click(object sender, EventArgs e)
        {
            try
            {

            this.lblTare.Text = "";
            this.lblLoad.Text = "";
            //sql server setup
            SqlConnection sql_connection;
            DataTable dt = new DataTable();
            SqlCommand sql_command;
            string szSQLConnectionInfo;

            szSQLConnectionInfo = GetSQLConnectionInfo();
            sql_connection = new SqlConnection(szSQLConnectionInfo);
            sql_connection.Open();
            sql_command = new SqlCommand("query_car_weights", sql_connection);
            sql_command.CommandType = CommandType.StoredProcedure;

            SqlParameter pvCar = new SqlParameter();
            pvCar.ParameterName = "@CarID";
            pvCar.DbType = DbType.String;
            pvCar.Value = this.txtCarID.Text;
            sql_command.Parameters.Add(pvCar);


            SqlParameter pvTare = new SqlParameter();
            pvTare.ParameterName = "@Tare_Weight";
            pvTare.DbType = DbType.Int32;
            pvTare.Direction = ParameterDirection.Output;
            sql_command.Parameters.Add(pvTare);

            SqlParameter pvLoadLimit = new SqlParameter();
            pvLoadLimit.ParameterName = "@Load_Limit";
            pvLoadLimit.DbType = DbType.Int32;
            pvLoadLimit.Direction = ParameterDirection.Output;
            sql_command.Parameters.Add(pvLoadLimit);

            int sqlRows = sql_command.ExecuteNonQuery();

            this.lblTare.Text = "Tare - " + sql_command.Parameters["@Tare_Weight"].Value.ToString();
            this.lblLoad.Text = "Load Limit - " + sql_command.Parameters["@Load_Limit"].Value.ToString();
            sql_connection.Close();
            }
            catch (Exception Ex)
            {

                logger.Error("Error Querying Car information - {0}", Ex.Message);
            }

        }
    }
}
