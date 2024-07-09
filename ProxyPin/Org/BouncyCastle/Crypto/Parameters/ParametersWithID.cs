using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000468 RID: 1128
	public class ParametersWithID : ICipherParameters
	{
		// Token: 0x06002325 RID: 8997 RVA: 0x000C636C File Offset: 0x000C636C
		public ParametersWithID(ICipherParameters parameters, byte[] id) : this(parameters, id, 0, id.Length)
		{
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000C637C File Offset: 0x000C637C
		public ParametersWithID(ICipherParameters parameters, byte[] id, int idOff, int idLen)
		{
			this.parameters = parameters;
			this.id = Arrays.CopyOfRange(id, idOff, idOff + idLen);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000C639C File Offset: 0x000C639C
		public byte[] GetID()
		{
			return this.id;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x000C63A4 File Offset: 0x000C63A4
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0400165A RID: 5722
		private readonly ICipherParameters parameters;

		// Token: 0x0400165B RID: 5723
		private readonly byte[] id;
	}
}
