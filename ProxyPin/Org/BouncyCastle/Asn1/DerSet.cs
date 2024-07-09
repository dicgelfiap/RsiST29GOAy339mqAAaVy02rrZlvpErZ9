using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200025B RID: 603
	public class DerSet : Asn1Set
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x0006A2E0 File Offset: 0x0006A2E0
		public static DerSet FromVector(Asn1EncodableVector elementVector)
		{
			if (elementVector.Count >= 1)
			{
				return new DerSet(elementVector);
			}
			return DerSet.Empty;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0006A2FC File Offset: 0x0006A2FC
		internal static DerSet FromVector(Asn1EncodableVector elementVector, bool needsSorting)
		{
			if (elementVector.Count >= 1)
			{
				return new DerSet(elementVector, needsSorting);
			}
			return DerSet.Empty;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0006A318 File Offset: 0x0006A318
		public DerSet()
		{
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0006A320 File Offset: 0x0006A320
		public DerSet(Asn1Encodable element) : base(element)
		{
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0006A32C File Offset: 0x0006A32C
		public DerSet(params Asn1Encodable[] elements) : base(elements)
		{
			base.Sort();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0006A33C File Offset: 0x0006A33C
		public DerSet(Asn1EncodableVector elementVector) : this(elementVector, true)
		{
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0006A348 File Offset: 0x0006A348
		internal DerSet(Asn1EncodableVector elementVector, bool needsSorting) : base(elementVector)
		{
			if (needsSorting)
			{
				base.Sort();
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0006A360 File Offset: 0x0006A360
		internal override void Encode(DerOutputStream derOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			DerOutputStream derOutputStream = new DerOutputStream(memoryStream);
			foreach (object obj in this)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				derOutputStream.WriteObject(obj2);
			}
			Platform.Dispose(derOutputStream);
			byte[] bytes = memoryStream.ToArray();
			derOut.WriteEncoded(49, bytes);
		}

		// Token: 0x04000D97 RID: 3479
		public static readonly DerSet Empty = new DerSet();
	}
}
