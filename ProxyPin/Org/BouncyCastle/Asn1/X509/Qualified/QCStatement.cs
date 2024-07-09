using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001DB RID: 475
	public class QCStatement : Asn1Encodable
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x0005BE90 File Offset: 0x0005BE90
		public static QCStatement GetInstance(object obj)
		{
			if (obj == null || obj is QCStatement)
			{
				return (QCStatement)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new QCStatement(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0005BEEC File Offset: 0x0005BEEC
		private QCStatement(Asn1Sequence seq)
		{
			this.qcStatementId = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.qcStatementInfo = seq[1];
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0005BF30 File Offset: 0x0005BF30
		public QCStatement(DerObjectIdentifier qcStatementId)
		{
			this.qcStatementId = qcStatementId;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0005BF40 File Offset: 0x0005BF40
		public QCStatement(DerObjectIdentifier qcStatementId, Asn1Encodable qcStatementInfo)
		{
			this.qcStatementId = qcStatementId;
			this.qcStatementInfo = qcStatementInfo;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0005BF58 File Offset: 0x0005BF58
		public DerObjectIdentifier StatementId
		{
			get
			{
				return this.qcStatementId;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0005BF60 File Offset: 0x0005BF60
		public Asn1Encodable StatementInfo
		{
			get
			{
				return this.qcStatementInfo;
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0005BF68 File Offset: 0x0005BF68
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.qcStatementId
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.qcStatementInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000B7E RID: 2942
		private readonly DerObjectIdentifier qcStatementId;

		// Token: 0x04000B7F RID: 2943
		private readonly Asn1Encodable qcStatementInfo;
	}
}
