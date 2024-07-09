using System;
using dnlib.DotNet.Writer;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x02000794 RID: 1940
	internal sealed class X64CpuArch : CpuArch
	{
		// Token: 0x06004548 RID: 17736 RVA: 0x0016D384 File Offset: 0x0016D384
		public override uint GetStubAlignment(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 4U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x0016D394 File Offset: 0x0016D394
		public override uint GetStubSize(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 14U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x0016D3A8 File Offset: 0x0016D3A8
		public override uint GetStubCodeOffset(StubType stubType)
		{
			if (stubType <= StubType.EntryPoint)
			{
				return 2U;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x0016D3B8 File Offset: 0x0016D3B8
		protected override bool TryGetExportedRvaFromStubCore(ref DataReader reader, IPEImage peImage, out uint funcRva)
		{
			funcRva = 0U;
			if (reader.ReadUInt16() != 41288)
			{
				return false;
			}
			ulong num = reader.ReadUInt64();
			if (reader.ReadUInt16() != 57599)
			{
				return false;
			}
			ulong num2 = num - peImage.ImageNTHeaders.OptionalHeader.ImageBase;
			if (num2 > (ulong)-1)
			{
				return false;
			}
			funcRva = (uint)num2;
			return true;
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x0016D41C File Offset: 0x0016D41C
		public override void WriteStubRelocs(StubType stubType, RelocDirectory relocDirectory, IChunk chunk, uint stubOffset)
		{
			if (stubType <= StubType.EntryPoint)
			{
				relocDirectory.Add(chunk, stubOffset + 4U);
				return;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x0016D438 File Offset: 0x0016D438
		public override void WriteStub(StubType stubType, DataWriter writer, ulong imageBase, uint stubRva, uint managedFuncRva)
		{
			if (stubType <= StubType.EntryPoint)
			{
				writer.WriteUInt16(0);
				writer.WriteUInt16(41288);
				writer.WriteUInt64(imageBase + (ulong)managedFuncRva);
				writer.WriteUInt16(57599);
				return;
			}
			throw new ArgumentOutOfRangeException();
		}
	}
}
