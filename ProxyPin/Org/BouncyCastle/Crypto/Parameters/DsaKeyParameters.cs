using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200043C RID: 1084
	public abstract class DsaKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002227 RID: 8743 RVA: 0x000C415C File Offset: 0x000C415C
		protected DsaKeyParameters(bool isPrivate, DsaParameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x000C416C File Offset: 0x000C416C
		public DsaParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000C4174 File Offset: 0x000C4174
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaKeyParameters dsaKeyParameters = obj as DsaKeyParameters;
			return dsaKeyParameters != null && this.Equals(dsaKeyParameters);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000C41A4 File Offset: 0x000C41A4
		protected bool Equals(DsaKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000C41C8 File Offset: 0x000C41C8
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x040015EF RID: 5615
		private readonly DsaParameters parameters;
	}
}
