using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200014C RID: 332
	public class CommitmentTypeQualifier : Asn1Encodable
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0004C208 File Offset: 0x0004C208
		public CommitmentTypeQualifier(DerObjectIdentifier commitmentTypeIdentifier) : this(commitmentTypeIdentifier, null)
		{
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0004C214 File Offset: 0x0004C214
		public CommitmentTypeQualifier(DerObjectIdentifier commitmentTypeIdentifier, Asn1Encodable qualifier)
		{
			if (commitmentTypeIdentifier == null)
			{
				throw new ArgumentNullException("commitmentTypeIdentifier");
			}
			this.commitmentTypeIdentifier = commitmentTypeIdentifier;
			if (qualifier != null)
			{
				this.qualifier = qualifier.ToAsn1Object();
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0004C248 File Offset: 0x0004C248
		public CommitmentTypeQualifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.commitmentTypeIdentifier = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.qualifier = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0004C2E0 File Offset: 0x0004C2E0
		public static CommitmentTypeQualifier GetInstance(object obj)
		{
			if (obj == null || obj is CommitmentTypeQualifier)
			{
				return (CommitmentTypeQualifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CommitmentTypeQualifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CommitmentTypeQualifier' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0004C33C File Offset: 0x0004C33C
		public DerObjectIdentifier CommitmentTypeIdentifier
		{
			get
			{
				return this.commitmentTypeIdentifier;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0004C344 File Offset: 0x0004C344
		public Asn1Object Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0004C34C File Offset: 0x0004C34C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.commitmentTypeIdentifier
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.qualifier
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040007F2 RID: 2034
		private readonly DerObjectIdentifier commitmentTypeIdentifier;

		// Token: 0x040007F3 RID: 2035
		private readonly Asn1Object qualifier;
	}
}
