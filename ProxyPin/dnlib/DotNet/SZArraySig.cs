using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000878 RID: 2168
	[ComVisible(true)]
	public sealed class SZArraySig : ArraySigBase
	{
		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x060052E7 RID: 21223 RVA: 0x001965AC File Offset: 0x001965AC
		public override ElementType ElementType
		{
			get
			{
				return ElementType.SZArray;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x060052E8 RID: 21224 RVA: 0x001965B0 File Offset: 0x001965B0
		// (set) Token: 0x060052E9 RID: 21225 RVA: 0x001965B4 File Offset: 0x001965B4
		public override uint Rank
		{
			get
			{
				return 1U;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x001965BC File Offset: 0x001965BC
		public SZArraySig(TypeSig nextSig) : base(nextSig)
		{
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x001965C8 File Offset: 0x001965C8
		public override IList<uint> GetSizes()
		{
			return Array2.Empty<uint>();
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x001965D0 File Offset: 0x001965D0
		public override IList<int> GetLowerBounds()
		{
			return Array2.Empty<int>();
		}
	}
}
