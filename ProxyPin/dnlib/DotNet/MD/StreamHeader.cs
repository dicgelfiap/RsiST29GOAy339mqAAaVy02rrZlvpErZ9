using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x020009D2 RID: 2514
	[DebuggerDisplay("O:{offset} L:{streamSize} {name}")]
	[ComVisible(true)]
	public sealed class StreamHeader : FileSection
	{
		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06005FC3 RID: 24515 RVA: 0x001C9D8C File Offset: 0x001C9D8C
		public uint Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06005FC4 RID: 24516 RVA: 0x001C9D94 File Offset: 0x001C9D94
		public uint StreamSize
		{
			get
			{
				return this.streamSize;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06005FC5 RID: 24517 RVA: 0x001C9D9C File Offset: 0x001C9D9C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x001C9DA4 File Offset: 0x001C9DA4
		public StreamHeader(ref DataReader reader, bool verify)
		{
			bool flag;
			this..ctor(ref reader, verify, verify, CLRRuntimeReaderKind.CLR, out flag);
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x001C9DC4 File Offset: 0x001C9DC4
		internal StreamHeader(ref DataReader reader, bool throwOnError, bool verify, CLRRuntimeReaderKind runtime, out bool failedVerification)
		{
			failedVerification = false;
			base.SetStartOffset(ref reader);
			this.offset = reader.ReadUInt32();
			this.streamSize = reader.ReadUInt32();
			this.name = StreamHeader.ReadString(ref reader, 32, verify, ref failedVerification);
			base.SetEndoffset(ref reader);
			if (runtime == CLRRuntimeReaderKind.Mono)
			{
				if (this.offset > reader.Length)
				{
					this.offset = reader.Length;
				}
				this.streamSize = reader.Length - this.offset;
			}
			if (verify && this.offset + this.size < this.offset)
			{
				failedVerification = true;
			}
			if (throwOnError & failedVerification)
			{
				throw new BadImageFormatException("Invalid stream header");
			}
		}

		// Token: 0x06005FC8 RID: 24520 RVA: 0x001C9E88 File Offset: 0x001C9E88
		internal StreamHeader(uint offset, uint streamSize, string name)
		{
			this.offset = offset;
			this.streamSize = streamSize;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x001C9EB8 File Offset: 0x001C9EB8
		private static string ReadString(ref DataReader reader, int maxLen, bool verify, ref bool failedVerification)
		{
			uint position = reader.Position;
			StringBuilder stringBuilder = new StringBuilder(maxLen);
			int i;
			for (i = 0; i < maxLen; i++)
			{
				byte b = reader.ReadByte();
				if (b == 0)
				{
					break;
				}
				stringBuilder.Append((char)b);
			}
			if (verify && i == maxLen)
			{
				failedVerification = true;
			}
			if (i != maxLen)
			{
				reader.Position = position + (uint)(i + 1 + 3 & -4);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002EFA RID: 12026
		private readonly uint offset;

		// Token: 0x04002EFB RID: 12027
		private readonly uint streamSize;

		// Token: 0x04002EFC RID: 12028
		private readonly string name;
	}
}
