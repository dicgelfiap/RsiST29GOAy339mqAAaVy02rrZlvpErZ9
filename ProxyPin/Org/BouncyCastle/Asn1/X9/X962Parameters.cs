using System;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200022E RID: 558
	public class X962Parameters : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600120D RID: 4621 RVA: 0x00066618 File Offset: 0x00066618
		public static X962Parameters GetInstance(object obj)
		{
			if (obj == null || obj is X962Parameters)
			{
				return (X962Parameters)obj;
			}
			if (obj is Asn1Object)
			{
				return new X962Parameters((Asn1Object)obj);
			}
			if (obj is byte[])
			{
				try
				{
					return new X962Parameters(Asn1Object.FromByteArray((byte[])obj));
				}
				catch (Exception ex)
				{
					throw new ArgumentException("unable to parse encoded data: " + ex.Message, ex);
				}
			}
			throw new ArgumentException("unknown object in getInstance()");
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000666AC File Offset: 0x000666AC
		public X962Parameters(X9ECParameters ecParameters)
		{
			this._params = ecParameters.ToAsn1Object();
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000666C0 File Offset: 0x000666C0
		public X962Parameters(DerObjectIdentifier namedCurve)
		{
			this._params = namedCurve;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000666D0 File Offset: 0x000666D0
		public X962Parameters(Asn1Null obj)
		{
			this._params = obj;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000666E0 File Offset: 0x000666E0
		[Obsolete("Use 'GetInstance' instead")]
		public X962Parameters(Asn1Object obj)
		{
			this._params = obj;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x000666F0 File Offset: 0x000666F0
		public bool IsNamedCurve
		{
			get
			{
				return this._params is DerObjectIdentifier;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00066700 File Offset: 0x00066700
		public bool IsImplicitlyCA
		{
			get
			{
				return this._params is Asn1Null;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00066710 File Offset: 0x00066710
		public Asn1Object Parameters
		{
			get
			{
				return this._params;
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00066718 File Offset: 0x00066718
		public override Asn1Object ToAsn1Object()
		{
			return this._params;
		}

		// Token: 0x04000D04 RID: 3332
		private readonly Asn1Object _params;
	}
}
