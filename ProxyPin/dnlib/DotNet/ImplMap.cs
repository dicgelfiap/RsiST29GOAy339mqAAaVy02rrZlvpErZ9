using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet
{
	// Token: 0x020007E7 RID: 2023
	[DebuggerDisplay("{Module} {Name}")]
	[ComVisible(true)]
	public abstract class ImplMap : IMDTokenProvider
	{
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0017729C File Offset: 0x0017729C
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.ImplMap, this.rid);
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060048D6 RID: 18646 RVA: 0x001772AC File Offset: 0x001772AC
		// (set) Token: 0x060048D7 RID: 18647 RVA: 0x001772B4 File Offset: 0x001772B4
		public uint Rid
		{
			get
			{
				return this.rid;
			}
			set
			{
				this.rid = value;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060048D8 RID: 18648 RVA: 0x001772C0 File Offset: 0x001772C0
		// (set) Token: 0x060048D9 RID: 18649 RVA: 0x001772CC File Offset: 0x001772CC
		public PInvokeAttributes Attributes
		{
			get
			{
				return (PInvokeAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x060048DA RID: 18650 RVA: 0x001772D8 File Offset: 0x001772D8
		// (set) Token: 0x060048DB RID: 18651 RVA: 0x001772E0 File Offset: 0x001772E0
		public UTF8String Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x060048DC RID: 18652 RVA: 0x001772EC File Offset: 0x001772EC
		// (set) Token: 0x060048DD RID: 18653 RVA: 0x001772F4 File Offset: 0x001772F4
		public ModuleRef Module
		{
			get
			{
				return this.module;
			}
			set
			{
				this.module = value;
			}
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x00177300 File Offset: 0x00177300
		private void ModifyAttributes(PInvokeAttributes andMask, PInvokeAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x00177314 File Offset: 0x00177314
		private void ModifyAttributes(bool set, PInvokeAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x0017733C File Offset: 0x0017733C
		// (set) Token: 0x060048E1 RID: 18657 RVA: 0x0017734C File Offset: 0x0017734C
		public bool IsNoMangle
		{
			get
			{
				return ((ushort)this.attributes & 1) > 0;
			}
			set
			{
				this.ModifyAttributes(value, PInvokeAttributes.NoMangle);
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060048E2 RID: 18658 RVA: 0x00177358 File Offset: 0x00177358
		// (set) Token: 0x060048E3 RID: 18659 RVA: 0x00177364 File Offset: 0x00177364
		public PInvokeAttributes CharSet
		{
			get
			{
				return (PInvokeAttributes)this.attributes & PInvokeAttributes.CharSetMask;
			}
			set
			{
				this.ModifyAttributes(~PInvokeAttributes.CharSetMask, value & PInvokeAttributes.CharSetMask);
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x00177374 File Offset: 0x00177374
		public bool IsCharSetNotSpec
		{
			get
			{
				return ((ushort)this.attributes & 6) == 0;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x00177384 File Offset: 0x00177384
		public bool IsCharSetAnsi
		{
			get
			{
				return ((ushort)this.attributes & 6) == 2;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060048E6 RID: 18662 RVA: 0x00177394 File Offset: 0x00177394
		public bool IsCharSetUnicode
		{
			get
			{
				return ((ushort)this.attributes & 6) == 4;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x001773A4 File Offset: 0x001773A4
		public bool IsCharSetAuto
		{
			get
			{
				return ((ushort)this.attributes & 6) == 6;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060048E8 RID: 18664 RVA: 0x001773B4 File Offset: 0x001773B4
		// (set) Token: 0x060048E9 RID: 18665 RVA: 0x001773C0 File Offset: 0x001773C0
		public PInvokeAttributes BestFit
		{
			get
			{
				return (PInvokeAttributes)this.attributes & PInvokeAttributes.BestFitMask;
			}
			set
			{
				this.ModifyAttributes(~(PInvokeAttributes.BestFitEnabled | PInvokeAttributes.BestFitDisabled), value & PInvokeAttributes.BestFitMask);
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x001773D4 File Offset: 0x001773D4
		public bool IsBestFitUseAssem
		{
			get
			{
				return ((ushort)this.attributes & 48) == 0;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x001773E4 File Offset: 0x001773E4
		public bool IsBestFitEnabled
		{
			get
			{
				return ((ushort)this.attributes & 48) == 16;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x001773F4 File Offset: 0x001773F4
		public bool IsBestFitDisabled
		{
			get
			{
				return ((ushort)this.attributes & 48) == 32;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x00177404 File Offset: 0x00177404
		// (set) Token: 0x060048EE RID: 18670 RVA: 0x00177414 File Offset: 0x00177414
		public PInvokeAttributes ThrowOnUnmappableChar
		{
			get
			{
				return (PInvokeAttributes)this.attributes & PInvokeAttributes.ThrowOnUnmappableCharMask;
			}
			set
			{
				this.ModifyAttributes(~(PInvokeAttributes.ThrowOnUnmappableCharEnabled | PInvokeAttributes.ThrowOnUnmappableCharDisabled), value & PInvokeAttributes.ThrowOnUnmappableCharMask);
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x00177428 File Offset: 0x00177428
		public bool IsThrowOnUnmappableCharUseAssem
		{
			get
			{
				return ((ushort)this.attributes & 12288) == 0;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060048F0 RID: 18672 RVA: 0x0017743C File Offset: 0x0017743C
		public bool IsThrowOnUnmappableCharEnabled
		{
			get
			{
				return ((ushort)this.attributes & 12288) == 4096;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x00177454 File Offset: 0x00177454
		public bool IsThrowOnUnmappableCharDisabled
		{
			get
			{
				return ((ushort)this.attributes & 12288) == 8192;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060048F2 RID: 18674 RVA: 0x0017746C File Offset: 0x0017746C
		// (set) Token: 0x060048F3 RID: 18675 RVA: 0x0017747C File Offset: 0x0017747C
		public bool SupportsLastError
		{
			get
			{
				return ((ushort)this.attributes & 64) > 0;
			}
			set
			{
				this.ModifyAttributes(value, PInvokeAttributes.SupportsLastError);
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x00177488 File Offset: 0x00177488
		// (set) Token: 0x060048F5 RID: 18677 RVA: 0x00177498 File Offset: 0x00177498
		public PInvokeAttributes CallConv
		{
			get
			{
				return (PInvokeAttributes)this.attributes & PInvokeAttributes.CallConvMask;
			}
			set
			{
				this.ModifyAttributes(~PInvokeAttributes.CallConvMask, value & PInvokeAttributes.CallConvMask);
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x001774AC File Offset: 0x001774AC
		public bool IsCallConvWinapi
		{
			get
			{
				return ((ushort)this.attributes & 1792) == 256;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060048F7 RID: 18679 RVA: 0x001774C4 File Offset: 0x001774C4
		public bool IsCallConvCdecl
		{
			get
			{
				return ((ushort)this.attributes & 1792) == 512;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x001774DC File Offset: 0x001774DC
		public bool IsCallConvStdcall
		{
			get
			{
				return ((ushort)this.attributes & 1792) == 768;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060048F9 RID: 18681 RVA: 0x001774F4 File Offset: 0x001774F4
		public bool IsCallConvThiscall
		{
			get
			{
				return ((ushort)this.attributes & 1792) == 1024;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x0017750C File Offset: 0x0017750C
		public bool IsCallConvFastcall
		{
			get
			{
				return ((ushort)this.attributes & 1792) == 1280;
			}
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x00177524 File Offset: 0x00177524
		public bool IsPinvokeMethod(string dllName, string funcName)
		{
			return this.IsPinvokeMethod(dllName, funcName, ImplMap.IsWindows());
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x00177534 File Offset: 0x00177534
		public bool IsPinvokeMethod(string dllName, string funcName, bool treatAsWindows)
		{
			if (this.name != funcName)
			{
				return false;
			}
			ModuleRef moduleRef = this.module;
			return moduleRef != null && ImplMap.GetDllName(dllName, treatAsWindows).Equals(ImplMap.GetDllName(moduleRef.Name, treatAsWindows), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x00177588 File Offset: 0x00177588
		private static string GetDllName(string dllName, bool treatAsWindows)
		{
			if (treatAsWindows)
			{
				dllName = dllName.TrimEnd(ImplMap.trimChars);
			}
			if (dllName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
			{
				return dllName.Substring(0, dllName.Length - 4);
			}
			return dllName;
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x001775D0 File Offset: 0x001775D0
		private static bool IsWindows()
		{
			return Path.DirectorySeparatorChar == '\\' || Path.AltDirectorySeparatorChar == '\\';
		}

		// Token: 0x0400251A RID: 9498
		protected uint rid;

		// Token: 0x0400251B RID: 9499
		protected int attributes;

		// Token: 0x0400251C RID: 9500
		protected UTF8String name;

		// Token: 0x0400251D RID: 9501
		protected ModuleRef module;

		// Token: 0x0400251E RID: 9502
		private static readonly char[] trimChars = new char[]
		{
			' '
		};
	}
}
