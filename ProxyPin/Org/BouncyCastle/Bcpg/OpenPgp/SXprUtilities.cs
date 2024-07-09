using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200066E RID: 1646
	public sealed class SXprUtilities
	{
		// Token: 0x060039BA RID: 14778 RVA: 0x00135D80 File Offset: 0x00135D80
		private SXprUtilities()
		{
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x00135D88 File Offset: 0x00135D88
		private static int ReadLength(Stream input, int ch)
		{
			int num = ch - 48;
			while ((ch = input.ReadByte()) >= 0 && ch != 58)
			{
				num = num * 10 + ch - 48;
			}
			return num;
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x00135DC4 File Offset: 0x00135DC4
		internal static string ReadString(Stream input, int ch)
		{
			int num = SXprUtilities.ReadLength(input, ch);
			char[] array = new char[num];
			for (int num2 = 0; num2 != array.Length; num2++)
			{
				array[num2] = (char)input.ReadByte();
			}
			return new string(array);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x00135E08 File Offset: 0x00135E08
		internal static byte[] ReadBytes(Stream input, int ch)
		{
			int num = SXprUtilities.ReadLength(input, ch);
			byte[] array = new byte[num];
			Streams.ReadFully(input, array);
			return array;
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x00135E34 File Offset: 0x00135E34
		internal static S2k ParseS2k(Stream input)
		{
			SXprUtilities.SkipOpenParenthesis(input);
			SXprUtilities.ReadString(input, input.ReadByte());
			byte[] iv = SXprUtilities.ReadBytes(input, input.ReadByte());
			long iterationCount = long.Parse(SXprUtilities.ReadString(input, input.ReadByte()));
			SXprUtilities.SkipCloseParenthesis(input);
			return new SXprUtilities.MyS2k(HashAlgorithmTag.Sha1, iv, iterationCount);
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x00135E88 File Offset: 0x00135E88
		internal static void SkipOpenParenthesis(Stream input)
		{
			int num = input.ReadByte();
			if (num != 40)
			{
				throw new IOException("unknown character encountered");
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x00135EB4 File Offset: 0x00135EB4
		internal static void SkipCloseParenthesis(Stream input)
		{
			int num = input.ReadByte();
			if (num != 41)
			{
				throw new IOException("unknown character encountered");
			}
		}

		// Token: 0x02000E64 RID: 3684
		private class MyS2k : S2k
		{
			// Token: 0x06008D5E RID: 36190 RVA: 0x002A6730 File Offset: 0x002A6730
			internal MyS2k(HashAlgorithmTag algorithm, byte[] iv, long iterationCount64) : base(algorithm, iv, (int)iterationCount64)
			{
				this.mIterationCount64 = iterationCount64;
			}

			// Token: 0x17001DA8 RID: 7592
			// (get) Token: 0x06008D5F RID: 36191 RVA: 0x002A6744 File Offset: 0x002A6744
			public override long IterationCount
			{
				get
				{
					return this.mIterationCount64;
				}
			}

			// Token: 0x04004244 RID: 16964
			private readonly long mIterationCount64;
		}
	}
}
