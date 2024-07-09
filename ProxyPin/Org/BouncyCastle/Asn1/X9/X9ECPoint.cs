using System;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000231 RID: 561
	public class X9ECPoint : Asn1Encodable
	{
		// Token: 0x0600122D RID: 4653 RVA: 0x00066D8C File Offset: 0x00066D8C
		public X9ECPoint(ECPoint p) : this(p, false)
		{
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00066D98 File Offset: 0x00066D98
		public X9ECPoint(ECPoint p, bool compressed)
		{
			this.p = p.Normalize();
			this.encoding = new DerOctetString(p.GetEncoded(compressed));
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00066DC0 File Offset: 0x00066DC0
		public X9ECPoint(ECCurve c, byte[] encoding)
		{
			this.c = c;
			this.encoding = new DerOctetString(Arrays.Clone(encoding));
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00066DE0 File Offset: 0x00066DE0
		public X9ECPoint(ECCurve c, Asn1OctetString s) : this(c, s.GetOctets())
		{
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00066DF0 File Offset: 0x00066DF0
		public byte[] GetPointEncoding()
		{
			return Arrays.Clone(this.encoding.GetOctets());
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00066E04 File Offset: 0x00066E04
		public ECPoint Point
		{
			get
			{
				if (this.p == null)
				{
					this.p = this.c.DecodePoint(this.encoding.GetOctets()).Normalize();
				}
				return this.p;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00066E38 File Offset: 0x00066E38
		public bool IsPointCompressed
		{
			get
			{
				byte[] octets = this.encoding.GetOctets();
				return octets != null && octets.Length > 0 && (octets[0] == 2 || octets[0] == 3);
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00066E78 File Offset: 0x00066E78
		public override Asn1Object ToAsn1Object()
		{
			return this.encoding;
		}

		// Token: 0x04000D0E RID: 3342
		private readonly Asn1OctetString encoding;

		// Token: 0x04000D0F RID: 3343
		private ECCurve c;

		// Token: 0x04000D10 RID: 3344
		private ECPoint p;
	}
}
