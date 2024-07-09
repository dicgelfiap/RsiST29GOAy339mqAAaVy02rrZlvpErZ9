using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200046A RID: 1130
	public class ParametersWithRandom : ICipherParameters
	{
		// Token: 0x0600232D RID: 9005 RVA: 0x000C640C File Offset: 0x000C640C
		public ParametersWithRandom(ICipherParameters parameters, SecureRandom random)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			this.parameters = parameters;
			this.random = random;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000C6444 File Offset: 0x000C6444
		public ParametersWithRandom(ICipherParameters parameters) : this(parameters, new SecureRandom())
		{
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000C6454 File Offset: 0x000C6454
		[Obsolete("Use Random property instead")]
		public SecureRandom GetRandom()
		{
			return this.Random;
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x000C645C File Offset: 0x000C645C
		public SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x000C6464 File Offset: 0x000C6464
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0400165E RID: 5726
		private readonly ICipherParameters parameters;

		// Token: 0x0400165F RID: 5727
		private readonly SecureRandom random;
	}
}
