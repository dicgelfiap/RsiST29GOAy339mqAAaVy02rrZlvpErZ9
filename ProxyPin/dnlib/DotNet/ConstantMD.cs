using System;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet
{
	// Token: 0x0200078F RID: 1935
	internal sealed class ConstantMD : Constant, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x0016CB34 File Offset: 0x0016CB34
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x0016CB3C File Offset: 0x0016CB3C
		public ConstantMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			RawConstantRow rawConstantRow;
			readerModule.TablesStream.TryReadConstantRow(this.origRid, out rawConstantRow);
			this.type = (ElementType)rawConstantRow.Type;
			DataReader dataReader = readerModule.BlobStream.CreateReader(rawConstantRow.Value);
			this.value = ConstantMD.GetValue(this.type, ref dataReader);
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x0016CBA8 File Offset: 0x0016CBA8
		private static object GetValue(ElementType etype, ref DataReader reader)
		{
			switch (etype)
			{
			case ElementType.Boolean:
				if (reader.Length < 1U)
				{
					return false;
				}
				return reader.ReadBoolean();
			case ElementType.Char:
				if (reader.Length < 2U)
				{
					return '\0';
				}
				return reader.ReadChar();
			case ElementType.I1:
				if (reader.Length < 1U)
				{
					return 0;
				}
				return reader.ReadSByte();
			case ElementType.U1:
				if (reader.Length < 1U)
				{
					return 0;
				}
				return reader.ReadByte();
			case ElementType.I2:
				if (reader.Length < 2U)
				{
					return 0;
				}
				return reader.ReadInt16();
			case ElementType.U2:
				if (reader.Length < 2U)
				{
					return 0;
				}
				return reader.ReadUInt16();
			case ElementType.I4:
				if (reader.Length < 4U)
				{
					return 0;
				}
				return reader.ReadInt32();
			case ElementType.U4:
				if (reader.Length < 4U)
				{
					return 0U;
				}
				return reader.ReadUInt32();
			case ElementType.I8:
				if (reader.Length < 8U)
				{
					return 0L;
				}
				return reader.ReadInt64();
			case ElementType.U8:
				if (reader.Length < 8U)
				{
					return 0UL;
				}
				return reader.ReadUInt64();
			case ElementType.R4:
				if (reader.Length < 4U)
				{
					return 0f;
				}
				return reader.ReadSingle();
			case ElementType.R8:
				if (reader.Length < 8U)
				{
					return 0.0;
				}
				return reader.ReadDouble();
			case ElementType.String:
				return reader.ReadUtf16String((int)(reader.BytesLeft / 2U));
			case ElementType.Class:
				return null;
			}
			return null;
		}

		// Token: 0x04002427 RID: 9255
		private readonly uint origRid;
	}
}
