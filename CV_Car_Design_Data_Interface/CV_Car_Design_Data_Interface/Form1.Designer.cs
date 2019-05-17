namespace CV_Car_Design_Data_Interface
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnFileBrowse = new System.Windows.Forms.Button();
            this.btnLoadCSV = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnBrowseExportFolder = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.lblRowsLoaded = new System.Windows.Forms.Label();
            this.lblTare = new System.Windows.Forms.Label();
            this.txtCarID = new System.Windows.Forms.TextBox();
            this.btnQueryCar = new System.Windows.Forms.Button();
            this.lblLoad = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(7, 14);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(134, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnFileBrowse
            // 
            this.btnFileBrowse.Location = new System.Drawing.Point(147, 12);
            this.btnFileBrowse.Name = "btnFileBrowse";
            this.btnFileBrowse.Size = new System.Drawing.Size(33, 23);
            this.btnFileBrowse.TabIndex = 2;
            this.btnFileBrowse.Text = "...";
            this.btnFileBrowse.UseVisualStyleBackColor = true;
            this.btnFileBrowse.Click += new System.EventHandler(this.BtnFileBrowse_Click);
            // 
            // btnLoadCSV
            // 
            this.btnLoadCSV.Location = new System.Drawing.Point(186, 12);
            this.btnLoadCSV.Name = "btnLoadCSV";
            this.btnLoadCSV.Size = new System.Drawing.Size(115, 23);
            this.btnLoadCSV.TabIndex = 3;
            this.btnLoadCSV.Text = "Load CSV File";
            this.btnLoadCSV.UseVisualStyleBackColor = true;
            this.btnLoadCSV.Click += new System.EventHandler(this.BtnLoadCSV_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(186, 55);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(115, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export CarIDs";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // btnBrowseExportFolder
            // 
            this.btnBrowseExportFolder.Location = new System.Drawing.Point(147, 55);
            this.btnBrowseExportFolder.Name = "btnBrowseExportFolder";
            this.btnBrowseExportFolder.Size = new System.Drawing.Size(33, 23);
            this.btnBrowseExportFolder.TabIndex = 5;
            this.btnBrowseExportFolder.Text = "...";
            this.btnBrowseExportFolder.UseVisualStyleBackColor = true;
            this.btnBrowseExportFolder.Click += new System.EventHandler(this.BtnBrowseExportFolder_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(7, 57);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(134, 20);
            this.txtFolderPath.TabIndex = 4;
            // 
            // lblRowsLoaded
            // 
            this.lblRowsLoaded.AutoSize = true;
            this.lblRowsLoaded.Location = new System.Drawing.Point(307, 17);
            this.lblRowsLoaded.Name = "lblRowsLoaded";
            this.lblRowsLoaded.Size = new System.Drawing.Size(82, 13);
            this.lblRowsLoaded.TabIndex = 7;
            this.lblRowsLoaded.Text = "0 Rows Loaded";
            // 
            // lblTare
            // 
            this.lblTare.AutoSize = true;
            this.lblTare.Location = new System.Drawing.Point(255, 106);
            this.lblTare.Name = "lblTare";
            this.lblTare.Size = new System.Drawing.Size(39, 13);
            this.lblTare.TabIndex = 8;
            this.lblTare.Text = "lblTare";
            // 
            // txtCarID
            // 
            this.txtCarID.Location = new System.Drawing.Point(7, 103);
            this.txtCarID.Name = "txtCarID";
            this.txtCarID.Size = new System.Drawing.Size(134, 20);
            this.txtCarID.TabIndex = 9;
            // 
            // btnQueryCar
            // 
            this.btnQueryCar.Location = new System.Drawing.Point(147, 103);
            this.btnQueryCar.Name = "btnQueryCar";
            this.btnQueryCar.Size = new System.Drawing.Size(102, 20);
            this.btnQueryCar.TabIndex = 10;
            this.btnQueryCar.Text = "Query Car Data";
            this.btnQueryCar.UseVisualStyleBackColor = true;
            this.btnQueryCar.Click += new System.EventHandler(this.BtnQueryCar_Click);
            // 
            // lblLoad
            // 
            this.lblLoad.AutoSize = true;
            this.lblLoad.Location = new System.Drawing.Point(371, 106);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(35, 13);
            this.lblLoad.TabIndex = 11;
            this.lblLoad.Text = "label1";
            this.lblLoad.Click += new System.EventHandler(this.Label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblLoad);
            this.Controls.Add(this.btnQueryCar);
            this.Controls.Add(this.txtCarID);
            this.Controls.Add(this.lblTare);
            this.Controls.Add(this.lblRowsLoaded);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnBrowseExportFolder);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.btnLoadCSV);
            this.Controls.Add(this.btnFileBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnFileBrowse;
        private System.Windows.Forms.Button btnLoadCSV;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnBrowseExportFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label lblRowsLoaded;
        private System.Windows.Forms.Label lblLoad;
        private System.Windows.Forms.Button btnQueryCar;
        private System.Windows.Forms.TextBox txtCarID;
        private System.Windows.Forms.Label lblTare;
    }
}

