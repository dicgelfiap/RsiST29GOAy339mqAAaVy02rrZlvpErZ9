using System;
using System.Collections.Generic;

namespace dnlib.DotNet
{
	// Token: 0x020007B6 RID: 1974
	internal sealed class GenericArguments
	{
		// Token: 0x060047E0 RID: 18400 RVA: 0x001766BC File Offset: 0x001766BC
		public void PushTypeArgs(IList<TypeSig> typeArgs)
		{
			this.typeArgsStack.Push(typeArgs);
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x001766CC File Offset: 0x001766CC
		public IList<TypeSig> PopTypeArgs()
		{
			return this.typeArgsStack.Pop();
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x001766DC File Offset: 0x001766DC
		public void PushMethodArgs(IList<TypeSig> methodArgs)
		{
			this.methodArgsStack.Push(methodArgs);
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x001766EC File Offset: 0x001766EC
		public IList<TypeSig> PopMethodArgs()
		{
			return this.methodArgsStack.Pop();
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x001766FC File Offset: 0x001766FC
		public TypeSig Resolve(TypeSig typeSig)
		{
			if (typeSig == null)
			{
				return null;
			}
			GenericMVar genericMVar = typeSig as GenericMVar;
			if (genericMVar != null)
			{
				TypeSig typeSig2 = this.methodArgsStack.Resolve(genericMVar.Number);
				if (typeSig2 == null || typeSig2 == typeSig)
				{
					return typeSig;
				}
				return typeSig2;
			}
			else
			{
				GenericVar genericVar = typeSig as GenericVar;
				if (genericVar == null)
				{
					return typeSig;
				}
				TypeSig typeSig3 = this.typeArgsStack.Resolve(genericVar.Number);
				if (typeSig3 == null || typeSig3 == typeSig)
				{
					return typeSig;
				}
				return typeSig3;
			}
		}

		// Token: 0x040024EC RID: 9452
		private GenericArgumentsStack typeArgsStack = new GenericArgumentsStack(true);

		// Token: 0x040024ED RID: 9453
		private GenericArgumentsStack methodArgsStack = new GenericArgumentsStack(false);
	}
}
