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

namespace CV_Car_Design_Data_Interface
{
    public partial class Form1 : Form
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logger.Info("Start");
        }

        private void BtnFileBrowse_Click(object sender, EventArgs e)
        {
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

            try
            {
                StreamReader reader = new StreamReader(this.txtFilePath.Text);
                var csv = new CsvReader(reader);
           

                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.Replace(" ", "_");
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.BadDataFound = context =>
                {
                    logger.Info(context.RawRow);
                };


                try
                {

                    IEnumerable < CN_CSV > records = csv.GetRecords<CN_CSV>();

                    string szSQLConnectionInfo;
                    
                    SqlConnection sql_connection;

                    SqlCommand sql_command;
                    szSQLConnectionInfo = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CN_Train_Data;Trusted_Connection=true";
                    sql_connection = new SqlConnection(szSQLConnectionInfo);
                    sql_connection.Open();

                    foreach (CN_CSV item in records)
                    {
                        try
                        {

                        
                        
                            sql_command = new SqlCommand("add_update_car", sql_connection);
                            sql_command.CommandType = CommandType.StoredProcedure;
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

                        sql_command.ExecuteNonQuery();
                         


                        
                        
                        }
                        catch (Exception exInstance)
                        {
                            logger.Info("Error on {0}{1} {2}", item.Initials, item.Number, exInstance.Message);
                        }

                    }
                    sql_connection.Close();
                }
                catch (Exception exParse)
                {
                    logger.Error("Error parsing file {0}", exParse.Message);
                }
            }
            catch (Exception exFile)
            {
                logger.Error("Error opening file {0}", exFile.Message);
            }

        }
    }
}
