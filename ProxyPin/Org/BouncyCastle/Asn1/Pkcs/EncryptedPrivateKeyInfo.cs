using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A9 RID: 425
	public class EncryptedPrivateKeyInfo : Asn1Encodable
	{
		// Token: 0x06000DE3 RID: 3555 RVA: 0x00055724 File Offset: 0x00055724
		private EncryptedPrivateKeyInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.algId = AlgorithmIdentifier.GetInstance(seq[0]);
			this.data = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0005577C File Offset: 0x0005577C
		public EncryptedPrivateKeyInfo(AlgorithmIdentifier algId, byte[] encoding)
		{
			this.algId = algId;
			this.data = new DerOctetString(encoding);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00055798 File Offset: 0x00055798
		public static EncryptedPrivateKeyInfo GetInstance(object obj)
		{
			if (obj is EncryptedPrivateKeyInfo)
			{
				return (EncryptedPrivateKeyInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedPrivateKeyInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x000557EC File Offset: 0x000557EC
		public AlgorithmIdentifier EncryptionAlgorithm
		{
			get
			{
				return this.algId;
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000557F4 File Offset: 0x000557F4
		public byte[] GetEncryptedData()
		{
			return this.data.GetOctets();
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00055804 File Offset: 0x00055804
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algId,
				this.data
			});
		}

		// Token: 0x040009DD RID: 2525
		private readonly AlgorithmIdentifier algId;

		// Token: 0x040009DE RID: 2526
		private readonly Asn1OctetString data;
	}
}
