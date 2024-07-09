using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000E1 RID: 225
	public class PkiConfirmContent : Asn1Encodable
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x000423C8 File Offset: 0x000423C8
		public static PkiConfirmContent GetInstance(object obj)
		{
			if (obj is PkiConfirmContent)
			{
				return (PkiConfirmContent)obj;
			}
			if (obj is Asn1Null)
			{
				return new PkiConfirmContent();
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00042410 File Offset: 0x00042410
		public override Asn1Object ToAsn1Object()
		{
			return DerNull.Instance;
		}
	}
}
