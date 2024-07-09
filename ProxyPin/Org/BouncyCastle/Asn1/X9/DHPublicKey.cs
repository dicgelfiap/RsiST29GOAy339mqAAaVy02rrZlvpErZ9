using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000228 RID: 552
	public class DHPublicKey : Asn1Encodable
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x00065D08 File Offset: 0x00065D08
		public static DHPublicKey GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return DHPublicKey.GetInstance(DerInteger.GetInstance(obj, isExplicit));
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00065D18 File Offset: 0x00065D18
		public static DHPublicKey GetInstance(object obj)
		{
			if (obj == null || obj is DHPublicKey)
			{
				return (DHPublicKey)obj;
			}
			if (obj is DerInteger)
			{
				return new DHPublicKey((DerInteger)obj);
			}
			throw new ArgumentException("Invalid DHPublicKey: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00065D74 File Offset: 0x00065D74
		public DHPublicKey(DerInteger y)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = y;
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00065D94 File Offset: 0x00065D94
		public DerInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00065D9C File Offset: 0x00065D9C
		public override Asn1Object ToAsn1Object()
		{
			return this.y;
		}

		// Token: 0x04000CF9 RID: 3321
		private readonly DerInteger y;
	}
}
