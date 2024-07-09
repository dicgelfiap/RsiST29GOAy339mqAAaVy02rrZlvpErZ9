using System;
using dnlib.DotNet.Writer;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x02000795 RID: 1941
	internal sealed class ItaniumCpuArch : CpuArch
	{
		// Token: 0x0600454F RID: 17743 RVA: 0x0016D488 File Offset: 0x0016D488
		public override uint GetStubAlignment(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 16U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x0016D49C File Offset: 0x0016D49C
		public override uint GetStubSize(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 48U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x0016D4B0 File Offset: 0x0016D4B0
		public override uint GetStubCodeOffset(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 32U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x0016D4C4 File Offset: 0x0016D4C4
		protected override bool TryGetExportedRvaFromStubCore(ref DataReader reader, IPEImage peImage, out uint funcRva)
		{
			funcRva = 0U;
			ulong num = reader.ReadUInt64();
			ulong num2 = reader.ReadUInt64();
			reader.Position = (uint)peImage.ToFileOffset((RVA)(num - peImage.ImageNTHeaders.OptionalHeader.ImageBase));
			if (reader.ReadUInt64() != 4656739709999925259UL)
			{
				return false;
			}
			if (reader.ReadUInt64() != 1125899909476388UL)
			{
				return false;
			}
			if (reader.ReadUInt64() != 5791646816365709328UL)
			{
				return false;
			}
			if (reader.ReadUInt64() != 36029209336053764UL)
			{
				return false;
			}
			ulong num3 = num2 - peImage.ImageNTHeaders.OptionalHeader.ImageBase;
			if (num3 > (ulong)-1)
			{
				return false;
			}
			funcRva = (uint)num3;
			return true;
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x0016D580 File Offset: 0x0016D580
		public override void WriteStubRelocs(StubType stubType, RelocDirectory relocDirectory, IChunk chunk, uint stubOffset)
		{
			if (stubType <= StubType.EntryPoint)
			{
				relocDirectory.Add(chunk, stubOffset + 32U);
				relocDirectory.Add(chunk, stubOffset + 40U);
				return;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x0016D5A8 File Offset: 0x0016D5A8
		public override void WriteStub(StubType stubType, DataWriter writer, ulong imageBase, uint stubRva, uint managedFuncRva)
		{
			if (stubType <= StubType.EntryPoint)
			{
				writer.WriteUInt64(4656739709999925259UL);
				writer.WriteUInt64(1125899909476388UL);
				writer.WriteUInt64(5791646816365709328UL);
				writer.WriteUInt64(36029209336053764UL);
				writer.WriteUInt64(imageBase + (ulong)stubRva);
				writer.WriteUInt64(imageBase + (ulong)managedFuncRva);
				return;
			}
			throw new ArgumentOutOfRangeException();
		}
	}
}
