using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001FE RID: 510
	public class IssuerSerial : Asn1Encodable
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x000600B4 File Offset: 0x000600B4
		public static IssuerSerial GetInstance(object obj)
		{
			if (obj == null || obj is IssuerSerial)
			{
				return (IssuerSerial)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuerSerial((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00060110 File Offset: 0x00060110
		public static IssuerSerial GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return IssuerSerial.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00060120 File Offset: 0x00060120
		private IssuerSerial(Asn1Sequence seq)
		{
			if (seq.Count != 2 && seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.issuer = GeneralNames.GetInstance(seq[0]);
			this.serial = DerInteger.GetInstance(seq[1]);
			if (seq.Count == 3)
			{
				this.issuerUid = DerBitString.GetInstance(seq[2]);
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000601AC File Offset: 0x000601AC
		public IssuerSerial(GeneralNames issuer, DerInteger serial)
		{
			this.issuer = issuer;
			this.serial = serial;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x000601C4 File Offset: 0x000601C4
		public GeneralNames Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x000601CC File Offset: 0x000601CC
		public DerInteger Serial
		{
			get
			{
				return this.serial;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x000601D4 File Offset: 0x000601D4
		public DerBitString IssuerUid
		{
			get
			{
				return this.issuerUid;
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x000601DC File Offset: 0x000601DC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.issuer,
				this.serial
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.issuerUid
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000BF7 RID: 3063
		internal readonly GeneralNames issuer;

		// Token: 0x04000BF8 RID: 3064
		internal readonly DerInteger serial;

		// Token: 0x04000BF9 RID: 3065
		internal readonly DerBitString issuerUid;
	}
}
