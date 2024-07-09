using System;
using System.Collections.Generic;
using System.Text;

namespace dnlib.DotNet
{
	// Token: 0x02000862 RID: 2146
	internal sealed class ReflectionTypeNameParser : TypeNameParser
	{
		// Token: 0x06005214 RID: 21012 RVA: 0x00194E5C File Offset: 0x00194E5C
		public ReflectionTypeNameParser(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper) : base(ownerModule, typeFullName, typeNameParserHelper, default(GenericParamContext))
		{
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x00194E80 File Offset: 0x00194E80
		public ReflectionTypeNameParser(ModuleDef ownerModule, string typeFullName, IAssemblyRefFinder typeNameParserHelper, GenericParamContext gpContext) : base(ownerModule, typeFullName, typeNameParserHelper, gpContext)
		{
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x00194E90 File Offset: 0x00194E90
		public static AssemblyRef ParseAssemblyRef(string asmFullName)
		{
			return ReflectionTypeNameParser.ParseAssemblyRef(asmFullName, default(GenericParamContext));
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00194EB0 File Offset: 0x00194EB0
		public static AssemblyRef ParseAssemblyRef(string asmFullName, GenericParamContext gpContext)
		{
			AssemblyRef result;
			try
			{
				using (ReflectionTypeNameParser reflectionTypeNameParser = new ReflectionTypeNameParser(null, asmFullName, null, gpContext))
				{
					result = reflectionTypeNameParser.ReadAssemblyRef();
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00194F08 File Offset: 0x00194F08
		internal override TypeSig ParseAsTypeSig()
		{
			TypeSig result;
			try
			{
				TypeSig typeSig = this.ReadType(true);
				base.SkipWhite();
				TypeNameParser.Verify(base.PeekChar() == -1, "Extra input after type name");
				result = typeSig;
			}
			catch (TypeNameParserException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new TypeNameParserException("Could not parse type name", innerException);
			}
			return result;
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x00194F68 File Offset: 0x00194F68
		private TypeSig ReadType(bool readAssemblyReference)
		{
			base.RecursionIncrement();
			base.SkipWhite();
			TypeSig typeSig;
			if (base.PeekChar() == 33)
			{
				GenericSig currentSig = base.ReadGenericSig();
				IList<TypeNameParser.TSpec> tspecs = this.ReadTSpecs();
				this.ReadOptionalAssemblyRef();
				typeSig = base.CreateTypeSig(tspecs, currentSig);
			}
			else
			{
				TypeRef typeRef = base.ReadTypeRefAndNestedNoAssembly('+');
				IList<TypeNameParser.TSpec> list = this.ReadTSpecs();
				TypeRef nonNestedTypeRef = TypeRef.GetNonNestedTypeRef(typeRef);
				AssemblyRef assemblyRef;
				if (readAssemblyReference)
				{
					assemblyRef = (this.ReadOptionalAssemblyRef() ?? base.FindAssemblyRef(nonNestedTypeRef));
				}
				else
				{
					assemblyRef = base.FindAssemblyRef(nonNestedTypeRef);
				}
				nonNestedTypeRef.ResolutionScope = assemblyRef;
				typeSig = null;
				if (typeRef == nonNestedTypeRef)
				{
					CorLibTypeSig corLibTypeSig = this.ownerModule.CorLibTypes.GetCorLibTypeSig(typeRef.Namespace, typeRef.Name, typeRef.DefinitionAssembly);
					if (corLibTypeSig != null)
					{
						typeSig = corLibTypeSig;
					}
				}
				if (typeSig == null)
				{
					TypeDef typeDef = this.Resolve(assemblyRef, typeRef);
					ITypeDefOrRef type;
					if (typeDef == null)
					{
						ITypeDefOrRef typeDefOrRef = typeRef;
						type = typeDefOrRef;
					}
					else
					{
						ITypeDefOrRef typeDefOrRef = typeDef;
						type = typeDefOrRef;
					}
					typeSig = base.ToTypeSig(type);
				}
				if (list.Count != 0)
				{
					typeSig = base.CreateTypeSig(list, typeSig);
				}
			}
			base.RecursionDecrement();
			return typeSig;
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x00195088 File Offset: 0x00195088
		private TypeDef Resolve(AssemblyRef asmRef, TypeRef typeRef)
		{
			AssemblyDef assembly = this.ownerModule.Assembly;
			if (assembly == null)
			{
				return null;
			}
			if (!AssemblyNameComparer.CompareAll.Equals(asmRef, assembly) || asmRef.IsRetargetable != assembly.IsRetargetable)
			{
				return null;
			}
			TypeDef typeDef = assembly.Find(typeRef);
			if (typeDef == null || typeDef.Module != this.ownerModule)
			{
				return null;
			}
			return typeDef;
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x001950F4 File Offset: 0x001950F4
		private AssemblyRef ReadOptionalAssemblyRef()
		{
			base.SkipWhite();
			if (base.PeekChar() == 44)
			{
				base.ReadChar();
				return this.ReadAssemblyRef();
			}
			return null;
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00195118 File Offset: 0x00195118
		private IList<TypeNameParser.TSpec> ReadTSpecs()
		{
			List<TypeNameParser.TSpec> list = new List<TypeNameParser.TSpec>();
			for (;;)
			{
				base.SkipWhite();
				int num = base.PeekChar();
				if (num != 38)
				{
					if (num != 42)
					{
						if (num != 91)
						{
							break;
						}
						base.ReadChar();
						base.SkipWhite();
						int num2 = base.PeekChar();
						if (num2 == 93)
						{
							TypeNameParser.Verify(base.ReadChar() == 93, "Expected ']'");
							list.Add(TypeNameParser.SZArraySpec.Instance);
						}
						else if (num2 == 42 || num2 == 44 || num2 == 45 || char.IsDigit((char)num2))
						{
							TypeNameParser.ArraySpec arraySpec = new TypeNameParser.ArraySpec();
							arraySpec.rank = 0U;
							for (;;)
							{
								base.SkipWhite();
								int num3 = base.PeekChar();
								if (num3 == 42)
								{
									base.ReadChar();
								}
								else if (num3 != 44 && num3 != 93)
								{
									if (num3 == 45 || char.IsDigit((char)num3))
									{
										int num4 = base.ReadInt32();
										base.SkipWhite();
										TypeNameParser.Verify(base.ReadChar() == 46, "Expected '.'");
										TypeNameParser.Verify(base.ReadChar() == 46, "Expected '.'");
										uint? num5;
										if (base.PeekChar() == 46)
										{
											base.ReadChar();
											num5 = null;
										}
										else
										{
											base.SkipWhite();
											if (base.PeekChar() == 45)
											{
												int num6 = base.ReadInt32();
												TypeNameParser.Verify(num6 >= num4, "upper < lower");
												num5 = new uint?((uint)(num6 - num4 + 1));
												TypeNameParser.Verify(num5.Value != 0U && num5.Value <= 536870911U, "Invalid size");
											}
											else
											{
												long num7 = (long)((ulong)base.ReadUInt32() - (ulong)((long)num4) + 1UL);
												TypeNameParser.Verify(num7 > 0L && num7 <= 536870911L, "Invalid size");
												num5 = new uint?((uint)num7);
											}
										}
										if ((long)arraySpec.lowerBounds.Count == (long)((ulong)arraySpec.rank))
										{
											arraySpec.lowerBounds.Add(num4);
										}
										if (num5 != null && (long)arraySpec.sizes.Count == (long)((ulong)arraySpec.rank))
										{
											arraySpec.sizes.Add(num5.Value);
										}
									}
									else
									{
										TypeNameParser.Verify(false, "Unknown char");
									}
								}
								arraySpec.rank += 1U;
								base.SkipWhite();
								if (base.PeekChar() != 44)
								{
									break;
								}
								base.ReadChar();
							}
							TypeNameParser.Verify(base.ReadChar() == 93, "Expected ']'");
							list.Add(arraySpec);
						}
						else
						{
							TypeNameParser.GenericInstSpec genericInstSpec = new TypeNameParser.GenericInstSpec();
							for (;;)
							{
								base.SkipWhite();
								num2 = base.PeekChar();
								bool flag = num2 == 91;
								if (num2 == 93)
								{
									break;
								}
								TypeNameParser.Verify(!flag || base.ReadChar() == 91, "Expected '['");
								genericInstSpec.args.Add(this.ReadType(flag));
								base.SkipWhite();
								TypeNameParser.Verify(!flag || base.ReadChar() == 93, "Expected ']'");
								base.SkipWhite();
								if (base.PeekChar() != 44)
								{
									break;
								}
								base.ReadChar();
							}
							TypeNameParser.Verify(base.ReadChar() == 93, "Expected ']'");
							list.Add(genericInstSpec);
						}
					}
					else
					{
						base.ReadChar();
						list.Add(TypeNameParser.PtrSpec.Instance);
					}
				}
				else
				{
					base.ReadChar();
					list.Add(TypeNameParser.ByRefSpec.Instance);
				}
			}
			return list;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x001954B8 File Offset: 0x001954B8
		private AssemblyRef ReadAssemblyRef()
		{
			AssemblyRefUser assemblyRefUser = new AssemblyRefUser();
			if (this.ownerModule != null)
			{
				this.ownerModule.UpdateRowId<AssemblyRefUser>(assemblyRefUser);
			}
			assemblyRefUser.Name = this.ReadAssemblyNameId();
			base.SkipWhite();
			if (base.PeekChar() != 44)
			{
				return assemblyRefUser;
			}
			base.ReadChar();
			for (;;)
			{
				base.SkipWhite();
				int num = base.PeekChar();
				if (num == -1 || num == 93)
				{
					break;
				}
				if (num == 44)
				{
					base.ReadChar();
				}
				else
				{
					string text = base.ReadId();
					base.SkipWhite();
					if (base.PeekChar() == 61)
					{
						base.ReadChar();
						string text2 = base.ReadId();
						string text3 = text.ToUpperInvariant();
						if (text3 != null)
						{
							uint num2 = dnlib.<PrivateImplementationDetails>.ComputeStringHash(text3);
							if (num2 <= 866246516U)
							{
								if (num2 != 210398395U)
								{
									if (num2 != 246083825U)
									{
										if (num2 != 866246516U)
										{
											continue;
										}
										if (!(text3 == "PUBLICKEYTOKEN"))
										{
											continue;
										}
										if (text2.Equals("null", StringComparison.OrdinalIgnoreCase) || text2.Equals("neutral", StringComparison.OrdinalIgnoreCase))
										{
											assemblyRefUser.PublicKeyOrToken = new PublicKeyToken();
											continue;
										}
										assemblyRefUser.PublicKeyOrToken = PublicKeyBase.CreatePublicKeyToken(Utils.ParseBytes(text2));
										continue;
									}
									else if (!(text3 == "CULTURE"))
									{
										continue;
									}
								}
								else if (!(text3 == "LANGUAGE"))
								{
									continue;
								}
								if (text2.Equals("neutral", StringComparison.OrdinalIgnoreCase))
								{
									assemblyRefUser.Culture = UTF8String.Empty;
								}
								else
								{
									assemblyRefUser.Culture = text2;
								}
							}
							else if (num2 <= 2408447615U)
							{
								if (num2 != 1815527209U)
								{
									if (num2 == 2408447615U)
									{
										if (text3 == "PUBLICKEY")
										{
											if (text2.Equals("null", StringComparison.OrdinalIgnoreCase) || text2.Equals("neutral", StringComparison.OrdinalIgnoreCase))
											{
												assemblyRefUser.PublicKeyOrToken = new PublicKey();
											}
											else
											{
												assemblyRefUser.PublicKeyOrToken = PublicKeyBase.CreatePublicKey(Utils.ParseBytes(text2));
											}
										}
									}
								}
								else if (text3 == "RETARGETABLE")
								{
									if (text2.Equals("Yes", StringComparison.OrdinalIgnoreCase))
									{
										assemblyRefUser.IsRetargetable = true;
									}
									else
									{
										assemblyRefUser.IsRetargetable = false;
									}
								}
							}
							else if (num2 != 3552295351U)
							{
								if (num2 == 4149707570U)
								{
									if (text3 == "CONTENTTYPE")
									{
										if (text2.Equals("WindowsRuntime", StringComparison.OrdinalIgnoreCase))
										{
											assemblyRefUser.ContentType = AssemblyAttributes.ContentType_WindowsRuntime;
										}
										else
										{
											assemblyRefUser.ContentType = AssemblyAttributes.None;
										}
									}
								}
							}
							else if (text3 == "VERSION")
							{
								assemblyRefUser.Version = Utils.ParseVersion(text2);
							}
						}
					}
				}
			}
			return assemblyRefUser;
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x001957BC File Offset: 0x001957BC
		private string ReadAssemblyNameId()
		{
			base.SkipWhite();
			StringBuilder stringBuilder = new StringBuilder();
			int asmNameChar;
			while ((asmNameChar = this.GetAsmNameChar()) != -1)
			{
				stringBuilder.Append((char)asmNameChar);
			}
			string text = stringBuilder.ToString().Trim();
			TypeNameParser.Verify(text.Length > 0, "Expected an assembly name");
			return text;
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x00195810 File Offset: 0x00195810
		private int GetAsmNameChar()
		{
			int num = base.PeekChar();
			if (num == -1)
			{
				return -1;
			}
			if (num != 44)
			{
				if (num == 92)
				{
					base.ReadChar();
					return base.ReadChar();
				}
				if (num != 93)
				{
					return base.ReadChar();
				}
			}
			return -1;
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00195864 File Offset: 0x00195864
		internal override int GetIdChar(bool ignoreWhiteSpace)
		{
			int num = base.PeekChar();
			if (num == -1)
			{
				return -1;
			}
			if (ignoreWhiteSpace && char.IsWhiteSpace((char)num))
			{
				return -1;
			}
			if (num <= 44)
			{
				if (num != 38 && num - 42 > 2)
				{
					goto IL_75;
				}
			}
			else if (num != 61)
			{
				switch (num)
				{
				case 91:
				case 93:
					break;
				case 92:
					base.ReadChar();
					return base.ReadChar();
				default:
					goto IL_75;
				}
			}
			return -1;
			IL_75:
			return base.ReadChar();
		}
	}
}
