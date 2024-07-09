using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000114 RID: 276
	public class OriginatorIdentifierOrKey : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x00046C80 File Offset: 0x00046C80
		public OriginatorIdentifierOrKey(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00046C90 File Offset: 0x00046C90
		[Obsolete("Use version taking a 'SubjectKeyIdentifier'")]
		public OriginatorIdentifierOrKey(Asn1OctetString id) : this(new SubjectKeyIdentifier(id))
		{
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00046CA0 File Offset: 0x00046CA0
		public OriginatorIdentifierOrKey(SubjectKeyIdentifier id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00046CB8 File Offset: 0x00046CB8
		public OriginatorIdentifierOrKey(OriginatorPublicKey id)
		{
			this.id = new DerTaggedObject(false, 1, id);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00046CD0 File Offset: 0x00046CD0
		[Obsolete("Use more specific version")]
		public OriginatorIdentifierOrKey(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00046CE0 File Offset: 0x00046CE0
		private OriginatorIdentifierOrKey(Asn1TaggedObject id)
		{
			this.id = id;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00046CF0 File Offset: 0x00046CF0
		public static OriginatorIdentifierOrKey GetInstance(Asn1TaggedObject o, bool explicitly)
		{
			if (!explicitly)
			{
				throw new ArgumentException("Can't implicitly tag OriginatorIdentifierOrKey");
			}
			return OriginatorIdentifierOrKey.GetInstance(o.GetObject());
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00046D10 File Offset: 0x00046D10
		public static OriginatorIdentifierOrKey GetInstance(object o)
		{
			if (o == null || o is OriginatorIdentifierOrKey)
			{
				return (OriginatorIdentifierOrKey)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new OriginatorIdentifierOrKey((IssuerAndSerialNumber)o);
			}
			if (o is SubjectKeyIdentifier)
			{
				return new OriginatorIdentifierOrKey((SubjectKeyIdentifier)o);
			}
			if (o is OriginatorPublicKey)
			{
				return new OriginatorIdentifierOrKey((OriginatorPublicKey)o);
			}
			if (o is Asn1TaggedObject)
			{
				return new OriginatorIdentifierOrKey((Asn1TaggedObject)o);
			}
			throw new ArgumentException("Invalid OriginatorIdentifierOrKey: " + Platform.GetTypeName(o));
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00046DAC File Offset: 0x00046DAC
		public Asn1Encodable ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00046DB4 File Offset: 0x00046DB4
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				if (this.id is IssuerAndSerialNumber)
				{
					return (IssuerAndSerialNumber)this.id;
				}
				return null;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00046DD4 File Offset: 0x00046DD4
		public SubjectKeyIdentifier SubjectKeyIdentifier
		{
			get
			{
				if (this.id is Asn1TaggedObject && ((Asn1TaggedObject)this.id).TagNo == 0)
				{
					return SubjectKeyIdentifier.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return null;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00046E10 File Offset: 0x00046E10
		[Obsolete("Use 'OriginatorPublicKey' property")]
		public OriginatorPublicKey OriginatorKey
		{
			get
			{
				return this.OriginatorPublicKey;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00046E18 File Offset: 0x00046E18
		public OriginatorPublicKey OriginatorPublicKey
		{
			get
			{
				if (this.id is Asn1TaggedObject && ((Asn1TaggedObject)this.id).TagNo == 1)
				{
					return OriginatorPublicKey.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return null;
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00046E54 File Offset: 0x00046E54
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x04000707 RID: 1799
		private Asn1Encodable id;
	}
}
