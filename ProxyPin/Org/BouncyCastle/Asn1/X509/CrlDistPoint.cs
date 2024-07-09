using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F0 RID: 496
	public class CrlDistPoint : Asn1Encodable
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x0005E370 File Offset: 0x0005E370
		public static CrlDistPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return CrlDistPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0005E380 File Offset: 0x0005E380
		public static CrlDistPoint GetInstance(object obj)
		{
			if (obj is CrlDistPoint)
			{
				return (CrlDistPoint)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new CrlDistPoint(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0005E3A8 File Offset: 0x0005E3A8
		public static CrlDistPoint FromExtensions(X509Extensions extensions)
		{
			return CrlDistPoint.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, X509Extensions.CrlDistributionPoints));
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0005E3BC File Offset: 0x0005E3BC
		private CrlDistPoint(Asn1Sequence seq)
		{
			this.seq = seq;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0005E3CC File Offset: 0x0005E3CC
		public CrlDistPoint(DistributionPoint[] points)
		{
			this.seq = new DerSequence(points);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0005E3E0 File Offset: 0x0005E3E0
		public DistributionPoint[] GetDistributionPoints()
		{
			DistributionPoint[] array = new DistributionPoint[this.seq.Count];
			for (int num = 0; num != this.seq.Count; num++)
			{
				array[num] = DistributionPoint.GetInstance(this.seq[num]);
			}
			return array;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0005E434 File Offset: 0x0005E434
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0005E43C File Offset: 0x0005E43C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("CRLDistPoint:");
			stringBuilder.Append(newLine);
			DistributionPoint[] distributionPoints = this.GetDistributionPoints();
			for (int num = 0; num != distributionPoints.Length; num++)
			{
				stringBuilder.Append("    ");
				stringBuilder.Append(distributionPoints[num]);
				stringBuilder.Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000BBD RID: 3005
		internal readonly Asn1Sequence seq;
	}
}
