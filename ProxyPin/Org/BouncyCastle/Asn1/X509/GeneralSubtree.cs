using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001FB RID: 507
	public class GeneralSubtree : Asn1Encodable
	{
		// Token: 0x0600105E RID: 4190 RVA: 0x0005F908 File Offset: 0x0005F908
		private GeneralSubtree(Asn1Sequence seq)
		{
			this.baseName = GeneralName.GetInstance(seq[0]);
			switch (seq.Count)
			{
			case 1:
				return;
			case 2:
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[1]);
				switch (instance.TagNo)
				{
				case 0:
					this.minimum = DerInteger.GetInstance(instance, false);
					return;
				case 1:
					this.maximum = DerInteger.GetInstance(instance, false);
					return;
				default:
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
				break;
			}
			case 3:
			{
				Asn1TaggedObject instance2 = Asn1TaggedObject.GetInstance(seq[1]);
				if (instance2.TagNo != 0)
				{
					throw new ArgumentException("Bad tag number for 'minimum': " + instance2.TagNo);
				}
				this.minimum = DerInteger.GetInstance(instance2, false);
				Asn1TaggedObject instance3 = Asn1TaggedObject.GetInstance(seq[2]);
				if (instance3.TagNo != 1)
				{
					throw new ArgumentException("Bad tag number for 'maximum': " + instance3.TagNo);
				}
				this.maximum = DerInteger.GetInstance(instance3, false);
				return;
			}
			default:
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0005FA4C File Offset: 0x0005FA4C
		public GeneralSubtree(GeneralName baseName, BigInteger minimum, BigInteger maximum)
		{
			this.baseName = baseName;
			if (minimum != null)
			{
				this.minimum = new DerInteger(minimum);
			}
			if (maximum != null)
			{
				this.maximum = new DerInteger(maximum);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0005FA80 File Offset: 0x0005FA80
		public GeneralSubtree(GeneralName baseName) : this(baseName, null, null)
		{
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0005FA8C File Offset: 0x0005FA8C
		public static GeneralSubtree GetInstance(Asn1TaggedObject o, bool isExplicit)
		{
			return new GeneralSubtree(Asn1Sequence.GetInstance(o, isExplicit));
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0005FA9C File Offset: 0x0005FA9C
		public static GeneralSubtree GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is GeneralSubtree)
			{
				return (GeneralSubtree)obj;
			}
			return new GeneralSubtree(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0005FAC4 File Offset: 0x0005FAC4
		public GeneralName Base
		{
			get
			{
				return this.baseName;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0005FACC File Offset: 0x0005FACC
		public BigInteger Minimum
		{
			get
			{
				if (this.minimum != null)
				{
					return this.minimum.Value;
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0005FAEC File Offset: 0x0005FAEC
		public BigInteger Maximum
		{
			get
			{
				if (this.maximum != null)
				{
					return this.maximum.Value;
				}
				return null;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0005FB08 File Offset: 0x0005FB08
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.baseName
			});
			if (this.minimum != null && this.minimum.Value.SignValue != 0)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 0, this.minimum));
			}
			asn1EncodableVector.AddOptionalTagged(false, 1, this.maximum);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000BEA RID: 3050
		private readonly GeneralName baseName;

		// Token: 0x04000BEB RID: 3051
		private readonly DerInteger minimum;

		// Token: 0x04000BEC RID: 3052
		private readonly DerInteger maximum;
	}
}
