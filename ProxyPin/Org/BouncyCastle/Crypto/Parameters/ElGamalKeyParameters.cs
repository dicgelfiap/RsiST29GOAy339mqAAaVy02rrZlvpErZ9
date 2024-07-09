using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000450 RID: 1104
	public class ElGamalKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x060022A9 RID: 8873 RVA: 0x000C54A8 File Offset: 0x000C54A8
		protected ElGamalKeyParameters(bool isPrivate, ElGamalParameters parameters) : base(isPrivate)
		{
			this.parameters = parameters;
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x000C54B8 File Offset: 0x000C54B8
		public ElGamalParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000C54C0 File Offset: 0x000C54C0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalKeyParameters elGamalKeyParameters = obj as ElGamalKeyParameters;
			return elGamalKeyParameters != null && this.Equals(elGamalKeyParameters);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000C54F0 File Offset: 0x000C54F0
		protected bool Equals(ElGamalKeyParameters other)
		{
			return object.Equals(this.parameters, other.parameters) && base.Equals(other);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000C5514 File Offset: 0x000C5514
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.parameters != null)
			{
				num ^= this.parameters.GetHashCode();
			}
			return num;
		}

		// Token: 0x0400161F RID: 5663
		private readonly ElGamalParameters parameters;
	}
}
