using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000723 RID: 1827
	public class X509V2AttributeCertificate : X509ExtensionBase, IX509AttributeCertificate, IX509Extension
	{
		// Token: 0x06004006 RID: 16390 RVA: 0x0015EF98 File Offset: 0x0015EF98
		private static AttributeCertificate GetObject(Stream input)
		{
			AttributeCertificate instance;
			try
			{
				instance = AttributeCertificate.GetInstance(Asn1Object.FromStream(input));
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception innerException)
			{
				throw new IOException("exception decoding certificate structure", innerException);
			}
			return instance;
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x0015EFE4 File Offset: 0x0015EFE4
		public X509V2AttributeCertificate(Stream encIn) : this(X509V2AttributeCertificate.GetObject(encIn))
		{
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x0015EFF4 File Offset: 0x0015EFF4
		public X509V2AttributeCertificate(byte[] encoded) : this(new MemoryStream(encoded, false))
		{
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x0015F004 File Offset: 0x0015F004
		internal X509V2AttributeCertificate(AttributeCertificate cert)
		{
			this.cert = cert;
			try
			{
				this.notAfter = cert.ACInfo.AttrCertValidityPeriod.NotAfterTime.ToDateTime();
				this.notBefore = cert.ACInfo.AttrCertValidityPeriod.NotBeforeTime.ToDateTime();
			}
			catch (Exception innerException)
			{
				throw new IOException("invalid data structure in certificate!", innerException);
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x0015F078 File Offset: 0x0015F078
		public virtual int Version
		{
			get
			{
				return this.cert.ACInfo.Version.IntValueExact + 1;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x0015F094 File Offset: 0x0015F094
		public virtual BigInteger SerialNumber
		{
			get
			{
				return this.cert.ACInfo.SerialNumber.Value;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x0015F0AC File Offset: 0x0015F0AC
		public virtual AttributeCertificateHolder Holder
		{
			get
			{
				return new AttributeCertificateHolder((Asn1Sequence)this.cert.ACInfo.Holder.ToAsn1Object());
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600400D RID: 16397 RVA: 0x0015F0D0 File Offset: 0x0015F0D0
		public virtual AttributeCertificateIssuer Issuer
		{
			get
			{
				return new AttributeCertificateIssuer(this.cert.ACInfo.Issuer);
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x0015F0E8 File Offset: 0x0015F0E8
		public virtual DateTime NotBefore
		{
			get
			{
				return this.notBefore;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600400F RID: 16399 RVA: 0x0015F0F0 File Offset: 0x0015F0F0
		public virtual DateTime NotAfter
		{
			get
			{
				return this.notAfter;
			}
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x0015F0F8 File Offset: 0x0015F0F8
		public virtual bool[] GetIssuerUniqueID()
		{
			DerBitString issuerUniqueID = this.cert.ACInfo.IssuerUniqueID;
			if (issuerUniqueID != null)
			{
				byte[] bytes = issuerUniqueID.GetBytes();
				bool[] array = new bool[bytes.Length * 8 - issuerUniqueID.PadBits];
				for (int num = 0; num != array.Length; num++)
				{
					array[num] = (((int)bytes[num / 8] & 128 >> num % 8) != 0);
				}
				return array;
			}
			return null;
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06004011 RID: 16401 RVA: 0x0015F168 File Offset: 0x0015F168
		public virtual bool IsValidNow
		{
			get
			{
				return this.IsValid(DateTime.UtcNow);
			}
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x0015F178 File Offset: 0x0015F178
		public virtual bool IsValid(DateTime date)
		{
			return date.CompareTo(this.NotBefore) >= 0 && date.CompareTo(this.NotAfter) <= 0;
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x0015F1AC File Offset: 0x0015F1AC
		public virtual void CheckValidity()
		{
			this.CheckValidity(DateTime.UtcNow);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x0015F1BC File Offset: 0x0015F1BC
		public virtual void CheckValidity(DateTime date)
		{
			if (date.CompareTo(this.NotAfter) > 0)
			{
				throw new CertificateExpiredException("certificate expired on " + this.NotAfter);
			}
			if (date.CompareTo(this.NotBefore) < 0)
			{
				throw new CertificateNotYetValidException("certificate not valid until " + this.NotBefore);
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x0015F234 File Offset: 0x0015F234
		public virtual AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.cert.SignatureAlgorithm;
			}
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x0015F244 File Offset: 0x0015F244
		public virtual byte[] GetSignature()
		{
			return this.cert.GetSignatureOctets();
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x0015F254 File Offset: 0x0015F254
		public virtual void Verify(AsymmetricKeyParameter key)
		{
			this.CheckSignature(new Asn1VerifierFactory(this.cert.SignatureAlgorithm, key));
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x0015F270 File Offset: 0x0015F270
		public virtual void Verify(IVerifierFactoryProvider verifierProvider)
		{
			this.CheckSignature(verifierProvider.CreateVerifierFactory(this.cert.SignatureAlgorithm));
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x0015F28C File Offset: 0x0015F28C
		protected virtual void CheckSignature(IVerifierFactory verifier)
		{
			if (!this.cert.SignatureAlgorithm.Equals(this.cert.ACInfo.Signature))
			{
				throw new CertificateException("Signature algorithm in certificate info not same as outer certificate");
			}
			IStreamCalculator streamCalculator = verifier.CreateCalculator();
			try
			{
				byte[] encoded = this.cert.ACInfo.GetEncoded();
				streamCalculator.Stream.Write(encoded, 0, encoded.Length);
				Platform.Dispose(streamCalculator.Stream);
			}
			catch (IOException exception)
			{
				throw new SignatureException("Exception encoding certificate info object", exception);
			}
			if (!((IVerifier)streamCalculator.GetResult()).IsVerified(this.GetSignature()))
			{
				throw new InvalidKeyException("Public key presented not for certificate signature");
			}
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x0015F344 File Offset: 0x0015F344
		public virtual byte[] GetEncoded()
		{
			return this.cert.GetEncoded();
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x0015F354 File Offset: 0x0015F354
		protected override X509Extensions GetX509Extensions()
		{
			return this.cert.ACInfo.Extensions;
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x0015F368 File Offset: 0x0015F368
		public virtual X509Attribute[] GetAttributes()
		{
			Asn1Sequence attributes = this.cert.ACInfo.Attributes;
			X509Attribute[] array = new X509Attribute[attributes.Count];
			for (int num = 0; num != attributes.Count; num++)
			{
				array[num] = new X509Attribute(attributes[num]);
			}
			return array;
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x0015F3C0 File Offset: 0x0015F3C0
		public virtual X509Attribute[] GetAttributes(string oid)
		{
			Asn1Sequence attributes = this.cert.ACInfo.Attributes;
			IList list = Platform.CreateArrayList();
			for (int num = 0; num != attributes.Count; num++)
			{
				X509Attribute x509Attribute = new X509Attribute(attributes[num]);
				if (x509Attribute.Oid.Equals(oid))
				{
					list.Add(x509Attribute);
				}
			}
			if (list.Count < 1)
			{
				return null;
			}
			X509Attribute[] array = new X509Attribute[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = (X509Attribute)list[i];
			}
			return array;
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x0015F470 File Offset: 0x0015F470
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509V2AttributeCertificate x509V2AttributeCertificate = obj as X509V2AttributeCertificate;
			return x509V2AttributeCertificate != null && this.cert.Equals(x509V2AttributeCertificate.cert);
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x0015F4AC File Offset: 0x0015F4AC
		public override int GetHashCode()
		{
			return this.cert.GetHashCode();
		}

		// Token: 0x040020D1 RID: 8401
		private readonly AttributeCertificate cert;

		// Token: 0x040020D2 RID: 8402
		private readonly DateTime notBefore;

		// Token: 0x040020D3 RID: 8403
		private readonly DateTime notAfter;
	}
}
