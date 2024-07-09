using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001FF RID: 511
	public class IssuingDistributionPoint : Asn1Encodable
	{
		// Token: 0x06001081 RID: 4225 RVA: 0x00060238 File Offset: 0x00060238
		public static IssuingDistributionPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return IssuingDistributionPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00060248 File Offset: 0x00060248
		public static IssuingDistributionPoint GetInstance(object obj)
		{
			if (obj == null || obj is IssuingDistributionPoint)
			{
				return (IssuingDistributionPoint)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuingDistributionPoint((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000602A4 File Offset: 0x000602A4
		public IssuingDistributionPoint(DistributionPointName distributionPoint, bool onlyContainsUserCerts, bool onlyContainsCACerts, ReasonFlags onlySomeReasons, bool indirectCRL, bool onlyContainsAttributeCerts)
		{
			this._distributionPoint = distributionPoint;
			this._indirectCRL = indirectCRL;
			this._onlyContainsAttributeCerts = onlyContainsAttributeCerts;
			this._onlyContainsCACerts = onlyContainsCACerts;
			this._onlyContainsUserCerts = onlyContainsUserCerts;
			this._onlySomeReasons = onlySomeReasons;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			if (distributionPoint != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(true, 0, distributionPoint));
			}
			if (onlyContainsUserCerts)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 1, DerBoolean.True));
			}
			if (onlyContainsCACerts)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 2, DerBoolean.True));
			}
			if (onlySomeReasons != null)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 3, onlySomeReasons));
			}
			if (indirectCRL)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 4, DerBoolean.True));
			}
			if (onlyContainsAttributeCerts)
			{
				asn1EncodableVector.Add(new DerTaggedObject(false, 5, DerBoolean.True));
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00060388 File Offset: 0x00060388
		private IssuingDistributionPoint(Asn1Sequence seq)
		{
			this.seq = seq;
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this._distributionPoint = DistributionPointName.GetInstance(instance, true);
					break;
				case 1:
					this._onlyContainsUserCerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 2:
					this._onlyContainsCACerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 3:
					this._onlySomeReasons = new ReasonFlags(DerBitString.GetInstance(instance, false));
					break;
				case 4:
					this._indirectCRL = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 5:
					this._onlyContainsAttributeCerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				default:
					throw new ArgumentException("unknown tag in IssuingDistributionPoint");
				}
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00060484 File Offset: 0x00060484
		public bool OnlyContainsUserCerts
		{
			get
			{
				return this._onlyContainsUserCerts;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0006048C File Offset: 0x0006048C
		public bool OnlyContainsCACerts
		{
			get
			{
				return this._onlyContainsCACerts;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00060494 File Offset: 0x00060494
		public bool IsIndirectCrl
		{
			get
			{
				return this._indirectCRL;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0006049C File Offset: 0x0006049C
		public bool OnlyContainsAttributeCerts
		{
			get
			{
				return this._onlyContainsAttributeCerts;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x000604A4 File Offset: 0x000604A4
		public DistributionPointName DistributionPoint
		{
			get
			{
				return this._distributionPoint;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x000604AC File Offset: 0x000604AC
		public ReasonFlags OnlySomeReasons
		{
			get
			{
				return this._onlySomeReasons;
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000604B4 File Offset: 0x000604B4
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000604BC File Offset: 0x000604BC
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IssuingDistributionPoint: [");
			stringBuilder.Append(newLine);
			if (this._distributionPoint != null)
			{
				this.appendObject(stringBuilder, newLine, "distributionPoint", this._distributionPoint.ToString());
			}
			if (this._onlyContainsUserCerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsUserCerts", this._onlyContainsUserCerts.ToString());
			}
			if (this._onlyContainsCACerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsCACerts", this._onlyContainsCACerts.ToString());
			}
			if (this._onlySomeReasons != null)
			{
				this.appendObject(stringBuilder, newLine, "onlySomeReasons", this._onlySomeReasons.ToString());
			}
			if (this._onlyContainsAttributeCerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsAttributeCerts", this._onlyContainsAttributeCerts.ToString());
			}
			if (this._indirectCRL)
			{
				this.appendObject(stringBuilder, newLine, "indirectCRL", this._indirectCRL.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x000605E8 File Offset: 0x000605E8
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

		// Token: 0x04000BFA RID: 3066
		private readonly DistributionPointName _distributionPoint;

		// Token: 0x04000BFB RID: 3067
		private readonly bool _onlyContainsUserCerts;

		// Token: 0x04000BFC RID: 3068
		private readonly bool _onlyContainsCACerts;

		// Token: 0x04000BFD RID: 3069
		private readonly ReasonFlags _onlySomeReasons;

		// Token: 0x04000BFE RID: 3070
		private readonly bool _indirectCRL;

		// Token: 0x04000BFF RID: 3071
		private readonly bool _onlyContainsAttributeCerts;

		// Token: 0x04000C00 RID: 3072
		private readonly Asn1Sequence seq;
	}
}
