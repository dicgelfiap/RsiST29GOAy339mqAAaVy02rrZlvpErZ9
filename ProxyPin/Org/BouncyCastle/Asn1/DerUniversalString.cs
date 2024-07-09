using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000276 RID: 630
	public class DerUniversalString : DerStringBase
	{
		// Token: 0x060013FC RID: 5116 RVA: 0x0006C4E0 File Offset: 0x0006C4E0
		public static DerUniversalString GetInstance(object obj)
		{
			if (obj == null || obj is DerUniversalString)
			{
				return (DerUniversalString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0006C510 File Offset: 0x0006C510
		public static DerUniversalString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerUniversalString)
			{
				return DerUniversalString.GetInstance(@object);
			}
			return new DerUniversalString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0006C550 File Offset: 0x0006C550
		public DerUniversalString(byte[] str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0006C570 File Offset: 0x0006C570
		public override string GetString()
		{
			StringBuilder stringBuilder = new StringBuilder("#");
			byte[] derEncoded = base.GetDerEncoded();
			for (int num = 0; num != derEncoded.Length; num++)
			{
				uint num2 = (uint)derEncoded[num];
				stringBuilder.Append(DerUniversalString.table[(int)((UIntPtr)(num2 >> 4 & 15U))]);
				stringBuilder.Append(DerUniversalString.table[(int)(derEncoded[num] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0006C5D8 File Offset: 0x0006C5D8
		public byte[] GetOctets()
		{
			return (byte[])this.str.Clone();
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0006C5EC File Offset: 0x0006C5EC
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(28, this.str);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0006C5FC File Offset: 0x0006C5FC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUniversalString derUniversalString = asn1Object as DerUniversalString;
			return derUniversalString != null && Arrays.AreEqual(this.str, derUniversalString.str);
		}

		// Token: 0x04000DBE RID: 3518
		private static readonly char[] table = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x04000DBF RID: 3519
		private readonly byte[] str;
	}
}
