using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F5 RID: 501
	public class DistributionPoint : Asn1Encodable
	{
		// Token: 0x0600101C RID: 4124 RVA: 0x0005E868 File Offset: 0x0005E868
		public static DistributionPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DistributionPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0005E878 File Offset: 0x0005E878
		public static DistributionPoint GetInstance(object obj)
		{
			if (obj == null || obj is DistributionPoint)
			{
				return (DistributionPoint)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DistributionPoint((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DistributionPoint: " + Platform.GetTypeName(obj));
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0005E8D0 File Offset: 0x0005E8D0
		private DistributionPoint(Asn1Sequence seq)
		{
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this.distributionPoint = DistributionPointName.GetInstance(instance, true);
					break;
				case 1:
					this.reasons = new ReasonFlags(DerBitString.GetInstance(instance, false));
					break;
				case 2:
					this.cRLIssuer = GeneralNames.GetInstance(instance, false);
					break;
				}
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0005E95C File Offset: 0x0005E95C
		public DistributionPoint(DistributionPointName distributionPointName, ReasonFlags reasons, GeneralNames crlIssuer)
		{
			this.distributionPoint = distributionPointName;
			this.reasons = reasons;
			this.cRLIssuer = crlIssuer;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0005E97C File Offset: 0x0005E97C
		public DistributionPointName DistributionPointName
		{
			get
			{
				return this.distributionPoint;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0005E984 File Offset: 0x0005E984
		public ReasonFlags Reasons
		{
			get
			{
				return this.reasons;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0005E98C File Offset: 0x0005E98C
		public GeneralNames CrlIssuer
		{
			get
			{
				return this.cRLIssuer;
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0005E994 File Offset: 0x0005E994
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.distributionPoint);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.reasons);
			asn1EncodableVector.AddOptionalTagged(false, 2, this.cRLIssuer);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0005E9DC File Offset: 0x0005E9DC
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistributionPoint: [");
			stringBuilder.Append(newLine);
			if (this.distributionPoint != null)
			{
				this.appendObject(stringBuilder, newLine, "distributionPoint", this.distributionPoint.ToString());
			}
			if (this.reasons != null)
			{
				this.appendObject(stringBuilder, newLine, "reasons", this.reasons.ToString());
			}
			if (this.cRLIssuer != null)
			{
				this.appendObject(stringBuilder, newLine, "cRLIssuer", this.cRLIssuer.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0005EA90 File Offset: 0x0005EA90
		private void appendObject(StringBuilder buf, string sep, string name, string val)
		{
			string value = "    ";
			buf.Append(value);
			buf.Append(name);
			buf.Append(":");
			buf.Append(sep);
			buf.Append(value);
			buf.Append(value);
			buf.Append(val);
			buf.Append(sep);
		}

		// Token: 0x04000BD2 RID: 3026
		internal readonly DistributionPointName distributionPoint;

		// Token: 0x04000BD3 RID: 3027
		internal readonly ReasonFlags reasons;

		// Token: 0x04000BD4 RID: 3028
		internal readonly GeneralNames cRLIssuer;
	}
}
