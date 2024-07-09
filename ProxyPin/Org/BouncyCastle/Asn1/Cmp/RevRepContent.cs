using System;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F6 RID: 246
	public class RevRepContent : Asn1Encodable
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x00043E48 File Offset: 0x00043E48
		private RevRepContent(Asn1Sequence seq)
		{
			this.status = Asn1Sequence.GetInstance(seq[0]);
			for (int i = 1; i < seq.Count; i++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[i]);
				if (instance.TagNo == 0)
				{
					this.revCerts = Asn1Sequence.GetInstance(instance, true);
				}
				else
				{
					this.crls = Asn1Sequence.GetInstance(instance, true);
				}
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00043EBC File Offset: 0x00043EBC
		public static RevRepContent GetInstance(object obj)
		{
			if (obj is RevRepContent)
			{
				return (RevRepContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevRepContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00043F10 File Offset: 0x00043F10
		public virtual PkiStatusInfo[] GetStatus()
		{
			PkiStatusInfo[] array = new PkiStatusInfo[this.status.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = PkiStatusInfo.GetInstance(this.status[num]);
			}
			return array;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00043F5C File Offset: 0x00043F5C
		public virtual CertId[] GetRevCerts()
		{
			if (this.revCerts == null)
			{
				return null;
			}
			CertId[] array = new CertId[this.revCerts.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertId.GetInstance(this.revCerts[num]);
			}
			return array;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00043FB8 File Offset: 0x00043FB8
		public virtual CertificateList[] GetCrls()
		{
			if (this.crls == null)
			{
				return null;
			}
			CertificateList[] array = new CertificateList[this.crls.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertificateList.GetInstance(this.crls[num]);
			}
			return array;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00044014 File Offset: 0x00044014
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.revCerts);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.crls);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400069A RID: 1690
		private readonly Asn1Sequence status;

		// Token: 0x0400069B RID: 1691
		private readonly Asn1Sequence revCerts;

		// Token: 0x0400069C RID: 1692
		private readonly Asn1Sequence crls;
	}
}
