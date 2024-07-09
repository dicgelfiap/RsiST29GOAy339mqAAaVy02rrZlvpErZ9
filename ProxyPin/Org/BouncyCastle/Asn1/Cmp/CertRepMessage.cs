using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D2 RID: 210
	public class CertRepMessage : Asn1Encodable
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x00040F0C File Offset: 0x00040F0C
		private CertRepMessage(Asn1Sequence seq)
		{
			int index = 0;
			if (seq.Count > 1)
			{
				this.caPubs = Asn1Sequence.GetInstance((Asn1TaggedObject)seq[index++], true);
			}
			this.response = Asn1Sequence.GetInstance(seq[index]);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00040F60 File Offset: 0x00040F60
		public static CertRepMessage GetInstance(object obj)
		{
			if (obj is CertRepMessage)
			{
				return (CertRepMessage)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertRepMessage((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00040FB4 File Offset: 0x00040FB4
		public CertRepMessage(CmpCertificate[] caPubs, CertResponse[] response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (caPubs != null)
			{
				this.caPubs = new DerSequence(caPubs);
			}
			this.response = new DerSequence(response);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00040FEC File Offset: 0x00040FEC
		public virtual CmpCertificate[] GetCAPubs()
		{
			if (this.caPubs == null)
			{
				return null;
			}
			CmpCertificate[] array = new CmpCertificate[this.caPubs.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CmpCertificate.GetInstance(this.caPubs[num]);
			}
			return array;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00041048 File Offset: 0x00041048
		public virtual CertResponse[] GetResponse()
		{
			CertResponse[] array = new CertResponse[this.response.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertResponse.GetInstance(this.response[num]);
			}
			return array;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00041094 File Offset: 0x00041094
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 1, this.caPubs);
			asn1EncodableVector.Add(this.response);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040005E1 RID: 1505
		private readonly Asn1Sequence caPubs;

		// Token: 0x040005E2 RID: 1506
		private readonly Asn1Sequence response;
	}
}
