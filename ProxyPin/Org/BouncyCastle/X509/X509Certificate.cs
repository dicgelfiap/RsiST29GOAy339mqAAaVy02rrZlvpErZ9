using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Misc;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000718 RID: 1816
	public class X509Certificate : X509ExtensionBase
	{
		// Token: 0x06003F81 RID: 16257 RVA: 0x0015C420 File Offset: 0x0015C420
		protected X509Certificate()
		{
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x0015C434 File Offset: 0x0015C434
		public X509Certificate(X509CertificateStructure c)
		{
			this.c = c;
			try
			{
				Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.19"));
				if (extensionValue != null)
				{
					this.basicConstraints = BasicConstraints.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				}
			}
			catch (Exception arg)
			{
				throw new CertificateParsingException("cannot construct BasicConstraints: " + arg);
			}
			try
			{
				Asn1OctetString extensionValue2 = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.15"));
				if (extensionValue2 != null)
				{
					DerBitString instance = DerBitString.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
					byte[] bytes = instance.GetBytes();
					int num = bytes.Length * 8 - instance.PadBits;
					this.keyUsage = new bool[(num < 9) ? 9 : num];
					for (int num2 = 0; num2 != num; num2++)
					{
						this.keyUsage[num2] = (((int)bytes[num2 / 8] & 128 >> num2 % 8) != 0);
					}
				}
				else
				{
					this.keyUsage = null;
				}
			}
			catch (Exception arg2)
			{
				throw new CertificateParsingException("cannot construct KeyUsage: " + arg2);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x0015C568 File Offset: 0x0015C568
		public virtual X509CertificateStructure CertificateStructure
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x0015C570 File Offset: 0x0015C570
		public virtual bool IsValidNow
		{
			get
			{
				return this.IsValid(DateTime.UtcNow);
			}
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x0015C580 File Offset: 0x0015C580
		public virtual bool IsValid(DateTime time)
		{
			return time.CompareTo(this.NotBefore) >= 0 && time.CompareTo(this.NotAfter) <= 0;
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x0015C5B4 File Offset: 0x0015C5B4
		public virtual void CheckValidity()
		{
			this.CheckValidity(DateTime.UtcNow);
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x0015C5C4 File Offset: 0x0015C5C4
		public virtual void CheckValidity(DateTime time)
		{
			if (time.CompareTo(this.NotAfter) > 0)
			{
				throw new CertificateExpiredException("certificate expired on " + this.c.EndDate.GetTime());
			}
			if (time.CompareTo(this.NotBefore) < 0)
			{
				throw new CertificateNotYetValidException("certificate not valid until " + this.c.StartDate.GetTime());
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x0015C648 File Offset: 0x0015C648
		public virtual int Version
		{
			get
			{
				return this.c.Version;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x0015C658 File Offset: 0x0015C658
		public virtual BigInteger SerialNumber
		{
			get
			{
				return this.c.SerialNumber.Value;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x0015C66C File Offset: 0x0015C66C
		public virtual X509Name IssuerDN
		{
			get
			{
				return this.c.Issuer;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x0015C67C File Offset: 0x0015C67C
		public virtual X509Name SubjectDN
		{
			get
			{
				return this.c.Subject;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x0015C68C File Offset: 0x0015C68C
		public virtual DateTime NotBefore
		{
			get
			{
				return this.c.StartDate.ToDateTime();
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x0015C6A0 File Offset: 0x0015C6A0
		public virtual DateTime NotAfter
		{
			get
			{
				return this.c.EndDate.ToDateTime();
			}
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x0015C6B4 File Offset: 0x0015C6B4
		public virtual byte[] GetTbsCertificate()
		{
			return this.c.TbsCertificate.GetDerEncoded();
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x0015C6C8 File Offset: 0x0015C6C8
		public virtual byte[] GetSignature()
		{
			return this.c.GetSignatureOctets();
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x0015C6D8 File Offset: 0x0015C6D8
		public virtual string SigAlgName
		{
			get
			{
				return SignerUtilities.GetEncodingName(this.c.SignatureAlgorithm.Algorithm);
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x0015C6F0 File Offset: 0x0015C6F0
		public virtual string SigAlgOid
		{
			get
			{
				return this.c.SignatureAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x0015C708 File Offset: 0x0015C708
		public virtual byte[] GetSigAlgParams()
		{
			if (this.c.SignatureAlgorithm.Parameters != null)
			{
				return this.c.SignatureAlgorithm.Parameters.GetDerEncoded();
			}
			return null;
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x0015C748 File Offset: 0x0015C748
		public virtual DerBitString IssuerUniqueID
		{
			get
			{
				return this.c.TbsCertificate.IssuerUniqueID;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x0015C75C File Offset: 0x0015C75C
		public virtual DerBitString SubjectUniqueID
		{
			get
			{
				return this.c.TbsCertificate.SubjectUniqueID;
			}
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x0015C770 File Offset: 0x0015C770
		public virtual bool[] GetKeyUsage()
		{
			return Arrays.Clone(this.keyUsage);
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x0015C780 File Offset: 0x0015C780
		public virtual IList GetExtendedKeyUsage()
		{
			Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.37"));
			if (extensionValue == null)
			{
				return null;
			}
			IList result;
			try
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				IList list = Platform.CreateArrayList();
				foreach (object obj in instance)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					list.Add(derObjectIdentifier.Id);
				}
				result = list;
			}
			catch (Exception exception)
			{
				throw new CertificateParsingException("error processing extended key usage extension", exception);
			}
			return result;
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x0015C838 File Offset: 0x0015C838
		public virtual int GetBasicConstraints()
		{
			if (this.basicConstraints == null || !this.basicConstraints.IsCA())
			{
				return -1;
			}
			if (this.basicConstraints.PathLenConstraint == null)
			{
				return int.MaxValue;
			}
			return this.basicConstraints.PathLenConstraint.IntValue;
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x0015C88C File Offset: 0x0015C88C
		public virtual ICollection GetSubjectAlternativeNames()
		{
			return this.GetAlternativeNames("2.5.29.17");
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x0015C89C File Offset: 0x0015C89C
		public virtual ICollection GetIssuerAlternativeNames()
		{
			return this.GetAlternativeNames("2.5.29.18");
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x0015C8AC File Offset: 0x0015C8AC
		protected virtual ICollection GetAlternativeNames(string oid)
		{
			Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier(oid));
			if (extensionValue == null)
			{
				return null;
			}
			Asn1Object obj = X509ExtensionUtilities.FromExtensionValue(extensionValue);
			GeneralNames instance = GeneralNames.GetInstance(obj);
			IList list = Platform.CreateArrayList();
			foreach (GeneralName generalName in instance.GetNames())
			{
				IList list2 = Platform.CreateArrayList();
				list2.Add(generalName.TagNo);
				list2.Add(generalName.Name.ToString());
				list.Add(list2);
			}
			return list;
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x0015C948 File Offset: 0x0015C948
		protected override X509Extensions GetX509Extensions()
		{
			if (this.c.Version < 3)
			{
				return null;
			}
			return this.c.TbsCertificate.Extensions;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x0015C970 File Offset: 0x0015C970
		public virtual AsymmetricKeyParameter GetPublicKey()
		{
			lock (this.cacheLock)
			{
				if (this.publicKeyValue != null)
				{
					return this.publicKeyValue;
				}
			}
			AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(this.c.SubjectPublicKeyInfo);
			AsymmetricKeyParameter result;
			lock (this.cacheLock)
			{
				if (this.publicKeyValue == null)
				{
					this.publicKeyValue = asymmetricKeyParameter;
				}
				result = this.publicKeyValue;
			}
			return result;
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x0015CA10 File Offset: 0x0015CA10
		public virtual byte[] GetEncoded()
		{
			return this.c.GetDerEncoded();
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x0015CA20 File Offset: 0x0015CA20
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			X509Certificate x509Certificate = other as X509Certificate;
			if (x509Certificate == null)
			{
				return false;
			}
			if (this.hashValueSet && x509Certificate.hashValueSet)
			{
				if (this.hashValue != x509Certificate.hashValue)
				{
					return false;
				}
			}
			else if (!this.c.Signature.Equals(x509Certificate.c.Signature))
			{
				return false;
			}
			return this.c.Equals(x509Certificate.c);
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x0015CAB0 File Offset: 0x0015CAB0
		public override int GetHashCode()
		{
			if (!this.hashValueSet)
			{
				this.hashValue = this.c.GetHashCode();
				this.hashValueSet = true;
			}
			return this.hashValue;
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x0015CAE4 File Offset: 0x0015CAE4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("  [0]         Version: ").Append(this.Version).Append(newLine);
			stringBuilder.Append("         SerialNumber: ").Append(this.SerialNumber).Append(newLine);
			stringBuilder.Append("             IssuerDN: ").Append(this.IssuerDN).Append(newLine);
			stringBuilder.Append("           Start Date: ").Append(this.NotBefore).Append(newLine);
			stringBuilder.Append("           Final Date: ").Append(this.NotAfter).Append(newLine);
			stringBuilder.Append("            SubjectDN: ").Append(this.SubjectDN).Append(newLine);
			stringBuilder.Append("           Public Key: ").Append(this.GetPublicKey()).Append(newLine);
			stringBuilder.Append("  Signature Algorithm: ").Append(this.SigAlgName).Append(newLine);
			byte[] signature = this.GetSignature();
			stringBuilder.Append("            Signature: ").Append(Hex.ToHexString(signature, 0, 20)).Append(newLine);
			for (int i = 20; i < signature.Length; i += 20)
			{
				int length = Math.Min(20, signature.Length - i);
				stringBuilder.Append("                       ").Append(Hex.ToHexString(signature, i, length)).Append(newLine);
			}
			X509Extensions extensions = this.c.TbsCertificate.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("       Extensions: \n");
				}
				do
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)enumerator.Current;
					X509Extension extension = extensions.GetExtension(derObjectIdentifier);
					if (extension.Value != null)
					{
						Asn1Object asn1Object = X509ExtensionUtilities.FromExtensionValue(extension.Value);
						stringBuilder.Append("                       critical(").Append(extension.IsCritical).Append(") ");
						try
						{
							if (derObjectIdentifier.Equals(X509Extensions.BasicConstraints))
							{
								stringBuilder.Append(BasicConstraints.GetInstance(asn1Object));
							}
							else if (derObjectIdentifier.Equals(X509Extensions.KeyUsage))
							{
								stringBuilder.Append(KeyUsage.GetInstance(asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.NetscapeCertType))
							{
								stringBuilder.Append(new NetscapeCertType((DerBitString)asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.NetscapeRevocationUrl))
							{
								stringBuilder.Append(new NetscapeRevocationUrl((DerIA5String)asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.VerisignCzagExtension))
							{
								stringBuilder.Append(new VerisignCzagExtension((DerIA5String)asn1Object));
							}
							else
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object));
							}
						}
						catch (Exception)
						{
							stringBuilder.Append(derObjectIdentifier.Id);
							stringBuilder.Append(" value = ").Append("*****");
						}
					}
					stringBuilder.Append(newLine);
				}
				while (enumerator.MoveNext());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x0015CE30 File Offset: 0x0015CE30
		public virtual void Verify(AsymmetricKeyParameter key)
		{
			this.CheckSignature(new Asn1VerifierFactory(this.c.SignatureAlgorithm, key));
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x0015CE4C File Offset: 0x0015CE4C
		public virtual void Verify(IVerifierFactoryProvider verifierProvider)
		{
			this.CheckSignature(verifierProvider.CreateVerifierFactory(this.c.SignatureAlgorithm));
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x0015CE68 File Offset: 0x0015CE68
		protected virtual void CheckSignature(IVerifierFactory verifier)
		{
			if (!X509Certificate.IsAlgIDEqual(this.c.SignatureAlgorithm, this.c.TbsCertificate.Signature))
			{
				throw new CertificateException("signature algorithm in TBS cert not same as outer cert");
			}
			Asn1Encodable parameters = this.c.SignatureAlgorithm.Parameters;
			IStreamCalculator streamCalculator = verifier.CreateCalculator();
			byte[] tbsCertificate = this.GetTbsCertificate();
			streamCalculator.Stream.Write(tbsCertificate, 0, tbsCertificate.Length);
			Platform.Dispose(streamCalculator.Stream);
			if (!((IVerifier)streamCalculator.GetResult()).IsVerified(this.GetSignature()))
			{
				throw new InvalidKeyException("Public key presented not for certificate signature");
			}
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x0015CF0C File Offset: 0x0015CF0C
		private static bool IsAlgIDEqual(AlgorithmIdentifier id1, AlgorithmIdentifier id2)
		{
			if (!id1.Algorithm.Equals(id2.Algorithm))
			{
				return false;
			}
			Asn1Encodable parameters = id1.Parameters;
			Asn1Encodable parameters2 = id2.Parameters;
			if (parameters == null == (parameters2 == null))
			{
				return object.Equals(parameters, parameters2);
			}
			if (parameters != null)
			{
				return parameters.ToAsn1Object() is Asn1Null;
			}
			return parameters2.ToAsn1Object() is Asn1Null;
		}

		// Token: 0x040020A0 RID: 8352
		private readonly X509CertificateStructure c;

		// Token: 0x040020A1 RID: 8353
		private readonly BasicConstraints basicConstraints;

		// Token: 0x040020A2 RID: 8354
		private readonly bool[] keyUsage;

		// Token: 0x040020A3 RID: 8355
		private readonly object cacheLock = new object();

		// Token: 0x040020A4 RID: 8356
		private AsymmetricKeyParameter publicKeyValue;

		// Token: 0x040020A5 RID: 8357
		private volatile bool hashValueSet;

		// Token: 0x040020A6 RID: 8358
		private volatile int hashValue;
	}
}
