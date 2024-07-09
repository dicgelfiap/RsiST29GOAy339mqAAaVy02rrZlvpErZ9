using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x02000C2E RID: 3118
	internal sealed class NetObjectCache
	{
		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x06007BB7 RID: 31671 RVA: 0x00246DA8 File Offset: 0x00246DA8
		private MutableList List
		{
			get
			{
				MutableList result;
				if ((result = this.underlyingList) == null)
				{
					result = (this.underlyingList = new MutableList());
				}
				return result;
			}
		}

		// Token: 0x06007BB8 RID: 31672 RVA: 0x00246DD4 File Offset: 0x00246DD4
		internal object GetKeyedObject(int key)
		{
			if (key-- == 0)
			{
				if (this.rootObject == null)
				{
					throw new ProtoException("No root object assigned");
				}
				return this.rootObject;
			}
			else
			{
				BasicList list = this.List;
				if (key < 0 || key >= list.Count)
				{
					throw new ProtoException("Internal error; a missing key occurred");
				}
				object obj = list[key];
				if (obj == null)
				{
					throw new ProtoException("A deferred key does not have a value yet");
				}
				return obj;
			}
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x00246E4C File Offset: 0x00246E4C
		internal void SetKeyedObject(int key, object value)
		{
			if (key-- != 0)
			{
				MutableList list = this.List;
				if (key < list.Count)
				{
					object obj = list[key];
					if (obj == null)
					{
						list[key] = value;
						return;
					}
					if (obj != value)
					{
						throw new ProtoException("Reference-tracked objects cannot change reference");
					}
				}
				else if (key != list.Add(value))
				{
					throw new ProtoException("Internal error; a key mismatch occurred");
				}
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.rootObject != null && this.rootObject != value)
			{
				throw new ProtoException("The root object cannot be reassigned");
			}
			this.rootObject = value;
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x00246EF8 File Offset: 0x00246EF8
		internal int AddObjectKey(object value, out bool existing)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value == this.rootObject)
			{
				existing = true;
				return 0;
			}
			string text = value as string;
			BasicList list = this.List;
			int num;
			if (text == null)
			{
				if (this.objectKeys == null)
				{
					this.objectKeys = new Dictionary<object, int>(NetObjectCache.ReferenceComparer.Default);
					num = -1;
				}
				else if (!this.objectKeys.TryGetValue(value, out num))
				{
					num = -1;
				}
			}
			else if (this.stringKeys == null)
			{
				this.stringKeys = new Dictionary<string, int>();
				num = -1;
			}
			else if (!this.stringKeys.TryGetValue(text, out num))
			{
				num = -1;
			}
			if (!(existing = (num >= 0)))
			{
				num = list.Add(value);
				if (text == null)
				{
					this.objectKeys.Add(value, num);
				}
				else
				{
					this.stringKeys.Add(text, num);
				}
			}
			return num + 1;
		}

		// Token: 0x06007BBB RID: 31675 RVA: 0x00246FF0 File Offset: 0x00246FF0
		internal void RegisterTrappedObject(object value)
		{
			if (this.rootObject == null)
			{
				this.rootObject = value;
				return;
			}
			if (this.underlyingList != null)
			{
				for (int i = this.trapStartIndex; i < this.underlyingList.Count; i++)
				{
					this.trapStartIndex = i + 1;
					if (this.underlyingList[i] == null)
					{
						this.underlyingList[i] = value;
						return;
					}
				}
			}
		}

		// Token: 0x06007BBC RID: 31676 RVA: 0x00247068 File Offset: 0x00247068
		internal void Clear()
		{
			this.trapStartIndex = 0;
			this.rootObject = null;
			if (this.underlyingList != null)
			{
				this.underlyingList.Clear();
			}
			if (this.stringKeys != null)
			{
				this.stringKeys.Clear();
			}
			if (this.objectKeys != null)
			{
				this.objectKeys.Clear();
			}
		}

		// Token: 0x04003BC2 RID: 15298
		internal const int Root = 0;

		// Token: 0x04003BC3 RID: 15299
		private MutableList underlyingList;

		// Token: 0x04003BC4 RID: 15300
		private object rootObject;

		// Token: 0x04003BC5 RID: 15301
		private int trapStartIndex;

		// Token: 0x04003BC6 RID: 15302
		private Dictionary<string, int> stringKeys;

		// Token: 0x04003BC7 RID: 15303
		private Dictionary<object, int> objectKeys;

		// Token: 0x0200116F RID: 4463
		private sealed class ReferenceComparer : IEqualityComparer<object>
		{
			// Token: 0x06009344 RID: 37700 RVA: 0x002C2B64 File Offset: 0x002C2B64
			private ReferenceComparer()
			{
			}

			// Token: 0x06009345 RID: 37701 RVA: 0x002C2B6C File Offset: 0x002C2B6C
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06009346 RID: 37702 RVA: 0x002C2B74 File Offset: 0x002C2B74
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}

			// Token: 0x04004B46 RID: 19270
			public static readonly NetObjectCache.ReferenceComparer Default = new NetObjectCache.ReferenceComparer();
		}
	}
}
