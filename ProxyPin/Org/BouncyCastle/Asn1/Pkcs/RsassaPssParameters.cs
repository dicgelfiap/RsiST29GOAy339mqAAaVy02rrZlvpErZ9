using System;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B9 RID: 441
	public class RsassaPssParameters : Asn1Encodable
	{
		// Token: 0x06000E5B RID: 3675 RVA: 0x00057594 File Offset: 0x00057594
		public static RsassaPssParameters GetInstance(object obj)
		{
			if (obj == null || obj is RsassaPssParameters)
			{
				return (RsassaPssParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsassaPssParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000575F0 File Offset: 0x000575F0
		public RsassaPssParameters()
		{
			this.hashAlgorithm = RsassaPssParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsassaPssParameters.DefaultMaskGenFunction;
			this.saltLength = RsassaPssParameters.DefaultSaltLength;
			this.trailerField = RsassaPssParameters.DefaultTrailerField;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00057624 File Offset: 0x00057624
		public RsassaPssParameters(AlgorithmIdentifier hashAlgorithm, AlgorithmIdentifier maskGenAlgorithm, DerInteger saltLength, DerInteger trailerField)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.maskGenAlgorithm = maskGenAlgorithm;
			this.saltLength = saltLength;
			this.trailerField = trailerField;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0005764C File Offset: 0x0005764C
		public RsassaPssParameters(Asn1Sequence seq)
		{
			this.hashAlgorithm = RsassaPssParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsassaPssParameters.DefaultMaskGenFunction;
			this.saltLength = RsassaPssParameters.DefaultSaltLength;
			this.trailerField = RsassaPssParameters.DefaultTrailerField;
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
					this.saltLength = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				case 3:
					this.trailerField = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag");
				}
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00057728 File Offset: 0x00057728
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00057730 File Offset: 0x00057730
		public AlgorithmIdentifier MaskGenAlgorithm
		{
			get
			{
				return this.maskGenAlgorithm;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00057738 File Offset: 0x00057738
		public DerInteger SaltLength
		{
			get
			{
				return this.saltLength;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00057740 File Offset: 0x00057740
		public DerInteger TrailerField
		{
			get
			{
				return this.trailerField;
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00057748 File Offset: 0x00057748
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (!this.hashAlgorithm.Equals(RsassaPssParameters.DefaultHashAlgorithm))
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, this.hashAlgorithm));
			}
			if (!this.maskGenAlgorithm.Equals(RsassaPssParameters.DefaultMaskGenFunction))
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 1, this.maskGenAlgorithm));
			}
			if (!this.saltLength.Equals(RsassaPssParameters.DefaultSaltLength))
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 2, this.saltLength));
			}
			if (!this.trailerField.Equals(RsassaPssParameters.DefaultTrailerField))
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 3, this.trailerField));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000A9A RID: 2714
		private AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000A9B RID: 2715
		private AlgorithmIdentifier maskGenAlgorithm;

		// Token: 0x04000A9C RID: 2716
		private DerInteger saltLength;

		// Token: 0x04000A9D RID: 2717
		private DerInteger trailerField;

		// Token: 0x04000A9E RID: 2718
		public static readonly AlgorithmIdentifier DefaultHashAlgorithm = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);

		// Token: 0x04000A9F RID: 2719
		public static readonly AlgorithmIdentifier DefaultMaskGenFunction = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, RsassaPssParameters.DefaultHashAlgorithm);

		// Token: 0x04000AA0 RID: 2720
		public static readonly DerInteger DefaultSaltLength = new DerInteger(20);

		// Token: 0x04000AA1 RID: 2721
		public static readonly DerInteger DefaultTrailerField = new DerInteger(1);
	}
}
