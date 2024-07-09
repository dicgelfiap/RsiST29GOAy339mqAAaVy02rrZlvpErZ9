using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PeNet.Utilities
{
	// Token: 0x02000B95 RID: 2965
	[ComVisible(true)]
	public static class FlagResolver
	{
		// Token: 0x06007786 RID: 30598 RVA: 0x0023A534 File Offset: 0x0023A534
		public static string ResolveSectionName(byte[] name)
		{
			return Encoding.UTF8.GetString(name).TrimEnd(new char[1]);
		}

		// Token: 0x06007787 RID: 30599 RVA: 0x0023A54C File Offset: 0x0023A54C
		public static string ResolveTargetMachine(ushort targetMachine)
		{
			string result = "unknown";
			if (targetMachine <= 497)
			{
				if (targetMachine <= 388)
				{
					if (targetMachine <= 354)
					{
						if (targetMachine != 332)
						{
							if (targetMachine != 333)
							{
								if (targetMachine == 354)
								{
									result = "MIPS R3000";
								}
							}
							else
							{
								result = "Intel i860";
							}
						}
						else
						{
							result = "Intel 386";
						}
					}
					else
					{
						switch (targetMachine)
						{
						case 358:
							result = "MIPS little endian (R4000)";
							break;
						case 359:
							break;
						case 360:
							result = "MIPS R10000";
							break;
						case 361:
							result = "MIPS little endian WCI v2";
							break;
						default:
							if (targetMachine != 387)
							{
								if (targetMachine == 388)
								{
									result = "Alpha AXP";
								}
							}
							else
							{
								result = "old Alpha AXP";
							}
							break;
						}
					}
				}
				else if (targetMachine <= 450)
				{
					switch (targetMachine)
					{
					case 418:
						result = "Hitachi SH3";
						break;
					case 419:
						result = "Hitachi SH3 DSP";
						break;
					case 420:
						result = "Hitachi SH3E";
						break;
					case 421:
					case 423:
						break;
					case 422:
						result = "Hitachi SH4";
						break;
					case 424:
						result = "Hitachi SH5";
						break;
					default:
						if (targetMachine != 448)
						{
							if (targetMachine == 450)
							{
								result = "Thumb";
							}
						}
						else
						{
							result = "ARM little endian";
						}
						break;
					}
				}
				else if (targetMachine != 467)
				{
					if (targetMachine != 496)
					{
						if (targetMachine == 497)
						{
							result = "PowerPC with floating point support";
						}
					}
					else
					{
						result = "PowerPC little endian";
					}
				}
				else
				{
					result = "Matsushita AM33";
				}
			}
			else if (targetMachine <= 1126)
			{
				if (targetMachine <= 616)
				{
					if (targetMachine != 512)
					{
						if (targetMachine != 614)
						{
							if (targetMachine == 616)
							{
								result = "Motorola 68000 series";
							}
						}
						else
						{
							result = "MIPS16";
						}
					}
					else
					{
						result = "Intel IA64";
					}
				}
				else if (targetMachine != 644)
				{
					if (targetMachine != 870)
					{
						if (targetMachine == 1126)
						{
							result = "MIPS16 with FPU";
						}
					}
					else
					{
						result = "MIPS with FPU";
					}
				}
				else
				{
					result = "Alpha AXP 64-bit";
				}
			}
			else if (targetMachine <= 3772)
			{
				if (targetMachine != 1312)
				{
					if (targetMachine != 3311)
					{
						if (targetMachine == 3772)
						{
							result = "EFI Byte Code";
						}
					}
					else
					{
						result = "CEF";
					}
				}
				else
				{
					result = "Tricore";
				}
			}
			else if (targetMachine != 34404)
			{
				if (targetMachine != 36929)
				{
					if (targetMachine == 49390)
					{
						result = "clr pure MSIL";
					}
				}
				else
				{
					result = "Mitsubishi M32R little endian";
				}
			}
			else
			{
				result = "AMD AMD64";
			}
			return result;
		}

		// Token: 0x06007788 RID: 30600 RVA: 0x0023A854 File Offset: 0x0023A854
		public static FileCharacteristics ResolveFileCharacteristics(ushort characteristics)
		{
			return new FileCharacteristics(characteristics);
		}

		// Token: 0x06007789 RID: 30601 RVA: 0x0023A85C File Offset: 0x0023A85C
		public static string ResolveResourceId(uint id)
		{
			switch (id)
			{
			case 1U:
				return "Cursor";
			case 2U:
				return "Bitmap";
			case 3U:
				return "Icon";
			case 4U:
				return "Menu";
			case 5U:
				return "Dialog";
			case 6U:
				return "String";
			case 7U:
				return "FontDirectory";
			case 8U:
				return "Fonst";
			case 9U:
				return "Accelerator";
			case 10U:
				return "RcData";
			case 11U:
				return "MessageTable";
			case 14U:
				return "GroupIcon";
			case 16U:
				return "Version";
			case 17U:
				return "DlgInclude";
			case 19U:
				return "PlugAndPlay";
			case 20U:
				return "VXD";
			case 21U:
				return "AnimatedCurser";
			case 22U:
				return "AnimatedIcon";
			case 23U:
				return "HTML";
			case 24U:
				return "Manifest";
			}
			return "unknown";
		}

		// Token: 0x0600778A RID: 30602 RVA: 0x0023A958 File Offset: 0x0023A958
		public static string ResolveSubsystem(ushort subsystem)
		{
			string result = "unknown";
			switch (subsystem)
			{
			case 1:
				result = "native";
				break;
			case 2:
				result = "Windows/GUI";
				break;
			case 3:
				result = "Windows non-GUI";
				break;
			case 5:
				result = "OS/2";
				break;
			case 7:
				result = "POSIX";
				break;
			case 8:
				result = "Native Windows 9x Driver";
				break;
			case 9:
				result = "Windows CE";
				break;
			case 10:
				result = "EFI Application";
				break;
			case 11:
				result = "EFI boot service device";
				break;
			case 12:
				result = "EFI runtime driver";
				break;
			case 13:
				result = "EFI ROM";
				break;
			case 14:
				result = "XBox";
				break;
			}
			return result;
		}

		// Token: 0x0600778B RID: 30603 RVA: 0x0023AA34 File Offset: 0x0023AA34
		public static List<string> ResolveSectionFlags(uint sectionFlags)
		{
			List<string> list = new List<string>();
			foreach (Constants.SectionFlags sectionFlags2 in (Constants.SectionFlags[])Enum.GetValues(typeof(Constants.SectionFlags)))
			{
				if ((sectionFlags & (uint)sectionFlags2) == (uint)sectionFlags2)
				{
					list.Add(sectionFlags2.ToString());
				}
			}
			return list;
		}

		// Token: 0x0600778C RID: 30604 RVA: 0x0023AA94 File Offset: 0x0023AA94
		public static List<string> ResolveCOMImageFlags(uint comImageFlags)
		{
			List<string> list = new List<string>();
			foreach (DotNetConstants.COMImageFlag comimageFlag in (DotNetConstants.COMImageFlag[])Enum.GetValues(typeof(DotNetConstants.COMImageFlag)))
			{
				if ((comImageFlags & (uint)comimageFlag) == (uint)comimageFlag)
				{
					list.Add(comimageFlag.ToString());
				}
			}
			return list;
		}

		// Token: 0x0600778D RID: 30605 RVA: 0x0023AAF4 File Offset: 0x0023AAF4
		public static List<string> ResolveMaskValidFlags(ulong maskValid)
		{
			List<string> list = new List<string>();
			foreach (DotNetConstants.MaskValidFlags maskValidFlags in (DotNetConstants.MaskValidFlags[])Enum.GetValues(typeof(DotNetConstants.MaskValidFlags)))
			{
				if ((maskValid & (ulong)maskValidFlags) == (ulong)maskValidFlags)
				{
					list.Add(maskValidFlags.ToString());
				}
			}
			return list;
		}
	}
}
