using System;

namespace Org.BouncyCastle.Math.Raw
{
	// Token: 0x02000621 RID: 1569
	internal abstract class Interleave
	{
		// Token: 0x0600353D RID: 13629 RVA: 0x001183D0 File Offset: 0x001183D0
		internal static uint Expand8to16(uint x)
		{
			x &= 255U;
			x = ((x | x << 4) & 3855U);
			x = ((x | x << 2) & 13107U);
			x = ((x | x << 1) & 21845U);
			return x;
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x00118404 File Offset: 0x00118404
		internal static uint Expand16to32(uint x)
		{
			x &= 65535U;
			x = ((x | x << 8) & 16711935U);
			x = ((x | x << 4) & 252645135U);
			x = ((x | x << 2) & 858993459U);
			x = ((x | x << 1) & 1431655765U);
			return x;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x00118444 File Offset: 0x00118444
		internal static ulong Expand32to64(uint x)
		{
			uint num = (x ^ x >> 8) & 65280U;
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 202116108U);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 572662306U);
			x ^= (num ^ num << 1);
			return ((ulong)(x >> 1) & 1431655765UL) << 32 | ((ulong)x & 1431655765UL);
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x001184C4 File Offset: 0x001184C4
		internal static void Expand64To128(ulong x, ulong[] z, int zOff)
		{
			ulong num = (x ^ x >> 16) & (ulong)-65536;
			x ^= (num ^ num << 16);
			num = ((x ^ x >> 8) & 280375465148160UL);
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 67555025218437360UL);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 868082074056920076UL);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 2459565876494606882UL);
			x ^= (num ^ num << 1);
			z[zOff] = (x & 6148914691236517205UL);
			z[zOff + 1] = (x >> 1 & 6148914691236517205UL);
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x00118574 File Offset: 0x00118574
		internal static void Expand64To128Rev(ulong x, ulong[] z, int zOff)
		{
			ulong num = (x ^ x >> 16) & (ulong)-65536;
			x ^= (num ^ num << 16);
			num = ((x ^ x >> 8) & 280375465148160UL);
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 67555025218437360UL);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 868082074056920076UL);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 2459565876494606882UL);
			x ^= (num ^ num << 1);
			z[zOff] = (x & 12297829382473034410UL);
			z[zOff + 1] = (x << 1 & 12297829382473034410UL);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x00118624 File Offset: 0x00118624
		internal static uint Shuffle(uint x)
		{
			uint num = (x ^ x >> 8) & 65280U;
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 202116108U);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 572662306U);
			x ^= (num ^ num << 1);
			return x;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x0011868C File Offset: 0x0011868C
		internal static ulong Shuffle(ulong x)
		{
			ulong num = (x ^ x >> 16) & (ulong)-65536;
			x ^= (num ^ num << 16);
			num = ((x ^ x >> 8) & 280375465148160UL);
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 67555025218437360UL);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 2) & 868082074056920076UL);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 1) & 2459565876494606882UL);
			x ^= (num ^ num << 1);
			return x;
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x0011871C File Offset: 0x0011871C
		internal static uint Shuffle2(uint x)
		{
			uint num = (x ^ x >> 7) & 11141290U;
			x ^= (num ^ num << 7);
			num = ((x ^ x >> 14) & 52428U);
			x ^= (num ^ num << 14);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 8) & 65280U);
			x ^= (num ^ num << 8);
			return x;
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x00118784 File Offset: 0x00118784
		internal static uint Unshuffle(uint x)
		{
			uint num = (x ^ x >> 1) & 572662306U;
			x ^= (num ^ num << 1);
			num = ((x ^ x >> 2) & 202116108U);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 8) & 65280U);
			x ^= (num ^ num << 8);
			return x;
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x001187EC File Offset: 0x001187EC
		internal static ulong Unshuffle(ulong x)
		{
			ulong num = (x ^ x >> 1) & 2459565876494606882UL;
			x ^= (num ^ num << 1);
			num = ((x ^ x >> 2) & 868082074056920076UL);
			x ^= (num ^ num << 2);
			num = ((x ^ x >> 4) & 67555025218437360UL);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 8) & 280375465148160UL);
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 16) & (ulong)-65536);
			x ^= (num ^ num << 16);
			return x;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x0011887C File Offset: 0x0011887C
		internal static uint Unshuffle2(uint x)
		{
			uint num = (x ^ x >> 8) & 65280U;
			x ^= (num ^ num << 8);
			num = ((x ^ x >> 4) & 15728880U);
			x ^= (num ^ num << 4);
			num = ((x ^ x >> 14) & 52428U);
			x ^= (num ^ num << 14);
			num = ((x ^ x >> 7) & 11141290U);
			x ^= (num ^ num << 7);
			return x;
		}

		// Token: 0x04001D1D RID: 7453
		private const ulong M32 = 1431655765UL;

		// Token: 0x04001D1E RID: 7454
		private const ulong M64 = 6148914691236517205UL;

		// Token: 0x04001D1F RID: 7455
		private const ulong M64R = 12297829382473034410UL;
	}
}
