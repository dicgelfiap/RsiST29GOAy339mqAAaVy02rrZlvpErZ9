using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Net;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F9 RID: 505
	public class GeneralName : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001044 RID: 4164 RVA: 0x0005F090 File Offset: 0x0005F090
		public GeneralName(X509Name directoryName)
		{
			this.obj = directoryName;
			this.tag = 4;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0005F0A8 File Offset: 0x0005F0A8
		public GeneralName(Asn1Object name, int tag)
		{
			this.obj = name;
			this.tag = tag;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0005F0C0 File Offset: 0x0005F0C0
		public GeneralName(int tag, Asn1Encodable name)
		{
			this.obj = name;
			this.tag = tag;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0005F0D8 File Offset: 0x0005F0D8
		public GeneralName(int tag, string name)
		{
			this.tag = tag;
			if (tag == 1 || tag == 2 || tag == 6)
			{
				this.obj = new DerIA5String(name);
				return;
			}
			if (tag == 8)
			{
				this.obj = new DerObjectIdentifier(name);
				return;
			}
			if (tag == 4)
			{
				this.obj = new X509Name(name);
				return;
			}
			if (tag != 7)
			{
				throw new ArgumentException("can't process string for tag: " + tag, "tag");
			}
			byte[] array = this.toGeneralNameEncoding(name);
			if (array == null)
			{
				throw new ArgumentException("IP Address is invalid", "name");
			}
			this.obj = new DerOctetString(array);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0005F18C File Offset: 0x0005F18C
		public static GeneralName GetInstance(object obj)
		{
			if (obj == null || obj is GeneralName)
			{
				return (GeneralName)obj;
			}
			if (!(obj is Asn1TaggedObject))
			{
				if (obj is byte[])
				{
					try
					{
						return GeneralName.GetInstance(Asn1Object.FromByteArray((byte[])obj));
					}
					catch (IOException)
					{
						throw new ArgumentException("unable to parse encoded general name");
					}
				}
				throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
			}
			Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
			int tagNo = asn1TaggedObject.TagNo;
			switch (tagNo)
			{
			case 0:
			case 3:
			case 5:
				return new GeneralName(tagNo, Asn1Sequence.GetInstance(asn1TaggedObject, false));
			case 1:
			case 2:
			case 6:
				return new GeneralName(tagNo, DerIA5String.GetInstance(asn1TaggedObject, false));
			case 4:
				return new GeneralName(tagNo, X509Name.GetInstance(asn1TaggedObject, true));
			case 7:
				return new GeneralName(tagNo, Asn1OctetString.GetInstance(asn1TaggedObject, false));
			case 8:
				return new GeneralName(tagNo, DerObjectIdentifier.GetInstance(asn1TaggedObject, false));
			default:
				throw new ArgumentException("unknown tag: " + tagNo);
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0005F2B0 File Offset: 0x0005F2B0
		public static GeneralName GetInstance(Asn1TaggedObject tagObj, bool explicitly)
		{
			return GeneralName.GetInstance(Asn1TaggedObject.GetInstance(tagObj, true));
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0005F2C0 File Offset: 0x0005F2C0
		public int TagNo
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0005F2C8 File Offset: 0x0005F2C8
		public Asn1Encodable Name
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0005F2D0 File Offset: 0x0005F2D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.tag);
			stringBuilder.Append(": ");
			switch (this.tag)
			{
			case 1:
			case 2:
			case 6:
				stringBuilder.Append(DerIA5String.GetInstance(this.obj).GetString());
				goto IL_95;
			case 4:
				stringBuilder.Append(X509Name.GetInstance(this.obj).ToString());
				goto IL_95;
			}
			stringBuilder.Append(this.obj.ToString());
			IL_95:
			return stringBuilder.ToString();
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0005F37C File Offset: 0x0005F37C
		private byte[] toGeneralNameEncoding(string ip)
		{
			if (Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv6WithNetmask(ip) || Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv6(ip))
			{
				int num = ip.IndexOf('/');
				if (num < 0)
				{
					byte[] array = new byte[16];
					int[] parsedIp = this.parseIPv6(ip);
					this.copyInts(parsedIp, array, 0);
					return array;
				}
				byte[] array2 = new byte[32];
				int[] parsedIp2 = this.parseIPv6(ip.Substring(0, num));
				this.copyInts(parsedIp2, array2, 0);
				string text = ip.Substring(num + 1);
				if (text.IndexOf(':') > 0)
				{
					parsedIp2 = this.parseIPv6(text);
				}
				else
				{
					parsedIp2 = this.parseMask(text);
				}
				this.copyInts(parsedIp2, array2, 16);
				return array2;
			}
			else
			{
				if (!Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv4WithNetmask(ip) && !Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv4(ip))
				{
					return null;
				}
				int num2 = ip.IndexOf('/');
				if (num2 < 0)
				{
					byte[] array3 = new byte[4];
					this.parseIPv4(ip, array3, 0);
					return array3;
				}
				byte[] array4 = new byte[8];
				this.parseIPv4(ip.Substring(0, num2), array4, 0);
				string text2 = ip.Substring(num2 + 1);
				if (text2.IndexOf('.') > 0)
				{
					this.parseIPv4(text2, array4, 4);
				}
				else
				{
					this.parseIPv4Mask(text2, array4, 4);
				}
				return array4;
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0005F4C4 File Offset: 0x0005F4C4
		private void parseIPv4Mask(string mask, byte[] addr, int offset)
		{
			int num = int.Parse(mask);
			for (int num2 = 0; num2 != num; num2++)
			{
				IntPtr intPtr;
				addr[(int)(intPtr = (IntPtr)(num2 / 8 + offset))] = (addr[(int)intPtr] | (byte)(1 << num2 % 8));
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0005F504 File Offset: 0x0005F504
		private void parseIPv4(string ip, byte[] addr, int offset)
		{
			foreach (string s in ip.Split(new char[]
			{
				'.',
				'/'
			}))
			{
				addr[offset++] = (byte)int.Parse(s);
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0005F558 File Offset: 0x0005F558
		private int[] parseMask(string mask)
		{
			int[] array = new int[8];
			int num = int.Parse(mask);
			for (int num2 = 0; num2 != num; num2++)
			{
				int[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)(num2 / 16))] = (array2[(int)intPtr] | 1 << num2 % 16);
			}
			return array;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0005F5A0 File Offset: 0x0005F5A0
		private void copyInts(int[] parsedIp, byte[] addr, int offSet)
		{
			for (int num = 0; num != parsedIp.Length; num++)
			{
				addr[num * 2 + offSet] = (byte)(parsedIp[num] >> 8);
				addr[num * 2 + 1 + offSet] = (byte)parsedIp[num];
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0005F5DC File Offset: 0x0005F5DC
		private int[] parseIPv6(string ip)
		{
			if (Platform.StartsWith(ip, "::"))
			{
				ip = ip.Substring(1);
			}
			else if (Platform.EndsWith(ip, "::"))
			{
				ip = ip.Substring(0, ip.Length - 1);
			}
			IEnumerator enumerator = ip.Split(new char[]
			{
				':'
			}).GetEnumerator();
			int num = 0;
			int[] array = new int[8];
			int num2 = -1;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = (string)obj;
				if (text.Length == 0)
				{
					num2 = num;
					array[num++] = 0;
				}
				else if (text.IndexOf('.') < 0)
				{
					array[num++] = int.Parse(text, NumberStyles.AllowHexSpecifier);
				}
				else
				{
					string[] array2 = text.Split(new char[]
					{
						'.'
					});
					array[num++] = (int.Parse(array2[0]) << 8 | int.Parse(array2[1]));
					array[num++] = (int.Parse(array2[2]) << 8 | int.Parse(array2[3]));
				}
			}
			if (num != array.Length)
			{
				Array.Copy(array, num2, array, array.Length - (num - num2), num - num2);
				for (int num3 = num2; num3 != array.Length - (num - num2); num3++)
				{
					array[num3] = 0;
				}
			}
			return array;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0005F750 File Offset: 0x0005F750
		public override Asn1Object ToAsn1Object()
		{
			bool explicitly = this.tag == 4;
			return new DerTaggedObject(explicitly, this.tag, this.obj);
		}

		// Token: 0x04000BDE RID: 3038
		public const int OtherName = 0;

		// Token: 0x04000BDF RID: 3039
		public const int Rfc822Name = 1;

		// Token: 0x04000BE0 RID: 3040
		public const int DnsName = 2;

		// Token: 0x04000BE1 RID: 3041
		public const int X400Address = 3;

		// Token: 0x04000BE2 RID: 3042
		public const int DirectoryName = 4;

		// Token: 0x04000BE3 RID: 3043
		public const int EdiPartyName = 5;

		// Token: 0x04000BE4 RID: 3044
		public const int UniformResourceIdentifier = 6;

		// Token: 0x04000BE5 RID: 3045
		public const int IPAddress = 7;

		// Token: 0x04000BE6 RID: 3046
		public const int RegisteredID = 8;

		// Token: 0x04000BE7 RID: 3047
		internal readonly Asn1Encodable obj;

		// Token: 0x04000BE8 RID: 3048
		internal readonly int tag;
	}
}
