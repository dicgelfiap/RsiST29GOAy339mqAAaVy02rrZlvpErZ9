using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F1 RID: 241
	public class PopoDecKeyChallContent : Asn1Encodable
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x00043960 File Offset: 0x00043960
		private PopoDecKeyChallContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00043970 File Offset: 0x00043970
		public static PopoDecKeyChallContent GetInstance(object obj)
		{
			if (obj is PopoDecKeyChallContent)
			{
				return (PopoDecKeyChallContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PopoDecKeyChallContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000439C4 File Offset: 0x000439C4
		public virtual Challenge[] ToChallengeArray()
		{
			Challenge[] array = new Challenge[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = Challenge.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00043A10 File Offset: 0x00043A10
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x0400068F RID: 1679
		private readonly Asn1Sequence content;
	}
}
