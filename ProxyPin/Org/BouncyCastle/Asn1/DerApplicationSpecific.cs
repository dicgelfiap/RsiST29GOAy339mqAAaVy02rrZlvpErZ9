using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000249 RID: 585
	public class DerApplicationSpecific : Asn1Object
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x000695EC File Offset: 0x000695EC
		internal DerApplicationSpecific(bool isConstructed, int tag, byte[] octets)
		{
			this.isConstructed = isConstructed;
			this.tag = tag;
			this.octets = octets;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0006960C File Offset: 0x0006960C
		public DerApplicationSpecific(int tag, byte[] octets) : this(false, tag, octets)
		{
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00069618 File Offset: 0x00069618
		public DerApplicationSpecific(int tag, Asn1Encodable obj) : this(true, tag, obj)
		{
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00069624 File Offset: 0x00069624
		public DerApplicationSpecific(bool isExplicit, int tag, Asn1Encodable obj)
		{
			Asn1Object asn1Object = obj.ToAsn1Object();
			byte[] derEncoded = asn1Object.GetDerEncoded();
			this.isConstructed = Asn1TaggedObject.IsConstructed(isExplicit, asn1Object);
			this.tag = tag;
			if (isExplicit)
			{
				this.octets = derEncoded;
				return;
			}
			int lengthOfHeader = this.GetLengthOfHeader(derEncoded);
			byte[] array = new byte[derEncoded.Length - lengthOfHeader];
			Array.Copy(derEncoded, lengthOfHeader, array, 0, array.Length);
			this.octets = array;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00069694 File Offset: 0x00069694
		public DerApplicationSpecific(int tagNo, Asn1EncodableVector vec)
		{
			this.tag = tagNo;
			this.isConstructed = true;
			MemoryStream memoryStream = new MemoryStream();
			for (int num = 0; num != vec.Count; num++)
			{
				try
				{
					byte[] derEncoded = vec[num].GetDerEncoded();
					memoryStream.Write(derEncoded, 0, derEncoded.Length);
				}
				catch (IOException innerException)
				{
					throw new InvalidOperationException("malformed object", innerException);
				}
			}
			this.octets = memoryStream.ToArray();
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00069718 File Offset: 0x00069718
		private int GetLengthOfHeader(byte[] data)
		{
			int num = (int)data[1];
			if (num == 128)
			{
				return 2;
			}
			if (num <= 127)
			{
				return 2;
			}
			int num2 = num & 127;
			if (num2 > 4)
			{
				throw new InvalidOperationException("DER length more than 4 bytes: " + num2);
			}
			return num2 + 2;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0006976C File Offset: 0x0006976C
		public bool IsConstructed()
		{
			return this.isConstructed;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00069774 File Offset: 0x00069774
		public byte[] GetContents()
		{
			return this.octets;
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0006977C File Offset: 0x0006977C
		public int ApplicationTag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00069784 File Offset: 0x00069784
		public Asn1Object GetObject()
		{
			return Asn1Object.FromByteArray(this.GetContents());
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00069794 File Offset: 0x00069794
		public Asn1Object GetObject(int derTagNo)
		{
			if (derTagNo >= 31)
			{
				throw new IOException("unsupported tag number");
			}
			byte[] encoded = base.GetEncoded();
			byte[] array = this.ReplaceTagNumber(derTagNo, encoded);
			if ((encoded[0] & 32) != 0)
			{
				byte[] array2;
				(array2 = array)[0] = (array2[0] | 32);
			}
			return Asn1Object.FromByteArray(array);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000697E8 File Offset: 0x000697E8
		internal override void Encode(DerOutputStream derOut)
		{
			int num = 64;
			if (this.isConstructed)
			{
				num |= 32;
			}
			derOut.WriteEncoded(num, this.tag, this.octets);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00069820 File Offset: 0x00069820
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerApplicationSpecific derApplicationSpecific = asn1Object as DerApplicationSpecific;
			return derApplicationSpecific != null && (this.isConstructed == derApplicationSpecific.isConstructed && this.tag == derApplicationSpecific.tag) && Arrays.AreEqual(this.octets, derApplicationSpecific.octets);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00069878 File Offset: 0x00069878
		protected override int Asn1GetHashCode()
		{
			return this.isConstructed.GetHashCode() ^ this.tag.GetHashCode() ^ Arrays.GetHashCode(this.octets);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000698B4 File Offset: 0x000698B4
		private byte[] ReplaceTagNumber(int newTag, byte[] input)
		{
			int num = (int)(input[0] & 31);
			int num2 = 1;
			if (num == 31)
			{
				int num3 = (int)input[num2++];
				if ((num3 & 127) == 0)
				{
					throw new IOException("corrupted stream - invalid high tag number found");
				}
				while ((num3 & 128) != 0)
				{
					num3 = (int)input[num2++];
				}
			}
			int num4 = input.Length - num2;
			byte[] array = new byte[1 + num4];
			array[0] = (byte)newTag;
			Array.Copy(input, num2, array, 1, num4);
			return array;
		}

		// Token: 0x04000D84 RID: 3460
		private readonly bool isConstructed;

		// Token: 0x04000D85 RID: 3461
		private readonly int tag;

		// Token: 0x04000D86 RID: 3462
		private readonly byte[] octets;
	}
}
