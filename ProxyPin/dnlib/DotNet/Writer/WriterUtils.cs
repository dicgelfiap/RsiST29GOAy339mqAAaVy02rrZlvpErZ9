using System;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008E2 RID: 2274
	internal static class WriterUtils
	{
		// Token: 0x0600589D RID: 22685 RVA: 0x001B3F4C File Offset: 0x001B3F4C
		public static uint WriteCompressedUInt32(this DataWriter writer, IWriterError helper, uint value)
		{
			if (value > 536870911U)
			{
				helper.Error("UInt32 value is too big and can't be compressed");
				value = 536870911U;
			}
			writer.WriteCompressedUInt32(value);
			return value;
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x001B3F74 File Offset: 0x001B3F74
		public static int WriteCompressedInt32(this DataWriter writer, IWriterError helper, int value)
		{
			if (value < -268435456)
			{
				helper.Error("Int32 value is too small and can't be compressed.");
				value = -268435456;
			}
			else if (value > 268435455)
			{
				helper.Error("Int32 value is too big and can't be compressed.");
				value = 268435455;
			}
			writer.WriteCompressedInt32(value);
			return value;
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x001B3FCC File Offset: 0x001B3FCC
		public static void Write(this DataWriter writer, IWriterError helper, UTF8String s)
		{
			if (UTF8String.IsNull(s))
			{
				helper.Error("UTF8String is null");
				s = UTF8String.Empty;
			}
			writer.WriteCompressedUInt32(helper, (uint)s.DataLength);
			writer.WriteBytes(s.Data);
		}
	}
}
