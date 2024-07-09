using System;
using System.Collections.Generic;

namespace dnlib.DotNet
{
	// Token: 0x02000884 RID: 2180
	internal sealed class ByteArrayEqualityComparer : IEqualityComparer<byte[]>
	{
		// Token: 0x06005387 RID: 21383 RVA: 0x001971C8 File Offset: 0x001971C8
		public bool Equals(byte[] x, byte[] y)
		{
			return Utils.Equals(x, y);
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x001971D4 File Offset: 0x001971D4
		public int GetHashCode(byte[] obj)
		{
			return Utils.GetHashCode(obj);
		}

		// Token: 0x040027E9 RID: 10217
		public static readonly ByteArrayEqualityComparer Instance = new ByteArrayEqualityComparer();
	}
}
