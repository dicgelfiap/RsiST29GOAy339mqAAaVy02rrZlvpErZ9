using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D15 RID: 3349
	internal abstract class DialogTemplateUtil
	{
		// Token: 0x06008850 RID: 34896 RVA: 0x00291B78 File Offset: 0x00291B78
		internal static IntPtr ReadResourceId(IntPtr lpRes, out ResourceId rc)
		{
			rc = null;
			ushort num = (ushort)Marshal.ReadInt16(lpRes);
			if (num != 0)
			{
				if (num != 65535)
				{
					rc = new ResourceId(Marshal.PtrToStringUni(lpRes));
					lpRes = new IntPtr(lpRes.ToInt64() + (long)((rc.Name.Length + 1) * Marshal.SystemDefaultCharSize));
				}
				else
				{
					lpRes = new IntPtr(lpRes.ToInt64() + 2L);
					rc = new ResourceId((uint)((ushort)Marshal.ReadInt16(lpRes)));
					lpRes = new IntPtr(lpRes.ToInt64() + 2L);
				}
			}
			else
			{
				lpRes = new IntPtr(lpRes.ToInt64() + 2L);
			}
			return lpRes;
		}

		// Token: 0x06008851 RID: 34897 RVA: 0x00291C28 File Offset: 0x00291C28
		internal static void WriteResourceId(BinaryWriter w, ResourceId rc)
		{
			if (rc == null)
			{
				w.Write(0);
				return;
			}
			if (rc.IsIntResource())
			{
				w.Write(ushort.MaxValue);
				w.Write((ushort)((int)rc.Id));
				return;
			}
			ResourceUtil.PadToWORD(w);
			w.Write(Encoding.Unicode.GetBytes(rc.Name));
			w.Write(0);
		}

		// Token: 0x06008852 RID: 34898 RVA: 0x00291C94 File Offset: 0x00291C94
		internal static string StyleToString<W, D>(uint style)
		{
			List<string> list = new List<string>();
			list.AddRange(ResourceUtil.FlagsToList<W>(style));
			list.AddRange(ResourceUtil.FlagsToList<D>(style));
			return string.Join(" | ", list.ToArray());
		}

		// Token: 0x06008853 RID: 34899 RVA: 0x00291CD4 File Offset: 0x00291CD4
		internal static string StyleToString<W, D>(uint style, uint exstyle)
		{
			List<string> list = new List<string>();
			list.AddRange(ResourceUtil.FlagsToList<W>(style));
			list.AddRange(ResourceUtil.FlagsToList<D>(exstyle));
			return string.Join(" | ", list.ToArray());
		}

		// Token: 0x06008854 RID: 34900 RVA: 0x00291D14 File Offset: 0x00291D14
		internal static string StyleToString<W>(uint style)
		{
			List<string> list = new List<string>();
			list.AddRange(ResourceUtil.FlagsToList<W>(style));
			return string.Join(" | ", list.ToArray());
		}
	}
}
