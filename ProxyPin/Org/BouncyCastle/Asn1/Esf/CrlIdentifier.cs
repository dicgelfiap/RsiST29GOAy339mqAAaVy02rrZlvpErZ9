using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200014F RID: 335
	public class CrlIdentifier : Asn1Encodable
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x0004C72C File Offset: 0x0004C72C
		public static CrlIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is CrlIdentifier)
			{
				return (CrlIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlIdentifier' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0004C788 File Offset: 0x0004C788
		private CrlIdentifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crlIssuer = X509Name.GetInstance(seq[0]);
			this.crlIssuedTime = DerUtcTime.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.crlNumber = DerInteger.GetInstance(seq[2]);
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0004C82C File Offset: 0x0004C82C
		public CrlIdentifier(X509Name crlIssuer, DateTime crlIssuedTime) : this(crlIssuer, crlIssuedTime, null)
		{
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0004C838 File Offset: 0x0004C838
		public CrlIdentifier(X509Name crlIssuer, DateTime crlIssuedTime, BigInteger crlNumber)
		{
			if (crlIssuer == null)
			{
				throw new ArgumentNullException("crlIssuer");
			}
			this.crlIssuer = crlIssuer;
			this.crlIssuedTime = new DerUtcTime(crlIssuedTime);
			if (crlNumber != null)
			{
				this.crlNumber = new DerInteger(crlNumber);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0004C878 File Offset: 0x0004C878
		public X509Name CrlIssuer
		{
			get
			{
				return this.crlIssuer;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0004C880 File Offset: 0x0004C880
		public DateTime CrlIssuedTime
		{
			get
			{
				return this.crlIssuedTime.ToAdjustedDateTime();
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0004C890 File Offset: 0x0004C890
		public BigInteger CrlNumber
		{
			get
			{
				if (this.crlNumber != null)
				{
					return this.crlNumber.Value;
				}
				return null;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0004C8AC File Offset: 0x0004C8AC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.crlIssuer.ToAsn1Object(),
				this.crlIssuedTime
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.crlNumber
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040007F6 RID: 2038
		private readonly X509Name crlIssuer;

		// Token: 0x040007F7 RID: 2039
		private readonly DerUtcTime crlIssuedTime;

		// Token: 0x040007F8 RID: 2040
		private readonly DerInteger crlNumber;
	}
}
