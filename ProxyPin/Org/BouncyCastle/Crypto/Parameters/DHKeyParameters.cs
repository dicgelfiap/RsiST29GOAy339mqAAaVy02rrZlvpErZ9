using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000436 RID: 1078
	public class DHKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x060021FA RID: 8698 RVA: 0x000C3AB8 File Offset: 0x000C3AB8
		protected DHKeyParameters(bool isPrivate, DHParameters parameters) : this(isPrivate, parameters, PkcsObjectIdentifiers.DhKeyAgreement)
		{
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000C3AC8 File Offset: 0x000C3AC8
		protected DHKeyParameters(bool isPrivate, DHParameters parameters, DerObjectIdentifier algorithmOid) : base(isPrivate)
		{
			this.parameters = parameters;
			this.algorithmOid = algorithmOid;
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x000C3AE0 File Offset: 0x000C3AE0
		public DHParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000C3AE8 File Offset: 0x000C3AE8
		public DerObjectIdentifier AlgorithmOid
		{
			get
			{
				return this.algorithmOid;
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000C3AF0 File Offset: 0x000C3AF0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHKeyParameters dhkeyParameters = obj as DHKeyParameters;
			return dhkeyParameters != null && this.Equals(dhkeyParameters);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000C3B20 File Offset: 0x000C3B20
		protected bool Equals(DHKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000C3B44 File Offset: 0x000C3B44
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x040015E0 RID: 5600
		private readonly DHParameters parameters;

		// Token: 0x040015E1 RID: 5601
		private readonly DerObjectIdentifier algorithmOid;
	}
}
