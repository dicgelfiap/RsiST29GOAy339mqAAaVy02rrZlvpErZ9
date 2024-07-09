using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003CE RID: 974
	public class OpenBsdBCrypt
	{
		// Token: 0x06001EBF RID: 7871 RVA: 0x000B4AC8 File Offset: 0x000B4AC8
		static OpenBsdBCrypt()
		{
			OpenBsdBCrypt.AllowedVersions.Add("2a");
			OpenBsdBCrypt.AllowedVersions.Add("2y");
			OpenBsdBCrypt.AllowedVersions.Add("2b");
			for (int i = 0; i < OpenBsdBCrypt.DecodingTable.Length; i++)
			{
				OpenBsdBCrypt.DecodingTable[i] = byte.MaxValue;
			}
			for (int j = 0; j < OpenBsdBCrypt.EncodingTable.Length; j++)
			{
				OpenBsdBCrypt.DecodingTable[(int)OpenBsdBCrypt.EncodingTable[j]] = (byte)j;
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000B4B90 File Offset: 0x000B4B90
		private static string CreateBcryptString(string version, byte[] password, byte[] salt, int cost)
		{
			if (!OpenBsdBCrypt.AllowedVersions.Contains(version))
			{
				throw new ArgumentException("Version " + version + " is not accepted by this implementation.", "version");
			}
			StringBuilder stringBuilder = new StringBuilder(60);
			stringBuilder.Append('$');
			stringBuilder.Append(version);
			stringBuilder.Append('$');
			stringBuilder.Append((cost < 10) ? ("0" + cost) : cost.ToString());
			stringBuilder.Append('$');
			stringBuilder.Append(OpenBsdBCrypt.EncodeData(salt));
			byte[] data = BCrypt.Generate(password, salt, cost);
			stringBuilder.Append(OpenBsdBCrypt.EncodeData(data));
			return stringBuilder.ToString();
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000B4C4C File Offset: 0x000B4C4C
		public static string Generate(char[] password, byte[] salt, int cost)
		{
			return OpenBsdBCrypt.Generate(OpenBsdBCrypt.DefaultVersion, password, salt, cost);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000B4C5C File Offset: 0x000B4C5C
		public static string Generate(string version, char[] password, byte[] salt, int cost)
		{
			if (!OpenBsdBCrypt.AllowedVersions.Contains(version))
			{
				throw new ArgumentException("Version " + version + " is not accepted by this implementation.", "version");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt");
			}
			if (salt.Length != 16)
			{
				throw new DataLengthException("16 byte salt required: " + salt.Length);
			}
			if (cost < 4 || cost > 31)
			{
				throw new ArgumentException("Invalid cost factor.", "cost");
			}
			byte[] array = Strings.ToUtf8ByteArray(password);
			byte[] array2 = new byte[(array.Length >= 72) ? 72 : (array.Length + 1)];
			int length = Math.Min(array.Length, array2.Length);
			Array.Copy(array, 0, array2, 0, length);
			Array.Clear(array, 0, array.Length);
			string result = OpenBsdBCrypt.CreateBcryptString(version, array2, salt, cost);
			Array.Clear(array2, 0, array2.Length);
			return result;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000B4D54 File Offset: 0x000B4D54
		public static bool CheckPassword(string bcryptString, char[] password)
		{
			if (bcryptString.Length != 60)
			{
				throw new DataLengthException("Bcrypt String length: " + bcryptString.Length + ", 60 required.");
			}
			if (bcryptString[0] != '$' || bcryptString[3] != '$' || bcryptString[6] != '$')
			{
				throw new ArgumentException("Invalid Bcrypt String format.", "bcryptString");
			}
			string text = bcryptString.Substring(1, 2);
			if (!OpenBsdBCrypt.AllowedVersions.Contains(text))
			{
				throw new ArgumentException("Bcrypt version '" + text + "' is not supported by this implementation", "bcryptString");
			}
			int num = 0;
			try
			{
				num = int.Parse(bcryptString.Substring(4, 2));
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Invalid cost factor: " + bcryptString.Substring(4, 2), "bcryptString", innerException);
			}
			if (num < 4 || num > 31)
			{
				throw new ArgumentException("Invalid cost factor: " + num + ", 4 < cost < 31 expected.");
			}
			if (password == null)
			{
				throw new ArgumentNullException("Missing password.");
			}
			int num2 = bcryptString.LastIndexOf('$') + 1;
			int num3 = bcryptString.Length - 31;
			byte[] salt = OpenBsdBCrypt.DecodeSaltString(bcryptString.Substring(num2, num3 - num2));
			string value = OpenBsdBCrypt.Generate(text, password, salt, num);
			return bcryptString.Equals(value);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000B4EB8 File Offset: 0x000B4EB8
		private static string EncodeData(byte[] data)
		{
			if (data.Length != 24 && data.Length != 16)
			{
				throw new DataLengthException("Invalid length: " + data.Length + ", 24 for key or 16 for salt expected");
			}
			bool flag = false;
			if (data.Length == 16)
			{
				flag = true;
				byte[] array = new byte[18];
				Array.Copy(data, 0, array, 0, data.Length);
				data = array;
			}
			else
			{
				data[data.Length - 1] = 0;
			}
			MemoryStream memoryStream = new MemoryStream();
			int num = data.Length;
			for (int i = 0; i < num; i += 3)
			{
				uint num2 = (uint)data[i];
				uint num3 = (uint)data[i + 1];
				uint num4 = (uint)data[i + 2];
				memoryStream.WriteByte(OpenBsdBCrypt.EncodingTable[(int)((UIntPtr)(num2 >> 2 & 63U))]);
				memoryStream.WriteByte(OpenBsdBCrypt.EncodingTable[(int)((UIntPtr)((num2 << 4 | num3 >> 4) & 63U))]);
				memoryStream.WriteByte(OpenBsdBCrypt.EncodingTable[(int)((UIntPtr)((num3 << 2 | num4 >> 6) & 63U))]);
				memoryStream.WriteByte(OpenBsdBCrypt.EncodingTable[(int)((UIntPtr)(num4 & 63U))]);
			}
			string text = Strings.FromByteArray(memoryStream.ToArray());
			int length = flag ? 22 : (text.Length - 1);
			return text.Substring(0, length);
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000B4FE8 File Offset: 0x000B4FE8
		private static byte[] DecodeSaltString(string saltString)
		{
			char[] array = saltString.ToCharArray();
			MemoryStream memoryStream = new MemoryStream(16);
			if (array.Length != 22)
			{
				throw new DataLengthException("Invalid base64 salt length: " + array.Length + " , 22 required.");
			}
			foreach (int num in array)
			{
				if (num > 122 || num < 46 || (num > 57 && num < 65))
				{
					throw new ArgumentException("Salt string contains invalid character: " + num, "saltString");
				}
			}
			char[] array2 = new char[24];
			Array.Copy(array, 0, array2, 0, array.Length);
			array = array2;
			int num2 = array.Length;
			for (int j = 0; j < num2; j += 4)
			{
				byte b = OpenBsdBCrypt.DecodingTable[(int)array[j]];
				byte b2 = OpenBsdBCrypt.DecodingTable[(int)array[j + 1]];
				byte b3 = OpenBsdBCrypt.DecodingTable[(int)array[j + 2]];
				byte b4 = OpenBsdBCrypt.DecodingTable[(int)array[j + 3]];
				memoryStream.WriteByte((byte)((int)b << 2 | b2 >> 4));
				memoryStream.WriteByte((byte)((int)b2 << 4 | b3 >> 2));
				memoryStream.WriteByte((byte)((int)b3 << 6 | (int)b4));
			}
			byte[] sourceArray = memoryStream.ToArray();
			byte[] array3 = new byte[16];
			Array.Copy(sourceArray, 0, array3, 0, array3.Length);
			return array3;
		}

		// Token: 0x04001463 RID: 5219
		private static readonly byte[] EncodingTable = new byte[]
		{
			46,
			47,
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57
		};

		// Token: 0x04001464 RID: 5220
		private static readonly byte[] DecodingTable = new byte[128];

		// Token: 0x04001465 RID: 5221
		private static readonly string DefaultVersion = "2y";

		// Token: 0x04001466 RID: 5222
		private static readonly ISet AllowedVersions = new HashSet();
	}
}
