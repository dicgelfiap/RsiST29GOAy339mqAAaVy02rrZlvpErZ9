using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000257 RID: 599
	public class DerSequence : Asn1Sequence
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x0006A0B8 File Offset: 0x0006A0B8
		public static DerSequence FromVector(Asn1EncodableVector elementVector)
		{
			if (elementVector.Count >= 1)
			{
				return new DerSequence(elementVector);
			}
			return DerSequence.Empty;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0006A0D4 File Offset: 0x0006A0D4
		public DerSequence()
		{
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0006A0DC File Offset: 0x0006A0DC
		public DerSequence(Asn1Encodable element) : base(element)
		{
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0006A0E8 File Offset: 0x0006A0E8
		public DerSequence(params Asn1Encodable[] elements) : base(elements)
		{
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0006A0F4 File Offset: 0x0006A0F4
		public DerSequence(Asn1EncodableVector elementVector) : base(elementVector)
		{
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0006A100 File Offset: 0x0006A100
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
			derOut.WriteEncoded(48, bytes);
		}

		// Token: 0x04000D94 RID: 3476
		public static readonly DerSequence Empty = new DerSequence();
	}
}
