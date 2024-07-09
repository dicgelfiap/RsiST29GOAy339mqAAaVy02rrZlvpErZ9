using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200015E RID: 350
	public class SignaturePolicyId : Asn1Encodable
	{
		// Token: 0x06000BF9 RID: 3065 RVA: 0x0004E24C File Offset: 0x0004E24C
		public static SignaturePolicyId GetInstance(object obj)
		{
			if (obj == null || obj is SignaturePolicyId)
			{
				return (SignaturePolicyId)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignaturePolicyId((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'SignaturePolicyId' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0004E2A8 File Offset: 0x0004E2A8
		private SignaturePolicyId(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.sigPolicyIdentifier = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.sigPolicyHash = OtherHashAlgAndValue.GetInstance(seq[1].ToAsn1Object());
			if (seq.Count > 2)
			{
				this.sigPolicyQualifiers = (Asn1Sequence)seq[2].ToAsn1Object();
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0004E35C File Offset: 0x0004E35C
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash) : this(sigPolicyIdentifier, sigPolicyHash, null)
		{
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0004E368 File Offset: 0x0004E368
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash, params SigPolicyQualifierInfo[] sigPolicyQualifiers)
		{
			if (sigPolicyIdentifier == null)
			{
				throw new ArgumentNullException("sigPolicyIdentifier");
			}
			if (sigPolicyHash == null)
			{
				throw new ArgumentNullException("sigPolicyHash");
			}
			this.sigPolicyIdentifier = sigPolicyIdentifier;
			this.sigPolicyHash = sigPolicyHash;
			if (sigPolicyQualifiers != null)
			{
				this.sigPolicyQualifiers = new DerSequence(sigPolicyQualifiers);
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0004E3C4 File Offset: 0x0004E3C4
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash, IEnumerable sigPolicyQualifiers)
		{
			if (sigPolicyIdentifier == null)
			{
				throw new ArgumentNullException("sigPolicyIdentifier");
			}
			if (sigPolicyHash == null)
			{
				throw new ArgumentNullException("sigPolicyHash");
			}
			this.sigPolicyIdentifier = sigPolicyIdentifier;
			this.sigPolicyHash = sigPolicyHash;
			if (sigPolicyQualifiers != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(sigPolicyQualifiers, typeof(SigPolicyQualifierInfo)))
				{
					throw new ArgumentException("Must contain only 'SigPolicyQualifierInfo' objects", "sigPolicyQualifiers");
				}
				this.sigPolicyQualifiers = new DerSequence(Asn1EncodableVector.FromEnumerable(sigPolicyQualifiers));
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0004E448 File Offset: 0x0004E448
		public DerObjectIdentifier SigPolicyIdentifier
		{
			get
			{
				return this.sigPolicyIdentifier;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0004E450 File Offset: 0x0004E450
		public OtherHashAlgAndValue SigPolicyHash
		{
			get
			{
				return this.sigPolicyHash;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0004E458 File Offset: 0x0004E458
		public SigPolicyQualifierInfo[] GetSigPolicyQualifiers()
		{
			if (this.sigPolicyQualifiers == null)
			{
				return null;
			}
			SigPolicyQualifierInfo[] array = new SigPolicyQualifierInfo[this.sigPolicyQualifiers.Count];
			for (int i = 0; i < this.sigPolicyQualifiers.Count; i++)
			{
				array[i] = SigPolicyQualifierInfo.GetInstance(this.sigPolicyQualifiers[i]);
			}
			return array;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0004E4BC File Offset: 0x0004E4BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.sigPolicyIdentifier,
				this.sigPolicyHash.ToAsn1Object()
			});
			if (this.sigPolicyQualifiers != null)
			{
				asn1EncodableVector.Add(this.sigPolicyQualifiers.ToAsn1Object());
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000821 RID: 2081
		private readonly DerObjectIdentifier sigPolicyIdentifier;

		// Token: 0x04000822 RID: 2082
		private readonly OtherHashAlgAndValue sigPolicyHash;

		// Token: 0x04000823 RID: 2083
		private readonly Asn1Sequence sigPolicyQualifiers;
	}
}
