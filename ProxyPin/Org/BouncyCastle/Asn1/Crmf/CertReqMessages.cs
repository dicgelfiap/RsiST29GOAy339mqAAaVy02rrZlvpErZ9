using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200012B RID: 299
	public class CertReqMessages : Asn1Encodable
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x00048FE4 File Offset: 0x00048FE4
		private CertReqMessages(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00048FF4 File Offset: 0x00048FF4
		public static CertReqMessages GetInstance(object obj)
		{
			if (obj is CertReqMessages)
			{
				return (CertReqMessages)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertReqMessages((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00049048 File Offset: 0x00049048
		public CertReqMessages(params CertReqMsg[] msgs)
		{
			this.content = new DerSequence(msgs);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0004905C File Offset: 0x0004905C
		public virtual CertReqMsg[] ToCertReqMsgArray()
		{
			CertReqMsg[] array = new CertReqMsg[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertReqMsg.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000490A8 File Offset: 0x000490A8
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x0400074B RID: 1867
		private readonly Asn1Sequence content;
	}
}
