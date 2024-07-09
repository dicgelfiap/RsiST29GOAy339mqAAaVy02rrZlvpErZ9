using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000E9 RID: 233
	public class PkiHeaderBuilder
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x00042EDC File Offset: 0x00042EDC
		public PkiHeaderBuilder(int pvno, GeneralName sender, GeneralName recipient) : this(new DerInteger(pvno), sender, recipient)
		{
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00042EEC File Offset: 0x00042EEC
		private PkiHeaderBuilder(DerInteger pvno, GeneralName sender, GeneralName recipient)
		{
			this.pvno = pvno;
			this.sender = sender;
			this.recipient = recipient;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00042F0C File Offset: 0x00042F0C
		public virtual PkiHeaderBuilder SetMessageTime(DerGeneralizedTime time)
		{
			this.messageTime = time;
			return this;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00042F18 File Offset: 0x00042F18
		public virtual PkiHeaderBuilder SetProtectionAlg(AlgorithmIdentifier aid)
		{
			this.protectionAlg = aid;
			return this;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00042F24 File Offset: 0x00042F24
		public virtual PkiHeaderBuilder SetSenderKID(byte[] kid)
		{
			return this.SetSenderKID((kid == null) ? null : new DerOctetString(kid));
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00042F40 File Offset: 0x00042F40
		public virtual PkiHeaderBuilder SetSenderKID(Asn1OctetString kid)
		{
			this.senderKID = kid;
			return this;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00042F4C File Offset: 0x00042F4C
		public virtual PkiHeaderBuilder SetRecipKID(byte[] kid)
		{
			return this.SetRecipKID((kid == null) ? null : new DerOctetString(kid));
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00042F68 File Offset: 0x00042F68
		public virtual PkiHeaderBuilder SetRecipKID(DerOctetString kid)
		{
			this.recipKID = kid;
			return this;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00042F74 File Offset: 0x00042F74
		public virtual PkiHeaderBuilder SetTransactionID(byte[] tid)
		{
			return this.SetTransactionID((tid == null) ? null : new DerOctetString(tid));
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00042F90 File Offset: 0x00042F90
		public virtual PkiHeaderBuilder SetTransactionID(Asn1OctetString tid)
		{
			this.transactionID = tid;
			return this;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00042F9C File Offset: 0x00042F9C
		public virtual PkiHeaderBuilder SetSenderNonce(byte[] nonce)
		{
			return this.SetSenderNonce((nonce == null) ? null : new DerOctetString(nonce));
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00042FB8 File Offset: 0x00042FB8
		public virtual PkiHeaderBuilder SetSenderNonce(Asn1OctetString nonce)
		{
			this.senderNonce = nonce;
			return this;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00042FC4 File Offset: 0x00042FC4
		public virtual PkiHeaderBuilder SetRecipNonce(byte[] nonce)
		{
			return this.SetRecipNonce((nonce == null) ? null : new DerOctetString(nonce));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00042FE0 File Offset: 0x00042FE0
		public virtual PkiHeaderBuilder SetRecipNonce(Asn1OctetString nonce)
		{
			this.recipNonce = nonce;
			return this;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00042FEC File Offset: 0x00042FEC
		public virtual PkiHeaderBuilder SetFreeText(PkiFreeText text)
		{
			this.freeText = text;
			return this;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00042FF8 File Offset: 0x00042FF8
		public virtual PkiHeaderBuilder SetGeneralInfo(InfoTypeAndValue genInfo)
		{
			return this.SetGeneralInfo(PkiHeaderBuilder.MakeGeneralInfoSeq(genInfo));
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00043008 File Offset: 0x00043008
		public virtual PkiHeaderBuilder SetGeneralInfo(InfoTypeAndValue[] genInfos)
		{
			return this.SetGeneralInfo(PkiHeaderBuilder.MakeGeneralInfoSeq(genInfos));
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00043018 File Offset: 0x00043018
		public virtual PkiHeaderBuilder SetGeneralInfo(Asn1Sequence seqOfInfoTypeAndValue)
		{
			this.generalInfo = seqOfInfoTypeAndValue;
			return this;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00043024 File Offset: 0x00043024
		private static Asn1Sequence MakeGeneralInfoSeq(InfoTypeAndValue generalInfo)
		{
			return new DerSequence(generalInfo);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0004302C File Offset: 0x0004302C
		private static Asn1Sequence MakeGeneralInfoSeq(InfoTypeAndValue[] generalInfos)
		{
			Asn1Sequence result = null;
			if (generalInfos != null)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
				for (int i = 0; i < generalInfos.Length; i++)
				{
					asn1EncodableVector.Add(generalInfos[i]);
				}
				result = new DerSequence(asn1EncodableVector);
			}
			return result;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00043074 File Offset: 0x00043074
		public virtual PkiHeader Build()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pvno,
				this.sender,
				this.recipient
			});
			this.AddOptional(asn1EncodableVector, 0, this.messageTime);
			this.AddOptional(asn1EncodableVector, 1, this.protectionAlg);
			this.AddOptional(asn1EncodableVector, 2, this.senderKID);
			this.AddOptional(asn1EncodableVector, 3, this.recipKID);
			this.AddOptional(asn1EncodableVector, 4, this.transactionID);
			this.AddOptional(asn1EncodableVector, 5, this.senderNonce);
			this.AddOptional(asn1EncodableVector, 6, this.recipNonce);
			this.AddOptional(asn1EncodableVector, 7, this.freeText);
			this.AddOptional(asn1EncodableVector, 8, this.generalInfo);
			this.messageTime = null;
			this.protectionAlg = null;
			this.senderKID = null;
			this.recipKID = null;
			this.transactionID = null;
			this.senderNonce = null;
			this.recipNonce = null;
			this.freeText = null;
			this.generalInfo = null;
			return PkiHeader.GetInstance(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00043184 File Offset: 0x00043184
		private void AddOptional(Asn1EncodableVector v, int tagNo, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new DerTaggedObject(true, tagNo, obj));
			}
		}

		// Token: 0x04000667 RID: 1639
		private DerInteger pvno;

		// Token: 0x04000668 RID: 1640
		private GeneralName sender;

		// Token: 0x04000669 RID: 1641
		private GeneralName recipient;

		// Token: 0x0400066A RID: 1642
		private DerGeneralizedTime messageTime;

		// Token: 0x0400066B RID: 1643
		private AlgorithmIdentifier protectionAlg;

		// Token: 0x0400066C RID: 1644
		private Asn1OctetString senderKID;

		// Token: 0x0400066D RID: 1645
		private Asn1OctetString recipKID;

		// Token: 0x0400066E RID: 1646
		private Asn1OctetString transactionID;

		// Token: 0x0400066F RID: 1647
		private Asn1OctetString senderNonce;

		// Token: 0x04000670 RID: 1648
		private Asn1OctetString recipNonce;

		// Token: 0x04000671 RID: 1649
		private PkiFreeText freeText;

		// Token: 0x04000672 RID: 1650
		private Asn1Sequence generalInfo;
	}
}
