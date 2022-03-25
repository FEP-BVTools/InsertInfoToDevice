namespace InsertInfoToDevice
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EnableSAM_Lab = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ReaderVer_Lab = new System.Windows.Forms.Label();
            this.ReaderStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ComPort_CB1 = new System.Windows.Forms.ComboBox();
            this.ReaderCOMStatus_Lab = new System.Windows.Forms.Label();
            this.BVRecTimeOutCounter = new System.Windows.Forms.Timer(this.components);
            this.Reader_Tx_Rich = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Reader_Rx_Rich = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.BV_Tx_Rich = new System.Windows.Forms.RichTextBox();
            this.BV_Rx_Rich = new System.Windows.Forms.RichTextBox();
            this.BVCOMStatus_Lab = new System.Windows.Forms.Label();
            this.Clear = new System.Windows.Forms.Button();
            this.ModeChange_btn = new System.Windows.Forms.Button();
            this.ReaderRecTimeOutCounter = new System.Windows.Forms.Timer(this.components);
            this.TypeChangeCountPlus = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Testlab = new System.Windows.Forms.Label();
            this.CMDHeader = new System.Windows.Forms.TextBox();
            this.CMDTarget = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Insert_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Value_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FakeBVCMD_btn = new System.Windows.Forms.Button();
            this.FakeBVCMD_TextBox = new System.Windows.Forms.TextBox();
            this.SendCMDToBV_btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EnableSAM_Lab);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ReaderVer_Lab);
            this.groupBox1.Controls.Add(this.ReaderStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(45, 405);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 87);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "卡機資訊";
            this.groupBox1.Visible = false;
            // 
            // EnableSAM_Lab
            // 
            this.EnableSAM_Lab.AutoSize = true;
            this.EnableSAM_Lab.Location = new System.Drawing.Point(88, 60);
            this.EnableSAM_Lab.Name = "EnableSAM_Lab";
            this.EnableSAM_Lab.Size = new System.Drawing.Size(30, 12);
            this.EnableSAM_Lab.TabIndex = 6;
            this.EnableSAM_Lab.Text = "None";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "可用票證：";
            // 
            // ReaderVer_Lab
            // 
            this.ReaderVer_Lab.AutoSize = true;
            this.ReaderVer_Lab.Location = new System.Drawing.Point(78, 39);
            this.ReaderVer_Lab.Name = "ReaderVer_Lab";
            this.ReaderVer_Lab.Size = new System.Drawing.Size(0, 12);
            this.ReaderVer_Lab.TabIndex = 4;
            // 
            // ReaderStatus
            // 
            this.ReaderStatus.AutoSize = true;
            this.ReaderStatus.Location = new System.Drawing.Point(77, 18);
            this.ReaderStatus.Name = "ReaderStatus";
            this.ReaderStatus.Size = new System.Drawing.Size(89, 12);
            this.ReaderStatus.TabIndex = 3;
            this.ReaderStatus.Text = "未連接/尋卡中...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "卡機版本:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "卡機狀態:";
            // 
            // ComPort_CB1
            // 
            this.ComPort_CB1.FormattingEnabled = true;
            this.ComPort_CB1.Items.AddRange(new object[] {
            "選擇ComPort"});
            this.ComPort_CB1.Location = new System.Drawing.Point(24, 38);
            this.ComPort_CB1.Name = "ComPort_CB1";
            this.ComPort_CB1.Size = new System.Drawing.Size(118, 20);
            this.ComPort_CB1.TabIndex = 2;
            this.ComPort_CB1.DropDown += new System.EventHandler(this.ComPort_CB_DropDown);
            this.ComPort_CB1.TextChanged += new System.EventHandler(this.ComPort_CB_TextChanged);
            // 
            // ReaderCOMStatus_Lab
            // 
            this.ReaderCOMStatus_Lab.AutoSize = true;
            this.ReaderCOMStatus_Lab.Location = new System.Drawing.Point(148, 41);
            this.ReaderCOMStatus_Lab.Name = "ReaderCOMStatus_Lab";
            this.ReaderCOMStatus_Lab.Size = new System.Drawing.Size(41, 12);
            this.ReaderCOMStatus_Lab.TabIndex = 5;
            this.ReaderCOMStatus_Lab.Text = "未連接";
            // 
            // BVRecTimeOutCounter
            // 
            this.BVRecTimeOutCounter.Interval = 5000;
            this.BVRecTimeOutCounter.Tick += new System.EventHandler(this.BVRecTimeOutCounter_Tick);
            // 
            // Reader_Tx_Rich
            // 
            this.Reader_Tx_Rich.Location = new System.Drawing.Point(24, 69);
            this.Reader_Tx_Rich.Name = "Reader_Tx_Rich";
            this.Reader_Tx_Rich.Size = new System.Drawing.Size(358, 156);
            this.Reader_Tx_Rich.TabIndex = 7;
            this.Reader_Tx_Rich.Text = "";
            this.Reader_Tx_Rich.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ComPort_CB1);
            this.groupBox2.Controls.Add(this.Reader_Rx_Rich);
            this.groupBox2.Controls.Add(this.Reader_Tx_Rich);
            this.groupBox2.Controls.Add(this.ReaderCOMStatus_Lab);
            this.groupBox2.Location = new System.Drawing.Point(21, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 474);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reader";
            // 
            // Reader_Rx_Rich
            // 
            this.Reader_Rx_Rich.Location = new System.Drawing.Point(24, 255);
            this.Reader_Rx_Rich.Name = "Reader_Rx_Rich";
            this.Reader_Rx_Rich.Size = new System.Drawing.Size(358, 204);
            this.Reader_Rx_Rich.TabIndex = 7;
            this.Reader_Rx_Rich.Text = "";
            this.Reader_Rx_Rich.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.BV_Tx_Rich);
            this.groupBox3.Controls.Add(this.BV_Rx_Rich);
            this.groupBox3.Controls.Add(this.BVCOMStatus_Lab);
            this.groupBox3.Location = new System.Drawing.Point(503, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(426, 474);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BV";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "選擇ComPort"});
            this.comboBox1.Location = new System.Drawing.Point(24, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(118, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // BV_Tx_Rich
            // 
            this.BV_Tx_Rich.Location = new System.Drawing.Point(24, 69);
            this.BV_Tx_Rich.Name = "BV_Tx_Rich";
            this.BV_Tx_Rich.Size = new System.Drawing.Size(358, 156);
            this.BV_Tx_Rich.TabIndex = 7;
            this.BV_Tx_Rich.Text = "";
            this.BV_Tx_Rich.TextChanged += new System.EventHandler(this.BV_Rx_Rich_TextChanged);
            // 
            // BV_Rx_Rich
            // 
            this.BV_Rx_Rich.Location = new System.Drawing.Point(24, 255);
            this.BV_Rx_Rich.Name = "BV_Rx_Rich";
            this.BV_Rx_Rich.Size = new System.Drawing.Size(358, 204);
            this.BV_Rx_Rich.TabIndex = 7;
            this.BV_Rx_Rich.Text = "";
            this.BV_Rx_Rich.TextChanged += new System.EventHandler(this.BV_Rx_Rich_TextChanged_1);
            // 
            // BVCOMStatus_Lab
            // 
            this.BVCOMStatus_Lab.AutoSize = true;
            this.BVCOMStatus_Lab.Location = new System.Drawing.Point(148, 41);
            this.BVCOMStatus_Lab.Name = "BVCOMStatus_Lab";
            this.BVCOMStatus_Lab.Size = new System.Drawing.Size(41, 12);
            this.BVCOMStatus_Lab.TabIndex = 5;
            this.BVCOMStatus_Lab.Text = "未連接";
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(503, 518);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 9;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // ModeChange_btn
            // 
            this.ModeChange_btn.Location = new System.Drawing.Point(372, 518);
            this.ModeChange_btn.Name = "ModeChange_btn";
            this.ModeChange_btn.Size = new System.Drawing.Size(75, 23);
            this.ModeChange_btn.TabIndex = 9;
            this.ModeChange_btn.Text = "Test Mode";
            this.ModeChange_btn.UseVisualStyleBackColor = true;
            this.ModeChange_btn.Click += new System.EventHandler(this.ModeChange_btn_Click);
            // 
            // ReaderRecTimeOutCounter
            // 
            this.ReaderRecTimeOutCounter.Interval = 2000;
            this.ReaderRecTimeOutCounter.Tick += new System.EventHandler(this.ReaderRecTimeOutCounter_Tick);
            // 
            // TypeChangeCountPlus
            // 
            this.TypeChangeCountPlus.Location = new System.Drawing.Point(617, 518);
            this.TypeChangeCountPlus.Name = "TypeChangeCountPlus";
            this.TypeChangeCountPlus.Size = new System.Drawing.Size(127, 23);
            this.TypeChangeCountPlus.TabIndex = 10;
            this.TypeChangeCountPlus.Text = "TypeChangeCount++";
            this.TypeChangeCountPlus.UseVisualStyleBackColor = true;
            this.TypeChangeCountPlus.Click += new System.EventHandler(this.TypeChangeCountPlus_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1034, 18);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(430, 148);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // Testlab
            // 
            this.Testlab.AutoSize = true;
            this.Testlab.Location = new System.Drawing.Point(1046, 265);
            this.Testlab.Name = "Testlab";
            this.Testlab.Size = new System.Drawing.Size(67, 12);
            this.Testlab.TabIndex = 12;
            this.Testlab.Text = "CMD Header";
            // 
            // CMDHeader
            // 
            this.CMDHeader.Location = new System.Drawing.Point(1119, 262);
            this.CMDHeader.Name = "CMDHeader";
            this.CMDHeader.Size = new System.Drawing.Size(314, 22);
            this.CMDHeader.TabIndex = 13;
            this.CMDHeader.Text = "EA,";
            // 
            // CMDTarget
            // 
            this.CMDTarget.FormattingEnabled = true;
            this.CMDTarget.Items.AddRange(new object[] {
            "Reader->BV",
            "BV->Reader"});
            this.CMDTarget.Location = new System.Drawing.Point(1119, 191);
            this.CMDTarget.Name = "CMDTarget";
            this.CMDTarget.Size = new System.Drawing.Size(121, 20);
            this.CMDTarget.TabIndex = 14;
            this.CMDTarget.Text = "Reader->BV";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1046, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Index";
            // 
            // Insert_textBox
            // 
            this.Insert_textBox.Location = new System.Drawing.Point(1119, 328);
            this.Insert_textBox.Name = "Insert_textBox";
            this.Insert_textBox.Size = new System.Drawing.Size(83, 22);
            this.Insert_textBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1046, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "Value";
            // 
            // Value_textBox
            // 
            this.Value_textBox.Location = new System.Drawing.Point(1119, 394);
            this.Value_textBox.Name = "Value_textBox";
            this.Value_textBox.Size = new System.Drawing.Size(83, 22);
            this.Value_textBox.TabIndex = 13;
            this.Value_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Vaule_textBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1046, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "Target";
            // 
            // FakeBVCMD_btn
            // 
            this.FakeBVCMD_btn.Location = new System.Drawing.Point(1048, 502);
            this.FakeBVCMD_btn.Name = "FakeBVCMD_btn";
            this.FakeBVCMD_btn.Size = new System.Drawing.Size(99, 23);
            this.FakeBVCMD_btn.TabIndex = 15;
            this.FakeBVCMD_btn.Text = "下給Reader";
            this.FakeBVCMD_btn.UseVisualStyleBackColor = true;
            this.FakeBVCMD_btn.Click += new System.EventHandler(this.FakeBVCMD_btn_Click);
            // 
            // FakeBVCMD_TextBox
            // 
            this.FakeBVCMD_TextBox.Location = new System.Drawing.Point(1048, 462);
            this.FakeBVCMD_TextBox.Name = "FakeBVCMD_TextBox";
            this.FakeBVCMD_TextBox.Size = new System.Drawing.Size(314, 22);
            this.FakeBVCMD_TextBox.TabIndex = 13;
            this.FakeBVCMD_TextBox.Text = "EA,01,00,00,01,00,90,00";
            // 
            // SendCMDToBV_btn
            // 
            this.SendCMDToBV_btn.Location = new System.Drawing.Point(1263, 502);
            this.SendCMDToBV_btn.Name = "SendCMDToBV_btn";
            this.SendCMDToBV_btn.Size = new System.Drawing.Size(99, 23);
            this.SendCMDToBV_btn.TabIndex = 15;
            this.SendCMDToBV_btn.Text = "下給BV";
            this.SendCMDToBV_btn.UseVisualStyleBackColor = true;
            this.SendCMDToBV_btn.Click += new System.EventHandler(this.SendCMDToBV_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1528, 566);
            this.Controls.Add(this.SendCMDToBV_btn);
            this.Controls.Add(this.FakeBVCMD_btn);
            this.Controls.Add(this.CMDTarget);
            this.Controls.Add(this.Value_textBox);
            this.Controls.Add(this.Insert_textBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FakeBVCMD_TextBox);
            this.Controls.Add(this.CMDHeader);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Testlab);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.TypeChangeCountPlus);
            this.Controls.Add(this.ModeChange_btn);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Reader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ReaderVer_Lab;
        private System.Windows.Forms.Label ReaderStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComPort_CB1;
        private System.Windows.Forms.Label ReaderCOMStatus_Lab;
        private System.Windows.Forms.Label EnableSAM_Lab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer BVRecTimeOutCounter;
        public System.Windows.Forms.RichTextBox Reader_Tx_Rich;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.RichTextBox Reader_Rx_Rich;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.RichTextBox BV_Tx_Rich;
        public System.Windows.Forms.RichTextBox BV_Rx_Rich;
        private System.Windows.Forms.Label BVCOMStatus_Lab;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button ModeChange_btn;
        private System.Windows.Forms.Timer ReaderRecTimeOutCounter;
        private System.Windows.Forms.Button TypeChangeCountPlus;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label Testlab;
        private System.Windows.Forms.TextBox CMDHeader;
        private System.Windows.Forms.ComboBox CMDTarget;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Insert_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Value_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FakeBVCMD_btn;
        private System.Windows.Forms.TextBox FakeBVCMD_TextBox;
        private System.Windows.Forms.Button SendCMDToBV_btn;
    }
}

