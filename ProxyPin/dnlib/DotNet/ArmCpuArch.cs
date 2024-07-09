using System;
using dnlib.DotNet.Writer;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x02000796 RID: 1942
	internal sealed class ArmCpuArch : CpuArch
	{
		// Token: 0x06004556 RID: 17750 RVA: 0x0016D620 File Offset: 0x0016D620
		public override uint GetStubAlignment(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 4U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x0016D630 File Offset: 0x0016D630
		public override uint GetStubSize(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 8U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x0016D640 File Offset: 0x0016D640
		public override uint GetStubCodeOffset(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 0U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x0016D650 File Offset: 0x0016D650
		protected override bool TryGetExportedRvaFromStubCore(ref DataReader reader, IPEImage peImage, out uint funcRva)
		{
			funcRva = 0U;
			if (reader.ReadUInt32() != 4026595551U)
			{
				return false;
			}
			funcRva = reader.ReadUInt32() - (uint)peImage.ImageNTHeaders.OptionalHeader.ImageBase;
			return true;
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x0016D694 File Offset: 0x0016D694
		public override void WriteStubRelocs(StubType stubType, RelocDirectory relocDirectory, IChunk chunk, uint stubOffset)
		{
			if (stubType <= StubType.EntryPoint)
			{
				relocDirectory.Add(chunk, stubOffset + 4U);
				return;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x0016D6B0 File Offset: 0x0016D6B0
		public override void WriteStub(StubType stubType, DataWriter writer, ulong imageBase, uint stubRva, uint managedFuncRva)
		{
			if (stubType <= StubType.EntryPoint)
			{
				writer.WriteUInt32(4026595551U);
				writer.WriteUInt32((uint)imageBase + managedFuncRva);
				return;
			}
			throw new ArgumentOutOfRangeException();
		}
	}
}
