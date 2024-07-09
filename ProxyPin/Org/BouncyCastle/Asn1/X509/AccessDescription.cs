using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001E2 RID: 482
	public class AccessDescription : Asn1Encodable
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x0005C834 File Offset: 0x0005C834
		public static AccessDescription GetInstance(object obj)
		{
			if (obj is AccessDescription)
			{
				return (AccessDescription)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AccessDescription((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0005C888 File Offset: 0x0005C888
		private AccessDescription(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("wrong number of elements in sequence");
			}
			this.accessMethod = DerObjectIdentifier.GetInstance(seq[0]);
			this.accessLocation = GeneralName.GetInstance(seq[1]);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0005C8DC File Offset: 0x0005C8DC
		public AccessDescription(DerObjectIdentifier oid, GeneralName location)
		{
			this.accessMethod = oid;
			this.accessLocation = location;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0005C8F4 File Offset: 0x0005C8F4
		public DerObjectIdentifier AccessMethod
		{
			get
			{
				return this.accessMethod;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0005C8FC File Offset: 0x0005C8FC
		public GeneralName AccessLocation
		{
			get
			{
				return this.accessLocation;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0005C904 File Offset: 0x0005C904
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.accessMethod,
				this.accessLocation
			});
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0005C93C File Offset: 0x0005C93C
		public override string ToString()
		{
			return "AccessDescription: Oid(" + this.accessMethod.Id + ")";
		}

		// Token: 0x04000B98 RID: 2968
		public static readonly DerObjectIdentifier IdADCAIssuers = new DerObjectIdentifier("1.3.6.1.5.5.7.48.2");

		// Token: 0x04000B99 RID: 2969
		public static readonly DerObjectIdentifier IdADOcsp = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1");

		// Token: 0x04000B9A RID: 2970
		private readonly DerObjectIdentifier accessMethod;

		// Token: 0x04000B9B RID: 2971
		private readonly GeneralName accessLocation;
	}
}
