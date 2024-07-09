using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001FD RID: 509
	public class IetfAttrSyntax : Asn1Encodable
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x0005FE14 File Offset: 0x0005FE14
		public IetfAttrSyntax(Asn1Sequence seq)
		{
			int num = 0;
			if (seq[0] is Asn1TaggedObject)
			{
				this.policyAuthority = GeneralNames.GetInstance((Asn1TaggedObject)seq[0], false);
				num++;
			}
			else if (seq.Count == 2)
			{
				this.policyAuthority = GeneralNames.GetInstance(seq[0]);
				num++;
			}
			if (!(seq[num] is Asn1Sequence))
			{
				throw new ArgumentException("Non-IetfAttrSyntax encoding");
			}
			seq = (Asn1Sequence)seq[num];
			foreach (object obj in seq)
			{
				Asn1Object asn1Object = (Asn1Object)obj;
				int num2;
				if (asn1Object is DerObjectIdentifier)
				{
					num2 = 2;
				}
				else if (asn1Object is DerUtf8String)
				{
					num2 = 3;
				}
				else
				{
					if (!(asn1Object is DerOctetString))
					{
						throw new ArgumentException("Bad value type encoding IetfAttrSyntax");
					}
					num2 = 1;
				}
				if (this.valueChoice < 0)
				{
					this.valueChoice = num2;
				}
				if (num2 != this.valueChoice)
				{
					throw new ArgumentException("Mix of value types in IetfAttrSyntax");
				}
				this.values.Add(asn1Object);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0005FF80 File Offset: 0x0005FF80
		public GeneralNames PolicyAuthority
		{
			get
			{
				return this.policyAuthority;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0005FF88 File Offset: 0x0005FF88
		public int ValueType
		{
			get
			{
				return this.valueChoice;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0005FF90 File Offset: 0x0005FF90
		public object[] GetValues()
		{
			if (this.ValueType == 1)
			{
				Asn1OctetString[] array = new Asn1OctetString[this.values.Count];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = (Asn1OctetString)this.values[num];
				}
				return array;
			}
			if (this.ValueType == 2)
			{
				DerObjectIdentifier[] array2 = new DerObjectIdentifier[this.values.Count];
				for (int num2 = 0; num2 != array2.Length; num2++)
				{
					array2[num2] = (DerObjectIdentifier)this.values[num2];
				}
				return array2;
			}
			DerUtf8String[] array3 = new DerUtf8String[this.values.Count];
			for (int num3 = 0; num3 != array3.Length; num3++)
			{
				array3[num3] = (DerUtf8String)this.values[num3];
			}
			return array3;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00060078 File Offset: 0x00060078
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.policyAuthority);
			asn1EncodableVector.Add(new DerSequence(this.values));
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000BF1 RID: 3057
		public const int ValueOctets = 1;

		// Token: 0x04000BF2 RID: 3058
		public const int ValueOid = 2;

		// Token: 0x04000BF3 RID: 3059
		public const int ValueUtf8 = 3;

		// Token: 0x04000BF4 RID: 3060
		internal readonly GeneralNames policyAuthority;

		// Token: 0x04000BF5 RID: 3061
		internal readonly Asn1EncodableVector values = new Asn1EncodableVector();

		// Token: 0x04000BF6 RID: 3062
		internal int valueChoice = -1;
	}
}
