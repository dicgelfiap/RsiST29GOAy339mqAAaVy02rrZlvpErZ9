namespace Server.Forms
{
	// Token: 0x02000059 RID: 89
	public partial class FormBuilder : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0001DD14 File Offset: 0x0001DD14
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001DD3C File Offset: 0x0001DD3C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.directoryEntry1 = new global::System.DirectoryServices.DirectoryEntry();
			this.toolTip1 = new global::System.Windows.Forms.ToolTip(this.components);
			this.label17 = new global::DarkUI.Controls.DarkLabel();
			this.numDelay = new global::DarkUI.Controls.DarkNumericUpDown();
			this.label16 = new global::DarkUI.Controls.DarkLabel();
			this.chkBsod = new global::DarkUI.Controls.DarkCheckBox();
			this.label5 = new global::DarkUI.Controls.DarkLabel();
			this.comboBoxFolder = new global::DarkUI.Controls.DarkComboBox();
			this.checkBox1 = new global::DarkUI.Controls.DarkCheckBox();
			this.label3 = new global::DarkUI.Controls.DarkLabel();
			this.label4 = new global::DarkUI.Controls.DarkLabel();
			this.btnBuild = new global::DarkUI.Controls.DarkButton();
			this.chkIcon = new global::DarkUI.Controls.DarkCheckBox();
			this.txtIcon = new global::DarkUI.Controls.DarkTextBox();
			this.btnIcon = new global::DarkUI.Controls.DarkButton();
			this.picIcon = new global::System.Windows.Forms.PictureBox();
			this.btnClone = new global::DarkUI.Controls.DarkButton();
			this.btnAssembly = new global::DarkUI.Controls.DarkCheckBox();
			this.label14 = new global::DarkUI.Controls.DarkLabel();
			this.label13 = new global::DarkUI.Controls.DarkLabel();
			this.label12 = new global::DarkUI.Controls.DarkLabel();
			this.label11 = new global::DarkUI.Controls.DarkLabel();
			this.label10 = new global::DarkUI.Controls.DarkLabel();
			this.label9 = new global::DarkUI.Controls.DarkLabel();
			this.label7 = new global::DarkUI.Controls.DarkLabel();
			this.label8 = new global::DarkUI.Controls.DarkLabel();
			this.btnRemoveIP = new global::DarkUI.Controls.DarkButton();
			this.btnAddIP = new global::DarkUI.Controls.DarkButton();
			this.textIP = new global::DarkUI.Controls.DarkTextBox();
			this.listBoxIP = new global::System.Windows.Forms.ListBox();
			this.label2 = new global::DarkUI.Controls.DarkLabel();
			this.textPort = new global::DarkUI.Controls.DarkTextBox();
			this.label1 = new global::DarkUI.Controls.DarkLabel();
			this.btnRemovePort = new global::DarkUI.Controls.DarkButton();
			this.btnAddPort = new global::DarkUI.Controls.DarkButton();
			this.chkPaste_bin = new global::DarkUI.Controls.DarkCheckBox();
			this.listBoxPort = new global::System.Windows.Forms.ListBox();
			this.txtGroup = new global::DarkUI.Controls.DarkTextBox();
			this.txtMutex = new global::DarkUI.Controls.DarkTextBox();
			this.textFilename = new global::DarkUI.Controls.DarkTextBox();
			this.txtFileVersion = new global::DarkUI.Controls.DarkTextBox();
			this.txtProductVersion = new global::DarkUI.Controls.DarkTextBox();
			this.txtOriginalFilename = new global::DarkUI.Controls.DarkTextBox();
			this.txtTrademarks = new global::DarkUI.Controls.DarkTextBox();
			this.txtCopyright = new global::DarkUI.Controls.DarkTextBox();
			this.txtCompany = new global::DarkUI.Controls.DarkTextBox();
			this.txtDescription = new global::DarkUI.Controls.DarkTextBox();
			this.txtProduct = new global::DarkUI.Controls.DarkTextBox();
			this.txtPaste_bin = new global::DarkUI.Controls.DarkTextBox();
			this.chkAnti = new global::DarkUI.Controls.DarkCheckBox();
			this.chkAntiProcess = new global::DarkUI.Controls.DarkCheckBox();
			((global::System.ComponentModel.ISupportInitialize)this.numDelay).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.picIcon).BeginInit();
			base.SuspendLayout();
			this.label17.AutoSize = true;
			this.label17.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label17.Location = new global::System.Drawing.Point(260, 185);
			this.label17.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label17.Name = "label17";
			this.label17.Size = new global::System.Drawing.Size(36, 13);
			this.label17.TabIndex = 109;
			this.label17.Text = "Group";
			this.numDelay.Location = new global::System.Drawing.Point(411, 364);
			this.numDelay.Margin = new global::System.Windows.Forms.Padding(2);
			global::System.Windows.Forms.NumericUpDown numericUpDown = this.numDelay;
			int[] array = new int[4];
			array[0] = 999;
			numericUpDown.Maximum = new decimal(array);
			global::System.Windows.Forms.NumericUpDown numericUpDown2 = this.numDelay;
			int[] array2 = new int[4];
			array2[0] = 1;
			numericUpDown2.Minimum = new decimal(array2);
			this.numDelay.Name = "numDelay";
			this.numDelay.Size = new global::System.Drawing.Size(56, 20);
			this.numDelay.TabIndex = 108;
			global::System.Windows.Forms.NumericUpDown numericUpDown3 = this.numDelay;
			int[] array3 = new int[4];
			array3[0] = 1;
			numericUpDown3.Value = new decimal(array3);
			this.label16.AutoSize = true;
			this.label16.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label16.Location = new global::System.Drawing.Point(260, 366);
			this.label16.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label16.Name = "label16";
			this.label16.Size = new global::System.Drawing.Size(48, 13);
			this.label16.TabIndex = 107;
			this.label16.Text = "Sleep (s)";
			this.chkBsod.AutoSize = true;
			this.chkBsod.Location = new global::System.Drawing.Point(395, 208);
			this.chkBsod.Margin = new global::System.Windows.Forms.Padding(2);
			this.chkBsod.Name = "chkBsod";
			this.chkBsod.Size = new global::System.Drawing.Size(56, 17);
			this.chkBsod.TabIndex = 105;
			this.chkBsod.Text = "BSOD";
			this.label5.AutoSize = true;
			this.label5.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label5.Location = new global::System.Drawing.Point(260, 243);
			this.label5.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(36, 13);
			this.label5.TabIndex = 103;
			this.label5.Text = "Mutex";
			this.comboBoxFolder.DrawMode = global::System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.comboBoxFolder.Enabled = false;
			this.comboBoxFolder.FormattingEnabled = true;
			this.comboBoxFolder.Items.AddRange(new object[]
			{
				"%AppData%",
				"%Temp%"
			});
			this.comboBoxFolder.Location = new global::System.Drawing.Point(549, 339);
			this.comboBoxFolder.Margin = new global::System.Windows.Forms.Padding(2);
			this.comboBoxFolder.Name = "comboBoxFolder";
			this.comboBoxFolder.Size = new global::System.Drawing.Size(203, 21);
			this.comboBoxFolder.TabIndex = 101;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new global::System.Drawing.Point(489, 269);
			this.checkBox1.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new global::System.Drawing.Size(53, 17);
			this.checkBox1.TabIndex = 100;
			this.checkBox1.Text = "Install";
			this.checkBox1.CheckedChanged += new global::System.EventHandler(this.checkBox1_CheckedChanged);
			this.label3.AutoSize = true;
			this.label3.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label3.Location = new global::System.Drawing.Point(487, 341);
			this.label3.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(47, 13);
			this.label3.TabIndex = 97;
			this.label3.Text = "File path";
			this.label4.AutoSize = true;
			this.label4.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label4.Location = new global::System.Drawing.Point(487, 304);
			this.label4.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(52, 13);
			this.label4.TabIndex = 98;
			this.label4.Text = "File name";
			this.btnBuild.Location = new global::System.Drawing.Point(490, 377);
			this.btnBuild.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnBuild.Name = "btnBuild";
			this.btnBuild.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnBuild.Size = new global::System.Drawing.Size(262, 49);
			this.btnBuild.TabIndex = 95;
			this.btnBuild.Text = "Build exe";
			this.btnBuild.Click += new global::System.EventHandler(this.BtnBuild_Click);
			this.chkIcon.AutoSize = true;
			this.chkIcon.Location = new global::System.Drawing.Point(20, 236);
			this.chkIcon.Margin = new global::System.Windows.Forms.Padding(2);
			this.chkIcon.Name = "chkIcon";
			this.chkIcon.Size = new global::System.Drawing.Size(47, 17);
			this.chkIcon.TabIndex = 94;
			this.chkIcon.Text = "Icon";
			this.chkIcon.CheckedChanged += new global::System.EventHandler(this.ChkIcon_CheckedChanged);
			this.txtIcon.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtIcon.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtIcon.Enabled = false;
			this.txtIcon.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtIcon.Location = new global::System.Drawing.Point(20, 258);
			this.txtIcon.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtIcon.Name = "txtIcon";
			this.txtIcon.Size = new global::System.Drawing.Size(205, 20);
			this.txtIcon.TabIndex = 93;
			this.btnIcon.Enabled = false;
			this.btnIcon.Location = new global::System.Drawing.Point(137, 297);
			this.btnIcon.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnIcon.Name = "btnIcon";
			this.btnIcon.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnIcon.Size = new global::System.Drawing.Size(88, 41);
			this.btnIcon.TabIndex = 92;
			this.btnIcon.Text = "Choose icon";
			this.btnIcon.Click += new global::System.EventHandler(this.BtnIcon_Click);
			this.picIcon.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.picIcon.ErrorImage = null;
			this.picIcon.InitialImage = null;
			this.picIcon.Location = new global::System.Drawing.Point(20, 297);
			this.picIcon.Margin = new global::System.Windows.Forms.Padding(2);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new global::System.Drawing.Size(110, 119);
			this.picIcon.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picIcon.TabIndex = 91;
			this.picIcon.TabStop = false;
			this.btnClone.Enabled = false;
			this.btnClone.Location = new global::System.Drawing.Point(683, 9);
			this.btnClone.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnClone.Name = "btnClone";
			this.btnClone.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnClone.Size = new global::System.Drawing.Size(69, 25);
			this.btnClone.TabIndex = 90;
			this.btnClone.Text = "Clone";
			this.btnClone.Click += new global::System.EventHandler(this.BtnClone_Click);
			this.btnAssembly.AutoSize = true;
			this.btnAssembly.Location = new global::System.Drawing.Point(489, 13);
			this.btnAssembly.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnAssembly.Name = "btnAssembly";
			this.btnAssembly.Size = new global::System.Drawing.Size(70, 17);
			this.btnAssembly.TabIndex = 89;
			this.btnAssembly.Text = "Assembly";
			this.btnAssembly.CheckedChanged += new global::System.EventHandler(this.BtnAssembly_CheckedChanged);
			this.label14.AutoSize = true;
			this.label14.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label14.Location = new global::System.Drawing.Point(487, 213);
			this.label14.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label14.Name = "label14";
			this.label14.Size = new global::System.Drawing.Size(85, 13);
			this.label14.TabIndex = 82;
			this.label14.Text = "Product Version:";
			this.label13.AutoSize = true;
			this.label13.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label13.Location = new global::System.Drawing.Point(487, 242);
			this.label13.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(64, 13);
			this.label13.TabIndex = 81;
			this.label13.Text = "File Version:";
			this.label12.AutoSize = true;
			this.label12.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label12.Location = new global::System.Drawing.Point(487, 186);
			this.label12.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(90, 13);
			this.label12.TabIndex = 80;
			this.label12.Text = "Original Filename:";
			this.label11.AutoSize = true;
			this.label11.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label11.Location = new global::System.Drawing.Point(487, 159);
			this.label11.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(66, 13);
			this.label11.TabIndex = 79;
			this.label11.Text = "Trademarks:";
			this.label10.AutoSize = true;
			this.label10.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label10.Location = new global::System.Drawing.Point(487, 132);
			this.label10.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(54, 13);
			this.label10.TabIndex = 78;
			this.label10.Text = "Copyright:";
			this.label9.AutoSize = true;
			this.label9.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label9.Location = new global::System.Drawing.Point(487, 105);
			this.label9.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(54, 13);
			this.label9.TabIndex = 77;
			this.label9.Text = "Company:";
			this.label7.AutoSize = true;
			this.label7.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label7.Location = new global::System.Drawing.Point(487, 77);
			this.label7.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(63, 13);
			this.label7.TabIndex = 75;
			this.label7.Text = "Description:";
			this.label8.AutoSize = true;
			this.label8.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label8.Location = new global::System.Drawing.Point(487, 50);
			this.label8.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(47, 13);
			this.label8.TabIndex = 73;
			this.label8.Text = "Product:";
			this.btnRemoveIP.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnRemoveIP.Location = new global::System.Drawing.Point(127, 129);
			this.btnRemoveIP.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnRemoveIP.Name = "btnRemoveIP";
			this.btnRemoveIP.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnRemoveIP.Size = new global::System.Drawing.Size(29, 24);
			this.btnRemoveIP.TabIndex = 72;
			this.btnRemoveIP.Text = "-";
			this.btnRemoveIP.Click += new global::System.EventHandler(this.BtnRemoveIP_Click);
			this.btnAddIP.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnAddIP.Location = new global::System.Drawing.Point(48, 129);
			this.btnAddIP.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnAddIP.Name = "btnAddIP";
			this.btnAddIP.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnAddIP.Size = new global::System.Drawing.Size(29, 24);
			this.btnAddIP.TabIndex = 71;
			this.btnAddIP.Text = "+";
			this.btnAddIP.Click += new global::System.EventHandler(this.BtnAddIP_Click);
			this.textIP.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.textIP.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textIP.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.textIP.Location = new global::System.Drawing.Point(48, 12);
			this.textIP.Margin = new global::System.Windows.Forms.Padding(2);
			this.textIP.Name = "textIP";
			this.textIP.Size = new global::System.Drawing.Size(109, 20);
			this.textIP.TabIndex = 69;
			this.listBoxIP.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listBoxIP.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.listBoxIP.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listBoxIP.FormattingEnabled = true;
			this.listBoxIP.Location = new global::System.Drawing.Point(48, 34);
			this.listBoxIP.Margin = new global::System.Windows.Forms.Padding(2);
			this.listBoxIP.Name = "listBoxIP";
			this.listBoxIP.Size = new global::System.Drawing.Size(109, 80);
			this.listBoxIP.TabIndex = 70;
			this.label2.AutoSize = true;
			this.label2.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label2.Location = new global::System.Drawing.Point(178, 15);
			this.label2.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(26, 13);
			this.label2.TabIndex = 61;
			this.label2.Text = "Port";
			this.textPort.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.textPort.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textPort.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.textPort.Location = new global::System.Drawing.Point(214, 13);
			this.textPort.Margin = new global::System.Windows.Forms.Padding(2);
			this.textPort.Name = "textPort";
			this.textPort.Size = new global::System.Drawing.Size(109, 20);
			this.textPort.TabIndex = 62;
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.label1.Location = new global::System.Drawing.Point(12, 14);
			this.label1.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(17, 13);
			this.label1.TabIndex = 68;
			this.label1.Text = "IP";
			this.btnRemovePort.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnRemovePort.Location = new global::System.Drawing.Point(293, 132);
			this.btnRemovePort.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnRemovePort.Name = "btnRemovePort";
			this.btnRemovePort.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnRemovePort.Size = new global::System.Drawing.Size(29, 26);
			this.btnRemovePort.TabIndex = 67;
			this.btnRemovePort.Text = "-";
			this.btnRemovePort.Click += new global::System.EventHandler(this.BtnRemovePort_Click);
			this.btnAddPort.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btnAddPort.Location = new global::System.Drawing.Point(214, 132);
			this.btnAddPort.Margin = new global::System.Windows.Forms.Padding(2);
			this.btnAddPort.Name = "btnAddPort";
			this.btnAddPort.Padding = new global::System.Windows.Forms.Padding(5);
			this.btnAddPort.Size = new global::System.Drawing.Size(29, 24);
			this.btnAddPort.TabIndex = 66;
			this.btnAddPort.Text = "+";
			this.btnAddPort.Click += new global::System.EventHandler(this.BtnAddPort_Click);
			this.chkPaste_bin.AutoSize = true;
			this.chkPaste_bin.Location = new global::System.Drawing.Point(17, 176);
			this.chkPaste_bin.Margin = new global::System.Windows.Forms.Padding(2);
			this.chkPaste_bin.Name = "chkPaste_bin";
			this.chkPaste_bin.Size = new global::System.Drawing.Size(87, 17);
			this.chkPaste_bin.TabIndex = 64;
			this.chkPaste_bin.Text = "Get ip by link";
			this.chkPaste_bin.CheckedChanged += new global::System.EventHandler(this.CheckBox2_CheckedChanged);
			this.listBoxPort.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.listBoxPort.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.listBoxPort.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.listBoxPort.FormattingEnabled = true;
			this.listBoxPort.Location = new global::System.Drawing.Point(214, 34);
			this.listBoxPort.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 2);
			this.listBoxPort.Name = "listBoxPort";
			this.listBoxPort.Size = new global::System.Drawing.Size(109, 80);
			this.listBoxPort.TabIndex = 65;
			this.txtGroup.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtGroup.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtGroup.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "Group", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtGroup.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtGroup.Location = new global::System.Drawing.Point(262, 206);
			this.txtGroup.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtGroup.Name = "txtGroup";
			this.txtGroup.Size = new global::System.Drawing.Size(110, 20);
			this.txtGroup.TabIndex = 110;
			this.txtGroup.Text = global::Server.Properties.Settings.Default.Group;
			this.txtMutex.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtMutex.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMutex.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "Mutex", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtMutex.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtMutex.Location = new global::System.Drawing.Point(262, 263);
			this.txtMutex.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtMutex.Name = "txtMutex";
			this.txtMutex.Size = new global::System.Drawing.Size(205, 20);
			this.txtMutex.TabIndex = 104;
			this.txtMutex.Text = global::Server.Properties.Settings.Default.Mutex;
			this.textFilename.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.textFilename.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textFilename.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "Filename", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textFilename.Enabled = false;
			this.textFilename.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.textFilename.Location = new global::System.Drawing.Point(549, 302);
			this.textFilename.Margin = new global::System.Windows.Forms.Padding(2);
			this.textFilename.Name = "textFilename";
			this.textFilename.Size = new global::System.Drawing.Size(203, 20);
			this.textFilename.TabIndex = 99;
			this.textFilename.Text = global::Server.Properties.Settings.Default.Filename;
			this.txtFileVersion.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtFileVersion.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFileVersion.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "txtFileVersion", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtFileVersion.Enabled = false;
			this.txtFileVersion.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtFileVersion.Location = new global::System.Drawing.Point(601, 238);
			this.txtFileVersion.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtFileVersion.Name = "txtFileVersion";
			this.txtFileVersion.Size = new global::System.Drawing.Size(151, 20);
			this.txtFileVersion.TabIndex = 88;
			this.txtFileVersion.Text = global::Server.Properties.Settings.Default.txtFileVersion;
			this.txtProductVersion.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtProductVersion.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtProductVersion.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "txtProductVersion", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtProductVersion.Enabled = false;
			this.txtProductVersion.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtProductVersion.Location = new global::System.Drawing.Point(601, 211);
			this.txtProductVersion.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtProductVersion.Name = "txtProductVersion";
			this.txtProductVersion.Size = new global::System.Drawing.Size(151, 20);
			this.txtProductVersion.TabIndex = 87;
			this.txtProductVersion.Text = global::Server.Properties.Settings.Default.txtProductVersion;
			this.txtOriginalFilename.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtOriginalFilename.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOriginalFilename.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "txtOriginalFilename", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtOriginalFilename.Enabled = false;
			this.txtOriginalFilename.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtOriginalFilename.Location = new global::System.Drawing.Point(601, 183);
			this.txtOriginalFilename.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtOriginalFilename.Name = "txtOriginalFilename";
			this.txtOriginalFilename.Size = new global::System.Drawing.Size(151, 20);
			this.txtOriginalFilename.TabIndex = 86;
			this.txtOriginalFilename.Text = global::Server.Properties.Settings.Default.txtOriginalFilename;
			this.txtTrademarks.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtTrademarks.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtTrademarks.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "txtTrademarks", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtTrademarks.Enabled = false;
			this.txtTrademarks.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtTrademarks.Location = new global::System.Drawing.Point(601, 156);
			this.txtTrademarks.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtTrademarks.Name = "txtTrademarks";
			this.txtTrademarks.Size = new global::System.Drawing.Size(151, 20);
			this.txtTrademarks.TabIndex = 85;
			this.txtTrademarks.Text = global::Server.Properties.Settings.Default.txtTrademarks;
			this.txtCopyright.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtCopyright.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCopyright.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "txtCopyright", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtCopyright.Enabled = false;
			this.txtCopyright.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtCopyright.Location = new global::System.Drawing.Point(601, 129);
			this.txtCopyright.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtCopyright.Name = "txtCopyright";
			this.txtCopyright.Size = new global::System.Drawing.Size(151, 20);
			this.txtCopyright.TabIndex = 84;
			this.txtCopyright.Text = global::Server.Properties.Settings.Default.txtCopyright;
			this.txtCompany.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtCompany.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCompany.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "txtCompany", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtCompany.Enabled = false;
			this.txtCompany.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtCompany.Location = new global::System.Drawing.Point(601, 102);
			this.txtCompany.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtCompany.Name = "txtCompany";
			this.txtCompany.Size = new global::System.Drawing.Size(151, 20);
			this.txtCompany.TabIndex = 83;
			this.txtCompany.Text = global::Server.Properties.Settings.Default.txtCompany;
			this.txtDescription.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtDescription.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDescription.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "txtDescription", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtDescription.Enabled = false;
			this.txtDescription.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtDescription.Location = new global::System.Drawing.Point(601, 75);
			this.txtDescription.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new global::System.Drawing.Size(151, 20);
			this.txtDescription.TabIndex = 76;
			this.txtDescription.Text = global::Server.Properties.Settings.Default.txtDescription;
			this.txtProduct.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtProduct.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtProduct.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "ProductName", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtProduct.Enabled = false;
			this.txtProduct.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtProduct.Location = new global::System.Drawing.Point(601, 47);
			this.txtProduct.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtProduct.Name = "txtProduct";
			this.txtProduct.Size = new global::System.Drawing.Size(151, 20);
			this.txtProduct.TabIndex = 74;
			this.txtProduct.Text = global::Server.Properties.Settings.Default.ProductName;
			this.txtPaste_bin.BackColor = global::System.Drawing.Color.FromArgb(69, 73, 74);
			this.txtPaste_bin.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPaste_bin.DataBindings.Add(new global::System.Windows.Forms.Binding("Text", global::Server.Properties.Settings.Default, "Paste_bin", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtPaste_bin.Enabled = false;
			this.txtPaste_bin.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.txtPaste_bin.Location = new global::System.Drawing.Point(17, 206);
			this.txtPaste_bin.Margin = new global::System.Windows.Forms.Padding(2);
			this.txtPaste_bin.Name = "txtPaste_bin";
			this.txtPaste_bin.Size = new global::System.Drawing.Size(140, 20);
			this.txtPaste_bin.TabIndex = 63;
			this.txtPaste_bin.Text = global::Server.Properties.Settings.Default.Paste_bin;
			this.chkAnti.AutoSize = true;
			this.chkAnti.Location = new global::System.Drawing.Point(262, 314);
			this.chkAnti.Name = "chkAnti";
			this.chkAnti.Size = new global::System.Drawing.Size(63, 17);
			this.chkAnti.TabIndex = 111;
			this.chkAnti.Text = "Anti-VM";
			this.chkAntiProcess.AutoSize = true;
			this.chkAntiProcess.Location = new global::System.Drawing.Point(359, 315);
			this.chkAntiProcess.Name = "chkAntiProcess";
			this.chkAntiProcess.Size = new global::System.Drawing.Size(93, 17);
			this.chkAntiProcess.TabIndex = 112;
			this.chkAntiProcess.Text = "Block taskmgr";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(768, 437);
			base.Controls.Add(this.chkAntiProcess);
			base.Controls.Add(this.chkAnti);
			base.Controls.Add(this.txtGroup);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.numDelay);
			base.Controls.Add(this.label16);
			base.Controls.Add(this.chkBsod);
			base.Controls.Add(this.txtMutex);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.comboBoxFolder);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.textFilename);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.btnBuild);
			base.Controls.Add(this.chkIcon);
			base.Controls.Add(this.txtIcon);
			base.Controls.Add(this.btnIcon);
			base.Controls.Add(this.picIcon);
			base.Controls.Add(this.btnClone);
			base.Controls.Add(this.btnAssembly);
			base.Controls.Add(this.txtFileVersion);
			base.Controls.Add(this.txtProductVersion);
			base.Controls.Add(this.txtOriginalFilename);
			base.Controls.Add(this.txtTrademarks);
			base.Controls.Add(this.txtCopyright);
			base.Controls.Add(this.txtCompany);
			base.Controls.Add(this.label14);
			base.Controls.Add(this.label13);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.label11);
			base.Controls.Add(this.label10);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.txtDescription);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.txtProduct);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.btnRemoveIP);
			base.Controls.Add(this.btnAddIP);
			base.Controls.Add(this.textIP);
			base.Controls.Add(this.listBoxIP);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textPort);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnRemovePort);
			base.Controls.Add(this.txtPaste_bin);
			base.Controls.Add(this.btnAddPort);
			base.Controls.Add(this.chkPaste_bin);
			base.Controls.Add(this.listBoxPort);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.Name = "FormBuilder";
			base.ShowIcon = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Builder";
			base.Load += new global::System.EventHandler(this.Builder_Load);
			((global::System.ComponentModel.ISupportInitialize)this.numDelay).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.picIcon).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001ED RID: 493
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040001EE RID: 494
		private global::System.DirectoryServices.DirectoryEntry directoryEntry1;

		// Token: 0x040001EF RID: 495
		private global::System.Windows.Forms.ToolTip toolTip1;

		// Token: 0x040001F0 RID: 496
		private global::System.Windows.Forms.PictureBox picIcon;

		// Token: 0x040001F1 RID: 497
		private global::System.Windows.Forms.ListBox listBoxIP;

		// Token: 0x040001F2 RID: 498
		private global::System.Windows.Forms.ListBox listBoxPort;

		// Token: 0x040001F3 RID: 499
		private global::DarkUI.Controls.DarkTextBox txtGroup;

		// Token: 0x040001F4 RID: 500
		private global::DarkUI.Controls.DarkLabel label17;

		// Token: 0x040001F5 RID: 501
		private global::DarkUI.Controls.DarkNumericUpDown numDelay;

		// Token: 0x040001F6 RID: 502
		private global::DarkUI.Controls.DarkLabel label16;

		// Token: 0x040001F7 RID: 503
		private global::DarkUI.Controls.DarkCheckBox chkBsod;

		// Token: 0x040001F8 RID: 504
		private global::DarkUI.Controls.DarkTextBox txtMutex;

		// Token: 0x040001F9 RID: 505
		private global::DarkUI.Controls.DarkLabel label5;

		// Token: 0x040001FA RID: 506
		private global::DarkUI.Controls.DarkComboBox comboBoxFolder;

		// Token: 0x040001FB RID: 507
		private global::DarkUI.Controls.DarkCheckBox checkBox1;

		// Token: 0x040001FC RID: 508
		private global::DarkUI.Controls.DarkTextBox textFilename;

		// Token: 0x040001FD RID: 509
		private global::DarkUI.Controls.DarkLabel label3;

		// Token: 0x040001FE RID: 510
		private global::DarkUI.Controls.DarkLabel label4;

		// Token: 0x040001FF RID: 511
		private global::DarkUI.Controls.DarkButton btnBuild;

		// Token: 0x04000200 RID: 512
		private global::DarkUI.Controls.DarkCheckBox chkIcon;

		// Token: 0x04000201 RID: 513
		private global::DarkUI.Controls.DarkTextBox txtIcon;

		// Token: 0x04000202 RID: 514
		private global::DarkUI.Controls.DarkButton btnIcon;

		// Token: 0x04000203 RID: 515
		private global::DarkUI.Controls.DarkButton btnClone;

		// Token: 0x04000204 RID: 516
		private global::DarkUI.Controls.DarkCheckBox btnAssembly;

		// Token: 0x04000205 RID: 517
		private global::DarkUI.Controls.DarkTextBox txtFileVersion;

		// Token: 0x04000206 RID: 518
		private global::DarkUI.Controls.DarkTextBox txtProductVersion;

		// Token: 0x04000207 RID: 519
		private global::DarkUI.Controls.DarkTextBox txtOriginalFilename;

		// Token: 0x04000208 RID: 520
		private global::DarkUI.Controls.DarkTextBox txtTrademarks;

		// Token: 0x04000209 RID: 521
		private global::DarkUI.Controls.DarkTextBox txtCopyright;

		// Token: 0x0400020A RID: 522
		private global::DarkUI.Controls.DarkTextBox txtCompany;

		// Token: 0x0400020B RID: 523
		private global::DarkUI.Controls.DarkLabel label14;

		// Token: 0x0400020C RID: 524
		private global::DarkUI.Controls.DarkLabel label13;

		// Token: 0x0400020D RID: 525
		private global::DarkUI.Controls.DarkLabel label12;

		// Token: 0x0400020E RID: 526
		private global::DarkUI.Controls.DarkLabel label11;

		// Token: 0x0400020F RID: 527
		private global::DarkUI.Controls.DarkLabel label10;

		// Token: 0x04000210 RID: 528
		private global::DarkUI.Controls.DarkLabel label9;

		// Token: 0x04000211 RID: 529
		private global::DarkUI.Controls.DarkTextBox txtDescription;

		// Token: 0x04000212 RID: 530
		private global::DarkUI.Controls.DarkLabel label7;

		// Token: 0x04000213 RID: 531
		private global::DarkUI.Controls.DarkTextBox txtProduct;

		// Token: 0x04000214 RID: 532
		private global::DarkUI.Controls.DarkLabel label8;

		// Token: 0x04000215 RID: 533
		private global::DarkUI.Controls.DarkButton btnRemoveIP;

		// Token: 0x04000216 RID: 534
		private global::DarkUI.Controls.DarkButton btnAddIP;

		// Token: 0x04000217 RID: 535
		private global::DarkUI.Controls.DarkTextBox textIP;

		// Token: 0x04000218 RID: 536
		private global::DarkUI.Controls.DarkLabel label2;

		// Token: 0x04000219 RID: 537
		private global::DarkUI.Controls.DarkTextBox textPort;

		// Token: 0x0400021A RID: 538
		private global::DarkUI.Controls.DarkLabel label1;

		// Token: 0x0400021B RID: 539
		private global::DarkUI.Controls.DarkButton btnRemovePort;

		// Token: 0x0400021C RID: 540
		private global::DarkUI.Controls.DarkTextBox txtPaste_bin;

		// Token: 0x0400021D RID: 541
		private global::DarkUI.Controls.DarkButton btnAddPort;

		// Token: 0x0400021E RID: 542
		private global::DarkUI.Controls.DarkCheckBox chkPaste_bin;

		// Token: 0x0400021F RID: 543
		private global::DarkUI.Controls.DarkCheckBox chkAnti;

		// Token: 0x04000220 RID: 544
		private global::DarkUI.Controls.DarkCheckBox chkAntiProcess;
	}
}
