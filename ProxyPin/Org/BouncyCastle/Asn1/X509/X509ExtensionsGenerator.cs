using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000223 RID: 547
	public class X509ExtensionsGenerator
	{
		// Token: 0x060011AE RID: 4526 RVA: 0x00064028 File Offset: 0x00064028
		public void Reset()
		{
			this.extensions = Platform.CreateHashtable();
			this.extOrdering = Platform.CreateArrayList();
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00064040 File Offset: 0x00064040
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extValue)
		{
			byte[] derEncoded;
			try
			{
				derEncoded = extValue.GetDerEncoded();
			}
			catch (Exception arg)
			{
				throw new ArgumentException("error encoding value: " + arg);
			}
			this.AddExtension(oid, critical, derEncoded);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00064084 File Offset: 0x00064084
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extValue)
		{
			if (this.extensions.Contains(oid))
			{
				throw new ArgumentException("extension " + oid + " already added");
			}
			this.extOrdering.Add(oid);
			this.extensions.Add(oid, new X509Extension(critical, new DerOctetString(extValue)));
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x000640E4 File Offset: 0x000640E4
		public bool IsEmpty
		{
			get
			{
				return this.extOrdering.Count < 1;
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000640F4 File Offset: 0x000640F4
		public X509Extensions Generate()
		{
			return new X509Extensions(this.extOrdering, this.extensions);
		}

		// Token: 0x04000CAC RID: 3244
		private IDictionary extensions = Platform.CreateHashtable();

		// Token: 0x04000CAD RID: 3245
		private IList extOrdering = Platform.CreateArrayList();
	}
}
