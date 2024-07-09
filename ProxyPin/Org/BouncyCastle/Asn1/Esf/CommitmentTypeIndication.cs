using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200014B RID: 331
	public class CommitmentTypeIndication : Asn1Encodable
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x0004C074 File Offset: 0x0004C074
		public static CommitmentTypeIndication GetInstance(object obj)
		{
			if (obj == null || obj is CommitmentTypeIndication)
			{
				return (CommitmentTypeIndication)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CommitmentTypeIndication((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CommitmentTypeIndication' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0004C0D0 File Offset: 0x0004C0D0
		public CommitmentTypeIndication(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.commitmentTypeId = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.commitmentTypeQualifier = (Asn1Sequence)seq[1].ToAsn1Object();
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0004C16C File Offset: 0x0004C16C
		public CommitmentTypeIndication(DerObjectIdentifier commitmentTypeId) : this(commitmentTypeId, null)
		{
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0004C178 File Offset: 0x0004C178
		public CommitmentTypeIndication(DerObjectIdentifier commitmentTypeId, Asn1Sequence commitmentTypeQualifier)
		{
			if (commitmentTypeId == null)
			{
				throw new ArgumentNullException("commitmentTypeId");
			}
			this.commitmentTypeId = commitmentTypeId;
			if (commitmentTypeQualifier != null)
			{
				this.commitmentTypeQualifier = commitmentTypeQualifier;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0004C1A8 File Offset: 0x0004C1A8
		public DerObjectIdentifier CommitmentTypeID
		{
			get
			{
				return this.commitmentTypeId;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0004C1B0 File Offset: 0x0004C1B0
		public Asn1Sequence CommitmentTypeQualifier
		{
			get
			{
				return this.commitmentTypeQualifier;
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0004C1B8 File Offset: 0x0004C1B8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.commitmentTypeId
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.commitmentTypeQualifier
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040007F0 RID: 2032
		private readonly DerObjectIdentifier commitmentTypeId;

		// Token: 0x040007F1 RID: 2033
		private readonly Asn1Sequence commitmentTypeQualifier;
	}
}
