using System;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B7 RID: 439
	public class RsaesOaepParameters : Asn1Encodable
	{
		// Token: 0x06000E45 RID: 3653 RVA: 0x000570B8 File Offset: 0x000570B8
		public static RsaesOaepParameters GetInstance(object obj)
		{
			if (obj is RsaesOaepParameters)
			{
				return (RsaesOaepParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsaesOaepParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0005710C File Offset: 0x0005710C
		public RsaesOaepParameters()
		{
			this.hashAlgorithm = RsaesOaepParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsaesOaepParameters.DefaultMaskGenFunction;
			this.pSourceAlgorithm = RsaesOaepParameters.DefaultPSourceAlgorithm;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00057138 File Offset: 0x00057138
		public RsaesOaepParameters(AlgorithmIdentifier hashAlgorithm, AlgorithmIdentifier maskGenAlgorithm, AlgorithmIdentifier pSourceAlgorithm)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.maskGenAlgorithm = maskGenAlgorithm;
			this.pSourceAlgorithm = pSourceAlgorithm;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00057158 File Offset: 0x00057158
		public RsaesOaepParameters(Asn1Sequence seq)
		{
			this.hashAlgorithm = RsaesOaepParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsaesOaepParameters.DefaultMaskGenFunction;
			this.pSourceAlgorithm = RsaesOaepParameters.DefaultPSourceAlgorithm;
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[num];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.hashAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.maskGenAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
					this.pSourceAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag");
				}
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00057210 File Offset: 0x00057210
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00057218 File Offset: 0x00057218
		public AlgorithmIdentifier MaskGenAlgorithm
		{
			get
			{
				return this.maskGenAlgorithm;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00057220 File Offset: 0x00057220
		public AlgorithmIdentifier PSourceAlgorithm
		{
			get
			{
				return this.pSourceAlgorithm;
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00057228 File Offset: 0x00057228
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (!this.hashAlgorithm.Equals(RsaesOaepParameters.DefaultHashAlgorithm))
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, this.hashAlgorithm));
			}
			if (!this.maskGenAlgorithm.Equals(RsaesOaepParameters.DefaultMaskGenFunction))
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 1, this.maskGenAlgorithm));
			}
			if (!this.pSourceAlgorithm.Equals(RsaesOaepParameters.DefaultPSourceAlgorithm))
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 2, this.pSourceAlgorithm));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000A8C RID: 2700
		private AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000A8D RID: 2701
		private AlgorithmIdentifier maskGenAlgorithm;

		// Token: 0x04000A8E RID: 2702
		private AlgorithmIdentifier pSourceAlgorithm;

		// Token: 0x04000A8F RID: 2703
		public static readonly AlgorithmIdentifier DefaultHashAlgorithm = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);

		// Token: 0x04000A90 RID: 2704
		public static readonly AlgorithmIdentifier DefaultMaskGenFunction = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, RsaesOaepParameters.DefaultHashAlgorithm);

		// Token: 0x04000A91 RID: 2705
		public static readonly AlgorithmIdentifier DefaultPSourceAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdPSpecified, new DerOctetString(new byte[0]));
	}
}
