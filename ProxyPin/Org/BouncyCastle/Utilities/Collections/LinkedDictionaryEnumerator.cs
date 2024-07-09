using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020006CD RID: 1741
	internal class LinkedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06003CE9 RID: 15593 RVA: 0x0014F974 File Offset: 0x0014F974
		internal LinkedDictionaryEnumerator(LinkedDictionary parent)
		{
			this.parent = parent;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x0014F98C File Offset: 0x0014F98C
		public virtual object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x0014F99C File Offset: 0x0014F99C
		public virtual DictionaryEntry Entry
		{
			get
			{
				object currentKey = this.CurrentKey;
				return new DictionaryEntry(currentKey, this.parent.hash[currentKey]);
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x0014F9CC File Offset: 0x0014F9CC
		public virtual object Key
		{
			get
			{
				return this.CurrentKey;
			}
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x0014F9D4 File Offset: 0x0014F9D4
		public virtual bool MoveNext()
		{
			return this.pos < this.parent.keys.Count && ++this.pos < this.parent.keys.Count;
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x0014FA28 File Offset: 0x0014FA28
		public virtual void Reset()
		{
			this.pos = -1;
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x0014FA34 File Offset: 0x0014FA34
		public virtual object Value
		{
			get
			{
				return this.parent.hash[this.CurrentKey];
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x0014FA4C File Offset: 0x0014FA4C
		private object CurrentKey
		{
			get
			{
				if (this.pos < 0 || this.pos >= this.parent.keys.Count)
				{
					throw new InvalidOperationException();
				}
				return this.parent.keys[this.pos];
			}
		}

		// Token: 0x04001EE5 RID: 7909
		private readonly LinkedDictionary parent;

		// Token: 0x04001EE6 RID: 7910
		private int pos = -1;
	}
}
