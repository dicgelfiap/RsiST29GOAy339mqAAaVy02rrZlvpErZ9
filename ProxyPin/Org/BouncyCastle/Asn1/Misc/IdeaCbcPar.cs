using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000183 RID: 387
	public class IdeaCbcPar : Asn1Encodable
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x000522CC File Offset: 0x000522CC
		public static IdeaCbcPar GetInstance(object o)
		{
			if (o is IdeaCbcPar)
			{
				return (IdeaCbcPar)o;
			}
			if (o is Asn1Sequence)
			{
				return new IdeaCbcPar((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in IDEACBCPar factory");
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00052304 File Offset: 0x00052304
		public IdeaCbcPar(byte[] iv)
		{
			this.iv = new DerOctetString(iv);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00052318 File Offset: 0x00052318
		private IdeaCbcPar(Asn1Sequence seq)
		{
			if (seq.Count == 1)
			{
				this.iv = (Asn1OctetString)seq[0];
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00052340 File Offset: 0x00052340
		public byte[] GetIV()
		{
			if (this.iv != null)
			{
				return this.iv.GetOctets();
			}
			return null;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0005235C File Offset: 0x0005235C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.iv
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000903 RID: 2307
		internal Asn1OctetString iv;
	}
}
