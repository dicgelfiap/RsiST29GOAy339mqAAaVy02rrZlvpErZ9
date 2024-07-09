using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000618 RID: 1560
	public class SimpleLookupTable : AbstractECLookupTable
	{
		// Token: 0x0600351B RID: 13595 RVA: 0x00118090 File Offset: 0x00118090
		private static ECPoint[] Copy(ECPoint[] points, int off, int len)
		{
			ECPoint[] array = new ECPoint[len];
			for (int i = 0; i < len; i++)
			{
				array[i] = points[off + i];
			}
			return array;
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x001180C8 File Offset: 0x001180C8
		public SimpleLookupTable(ECPoint[] points, int off, int len)
		{
			this.points = SimpleLookupTable.Copy(points, off, len);
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x001180E0 File Offset: 0x001180E0
		public override int Size
		{
			get
			{
				return this.points.Length;
			}
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x001180EC File Offset: 0x001180EC
		public override ECPoint Lookup(int index)
		{
			throw new NotSupportedException("Constant-time lookup not supported");
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x001180F8 File Offset: 0x001180F8
		public override ECPoint LookupVar(int index)
		{
			return this.points[index];
		}

		// Token: 0x04001D16 RID: 7446
		private readonly ECPoint[] points;
	}
}
