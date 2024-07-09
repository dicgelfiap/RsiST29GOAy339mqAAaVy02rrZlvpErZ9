using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.DotNet
{
	// Token: 0x02000861 RID: 2145
	[ComVisible(true)]
	public abstract class TypeNameParser : IDisposable
	{
		// Token: 0x060051F2 RID: 20978 RVA: 0x00194754 File Offset: 0x00194754
		public static ITypeDefOrRef ParseReflectionThrow(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper)
		{
			return TypeNameParser.ParseReflectionThrow(ownerModule, typeFullName, typeNameParserHelper, default(GenericParamContext));
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00194778 File Offset: 0x00194778
		public static ITypeDefOrRef ParseReflectionThrow(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper, GenericParamContext gpContext)
		{
			ITypeDefOrRef result;
			using (ReflectionTypeNameParser reflectionTypeNameParser = new ReflectionTypeNameParser(ownerModule, typeFullName, typeNameParserHelper, gpContext))
			{
				result = reflectionTypeNameParser.Parse();
			}
			return result;
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x001947BC File Offset: 0x001947BC
		public static ITypeDefOrRef ParseReflection(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper)
		{
			return TypeNameParser.ParseReflection(ownerModule, typeFullName, typeNameParserHelper, default(GenericParamContext));
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x001947E0 File Offset: 0x001947E0
		public static ITypeDefOrRef ParseReflection(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper, GenericParamContext gpContext)
		{
			ITypeDefOrRef result;
			try
			{
				result = TypeNameParser.ParseReflectionThrow(ownerModule, typeFullName, typeNameParserHelper, gpContext);
			}
			catch (TypeNameParserException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00194818 File Offset: 0x00194818
		public static TypeSig ParseAsTypeSigReflectionThrow(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper)
		{
			return TypeNameParser.ParseAsTypeSigReflectionThrow(ownerModule, typeFullName, typeNameParserHelper, default(GenericParamContext));
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x0019483C File Offset: 0x0019483C
		public static TypeSig ParseAsTypeSigReflectionThrow(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper, GenericParamContext gpContext)
		{
			TypeSig result;
			using (ReflectionTypeNameParser reflectionTypeNameParser = new ReflectionTypeNameParser(ownerModule, typeFullName, typeNameParserHelper, gpContext))
			{
				result = reflectionTypeNameParser.ParseAsTypeSig();
			}
			return result;
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x00194880 File Offset: 0x00194880
		public static TypeSig ParseAsTypeSigReflection(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper)
		{
			return TypeNameParser.ParseAsTypeSigReflection(ownerModule, typeFullName, typeNameParserHelper, default(GenericParamContext));
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x001948A4 File Offset: 0x001948A4
		public static TypeSig ParseAsTypeSigReflection(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper, GenericParamContext gpContext)
		{
			TypeSig result;
			try
			{
				result = TypeNameParser.ParseAsTypeSigReflectionThrow(ownerModule, typeFullName, typeNameParserHelper, gpContext);
			}
			catch (TypeNameParserException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x001948DC File Offset: 0x001948DC
		protected TypeNameParser(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper) : this(ownerModule, typeFullName, typeNameParserHelper, default(GenericParamContext))
		{
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x00194900 File Offset: 0x00194900
		protected TypeNameParser(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper, GenericParamContext gpContext)
		{
			this.ownerModule = ownerModule;
			this.reader = new StringReader(typeFullName ?? string.Empty);
			this.typeNameParserHelper = typeNameParserHelper;
			this.gpContext = gpContext;
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00194938 File Offset: 0x00194938
		internal ITypeDefOrRef Parse()
		{
			return this.ownerModule.UpdateRowId<ITypeDefOrRef>(this.ParseAsTypeSig().ToTypeDefOrRef());
		}

		// Token: 0x060051FD RID: 20989
		internal abstract TypeSig ParseAsTypeSig();

		// Token: 0x060051FE RID: 20990 RVA: 0x00194950 File Offset: 0x00194950
		protected void RecursionIncrement()
		{
			if (!this.recursionCounter.Increment())
			{
				throw new TypeNameParserException("Stack overflow");
			}
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x00194970 File Offset: 0x00194970
		protected void RecursionDecrement()
		{
			this.recursionCounter.Decrement();
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00194980 File Offset: 0x00194980
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00194990 File Offset: 0x00194990
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.reader != null)
			{
				this.reader.Dispose();
			}
			this.reader = null;
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x001949B8 File Offset: 0x001949B8
		internal GenericSig ReadGenericSig()
		{
			TypeNameParser.Verify(this.ReadChar() == 33, "Expected '!'");
			if (this.PeekChar() == 33)
			{
				this.ReadChar();
				return new GenericMVar(this.ReadUInt32(), this.gpContext.Method);
			}
			return new GenericVar(this.ReadUInt32(), this.gpContext.Type);
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x00194A20 File Offset: 0x00194A20
		internal TypeSig CreateTypeSig(IList<TypeNameParser.TSpec> tspecs, TypeSig currentSig)
		{
			int count = tspecs.Count;
			int i = 0;
			while (i < count)
			{
				TypeNameParser.TSpec tspec = tspecs[i];
				ElementType etype = tspec.etype;
				switch (etype)
				{
				case ElementType.Ptr:
					currentSig = new PtrSig(currentSig);
					break;
				case ElementType.ByRef:
					currentSig = new ByRefSig(currentSig);
					break;
				case ElementType.ValueType:
				case ElementType.Class:
				case ElementType.Var:
					goto IL_BC;
				case ElementType.Array:
				{
					TypeNameParser.ArraySpec arraySpec = (TypeNameParser.ArraySpec)tspec;
					currentSig = new ArraySig(currentSig, arraySpec.rank, arraySpec.sizes, arraySpec.lowerBounds);
					break;
				}
				case ElementType.GenericInst:
				{
					TypeNameParser.GenericInstSpec genericInstSpec = (TypeNameParser.GenericInstSpec)tspec;
					currentSig = new GenericInstSig(currentSig as ClassOrValueTypeSig, genericInstSpec.args);
					break;
				}
				default:
					if (etype != ElementType.SZArray)
					{
						goto IL_BC;
					}
					currentSig = new SZArraySig(currentSig);
					break;
				}
				IL_C7:
				i++;
				continue;
				IL_BC:
				TypeNameParser.Verify(false, "Unknown TSpec");
				goto IL_C7;
			}
			return currentSig;
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x00194B04 File Offset: 0x00194B04
		protected TypeRef ReadTypeRefAndNestedNoAssembly(char nestedChar)
		{
			TypeRef typeRef = this.ReadTypeRefNoAssembly();
			for (;;)
			{
				this.SkipWhite();
				if (this.PeekChar() != (int)nestedChar)
				{
					break;
				}
				this.ReadChar();
				TypeRef typeRef2 = this.ReadTypeRefNoAssembly();
				typeRef2.ResolutionScope = typeRef;
				typeRef = typeRef2;
			}
			return typeRef;
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00194B48 File Offset: 0x00194B48
		protected TypeRef ReadTypeRefNoAssembly()
		{
			string s;
			string s2;
			TypeNameParser.GetNamespaceAndName(this.ReadId(false), out s, out s2);
			return this.ownerModule.UpdateRowId<TypeRefUser>(new TypeRefUser(this.ownerModule, s, s2));
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00194B8C File Offset: 0x00194B8C
		private static void GetNamespaceAndName(string fullName, out string ns, out string name)
		{
			int num = fullName.LastIndexOf('.');
			if (num < 0)
			{
				ns = string.Empty;
				name = fullName;
				return;
			}
			ns = fullName.Substring(0, num);
			name = fullName.Substring(num + 1);
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00194BD0 File Offset: 0x00194BD0
		internal TypeSig ToTypeSig(ITypeDefOrRef type)
		{
			TypeDef typeDef = type as TypeDef;
			if (typeDef != null)
			{
				return TypeNameParser.ToTypeSig(typeDef, typeDef.IsValueType);
			}
			TypeRef typeRef = type as TypeRef;
			if (typeRef != null)
			{
				return TypeNameParser.ToTypeSig(typeRef, this.IsValueType(typeRef));
			}
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return typeSpec.TypeSig;
			}
			TypeNameParser.Verify(false, "Unknown type");
			return null;
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x00194C38 File Offset: 0x00194C38
		private static TypeSig ToTypeSig(ITypeDefOrRef type, bool isValueType)
		{
			if (!isValueType)
			{
				return new ClassSig(type);
			}
			return new ValueTypeSig(type);
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x00194C50 File Offset: 0x00194C50
		internal AssemblyRef FindAssemblyRef(TypeRef nonNestedTypeRef)
		{
			AssemblyRef assemblyRef = null;
			if (nonNestedTypeRef != null && this.typeNameParserHelper != null)
			{
				assemblyRef = this.typeNameParserHelper.FindAssemblyRef(nonNestedTypeRef);
			}
			if (assemblyRef != null)
			{
				return assemblyRef;
			}
			AssemblyDef assembly = this.ownerModule.Assembly;
			if (assembly != null)
			{
				return this.ownerModule.UpdateRowId<AssemblyRef>(assembly.ToAssemblyRef());
			}
			return AssemblyRef.CurrentAssembly;
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x00194CB4 File Offset: 0x00194CB4
		internal bool IsValueType(TypeRef typeRef)
		{
			return typeRef != null && typeRef.IsValueType;
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x00194CC4 File Offset: 0x00194CC4
		internal static void Verify(bool b, string msg)
		{
			if (!b)
			{
				throw new TypeNameParserException(msg);
			}
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x00194CD4 File Offset: 0x00194CD4
		internal void SkipWhite()
		{
			for (;;)
			{
				int num = this.PeekChar();
				if (num == -1 || !char.IsWhiteSpace((char)num))
				{
					break;
				}
				this.ReadChar();
			}
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x00194D08 File Offset: 0x00194D08
		internal uint ReadUInt32()
		{
			this.SkipWhite();
			bool b = false;
			uint num = 0U;
			for (;;)
			{
				int num2 = this.PeekChar();
				if (num2 == -1 || num2 < 48 || num2 > 57)
				{
					break;
				}
				this.ReadChar();
				uint num3 = num * 10U + (uint)(num2 - 48);
				TypeNameParser.Verify(num3 >= num, "Integer overflow");
				num = num3;
				b = true;
			}
			TypeNameParser.Verify(b, "Expected an integer");
			return num;
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x00194D74 File Offset: 0x00194D74
		internal int ReadInt32()
		{
			this.SkipWhite();
			bool flag = false;
			if (this.PeekChar() == 45)
			{
				flag = true;
				this.ReadChar();
			}
			uint num = this.ReadUInt32();
			if (flag)
			{
				TypeNameParser.Verify(num <= 2147483648U, "Integer overflow");
				return (int)(-(int)num);
			}
			TypeNameParser.Verify(num <= 2147483647U, "Integer overflow");
			return (int)num;
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00194DE0 File Offset: 0x00194DE0
		internal string ReadId()
		{
			return this.ReadId(true);
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x00194DEC File Offset: 0x00194DEC
		internal string ReadId(bool ignoreWhiteSpace)
		{
			this.SkipWhite();
			StringBuilder stringBuilder = new StringBuilder();
			int idChar;
			while ((idChar = this.GetIdChar(ignoreWhiteSpace)) != -1)
			{
				stringBuilder.Append((char)idChar);
			}
			TypeNameParser.Verify(stringBuilder.Length > 0, "Expected an id");
			return stringBuilder.ToString();
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00194E3C File Offset: 0x00194E3C
		protected int PeekChar()
		{
			return this.reader.Peek();
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x00194E4C File Offset: 0x00194E4C
		protected int ReadChar()
		{
			return this.reader.Read();
		}

		// Token: 0x06005213 RID: 21011
		internal abstract int GetIdChar(bool ignoreWhiteSpace);

		// Token: 0x040027B9 RID: 10169
		protected ModuleDef ownerModule;

		// Token: 0x040027BA RID: 10170
		private readonly GenericParamContext gpContext;

		// Token: 0x040027BB RID: 10171
		private StringReader reader;

		// Token: 0x040027BC RID: 10172
		private readonly IAssemblyRefFinder typeNameParserHelper;

		// Token: 0x040027BD RID: 10173
		private RecursionCounter recursionCounter;

		// Token: 0x02001002 RID: 4098
		internal abstract class TSpec
		{
			// Token: 0x06008EE5 RID: 36581 RVA: 0x002AAF44 File Offset: 0x002AAF44
			protected TSpec(ElementType etype)
			{
				this.etype = etype;
			}

			// Token: 0x04004449 RID: 17481
			public readonly ElementType etype;
		}

		// Token: 0x02001003 RID: 4099
		internal sealed class SZArraySpec : TypeNameParser.TSpec
		{
			// Token: 0x06008EE6 RID: 36582 RVA: 0x002AAF54 File Offset: 0x002AAF54
			private SZArraySpec() : base(ElementType.SZArray)
			{
			}

			// Token: 0x0400444A RID: 17482
			public static readonly TypeNameParser.SZArraySpec Instance = new TypeNameParser.SZArraySpec();
		}

		// Token: 0x02001004 RID: 4100
		internal sealed class ArraySpec : TypeNameParser.TSpec
		{
			// Token: 0x06008EE8 RID: 36584 RVA: 0x002AAF6C File Offset: 0x002AAF6C
			public ArraySpec() : base(ElementType.Array)
			{
			}

			// Token: 0x0400444B RID: 17483
			public uint rank;

			// Token: 0x0400444C RID: 17484
			public readonly IList<uint> sizes = new List<uint>();

			// Token: 0x0400444D RID: 17485
			public readonly IList<int> lowerBounds = new List<int>();
		}

		// Token: 0x02001005 RID: 4101
		internal sealed class GenericInstSpec : TypeNameParser.TSpec
		{
			// Token: 0x06008EE9 RID: 36585 RVA: 0x002AAF8C File Offset: 0x002AAF8C
			public GenericInstSpec() : base(ElementType.GenericInst)
			{
			}

			// Token: 0x0400444E RID: 17486
			public readonly List<TypeSig> args = new List<TypeSig>();
		}

		// Token: 0x02001006 RID: 4102
		internal sealed class ByRefSpec : TypeNameParser.TSpec
		{
			// Token: 0x06008EEA RID: 36586 RVA: 0x002AAFA4 File Offset: 0x002AAFA4
			private ByRefSpec() : base(ElementType.ByRef)
			{
			}

			// Token: 0x0400444F RID: 17487
			public static readonly TypeNameParser.ByRefSpec Instance = new TypeNameParser.ByRefSpec();
		}

		// Token: 0x02001007 RID: 4103
		internal sealed class PtrSpec : TypeNameParser.TSpec
		{
			// Token: 0x06008EEC RID: 36588 RVA: 0x002AAFBC File Offset: 0x002AAFBC
			private PtrSpec() : base(ElementType.Ptr)
			{
			}

			// Token: 0x04004450 RID: 17488
			public static readonly TypeNameParser.PtrSpec Instance = new TypeNameParser.PtrSpec();
		}
	}
}
