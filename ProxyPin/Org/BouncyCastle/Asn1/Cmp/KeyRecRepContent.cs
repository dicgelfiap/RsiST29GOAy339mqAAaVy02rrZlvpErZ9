using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000DD RID: 221
	public class KeyRecRepContent : Asn1Encodable
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x00041CBC File Offset: 0x00041CBC
		private KeyRecRepContent(Asn1Sequence seq)
		{
			this.status = PkiStatusInfo.GetInstance(seq[0]);
			for (int i = 1; i < seq.Count; i++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[i]);
				switch (instance.TagNo)
				{
				case 0:
					this.newSigCert = CmpCertificate.GetInstance(instance.GetObject());
					break;
				case 1:
					this.caCerts = Asn1Sequence.GetInstance(instance.GetObject());
					break;
				case 2:
					this.keyPairHist = Asn1Sequence.GetInstance(instance.GetObject());
					break;
				default:
					throw new ArgumentException("unknown tag number: " + instance.TagNo, "seq");
				}
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00041D8C File Offset: 0x00041D8C
		public static KeyRecRepContent GetInstance(object obj)
		{
			if (obj is KeyRecRepContent)
			{
				return (KeyRecRepContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KeyRecRepContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00041DE0 File Offset: 0x00041DE0
		public virtual PkiStatusInfo Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00041DE8 File Offset: 0x00041DE8
		public virtual CmpCertificate NewSigCert
		{
			get
			{
				return this.newSigCert;
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00041DF0 File Offset: 0x00041DF0
		public virtual CmpCertificate[] GetCACerts()
		{
			if (this.caCerts == null)
			{
				return null;
			}
			CmpCertificate[] array = new CmpCertificate[this.caCerts.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CmpCertificate.GetInstance(this.caCerts[num]);
			}
			return array;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00041E4C File Offset: 0x00041E4C
		public virtual CertifiedKeyPair[] GetKeyPairHist()
		{
			if (this.keyPairHist == null)
			{
				return null;
			}
			CertifiedKeyPair[] array = new CertifiedKeyPair[this.keyPairHist.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertifiedKeyPair.GetInstance(this.keyPairHist[num]);
			}
			return array;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00041EA8 File Offset: 0x00041EA8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.newSigCert);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.caCerts);
			asn1EncodableVector.AddOptionalTagged(true, 2, this.keyPairHist);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000611 RID: 1553
		private readonly PkiStatusInfo status;

		// Token: 0x04000612 RID: 1554
		private readonly CmpCertificate newSigCert;

		// Token: 0x04000613 RID: 1555
		private readonly Asn1Sequence caCerts;

		// Token: 0x04000614 RID: 1556
		private readonly Asn1Sequence keyPairHist;
	}
}
