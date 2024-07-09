using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x020000BB RID: 187
[CompilerGenerated]
internal sealed class <>f__AnonymousType0<<iconBytes>j__TPar, <iconFileInf>j__TPar>
{
	// Token: 0x170001BC RID: 444
	// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0003FDA4 File Offset: 0x0003FDA4
	public <iconBytes>j__TPar iconBytes
	{
		get
		{
			return this.<iconBytes>i__Field;
		}
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0003FDAC File Offset: 0x0003FDAC
	public <iconFileInf>j__TPar iconFileInf
	{
		get
		{
			return this.<iconFileInf>i__Field;
		}
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0003FDB4 File Offset: 0x0003FDB4
	[DebuggerHidden]
	public <>f__AnonymousType0(<iconBytes>j__TPar iconBytes, <iconFileInf>j__TPar iconFileInf)
	{
		this.<iconBytes>i__Field = iconBytes;
		this.<iconFileInf>i__Field = iconFileInf;
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0003FDCC File Offset: 0x0003FDCC
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		IconExtractor.<>f__AnonymousType0<<iconBytes>j__TPar, <iconFileInf>j__TPar> iconExtractor.<>f__AnonymousType = value as IconExtractor.<>f__AnonymousType0<<iconBytes>j__TPar, <iconFileInf>j__TPar>;
		return iconExtractor.<>f__AnonymousType != null && EqualityComparer<<iconBytes>j__TPar>.Default.Equals(this.<iconBytes>i__Field, iconExtractor.<>f__AnonymousType.<iconBytes>i__Field) && EqualityComparer<<iconFileInf>j__TPar>.Default.Equals(this.<iconFileInf>i__Field, iconExtractor.<>f__AnonymousType.<iconFileInf>i__Field);
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0003FE20 File Offset: 0x0003FE20
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (193787062 * -1521134295 + EqualityComparer<<iconBytes>j__TPar>.Default.GetHashCode(this.<iconBytes>i__Field)) * -1521134295 + EqualityComparer<<iconFileInf>j__TPar>.Default.GetHashCode(this.<iconFileInf>i__Field);
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0003FE58 File Offset: 0x0003FE58
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ iconBytes = {0}, iconFileInf = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<iconBytes>j__TPar <iconBytes>j__TPar = this.<iconBytes>i__Field;
		ref <iconBytes>j__TPar ptr = ref <iconBytes>j__TPar;
		<iconBytes>j__TPar <iconBytes>j__TPar2 = default(<iconBytes>j__TPar);
		object obj;
		if (<iconBytes>j__TPar2 == null)
		{
			<iconBytes>j__TPar2 = <iconBytes>j__TPar;
			ptr = ref <iconBytes>j__TPar2;
			if (<iconBytes>j__TPar2 == null)
			{
				obj = null;
				goto IL_4F;
			}
		}
		obj = ptr.ToString();
		IL_4F:
		array[num] = obj;
		int num2 = 1;
		<iconFileInf>j__TPar <iconFileInf>j__TPar = this.<iconFileInf>i__Field;
		ref <iconFileInf>j__TPar ptr2 = ref <iconFileInf>j__TPar;
		<iconFileInf>j__TPar <iconFileInf>j__TPar2 = default(<iconFileInf>j__TPar);
		object obj2;
		if (<iconFileInf>j__TPar2 == null)
		{
			<iconFileInf>j__TPar2 = <iconFileInf>j__TPar;
			ptr2 = ref <iconFileInf>j__TPar2;
			if (<iconFileInf>j__TPar2 == null)
			{
				obj2 = null;
				goto IL_93;
			}
		}
		obj2 = ptr2.ToString();
		IL_93:
		array[num2] = obj2;
		return string.Format(provider, format, array);
	}

	// Token: 0x0400055D RID: 1373
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <iconBytes>j__TPar <iconBytes>i__Field;

	// Token: 0x0400055E RID: 1374
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <iconFileInf>j__TPar <iconFileInf>i__Field;
}
