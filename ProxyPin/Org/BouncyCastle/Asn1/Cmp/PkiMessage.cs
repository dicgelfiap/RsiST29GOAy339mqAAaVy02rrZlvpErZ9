using System;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000EA RID: 234
	public class PkiMessage : Asn1Encodable
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x0004319C File Offset: 0x0004319C
		private PkiMessage(Asn1Sequence seq)
		{
			this.header = PkiHeader.GetInstance(seq[0]);
			this.body = PkiBody.GetInstance(seq[1]);
			for (int i = 2; i < seq.Count; i++)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i].ToAsn1Object();
				if (asn1TaggedObject.TagNo == 0)
				{
					this.protection = DerBitString.GetInstance(asn1TaggedObject, true);
				}
				else
				{
					this.extraCerts = Asn1Sequence.GetInstance(asn1TaggedObject, true);
				}
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00043228 File Offset: 0x00043228
		public static PkiMessage GetInstance(object obj)
		{
			if (obj is PkiMessage)
			{
				return (PkiMessage)obj;
			}
			if (obj != null)
			{
				return new PkiMessage(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00043250 File Offset: 0x00043250
		public PkiMessage(PkiHeader header, PkiBody body, DerBitString protection, CmpCertificate[] extraCerts)
		{
			this.header = header;
			this.body = body;
			this.protection = protection;
			if (extraCerts != null)
			{
				this.extraCerts = new DerSequence(extraCerts);
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00043284 File Offset: 0x00043284
		public PkiMessage(PkiHeader header, PkiBody body, DerBitString protection) : this(header, body, protection, null)
		{
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00043290 File Offset: 0x00043290
		public PkiMessage(PkiHeader header, PkiBody body) : this(header, body, null, null)
		{
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0004329C File Offset: 0x0004329C
		public virtual PkiHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x000432A4 File Offset: 0x000432A4
		public virtual PkiBody Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000432AC File Offset: 0x000432AC
		public virtual DerBitString Protection
		{
			get
			{
				return this.protection;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000432B4 File Offset: 0x000432B4
		public virtual CmpCertificate[] GetExtraCerts()
		{
			if (this.extraCerts == null)
			{
				return null;
			}
			CmpCertificate[] array = new CmpCertificate[this.extraCerts.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = CmpCertificate.GetInstance(this.extraCerts[i]);
			}
			return array;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00043310 File Offset: 0x00043310
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.header,
				this.body
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.protection);
			asn1EncodableVector.AddOptionalTagged(true, 1, this.extraCerts);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000673 RID: 1651
		private readonly PkiHeader header;

		// Token: 0x04000674 RID: 1652
		private readonly PkiBody body;

		// Token: 0x04000675 RID: 1653
		private readonly DerBitString protection;

		// Token: 0x04000676 RID: 1654
		private readonly Asn1Sequence extraCerts;
	}
}
