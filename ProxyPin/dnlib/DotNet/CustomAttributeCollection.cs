using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200079A RID: 1946
	[ComVisible(true)]
	public class CustomAttributeCollection : LazyList<CustomAttribute, object>
	{
		// Token: 0x06004597 RID: 17815 RVA: 0x0016DC80 File Offset: 0x0016DC80
		public CustomAttributeCollection()
		{
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x0016DC88 File Offset: 0x0016DC88
		public CustomAttributeCollection(int length, object context, Func<object, int, CustomAttribute> readOriginalValue) : base(length, context, readOriginalValue)
		{
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x0016DC94 File Offset: 0x0016DC94
		public bool IsDefined(string fullName)
		{
			return this.Find(fullName) != null;
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x0016DCA4 File Offset: 0x0016DCA4
		public void RemoveAll(string fullName)
		{
			for (int i = base.Count - 1; i >= 0; i--)
			{
				if (base[i].TypeFullName == fullName)
				{
					base.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x0016DCEC File Offset: 0x0016DCEC
		public CustomAttribute Find(string fullName)
		{
			foreach (CustomAttribute customAttribute in this)
			{
				if (customAttribute != null && customAttribute.TypeFullName == fullName)
				{
					return customAttribute;
				}
			}
			return null;
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x0016DD5C File Offset: 0x0016DD5C
		public IEnumerable<CustomAttribute> FindAll(string fullName)
		{
			foreach (CustomAttribute customAttribute in this)
			{
				if (customAttribute != null && customAttribute.TypeFullName == fullName)
				{
					yield return customAttribute;
				}
			}
			LazyList<CustomAttribute>.Enumerator enumerator = default(LazyList<CustomAttribute>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x0016DD74 File Offset: 0x0016DD74
		public CustomAttribute Find(IType attrType)
		{
			return this.Find(attrType, (SigComparerOptions)0U);
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x0016DD80 File Offset: 0x0016DD80
		public CustomAttribute Find(IType attrType, SigComparerOptions options)
		{
			SigComparer sigComparer = new SigComparer(options);
			foreach (CustomAttribute customAttribute in this)
			{
				if (sigComparer.Equals(customAttribute.AttributeType, attrType))
				{
					return customAttribute;
				}
			}
			return null;
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x0016DDF4 File Offset: 0x0016DDF4
		public IEnumerable<CustomAttribute> FindAll(IType attrType)
		{
			return this.FindAll(attrType, (SigComparerOptions)0U);
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x0016DE00 File Offset: 0x0016DE00
		public IEnumerable<CustomAttribute> FindAll(IType attrType, SigComparerOptions options)
		{
			SigComparer comparer = new SigComparer(options);
			foreach (CustomAttribute customAttribute in this)
			{
				if (comparer.Equals(customAttribute.AttributeType, attrType))
				{
					yield return customAttribute;
				}
			}
			LazyList<CustomAttribute>.Enumerator enumerator = default(LazyList<CustomAttribute>.Enumerator);
			yield break;
			yield break;
		}
	}
}
