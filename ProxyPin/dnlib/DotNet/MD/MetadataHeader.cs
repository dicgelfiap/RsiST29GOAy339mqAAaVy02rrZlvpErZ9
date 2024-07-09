using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000998 RID: 2456
	[ComVisible(true)]
	public sealed class MetadataHeader : FileSection
	{
		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06005ECA RID: 24266 RVA: 0x001C7110 File Offset: 0x001C7110
		public uint Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06005ECB RID: 24267 RVA: 0x001C7118 File Offset: 0x001C7118
		public ushort MajorVersion
		{
			get
			{
				return this.majorVersion;
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06005ECC RID: 24268 RVA: 0x001C7120 File Offset: 0x001C7120
		public ushort MinorVersion
		{
			get
			{
				return this.minorVersion;
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x001C7128 File Offset: 0x001C7128
		public uint Reserved1
		{
			get
			{
				return this.reserved1;
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06005ECE RID: 24270 RVA: 0x001C7130 File Offset: 0x001C7130
		public uint StringLength
		{
			get
			{
				return this.stringLength;
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x001C7138 File Offset: 0x001C7138
		public string VersionString
		{
			get
			{
				return this.versionString;
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06005ED0 RID: 24272 RVA: 0x001C7140 File Offset: 0x001C7140
		public FileOffset StorageHeaderOffset
		{
			get
			{
				return this.offset2ndPart;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06005ED1 RID: 24273 RVA: 0x001C7148 File Offset: 0x001C7148
		public StorageFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06005ED2 RID: 24274 RVA: 0x001C7150 File Offset: 0x001C7150
		public byte Reserved2
		{
			get
			{
				return this.reserved2;
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x001C7158 File Offset: 0x001C7158
		public ushort Streams
		{
			get
			{
				return this.streams;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06005ED4 RID: 24276 RVA: 0x001C7160 File Offset: 0x001C7160
		public IList<StreamHeader> StreamHeaders
		{
			get
			{
				return this.streamHeaders;
			}
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x001C7168 File Offset: 0x001C7168
		public MetadataHeader(ref DataReader reader, bool verify) : this(ref reader, CLRRuntimeReaderKind.CLR, verify)
		{
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x001C7174 File Offset: 0x001C7174
		public MetadataHeader(ref DataReader reader, CLRRuntimeReaderKind runtime, bool verify)
		{
			base.SetStartOffset(ref reader);
			this.signature = reader.ReadUInt32();
			if (verify && this.signature != 1112167234U)
			{
				throw new BadImageFormatException("Invalid metadata header signature");
			}
			this.majorVersion = reader.ReadUInt16();
			this.minorVersion = reader.ReadUInt16();
			this.reserved1 = reader.ReadUInt32();
			this.stringLength = reader.ReadUInt32();
			this.versionString = MetadataHeader.ReadString(ref reader, this.stringLength, runtime);
			this.offset2ndPart = (FileOffset)reader.CurrentOffset;
			this.flags = (StorageFlags)reader.ReadByte();
			this.reserved2 = reader.ReadByte();
			this.streams = reader.ReadUInt16();
			this.streamHeaders = new StreamHeader[(int)this.streams];
			for (int i = 0; i < this.streamHeaders.Count; i++)
			{
				bool flag;
				StreamHeader streamHeader = new StreamHeader(ref reader, false, verify, runtime, ref flag);
				if (flag || (ulong)streamHeader.Offset + (ulong)streamHeader.StreamSize > (ulong)reader.EndOffset)
				{
					streamHeader = new StreamHeader(0U, 0U, "<invalid>");
				}
				this.streamHeaders[i] = streamHeader;
			}
			base.SetEndoffset(ref reader);
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x001C72AC File Offset: 0x001C72AC
		private static string ReadString(ref DataReader reader, uint maxLength, CLRRuntimeReaderKind runtime)
		{
			ulong num = (ulong)reader.CurrentOffset + (ulong)maxLength;
			if (runtime == CLRRuntimeReaderKind.Mono)
			{
				num = (num + 3UL) / 4UL * 4UL;
			}
			if (num > (ulong)reader.EndOffset)
			{
				throw new BadImageFormatException("Invalid MD version string");
			}
			byte[] array = new byte[maxLength];
			uint num2;
			for (num2 = 0U; num2 < maxLength; num2 += 1U)
			{
				byte b = reader.ReadByte();
				if (b == 0)
				{
					break;
				}
				array[(int)num2] = b;
			}
			reader.CurrentOffset = (uint)num;
			return Encoding.UTF8.GetString(array, 0, (int)num2);
		}

		// Token: 0x04002E47 RID: 11847
		private readonly uint signature;

		// Token: 0x04002E48 RID: 11848
		private readonly ushort majorVersion;

		// Token: 0x04002E49 RID: 11849
		private readonly ushort minorVersion;

		// Token: 0x04002E4A RID: 11850
		private readonly uint reserved1;

		// Token: 0x04002E4B RID: 11851
		private readonly uint stringLength;

		// Token: 0x04002E4C RID: 11852
		private readonly string versionString;

		// Token: 0x04002E4D RID: 11853
		private readonly FileOffset offset2ndPart;

		// Token: 0x04002E4E RID: 11854
		private readonly StorageFlags flags;

		// Token: 0x04002E4F RID: 11855
		private readonly byte reserved2;

		// Token: 0x04002E50 RID: 11856
		private readonly ushort streams;

		// Token: 0x04002E51 RID: 11857
		private readonly IList<StreamHeader> streamHeaders;
	}
}
