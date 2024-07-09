using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000E7 RID: 231
	public class PkiFreeText : Asn1Encodable
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x00042A14 File Offset: 0x00042A14
		public static PkiFreeText GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PkiFreeText.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00042A24 File Offset: 0x00042A24
		public static PkiFreeText GetInstance(object obj)
		{
			if (obj is PkiFreeText)
			{
				return (PkiFreeText)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiFreeText((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00042A78 File Offset: 0x00042A78
		public PkiFreeText(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				if (!(obj is DerUtf8String))
				{
					throw new ArgumentException("attempt to insert non UTF8 STRING into PkiFreeText");
				}
			}
			this.strings = seq;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00042AEC File Offset: 0x00042AEC
		public PkiFreeText(DerUtf8String p)
		{
			this.strings = new DerSequence(p);
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00042B00 File Offset: 0x00042B00
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.strings.Count;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00042B10 File Offset: 0x00042B10
		public int Count
		{
			get
			{
				return this.strings.Count;
			}
		}

		// Token: 0x170001E9 RID: 489
		public DerUtf8String this[int index]
		{
			get
			{
				return (DerUtf8String)this.strings[index];
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00042B34 File Offset: 0x00042B34
		[Obsolete("Use 'object[index]' syntax instead")]
		public DerUtf8String GetStringAt(int index)
		{
			return this[index];
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00042B40 File Offset: 0x00042B40
		public override Asn1Object ToAsn1Object()
		{
			return this.strings;
		}

		// Token: 0x04000657 RID: 1623
		internal Asn1Sequence strings;
	}
}
