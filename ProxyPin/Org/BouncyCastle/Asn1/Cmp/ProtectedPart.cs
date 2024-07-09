using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F3 RID: 243
	public class ProtectedPart : Asn1Encodable
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x00043AD0 File Offset: 0x00043AD0
		private ProtectedPart(Asn1Sequence seq)
		{
			this.header = PkiHeader.GetInstance(seq[0]);
			this.body = PkiBody.GetInstance(seq[1]);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00043AFC File Offset: 0x00043AFC
		public static ProtectedPart GetInstance(object obj)
		{
			if (obj is ProtectedPart)
			{
				return (ProtectedPart)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ProtectedPart((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00043B50 File Offset: 0x00043B50
		public ProtectedPart(PkiHeader header, PkiBody body)
		{
			this.header = header;
			this.body = body;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00043B68 File Offset: 0x00043B68
		public virtual PkiHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00043B70 File Offset: 0x00043B70
		public virtual PkiBody Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00043B78 File Offset: 0x00043B78
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.header,
				this.body
			});
		}

		// Token: 0x04000691 RID: 1681
		private readonly PkiHeader header;

		// Token: 0x04000692 RID: 1682
		private readonly PkiBody body;
	}
}
