using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000116 RID: 278
	public class OriginatorPublicKey : Asn1Encodable
	{
		// Token: 0x060009F4 RID: 2548 RVA: 0x00047000 File Offset: 0x00047000
		public OriginatorPublicKey(AlgorithmIdentifier algorithm, byte[] publicKey)
		{
			this.mAlgorithm = algorithm;
			this.mPublicKey = new DerBitString(publicKey);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0004701C File Offset: 0x0004701C
		[Obsolete("Use 'GetInstance' instead")]
		public OriginatorPublicKey(Asn1Sequence seq)
		{
			this.mAlgorithm = AlgorithmIdentifier.GetInstance(seq[0]);
			this.mPublicKey = DerBitString.GetInstance(seq[1]);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00047048 File Offset: 0x00047048
		public static OriginatorPublicKey GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OriginatorPublicKey.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00047058 File Offset: 0x00047058
		public static OriginatorPublicKey GetInstance(object obj)
		{
			if (obj == null || obj is OriginatorPublicKey)
			{
				return (OriginatorPublicKey)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OriginatorPublicKey(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("Invalid OriginatorPublicKey: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x000470B0 File Offset: 0x000470B0
		public AlgorithmIdentifier Algorithm
		{
			get
			{
				return this.mAlgorithm;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x000470B8 File Offset: 0x000470B8
		public DerBitString PublicKey
		{
			get
			{
				return this.mPublicKey;
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000470C0 File Offset: 0x000470C0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.mAlgorithm,
				this.mPublicKey
			});
		}

		// Token: 0x0400070A RID: 1802
		private readonly AlgorithmIdentifier mAlgorithm;

		// Token: 0x0400070B RID: 1803
		private readonly DerBitString mPublicKey;
	}
}
