using System;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Portable
{
	// Token: 0x0200093C RID: 2364
	internal struct LocalConstantSigBlobReader
	{
		// Token: 0x06005AE4 RID: 23268 RVA: 0x001BA960 File Offset: 0x001BA960
		public LocalConstantSigBlobReader(ModuleDef module, ref DataReader reader, GenericParamContext gpContext)
		{
			this.module = module;
			this.reader = reader;
			this.gpContext = gpContext;
			this.recursionCounter = default(RecursionCounter);
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x001BA988 File Offset: 0x001BA988
		public bool Read(out TypeSig type, out object value)
		{
			return this.ReadCatch(out type, out value);
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x001BA994 File Offset: 0x001BA994
		private bool ReadCatch(out TypeSig type, out object value)
		{
			try
			{
				return this.ReadCore(out type, out value);
			}
			catch
			{
			}
			type = null;
			value = null;
			return false;
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x001BA9D0 File Offset: 0x001BA9D0
		private bool ReadCore(out TypeSig type, out object value)
		{
			if (!this.recursionCounter.Increment())
			{
				type = null;
				value = null;
				return false;
			}
			bool flag;
			switch (this.reader.ReadByte())
			{
			case 2:
				type = this.module.CorLibTypes.Boolean;
				value = this.reader.ReadBoolean();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 3:
				type = this.module.CorLibTypes.Char;
				value = this.reader.ReadChar();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 4:
				type = this.module.CorLibTypes.SByte;
				value = this.reader.ReadSByte();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 5:
				type = this.module.CorLibTypes.Byte;
				value = this.reader.ReadByte();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 6:
				type = this.module.CorLibTypes.Int16;
				value = this.reader.ReadInt16();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 7:
				type = this.module.CorLibTypes.UInt16;
				value = this.reader.ReadUInt16();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 8:
				type = this.module.CorLibTypes.Int32;
				value = this.reader.ReadInt32();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 9:
				type = this.module.CorLibTypes.UInt32;
				value = this.reader.ReadUInt32();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 10:
				type = this.module.CorLibTypes.Int64;
				value = this.reader.ReadInt64();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 11:
				type = this.module.CorLibTypes.UInt64;
				value = this.reader.ReadUInt64();
				if (this.reader.Position < this.reader.Length)
				{
					type = this.ReadTypeDefOrRefSig();
				}
				flag = true;
				goto IL_703;
			case 12:
				type = this.module.CorLibTypes.Single;
				value = this.reader.ReadSingle();
				flag = true;
				goto IL_703;
			case 13:
				type = this.module.CorLibTypes.Double;
				value = this.reader.ReadDouble();
				flag = true;
				goto IL_703;
			case 14:
				type = this.module.CorLibTypes.String;
				value = this.ReadString();
				flag = true;
				goto IL_703;
			case 15:
				flag = this.ReadCatch(out type, out value);
				if (flag)
				{
					type = new PtrSig(type);
					goto IL_703;
				}
				goto IL_703;
			case 16:
				flag = this.ReadCatch(out type, out value);
				if (flag)
				{
					type = new ByRefSig(type);
					goto IL_703;
				}
				goto IL_703;
			case 17:
			{
				ITypeDefOrRef typeDefOrRef = this.ReadTypeDefOrRef();
				type = typeDefOrRef.ToTypeSig(true);
				value = null;
				UTF8String left;
				UTF8String left2;
				if (LocalConstantSigBlobReader.GetName(typeDefOrRef, out left, out left2) && left == LocalConstantSigBlobReader.stringSystem && typeDefOrRef.DefinitionAssembly.IsCorLib())
				{
					if (left2 == LocalConstantSigBlobReader.stringDecimal)
					{
						if (this.reader.Length - this.reader.Position != 13U)
						{
							break;
						}
						try
						{
							byte b = this.reader.ReadByte();
							value = new decimal(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32(), (b & 128) > 0, b & 127);
							goto IL_639;
						}
						catch
						{
							break;
						}
					}
					if (left2 == LocalConstantSigBlobReader.stringDateTime)
					{
						if (this.reader.Length - this.reader.Position != 8U)
						{
							break;
						}
						try
						{
							value = new DateTime(this.reader.ReadInt64());
						}
						catch
						{
							break;
						}
					}
				}
				IL_639:
				if (value == null && this.reader.Position != this.reader.Length)
				{
					value = this.reader.ReadRemainingBytes();
				}
				flag = true;
				goto IL_703;
			}
			case 18:
				type = new ClassSig(this.ReadTypeDefOrRef());
				value = ((this.reader.Position == this.reader.Length) ? null : this.reader.ReadRemainingBytes());
				flag = true;
				goto IL_703;
			case 28:
				type = this.module.CorLibTypes.Object;
				value = null;
				flag = true;
				goto IL_703;
			case 31:
			{
				ITypeDefOrRef typeDefOrRef = this.ReadTypeDefOrRef();
				flag = this.ReadCatch(out type, out value);
				if (flag)
				{
					type = new CModReqdSig(typeDefOrRef, type);
					goto IL_703;
				}
				goto IL_703;
			}
			case 32:
			{
				ITypeDefOrRef typeDefOrRef = this.ReadTypeDefOrRef();
				flag = this.ReadCatch(out type, out value);
				if (flag)
				{
					type = new CModOptSig(typeDefOrRef, type);
					goto IL_703;
				}
				goto IL_703;
			}
			}
			flag = false;
			type = null;
			value = null;
			IL_703:
			this.recursionCounter.Decrement();
			return flag;
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x001BB108 File Offset: 0x001BB108
		private static bool GetName(ITypeDefOrRef tdr, out UTF8String @namespace, out UTF8String name)
		{
			TypeRef typeRef = tdr as TypeRef;
			if (typeRef != null)
			{
				@namespace = typeRef.Namespace;
				name = typeRef.Name;
				return true;
			}
			TypeDef typeDef = tdr as TypeDef;
			if (typeDef != null)
			{
				@namespace = typeDef.Namespace;
				name = typeDef.Name;
				return true;
			}
			@namespace = null;
			name = null;
			return false;
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x001BB160 File Offset: 0x001BB160
		private TypeSig ReadTypeDefOrRefSig()
		{
			uint codedToken;
			if (!this.reader.TryReadCompressedUInt32(out codedToken))
			{
				return null;
			}
			return ((ISignatureReaderHelper)this.module).ResolveTypeDefOrRef(codedToken, this.gpContext).ToTypeSig(true);
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x001BB1A0 File Offset: 0x001BB1A0
		private ITypeDefOrRef ReadTypeDefOrRef()
		{
			uint codedToken;
			if (!this.reader.TryReadCompressedUInt32(out codedToken))
			{
				return null;
			}
			ITypeDefOrRef typeDefOrRef = ((ISignatureReaderHelper)this.module).ResolveTypeDefOrRef(codedToken, this.gpContext);
			CorLibTypeSig corLibTypeSig = this.module.CorLibTypes.GetCorLibTypeSig(typeDefOrRef);
			if (corLibTypeSig != null)
			{
				return corLibTypeSig.TypeDefOrRef;
			}
			return typeDefOrRef;
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x001BB1F8 File Offset: 0x001BB1F8
		private string ReadString()
		{
			if (this.reader.Position == this.reader.Length)
			{
				return string.Empty;
			}
			if (this.reader.ReadByte() == 255 && this.reader.Position == this.reader.Length)
			{
				return null;
			}
			uint position = this.reader.Position;
			this.reader.Position = position - 1U;
			return this.reader.ReadUtf16String((int)(this.reader.BytesLeft / 2U));
		}

		// Token: 0x04002BEE RID: 11246
		private readonly ModuleDef module;

		// Token: 0x04002BEF RID: 11247
		private DataReader reader;

		// Token: 0x04002BF0 RID: 11248
		private readonly GenericParamContext gpContext;

		// Token: 0x04002BF1 RID: 11249
		private RecursionCounter recursionCounter;

		// Token: 0x04002BF2 RID: 11250
		private static readonly UTF8String stringSystem = new UTF8String("System");

		// Token: 0x04002BF3 RID: 11251
		private static readonly UTF8String stringDecimal = new UTF8String("Decimal");

		// Token: 0x04002BF4 RID: 11252
		private static readonly UTF8String stringDateTime = new UTF8String("DateTime");
	}
}
