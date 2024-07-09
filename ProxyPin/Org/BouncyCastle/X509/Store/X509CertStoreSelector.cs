using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x02000309 RID: 777
	public class X509CertStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06001771 RID: 6001 RVA: 0x0007A924 File Offset: 0x0007A924
		public X509CertStoreSelector()
		{
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0007A934 File Offset: 0x0007A934
		public X509CertStoreSelector(X509CertStoreSelector o)
		{
			this.authorityKeyIdentifier = o.AuthorityKeyIdentifier;
			this.basicConstraints = o.BasicConstraints;
			this.certificate = o.Certificate;
			this.certificateValid = o.CertificateValid;
			this.extendedKeyUsage = o.ExtendedKeyUsage;
			this.ignoreX509NameOrdering = o.IgnoreX509NameOrdering;
			this.issuer = o.Issuer;
			this.keyUsage = o.KeyUsage;
			this.policy = o.Policy;
			this.privateKeyValid = o.PrivateKeyValid;
			this.serialNumber = o.SerialNumber;
			this.subject = o.Subject;
			this.subjectKeyIdentifier = o.SubjectKeyIdentifier;
			this.subjectPublicKey = o.SubjectPublicKey;
			this.subjectPublicKeyAlgID = o.SubjectPublicKeyAlgID;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0007AA08 File Offset: 0x0007AA08
		public virtual object Clone()
		{
			return new X509CertStoreSelector(this);
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x0007AA10 File Offset: 0x0007AA10
		// (set) Token: 0x06001775 RID: 6005 RVA: 0x0007AA20 File Offset: 0x0007AA20
		public byte[] AuthorityKeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.authorityKeyIdentifier);
			}
			set
			{
				this.authorityKeyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0007AA30 File Offset: 0x0007AA30
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x0007AA38 File Offset: 0x0007AA38
		public int BasicConstraints
		{
			get
			{
				return this.basicConstraints;
			}
			set
			{
				if (value < -2)
				{
					throw new ArgumentException("value can't be less than -2", "value");
				}
				this.basicConstraints = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0007AA5C File Offset: 0x0007AA5C
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x0007AA64 File Offset: 0x0007AA64
		public X509Certificate Certificate
		{
			get
			{
				return this.certificate;
			}
			set
			{
				this.certificate = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0007AA70 File Offset: 0x0007AA70
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x0007AA78 File Offset: 0x0007AA78
		public DateTimeObject CertificateValid
		{
			get
			{
				return this.certificateValid;
			}
			set
			{
				this.certificateValid = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0007AA84 File Offset: 0x0007AA84
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x0007AA94 File Offset: 0x0007AA94
		public ISet ExtendedKeyUsage
		{
			get
			{
				return X509CertStoreSelector.CopySet(this.extendedKeyUsage);
			}
			set
			{
				this.extendedKeyUsage = X509CertStoreSelector.CopySet(value);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0007AAA4 File Offset: 0x0007AAA4
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x0007AAAC File Offset: 0x0007AAAC
		public bool IgnoreX509NameOrdering
		{
			get
			{
				return this.ignoreX509NameOrdering;
			}
			set
			{
				this.ignoreX509NameOrdering = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0007AAB8 File Offset: 0x0007AAB8
		// (set) Token: 0x06001781 RID: 6017 RVA: 0x0007AAC0 File Offset: 0x0007AAC0
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
			set
			{
				this.issuer = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0007AACC File Offset: 0x0007AACC
		[Obsolete("Avoid working with X509Name objects in string form")]
		public string IssuerAsString
		{
			get
			{
				if (this.issuer == null)
				{
					return null;
				}
				return this.issuer.ToString();
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0007AAE8 File Offset: 0x0007AAE8
		// (set) Token: 0x06001784 RID: 6020 RVA: 0x0007AAF8 File Offset: 0x0007AAF8
		public bool[] KeyUsage
		{
			get
			{
				return X509CertStoreSelector.CopyBoolArray(this.keyUsage);
			}
			set
			{
				this.keyUsage = X509CertStoreSelector.CopyBoolArray(value);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0007AB08 File Offset: 0x0007AB08
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x0007AB18 File Offset: 0x0007AB18
		public ISet Policy
		{
			get
			{
				return X509CertStoreSelector.CopySet(this.policy);
			}
			set
			{
				this.policy = X509CertStoreSelector.CopySet(value);
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0007AB28 File Offset: 0x0007AB28
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x0007AB30 File Offset: 0x0007AB30
		public DateTimeObject PrivateKeyValid
		{
			get
			{
				return this.privateKeyValid;
			}
			set
			{
				this.privateKeyValid = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0007AB3C File Offset: 0x0007AB3C
		// (set) Token: 0x0600178A RID: 6026 RVA: 0x0007AB44 File Offset: 0x0007AB44
		public BigInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0007AB50 File Offset: 0x0007AB50
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x0007AB58 File Offset: 0x0007AB58
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0007AB64 File Offset: 0x0007AB64
		[Obsolete("Avoid working with X509Name objects in string form")]
		public string SubjectAsString
		{
			get
			{
				if (this.subject == null)
				{
					return null;
				}
				return this.subject.ToString();
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0007AB80 File Offset: 0x0007AB80
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x0007AB90 File Offset: 0x0007AB90
		public byte[] SubjectKeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.subjectKeyIdentifier);
			}
			set
			{
				this.subjectKeyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0007ABA0 File Offset: 0x0007ABA0
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0007ABA8 File Offset: 0x0007ABA8
		public SubjectPublicKeyInfo SubjectPublicKey
		{
			get
			{
				return this.subjectPublicKey;
			}
			set
			{
				this.subjectPublicKey = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0007ABB4 File Offset: 0x0007ABB4
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0007ABBC File Offset: 0x0007ABBC
		public DerObjectIdentifier SubjectPublicKeyAlgID
		{
			get
			{
				return this.subjectPublicKeyAlgID;
			}
			set
			{
				this.subjectPublicKeyAlgID = value;
			}
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0007ABC8 File Offset: 0x0007ABC8
		public virtual bool Match(object obj)
		{
			X509Certificate x509Certificate = obj as X509Certificate;
			if (x509Certificate == null)
			{
				return false;
			}
			if (!X509CertStoreSelector.MatchExtension(this.authorityKeyIdentifier, x509Certificate, X509Extensions.AuthorityKeyIdentifier))
			{
				return false;
			}
			if (this.basicConstraints != -1)
			{
				int num = x509Certificate.GetBasicConstraints();
				if (this.basicConstraints == -2)
				{
					if (num != -1)
					{
						return false;
					}
				}
				else if (num < this.basicConstraints)
				{
					return false;
				}
			}
			if (this.certificate != null && !this.certificate.Equals(x509Certificate))
			{
				return false;
			}
			if (this.certificateValid != null && !x509Certificate.IsValid(this.certificateValid.Value))
			{
				return false;
			}
			if (this.extendedKeyUsage != null)
			{
				IList list = x509Certificate.GetExtendedKeyUsage();
				if (list != null)
				{
					foreach (object obj2 in this.extendedKeyUsage)
					{
						DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj2;
						if (!list.Contains(derObjectIdentifier.Id))
						{
							return false;
						}
					}
				}
			}
			if (this.issuer != null && !this.issuer.Equivalent(x509Certificate.IssuerDN, !this.ignoreX509NameOrdering))
			{
				return false;
			}
			if (this.keyUsage != null)
			{
				bool[] array = x509Certificate.GetKeyUsage();
				if (array != null)
				{
					for (int i = 0; i < 9; i++)
					{
						if (this.keyUsage[i] && !array[i])
						{
							return false;
						}
					}
				}
			}
			if (this.policy != null)
			{
				Asn1OctetString extensionValue = x509Certificate.GetExtensionValue(X509Extensions.CertificatePolicies);
				if (extensionValue == null)
				{
					return false;
				}
				Asn1Sequence instance = Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				if (this.policy.Count < 1 && instance.Count < 1)
				{
					return false;
				}
				bool flag = false;
				foreach (object obj3 in instance)
				{
					PolicyInformation policyInformation = (PolicyInformation)obj3;
					if (this.policy.Contains(policyInformation.PolicyIdentifier))
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
			if (this.privateKeyValid != null)
			{
				Asn1OctetString extensionValue2 = x509Certificate.GetExtensionValue(X509Extensions.PrivateKeyUsagePeriod);
				if (extensionValue2 == null)
				{
					return false;
				}
				PrivateKeyUsagePeriod instance2 = PrivateKeyUsagePeriod.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
				DateTime value = this.privateKeyValid.Value;
				DateTime dateTime = instance2.NotAfter.ToDateTime();
				DateTime dateTime2 = instance2.NotBefore.ToDateTime();
				if (value.CompareTo(dateTime) > 0 || value.CompareTo(dateTime2) < 0)
				{
					return false;
				}
			}
			return (this.serialNumber == null || this.serialNumber.Equals(x509Certificate.SerialNumber)) && (this.subject == null || this.subject.Equivalent(x509Certificate.SubjectDN, !this.ignoreX509NameOrdering)) && X509CertStoreSelector.MatchExtension(this.subjectKeyIdentifier, x509Certificate, X509Extensions.SubjectKeyIdentifier) && (this.subjectPublicKey == null || this.subjectPublicKey.Equals(X509CertStoreSelector.GetSubjectPublicKey(x509Certificate))) && (this.subjectPublicKeyAlgID == null || this.subjectPublicKeyAlgID.Equals(X509CertStoreSelector.GetSubjectPublicKey(x509Certificate).AlgorithmID));
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0007AF68 File Offset: 0x0007AF68
		internal static bool IssuersMatch(X509Name a, X509Name b)
		{
			if (a != null)
			{
				return a.Equivalent(b, true);
			}
			return b == null;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0007AF80 File Offset: 0x0007AF80
		private static bool[] CopyBoolArray(bool[] b)
		{
			if (b != null)
			{
				return (bool[])b.Clone();
			}
			return null;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0007AF98 File Offset: 0x0007AF98
		private static ISet CopySet(ISet s)
		{
			if (s != null)
			{
				return new HashSet(s);
			}
			return null;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0007AFA8 File Offset: 0x0007AFA8
		private static SubjectPublicKeyInfo GetSubjectPublicKey(X509Certificate c)
		{
			return SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(c.GetPublicKey());
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0007AFB8 File Offset: 0x0007AFB8
		private static bool MatchExtension(byte[] b, X509Certificate c, DerObjectIdentifier oid)
		{
			if (b == null)
			{
				return true;
			}
			Asn1OctetString extensionValue = c.GetExtensionValue(oid);
			return extensionValue != null && Arrays.AreEqual(b, extensionValue.GetOctets());
		}

		// Token: 0x04000FBD RID: 4029
		private byte[] authorityKeyIdentifier;

		// Token: 0x04000FBE RID: 4030
		private int basicConstraints = -1;

		// Token: 0x04000FBF RID: 4031
		private X509Certificate certificate;

		// Token: 0x04000FC0 RID: 4032
		private DateTimeObject certificateValid;

		// Token: 0x04000FC1 RID: 4033
		private ISet extendedKeyUsage;

		// Token: 0x04000FC2 RID: 4034
		private bool ignoreX509NameOrdering;

		// Token: 0x04000FC3 RID: 4035
		private X509Name issuer;

		// Token: 0x04000FC4 RID: 4036
		private bool[] keyUsage;

		// Token: 0x04000FC5 RID: 4037
		private ISet policy;

		// Token: 0x04000FC6 RID: 4038
		private DateTimeObject privateKeyValid;

		// Token: 0x04000FC7 RID: 4039
		private BigInteger serialNumber;

		// Token: 0x04000FC8 RID: 4040
		private X509Name subject;

		// Token: 0x04000FC9 RID: 4041
		private byte[] subjectKeyIdentifier;

		// Token: 0x04000FCA RID: 4042
		private SubjectPublicKeyInfo subjectPublicKey;

		// Token: 0x04000FCB RID: 4043
		private DerObjectIdentifier subjectPublicKeyAlgID;
	}
}
