using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000204 RID: 516
	public class ObjectDigestInfo : Asn1Encodable
	{
		// Token: 0x060010A6 RID: 4262 RVA: 0x00060BE8 File Offset: 0x00060BE8
		public static ObjectDigestInfo GetInstance(object obj)
		{
			if (obj == null || obj is ObjectDigestInfo)
			{
				return (ObjectDigestInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ObjectDigestInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00060C44 File Offset: 0x00060C44
		public static ObjectDigestInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return ObjectDigestInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00060C54 File Offset: 0x00060C54
		public ObjectDigestInfo(int digestedObjectType, string otherObjectTypeID, AlgorithmIdentifier digestAlgorithm, byte[] objectDigest)
		{
			this.digestedObjectType = new DerEnumerated(digestedObjectType);
			if (digestedObjectType == 2)
			{
				this.otherObjectTypeID = new DerObjectIdentifier(otherObjectTypeID);
			}
			this.digestAlgorithm = digestAlgorithm;
			this.objectDigest = new DerBitString(objectDigest);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00060C90 File Offset: 0x00060C90
		private ObjectDigestInfo(Asn1Sequence seq)
		{
			if (seq.Count > 4 || seq.Count < 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.digestedObjectType = DerEnumerated.GetInstance(seq[0]);
			int num = 0;
			if (seq.Count == 4)
			{
				this.otherObjectTypeID = DerObjectIdentifier.GetInstance(seq[1]);
				num++;
			}
			this.digestAlgorithm = AlgorithmIdentifier.GetInstance(seq[1 + num]);
			this.objectDigest = DerBitString.GetInstance(seq[2 + num]);
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x00060D38 File Offset: 0x00060D38
		public DerEnumerated DigestedObjectType
		{
			get
			{
				return this.digestedObjectType;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00060D40 File Offset: 0x00060D40
		public DerObjectIdentifier OtherObjectTypeID
		{
			get
			{
				return this.otherObjectTypeID;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x00060D48 File Offset: 0x00060D48
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00060D50 File Offset: 0x00060D50
		public DerBitString ObjectDigest
		{
			get
			{
				return this.objectDigest;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00060D58 File Offset: 0x00060D58
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.digestedObjectType
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.otherObjectTypeID
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.digestAlgorithm,
				this.objectDigest
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C1B RID: 3099
		public const int PublicKey = 0;

		// Token: 0x04000C1C RID: 3100
		public const int PublicKeyCert = 1;

		// Token: 0x04000C1D RID: 3101
		public const int OtherObjectDigest = 2;

		// Token: 0x04000C1E RID: 3102
		internal readonly DerEnumerated digestedObjectType;

		// Token: 0x04000C1F RID: 3103
		internal readonly DerObjectIdentifier otherObjectTypeID;

		// Token: 0x04000C20 RID: 3104
		internal readonly AlgorithmIdentifier digestAlgorithm;

		// Token: 0x04000C21 RID: 3105
		internal readonly DerBitString objectDigest;
	}
}
