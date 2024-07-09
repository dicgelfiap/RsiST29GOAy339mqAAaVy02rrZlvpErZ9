using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001D7 RID: 471
	public class BiometricData : Asn1Encodable
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x0005B97C File Offset: 0x0005B97C
		public static BiometricData GetInstance(object obj)
		{
			if (obj == null || obj is BiometricData)
			{
				return (BiometricData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new BiometricData(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0005B9D8 File Offset: 0x0005B9D8
		private BiometricData(Asn1Sequence seq)
		{
			this.typeOfBiometricData = TypeOfBiometricData.GetInstance(seq[0]);
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.biometricDataHash = Asn1OctetString.GetInstance(seq[2]);
			if (seq.Count > 3)
			{
				this.sourceDataUri = DerIA5String.GetInstance(seq[3]);
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0005BA44 File Offset: 0x0005BA44
		public BiometricData(TypeOfBiometricData typeOfBiometricData, AlgorithmIdentifier hashAlgorithm, Asn1OctetString biometricDataHash, DerIA5String sourceDataUri)
		{
			this.typeOfBiometricData = typeOfBiometricData;
			this.hashAlgorithm = hashAlgorithm;
			this.biometricDataHash = biometricDataHash;
			this.sourceDataUri = sourceDataUri;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0005BA6C File Offset: 0x0005BA6C
		public BiometricData(TypeOfBiometricData typeOfBiometricData, AlgorithmIdentifier hashAlgorithm, Asn1OctetString biometricDataHash)
		{
			this.typeOfBiometricData = typeOfBiometricData;
			this.hashAlgorithm = hashAlgorithm;
			this.biometricDataHash = biometricDataHash;
			this.sourceDataUri = null;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0005BA90 File Offset: 0x0005BA90
		public TypeOfBiometricData TypeOfBiometricData
		{
			get
			{
				return this.typeOfBiometricData;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0005BA98 File Offset: 0x0005BA98
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0005BAA0 File Offset: 0x0005BAA0
		public Asn1OctetString BiometricDataHash
		{
			get
			{
				return this.biometricDataHash;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0005BAA8 File Offset: 0x0005BAA8
		public DerIA5String SourceDataUri
		{
			get
			{
				return this.sourceDataUri;
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0005BAB0 File Offset: 0x0005BAB0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.typeOfBiometricData,
				this.hashAlgorithm,
				this.biometricDataHash
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.sourceDataUri
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000B6E RID: 2926
		private readonly TypeOfBiometricData typeOfBiometricData;

		// Token: 0x04000B6F RID: 2927
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000B70 RID: 2928
		private readonly Asn1OctetString biometricDataHash;

		// Token: 0x04000B71 RID: 2929
		private readonly DerIA5String sourceDataUri;
	}
}
