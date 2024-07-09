using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000152 RID: 338
	public class CrlValidatedID : Asn1Encodable
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x0004CD14 File Offset: 0x0004CD14
		public static CrlValidatedID GetInstance(object obj)
		{
			if (obj == null || obj is CrlValidatedID)
			{
				return (CrlValidatedID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlValidatedID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlValidatedID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0004CD70 File Offset: 0x0004CD70
		private CrlValidatedID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crlHash = OtherHash.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.crlIdentifier = CrlIdentifier.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0004CE0C File Offset: 0x0004CE0C
		public CrlValidatedID(OtherHash crlHash) : this(crlHash, null)
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0004CE18 File Offset: 0x0004CE18
		public CrlValidatedID(OtherHash crlHash, CrlIdentifier crlIdentifier)
		{
			if (crlHash == null)
			{
				throw new ArgumentNullException("crlHash");
			}
			this.crlHash = crlHash;
			this.crlIdentifier = crlIdentifier;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0004CE40 File Offset: 0x0004CE40
		public OtherHash CrlHash
		{
			get
			{
				return this.crlHash;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0004CE48 File Offset: 0x0004CE48
		public CrlIdentifier CrlIdentifier
		{
			get
			{
				return this.crlIdentifier;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0004CE50 File Offset: 0x0004CE50
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.crlHash.ToAsn1Object()
			});
			if (this.crlIdentifier != null)
			{
				asn1EncodableVector.Add(this.crlIdentifier.ToAsn1Object());
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040007FD RID: 2045
		private readonly OtherHash crlHash;

		// Token: 0x040007FE RID: 2046
		private readonly CrlIdentifier crlIdentifier;
	}
}
