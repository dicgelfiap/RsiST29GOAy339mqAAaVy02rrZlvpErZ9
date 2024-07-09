using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000710 RID: 1808
	public class AttributeCertificateHolder : IX509Selector, ICloneable
	{
		// Token: 0x06003F38 RID: 16184 RVA: 0x0015AFA0 File Offset: 0x0015AFA0
		internal AttributeCertificateHolder(Asn1Sequence seq)
		{
			this.holder = Holder.GetInstance(seq);
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x0015AFB4 File Offset: 0x0015AFB4
		public AttributeCertificateHolder(X509Name issuerName, BigInteger serialNumber)
		{
			this.holder = new Holder(new IssuerSerial(this.GenerateGeneralNames(issuerName), new DerInteger(serialNumber)));
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x0015AFDC File Offset: 0x0015AFDC
		public AttributeCertificateHolder(X509Certificate cert)
		{
			X509Name issuerX509Principal;
			try
			{
				issuerX509Principal = PrincipalUtilities.GetIssuerX509Principal(cert);
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException(ex.Message);
			}
			this.holder = new Holder(new IssuerSerial(this.GenerateGeneralNames(issuerX509Principal), new DerInteger(cert.SerialNumber)));
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x0015B03C File Offset: 0x0015B03C
		public AttributeCertificateHolder(X509Name principal)
		{
			this.holder = new Holder(this.GenerateGeneralNames(principal));
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x0015B058 File Offset: 0x0015B058
		public AttributeCertificateHolder(int digestedObjectType, string digestAlgorithm, string otherObjectTypeID, byte[] objectDigest)
		{
			this.holder = new Holder(new ObjectDigestInfo(digestedObjectType, otherObjectTypeID, new AlgorithmIdentifier(new DerObjectIdentifier(digestAlgorithm)), Arrays.Clone(objectDigest)));
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x0015B084 File Offset: 0x0015B084
		public int DigestedObjectType
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.DigestedObjectType.IntValueExact;
				}
				return -1;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x0015B0B4 File Offset: 0x0015B0B4
		public string DigestAlgorithm
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.DigestAlgorithm.Algorithm.Id;
				}
				return null;
			}
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x0015B0EC File Offset: 0x0015B0EC
		public byte[] GetObjectDigest()
		{
			ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
			if (objectDigestInfo != null)
			{
				return objectDigestInfo.ObjectDigest.GetBytes();
			}
			return null;
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x0015B11C File Offset: 0x0015B11C
		public string OtherObjectTypeID
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.OtherObjectTypeID.Id;
				}
				return null;
			}
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x0015B14C File Offset: 0x0015B14C
		private GeneralNames GenerateGeneralNames(X509Name principal)
		{
			return new GeneralNames(new GeneralName(principal));
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x0015B15C File Offset: 0x0015B15C
		private bool MatchesDN(X509Name subject, GeneralNames targets)
		{
			GeneralName[] names = targets.GetNames();
			for (int num = 0; num != names.Length; num++)
			{
				GeneralName generalName = names[num];
				if (generalName.TagNo == 4)
				{
					try
					{
						if (X509Name.GetInstance(generalName.Name).Equivalent(subject))
						{
							return true;
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return false;
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x0015B1D0 File Offset: 0x0015B1D0
		private object[] GetNames(GeneralName[] names)
		{
			int num = 0;
			for (int num2 = 0; num2 != names.Length; num2++)
			{
				if (names[num2].TagNo == 4)
				{
					num++;
				}
			}
			object[] array = new object[num];
			int num3 = 0;
			for (int num4 = 0; num4 != names.Length; num4++)
			{
				if (names[num4].TagNo == 4)
				{
					array[num3++] = X509Name.GetInstance(names[num4].Name);
				}
			}
			return array;
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x0015B258 File Offset: 0x0015B258
		private X509Name[] GetPrincipals(GeneralNames names)
		{
			object[] names2 = this.GetNames(names.GetNames());
			int num = 0;
			for (int num2 = 0; num2 != names2.Length; num2++)
			{
				if (names2[num2] is X509Name)
				{
					num++;
				}
			}
			X509Name[] array = new X509Name[num];
			int num3 = 0;
			for (int num4 = 0; num4 != names2.Length; num4++)
			{
				if (names2[num4] is X509Name)
				{
					array[num3++] = (X509Name)names2[num4];
				}
			}
			return array;
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x0015B2DC File Offset: 0x0015B2DC
		public X509Name[] GetEntityNames()
		{
			if (this.holder.EntityName != null)
			{
				return this.GetPrincipals(this.holder.EntityName);
			}
			return null;
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x0015B304 File Offset: 0x0015B304
		public X509Name[] GetIssuer()
		{
			if (this.holder.BaseCertificateID != null)
			{
				return this.GetPrincipals(this.holder.BaseCertificateID.Issuer);
			}
			return null;
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x0015B330 File Offset: 0x0015B330
		public BigInteger SerialNumber
		{
			get
			{
				if (this.holder.BaseCertificateID != null)
				{
					return this.holder.BaseCertificateID.Serial.Value;
				}
				return null;
			}
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x0015B35C File Offset: 0x0015B35C
		public object Clone()
		{
			return new AttributeCertificateHolder((Asn1Sequence)this.holder.ToAsn1Object());
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x0015B374 File Offset: 0x0015B374
		public bool Match(X509Certificate x509Cert)
		{
			try
			{
				if (this.holder.BaseCertificateID != null)
				{
					return this.holder.BaseCertificateID.Serial.Value.Equals(x509Cert.SerialNumber) && this.MatchesDN(PrincipalUtilities.GetIssuerX509Principal(x509Cert), this.holder.BaseCertificateID.Issuer);
				}
				if (this.holder.EntityName != null && this.MatchesDN(PrincipalUtilities.GetSubjectX509Principal(x509Cert), this.holder.EntityName))
				{
					return true;
				}
				if (this.holder.ObjectDigestInfo != null)
				{
					IDigest digest = null;
					try
					{
						digest = DigestUtilities.GetDigest(this.DigestAlgorithm);
					}
					catch (Exception)
					{
						return false;
					}
					switch (this.DigestedObjectType)
					{
					case 0:
					{
						byte[] encoded = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(x509Cert.GetPublicKey()).GetEncoded();
						digest.BlockUpdate(encoded, 0, encoded.Length);
						break;
					}
					case 1:
					{
						byte[] encoded2 = x509Cert.GetEncoded();
						digest.BlockUpdate(encoded2, 0, encoded2.Length);
						break;
					}
					}
					if (!Arrays.AreEqual(DigestUtilities.DoFinal(digest), this.GetObjectDigest()))
					{
						return false;
					}
				}
			}
			catch (CertificateEncodingException)
			{
				return false;
			}
			return false;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x0015B4F0 File Offset: 0x0015B4F0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is AttributeCertificateHolder))
			{
				return false;
			}
			AttributeCertificateHolder attributeCertificateHolder = (AttributeCertificateHolder)obj;
			return this.holder.Equals(attributeCertificateHolder.holder);
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x0015B530 File Offset: 0x0015B530
		public override int GetHashCode()
		{
			return this.holder.GetHashCode();
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x0015B540 File Offset: 0x0015B540
		public bool Match(object obj)
		{
			return obj is X509Certificate && this.Match((X509Certificate)obj);
		}

		// Token: 0x04002095 RID: 8341
		internal readonly Holder holder;
	}
}
