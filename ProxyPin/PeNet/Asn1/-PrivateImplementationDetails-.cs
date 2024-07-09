using System;
using System.Runtime.CompilerServices;

// Token: 0x02000B88 RID: 2952
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x060076B9 RID: 30393 RVA: 0x002381D8 File Offset: 0x002381D8
	internal static uint ComputeStringHash(string s)
	{
		uint num;
		if (s != null)
		{
			num = 2166136261U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((uint)s[i] ^ num) * 16777619U;
			}
		}
		return num;
	}
}
