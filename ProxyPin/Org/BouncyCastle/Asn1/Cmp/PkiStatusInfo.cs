using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000EE RID: 238
	public class PkiStatusInfo : Asn1Encodable
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x00043524 File Offset: 0x00043524
		public static PkiStatusInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PkiStatusInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00043534 File Offset: 0x00043534
		public static PkiStatusInfo GetInstance(object obj)
		{
			if (obj is PkiStatusInfo)
			{
				return (PkiStatusInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiStatusInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00043588 File Offset: 0x00043588
		public PkiStatusInfo(Asn1Sequence seq)
		{
			this.status = DerInteger.GetInstance(seq[0]);
			this.statusString = null;
			this.failInfo = null;
			if (seq.Count > 2)
			{
				this.statusString = PkiFreeText.GetInstance(seq[1]);
				this.failInfo = DerBitString.GetInstance(seq[2]);
				return;
			}
			if (seq.Count > 1)
			{
				object obj = seq[1];
				if (obj is DerBitString)
				{
					this.failInfo = DerBitString.GetInstance(obj);
					return;
				}
				this.statusString = PkiFreeText.GetInstance(obj);
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00043628 File Offset: 0x00043628
		public PkiStatusInfo(int status)
		{
			this.status = new DerInteger(status);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0004363C File Offset: 0x0004363C
		public PkiStatusInfo(int status, PkiFreeText statusString)
		{
			this.status = new DerInteger(status);
			this.statusString = statusString;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00043658 File Offset: 0x00043658
		public PkiStatusInfo(int status, PkiFreeText statusString, PkiFailureInfo failInfo)
		{
			this.status = new DerInteger(status);
			this.statusString = statusString;
			this.failInfo = failInfo;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0004367C File Offset: 0x0004367C
		public BigInteger Status
		{
			get
			{
				return this.status.Value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0004368C File Offset: 0x0004368C
		public PkiFreeText StatusString
		{
			get
			{
				return this.statusString;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00043694 File Offset: 0x00043694
		public DerBitString FailInfo
		{
			get
			{
				return this.failInfo;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0004369C File Offset: 0x0004369C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.statusString,
				this.failInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000688 RID: 1672
		private DerInteger status;

		// Token: 0x04000689 RID: 1673
		private PkiFreeText statusString;

		// Token: 0x0400068A RID: 1674
		private DerBitString failInfo;
	}
}
