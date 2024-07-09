using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A8 RID: 424
	public class EncryptedData : Asn1Encodable
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x00055578 File Offset: 0x00055578
		public static EncryptedData GetInstance(object obj)
		{
			if (obj is EncryptedData)
			{
				return (EncryptedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000555CC File Offset: 0x000555CC
		private EncryptedData(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			int intValueExact = ((DerInteger)seq[0]).IntValueExact;
			if (intValueExact != 0)
			{
				throw new ArgumentException("sequence not version 0");
			}
			this.data = (Asn1Sequence)seq[1];
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00055634 File Offset: 0x00055634
		public EncryptedData(DerObjectIdentifier contentType, AlgorithmIdentifier encryptionAlgorithm, Asn1Encodable content)
		{
			this.data = new BerSequence(new Asn1Encodable[]
			{
				contentType,
				encryptionAlgorithm.ToAsn1Object(),
				new BerTaggedObject(false, 0, content)
			});
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00055684 File Offset: 0x00055684
		public DerObjectIdentifier ContentType
		{
			get
			{
				return (DerObjectIdentifier)this.data[0];
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00055698 File Offset: 0x00055698
		public AlgorithmIdentifier EncryptionAlgorithm
		{
			get
			{
				return AlgorithmIdentifier.GetInstance(this.data[1]);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000556AC File Offset: 0x000556AC
		public Asn1OctetString Content
		{
			get
			{
				if (this.data.Count == 3)
				{
					DerTaggedObject obj = (DerTaggedObject)this.data[2];
					return Asn1OctetString.GetInstance(obj, false);
				}
				return null;
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000556EC File Offset: 0x000556EC
		public override Asn1Object ToAsn1Object()
		{
			return new BerSequence(new Asn1Encodable[]
			{
				new DerInteger(0),
				this.data
			});
		}

		// Token: 0x040009DC RID: 2524
		private readonly Asn1Sequence data;
	}
}
