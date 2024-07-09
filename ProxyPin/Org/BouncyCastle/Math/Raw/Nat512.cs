using System;

namespace Org.BouncyCastle.Math.Raw
{
	// Token: 0x0200062C RID: 1580
	internal abstract class Nat512
	{
		// Token: 0x060036AF RID: 13999 RVA: 0x00123C2C File Offset: 0x00123C2C
		public static void Mul(uint[] x, uint[] y, uint[] zz)
		{
			Nat256.Mul(x, y, zz);
			Nat256.Mul(x, 8, y, 8, zz, 16);
			uint num = Nat256.AddToEachOther(zz, 8, zz, 16);
			uint cIn = num + Nat256.AddTo(zz, 0, zz, 8, 0U);
			num += Nat256.AddTo(zz, 24, zz, 16, cIn);
			uint[] array = Nat256.Create();
			uint[] array2 = Nat256.Create();
			bool flag = Nat256.Diff(x, 8, x, 0, array, 0) != Nat256.Diff(y, 8, y, 0, array2, 0);
			uint[] array3 = Nat256.CreateExt();
			Nat256.Mul(array, array2, array3);
			num += (flag ? Nat.AddTo(16, array3, 0, zz, 8) : ((uint)Nat.SubFrom(16, array3, 0, zz, 8)));
			Nat.AddWordAt(32, num, zz, 24);
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x00123CE4 File Offset: 0x00123CE4
		public static void Square(uint[] x, uint[] zz)
		{
			Nat256.Square(x, zz);
			Nat256.Square(x, 8, zz, 16);
			uint num = Nat256.AddToEachOther(zz, 8, zz, 16);
			uint cIn = num + Nat256.AddTo(zz, 0, zz, 8, 0U);
			num += Nat256.AddTo(zz, 24, zz, 16, cIn);
			uint[] array = Nat256.Create();
			Nat256.Diff(x, 8, x, 0, array, 0);
			uint[] array2 = Nat256.CreateExt();
			Nat256.Square(array, array2);
			num += (uint)Nat.SubFrom(16, array2, 0, zz, 8);
			Nat.AddWordAt(32, num, zz, 24);
		}
	}
}
