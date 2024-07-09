using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000631 RID: 1585
	public abstract class X509ExtensionBase : IX509Extension
	{
		// Token: 0x0600373E RID: 14142
		protected abstract X509Extensions GetX509Extensions();

		// Token: 0x0600373F RID: 14143 RVA: 0x00129060 File Offset: 0x00129060
		protected virtual ISet GetExtensionOids(bool critical)
		{
			X509Extensions x509Extensions = this.GetX509Extensions();
			if (x509Extensions != null)
			{
				HashSet hashSet = new HashSet();
				foreach (object obj in x509Extensions.ExtensionOids)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					X509Extension extension = x509Extensions.GetExtension(derObjectIdentifier);
					if (extension.IsCritical == critical)
					{
						hashSet.Add(derObjectIdentifier.Id);
					}
				}
				return hashSet;
			}
			return null;
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x001290F8 File Offset: 0x001290F8
		public virtual ISet GetNonCriticalExtensionOids()
		{
			return this.GetExtensionOids(false);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x00129104 File Offset: 0x00129104
		public virtual ISet GetCriticalExtensionOids()
		{
			return this.GetExtensionOids(true);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x00129110 File Offset: 0x00129110
		[Obsolete("Use version taking a DerObjectIdentifier instead")]
		public Asn1OctetString GetExtensionValue(string oid)
		{
			return this.GetExtensionValue(new DerObjectIdentifier(oid));
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x00129120 File Offset: 0x00129120
		public virtual Asn1OctetString GetExtensionValue(DerObjectIdentifier oid)
		{
			X509Extensions x509Extensions = this.GetX509Extensions();
			if (x509Extensions != null)
			{
				X509Extension extension = x509Extensions.GetExtension(oid);
				if (extension != null)
				{
					return extension.Value;
				}
			}
			return null;
		}
	}
}
