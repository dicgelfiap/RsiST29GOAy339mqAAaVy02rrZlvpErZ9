using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000159 RID: 345
	public class OtherHashAlgAndValue : Asn1Encodable
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0004D6E8 File Offset: 0x0004D6E8
		public static OtherHashAlgAndValue GetInstance(object obj)
		{
			if (obj == null || obj is OtherHashAlgAndValue)
			{
				return (OtherHashAlgAndValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherHashAlgAndValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherHashAlgAndValue' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0004D744 File Offset: 0x0004D744
		private OtherHashAlgAndValue(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0].ToAsn1Object());
			this.hashValue = (Asn1OctetString)seq[1].ToAsn1Object();
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0004D7C8 File Offset: 0x0004D7C8
		public OtherHashAlgAndValue(AlgorithmIdentifier hashAlgorithm, byte[] hashValue)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (hashValue == null)
			{
				throw new ArgumentNullException("hashValue");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.hashValue = new DerOctetString(hashValue);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0004D808 File Offset: 0x0004D808
		public OtherHashAlgAndValue(AlgorithmIdentifier hashAlgorithm, Asn1OctetString hashValue)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (hashValue == null)
			{
				throw new ArgumentNullException("hashValue");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.hashValue = hashValue;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0004D840 File Offset: 0x0004D840
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0004D848 File Offset: 0x0004D848
		public byte[] GetHashValue()
		{
			return this.hashValue.GetOctets();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0004D858 File Offset: 0x0004D858
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				this.hashValue
			});
		}

		// Token: 0x04000816 RID: 2070
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000817 RID: 2071
		private readonly Asn1OctetString hashValue;
	}
}
