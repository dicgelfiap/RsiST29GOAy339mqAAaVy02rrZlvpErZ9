using System;
using dnlib.DotNet.Writer;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x02000793 RID: 1939
	internal sealed class X86CpuArch : CpuArch
	{
		// Token: 0x06004541 RID: 17729 RVA: 0x0016D2C0 File Offset: 0x0016D2C0
		public override uint GetStubAlignment(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 4U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x0016D2D0 File Offset: 0x0016D2D0
		public override uint GetStubSize(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 8U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x0016D2E0 File Offset: 0x0016D2E0
		public override uint GetStubCodeOffset(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 2U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x0016D2F0 File Offset: 0x0016D2F0
		protected override bool TryGetExportedRvaFromStubCore(ref DataReader reader, IPEImage peImage, out uint funcRva)
		{
			funcRva = 0U;
			if (reader.ReadUInt16() != 9727)
			{
				return false;
			}
			funcRva = reader.ReadUInt32() - (uint)peImage.ImageNTHeaders.OptionalHeader.ImageBase;
			return true;
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x0016D334 File Offset: 0x0016D334
		public override void WriteStubRelocs(StubType stubType, RelocDirectory relocDirectory, IChunk chunk, uint stubOffset)
		{
			if (stubType <= StubType.EntryPoint)
			{
				relocDirectory.Add(chunk, stubOffset + 4U);
				return;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x0016D350 File Offset: 0x0016D350
		public override void WriteStub(StubType stubType, DataWriter writer, ulong imageBase, uint stubRva, uint managedFuncRva)
		{
			if (stubType <= StubType.EntryPoint)
			{
				writer.WriteUInt16(0);
				writer.WriteUInt16(9727);
				writer.WriteUInt32((uint)imageBase + managedFuncRva);
				return;
			}
			throw new ArgumentOutOfRangeException();
		}
	}
}
