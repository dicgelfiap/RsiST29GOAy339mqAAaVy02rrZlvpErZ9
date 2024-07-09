using System;
using System.Collections.Generic;
using dnlib.DotNet.Pdb.Symbols;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x0200097F RID: 2431
	internal sealed class SymbolScopeImpl : SymbolScope
	{
		// Token: 0x06005DA8 RID: 23976 RVA: 0x001C1C64 File Offset: 0x001C1C64
		public SymbolScopeImpl(ISymUnmanagedScope scope, SymbolMethod method, SymbolScope parent)
		{
			this.scope = scope;
			this.method = method;
			this.parent = parent;
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06005DA9 RID: 23977 RVA: 0x001C1C84 File Offset: 0x001C1C84
		public override SymbolMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x001C1C8C File Offset: 0x001C1C8C
		public override SymbolScope Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06005DAB RID: 23979 RVA: 0x001C1C94 File Offset: 0x001C1C94
		public override int StartOffset
		{
			get
			{
				uint result;
				this.scope.GetStartOffset(out result);
				return (int)result;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06005DAC RID: 23980 RVA: 0x001C1CB4 File Offset: 0x001C1CB4
		public override int EndOffset
		{
			get
			{
				uint result;
				this.scope.GetEndOffset(out result);
				return (int)result;
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06005DAD RID: 23981 RVA: 0x001C1CD4 File Offset: 0x001C1CD4
		public override IList<SymbolScope> Children
		{
			get
			{
				if (this.children == null)
				{
					uint num;
					this.scope.GetChildren(0U, out num, null);
					ISymUnmanagedScope[] array = new ISymUnmanagedScope[num];
					this.scope.GetChildren((uint)array.Length, out num, array);
					SymbolScope[] array2 = new SymbolScope[num];
					for (uint num2 = 0U; num2 < num; num2 += 1U)
					{
						array2[(int)num2] = new SymbolScopeImpl(array[(int)num2], this.method, this);
					}
					this.children = array2;
				}
				return this.children;
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x001C1D5C File Offset: 0x001C1D5C
		public override IList<SymbolVariable> Locals
		{
			get
			{
				if (this.locals == null)
				{
					uint num;
					this.scope.GetLocals(0U, out num, null);
					ISymUnmanagedVariable[] array = new ISymUnmanagedVariable[num];
					this.scope.GetLocals((uint)array.Length, out num, array);
					SymbolVariable[] array2 = new SymbolVariable[num];
					for (uint num2 = 0U; num2 < num; num2 += 1U)
					{
						array2[(int)num2] = new SymbolVariableImpl(array[(int)num2]);
					}
					this.locals = array2;
				}
				return this.locals;
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x001C1DDC File Offset: 0x001C1DDC
		public override IList<SymbolNamespace> Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					uint num;
					this.scope.GetNamespaces(0U, out num, null);
					ISymUnmanagedNamespace[] array = new ISymUnmanagedNamespace[num];
					this.scope.GetNamespaces((uint)array.Length, out num, array);
					SymbolNamespace[] array2 = new SymbolNamespace[num];
					for (uint num2 = 0U; num2 < num; num2 += 1U)
					{
						array2[(int)num2] = new SymbolNamespaceImpl(array[(int)num2]);
					}
					this.namespaces = array2;
				}
				return this.namespaces;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x001C1E5C File Offset: 0x001C1E5C
		public override IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				return Array2.Empty<PdbCustomDebugInfo>();
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06005DB1 RID: 23985 RVA: 0x001C1E64 File Offset: 0x001C1E64
		public override PdbImportScope ImportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005DB2 RID: 23986 RVA: 0x001C1E68 File Offset: 0x001C1E68
		public override IList<PdbConstant> GetConstants(ModuleDef module, GenericParamContext gpContext)
		{
			ISymUnmanagedScope2 symUnmanagedScope = this.scope as ISymUnmanagedScope2;
			if (symUnmanagedScope == null)
			{
				return Array2.Empty<PdbConstant>();
			}
			uint num;
			symUnmanagedScope.GetConstants(0U, out num, null);
			if (num == 0U)
			{
				return Array2.Empty<PdbConstant>();
			}
			ISymUnmanagedConstant[] array = new ISymUnmanagedConstant[num];
			symUnmanagedScope.GetConstants((uint)array.Length, out num, array);
			PdbConstant[] array2 = new PdbConstant[num];
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				ISymUnmanagedConstant symUnmanagedConstant = array[(int)num2];
				string name = this.GetName(symUnmanagedConstant);
				object value;
				symUnmanagedConstant.GetValue(out value);
				byte[] signatureBytes = this.GetSignatureBytes(symUnmanagedConstant);
				TypeSig type;
				if (signatureBytes.Length == 0)
				{
					type = null;
				}
				else
				{
					type = SignatureReader.ReadTypeSig(module, module.CorLibTypes, signatureBytes, gpContext);
				}
				array2[(int)num2] = new PdbConstant(name, type, value);
			}
			return array2;
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x001C1F30 File Offset: 0x001C1F30
		private string GetName(ISymUnmanagedConstant unc)
		{
			uint num;
			unc.GetName(0U, out num, null);
			char[] array = new char[num];
			unc.GetName((uint)array.Length, out num, array);
			if (array.Length == 0)
			{
				return string.Empty;
			}
			return new string(array, 0, array.Length - 1);
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x001C1F78 File Offset: 0x001C1F78
		private byte[] GetSignatureBytes(ISymUnmanagedConstant unc)
		{
			uint num;
			int signature = unc.GetSignature(0U, out num, null);
			if (num == 0U || (signature < 0 && signature != -2147467259 && signature != -2147467263))
			{
				return Array2.Empty<byte>();
			}
			byte[] array = new byte[num];
			signature = unc.GetSignature((uint)array.Length, out num, array);
			if (signature != 0)
			{
				return Array2.Empty<byte>();
			}
			return array;
		}

		// Token: 0x04002D6F RID: 11631
		private readonly ISymUnmanagedScope scope;

		// Token: 0x04002D70 RID: 11632
		private readonly SymbolMethod method;

		// Token: 0x04002D71 RID: 11633
		private readonly SymbolScope parent;

		// Token: 0x04002D72 RID: 11634
		private volatile SymbolScope[] children;

		// Token: 0x04002D73 RID: 11635
		private volatile SymbolVariable[] locals;

		// Token: 0x04002D74 RID: 11636
		private volatile SymbolNamespace[] namespaces;
	}
}
