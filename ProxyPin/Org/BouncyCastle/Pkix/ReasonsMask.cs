using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200069E RID: 1694
	internal class ReasonsMask
	{
		// Token: 0x06003B42 RID: 15170 RVA: 0x00140AD0 File Offset: 0x00140AD0
		internal ReasonsMask(int reasons)
		{
			this._reasons = reasons;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x00140AE0 File Offset: 0x00140AE0
		internal ReasonsMask() : this(0)
		{
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x00140AEC File Offset: 0x00140AEC
		internal void AddReasons(ReasonsMask mask)
		{
			this._reasons |= mask.Reasons.IntValue;
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x00140B08 File Offset: 0x00140B08
		internal bool IsAllReasons
		{
			get
			{
				return this._reasons == ReasonsMask.AllReasons._reasons;
			}
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x00140B1C File Offset: 0x00140B1C
		internal ReasonsMask Intersect(ReasonsMask mask)
		{
			ReasonsMask reasonsMask = new ReasonsMask();
			reasonsMask.AddReasons(new ReasonsMask(this._reasons & mask.Reasons.IntValue));
			return reasonsMask;
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x00140B54 File Offset: 0x00140B54
		internal bool HasNewReasons(ReasonsMask mask)
		{
			return (this._reasons | (mask.Reasons.IntValue ^ this._reasons)) != 0;
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x00140B78 File Offset: 0x00140B78
		public ReasonFlags Reasons
		{
			get
			{
				return new ReasonFlags(this._reasons);
			}
		}

		// Token: 0x04001E87 RID: 7815
		private int _reasons;

		// Token: 0x04001E88 RID: 7816
		internal static readonly ReasonsMask AllReasons = new ReasonsMask(33023);
	}
}
