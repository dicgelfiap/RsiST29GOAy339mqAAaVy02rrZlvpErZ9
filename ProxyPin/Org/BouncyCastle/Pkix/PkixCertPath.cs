using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000690 RID: 1680
	public class PkixCertPath
	{
		// Token: 0x06003AA3 RID: 15011 RVA: 0x0013BA74 File Offset: 0x0013BA74
		static PkixCertPath()
		{
			IList list = Platform.CreateArrayList();
			list.Add("PkiPath");
			list.Add("PEM");
			list.Add("PKCS7");
			PkixCertPath.certPathEncodings = CollectionUtilities.ReadOnly(list);
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x0013BABC File Offset: 0x0013BABC
		private static IList SortCerts(IList certs)
		{
			if (certs.Count < 2)
			{
				return certs;
			}
			X509Name issuerDN = ((X509Certificate)certs[0]).IssuerDN;
			bool flag = true;
			for (int num = 1; num != certs.Count; num++)
			{
				X509Certificate x509Certificate = (X509Certificate)certs[num];
				if (!issuerDN.Equivalent(x509Certificate.SubjectDN, true))
				{
					flag = false;
					break;
				}
				issuerDN = ((X509Certificate)certs[num]).IssuerDN;
			}
			if (flag)
			{
				return certs;
			}
			IList list = Platform.CreateArrayList(certs.Count);
			IList result = Platform.CreateArrayList(certs);
			for (int i = 0; i < certs.Count; i++)
			{
				X509Certificate x509Certificate2 = (X509Certificate)certs[i];
				bool flag2 = false;
				X509Name subjectDN = x509Certificate2.SubjectDN;
				foreach (object obj in certs)
				{
					X509Certificate x509Certificate3 = (X509Certificate)obj;
					if (x509Certificate3.IssuerDN.Equivalent(subjectDN, true))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					list.Add(x509Certificate2);
					certs.RemoveAt(i);
				}
			}
			if (list.Count > 1)
			{
				return result;
			}
			for (int num2 = 0; num2 != list.Count; num2++)
			{
				issuerDN = ((X509Certificate)list[num2]).IssuerDN;
				for (int j = 0; j < certs.Count; j++)
				{
					X509Certificate x509Certificate4 = (X509Certificate)certs[j];
					if (issuerDN.Equivalent(x509Certificate4.SubjectDN, true))
					{
						list.Add(x509Certificate4);
						certs.RemoveAt(j);
						break;
					}
				}
			}
			if (certs.Count > 0)
			{
				return result;
			}
			return list;
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x0013BCB4 File Offset: 0x0013BCB4
		public PkixCertPath(ICollection certificates)
		{
			this.certificates = PkixCertPath.SortCerts(Platform.CreateArrayList(certificates));
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x0013BCD0 File Offset: 0x0013BCD0
		public PkixCertPath(Stream inStream) : this(inStream, "PkiPath")
		{
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x0013BCE0 File Offset: 0x0013BCE0
		public PkixCertPath(Stream inStream, string encoding)
		{
			string text = Platform.ToUpperInvariant(encoding);
			IList list;
			try
			{
				if (text.Equals(Platform.ToUpperInvariant("PkiPath")))
				{
					Asn1InputStream asn1InputStream = new Asn1InputStream(inStream);
					Asn1Object asn1Object = asn1InputStream.ReadObject();
					if (!(asn1Object is Asn1Sequence))
					{
						throw new CertificateException("input stream does not contain a ASN1 SEQUENCE while reading PkiPath encoded data to load CertPath");
					}
					list = Platform.CreateArrayList();
					using (IEnumerator enumerator = ((Asn1Sequence)asn1Object).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
							byte[] encoded = asn1Encodable.GetEncoded("DER");
							Stream inStream2 = new MemoryStream(encoded, false);
							list.Insert(0, new X509CertificateParser().ReadCertificate(inStream2));
						}
						goto IL_104;
					}
				}
				if (!text.Equals("PKCS7") && !text.Equals("PEM"))
				{
					throw new CertificateException("unsupported encoding: " + encoding);
				}
				list = Platform.CreateArrayList(new X509CertificateParser().ReadCertificates(inStream));
				IL_104:;
			}
			catch (IOException ex)
			{
				throw new CertificateException("IOException throw while decoding CertPath:\n" + ex.ToString());
			}
			this.certificates = PkixCertPath.SortCerts(list);
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x0013BE38 File Offset: 0x0013BE38
		public virtual IEnumerable Encodings
		{
			get
			{
				return new EnumerableProxy(PkixCertPath.certPathEncodings);
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x0013BE44 File Offset: 0x0013BE44
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			PkixCertPath pkixCertPath = obj as PkixCertPath;
			if (pkixCertPath == null)
			{
				return false;
			}
			IList list = this.Certificates;
			IList list2 = pkixCertPath.Certificates;
			if (list.Count != list2.Count)
			{
				return false;
			}
			IEnumerator enumerator = list.GetEnumerator();
			IEnumerator enumerator2 = list2.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator2.MoveNext();
				if (!object.Equals(enumerator.Current, enumerator2.Current))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x0013BECC File Offset: 0x0013BECC
		public override int GetHashCode()
		{
			return this.Certificates.GetHashCode();
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x0013BEDC File Offset: 0x0013BEDC
		public virtual byte[] GetEncoded()
		{
			foreach (object obj in this.Encodings)
			{
				if (obj is string)
				{
					return this.GetEncoded((string)obj);
				}
			}
			return null;
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x0013BF54 File Offset: 0x0013BF54
		public virtual byte[] GetEncoded(string encoding)
		{
			if (Platform.EqualsIgnoreCase(encoding, "PkiPath"))
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
				for (int i = this.certificates.Count - 1; i >= 0; i--)
				{
					asn1EncodableVector.Add(this.ToAsn1Object((X509Certificate)this.certificates[i]));
				}
				return this.ToDerEncoded(new DerSequence(asn1EncodableVector));
			}
			if (Platform.EqualsIgnoreCase(encoding, "PKCS7"))
			{
				ContentInfo contentInfo = new ContentInfo(PkcsObjectIdentifiers.Data, null);
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector();
				for (int num = 0; num != this.certificates.Count; num++)
				{
					asn1EncodableVector2.Add(this.ToAsn1Object((X509Certificate)this.certificates[num]));
				}
				SignedData content = new SignedData(new DerInteger(1), new DerSet(), contentInfo, new DerSet(asn1EncodableVector2), null, new DerSet());
				return this.ToDerEncoded(new ContentInfo(PkcsObjectIdentifiers.SignedData, content));
			}
			if (Platform.EqualsIgnoreCase(encoding, "PEM"))
			{
				MemoryStream memoryStream = new MemoryStream();
				PemWriter pemWriter = new PemWriter(new StreamWriter(memoryStream));
				try
				{
					for (int num2 = 0; num2 != this.certificates.Count; num2++)
					{
						pemWriter.WriteObject(this.certificates[num2]);
					}
					Platform.Dispose(pemWriter.Writer);
				}
				catch (Exception)
				{
					throw new CertificateEncodingException("can't encode certificate for PEM encoded path");
				}
				return memoryStream.ToArray();
			}
			throw new CertificateEncodingException("unsupported encoding: " + encoding);
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x0013C0E8 File Offset: 0x0013C0E8
		public virtual IList Certificates
		{
			get
			{
				return CollectionUtilities.ReadOnly(this.certificates);
			}
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x0013C0F8 File Offset: 0x0013C0F8
		private Asn1Object ToAsn1Object(X509Certificate cert)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(cert.GetEncoded());
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("Exception while encoding certificate", e);
			}
			return result;
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x0013C134 File Offset: 0x0013C134
		private byte[] ToDerEncoded(Asn1Encodable obj)
		{
			byte[] encoded;
			try
			{
				encoded = obj.GetEncoded("DER");
			}
			catch (IOException e)
			{
				throw new CertificateEncodingException("Exception thrown", e);
			}
			return encoded;
		}

		// Token: 0x04001E65 RID: 7781
		internal static readonly IList certPathEncodings;

		// Token: 0x04001E66 RID: 7782
		private readonly IList certificates;
	}
}
