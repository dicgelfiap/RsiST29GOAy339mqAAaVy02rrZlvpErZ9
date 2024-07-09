using System;
using dnlib.DotNet.Writer;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x02000792 RID: 1938
	internal abstract class CpuArch
	{
		// Token: 0x06004537 RID: 17719
		public abstract uint GetStubAlignment(StubType stubType);

		// Token: 0x06004538 RID: 17720
		public abstract uint GetStubSize(StubType stubType);

		// Token: 0x06004539 RID: 17721
		public abstract uint GetStubCodeOffset(StubType stubType);

		// Token: 0x0600453A RID: 17722 RVA: 0x0016D0E8 File Offset: 0x0016D0E8
		public static bool TryGetCpuArch(Machine machine, out CpuArch cpuArch)
		{
			if (machine <= Machine.I386_Native_Linux)
			{
				if (machine <= Machine.ARMNT_Native_NetBSD)
				{
					if (machine <= Machine.ARMNT)
					{
						if (machine != Machine.I386)
						{
							if (machine != Machine.ARMNT)
							{
								goto IL_180;
							}
							goto IL_177;
						}
					}
					else
					{
						if (machine == Machine.IA64)
						{
							cpuArch = CpuArch.itaniumCpuArch;
							return true;
						}
						if (machine == Machine.ARM64_Native_FreeBSD)
						{
							goto IL_180;
						}
						if (machine != Machine.ARMNT_Native_NetBSD)
						{
							goto IL_180;
						}
						goto IL_177;
					}
				}
				else if (machine <= Machine.AMD64_Native_FreeBSD)
				{
					if (machine != Machine.I386_Native_NetBSD)
					{
						if (machine != Machine.AMD64_Native_FreeBSD)
						{
							goto IL_180;
						}
						goto IL_165;
					}
				}
				else if (machine != Machine.I386_Native_Apple)
				{
					if (machine == Machine.ARMNT_Native_Apple)
					{
						goto IL_177;
					}
					if (machine != Machine.I386_Native_Linux)
					{
						goto IL_180;
					}
				}
			}
			else if (machine <= Machine.ARMNT_Native_FreeBSD)
			{
				if (machine <= Machine.AMD64)
				{
					if (machine == Machine.ARMNT_Native_Linux)
					{
						goto IL_177;
					}
					if (machine != Machine.AMD64)
					{
						goto IL_180;
					}
					goto IL_165;
				}
				else
				{
					if (machine == Machine.AMD64_Native_NetBSD)
					{
						goto IL_165;
					}
					if (machine == Machine.ARM64)
					{
						goto IL_180;
					}
					if (machine != Machine.ARMNT_Native_FreeBSD)
					{
						goto IL_180;
					}
					goto IL_177;
				}
			}
			else if (machine <= Machine.AMD64_Native_Apple)
			{
				if (machine != Machine.I386_Native_FreeBSD)
				{
					if (machine == Machine.ARM64_Native_NetBSD)
					{
						goto IL_180;
					}
					if (machine != Machine.AMD64_Native_Apple)
					{
						goto IL_180;
					}
					goto IL_165;
				}
			}
			else
			{
				if (machine == Machine.ARM64_Native_Linux || machine == Machine.ARM64_Native_Apple)
				{
					goto IL_180;
				}
				if (machine != Machine.AMD64_Native_Linux)
				{
					goto IL_180;
				}
				goto IL_165;
			}
			cpuArch = CpuArch.x86CpuArch;
			return true;
			IL_165:
			cpuArch = CpuArch.x64CpuArch;
			return true;
			IL_177:
			cpuArch = CpuArch.armCpuArch;
			return true;
			IL_180:
			cpuArch = null;
			return false;
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x0016D280 File Offset: 0x0016D280
		public bool TryGetExportedRvaFromStub(ref DataReader reader, IPEImage peImage, out uint funcRva)
		{
			return this.TryGetExportedRvaFromStubCore(ref reader, peImage, out funcRva);
		}

		// Token: 0x0600453C RID: 17724
		protected abstract bool TryGetExportedRvaFromStubCore(ref DataReader reader, IPEImage peImage, out uint funcRva);

		// Token: 0x0600453D RID: 17725
		public abstract void WriteStubRelocs(StubType stubType, RelocDirectory relocDirectory, IChunk chunk, uint stubOffset);

		// Token: 0x0600453E RID: 17726
		public abstract void WriteStub(StubType stubType, DataWriter writer, ulong imageBase, uint stubRva, uint managedFuncRva);

		// Token: 0x0400243F RID: 9279
		private static readonly X86CpuArch x86CpuArch = new X86CpuArch();

		// Token: 0x04002440 RID: 9280
		private static readonly X64CpuArch x64CpuArch = new X64CpuArch();

		// Token: 0x04002441 RID: 9281
		private static readonly ItaniumCpuArch itaniumCpuArch = new ItaniumCpuArch();

		// Token: 0x04002442 RID: 9282
		private static readonly ArmCpuArch armCpuArch = new ArmCpuArch();
	}
}
