using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000790 RID: 1936
	[ComVisible(true)]
	public sealed class CorLibTypes : ICorLibTypes
	{
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x0016CDA0 File Offset: 0x0016CDA0
		public CorLibTypeSig Void
		{
			get
			{
				return this.typeVoid;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x0016CDA8 File Offset: 0x0016CDA8
		public CorLibTypeSig Boolean
		{
			get
			{
				return this.typeBoolean;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004520 RID: 17696 RVA: 0x0016CDB0 File Offset: 0x0016CDB0
		public CorLibTypeSig Char
		{
			get
			{
				return this.typeChar;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x0016CDB8 File Offset: 0x0016CDB8
		public CorLibTypeSig SByte
		{
			get
			{
				return this.typeSByte;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004522 RID: 17698 RVA: 0x0016CDC0 File Offset: 0x0016CDC0
		public CorLibTypeSig Byte
		{
			get
			{
				return this.typeByte;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x0016CDC8 File Offset: 0x0016CDC8
		public CorLibTypeSig Int16
		{
			get
			{
				return this.typeInt16;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x0016CDD0 File Offset: 0x0016CDD0
		public CorLibTypeSig UInt16
		{
			get
			{
				return this.typeUInt16;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06004525 RID: 17701 RVA: 0x0016CDD8 File Offset: 0x0016CDD8
		public CorLibTypeSig Int32
		{
			get
			{
				return this.typeInt32;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x0016CDE0 File Offset: 0x0016CDE0
		public CorLibTypeSig UInt32
		{
			get
			{
				return this.typeUInt32;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x0016CDE8 File Offset: 0x0016CDE8
		public CorLibTypeSig Int64
		{
			get
			{
				return this.typeInt64;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x0016CDF0 File Offset: 0x0016CDF0
		public CorLibTypeSig UInt64
		{
			get
			{
				return this.typeUInt64;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x0016CDF8 File Offset: 0x0016CDF8
		public CorLibTypeSig Single
		{
			get
			{
				return this.typeSingle;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x0016CE00 File Offset: 0x0016CE00
		public CorLibTypeSig Double
		{
			get
			{
				return this.typeDouble;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x0016CE08 File Offset: 0x0016CE08
		public CorLibTypeSig String
		{
			get
			{
				return this.typeString;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x0016CE10 File Offset: 0x0016CE10
		public CorLibTypeSig TypedReference
		{
			get
			{
				return this.typeTypedReference;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x0016CE18 File Offset: 0x0016CE18
		public CorLibTypeSig IntPtr
		{
			get
			{
				return this.typeIntPtr;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x0016CE20 File Offset: 0x0016CE20
		public CorLibTypeSig UIntPtr
		{
			get
			{
				return this.typeUIntPtr;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600452F RID: 17711 RVA: 0x0016CE28 File Offset: 0x0016CE28
		public CorLibTypeSig Object
		{
			get
			{
				return this.typeObject;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x0016CE30 File Offset: 0x0016CE30
		public AssemblyRef AssemblyRef
		{
			get
			{
				return this.corLibAssemblyRef;
			}
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x0016CE38 File Offset: 0x0016CE38
		public CorLibTypes(ModuleDef module) : this(module, null)
		{
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x0016CE44 File Offset: 0x0016CE44
		public CorLibTypes(ModuleDef module, AssemblyRef corLibAssemblyRef)
		{
			this.module = module;
			this.corLibAssemblyRef = (corLibAssemblyRef ?? this.CreateCorLibAssemblyRef());
			this.Initialize();
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x0016CE70 File Offset: 0x0016CE70
		private AssemblyRef CreateCorLibAssemblyRef()
		{
			return this.module.UpdateRowId<AssemblyRefUser>(AssemblyRefUser.CreateMscorlibReferenceCLR20());
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x0016CE84 File Offset: 0x0016CE84
		private void Initialize()
		{
			bool isCorLib = this.module.Assembly.IsCorLib();
			this.typeVoid = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Void"), ElementType.Void);
			this.typeBoolean = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Boolean"), ElementType.Boolean);
			this.typeChar = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Char"), ElementType.Char);
			this.typeSByte = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "SByte"), ElementType.I1);
			this.typeByte = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Byte"), ElementType.U1);
			this.typeInt16 = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Int16"), ElementType.I2);
			this.typeUInt16 = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "UInt16"), ElementType.U2);
			this.typeInt32 = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Int32"), ElementType.I4);
			this.typeUInt32 = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "UInt32"), ElementType.U4);
			this.typeInt64 = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Int64"), ElementType.I8);
			this.typeUInt64 = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "UInt64"), ElementType.U8);
			this.typeSingle = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Single"), ElementType.R4);
			this.typeDouble = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Double"), ElementType.R8);
			this.typeString = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "String"), ElementType.String);
			this.typeTypedReference = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "TypedReference"), ElementType.TypedByRef);
			this.typeIntPtr = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "IntPtr"), ElementType.I);
			this.typeUIntPtr = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "UIntPtr"), ElementType.U);
			this.typeObject = new CorLibTypeSig(this.CreateCorLibTypeRef(isCorLib, "Object"), ElementType.Object);
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x0016D060 File Offset: 0x0016D060
		private ITypeDefOrRef CreateCorLibTypeRef(bool isCorLib, string name)
		{
			TypeRefUser typeRefUser = new TypeRefUser(this.module, "System", name, this.corLibAssemblyRef);
			if (isCorLib)
			{
				TypeDef typeDef = this.module.Find(typeRefUser);
				if (typeDef != null)
				{
					return typeDef;
				}
			}
			return this.module.UpdateRowId<TypeRefUser>(typeRefUser);
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x0016D0BC File Offset: 0x0016D0BC
		public TypeRef GetTypeRef(string @namespace, string name)
		{
			return this.module.UpdateRowId<TypeRefUser>(new TypeRefUser(this.module, @namespace, name, this.corLibAssemblyRef));
		}

		// Token: 0x04002428 RID: 9256
		private readonly ModuleDef module;

		// Token: 0x04002429 RID: 9257
		private CorLibTypeSig typeVoid;

		// Token: 0x0400242A RID: 9258
		private CorLibTypeSig typeBoolean;

		// Token: 0x0400242B RID: 9259
		private CorLibTypeSig typeChar;

		// Token: 0x0400242C RID: 9260
		private CorLibTypeSig typeSByte;

		// Token: 0x0400242D RID: 9261
		private CorLibTypeSig typeByte;

		// Token: 0x0400242E RID: 9262
		private CorLibTypeSig typeInt16;

		// Token: 0x0400242F RID: 9263
		private CorLibTypeSig typeUInt16;

		// Token: 0x04002430 RID: 9264
		private CorLibTypeSig typeInt32;

		// Token: 0x04002431 RID: 9265
		private CorLibTypeSig typeUInt32;

		// Token: 0x04002432 RID: 9266
		private CorLibTypeSig typeInt64;

		// Token: 0x04002433 RID: 9267
		private CorLibTypeSig typeUInt64;

		// Token: 0x04002434 RID: 9268
		private CorLibTypeSig typeSingle;

		// Token: 0x04002435 RID: 9269
		private CorLibTypeSig typeDouble;

		// Token: 0x04002436 RID: 9270
		private CorLibTypeSig typeString;

		// Token: 0x04002437 RID: 9271
		private CorLibTypeSig typeTypedReference;

		// Token: 0x04002438 RID: 9272
		private CorLibTypeSig typeIntPtr;

		// Token: 0x04002439 RID: 9273
		private CorLibTypeSig typeUIntPtr;

		// Token: 0x0400243A RID: 9274
		private CorLibTypeSig typeObject;

		// Token: 0x0400243B RID: 9275
		private readonly AssemblyRef corLibAssemblyRef;
	}
}
