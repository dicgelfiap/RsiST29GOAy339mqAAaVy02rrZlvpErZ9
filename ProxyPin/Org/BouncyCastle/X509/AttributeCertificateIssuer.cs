using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000711 RID: 1809
	public class AttributeCertificateIssuer : IX509Selector, ICloneable
	{
		// Token: 0x06003F4D RID: 16205 RVA: 0x0015B55C File Offset: 0x0015B55C
		public AttributeCertificateIssuer(AttCertIssuer issuer)
		{
			this.form = issuer.Issuer;
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x0015B570 File Offset: 0x0015B570
		public AttributeCertificateIssuer(X509Name principal)
		{
			this.form = new V2Form(new GeneralNames(new GeneralName(principal)));
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x0015B590 File Offset: 0x0015B590
		private object[] GetNames()
		{
			GeneralNames generalNames;
			if (this.form is V2Form)
			{
				generalNames = ((V2Form)this.form).IssuerName;
			}
			else
			{
				generalNames = (GeneralNames)this.form;
			}
			GeneralName[] names = generalNames.GetNames();
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

		// Token: 0x06003F50 RID: 16208 RVA: 0x0015B654 File Offset: 0x0015B654
		public X509Name[] GetPrincipals()
		{
			object[] names = this.GetNames();
			int num = 0;
			for (int num2 = 0; num2 != names.Length; num2++)
			{
				if (names[num2] is X509Name)
				{
					num++;
				}
			}
			X509Name[] array = new X509Name[num];
			int num3 = 0;
			for (int num4 = 0; num4 != names.Length; num4++)
			{
				if (names[num4] is X509Name)
				{
					array[num3++] = (X509Name)names[num4];
				}
			}
			return array;
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x0015B6D0 File Offset: 0x0015B6D0
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

		// Token: 0x06003F52 RID: 16210 RVA: 0x0015B744 File Offset: 0x0015B744
		public object Clone()
		{
			return new AttributeCertificateIssuer(AttCertIssuer.GetInstance(this.form));
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x0015B758 File Offset: 0x0015B758
		public bool Match(X509Certificate x509Cert)
		{
			if (!(this.form is V2Form))
			{
				return this.MatchesDN(x509Cert.SubjectDN, (GeneralNames)this.form);
			}
			V2Form v2Form = (V2Form)this.form;
			if (v2Form.BaseCertificateID != null)
			{
				return v2Form.BaseCertificateID.Serial.Value.Equals(x509Cert.SerialNumber) && this.MatchesDN(x509Cert.IssuerDN, v2Form.BaseCertificateID.Issuer);
			}
			return this.MatchesDN(x509Cert.SubjectDN, v2Form.IssuerName);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x0015B7F4 File Offset: 0x0015B7F4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is AttributeCertificateIssuer))
			{
				return false;
			}
			AttributeCertificateIssuer attributeCertificateIssuer = (AttributeCertificateIssuer)obj;
			return this.form.Equals(attributeCertificateIssuer.form);
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0015B834 File Offset: 0x0015B834
		public override int GetHashCode()
		{
			return this.form.GetHashCode();
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x0015B844 File Offset: 0x0015B844
		public bool Match(object obj)
		{
			return obj is X509Certificate && this.Match((X509Certificate)obj);
		}

		// Token: 0x04002096 RID: 8342
		internal readonly Asn1Encodable form;
	}
}
