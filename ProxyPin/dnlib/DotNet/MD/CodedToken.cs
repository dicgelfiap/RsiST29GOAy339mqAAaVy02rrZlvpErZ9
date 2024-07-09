using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000983 RID: 2435
	[ComVisible(true)]
	public sealed class CodedToken
	{
		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x001C26B0 File Offset: 0x001C26B0
		public Table[] TableTypes
		{
			get
			{
				return this.tableTypes;
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x001C26B8 File Offset: 0x001C26B8
		public int Bits
		{
			get
			{
				return this.bits;
			}
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x001C26C0 File Offset: 0x001C26C0
		internal CodedToken(int bits, Table[] tableTypes)
		{
			this.bits = bits;
			this.mask = (1 << bits) - 1;
			this.tableTypes = tableTypes;
		}

		// Token: 0x06005DDA RID: 24026 RVA: 0x001C26E4 File Offset: 0x001C26E4
		public uint Encode(MDToken token)
		{
			return this.Encode(token.Raw);
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x001C26F4 File Offset: 0x001C26F4
		public uint Encode(uint token)
		{
			uint result;
			this.Encode(token, out result);
			return result;
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x001C2710 File Offset: 0x001C2710
		public bool Encode(MDToken token, out uint codedToken)
		{
			return this.Encode(token.Raw, out codedToken);
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x001C2720 File Offset: 0x001C2720
		public bool Encode(uint token, out uint codedToken)
		{
			int num = Array.IndexOf<Table>(this.tableTypes, MDToken.ToTable(token));
			if (num < 0)
			{
				codedToken = uint.MaxValue;
				return false;
			}
			codedToken = (MDToken.ToRID(token) << this.bits | (uint)num);
			return true;
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x001C2764 File Offset: 0x001C2764
		public MDToken Decode2(uint codedToken)
		{
			uint token;
			this.Decode(codedToken, out token);
			return new MDToken(token);
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x001C2788 File Offset: 0x001C2788
		public uint Decode(uint codedToken)
		{
			uint result;
			this.Decode(codedToken, out result);
			return result;
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x001C27A4 File Offset: 0x001C27A4
		public bool Decode(uint codedToken, out MDToken token)
		{
			uint token2;
			bool result = this.Decode(codedToken, out token2);
			token = new MDToken(token2);
			return result;
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x001C27CC File Offset: 0x001C27CC
		public bool Decode(uint codedToken, out uint token)
		{
			uint num = codedToken >> this.bits;
			int num2 = (int)((ulong)codedToken & (ulong)((long)this.mask));
			if (num > 16777215U || num2 >= this.tableTypes.Length)
			{
				token = 0U;
				return false;
			}
			token = ((uint)((uint)this.tableTypes[num2] << 24) | num);
			return true;
		}

		// Token: 0x04002D7D RID: 11645
		public static readonly CodedToken TypeDefOrRef = new CodedToken(2, new Table[]
		{
			Table.TypeDef,
			Table.TypeRef,
			Table.TypeSpec
		});

		// Token: 0x04002D7E RID: 11646
		public static readonly CodedToken HasConstant = new CodedToken(2, new Table[]
		{
			Table.Field,
			Table.Param,
			Table.Property
		});

		// Token: 0x04002D7F RID: 11647
		public static readonly CodedToken HasCustomAttribute = new CodedToken(5, new Table[]
		{
			Table.Method,
			Table.Field,
			Table.TypeRef,
			Table.TypeDef,
			Table.Param,
			Table.InterfaceImpl,
			Table.MemberRef,
			Table.Module,
			Table.DeclSecurity,
			Table.Property,
			Table.Event,
			Table.StandAloneSig,
			Table.ModuleRef,
			Table.TypeSpec,
			Table.Assembly,
			Table.AssemblyRef,
			Table.File,
			Table.ExportedType,
			Table.ManifestResource,
			Table.GenericParam,
			Table.GenericParamConstraint,
			Table.MethodSpec,
			Table.Module,
			Table.Module
		});

		// Token: 0x04002D80 RID: 11648
		public static readonly CodedToken HasFieldMarshal = new CodedToken(1, new Table[]
		{
			Table.Field,
			Table.Param
		});

		// Token: 0x04002D81 RID: 11649
		public static readonly CodedToken HasDeclSecurity = new CodedToken(2, new Table[]
		{
			Table.TypeDef,
			Table.Method,
			Table.Assembly
		});

		// Token: 0x04002D82 RID: 11650
		public static readonly CodedToken MemberRefParent = new CodedToken(3, new Table[]
		{
			Table.TypeDef,
			Table.TypeRef,
			Table.ModuleRef,
			Table.Method,
			Table.TypeSpec
		});

		// Token: 0x04002D83 RID: 11651
		public static readonly CodedToken HasSemantic = new CodedToken(1, new Table[]
		{
			Table.Event,
			Table.Property
		});

		// Token: 0x04002D84 RID: 11652
		public static readonly CodedToken MethodDefOrRef = new CodedToken(1, new Table[]
		{
			Table.Method,
			Table.MemberRef
		});

		// Token: 0x04002D85 RID: 11653
		public static readonly CodedToken MemberForwarded = new CodedToken(1, new Table[]
		{
			Table.Field,
			Table.Method
		});

		// Token: 0x04002D86 RID: 11654
		public static readonly CodedToken Implementation = new CodedToken(2, new Table[]
		{
			Table.File,
			Table.AssemblyRef,
			Table.ExportedType
		});

		// Token: 0x04002D87 RID: 11655
		public static readonly CodedToken CustomAttributeType = new CodedToken(3, new Table[]
		{
			Table.Module,
			Table.Module,
			Table.Method,
			Table.MemberRef
		});

		// Token: 0x04002D88 RID: 11656
		public static readonly CodedToken ResolutionScope = new CodedToken(2, new Table[]
		{
			Table.Module,
			Table.ModuleRef,
			Table.AssemblyRef,
			Table.TypeRef
		});

		// Token: 0x04002D89 RID: 11657
		public static readonly CodedToken TypeOrMethodDef = new CodedToken(1, new Table[]
		{
			Table.TypeDef,
			Table.Method
		});

		// Token: 0x04002D8A RID: 11658
		public static readonly CodedToken HasCustomDebugInformation = new CodedToken(5, new Table[]
		{
			Table.Method,
			Table.Field,
			Table.TypeRef,
			Table.TypeDef,
			Table.Param,
			Table.InterfaceImpl,
			Table.MemberRef,
			Table.Module,
			Table.DeclSecurity,
			Table.Property,
			Table.Event,
			Table.StandAloneSig,
			Table.ModuleRef,
			Table.TypeSpec,
			Table.Assembly,
			Table.AssemblyRef,
			Table.File,
			Table.ExportedType,
			Table.ManifestResource,
			Table.GenericParam,
			Table.GenericParamConstraint,
			Table.MethodSpec,
			Table.Document,
			Table.LocalScope,
			Table.LocalVariable,
			Table.LocalConstant,
			Table.ImportScope
		});

		// Token: 0x04002D8B RID: 11659
		private readonly Table[] tableTypes;

		// Token: 0x04002D8C RID: 11660
		private readonly int bits;

		// Token: 0x04002D8D RID: 11661
		private readonly int mask;
	}
}
