using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000182 RID: 386
	public class Cast5CbcParameters : Asn1Encodable
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x000521C0 File Offset: 0x000521C0
		public static Cast5CbcParameters GetInstance(object o)
		{
			if (o is Cast5CbcParameters)
			{
				return (Cast5CbcParameters)o;
			}
			if (o is Asn1Sequence)
			{
				return new Cast5CbcParameters((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in Cast5CbcParameters factory");
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000521F8 File Offset: 0x000521F8
		public Cast5CbcParameters(byte[] iv, int keyLength)
		{
			this.iv = new DerOctetString(iv);
			this.keyLength = new DerInteger(keyLength);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00052218 File Offset: 0x00052218
		private Cast5CbcParameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = (Asn1OctetString)seq[0];
			this.keyLength = (DerInteger)seq[1];
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00052270 File Offset: 0x00052270
		public byte[] GetIV()
		{
			return Arrays.Clone(this.iv.GetOctets());
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00052284 File Offset: 0x00052284
		public int KeyLength
		{
			get
			{
				return this.keyLength.IntValueExact;
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00052294 File Offset: 0x00052294
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.keyLength
			});
		}

		// Token: 0x04000901 RID: 2305
		private readonly DerInteger keyLength;

		// Token: 0x04000902 RID: 2306
		private readonly Asn1OctetString iv;
	}
}
