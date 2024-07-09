using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200086D RID: 2157
	[ComVisible(true)]
	public abstract class GenericSig : LeafSig
	{
		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x00196188 File Offset: 0x00196188
		public bool HasOwner
		{
			get
			{
				return this.genericParamProvider != null;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x00196198 File Offset: 0x00196198
		public bool HasOwnerType
		{
			get
			{
				return this.OwnerType != null;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x001961A8 File Offset: 0x001961A8
		public bool HasOwnerMethod
		{
			get
			{
				return this.OwnerMethod != null;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x001961B8 File Offset: 0x001961B8
		public TypeDef OwnerType
		{
			get
			{
				return this.genericParamProvider as TypeDef;
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x001961C8 File Offset: 0x001961C8
		public MethodDef OwnerMethod
		{
			get
			{
				return this.genericParamProvider as MethodDef;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x001961D8 File Offset: 0x001961D8
		public uint Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x001961E0 File Offset: 0x001961E0
		public GenericParam GenericParam
		{
			get
			{
				ITypeOrMethodDef typeOrMethodDef = this.genericParamProvider;
				if (typeOrMethodDef == null)
				{
					return null;
				}
				IList<GenericParam> genericParameters = typeOrMethodDef.GenericParameters;
				int count = genericParameters.Count;
				for (int i = 0; i < count; i++)
				{
					GenericParam genericParam = genericParameters[i];
					if ((uint)genericParam.Number == this.number)
					{
						return genericParam;
					}
				}
				return null;
			}
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x0019623C File Offset: 0x0019623C
		protected GenericSig(bool isTypeVar, uint number) : this(isTypeVar, number, null)
		{
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x00196248 File Offset: 0x00196248
		protected GenericSig(bool isTypeVar, uint number, ITypeOrMethodDef genericParamProvider)
		{
			this.isTypeVar = isTypeVar;
			this.number = number;
			this.genericParamProvider = genericParamProvider;
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x00196268 File Offset: 0x00196268
		public bool IsMethodVar
		{
			get
			{
				return !this.isTypeVar;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x00196274 File Offset: 0x00196274
		public bool IsTypeVar
		{
			get
			{
				return this.isTypeVar;
			}
		}

		// Token: 0x040027CD RID: 10189
		private readonly bool isTypeVar;

		// Token: 0x040027CE RID: 10190
		private readonly uint number;

		// Token: 0x040027CF RID: 10191
		private readonly ITypeOrMethodDef genericParamProvider;
	}
}
