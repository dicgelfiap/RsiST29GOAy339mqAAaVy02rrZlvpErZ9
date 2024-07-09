using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A5 RID: 677
	public class ECDsaPublicBcpgKey : ECPublicBcpgKey
	{
		// Token: 0x06001511 RID: 5393 RVA: 0x0007024C File Offset: 0x0007024C
		protected internal ECDsaPublicBcpgKey(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00070258 File Offset: 0x00070258
		public ECDsaPublicBcpgKey(DerObjectIdentifier oid, ECPoint point) : base(oid, point)
		{
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00070264 File Offset: 0x00070264
		public ECDsaPublicBcpgKey(DerObjectIdentifier oid, BigInteger encodedPoint) : base(oid, encodedPoint)
		{
		}
	}
}
