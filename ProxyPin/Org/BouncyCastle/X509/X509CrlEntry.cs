using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200071D RID: 1821
	public class X509CrlEntry : X509ExtensionBase
	{
		// Token: 0x06003FD3 RID: 16339 RVA: 0x0015DE84 File Offset: 0x0015DE84
		public X509CrlEntry(CrlEntry c)
		{
			this.c = c;
			this.certificateIssuer = this.loadCertificateIssuer();
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x0015DEA0 File Offset: 0x0015DEA0
		public X509CrlEntry(CrlEntry c, bool isIndirect, X509Name previousCertificateIssuer)
		{
			this.c = c;
			this.isIndirect = isIndirect;
			this.previousCertificateIssuer = previousCertificateIssuer;
			this.certificateIssuer = this.loadCertificateIssuer();
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x0015DECC File Offset: 0x0015DECC
		private X509Name loadCertificateIssuer()
		{
			if (!this.isIndirect)
			{
				return null;
			}
			Asn1OctetString extensionValue = this.GetExtensionValue(X509Extensions.CertificateIssuer);
			if (extensionValue == null)
			{
				return this.previousCertificateIssuer;
			}
			try
			{
				GeneralName[] names = GeneralNames.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).GetNames();
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].TagNo == 4)
					{
						return X509Name.GetInstance(names[i].Name);
					}
				}
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x0015DF68 File Offset: 0x0015DF68
		public X509Name GetCertificateIssuer()
		{
			return this.certificateIssuer;
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x0015DF70 File Offset: 0x0015DF70
		protected override X509Extensions GetX509Extensions()
		{
			return this.c.Extensions;
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x0015DF80 File Offset: 0x0015DF80
		public byte[] GetEncoded()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.c.GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CrlException(ex.ToString());
			}
			return derEncoded;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x0015DFBC File Offset: 0x0015DFBC
		public BigInteger SerialNumber
		{
			get
			{
				return this.c.UserCertificate.Value;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x0015DFD0 File Offset: 0x0015DFD0
		public DateTime RevocationDate
		{
			get
			{
				return this.c.RevocationDate.ToDateTime();
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x0015DFE4 File Offset: 0x0015DFE4
		public bool HasExtensions
		{
			get
			{
				return this.c.Extensions != null;
			}
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x0015DFF8 File Offset: 0x0015DFF8
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			X509CrlEntry x509CrlEntry = other as X509CrlEntry;
			return x509CrlEntry != null && (!this.hashValueSet || !x509CrlEntry.hashValueSet || this.hashValue == x509CrlEntry.hashValue) && this.c.Equals(x509CrlEntry.c);
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x0015E064 File Offset: 0x0015E064
		public override int GetHashCode()
		{
			if (!this.hashValueSet)
			{
				this.hashValue = this.c.GetHashCode();
				this.hashValueSet = true;
			}
			return this.hashValue;
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x0015E098 File Offset: 0x0015E098
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("        userCertificate: ").Append(this.SerialNumber).Append(newLine);
			stringBuilder.Append("         revocationDate: ").Append(this.RevocationDate).Append(newLine);
			stringBuilder.Append("      certificateIssuer: ").Append(this.GetCertificateIssuer()).Append(newLine);
			X509Extensions extensions = this.c.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("   crlEntryExtensions:").Append(newLine);
					for (;;)
					{
						DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)enumerator.Current;
						X509Extension extension = extensions.GetExtension(derObjectIdentifier);
						if (extension.Value != null)
						{
							Asn1Object asn1Object = X509ExtensionUtilities.FromExtensionValue(extension.Value);
							stringBuilder.Append("                       critical(").Append(extension.IsCritical).Append(") ");
							try
							{
								if (derObjectIdentifier.Equals(X509Extensions.ReasonCode))
								{
									stringBuilder.Append(new CrlReason(DerEnumerated.GetInstance(asn1Object)));
								}
								else if (derObjectIdentifier.Equals(X509Extensions.CertificateIssuer))
								{
									stringBuilder.Append("Certificate issuer: ").Append(GeneralNames.GetInstance((Asn1Sequence)asn1Object));
								}
								else
								{
									stringBuilder.Append(derObjectIdentifier.Id);
									stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object));
								}
								stringBuilder.Append(newLine);
								goto IL_1BD;
							}
							catch (Exception)
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append("*****").Append(newLine);
								goto IL_1BD;
							}
							goto IL_1B5;
						}
						goto IL_1B5;
						IL_1BD:
						if (!enumerator.MoveNext())
						{
							break;
						}
						continue;
						IL_1B5:
						stringBuilder.Append(newLine);
						goto IL_1BD;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040020B4 RID: 8372
		private CrlEntry c;

		// Token: 0x040020B5 RID: 8373
		private bool isIndirect;

		// Token: 0x040020B6 RID: 8374
		private X509Name previousCertificateIssuer;

		// Token: 0x040020B7 RID: 8375
		private X509Name certificateIssuer;

		// Token: 0x040020B8 RID: 8376
		private volatile bool hashValueSet;

		// Token: 0x040020B9 RID: 8377
		private volatile int hashValue;
	}
}
