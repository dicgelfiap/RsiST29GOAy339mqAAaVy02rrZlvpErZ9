using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200071C RID: 1820
	public class X509Crl : X509ExtensionBase
	{
		// Token: 0x06003FBC RID: 16316 RVA: 0x0015D534 File Offset: 0x0015D534
		public X509Crl(CertificateList c)
		{
			this.c = c;
			try
			{
				this.sigAlgName = X509SignatureUtilities.GetSignatureName(c.SignatureAlgorithm);
				if (c.SignatureAlgorithm.Parameters != null)
				{
					this.sigAlgParams = c.SignatureAlgorithm.Parameters.GetDerEncoded();
				}
				else
				{
					this.sigAlgParams = null;
				}
				this.isIndirect = this.IsIndirectCrl;
			}
			catch (Exception arg)
			{
				throw new CrlException("CRL contents invalid: " + arg);
			}
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x0015D5C4 File Offset: 0x0015D5C4
		protected override X509Extensions GetX509Extensions()
		{
			if (this.c.Version < 2)
			{
				return null;
			}
			return this.c.TbsCertList.Extensions;
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x0015D5EC File Offset: 0x0015D5EC
		public virtual byte[] GetEncoded()
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

		// Token: 0x06003FBF RID: 16319 RVA: 0x0015D628 File Offset: 0x0015D628
		public virtual void Verify(AsymmetricKeyParameter publicKey)
		{
			this.Verify(new Asn1VerifierFactoryProvider(publicKey));
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x0015D638 File Offset: 0x0015D638
		public virtual void Verify(IVerifierFactoryProvider verifierProvider)
		{
			this.CheckSignature(verifierProvider.CreateVerifierFactory(this.c.SignatureAlgorithm));
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x0015D654 File Offset: 0x0015D654
		protected virtual void CheckSignature(IVerifierFactory verifier)
		{
			if (!this.c.SignatureAlgorithm.Equals(this.c.TbsCertList.Signature))
			{
				throw new CrlException("Signature algorithm on CertificateList does not match TbsCertList.");
			}
			Asn1Encodable parameters = this.c.SignatureAlgorithm.Parameters;
			IStreamCalculator streamCalculator = verifier.CreateCalculator();
			byte[] tbsCertList = this.GetTbsCertList();
			streamCalculator.Stream.Write(tbsCertList, 0, tbsCertList.Length);
			Platform.Dispose(streamCalculator.Stream);
			if (!((IVerifier)streamCalculator.GetResult()).IsVerified(this.GetSignature()))
			{
				throw new InvalidKeyException("CRL does not verify with supplied public key.");
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x0015D6F8 File Offset: 0x0015D6F8
		public virtual int Version
		{
			get
			{
				return this.c.Version;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x0015D708 File Offset: 0x0015D708
		public virtual X509Name IssuerDN
		{
			get
			{
				return this.c.Issuer;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x0015D718 File Offset: 0x0015D718
		public virtual DateTime ThisUpdate
		{
			get
			{
				return this.c.ThisUpdate.ToDateTime();
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x0015D72C File Offset: 0x0015D72C
		public virtual DateTimeObject NextUpdate
		{
			get
			{
				if (this.c.NextUpdate != null)
				{
					return new DateTimeObject(this.c.NextUpdate.ToDateTime());
				}
				return null;
			}
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x0015D758 File Offset: 0x0015D758
		private ISet LoadCrlEntries()
		{
			ISet set = new HashSet();
			IEnumerable revokedCertificateEnumeration = this.c.GetRevokedCertificateEnumeration();
			X509Name previousCertificateIssuer = this.IssuerDN;
			foreach (object obj in revokedCertificateEnumeration)
			{
				CrlEntry crlEntry = (CrlEntry)obj;
				X509CrlEntry x509CrlEntry = new X509CrlEntry(crlEntry, this.isIndirect, previousCertificateIssuer);
				set.Add(x509CrlEntry);
				previousCertificateIssuer = x509CrlEntry.GetCertificateIssuer();
			}
			return set;
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x0015D7EC File Offset: 0x0015D7EC
		public virtual X509CrlEntry GetRevokedCertificate(BigInteger serialNumber)
		{
			IEnumerable revokedCertificateEnumeration = this.c.GetRevokedCertificateEnumeration();
			X509Name previousCertificateIssuer = this.IssuerDN;
			foreach (object obj in revokedCertificateEnumeration)
			{
				CrlEntry crlEntry = (CrlEntry)obj;
				X509CrlEntry x509CrlEntry = new X509CrlEntry(crlEntry, this.isIndirect, previousCertificateIssuer);
				if (serialNumber.Equals(crlEntry.UserCertificate.Value))
				{
					return x509CrlEntry;
				}
				previousCertificateIssuer = x509CrlEntry.GetCertificateIssuer();
			}
			return null;
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x0015D894 File Offset: 0x0015D894
		public virtual ISet GetRevokedCertificates()
		{
			ISet set = this.LoadCrlEntries();
			if (set.Count > 0)
			{
				return set;
			}
			return null;
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x0015D8BC File Offset: 0x0015D8BC
		public virtual byte[] GetTbsCertList()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.c.TbsCertList.GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CrlException(ex.ToString());
			}
			return derEncoded;
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0015D900 File Offset: 0x0015D900
		public virtual byte[] GetSignature()
		{
			return this.c.GetSignatureOctets();
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x0015D910 File Offset: 0x0015D910
		public virtual string SigAlgName
		{
			get
			{
				return this.sigAlgName;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x0015D918 File Offset: 0x0015D918
		public virtual string SigAlgOid
		{
			get
			{
				return this.c.SignatureAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0015D930 File Offset: 0x0015D930
		public virtual byte[] GetSigAlgParams()
		{
			return Arrays.Clone(this.sigAlgParams);
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0015D940 File Offset: 0x0015D940
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			X509Crl x509Crl = other as X509Crl;
			if (x509Crl == null)
			{
				return false;
			}
			if (this.hashValueSet && x509Crl.hashValueSet)
			{
				if (this.hashValue != x509Crl.hashValue)
				{
					return false;
				}
			}
			else if (!this.c.Signature.Equals(x509Crl.c.Signature))
			{
				return false;
			}
			return this.c.Equals(x509Crl.c);
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x0015D9D0 File Offset: 0x0015D9D0
		public override int GetHashCode()
		{
			if (!this.hashValueSet)
			{
				this.hashValue = this.c.GetHashCode();
				this.hashValueSet = true;
			}
			return this.hashValue;
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x0015DA04 File Offset: 0x0015DA04
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("              Version: ").Append(this.Version).Append(newLine);
			stringBuilder.Append("             IssuerDN: ").Append(this.IssuerDN).Append(newLine);
			stringBuilder.Append("          This update: ").Append(this.ThisUpdate).Append(newLine);
			stringBuilder.Append("          Next update: ").Append(this.NextUpdate).Append(newLine);
			stringBuilder.Append("  Signature Algorithm: ").Append(this.SigAlgName).Append(newLine);
			byte[] signature = this.GetSignature();
			stringBuilder.Append("            Signature: ");
			stringBuilder.Append(Hex.ToHexString(signature, 0, 20)).Append(newLine);
			for (int i = 20; i < signature.Length; i += 20)
			{
				int length = Math.Min(20, signature.Length - i);
				stringBuilder.Append("                       ");
				stringBuilder.Append(Hex.ToHexString(signature, i, length)).Append(newLine);
			}
			X509Extensions extensions = this.c.TbsCertList.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("           Extensions: ").Append(newLine);
				}
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
							if (derObjectIdentifier.Equals(X509Extensions.CrlNumber))
							{
								stringBuilder.Append(new CrlNumber(DerInteger.GetInstance(asn1Object).PositiveValue)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.DeltaCrlIndicator))
							{
								stringBuilder.Append("Base CRL: " + new CrlNumber(DerInteger.GetInstance(asn1Object).PositiveValue)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.IssuingDistributionPoint))
							{
								stringBuilder.Append(IssuingDistributionPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.CrlDistributionPoints))
							{
								stringBuilder.Append(CrlDistPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.FreshestCrl))
							{
								stringBuilder.Append(CrlDistPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object)).Append(newLine);
							}
							goto IL_310;
						}
						catch (Exception)
						{
							stringBuilder.Append(derObjectIdentifier.Id);
							stringBuilder.Append(" value = ").Append("*****").Append(newLine);
							goto IL_310;
						}
						goto IL_308;
					}
					goto IL_308;
					IL_310:
					if (!enumerator.MoveNext())
					{
						break;
					}
					continue;
					IL_308:
					stringBuilder.Append(newLine);
					goto IL_310;
				}
			}
			ISet revokedCertificates = this.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				foreach (object obj in revokedCertificates)
				{
					X509CrlEntry value = (X509CrlEntry)obj;
					stringBuilder.Append(value);
					stringBuilder.Append(newLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x0015DDCC File Offset: 0x0015DDCC
		public virtual bool IsRevoked(X509Certificate cert)
		{
			CrlEntry[] revokedCertificates = this.c.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				BigInteger serialNumber = cert.SerialNumber;
				for (int i = 0; i < revokedCertificates.Length; i++)
				{
					if (revokedCertificates[i].UserCertificate.Value.Equals(serialNumber))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x0015DE28 File Offset: 0x0015DE28
		protected virtual bool IsIndirectCrl
		{
			get
			{
				Asn1OctetString extensionValue = this.GetExtensionValue(X509Extensions.IssuingDistributionPoint);
				bool result = false;
				try
				{
					if (extensionValue != null)
					{
						result = IssuingDistributionPoint.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).IsIndirectCrl;
					}
				}
				catch (Exception arg)
				{
					throw new CrlException("Exception reading IssuingDistributionPoint" + arg);
				}
				return result;
			}
		}

		// Token: 0x040020AE RID: 8366
		private readonly CertificateList c;

		// Token: 0x040020AF RID: 8367
		private readonly string sigAlgName;

		// Token: 0x040020B0 RID: 8368
		private readonly byte[] sigAlgParams;

		// Token: 0x040020B1 RID: 8369
		private readonly bool isIndirect;

		// Token: 0x040020B2 RID: 8370
		private volatile bool hashValueSet;

		// Token: 0x040020B3 RID: 8371
		private volatile int hashValue;
	}
}
