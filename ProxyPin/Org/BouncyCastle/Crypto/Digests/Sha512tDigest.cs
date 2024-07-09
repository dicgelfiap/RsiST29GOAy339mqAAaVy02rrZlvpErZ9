using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000367 RID: 871
	public class Sha512tDigest : LongDigest
	{
		// Token: 0x06001ABD RID: 6845 RVA: 0x00090A70 File Offset: 0x00090A70
		public Sha512tDigest(int bitLength)
		{
			if (bitLength >= 512)
			{
				throw new ArgumentException("cannot be >= 512", "bitLength");
			}
			if (bitLength % 8 != 0)
			{
				throw new ArgumentException("needs to be a multiple of 8", "bitLength");
			}
			if (bitLength == 384)
			{
				throw new ArgumentException("cannot be 384 use SHA384 instead", "bitLength");
			}
			this.digestLength = bitLength / 8;
			this.tIvGenerate(this.digestLength * 8);
			this.Reset();
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00090AF4 File Offset: 0x00090AF4
		public Sha512tDigest(Sha512tDigest t) : base(t)
		{
			this.digestLength = t.digestLength;
			this.Reset(t);
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00090B10 File Offset: 0x00090B10
		public override string AlgorithmName
		{
			get
			{
				return "SHA-512/" + this.digestLength * 8;
			}
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00090B2C File Offset: 0x00090B2C
		public override int GetDigestSize()
		{
			return this.digestLength;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00090B34 File Offset: 0x00090B34
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Sha512tDigest.UInt64_To_BE(this.H1, output, outOff, this.digestLength);
			Sha512tDigest.UInt64_To_BE(this.H2, output, outOff + 8, this.digestLength - 8);
			Sha512tDigest.UInt64_To_BE(this.H3, output, outOff + 16, this.digestLength - 16);
			Sha512tDigest.UInt64_To_BE(this.H4, output, outOff + 24, this.digestLength - 24);
			Sha512tDigest.UInt64_To_BE(this.H5, output, outOff + 32, this.digestLength - 32);
			Sha512tDigest.UInt64_To_BE(this.H6, output, outOff + 40, this.digestLength - 40);
			Sha512tDigest.UInt64_To_BE(this.H7, output, outOff + 48, this.digestLength - 48);
			Sha512tDigest.UInt64_To_BE(this.H8, output, outOff + 56, this.digestLength - 56);
			this.Reset();
			return this.digestLength;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00090C18 File Offset: 0x00090C18
		public override void Reset()
		{
			base.Reset();
			this.H1 = this.H1t;
			this.H2 = this.H2t;
			this.H3 = this.H3t;
			this.H4 = this.H4t;
			this.H5 = this.H5t;
			this.H6 = this.H6t;
			this.H7 = this.H7t;
			this.H8 = this.H8t;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00090C90 File Offset: 0x00090C90
		private void tIvGenerate(int bitLength)
		{
			this.H1 = 14964410163792538797UL;
			this.H2 = 2216346199247487646UL;
			this.H3 = 11082046791023156622UL;
			this.H4 = 65953792586715988UL;
			this.H5 = 17630457682085488500UL;
			this.H6 = 4512832404995164602UL;
			this.H7 = 13413544941332994254UL;
			this.H8 = 18322165818757711068UL;
			base.Update(83);
			base.Update(72);
			base.Update(65);
			base.Update(45);
			base.Update(53);
			base.Update(49);
			base.Update(50);
			base.Update(47);
			if (bitLength > 100)
			{
				base.Update((byte)(bitLength / 100 + 48));
				bitLength %= 100;
				base.Update((byte)(bitLength / 10 + 48));
				bitLength %= 10;
				base.Update((byte)(bitLength + 48));
			}
			else if (bitLength > 10)
			{
				base.Update((byte)(bitLength / 10 + 48));
				bitLength %= 10;
				base.Update((byte)(bitLength + 48));
			}
			else
			{
				base.Update((byte)(bitLength + 48));
			}
			base.Finish();
			this.H1t = this.H1;
			this.H2t = this.H2;
			this.H3t = this.H3;
			this.H4t = this.H4;
			this.H5t = this.H5;
			this.H6t = this.H6;
			this.H7t = this.H7;
			this.H8t = this.H8;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00090E38 File Offset: 0x00090E38
		private static void UInt64_To_BE(ulong n, byte[] bs, int off, int max)
		{
			if (max > 0)
			{
				Sha512tDigest.UInt32_To_BE((uint)(n >> 32), bs, off, max);
				if (max > 4)
				{
					Sha512tDigest.UInt32_To_BE((uint)n, bs, off + 4, max - 4);
				}
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00090E64 File Offset: 0x00090E64
		private static void UInt32_To_BE(uint n, byte[] bs, int off, int max)
		{
			int num = Math.Min(4, max);
			while (--num >= 0)
			{
				int num2 = 8 * (3 - num);
				bs[off + num] = (byte)(n >> num2);
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00090E9C File Offset: 0x00090E9C
		public override IMemoable Copy()
		{
			return new Sha512tDigest(this);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00090EA4 File Offset: 0x00090EA4
		public override void Reset(IMemoable other)
		{
			Sha512tDigest sha512tDigest = (Sha512tDigest)other;
			if (this.digestLength != sha512tDigest.digestLength)
			{
				throw new MemoableResetException("digestLength inappropriate in other");
			}
			base.CopyIn(sha512tDigest);
			this.H1t = sha512tDigest.H1t;
			this.H2t = sha512tDigest.H2t;
			this.H3t = sha512tDigest.H3t;
			this.H4t = sha512tDigest.H4t;
			this.H5t = sha512tDigest.H5t;
			this.H6t = sha512tDigest.H6t;
			this.H7t = sha512tDigest.H7t;
			this.H8t = sha512tDigest.H8t;
		}

		// Token: 0x040011CA RID: 4554
		private const ulong A5 = 11936128518282651045UL;

		// Token: 0x040011CB RID: 4555
		private readonly int digestLength;

		// Token: 0x040011CC RID: 4556
		private ulong H1t;

		// Token: 0x040011CD RID: 4557
		private ulong H2t;

		// Token: 0x040011CE RID: 4558
		private ulong H3t;

		// Token: 0x040011CF RID: 4559
		private ulong H4t;

		// Token: 0x040011D0 RID: 4560
		private ulong H5t;

		// Token: 0x040011D1 RID: 4561
		private ulong H6t;

		// Token: 0x040011D2 RID: 4562
		private ulong H7t;

		// Token: 0x040011D3 RID: 4563
		private ulong H8t;
	}
}
