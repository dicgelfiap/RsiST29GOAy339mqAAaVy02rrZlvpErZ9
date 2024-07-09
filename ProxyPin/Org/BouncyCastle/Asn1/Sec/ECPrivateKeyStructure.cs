using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Sec
{
	// Token: 0x020001BE RID: 446
	public class ECPrivateKeyStructure : Asn1Encodable
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x00057FA8 File Offset: 0x00057FA8
		public static ECPrivateKeyStructure GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is ECPrivateKeyStructure)
			{
				return (ECPrivateKeyStructure)obj;
			}
			return new ECPrivateKeyStructure(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00057FD0 File Offset: 0x00057FD0
		[Obsolete("Use 'GetInstance' instead")]
		public ECPrivateKeyStructure(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			this.seq = seq;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00057FF0 File Offset: 0x00057FF0
		[Obsolete("Use constructor which takes 'orderBitLength' instead, to guarantee correct encoding")]
		public ECPrivateKeyStructure(BigInteger key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.seq = new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(1),
				new DerOctetString(key.ToByteArrayUnsigned())
			});
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0005804C File Offset: 0x0005804C
		public ECPrivateKeyStructure(int orderBitLength, BigInteger key) : this(orderBitLength, key, null)
		{
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00058058 File Offset: 0x00058058
		[Obsolete("Use constructor which takes 'orderBitLength' instead, to guarantee correct encoding")]
		public ECPrivateKeyStructure(BigInteger key, Asn1Encodable parameters) : this(key, null, parameters)
		{
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00058064 File Offset: 0x00058064
		[Obsolete("Use constructor which takes 'orderBitLength' instead, to guarantee correct encoding")]
		public ECPrivateKeyStructure(BigInteger key, DerBitString publicKey, Asn1Encodable parameters)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(1),
				new DerOctetString(key.ToByteArrayUnsigned())
			});
			if (parameters != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, parameters));
			}
			if (publicKey != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 1, publicKey));
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000580F0 File Offset: 0x000580F0
		public ECPrivateKeyStructure(int orderBitLength, BigInteger key, Asn1Encodable parameters) : this(orderBitLength, key, null, parameters)
		{
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000580FC File Offset: 0x000580FC
		public ECPrivateKeyStructure(int orderBitLength, BigInteger key, DerBitString publicKey, Asn1Encodable parameters)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (orderBitLength < key.BitLength)
			{
				throw new ArgumentException("must be >= key bitlength", "orderBitLength");
			}
			byte[] str = BigIntegers.AsUnsignedByteArray((orderBitLength + 7) / 8, key);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(1),
				new DerOctetString(str)
			});
			if (parameters != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, parameters));
			}
			if (publicKey != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 1, publicKey));
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000581AC File Offset: 0x000581AC
		public virtual BigInteger GetKey()
		{
			Asn1OctetString asn1OctetString = (Asn1OctetString)this.seq[1];
			return new BigInteger(1, asn1OctetString.GetOctets());
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000581DC File Offset: 0x000581DC
		public virtual DerBitString GetPublicKey()
		{
			return (DerBitString)this.GetObjectInTag(1);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000581EC File Offset: 0x000581EC
		public virtual Asn1Object GetParameters()
		{
			return this.GetObjectInTag(0);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000581F8 File Offset: 0x000581F8
		private Asn1Object GetObjectInTag(int tagNo)
		{
			foreach (object obj in this.seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				Asn1Object asn1Object = asn1Encodable.ToAsn1Object();
				if (asn1Object is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Object;
					if (asn1TaggedObject.TagNo == tagNo)
					{
						return asn1TaggedObject.GetObject();
					}
				}
			}
			return null;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0005828C File Offset: 0x0005828C
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000AC6 RID: 2758
		private readonly Asn1Sequence seq;
	}
}
