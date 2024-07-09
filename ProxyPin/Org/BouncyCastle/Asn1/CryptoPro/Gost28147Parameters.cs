using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000143 RID: 323
	public class Gost28147Parameters : Asn1Encodable
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0004B63C File Offset: 0x0004B63C
		public static Gost28147Parameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost28147Parameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0004B64C File Offset: 0x0004B64C
		public static Gost28147Parameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost28147Parameters)
			{
				return (Gost28147Parameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Gost28147Parameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0004B6A4 File Offset: 0x0004B6A4
		private Gost28147Parameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.iv = Asn1OctetString.GetInstance(seq[0]);
			this.paramSet = DerObjectIdentifier.GetInstance(seq[1]);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0004B6FC File Offset: 0x0004B6FC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.iv,
				this.paramSet
			});
		}

		// Token: 0x040007C1 RID: 1985
		private readonly Asn1OctetString iv;

		// Token: 0x040007C2 RID: 1986
		private readonly DerObjectIdentifier paramSet;
	}
}
