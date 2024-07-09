using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace CrackedAuth
{
	// Token: 0x0200000B RID: 11
	internal class Auth
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003170 File Offset: 0x00003170
		public static int login(string KEY)
		{
			if (!KEY.Contains("CRACKED"))
			{
				return 0;
			}
			string text = null;
			if (string.IsNullOrEmpty(text))
			{
				foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
				{
					if (driveInfo.IsReady)
					{
						text = driveInfo.RootDirectory.ToString();
						break;
					}
				}
			}
			if (!string.IsNullOrEmpty(text) && text.EndsWith(":\\"))
			{
				text = text.Substring(0, text.Length - 2);
			}
			string str;
			using (ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + text + ":\""))
			{
				managementObject.Get();
				str = managementObject["VolumeSerialNumber"].ToString();
			}
			HttpWebRequest httpWebRequest = WebRequest.Create("https://cracked.io/auth.php") as HttpWebRequest;
			httpWebRequest.Proxy = null;
			HttpWebRequest httpWebRequest2 = httpWebRequest;
			httpWebRequest2.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(httpWebRequest2.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback((object obj, X509Certificate cert, X509Chain ssl, SslPolicyErrors error) => (cert as X509Certificate2).Verify()));
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.Method = "POST";
			byte[] bytes = Encoding.UTF8.GetBytes("a=auth&k=" + KEY + "&hwid=" + str);
			httpWebRequest.ContentLength = (long)bytes.Length;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
			}
			string json = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream()).ReadToEnd();
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(json);
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			if (dictionary.ContainsKey("error"))
			{
				if (File.Exists("key.cto"))
				{
					File.Delete("key.cto");
				}
				MessageBox.Show(textInfo.ToTitleCase(dictionary["error"] as string) + ".\n\n");
				return 0;
			}
			if (!new string[]
			{
				"12",
				"96",
				"97",
				"99",
				"100",
				"101",
				"4",
				"3",
				"6",
				"94",
				"92"
			}.Any((string t) => t == dictionary["group"] as string))
			{
				Process.Start("http://cracked.io/upgrade.php");
				if (File.Exists("key.cto"))
				{
					File.Delete("key.cto");
				}
				MessageBox.Show("You have to be at least Supreme+ to be able to use this tool. Press any key to continue to the forum page");
				return 0;
			}
			MessageBox.Show("Auth Granted. Welcome " + (dictionary["username"] as string) + "\n\n");
			File.WriteAllText("key.cto", KEY);
			return 1;
		}
	}
}
