using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200019B RID: 411
	public class ServiceLocator : Asn1Encodable
	{
		// Token: 0x06000D7F RID: 3455 RVA: 0x00054444 File Offset: 0x00054444
		public static ServiceLocator GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ServiceLocator.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00054454 File Offset: 0x00054454
		public static ServiceLocator GetInstance(object obj)
		{
			if (obj == null || obj is ServiceLocator)
			{
				return (ServiceLocator)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ServiceLocator((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000544B0 File Offset: 0x000544B0
		public ServiceLocator(X509Name issuer) : this(issuer, null)
		{
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000544BC File Offset: 0x000544BC
		public ServiceLocator(X509Name issuer, Asn1Object locator)
		{
			if (issuer == null)
			{
				throw new ArgumentNullException("issuer");
			}
			this.issuer = issuer;
			this.locator = locator;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000544E4 File Offset: 0x000544E4
		private ServiceLocator(Asn1Sequence seq)
		{
			this.issuer = X509Name.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.locator = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0005452C File Offset: 0x0005452C
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00054534 File Offset: 0x00054534
		public Asn1Object Locator
		{
			get
			{
				return this.locator;
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0005453C File Offset: 0x0005453C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.issuer
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.locator
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009AC RID: 2476
		private readonly X509Name issuer;

		// Token: 0x040009AD RID: 2477
		private readonly Asn1Object locator;
	}
}
