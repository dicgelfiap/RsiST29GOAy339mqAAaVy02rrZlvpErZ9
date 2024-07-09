using System;
using System.Collections.Generic;

namespace dnlib.DotNet
{
	// Token: 0x020007B5 RID: 1973
	internal readonly struct GenericArgumentsStack
	{
		// Token: 0x060047DC RID: 18396 RVA: 0x001765DC File Offset: 0x001765DC
		public GenericArgumentsStack(bool isTypeVar)
		{
			this.argsStack = new List<IList<TypeSig>>();
			this.isTypeVar = isTypeVar;
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x001765F0 File Offset: 0x001765F0
		public void Push(IList<TypeSig> args)
		{
			this.argsStack.Add(args);
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x00176600 File Offset: 0x00176600
		public IList<TypeSig> Pop()
		{
			int index = this.argsStack.Count - 1;
			IList<TypeSig> result = this.argsStack[index];
			this.argsStack.RemoveAt(index);
			return result;
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x00176638 File Offset: 0x00176638
		public TypeSig Resolve(uint number)
		{
			TypeSig result = null;
			for (int i = this.argsStack.Count - 1; i >= 0; i--)
			{
				IList<TypeSig> list = this.argsStack[i];
				if ((ulong)number >= (ulong)((long)list.Count))
				{
					return null;
				}
				TypeSig typeSig = list[(int)number];
				GenericSig genericSig = typeSig as GenericSig;
				if (genericSig == null || genericSig.IsTypeVar != this.isTypeVar)
				{
					return typeSig;
				}
				result = genericSig;
				number = genericSig.Number;
			}
			return result;
		}

		// Token: 0x040024EA RID: 9450
		private readonly List<IList<TypeSig>> argsStack;

		// Token: 0x040024EB RID: 9451
		private readonly bool isTypeVar;
	}
}
