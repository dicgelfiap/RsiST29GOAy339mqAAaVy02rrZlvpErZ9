using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000222 RID: 546
	public class X509Extensions : Asn1Encodable
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x000636E0 File Offset: 0x000636E0
		public static X509Extension GetExtension(X509Extensions extensions, DerObjectIdentifier oid)
		{
			if (extensions != null)
			{
				return extensions.GetExtension(oid);
			}
			return null;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000636F4 File Offset: 0x000636F4
		public static Asn1Encodable GetExtensionParsedValue(X509Extensions extensions, DerObjectIdentifier oid)
		{
			if (extensions != null)
			{
				return extensions.GetExtensionParsedValue(oid);
			}
			return null;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00063708 File Offset: 0x00063708
		public static X509Extensions GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509Extensions.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00063718 File Offset: 0x00063718
		public static X509Extensions GetInstance(object obj)
		{
			if (obj == null || obj is X509Extensions)
			{
				return (X509Extensions)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new X509Extensions((Asn1Sequence)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return X509Extensions.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00063790 File Offset: 0x00063790
		private X509Extensions(Asn1Sequence seq)
		{
			this.ordering = Platform.CreateArrayList();
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				Asn1Sequence instance = Asn1Sequence.GetInstance(asn1Encodable.ToAsn1Object());
				if (instance.Count < 2 || instance.Count > 3)
				{
					throw new ArgumentException("Bad sequence size: " + instance.Count);
				}
				DerObjectIdentifier instance2 = DerObjectIdentifier.GetInstance(instance[0].ToAsn1Object());
				bool critical = instance.Count == 3 && DerBoolean.GetInstance(instance[1].ToAsn1Object()).IsTrue;
				Asn1OctetString instance3 = Asn1OctetString.GetInstance(instance[instance.Count - 1].ToAsn1Object());
				if (this.extensions.Contains(instance2))
				{
					throw new ArgumentException("repeated extension found: " + instance2);
				}
				this.extensions.Add(instance2, new X509Extension(critical, instance3));
				this.ordering.Add(instance2);
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000638E4 File Offset: 0x000638E4
		public X509Extensions(IDictionary extensions) : this(null, extensions)
		{
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000638F0 File Offset: 0x000638F0
		public X509Extensions(IList ordering, IDictionary extensions)
		{
			if (ordering == null)
			{
				this.ordering = Platform.CreateArrayList(extensions.Keys);
			}
			else
			{
				this.ordering = Platform.CreateArrayList(ordering);
			}
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)extensions[key]);
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0006399C File Offset: 0x0006399C
		public X509Extensions(IList oids, IList values)
		{
			this.ordering = Platform.CreateArrayList(oids);
			int num = 0;
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)values[num++]);
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00063A34 File Offset: 0x00063A34
		[Obsolete]
		public X509Extensions(Hashtable extensions) : this(null, extensions)
		{
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00063A40 File Offset: 0x00063A40
		[Obsolete]
		public X509Extensions(ArrayList ordering, Hashtable extensions)
		{
			if (ordering == null)
			{
				this.ordering = Platform.CreateArrayList(extensions.Keys);
			}
			else
			{
				this.ordering = Platform.CreateArrayList(ordering);
			}
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)extensions[key]);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00063AEC File Offset: 0x00063AEC
		[Obsolete]
		public X509Extensions(ArrayList oids, ArrayList values)
		{
			this.ordering = Platform.CreateArrayList(oids);
			int num = 0;
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)values[num++]);
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00063B84 File Offset: 0x00063B84
		[Obsolete("Use ExtensionOids IEnumerable property")]
		public IEnumerator Oids()
		{
			return this.ExtensionOids.GetEnumerator();
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00063B94 File Offset: 0x00063B94
		public IEnumerable ExtensionOids
		{
			get
			{
				return new EnumerableProxy(this.ordering);
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00063BA4 File Offset: 0x00063BA4
		public X509Extension GetExtension(DerObjectIdentifier oid)
		{
			return (X509Extension)this.extensions[oid];
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00063BB8 File Offset: 0x00063BB8
		public Asn1Encodable GetExtensionParsedValue(DerObjectIdentifier oid)
		{
			X509Extension extension = this.GetExtension(oid);
			if (extension != null)
			{
				return extension.GetParsedValue();
			}
			return null;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00063BE0 File Offset: 0x00063BE0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
				X509Extension x509Extension = (X509Extension)this.extensions[derObjectIdentifier];
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[]
				{
					derObjectIdentifier
				});
				if (x509Extension.IsCritical)
				{
					asn1EncodableVector2.Add(DerBoolean.True);
				}
				asn1EncodableVector2.Add(x509Extension.Value);
				asn1EncodableVector.Add(new DerSequence(asn1EncodableVector2));
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00063CA8 File Offset: 0x00063CA8
		public bool Equivalent(X509Extensions other)
		{
			if (this.extensions.Count != other.extensions.Count)
			{
				return false;
			}
			foreach (object obj in this.extensions.Keys)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				if (!this.extensions[key].Equals(other.extensions[key]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00063D54 File Offset: 0x00063D54
		public DerObjectIdentifier[] GetExtensionOids()
		{
			return X509Extensions.ToOidArray(this.ordering);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00063D64 File Offset: 0x00063D64
		public DerObjectIdentifier[] GetNonCriticalExtensionOids()
		{
			return this.GetExtensionOids(false);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00063D70 File Offset: 0x00063D70
		public DerObjectIdentifier[] GetCriticalExtensionOids()
		{
			return this.GetExtensionOids(true);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00063D7C File Offset: 0x00063D7C
		private DerObjectIdentifier[] GetExtensionOids(bool isCritical)
		{
			IList list = Platform.CreateArrayList();
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
				X509Extension x509Extension = (X509Extension)this.extensions[derObjectIdentifier];
				if (x509Extension.IsCritical == isCritical)
				{
					list.Add(derObjectIdentifier);
				}
			}
			return X509Extensions.ToOidArray(list);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00063E0C File Offset: 0x00063E0C
		private static DerObjectIdentifier[] ToOidArray(IList oids)
		{
			DerObjectIdentifier[] array = new DerObjectIdentifier[oids.Count];
			oids.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000C8A RID: 3210
		public static readonly DerObjectIdentifier SubjectDirectoryAttributes = new DerObjectIdentifier("2.5.29.9");

		// Token: 0x04000C8B RID: 3211
		public static readonly DerObjectIdentifier SubjectKeyIdentifier = new DerObjectIdentifier("2.5.29.14");

		// Token: 0x04000C8C RID: 3212
		public static readonly DerObjectIdentifier KeyUsage = new DerObjectIdentifier("2.5.29.15");

		// Token: 0x04000C8D RID: 3213
		public static readonly DerObjectIdentifier PrivateKeyUsagePeriod = new DerObjectIdentifier("2.5.29.16");

		// Token: 0x04000C8E RID: 3214
		public static readonly DerObjectIdentifier SubjectAlternativeName = new DerObjectIdentifier("2.5.29.17");

		// Token: 0x04000C8F RID: 3215
		public static readonly DerObjectIdentifier IssuerAlternativeName = new DerObjectIdentifier("2.5.29.18");

		// Token: 0x04000C90 RID: 3216
		public static readonly DerObjectIdentifier BasicConstraints = new DerObjectIdentifier("2.5.29.19");

		// Token: 0x04000C91 RID: 3217
		public static readonly DerObjectIdentifier CrlNumber = new DerObjectIdentifier("2.5.29.20");

		// Token: 0x04000C92 RID: 3218
		public static readonly DerObjectIdentifier ReasonCode = new DerObjectIdentifier("2.5.29.21");

		// Token: 0x04000C93 RID: 3219
		public static readonly DerObjectIdentifier InstructionCode = new DerObjectIdentifier("2.5.29.23");

		// Token: 0x04000C94 RID: 3220
		public static readonly DerObjectIdentifier InvalidityDate = new DerObjectIdentifier("2.5.29.24");

		// Token: 0x04000C95 RID: 3221
		public static readonly DerObjectIdentifier DeltaCrlIndicator = new DerObjectIdentifier("2.5.29.27");

		// Token: 0x04000C96 RID: 3222
		public static readonly DerObjectIdentifier IssuingDistributionPoint = new DerObjectIdentifier("2.5.29.28");

		// Token: 0x04000C97 RID: 3223
		public static readonly DerObjectIdentifier CertificateIssuer = new DerObjectIdentifier("2.5.29.29");

		// Token: 0x04000C98 RID: 3224
		public static readonly DerObjectIdentifier NameConstraints = new DerObjectIdentifier("2.5.29.30");

		// Token: 0x04000C99 RID: 3225
		public static readonly DerObjectIdentifier CrlDistributionPoints = new DerObjectIdentifier("2.5.29.31");

		// Token: 0x04000C9A RID: 3226
		public static readonly DerObjectIdentifier CertificatePolicies = new DerObjectIdentifier("2.5.29.32");

		// Token: 0x04000C9B RID: 3227
		public static readonly DerObjectIdentifier PolicyMappings = new DerObjectIdentifier("2.5.29.33");

		// Token: 0x04000C9C RID: 3228
		public static readonly DerObjectIdentifier AuthorityKeyIdentifier = new DerObjectIdentifier("2.5.29.35");

		// Token: 0x04000C9D RID: 3229
		public static readonly DerObjectIdentifier PolicyConstraints = new DerObjectIdentifier("2.5.29.36");

		// Token: 0x04000C9E RID: 3230
		public static readonly DerObjectIdentifier ExtendedKeyUsage = new DerObjectIdentifier("2.5.29.37");

		// Token: 0x04000C9F RID: 3231
		public static readonly DerObjectIdentifier FreshestCrl = new DerObjectIdentifier("2.5.29.46");

		// Token: 0x04000CA0 RID: 3232
		public static readonly DerObjectIdentifier InhibitAnyPolicy = new DerObjectIdentifier("2.5.29.54");

		// Token: 0x04000CA1 RID: 3233
		public static readonly DerObjectIdentifier AuthorityInfoAccess = new DerObjectIdentifier("1.3.6.1.5.5.7.1.1");

		// Token: 0x04000CA2 RID: 3234
		public static readonly DerObjectIdentifier SubjectInfoAccess = new DerObjectIdentifier("1.3.6.1.5.5.7.1.11");

		// Token: 0x04000CA3 RID: 3235
		public static readonly DerObjectIdentifier LogoType = new DerObjectIdentifier("1.3.6.1.5.5.7.1.12");

		// Token: 0x04000CA4 RID: 3236
		public static readonly DerObjectIdentifier BiometricInfo = new DerObjectIdentifier("1.3.6.1.5.5.7.1.2");

		// Token: 0x04000CA5 RID: 3237
		public static readonly DerObjectIdentifier QCStatements = new DerObjectIdentifier("1.3.6.1.5.5.7.1.3");

		// Token: 0x04000CA6 RID: 3238
		public static readonly DerObjectIdentifier AuditIdentity = new DerObjectIdentifier("1.3.6.1.5.5.7.1.4");

		// Token: 0x04000CA7 RID: 3239
		public static readonly DerObjectIdentifier NoRevAvail = new DerObjectIdentifier("2.5.29.56");

		// Token: 0x04000CA8 RID: 3240
		public static readonly DerObjectIdentifier TargetInformation = new DerObjectIdentifier("2.5.29.55");

		// Token: 0x04000CA9 RID: 3241
		public static readonly DerObjectIdentifier ExpiredCertsOnCrl = new DerObjectIdentifier("2.5.29.60");

		// Token: 0x04000CAA RID: 3242
		private readonly IDictionary extensions = Platform.CreateHashtable();

		// Token: 0x04000CAB RID: 3243
		private readonly IList ordering;
	}
}
