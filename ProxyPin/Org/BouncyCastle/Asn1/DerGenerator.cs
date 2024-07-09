using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200026C RID: 620
	public abstract class DerGenerator : Asn1Generator
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x0006BC1C File Offset: 0x0006BC1C
		protected DerGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0006BC2C File Offset: 0x0006BC2C
		protected DerGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream)
		{
			this._tagged = true;
			this._isExplicit = isExplicit;
			this._tagNo = tagNo;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0006BC54 File Offset: 0x0006BC54
		private static void WriteLength(Stream outStr, int length)
		{
			if (length > 127)
			{
				int num = 1;
				int num2 = length;
				while ((num2 >>= 8) != 0)
				{
					num++;
				}
				outStr.WriteByte((byte)(num | 128));
				for (int i = (num - 1) * 8; i >= 0; i -= 8)
				{
					outStr.WriteByte((byte)(length >> i));
				}
				return;
			}
			outStr.WriteByte((byte)length);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0006BCB8 File Offset: 0x0006BCB8
		internal static void WriteDerEncoded(Stream outStream, int tag, byte[] bytes)
		{
			outStream.WriteByte((byte)tag);
			DerGenerator.WriteLength(outStream, bytes.Length);
			outStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0006BCD8 File Offset: 0x0006BCD8
		internal void WriteDerEncoded(int tag, byte[] bytes)
		{
			if (!this._tagged)
			{
				DerGenerator.WriteDerEncoded(base.Out, tag, bytes);
				return;
			}
			int num = this._tagNo | 128;
			if (this._isExplicit)
			{
				int tag2 = this._tagNo | 32 | 128;
				MemoryStream memoryStream = new MemoryStream();
				DerGenerator.WriteDerEncoded(memoryStream, tag, bytes);
				DerGenerator.WriteDerEncoded(base.Out, tag2, memoryStream.ToArray());
				return;
			}
			if ((tag & 32) != 0)
			{
				num |= 32;
			}
			DerGenerator.WriteDerEncoded(base.Out, num, bytes);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0006BD68 File Offset: 0x0006BD68
		internal static void WriteDerEncoded(Stream outStr, int tag, Stream inStr)
		{
			DerGenerator.WriteDerEncoded(outStr, tag, Streams.ReadAll(inStr));
		}

		// Token: 0x04000DB2 RID: 3506
		private bool _tagged = false;

		// Token: 0x04000DB3 RID: 3507
		private bool _isExplicit;

		// Token: 0x04000DB4 RID: 3508
		private int _tagNo;
	}
}
