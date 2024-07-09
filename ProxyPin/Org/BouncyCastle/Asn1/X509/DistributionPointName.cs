using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001F6 RID: 502
	public class DistributionPointName : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001026 RID: 4134 RVA: 0x0005EAEC File Offset: 0x0005EAEC
		public static DistributionPointName GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DistributionPointName.GetInstance(Asn1TaggedObject.GetInstance(obj, true));
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0005EAFC File Offset: 0x0005EAFC
		public static DistributionPointName GetInstance(object obj)
		{
			if (obj == null || obj is DistributionPointName)
			{
				return (DistributionPointName)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new DistributionPointName((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0005EB58 File Offset: 0x0005EB58
		public DistributionPointName(int type, Asn1Encodable name)
		{
			this.type = type;
			this.name = name;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0005EB70 File Offset: 0x0005EB70
		public DistributionPointName(GeneralNames name) : this(0, name)
		{
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0005EB7C File Offset: 0x0005EB7C
		public int PointType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0005EB84 File Offset: 0x0005EB84
		public Asn1Encodable Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0005EB8C File Offset: 0x0005EB8C
		public DistributionPointName(Asn1TaggedObject obj)
		{
			this.type = obj.TagNo;
			if (this.type == 0)
			{
				this.name = GeneralNames.GetInstance(obj, false);
				return;
			}
			this.name = Asn1Set.GetInstance(obj, false);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0005EBC8 File Offset: 0x0005EBC8
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.type, this.name);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0005EBDC File Offset: 0x0005EBDC
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistributionPointName: [");
			stringBuilder.Append(newLine);
			if (this.type == 0)
			{
				this.appendObject(stringBuilder, newLine, "fullName", this.name.ToString());
			}
			else
			{
				this.appendObject(stringBuilder, newLine, "nameRelativeToCRLIssuer", this.name.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0005EC68 File Offset: 0x0005EC68
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

		// Token: 0x04000BD5 RID: 3029
		public const int FullName = 0;

		// Token: 0x04000BD6 RID: 3030
		public const int NameRelativeToCrlIssuer = 1;

		// Token: 0x04000BD7 RID: 3031
		internal readonly Asn1Encodable name;

		// Token: 0x04000BD8 RID: 3032
		internal readonly int type;
	}
}
