using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001E3 RID: 483
	public class AttCertIssuer : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000F80 RID: 3968 RVA: 0x0005C978 File Offset: 0x0005C978
		public static AttCertIssuer GetInstance(object obj)
		{
			if (obj is AttCertIssuer)
			{
				return (AttCertIssuer)obj;
			}
			if (obj is V2Form)
			{
				return new AttCertIssuer(V2Form.GetInstance(obj));
			}
			if (obj is GeneralNames)
			{
				return new AttCertIssuer((GeneralNames)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return new AttCertIssuer(V2Form.GetInstance((Asn1TaggedObject)obj, false));
			}
			if (obj is Asn1Sequence)
			{
				return new AttCertIssuer(GeneralNames.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0005CA18 File Offset: 0x0005CA18
		public static AttCertIssuer GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AttCertIssuer.GetInstance(obj.GetObject());
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0005CA28 File Offset: 0x0005CA28
		public AttCertIssuer(GeneralNames names)
		{
			this.obj = names;
			this.choiceObj = this.obj.ToAsn1Object();
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0005CA48 File Offset: 0x0005CA48
		public AttCertIssuer(V2Form v2Form)
		{
			this.obj = v2Form;
			this.choiceObj = new DerTaggedObject(false, 0, this.obj);
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0005CA6C File Offset: 0x0005CA6C
		public Asn1Encodable Issuer
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0005CA74 File Offset: 0x0005CA74
		public override Asn1Object ToAsn1Object()
		{
			return this.choiceObj;
		}

		// Token: 0x04000B9C RID: 2972
		internal readonly Asn1Encodable obj;

		// Token: 0x04000B9D RID: 2973
		internal readonly Asn1Object choiceObj;
	}
}
