using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000868 RID: 2152
	[ComVisible(true)]
	public abstract class TypeDefOrRefSig : LeafSig
	{
		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x001960A8 File Offset: 0x001960A8
		public ITypeDefOrRef TypeDefOrRef
		{
			get
			{
				return this.typeDefOrRef;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06005297 RID: 21143 RVA: 0x001960B0 File Offset: 0x001960B0
		public bool IsTypeRef
		{
			get
			{
				return this.TypeRef != null;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06005298 RID: 21144 RVA: 0x001960C0 File Offset: 0x001960C0
		public bool IsTypeDef
		{
			get
			{
				return this.TypeDef != null;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x001960D0 File Offset: 0x001960D0
		public bool IsTypeSpec
		{
			get
			{
				return this.TypeSpec != null;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x001960E0 File Offset: 0x001960E0
		public TypeRef TypeRef
		{
			get
			{
				return this.typeDefOrRef as TypeRef;
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x001960F0 File Offset: 0x001960F0
		public TypeDef TypeDef
		{
			get
			{
				return this.typeDefOrRef as TypeDef;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x0600529C RID: 21148 RVA: 0x00196100 File Offset: 0x00196100
		public TypeSpec TypeSpec
		{
			get
			{
				return this.typeDefOrRef as TypeSpec;
			}
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x00196110 File Offset: 0x00196110
		protected TypeDefOrRefSig(ITypeDefOrRef typeDefOrRef)
		{
			this.typeDefOrRef = typeDefOrRef;
		}

		// Token: 0x040027CB RID: 10187
		private readonly ITypeDefOrRef typeDefOrRef;
	}
}
