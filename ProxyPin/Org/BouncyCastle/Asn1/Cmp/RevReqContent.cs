using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000F8 RID: 248
	public class RevReqContent : Asn1Encodable
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x00044184 File Offset: 0x00044184
		private RevReqContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00044194 File Offset: 0x00044194
		public static RevReqContent GetInstance(object obj)
		{
			if (obj is RevReqContent)
			{
				return (RevReqContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevReqContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000441E8 File Offset: 0x000441E8
		public RevReqContent(params RevDetails[] revDetails)
		{
			this.content = new DerSequence(revDetails);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000441FC File Offset: 0x000441FC
		public virtual RevDetails[] ToRevDetailsArray()
		{
			RevDetails[] array = new RevDetails[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = RevDetails.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00044248 File Offset: 0x00044248
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x040006A0 RID: 1696
		private readonly Asn1Sequence content;
	}
}
