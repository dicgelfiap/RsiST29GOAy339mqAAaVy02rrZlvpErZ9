using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x0200070E RID: 1806
	public class X509CrlStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06003F1B RID: 16155 RVA: 0x0015A9D4 File Offset: 0x0015A9D4
		public X509CrlStoreSelector()
		{
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0015A9DC File Offset: 0x0015A9DC
		public X509CrlStoreSelector(X509CrlStoreSelector o)
		{
			this.certificateChecking = o.CertificateChecking;
			this.dateAndTime = o.DateAndTime;
			this.issuers = o.Issuers;
			this.maxCrlNumber = o.MaxCrlNumber;
			this.minCrlNumber = o.MinCrlNumber;
			this.deltaCrlIndicatorEnabled = o.DeltaCrlIndicatorEnabled;
			this.completeCrlEnabled = o.CompleteCrlEnabled;
			this.maxBaseCrlNumber = o.MaxBaseCrlNumber;
			this.attrCertChecking = o.AttrCertChecking;
			this.issuingDistributionPointEnabled = o.IssuingDistributionPointEnabled;
			this.issuingDistributionPoint = o.IssuingDistributionPoint;
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0015AA78 File Offset: 0x0015AA78
		public virtual object Clone()
		{
			return new X509CrlStoreSelector(this);
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x0015AA80 File Offset: 0x0015AA80
		// (set) Token: 0x06003F1F RID: 16159 RVA: 0x0015AA88 File Offset: 0x0015AA88
		public X509Certificate CertificateChecking
		{
			get
			{
				return this.certificateChecking;
			}
			set
			{
				this.certificateChecking = value;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x0015AA94 File Offset: 0x0015AA94
		// (set) Token: 0x06003F21 RID: 16161 RVA: 0x0015AA9C File Offset: 0x0015AA9C
		public DateTimeObject DateAndTime
		{
			get
			{
				return this.dateAndTime;
			}
			set
			{
				this.dateAndTime = value;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x0015AAA8 File Offset: 0x0015AAA8
		// (set) Token: 0x06003F23 RID: 16163 RVA: 0x0015AAB8 File Offset: 0x0015AAB8
		public ICollection Issuers
		{
			get
			{
				return Platform.CreateArrayList(this.issuers);
			}
			set
			{
				this.issuers = Platform.CreateArrayList(value);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x0015AAC8 File Offset: 0x0015AAC8
		// (set) Token: 0x06003F25 RID: 16165 RVA: 0x0015AAD0 File Offset: 0x0015AAD0
		public BigInteger MaxCrlNumber
		{
			get
			{
				return this.maxCrlNumber;
			}
			set
			{
				this.maxCrlNumber = value;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x0015AADC File Offset: 0x0015AADC
		// (set) Token: 0x06003F27 RID: 16167 RVA: 0x0015AAE4 File Offset: 0x0015AAE4
		public BigInteger MinCrlNumber
		{
			get
			{
				return this.minCrlNumber;
			}
			set
			{
				this.minCrlNumber = value;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x0015AAF0 File Offset: 0x0015AAF0
		// (set) Token: 0x06003F29 RID: 16169 RVA: 0x0015AAF8 File Offset: 0x0015AAF8
		public IX509AttributeCertificate AttrCertChecking
		{
			get
			{
				return this.attrCertChecking;
			}
			set
			{
				this.attrCertChecking = value;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x0015AB04 File Offset: 0x0015AB04
		// (set) Token: 0x06003F2B RID: 16171 RVA: 0x0015AB0C File Offset: 0x0015AB0C
		public bool CompleteCrlEnabled
		{
			get
			{
				return this.completeCrlEnabled;
			}
			set
			{
				this.completeCrlEnabled = value;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x0015AB18 File Offset: 0x0015AB18
		// (set) Token: 0x06003F2D RID: 16173 RVA: 0x0015AB20 File Offset: 0x0015AB20
		public bool DeltaCrlIndicatorEnabled
		{
			get
			{
				return this.deltaCrlIndicatorEnabled;
			}
			set
			{
				this.deltaCrlIndicatorEnabled = value;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x0015AB2C File Offset: 0x0015AB2C
		// (set) Token: 0x06003F2F RID: 16175 RVA: 0x0015AB3C File Offset: 0x0015AB3C
		public byte[] IssuingDistributionPoint
		{
			get
			{
				return Arrays.Clone(this.issuingDistributionPoint);
			}
			set
			{
				this.issuingDistributionPoint = Arrays.Clone(value);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x0015AB4C File Offset: 0x0015AB4C
		// (set) Token: 0x06003F31 RID: 16177 RVA: 0x0015AB54 File Offset: 0x0015AB54
		public bool IssuingDistributionPointEnabled
		{
			get
			{
				return this.issuingDistributionPointEnabled;
			}
			set
			{
				this.issuingDistributionPointEnabled = value;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x0015AB60 File Offset: 0x0015AB60
		// (set) Token: 0x06003F33 RID: 16179 RVA: 0x0015AB68 File Offset: 0x0015AB68
		public BigInteger MaxBaseCrlNumber
		{
			get
			{
				return this.maxBaseCrlNumber;
			}
			set
			{
				this.maxBaseCrlNumber = value;
			}
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x0015AB74 File Offset: 0x0015AB74
		public virtual bool Match(object obj)
		{
			X509Crl x509Crl = obj as X509Crl;
			if (x509Crl == null)
			{
				return false;
			}
			if (this.dateAndTime != null)
			{
				DateTime value = this.dateAndTime.Value;
				DateTime thisUpdate = x509Crl.ThisUpdate;
				DateTimeObject nextUpdate = x509Crl.NextUpdate;
				if (value.CompareTo(thisUpdate) < 0 || nextUpdate == null || value.CompareTo(nextUpdate.Value) >= 0)
				{
					return false;
				}
			}
			if (this.issuers != null)
			{
				X509Name issuerDN = x509Crl.IssuerDN;
				bool flag = false;
				foreach (object obj2 in this.issuers)
				{
					X509Name x509Name = (X509Name)obj2;
					if (x509Name.Equivalent(issuerDN, true))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			if (this.maxCrlNumber != null || this.minCrlNumber != null)
			{
				Asn1OctetString extensionValue = x509Crl.GetExtensionValue(X509Extensions.CrlNumber);
				if (extensionValue == null)
				{
					return false;
				}
				BigInteger positiveValue = DerInteger.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).PositiveValue;
				if (this.maxCrlNumber != null && positiveValue.CompareTo(this.maxCrlNumber) > 0)
				{
					return false;
				}
				if (this.minCrlNumber != null && positiveValue.CompareTo(this.minCrlNumber) < 0)
				{
					return false;
				}
			}
			DerInteger derInteger = null;
			try
			{
				Asn1OctetString extensionValue2 = x509Crl.GetExtensionValue(X509Extensions.DeltaCrlIndicator);
				if (extensionValue2 != null)
				{
					derInteger = DerInteger.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
				}
			}
			catch (Exception)
			{
				return false;
			}
			if (derInteger == null)
			{
				if (this.DeltaCrlIndicatorEnabled)
				{
					return false;
				}
			}
			else
			{
				if (this.CompleteCrlEnabled)
				{
					return false;
				}
				if (this.maxBaseCrlNumber != null && derInteger.PositiveValue.CompareTo(this.maxBaseCrlNumber) > 0)
				{
					return false;
				}
			}
			if (this.issuingDistributionPointEnabled)
			{
				Asn1OctetString extensionValue3 = x509Crl.GetExtensionValue(X509Extensions.IssuingDistributionPoint);
				if (this.issuingDistributionPoint == null)
				{
					if (extensionValue3 != null)
					{
						return false;
					}
				}
				else if (!Arrays.AreEqual(extensionValue3.GetOctets(), this.issuingDistributionPoint))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400208A RID: 8330
		private X509Certificate certificateChecking;

		// Token: 0x0400208B RID: 8331
		private DateTimeObject dateAndTime;

		// Token: 0x0400208C RID: 8332
		private ICollection issuers;

		// Token: 0x0400208D RID: 8333
		private BigInteger maxCrlNumber;

		// Token: 0x0400208E RID: 8334
		private BigInteger minCrlNumber;

		// Token: 0x0400208F RID: 8335
		private IX509AttributeCertificate attrCertChecking;

		// Token: 0x04002090 RID: 8336
		private bool completeCrlEnabled;

		// Token: 0x04002091 RID: 8337
		private bool deltaCrlIndicatorEnabled;

		// Token: 0x04002092 RID: 8338
		private byte[] issuingDistributionPoint;

		// Token: 0x04002093 RID: 8339
		private bool issuingDistributionPointEnabled;

		// Token: 0x04002094 RID: 8340
		private BigInteger maxBaseCrlNumber;
	}
}
