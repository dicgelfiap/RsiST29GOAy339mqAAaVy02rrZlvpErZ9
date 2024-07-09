using System;
using System.Runtime.InteropServices;

namespace dnlib.PE
{
	// Token: 0x02000759 RID: 1881
	[ComVisible(true)]
	public static class MachineExtensions
	{
		// Token: 0x060041C9 RID: 16841 RVA: 0x00163868 File Offset: 0x00163868
		public static bool Is64Bit(this Machine machine)
		{
			if (machine <= Machine.AMD64_Native_NetBSD)
			{
				if (machine <= Machine.ARM64_Native_FreeBSD)
				{
					if (machine != Machine.IA64 && machine != Machine.ARM64_Native_FreeBSD)
					{
						return false;
					}
				}
				else if (machine != Machine.AMD64_Native_FreeBSD && machine != Machine.AMD64 && machine != Machine.AMD64_Native_NetBSD)
				{
					return false;
				}
			}
			else if (machine <= Machine.AMD64_Native_Apple)
			{
				if (machine != Machine.ARM64 && machine != Machine.ARM64_Native_NetBSD && machine != Machine.AMD64_Native_Apple)
				{
					return false;
				}
			}
			else if (machine != Machine.ARM64_Native_Linux && machine != Machine.ARM64_Native_Apple && machine != Machine.AMD64_Native_Linux)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x00163928 File Offset: 0x00163928
		public static bool IsI386(this Machine machine)
		{
			if (machine <= Machine.I386_Native_NetBSD)
			{
				if (machine != Machine.I386 && machine != Machine.I386_Native_NetBSD)
				{
					return false;
				}
			}
			else if (machine != Machine.I386_Native_Apple && machine != Machine.I386_Native_Linux && machine != Machine.I386_Native_FreeBSD)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x00163984 File Offset: 0x00163984
		public static bool IsAMD64(this Machine machine)
		{
			if (machine <= Machine.AMD64)
			{
				if (machine != Machine.AMD64_Native_FreeBSD && machine != Machine.AMD64)
				{
					return false;
				}
			}
			else if (machine != Machine.AMD64_Native_NetBSD && machine != Machine.AMD64_Native_Apple && machine != Machine.AMD64_Native_Linux)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x001639E0 File Offset: 0x001639E0
		public static bool IsARMNT(this Machine machine)
		{
			if (machine <= Machine.ARMNT_Native_NetBSD)
			{
				if (machine != Machine.ARMNT && machine != Machine.ARMNT_Native_NetBSD)
				{
					return false;
				}
			}
			else if (machine != Machine.ARMNT_Native_Apple && machine != Machine.ARMNT_Native_Linux && machine != Machine.ARMNT_Native_FreeBSD)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x00163A3C File Offset: 0x00163A3C
		public static bool IsARM64(this Machine machine)
		{
			if (machine <= Machine.ARM64)
			{
				if (machine != Machine.ARM64_Native_FreeBSD && machine != Machine.ARM64)
				{
					return false;
				}
			}
			else if (machine != Machine.ARM64_Native_NetBSD && machine != Machine.ARM64_Native_Linux && machine != Machine.ARM64_Native_Apple)
			{
				return false;
			}
			return true;
		}
	}
}
