using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001DD RID: 477
	public class SemanticsInformation : Asn1Encodable
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x0005C014 File Offset: 0x0005C014
		public static SemanticsInformation GetInstance(object obj)
		{
			if (obj == null || obj is SemanticsInformation)
			{
				return (SemanticsInformation)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SemanticsInformation(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0005C070 File Offset: 0x0005C070
		public SemanticsInformation(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("no objects in SemanticsInformation");
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			object obj = enumerator.Current;
			if (obj is DerObjectIdentifier)
			{
				this.semanticsIdentifier = DerObjectIdentifier.GetInstance(obj);
				if (enumerator.MoveNext())
				{
					obj = enumerator.Current;
				}
				else
				{
					obj = null;
				}
			}
			if (obj != null)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(obj);
				this.nameRegistrationAuthorities = new GeneralName[instance.Count];
				for (int i = 0; i < instance.Count; i++)
				{
					this.nameRegistrationAuthorities[i] = GeneralName.GetInstance(instance[i]);
				}
			}
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0005C130 File Offset: 0x0005C130
		public SemanticsInformation(DerObjectIdentifier semanticsIdentifier, GeneralName[] generalNames)
		{
			this.semanticsIdentifier = semanticsIdentifier;
			this.nameRegistrationAuthorities = generalNames;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0005C148 File Offset: 0x0005C148
		public SemanticsInformation(DerObjectIdentifier semanticsIdentifier)
		{
			this.semanticsIdentifier = semanticsIdentifier;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0005C158 File Offset: 0x0005C158
		public SemanticsInformation(GeneralName[] generalNames)
		{
			this.nameRegistrationAuthorities = generalNames;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0005C168 File Offset: 0x0005C168
		public DerObjectIdentifier SemanticsIdentifier
		{
			get
			{
				return this.semanticsIdentifier;
			}
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0005C170 File Offset: 0x0005C170
		public GeneralName[] GetNameRegistrationAuthorities()
		{
			return this.nameRegistrationAuthorities;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0005C178 File Offset: 0x0005C178
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.semanticsIdentifier
			});
			if (this.nameRegistrationAuthorities != null)
			{
				asn1EncodableVector.Add(new DerSequence(this.nameRegistrationAuthorities));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000B83 RID: 2947
		private readonly DerObjectIdentifier semanticsIdentifier;

		// Token: 0x04000B84 RID: 2948
		private readonly GeneralName[] nameRegistrationAuthorities;
	}
}
