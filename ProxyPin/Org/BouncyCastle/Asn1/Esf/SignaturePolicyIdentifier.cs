using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200015F RID: 351
	public class SignaturePolicyIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000C02 RID: 3074 RVA: 0x0004E51C File Offset: 0x0004E51C
		public static SignaturePolicyIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is SignaturePolicyIdentifier)
			{
				return (SignaturePolicyIdentifier)obj;
			}
			if (obj is SignaturePolicyId)
			{
				return new SignaturePolicyIdentifier((SignaturePolicyId)obj);
			}
			if (obj is Asn1Null)
			{
				return new SignaturePolicyIdentifier();
			}
			throw new ArgumentException("Unknown object in 'SignaturePolicyIdentifier' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0004E588 File Offset: 0x0004E588
		public SignaturePolicyIdentifier()
		{
			this.sigPolicy = null;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0004E598 File Offset: 0x0004E598
		public SignaturePolicyIdentifier(SignaturePolicyId signaturePolicyId)
		{
			if (signaturePolicyId == null)
			{
				throw new ArgumentNullException("signaturePolicyId");
			}
			this.sigPolicy = signaturePolicyId;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0004E5B8 File Offset: 0x0004E5B8
		public SignaturePolicyId SignaturePolicyId
		{
			get
			{
				return this.sigPolicy;
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0004E5C0 File Offset: 0x0004E5C0
		public override Asn1Object ToAsn1Object()
		{
			if (this.sigPolicy != null)
			{
				return this.sigPolicy.ToAsn1Object();
			}
			return DerNull.Instance;
		}

		// Token: 0x04000824 RID: 2084
		private readonly SignaturePolicyId sigPolicy;
	}
}
