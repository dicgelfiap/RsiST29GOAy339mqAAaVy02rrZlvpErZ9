using System;

namespace Org.BouncyCastle.Math.Raw
{
	// Token: 0x0200062A RID: 1578
	internal abstract class Nat384
	{
		// Token: 0x060036A2 RID: 13986 RVA: 0x00123908 File Offset: 0x00123908
		public static void Mul(uint[] x, uint[] y, uint[] zz)
		{
			Nat192.Mul(x, y, zz);
			Nat192.Mul(x, 6, y, 6, zz, 12);
			uint num = Nat192.AddToEachOther(zz, 6, zz, 12);
			uint cIn = num + Nat192.AddTo(zz, 0, zz, 6, 0U);
			num += Nat192.AddTo(zz, 18, zz, 12, cIn);
			uint[] array = Nat192.Create();
			uint[] array2 = Nat192.Create();
			bool flag = Nat192.Diff(x, 6, x, 0, array, 0) != Nat192.Diff(y, 6, y, 0, array2, 0);
			uint[] array3 = Nat192.CreateExt();
			Nat192.Mul(array, array2, array3);
			num += (flag ? Nat.AddTo(12, array3, 0, zz, 6) : ((uint)Nat.SubFrom(12, array3, 0, zz, 6)));
			Nat.AddWordAt(24, num, zz, 18);
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x001239C0 File Offset: 0x001239C0
		public static void Square(uint[] x, uint[] zz)
		{
			Nat192.Square(x, zz);
			Nat192.Square(x, 6, zz, 12);
			uint num = Nat192.AddToEachOther(zz, 6, zz, 12);
			uint cIn = num + Nat192.AddTo(zz, 0, zz, 6, 0U);
			num += Nat192.AddTo(zz, 18, zz, 12, cIn);
			uint[] array = Nat192.Create();
			Nat192.Diff(x, 6, x, 0, array, 0);
			uint[] array2 = Nat192.CreateExt();
			Nat192.Square(array, array2);
			num += (uint)Nat.SubFrom(12, array2, 0, zz, 6);
			Nat.AddWordAt(24, num, zz, 18);
		}
	}
}
