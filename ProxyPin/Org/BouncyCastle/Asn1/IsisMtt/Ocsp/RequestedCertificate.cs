using System;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.Ocsp
{
	// Token: 0x02000175 RID: 373
	public class RequestedCertificate : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000C90 RID: 3216 RVA: 0x00050910 File Offset: 0x00050910
		public static RequestedCertificate GetInstance(object obj)
		{
			if (obj == null || obj is RequestedCertificate)
			{
				return (RequestedCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RequestedCertificate(X509CertificateStructure.GetInstance(obj));
			}
			if (obj is Asn1TaggedObject)
			{
				return new RequestedCertificate((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00050984 File Offset: 0x00050984
		public static RequestedCertificate GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			if (!isExplicit)
			{
				throw new ArgumentException("choice item must be explicitly tagged");
			}
			return RequestedCertificate.GetInstance(obj.GetObject());
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000509A4 File Offset: 0x000509A4
		private RequestedCertificate(Asn1TaggedObject tagged)
		{
			switch (tagged.TagNo)
			{
			case 0:
				this.publicKeyCert = Asn1OctetString.GetInstance(tagged, true).GetOctets();
				return;
			case 1:
				this.attributeCert = Asn1OctetString.GetInstance(tagged, true).GetOctets();
				return;
			default:
				throw new ArgumentException("unknown tag number: " + tagged.TagNo);
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00050A18 File Offset: 0x00050A18
		public RequestedCertificate(X509CertificateStructure certificate)
		{
			this.cert = certificate;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00050A28 File Offset: 0x00050A28
		public RequestedCertificate(RequestedCertificate.Choice type, byte[] certificateOctets) : this(new DerTaggedObject((int)type, new DerOctetString(certificateOctets)))
		{
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00050A3C File Offset: 0x00050A3C
		public RequestedCertificate.Choice Type
		{
			get
			{
				if (this.cert != null)
				{
					return RequestedCertificate.Choice.Certificate;
				}
				if (this.publicKeyCert != null)
				{
					return RequestedCertificate.Choice.PublicKeyCertificate;
				}
				return RequestedCertificate.Choice.AttributeCertificate;
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00050A5C File Offset: 0x00050A5C
		public byte[] GetCertificateBytes()
		{
			if (this.cert != null)
			{
				try
				{
					return this.cert.GetEncoded();
				}
				catch (IOException arg)
				{
					throw new InvalidOperationException("can't decode certificate: " + arg);
				}
			}
			if (this.publicKeyCert != null)
			{
				return this.publicKeyCert;
			}
			return this.attributeCert;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00050AC4 File Offset: 0x00050AC4
		public override Asn1Object ToAsn1Object()
		{
			if (this.publicKeyCert != null)
			{
				return new DerTaggedObject(0, new DerOctetString(this.publicKeyCert));
			}
			if (this.attributeCert != null)
			{
				return new DerTaggedObject(1, new DerOctetString(this.attributeCert));
			}
			return this.cert.ToAsn1Object();
		}

		// Token: 0x040008B6 RID: 2230
		private readonly X509CertificateStructure cert;

		// Token: 0x040008B7 RID: 2231
		private readonly byte[] publicKeyCert;

		// Token: 0x040008B8 RID: 2232
		private readonly byte[] attributeCert;

		// Token: 0x02000D89 RID: 3465
		public enum Choice
		{
			// Token: 0x04003FD5 RID: 16341
			Certificate = -1,
			// Token: 0x04003FD6 RID: 16342
			PublicKeyCertificate,
			// Token: 0x04003FD7 RID: 16343
			AttributeCertificate
		}
	}
}
