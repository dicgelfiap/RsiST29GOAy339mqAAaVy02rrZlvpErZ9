using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200013A RID: 314
	public class PopoSigningKey : Asn1Encodable
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x0004A25C File Offset: 0x0004A25C
		private PopoSigningKey(Asn1Sequence seq)
		{
			int index = 0;
			if (seq[index] is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[index++];
				if (asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Unknown PopoSigningKeyInput tag: " + asn1TaggedObject.TagNo, "seq");
				}
				this.poposkInput = PopoSigningKeyInput.GetInstance(asn1TaggedObject.GetObject());
			}
			this.algorithmIdentifier = AlgorithmIdentifier.GetInstance(seq[index++]);
			this.signature = DerBitString.GetInstance(seq[index]);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0004A2FC File Offset: 0x0004A2FC
		public static PopoSigningKey GetInstance(object obj)
		{
			if (obj is PopoSigningKey)
			{
				return (PopoSigningKey)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PopoSigningKey((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0004A350 File Offset: 0x0004A350
		public static PopoSigningKey GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PopoSigningKey.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0004A360 File Offset: 0x0004A360
		public PopoSigningKey(PopoSigningKeyInput poposkIn, AlgorithmIdentifier aid, DerBitString signature)
		{
			this.poposkInput = poposkIn;
			this.algorithmIdentifier = aid;
			this.signature = signature;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0004A380 File Offset: 0x0004A380
		public virtual PopoSigningKeyInput PoposkInput
		{
			get
			{
				return this.poposkInput;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0004A388 File Offset: 0x0004A388
		public virtual AlgorithmIdentifier AlgorithmIdentifier
		{
			get
			{
				return this.algorithmIdentifier;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0004A390 File Offset: 0x0004A390
		public virtual DerBitString Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0004A398 File Offset: 0x0004A398
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(false, 0, this.poposkInput);
			asn1EncodableVector.Add(this.algorithmIdentifier);
			asn1EncodableVector.Add(this.signature);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400078B RID: 1931
		private readonly PopoSigningKeyInput poposkInput;

		// Token: 0x0400078C RID: 1932
		private readonly AlgorithmIdentifier algorithmIdentifier;

		// Token: 0x0400078D RID: 1933
		private readonly DerBitString signature;
	}
}
