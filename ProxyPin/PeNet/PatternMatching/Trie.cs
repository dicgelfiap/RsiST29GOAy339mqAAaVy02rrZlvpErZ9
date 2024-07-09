using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PeNet.PatternMatching
{
	// Token: 0x02000BEE RID: 3054
	[ComVisible(true)]
	public class Trie : Trie<byte, string>
	{
		// Token: 0x06007A92 RID: 31378 RVA: 0x002411EC File Offset: 0x002411EC
		public void Add(string pattern, Encoding encoding, string name)
		{
			byte[] bytes = encoding.GetBytes(pattern);
			base.Add(bytes, name);
		}

		// Token: 0x06007A93 RID: 31379 RVA: 0x00241210 File Offset: 0x00241210
		public void Add(byte[] pattern, string name)
		{
			base.Add(pattern, name);
		}
	}
}
