namespace OracleAccountChecking
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.DataBtn = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.TotalTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.SuccessTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.FailedTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.ScannedTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.ProxyBtn = new Krypton.Toolkit.KryptonButton();
            this.StopBtn = new Krypton.Toolkit.KryptonButton();
            this.StartBtn = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            this.ThreadInput = new Krypton.Toolkit.KryptonNumericUpDown();
            this.ProxyTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new Krypton.Toolkit.KryptonLabel();
            this.HideChromeCheckBox = new Krypton.Toolkit.KryptonCheckBox();
            this.HttpRadioBtn = new Krypton.Toolkit.KryptonRadioButton();
            this.ProxyLessRadioBtn = new Krypton.Toolkit.KryptonRadioButton();
            this.Socks5RadioBtn = new Krypton.Toolkit.KryptonRadioButton();
            this.kryptonLabel7 = new Krypton.Toolkit.KryptonLabel();
            this.TopMostBtn = new Krypton.Toolkit.KryptonCheckBox();
            this.ForceStopBtn = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel8 = new Krypton.Toolkit.KryptonLabel();
            this.TimeoutInput = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel9 = new Krypton.Toolkit.KryptonLabel();
            this.ExtensionsTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.ExtensionBtn = new Krypton.Toolkit.KryptonButton();
            this.ClearExtensionsBtn = new Krypton.Toolkit.KryptonButton();
            this.LoadExtensionBtn = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel11 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel12 = new Krypton.Toolkit.KryptonLabel();
            this.StatusTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.StartPointInput = new Krypton.Toolkit.KryptonNumericUpDown();
            this.SuspendLayout();
            // 
            // DataBtn
            // 
            this.DataBtn.CornerRoundingRadius = -1F;
            this.DataBtn.Location = new System.Drawing.Point(245, 17);
            this.DataBtn.Name = "DataBtn";
            this.DataBtn.Size = new System.Drawing.Size(119, 25);
            this.DataBtn.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.DataBtn.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.DataBtn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataBtn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DataBtn.TabIndex = 0;
            this.DataBtn.Values.Text = "File dữ liệu";
            this.DataBtn.Click += new System.EventHandler(this.DataBtn_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 22);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(118, 20);
            this.kryptonLabel1.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel1.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Tổng số tài khoản:";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(12, 59);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(82, 20);
            this.kryptonLabel2.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel2.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Thành công:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(12, 97);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(61, 20);
            this.kryptonLabel3.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel3.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel3.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.Values.Text = "Thất bại:";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(12, 132);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(81, 20);
            this.kryptonLabel4.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel4.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel4.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel4.TabIndex = 4;
            this.kryptonLabel4.Values.Text = "Đã kiểm tra:";
            // 
            // TotalTextBox
            // 
            this.TotalTextBox.Location = new System.Drawing.Point(127, 19);
            this.TotalTextBox.Name = "TotalTextBox";
            this.TotalTextBox.ReadOnly = true;
            this.TotalTextBox.Size = new System.Drawing.Size(100, 23);
            this.TotalTextBox.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TotalTextBox.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TotalTextBox.TabIndex = 5;
            this.TotalTextBox.Text = "0";
            this.TotalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SuccessTextBox
            // 
            this.SuccessTextBox.Location = new System.Drawing.Point(127, 56);
            this.SuccessTextBox.Name = "SuccessTextBox";
            this.SuccessTextBox.ReadOnly = true;
            this.SuccessTextBox.Size = new System.Drawing.Size(100, 23);
            this.SuccessTextBox.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SuccessTextBox.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SuccessTextBox.TabIndex = 6;
            this.SuccessTextBox.Text = "0";
            this.SuccessTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FailedTextBox
            // 
            this.FailedTextBox.Location = new System.Drawing.Point(127, 94);
            this.FailedTextBox.Name = "FailedTextBox";
            this.FailedTextBox.ReadOnly = true;
            this.FailedTextBox.Size = new System.Drawing.Size(100, 23);
            this.FailedTextBox.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.FailedTextBox.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.FailedTextBox.TabIndex = 7;
            this.FailedTextBox.Text = "0";
            this.FailedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ScannedTextBox
            // 
            this.ScannedTextBox.Location = new System.Drawing.Point(127, 132);
            this.ScannedTextBox.Name = "ScannedTextBox";
            this.ScannedTextBox.ReadOnly = true;
            this.ScannedTextBox.Size = new System.Drawing.Size(100, 23);
            this.ScannedTextBox.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ScannedTextBox.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ScannedTextBox.TabIndex = 8;
            this.ScannedTextBox.Text = "0";
            this.ScannedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ProxyBtn
            // 
            this.ProxyBtn.CornerRoundingRadius = -1F;
            this.ProxyBtn.Location = new System.Drawing.Point(245, 54);
            this.ProxyBtn.Name = "ProxyBtn";
            this.ProxyBtn.Size = new System.Drawing.Size(119, 25);
            this.ProxyBtn.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ProxyBtn.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ProxyBtn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProxyBtn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProxyBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProxyBtn.TabIndex = 9;
            this.ProxyBtn.Values.Text = "File proxy";
            this.ProxyBtn.Click += new System.EventHandler(this.ProxyBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.CornerRoundingRadius = -1F;
            this.StopBtn.Enabled = false;
            this.StopBtn.Location = new System.Drawing.Point(245, 246);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(119, 25);
            this.StopBtn.StateCommon.Back.Color1 = System.Drawing.Color.Maroon;
            this.StopBtn.StateCommon.Back.Color2 = System.Drawing.Color.Maroon;
            this.StopBtn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.StopBtn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.StopBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StopBtn.TabIndex = 11;
            this.StopBtn.Values.Text = "Dừng lại";
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.CornerRoundingRadius = -1F;
            this.StartBtn.Location = new System.Drawing.Point(245, 208);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(119, 25);
            this.StartBtn.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.StartBtn.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.StartBtn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.StartBtn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.StartBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StartBtn.TabIndex = 12;
            this.StartBtn.Values.Text = "Bắt đầu";
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(245, 97);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(50, 20);
            this.kryptonLabel5.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel5.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel5.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel5.TabIndex = 13;
            this.kryptonLabel5.Values.Text = "Luồng:";
            // 
            // ThreadInput
            // 
            this.ThreadInput.Location = new System.Drawing.Point(293, 94);
            this.ThreadInput.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.ThreadInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThreadInput.Name = "ThreadInput";
            this.ThreadInput.Size = new System.Drawing.Size(71, 22);
            this.ThreadInput.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ThreadInput.StateCommon.Content.Color1 = System.Drawing.Color.Black;
            this.ThreadInput.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ThreadInput.TabIndex = 14;
            this.ThreadInput.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ThreadInput.ValueChanged += new System.EventHandler(this.ThreadInput_ValueChanged);
            // 
            // ProxyTextBox
            // 
            this.ProxyTextBox.Location = new System.Drawing.Point(127, 170);
            this.ProxyTextBox.Name = "ProxyTextBox";
            this.ProxyTextBox.ReadOnly = true;
            this.ProxyTextBox.Size = new System.Drawing.Size(100, 23);
            this.ProxyTextBox.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ProxyTextBox.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProxyTextBox.TabIndex = 16;
            this.ProxyTextBox.Text = "0";
            this.ProxyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(12, 170);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(103, 20);
            this.kryptonLabel6.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel6.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel6.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel6.TabIndex = 15;
            this.kryptonLabel6.Values.Text = "Số lượng Proxy:";
            // 
            // HideChromeCheckBox
            // 
            this.HideChromeCheckBox.Checked = true;
            this.HideChromeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HideChromeCheckBox.Location = new System.Drawing.Point(245, 135);
            this.HideChromeCheckBox.Name = "HideChromeCheckBox";
            this.HideChromeCheckBox.Size = new System.Drawing.Size(87, 20);
            this.HideChromeCheckBox.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HideChromeCheckBox.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HideChromeCheckBox.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.HideChromeCheckBox.TabIndex = 18;
            this.HideChromeCheckBox.Values.Text = "Ẩn chrome";
            this.HideChromeCheckBox.CheckedChanged += new System.EventHandler(this.HideChromeCheckBox_CheckedChanged);
            // 
            // HttpRadioBtn
            // 
            this.HttpRadioBtn.Location = new System.Drawing.Point(127, 234);
            this.HttpRadioBtn.Name = "HttpRadioBtn";
            this.HttpRadioBtn.Size = new System.Drawing.Size(49, 20);
            this.HttpRadioBtn.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HttpRadioBtn.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HttpRadioBtn.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.HttpRadioBtn.TabIndex = 19;
            this.HttpRadioBtn.Values.Text = "Http";
            this.HttpRadioBtn.CheckedChanged += new System.EventHandler(this.HttpRadioBtn_CheckedChanged);
            // 
            // ProxyLessRadioBtn
            // 
            this.ProxyLessRadioBtn.Checked = true;
            this.ProxyLessRadioBtn.Location = new System.Drawing.Point(127, 208);
            this.ProxyLessRadioBtn.Name = "ProxyLessRadioBtn";
            this.ProxyLessRadioBtn.Size = new System.Drawing.Size(77, 20);
            this.ProxyLessRadioBtn.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProxyLessRadioBtn.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ProxyLessRadioBtn.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProxyLessRadioBtn.TabIndex = 20;
            this.ProxyLessRadioBtn.Values.Text = "Proxyless";
            this.ProxyLessRadioBtn.CheckedChanged += new System.EventHandler(this.ProxyLessRadioBtn_CheckedChanged);
            // 
            // Socks5RadioBtn
            // 
            this.Socks5RadioBtn.Location = new System.Drawing.Point(127, 260);
            this.Socks5RadioBtn.Name = "Socks5RadioBtn";
            this.Socks5RadioBtn.Size = new System.Drawing.Size(62, 20);
            this.Socks5RadioBtn.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Socks5RadioBtn.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Socks5RadioBtn.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Socks5RadioBtn.TabIndex = 21;
            this.Socks5RadioBtn.Values.Text = "Socks5";
            this.Socks5RadioBtn.CheckedChanged += new System.EventHandler(this.Socks5RadioBtn_CheckedChanged);
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(12, 208);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel7.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel7.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel7.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel7.TabIndex = 22;
            this.kryptonLabel7.Values.Text = "Loại Proxy:";
            // 
            // TopMostBtn
            // 
            this.TopMostBtn.Checked = true;
            this.TopMostBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TopMostBtn.Location = new System.Drawing.Point(245, 170);
            this.TopMostBtn.Name = "TopMostBtn";
            this.TopMostBtn.Size = new System.Drawing.Size(79, 20);
            this.TopMostBtn.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TopMostBtn.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TopMostBtn.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TopMostBtn.TabIndex = 23;
            this.TopMostBtn.Values.Text = "Top Most";
            this.TopMostBtn.CheckedChanged += new System.EventHandler(this.TopMostBtn_CheckedChanged);
            // 
            // ForceStopBtn
            // 
            this.ForceStopBtn.CornerRoundingRadius = -1F;
            this.ForceStopBtn.Location = new System.Drawing.Point(245, 288);
            this.ForceStopBtn.Name = "ForceStopBtn";
            this.ForceStopBtn.Size = new System.Drawing.Size(119, 25);
            this.ForceStopBtn.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ForceStopBtn.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ForceStopBtn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ForceStopBtn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ForceStopBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForceStopBtn.TabIndex = 24;
            this.ForceStopBtn.Values.Text = "Dừng ngay";
            this.ForceStopBtn.Click += new System.EventHandler(this.ForceStopBtn_Click);
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(12, 293);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(66, 20);
            this.kryptonLabel8.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel8.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel8.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel8.TabIndex = 25;
            this.kryptonLabel8.Values.Text = "Time out:";
            // 
            // TimeoutInput
            // 
            this.TimeoutInput.Location = new System.Drawing.Point(127, 291);
            this.TimeoutInput.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.TimeoutInput.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TimeoutInput.Name = "TimeoutInput";
            this.TimeoutInput.Size = new System.Drawing.Size(100, 22);
            this.TimeoutInput.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.TimeoutInput.StateCommon.Content.Color1 = System.Drawing.Color.Black;
            this.TimeoutInput.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TimeoutInput.TabIndex = 26;
            this.TimeoutInput.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.TimeoutInput.ValueChanged += new System.EventHandler(this.TimeoutInput_ValueChanged);
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(12, 368);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(76, 20);
            this.kryptonLabel9.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel9.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel9.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel9.TabIndex = 27;
            this.kryptonLabel9.Values.Text = "Extensions:";
            // 
            // ExtensionsTextBox
            // 
            this.ExtensionsTextBox.Location = new System.Drawing.Point(127, 368);
            this.ExtensionsTextBox.Name = "ExtensionsTextBox";
            this.ExtensionsTextBox.ReadOnly = true;
            this.ExtensionsTextBox.Size = new System.Drawing.Size(100, 23);
            this.ExtensionsTextBox.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ExtensionsTextBox.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ExtensionsTextBox.TabIndex = 28;
            this.ExtensionsTextBox.Text = "0";
            this.ExtensionsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtensionBtn
            // 
            this.ExtensionBtn.CornerRoundingRadius = -1F;
            this.ExtensionBtn.Location = new System.Drawing.Point(245, 328);
            this.ExtensionBtn.Name = "ExtensionBtn";
            this.ExtensionBtn.Size = new System.Drawing.Size(119, 25);
            this.ExtensionBtn.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ExtensionBtn.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ExtensionBtn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ExtensionBtn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ExtensionBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ExtensionBtn.TabIndex = 29;
            this.ExtensionBtn.Values.Text = "Thêm Extension";
            this.ExtensionBtn.Click += new System.EventHandler(this.ExtensionBtn_Click);
            // 
            // ClearExtensionsBtn
            // 
            this.ClearExtensionsBtn.CornerRoundingRadius = -1F;
            this.ClearExtensionsBtn.Location = new System.Drawing.Point(245, 409);
            this.ClearExtensionsBtn.Name = "ClearExtensionsBtn";
            this.ClearExtensionsBtn.Size = new System.Drawing.Size(119, 25);
            this.ClearExtensionsBtn.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClearExtensionsBtn.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClearExtensionsBtn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClearExtensionsBtn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClearExtensionsBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ClearExtensionsBtn.TabIndex = 30;
            this.ClearExtensionsBtn.Values.Text = "Clear Extension";
            this.ClearExtensionsBtn.Click += new System.EventHandler(this.ClearExtensionsBtn_Click);
            // 
            // LoadExtensionBtn
            // 
            this.LoadExtensionBtn.CornerRoundingRadius = -1F;
            this.LoadExtensionBtn.Location = new System.Drawing.Point(245, 371);
            this.LoadExtensionBtn.Name = "LoadExtensionBtn";
            this.LoadExtensionBtn.Size = new System.Drawing.Size(119, 25);
            this.LoadExtensionBtn.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.LoadExtensionBtn.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.LoadExtensionBtn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LoadExtensionBtn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LoadExtensionBtn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LoadExtensionBtn.TabIndex = 31;
            this.LoadExtensionBtn.Values.Text = "Load Extensions";
            this.LoadExtensionBtn.Click += new System.EventHandler(this.LoadExtensionBtn_Click);
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(12, 328);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel11.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel11.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel11.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel11.TabIndex = 33;
            this.kryptonLabel11.Values.Text = "Bắt đầu từ:";
            // 
            // kryptonLabel12
            // 
            this.kryptonLabel12.Location = new System.Drawing.Point(12, 409);
            this.kryptonLabel12.Name = "kryptonLabel12";
            this.kryptonLabel12.Size = new System.Drawing.Size(74, 20);
            this.kryptonLabel12.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel12.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonLabel12.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.kryptonLabel12.TabIndex = 34;
            this.kryptonLabel12.Values.Text = "Trạng thái:";
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Location = new System.Drawing.Point(127, 409);
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.Size = new System.Drawing.Size(100, 23);
            this.StatusTextBox.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.StatusTextBox.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StatusTextBox.TabIndex = 37;
            this.StatusTextBox.Text = "Chưa bắt đầu";
            this.StatusTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StartPointInput
            // 
            this.StartPointInput.Location = new System.Drawing.Point(127, 328);
            this.StartPointInput.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.StartPointInput.Name = "StartPointInput";
            this.StartPointInput.Size = new System.Drawing.Size(100, 22);
            this.StartPointInput.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.StartPointInput.StateCommon.Content.Color1 = System.Drawing.Color.Black;
            this.StartPointInput.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StartPointInput.TabIndex = 38;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(384, 451);
            this.Controls.Add(this.StartPointInput);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.kryptonLabel12);
            this.Controls.Add(this.kryptonLabel11);
            this.Controls.Add(this.LoadExtensionBtn);
            this.Controls.Add(this.ClearExtensionsBtn);
            this.Controls.Add(this.ExtensionBtn);
            this.Controls.Add(this.ExtensionsTextBox);
            this.Controls.Add(this.kryptonLabel9);
            this.Controls.Add(this.TimeoutInput);
            this.Controls.Add(this.kryptonLabel8);
            this.Controls.Add(this.ForceStopBtn);
            this.Controls.Add(this.TopMostBtn);
            this.Controls.Add(this.kryptonLabel7);
            this.Controls.Add(this.Socks5RadioBtn);
            this.Controls.Add(this.ProxyLessRadioBtn);
            this.Controls.Add(this.HttpRadioBtn);
            this.Controls.Add(this.HideChromeCheckBox);
            this.Controls.Add(this.ProxyTextBox);
            this.Controls.Add(this.kryptonLabel6);
            this.Controls.Add(this.ThreadInput);
            this.Controls.Add(this.kryptonLabel5);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.ProxyBtn);
            this.Controls.Add(this.ScannedTextBox);
            this.Controls.Add(this.FailedTextBox);
            this.Controls.Add(this.SuccessTextBox);
            this.Controls.Add(this.TotalTextBox);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.DataBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 490);
            this.MinimumSize = new System.Drawing.Size(400, 490);
            this.Name = "FrmMain";
            this.Text = "Oracle Scan - Tele: @lukaxsx";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonButton DataBtn;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonTextBox TotalTextBox;
        private Krypton.Toolkit.KryptonTextBox SuccessTextBox;
        private Krypton.Toolkit.KryptonTextBox FailedTextBox;
        private Krypton.Toolkit.KryptonTextBox ScannedTextBox;
        private Krypton.Toolkit.KryptonButton ProxyBtn;
        private Krypton.Toolkit.KryptonButton StopBtn;
        private Krypton.Toolkit.KryptonButton StartBtn;
        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private Krypton.Toolkit.KryptonNumericUpDown ThreadInput;
        private Krypton.Toolkit.KryptonTextBox ProxyTextBox;
        private Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private Krypton.Toolkit.KryptonCheckBox HideChromeCheckBox;
        private Krypton.Toolkit.KryptonRadioButton HttpRadioBtn;
        private Krypton.Toolkit.KryptonRadioButton ProxyLessRadioBtn;
        private Krypton.Toolkit.KryptonRadioButton Socks5RadioBtn;
        private Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private Krypton.Toolkit.KryptonCheckBox TopMostBtn;
        private Krypton.Toolkit.KryptonButton ForceStopBtn;
        private Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private Krypton.Toolkit.KryptonNumericUpDown TimeoutInput;
        private Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private Krypton.Toolkit.KryptonTextBox ExtensionsTextBox;
        private Krypton.Toolkit.KryptonButton ExtensionBtn;
        private Krypton.Toolkit.KryptonButton ClearExtensionsBtn;
        private Krypton.Toolkit.KryptonButton LoadExtensionBtn;
        private Krypton.Toolkit.KryptonLabel kryptonLabel11;
        private Krypton.Toolkit.KryptonLabel kryptonLabel12;
        private Krypton.Toolkit.KryptonTextBox StatusTextBox;
        private Krypton.Toolkit.KryptonNumericUpDown StartPointInput;
    }
}