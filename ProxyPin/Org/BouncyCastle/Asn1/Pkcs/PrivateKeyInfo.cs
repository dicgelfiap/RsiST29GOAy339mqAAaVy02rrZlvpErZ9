using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B5 RID: 437
	public class PrivateKeyInfo : Asn1Encodable
	{
		// Token: 0x06000E30 RID: 3632 RVA: 0x00056CAC File Offset: 0x00056CAC
		public static PrivateKeyInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return PrivateKeyInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00056CBC File Offset: 0x00056CBC
		public static PrivateKeyInfo GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is PrivateKeyInfo)
			{
				return (PrivateKeyInfo)obj;
			}
			return new PrivateKeyInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00056CE4 File Offset: 0x00056CE4
		private static int GetVersionValue(DerInteger version)
		{
			BigInteger value = version.Value;
			if (value.CompareTo(BigInteger.Zero) < 0 || value.CompareTo(BigInteger.One) > 0)
			{
				throw new ArgumentException("invalid version for private key info", "version");
			}
			return value.IntValue;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00056D34 File Offset: 0x00056D34
		public PrivateKeyInfo(AlgorithmIdentifier privateKeyAlgorithm, Asn1Encodable privateKey) : this(privateKeyAlgorithm, privateKey, null, null)
		{
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00056D40 File Offset: 0x00056D40
		public PrivateKeyInfo(AlgorithmIdentifier privateKeyAlgorithm, Asn1Encodable privateKey, Asn1Set attributes) : this(privateKeyAlgorithm, privateKey, attributes, null)
		{
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00056D4C File Offset: 0x00056D4C
		public PrivateKeyInfo(AlgorithmIdentifier privateKeyAlgorithm, Asn1Encodable privateKey, Asn1Set attributes, byte[] publicKey)
		{
			this.version = new DerInteger((publicKey != null) ? BigInteger.One : BigInteger.Zero);
			this.privateKeyAlgorithm = privateKeyAlgorithm;
			this.privateKey = new DerOctetString(privateKey);
			this.attributes = attributes;
			this.publicKey = ((publicKey == null) ? null : new DerBitString(publicKey));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00056DB8 File Offset: 0x00056DB8
		private PrivateKeyInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			this.version = DerInteger.GetInstance(CollectionUtilities.RequireNext(enumerator));
			int versionValue = PrivateKeyInfo.GetVersionValue(this.version);
			this.privateKeyAlgorithm = AlgorithmIdentifier.GetInstance(CollectionUtilities.RequireNext(enumerator));
			this.privateKey = Asn1OctetString.GetInstance(CollectionUtilities.RequireNext(enumerator));
			int num = -1;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				int tagNo = asn1TaggedObject.TagNo;
				if (tagNo <= num)
				{
					throw new ArgumentException("invalid optional field in private key info", "seq");
				}
				num = tagNo;
				switch (tagNo)
				{
				case 0:
					this.attributes = Asn1Set.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					if (versionValue < 1)
					{
						throw new ArgumentException("'publicKey' requires version v2(1) or later", "seq");
					}
					this.publicKey = DerBitString.GetInstance(asn1TaggedObject, false);
					break;
				default:
					throw new ArgumentException("unknown optional field in private key info", "seq");
				}
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00056EBC File Offset: 0x00056EBC
		public virtual Asn1Set Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00056EC4 File Offset: 0x00056EC4
		public virtual bool HasPublicKey
		{
			get
			{
				return this.publicKey != null;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00056ED4 File Offset: 0x00056ED4
		public virtual AlgorithmIdentifier PrivateKeyAlgorithm
		{
			get
			{
				return this.privateKeyAlgorithm;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00056EDC File Offset: 0x00056EDC
		public virtual Asn1Object ParsePrivateKey()
		{
			return Asn1Object.FromByteArray(this.privateKey.GetOctets());
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00056EF0 File Offset: 0x00056EF0
		public virtual Asn1Object ParsePublicKey()
		{
			if (this.publicKey != null)
			{
				return Asn1Object.FromByteArray(this.publicKey.GetOctets());
			}
			return null;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00056F10 File Offset: 0x00056F10
		public virtual DerBitString PublicKeyData
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00056F18 File Offset: 0x00056F18
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.privateKeyAlgorithm,
				this.privateKey
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.attributes);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.publicKey);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000A85 RID: 2693
		private readonly DerInteger version;

		// Token: 0x04000A86 RID: 2694
		private readonly AlgorithmIdentifier privateKeyAlgorithm;

		// Token: 0x04000A87 RID: 2695
		private readonly Asn1OctetString privateKey;

		// Token: 0x04000A88 RID: 2696
		private readonly Asn1Set attributes;

		// Token: 0x04000A89 RID: 2697
		private readonly DerBitString publicKey;
	}
}
