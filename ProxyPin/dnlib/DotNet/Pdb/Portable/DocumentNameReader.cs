using System;
using System.Collections.Generic;
using System.Text;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x02000937 RID: 2359
	internal struct DocumentNameReader
	{
		// Token: 0x06005AD1 RID: 23249 RVA: 0x001B9F68 File Offset: 0x001B9F68
		public DocumentNameReader(BlobStream blobStream)
		{
			this.docNamePartDict = new Dictionary<uint, string>();
			this.blobStream = blobStream;
			this.sb = new StringBuilder();
			this.prevSepChars = new char[2];
			this.prevSepCharsLength = 0;
			this.prevSepCharBytes = new byte[3];
			this.prevSepCharBytesCount = 0;
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x001B9FBC File Offset: 0x001B9FBC
		public string ReadDocumentName(uint offset)
		{
			this.sb.Length = 0;
			DataReader dataReader;
			if (!this.blobStream.TryCreateReader(offset, out dataReader))
			{
				return string.Empty;
			}
			int num;
			char[] array = this.ReadSeparatorChar(ref dataReader, out num);
			bool flag = false;
			while (dataReader.Position < dataReader.Length)
			{
				if (flag)
				{
					this.sb.Append(array, 0, num);
				}
				flag = (num != 1 || array[0] > '\0');
				string value = this.ReadDocumentNamePart(dataReader.ReadCompressedUInt32());
				this.sb.Append(value);
				if (this.sb.Length > 65536)
				{
					this.sb.Length = 65536;
					break;
				}
			}
			return this.sb.ToString();
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x001BA090 File Offset: 0x001BA090
		private string ReadDocumentNamePart(uint offset)
		{
			string text;
			if (this.docNamePartDict.TryGetValue(offset, out text))
			{
				return text;
			}
			DataReader dataReader;
			if (!this.blobStream.TryCreateReader(offset, out dataReader))
			{
				return string.Empty;
			}
			text = dataReader.ReadUtf8String((int)dataReader.BytesLeft);
			this.docNamePartDict.Add(offset, text);
			return text;
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x001BA0EC File Offset: 0x001BA0EC
		private char[] ReadSeparatorChar(ref DataReader reader, out int charLength)
		{
			if (this.prevSepCharBytesCount != 0 && (long)this.prevSepCharBytesCount <= (long)((ulong)reader.Length))
			{
				uint position = reader.Position;
				bool flag = true;
				for (int i = 0; i < this.prevSepCharBytesCount; i++)
				{
					if (i >= this.prevSepCharBytes.Length || reader.ReadByte() != this.prevSepCharBytes[i])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					charLength = this.prevSepCharsLength;
					return this.prevSepChars;
				}
				reader.Position = position;
			}
			Decoder decoder = Encoding.UTF8.GetDecoder();
			byte[] array = new byte[1];
			this.prevSepCharBytesCount = 0;
			int num = 0;
			for (;;)
			{
				byte b = reader.ReadByte();
				this.prevSepCharBytesCount++;
				if (num == 0 && b == 0)
				{
					break;
				}
				if (num < this.prevSepCharBytes.Length)
				{
					this.prevSepCharBytes[num] = b;
				}
				array[0] = b;
				bool flush = reader.Position + 1U == reader.Length;
				int num2;
				bool flag2;
				decoder.Convert(array, 0, 1, this.prevSepChars, 0, this.prevSepChars.Length, flush, out num2, out this.prevSepCharsLength, out flag2);
				if (this.prevSepCharsLength > 0)
				{
					break;
				}
				num++;
			}
			charLength = this.prevSepCharsLength;
			return this.prevSepChars;
		}

		// Token: 0x04002BDF RID: 11231
		private const int MAX_NAME_LENGTH = 65536;

		// Token: 0x04002BE0 RID: 11232
		private readonly Dictionary<uint, string> docNamePartDict;

		// Token: 0x04002BE1 RID: 11233
		private readonly BlobStream blobStream;

		// Token: 0x04002BE2 RID: 11234
		private readonly StringBuilder sb;

		// Token: 0x04002BE3 RID: 11235
		private char[] prevSepChars;

		// Token: 0x04002BE4 RID: 11236
		private int prevSepCharsLength;

		// Token: 0x04002BE5 RID: 11237
		private byte[] prevSepCharBytes;

		// Token: 0x04002BE6 RID: 11238
		private int prevSepCharBytesCount;
	}
}
