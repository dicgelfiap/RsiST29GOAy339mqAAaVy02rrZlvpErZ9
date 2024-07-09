using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020001CB RID: 459
	public class MessageImprint : Asn1Encodable
	{
		// Token: 0x06000ED4 RID: 3796 RVA: 0x00059778 File Offset: 0x00059778
		public static MessageImprint GetInstance(object obj)
		{
			if (obj is MessageImprint)
			{
				return (MessageImprint)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new MessageImprint(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000597A0 File Offset: 0x000597A0
		private MessageImprint(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.hashedMessage = Asn1OctetString.GetInstance(seq[1]).GetOctets();
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000597FC File Offset: 0x000597FC
		public MessageImprint(AlgorithmIdentifier hashAlgorithm, byte[] hashedMessage)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.hashedMessage = hashedMessage;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00059814 File Offset: 0x00059814
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0005981C File Offset: 0x0005981C
		public byte[] GetHashedMessage()
		{
			return this.hashedMessage;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00059824 File Offset: 0x00059824
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				new DerOctetString(this.hashedMessage)
			});
		}

		// Token: 0x04000B2B RID: 2859
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000B2C RID: 2860
		private readonly byte[] hashedMessage;
	}
}
