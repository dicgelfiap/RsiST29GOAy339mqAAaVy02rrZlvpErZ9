using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000221 RID: 545
	public class X509Extension
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x000635CC File Offset: 0x000635CC
		public X509Extension(DerBoolean critical, Asn1OctetString value)
		{
			if (critical == null)
			{
				throw new ArgumentNullException("critical");
			}
			this.critical = critical.IsTrue;
			this.value = value;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000635F8 File Offset: 0x000635F8
		public X509Extension(bool critical, Asn1OctetString value)
		{
			this.critical = critical;
			this.value = value;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x00063610 File Offset: 0x00063610
		public bool IsCritical
		{
			get
			{
				return this.critical;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00063618 File Offset: 0x00063618
		public Asn1OctetString Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00063620 File Offset: 0x00063620
		public Asn1Encodable GetParsedValue()
		{
			return X509Extension.ConvertValueToObject(this);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00063628 File Offset: 0x00063628
		public override int GetHashCode()
		{
			int hashCode = this.Value.GetHashCode();
			if (!this.IsCritical)
			{
				return ~hashCode;
			}
			return hashCode;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00063654 File Offset: 0x00063654
		public override bool Equals(object obj)
		{
			X509Extension x509Extension = obj as X509Extension;
			return x509Extension != null && this.Value.Equals(x509Extension.Value) && this.IsCritical == x509Extension.IsCritical;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0006369C File Offset: 0x0006369C
		public static Asn1Object ConvertValueToObject(X509Extension ext)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(ext.Value.GetOctets());
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("can't convert extension", innerException);
			}
			return result;
		}

		// Token: 0x04000C88 RID: 3208
		internal bool critical;

		// Token: 0x04000C89 RID: 3209
		internal Asn1OctetString value;
	}
}
