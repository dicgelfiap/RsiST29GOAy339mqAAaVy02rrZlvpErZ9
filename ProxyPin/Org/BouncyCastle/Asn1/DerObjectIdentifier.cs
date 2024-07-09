using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020001EE RID: 494
	public class DerObjectIdentifier : Asn1Object
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x0005DC5C File Offset: 0x0005DC5C
		public static DerObjectIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is DerObjectIdentifier)
			{
				return (DerObjectIdentifier)obj;
			}
			if (obj is Asn1Encodable)
			{
				Asn1Object asn1Object = ((Asn1Encodable)obj).ToAsn1Object();
				if (asn1Object is DerObjectIdentifier)
				{
					return (DerObjectIdentifier)asn1Object;
				}
			}
			if (obj is byte[])
			{
				return DerObjectIdentifier.FromOctetString((byte[])obj);
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0005DCE0 File Offset: 0x0005DCE0
		public static DerObjectIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			Asn1Object @object = obj.GetObject();
			if (explicitly || @object is DerObjectIdentifier)
			{
				return DerObjectIdentifier.GetInstance(@object);
			}
			return DerObjectIdentifier.FromOctetString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0005DD20 File Offset: 0x0005DD20
		public DerObjectIdentifier(string identifier)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			if (!DerObjectIdentifier.IsValidIdentifier(identifier))
			{
				throw new FormatException("string " + identifier + " not an OID");
			}
			this.identifier = identifier;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0005DD78 File Offset: 0x0005DD78
		internal DerObjectIdentifier(DerObjectIdentifier oid, string branchID)
		{
			if (!DerObjectIdentifier.IsValidBranchID(branchID, 0))
			{
				throw new ArgumentException("string " + branchID + " not a valid OID branch", "branchID");
			}
			this.identifier = oid.Id + "." + branchID;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0005DDD4 File Offset: 0x0005DDD4
		public string Id
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0005DDDC File Offset: 0x0005DDDC
		public virtual DerObjectIdentifier Branch(string branchID)
		{
			return new DerObjectIdentifier(this, branchID);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0005DDE8 File Offset: 0x0005DDE8
		public virtual bool On(DerObjectIdentifier stem)
		{
			string id = this.Id;
			string id2 = stem.Id;
			return id.Length > id2.Length && id[id2.Length] == '.' && Platform.StartsWith(id, id2);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0005DE34 File Offset: 0x0005DE34
		internal DerObjectIdentifier(byte[] bytes)
		{
			this.identifier = DerObjectIdentifier.MakeOidStringFromBytes(bytes);
			this.body = Arrays.Clone(bytes);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0005DE5C File Offset: 0x0005DE5C
		private void WriteField(Stream outputStream, long fieldValue)
		{
			byte[] array = new byte[9];
			int num = 8;
			array[num] = (byte)(fieldValue & 127L);
			while (fieldValue >= 128L)
			{
				fieldValue >>= 7;
				array[--num] = (byte)((fieldValue & 127L) | 128L);
			}
			outputStream.Write(array, num, 9 - num);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0005DEB4 File Offset: 0x0005DEB4
		private void WriteField(Stream outputStream, BigInteger fieldValue)
		{
			int num = (fieldValue.BitLength + 6) / 7;
			if (num == 0)
			{
				outputStream.WriteByte(0);
				return;
			}
			BigInteger bigInteger = fieldValue;
			byte[] array = new byte[num];
			for (int i = num - 1; i >= 0; i--)
			{
				array[i] = (byte)((bigInteger.IntValue & 127) | 128);
				bigInteger = bigInteger.ShiftRight(7);
			}
			byte[] array2;
			IntPtr intPtr;
			(array2 = array)[(int)(intPtr = (IntPtr)(num - 1))] = (array2[(int)intPtr] & 127);
			outputStream.Write(array, 0, array.Length);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0005DF34 File Offset: 0x0005DF34
		private void DoOutput(MemoryStream bOut)
		{
			OidTokenizer oidTokenizer = new OidTokenizer(this.identifier);
			string text = oidTokenizer.NextToken();
			int num = int.Parse(text) * 40;
			text = oidTokenizer.NextToken();
			if (text.Length <= 18)
			{
				this.WriteField(bOut, (long)num + long.Parse(text));
			}
			else
			{
				this.WriteField(bOut, new BigInteger(text).Add(BigInteger.ValueOf((long)num)));
			}
			while (oidTokenizer.HasMoreTokens)
			{
				text = oidTokenizer.NextToken();
				if (text.Length <= 18)
				{
					this.WriteField(bOut, long.Parse(text));
				}
				else
				{
					this.WriteField(bOut, new BigInteger(text));
				}
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0005DFE4 File Offset: 0x0005DFE4
		internal byte[] GetBody()
		{
			lock (this)
			{
				if (this.body == null)
				{
					MemoryStream memoryStream = new MemoryStream();
					this.DoOutput(memoryStream);
					this.body = memoryStream.ToArray();
				}
			}
			return this.body;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0005E040 File Offset: 0x0005E040
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(6, this.GetBody());
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0005E050 File Offset: 0x0005E050
		protected override int Asn1GetHashCode()
		{
			return this.identifier.GetHashCode();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0005E060 File Offset: 0x0005E060
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerObjectIdentifier derObjectIdentifier = asn1Object as DerObjectIdentifier;
			return derObjectIdentifier != null && this.identifier.Equals(derObjectIdentifier.identifier);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0005E094 File Offset: 0x0005E094
		public override string ToString()
		{
			return this.identifier;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0005E09C File Offset: 0x0005E09C
		private static bool IsValidBranchID(string branchID, int start)
		{
			int num = 0;
			int num2 = branchID.Length;
			while (--num2 >= start)
			{
				char c = branchID[num2];
				if (c == '.')
				{
					if (num == 0 || (num > 1 && branchID[num2 + 1] == '0'))
					{
						return false;
					}
					num = 0;
				}
				else
				{
					if ('0' > c || c > '9')
					{
						return false;
					}
					num++;
				}
			}
			return num != 0 && (num <= 1 || branchID[num2 + 1] != '0');
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0005E134 File Offset: 0x0005E134
		private static bool IsValidIdentifier(string identifier)
		{
			if (identifier.Length < 3 || identifier[1] != '.')
			{
				return false;
			}
			char c = identifier[0];
			return c >= '0' && c <= '2' && DerObjectIdentifier.IsValidBranchID(identifier, 2);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0005E184 File Offset: 0x0005E184
		private static string MakeOidStringFromBytes(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			long num = 0L;
			BigInteger bigInteger = null;
			bool flag = true;
			for (int num2 = 0; num2 != bytes.Length; num2++)
			{
				int num3 = (int)bytes[num2];
				if (num <= 72057594037927808L)
				{
					num += (long)(num3 & 127);
					if ((num3 & 128) == 0)
					{
						if (flag)
						{
							if (num < 40L)
							{
								stringBuilder.Append('0');
							}
							else if (num < 80L)
							{
								stringBuilder.Append('1');
								num -= 40L;
							}
							else
							{
								stringBuilder.Append('2');
								num -= 80L;
							}
							flag = false;
						}
						stringBuilder.Append('.');
						stringBuilder.Append(num);
						num = 0L;
					}
					else
					{
						num <<= 7;
					}
				}
				else
				{
					if (bigInteger == null)
					{
						bigInteger = BigInteger.ValueOf(num);
					}
					bigInteger = bigInteger.Or(BigInteger.ValueOf((long)(num3 & 127)));
					if ((num3 & 128) == 0)
					{
						if (flag)
						{
							stringBuilder.Append('2');
							bigInteger = bigInteger.Subtract(BigInteger.ValueOf(80L));
							flag = false;
						}
						stringBuilder.Append('.');
						stringBuilder.Append(bigInteger);
						bigInteger = null;
						num = 0L;
					}
					else
					{
						bigInteger = bigInteger.ShiftLeft(7);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0005E2C8 File Offset: 0x0005E2C8
		internal static DerObjectIdentifier FromOctetString(byte[] enc)
		{
			int hashCode = Arrays.GetHashCode(enc);
			int num = hashCode & 1023;
			DerObjectIdentifier result;
			lock (DerObjectIdentifier.cache)
			{
				DerObjectIdentifier derObjectIdentifier = DerObjectIdentifier.cache[num];
				if (derObjectIdentifier != null && Arrays.AreEqual(enc, derObjectIdentifier.GetBody()))
				{
					result = derObjectIdentifier;
				}
				else
				{
					result = (DerObjectIdentifier.cache[num] = new DerObjectIdentifier(enc));
				}
			}
			return result;
		}

		// Token: 0x04000BB9 RID: 3001
		private const long LONG_LIMIT = 72057594037927808L;

		// Token: 0x04000BBA RID: 3002
		private readonly string identifier;

		// Token: 0x04000BBB RID: 3003
		private byte[] body = null;

		// Token: 0x04000BBC RID: 3004
		private static readonly DerObjectIdentifier[] cache = new DerObjectIdentifier[1024];
	}
}
