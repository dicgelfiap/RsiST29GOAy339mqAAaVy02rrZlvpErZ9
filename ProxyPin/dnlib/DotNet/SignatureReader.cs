using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet
{
	// Token: 0x0200084E RID: 2126
	[ComVisible(true)]
	public struct SignatureReader
	{
		// Token: 0x0600502E RID: 20526 RVA: 0x0018EC80 File Offset: 0x0018EC80
		public static CallingConventionSig ReadSig(ModuleDefMD readerModule, uint sig)
		{
			return SignatureReader.ReadSig(readerModule, sig, default(GenericParamContext));
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x0018ECA4 File Offset: 0x0018ECA4
		public static CallingConventionSig ReadSig(ModuleDefMD readerModule, uint sig, GenericParamContext gpContext)
		{
			CallingConventionSig result;
			try
			{
				SignatureReader signatureReader = new SignatureReader(readerModule, sig, gpContext);
				if (signatureReader.reader.Length == 0U)
				{
					result = null;
				}
				else
				{
					CallingConventionSig callingConventionSig = signatureReader.ReadSig();
					if (callingConventionSig != null)
					{
						callingConventionSig.ExtraData = signatureReader.GetExtraData();
					}
					result = callingConventionSig;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x0018ED10 File Offset: 0x0018ED10
		public static CallingConventionSig ReadSig(ModuleDefMD module, byte[] signature)
		{
			return SignatureReader.ReadSig(module, module.CorLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), default(GenericParamContext));
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x0018ED3C File Offset: 0x0018ED3C
		public static CallingConventionSig ReadSig(ModuleDefMD module, byte[] signature, GenericParamContext gpContext)
		{
			return SignatureReader.ReadSig(module, module.CorLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), gpContext);
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x0018ED54 File Offset: 0x0018ED54
		public static CallingConventionSig ReadSig(ModuleDefMD module, DataReader signature)
		{
			return SignatureReader.ReadSig(module, module.CorLibTypes, signature, default(GenericParamContext));
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x0018ED7C File Offset: 0x0018ED7C
		public static CallingConventionSig ReadSig(ModuleDefMD module, DataReader signature, GenericParamContext gpContext)
		{
			return SignatureReader.ReadSig(module, module.CorLibTypes, signature, gpContext);
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x0018ED8C File Offset: 0x0018ED8C
		public static CallingConventionSig ReadSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, byte[] signature)
		{
			return SignatureReader.ReadSig(helper, corLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), default(GenericParamContext));
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x0018EDB4 File Offset: 0x0018EDB4
		public static CallingConventionSig ReadSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, byte[] signature, GenericParamContext gpContext)
		{
			return SignatureReader.ReadSig(helper, corLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), gpContext);
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x0018EDC4 File Offset: 0x0018EDC4
		public static CallingConventionSig ReadSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, DataReader signature)
		{
			return SignatureReader.ReadSig(helper, corLibTypes, signature, default(GenericParamContext));
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0018EDE8 File Offset: 0x0018EDE8
		public static CallingConventionSig ReadSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, DataReader signature, GenericParamContext gpContext)
		{
			CallingConventionSig result;
			try
			{
				SignatureReader signatureReader = new SignatureReader(helper, corLibTypes, ref signature, gpContext);
				if (signatureReader.reader.Length == 0U)
				{
					result = null;
				}
				else
				{
					result = signatureReader.ReadSig();
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0018EE40 File Offset: 0x0018EE40
		public static TypeSig ReadTypeSig(ModuleDefMD readerModule, uint sig)
		{
			return SignatureReader.ReadTypeSig(readerModule, sig, default(GenericParamContext));
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0018EE64 File Offset: 0x0018EE64
		public static TypeSig ReadTypeSig(ModuleDefMD readerModule, uint sig, GenericParamContext gpContext)
		{
			TypeSig result;
			try
			{
				SignatureReader signatureReader = new SignatureReader(readerModule, sig, gpContext);
				result = signatureReader.ReadType();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0018EEA4 File Offset: 0x0018EEA4
		public static TypeSig ReadTypeSig(ModuleDefMD readerModule, uint sig, out byte[] extraData)
		{
			return SignatureReader.ReadTypeSig(readerModule, sig, default(GenericParamContext), out extraData);
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0018EEC8 File Offset: 0x0018EEC8
		public static TypeSig ReadTypeSig(ModuleDefMD readerModule, uint sig, GenericParamContext gpContext, out byte[] extraData)
		{
			TypeSig result;
			try
			{
				SignatureReader signatureReader = new SignatureReader(readerModule, sig, gpContext);
				TypeSig typeSig;
				try
				{
					typeSig = signatureReader.ReadType();
				}
				catch (IOException)
				{
					signatureReader.reader.Position = 0U;
					typeSig = null;
				}
				extraData = signatureReader.GetExtraData();
				result = typeSig;
			}
			catch
			{
				extraData = null;
				result = null;
			}
			return result;
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x0018EF3C File Offset: 0x0018EF3C
		public static TypeSig ReadTypeSig(ModuleDefMD module, byte[] signature)
		{
			return SignatureReader.ReadTypeSig(module, module.CorLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), default(GenericParamContext));
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x0018EF68 File Offset: 0x0018EF68
		public static TypeSig ReadTypeSig(ModuleDefMD module, byte[] signature, GenericParamContext gpContext)
		{
			return SignatureReader.ReadTypeSig(module, module.CorLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), gpContext);
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x0018EF80 File Offset: 0x0018EF80
		public static TypeSig ReadTypeSig(ModuleDefMD module, DataReader signature)
		{
			return SignatureReader.ReadTypeSig(module, module.CorLibTypes, signature, default(GenericParamContext));
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x0018EFA8 File Offset: 0x0018EFA8
		public static TypeSig ReadTypeSig(ModuleDefMD module, DataReader signature, GenericParamContext gpContext)
		{
			return SignatureReader.ReadTypeSig(module, module.CorLibTypes, signature, gpContext);
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0018EFB8 File Offset: 0x0018EFB8
		public static TypeSig ReadTypeSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, byte[] signature)
		{
			return SignatureReader.ReadTypeSig(helper, corLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), default(GenericParamContext));
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x0018EFE0 File Offset: 0x0018EFE0
		public static TypeSig ReadTypeSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, byte[] signature, GenericParamContext gpContext)
		{
			return SignatureReader.ReadTypeSig(helper, corLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), gpContext);
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x0018EFF0 File Offset: 0x0018EFF0
		public static TypeSig ReadTypeSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, DataReader signature)
		{
			return SignatureReader.ReadTypeSig(helper, corLibTypes, signature, default(GenericParamContext));
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x0018F014 File Offset: 0x0018F014
		public static TypeSig ReadTypeSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, DataReader signature, GenericParamContext gpContext)
		{
			byte[] array;
			return SignatureReader.ReadTypeSig(helper, corLibTypes, signature, gpContext, out array);
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0018F030 File Offset: 0x0018F030
		public static TypeSig ReadTypeSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, byte[] signature, GenericParamContext gpContext, out byte[] extraData)
		{
			return SignatureReader.ReadTypeSig(helper, corLibTypes, ByteArrayDataReaderFactory.CreateReader(signature), gpContext, out extraData);
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x0018F044 File Offset: 0x0018F044
		public static TypeSig ReadTypeSig(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, DataReader signature, GenericParamContext gpContext, out byte[] extraData)
		{
			TypeSig result;
			try
			{
				SignatureReader signatureReader = new SignatureReader(helper, corLibTypes, ref signature, gpContext);
				TypeSig typeSig;
				try
				{
					typeSig = signatureReader.ReadType();
				}
				catch (IOException)
				{
					signatureReader.reader.Position = 0U;
					typeSig = null;
				}
				extraData = signatureReader.GetExtraData();
				result = typeSig;
			}
			catch
			{
				extraData = null;
				result = null;
			}
			return result;
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x0018F0B8 File Offset: 0x0018F0B8
		private SignatureReader(ModuleDefMD readerModule, uint sig, GenericParamContext gpContext)
		{
			this.helper = readerModule;
			this.corLibTypes = readerModule.CorLibTypes;
			this.reader = readerModule.BlobStream.CreateReader(sig);
			this.gpContext = gpContext;
			this.recursionCounter = default(RecursionCounter);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x0018F0F4 File Offset: 0x0018F0F4
		private SignatureReader(ISignatureReaderHelper helper, ICorLibTypes corLibTypes, ref DataReader reader, GenericParamContext gpContext)
		{
			this.helper = helper;
			this.corLibTypes = corLibTypes;
			this.reader = reader;
			this.gpContext = gpContext;
			this.recursionCounter = default(RecursionCounter);
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x0018F124 File Offset: 0x0018F124
		private byte[] GetExtraData()
		{
			if (this.reader.Position == this.reader.Length)
			{
				return null;
			}
			return this.reader.ReadRemainingBytes();
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x0018F150 File Offset: 0x0018F150
		private CallingConventionSig ReadSig()
		{
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			CallingConvention callingConvention = (CallingConvention)this.reader.ReadByte();
			CallingConventionSig result;
			switch (callingConvention & CallingConvention.Mask)
			{
			case CallingConvention.Default:
			case CallingConvention.C:
			case CallingConvention.StdCall:
			case CallingConvention.ThisCall:
			case CallingConvention.FastCall:
			case CallingConvention.VarArg:
			case CallingConvention.NativeVarArg:
				result = this.ReadMethod(callingConvention);
				goto IL_A1;
			case CallingConvention.Field:
				result = this.ReadField(callingConvention);
				goto IL_A1;
			case CallingConvention.LocalSig:
				result = this.ReadLocalSig(callingConvention);
				goto IL_A1;
			case CallingConvention.Property:
				result = this.ReadProperty(callingConvention);
				goto IL_A1;
			case CallingConvention.GenericInst:
				result = this.ReadGenericInstMethod(callingConvention);
				goto IL_A1;
			}
			result = null;
			IL_A1:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x0018F210 File Offset: 0x0018F210
		private FieldSig ReadField(CallingConvention callingConvention)
		{
			return new FieldSig(callingConvention, this.ReadType());
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x0018F220 File Offset: 0x0018F220
		private MethodSig ReadMethod(CallingConvention callingConvention)
		{
			return this.ReadSig<MethodSig>(new MethodSig(callingConvention));
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0018F230 File Offset: 0x0018F230
		private PropertySig ReadProperty(CallingConvention callingConvention)
		{
			return this.ReadSig<PropertySig>(new PropertySig(callingConvention));
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0018F240 File Offset: 0x0018F240
		private T ReadSig<T>(T methodSig) where T : MethodBaseSig
		{
			if (methodSig.Generic)
			{
				uint genParamCount;
				if (!this.reader.TryReadCompressedUInt32(out genParamCount))
				{
					return default(T);
				}
				methodSig.GenParamCount = genParamCount;
			}
			uint num;
			if (!this.reader.TryReadCompressedUInt32(out num))
			{
				return default(T);
			}
			methodSig.RetType = this.ReadType();
			IList<TypeSig> list = methodSig.Params;
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				TypeSig typeSig = this.ReadType();
				if (typeSig is SentinelSig)
				{
					if (methodSig.ParamsAfterSentinel == null)
					{
						list = (methodSig.ParamsAfterSentinel = new List<TypeSig>((int)(num - num2)));
					}
					num2 -= 1U;
				}
				else
				{
					list.Add(typeSig);
				}
			}
			return methodSig;
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x0018F324 File Offset: 0x0018F324
		private LocalSig ReadLocalSig(CallingConvention callingConvention)
		{
			uint num;
			if (!this.reader.TryReadCompressedUInt32(out num))
			{
				return null;
			}
			LocalSig localSig = new LocalSig(callingConvention, num);
			IList<TypeSig> locals = localSig.Locals;
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				locals.Add(this.ReadType());
			}
			return localSig;
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x0018F374 File Offset: 0x0018F374
		private GenericInstMethodSig ReadGenericInstMethod(CallingConvention callingConvention)
		{
			uint num;
			if (!this.reader.TryReadCompressedUInt32(out num))
			{
				return null;
			}
			GenericInstMethodSig genericInstMethodSig = new GenericInstMethodSig(callingConvention, num);
			IList<TypeSig> genericArguments = genericInstMethodSig.GenericArguments;
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				genericArguments.Add(this.ReadType());
			}
			return genericInstMethodSig;
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x0018F3C4 File Offset: 0x0018F3C4
		private TypeSig ReadType()
		{
			if (!this.recursionCounter.Increment())
			{
				return null;
			}
			TypeSig result = null;
			switch (this.reader.ReadByte())
			{
			case 1:
				result = this.corLibTypes.Void;
				goto IL_54F;
			case 2:
				result = this.corLibTypes.Boolean;
				goto IL_54F;
			case 3:
				result = this.corLibTypes.Char;
				goto IL_54F;
			case 4:
				result = this.corLibTypes.SByte;
				goto IL_54F;
			case 5:
				result = this.corLibTypes.Byte;
				goto IL_54F;
			case 6:
				result = this.corLibTypes.Int16;
				goto IL_54F;
			case 7:
				result = this.corLibTypes.UInt16;
				goto IL_54F;
			case 8:
				result = this.corLibTypes.Int32;
				goto IL_54F;
			case 9:
				result = this.corLibTypes.UInt32;
				goto IL_54F;
			case 10:
				result = this.corLibTypes.Int64;
				goto IL_54F;
			case 11:
				result = this.corLibTypes.UInt64;
				goto IL_54F;
			case 12:
				result = this.corLibTypes.Single;
				goto IL_54F;
			case 13:
				result = this.corLibTypes.Double;
				goto IL_54F;
			case 14:
				result = this.corLibTypes.String;
				goto IL_54F;
			case 15:
				result = new PtrSig(this.ReadType());
				goto IL_54F;
			case 16:
				result = new ByRefSig(this.ReadType());
				goto IL_54F;
			case 17:
				result = new ValueTypeSig(this.ReadTypeDefOrRef(false));
				goto IL_54F;
			case 18:
				result = new ClassSig(this.ReadTypeDefOrRef(false));
				goto IL_54F;
			case 19:
			{
				uint num;
				if (this.reader.TryReadCompressedUInt32(out num))
				{
					result = new GenericVar(num, this.gpContext.Type);
					goto IL_54F;
				}
				goto IL_54F;
			}
			case 20:
			{
				TypeSig typeSig = this.ReadType();
				uint num2;
				if (!this.reader.TryReadCompressedUInt32(out num2) || num2 > 64U)
				{
					goto IL_54F;
				}
				if (num2 == 0U)
				{
					result = new ArraySig(typeSig, num2);
					goto IL_54F;
				}
				uint num;
				if (!this.reader.TryReadCompressedUInt32(out num) || num > 64U)
				{
					goto IL_54F;
				}
				List<uint> list = new List<uint>((int)num);
				for (uint num3 = 0U; num3 < num; num3 += 1U)
				{
					uint item;
					if (!this.reader.TryReadCompressedUInt32(out item))
					{
						goto IL_54F;
					}
					list.Add(item);
				}
				if (this.reader.TryReadCompressedUInt32(out num) && num <= 64U)
				{
					List<int> list2 = new List<int>((int)num);
					for (uint num3 = 0U; num3 < num; num3 += 1U)
					{
						int item2;
						if (!this.reader.TryReadCompressedInt32(out item2))
						{
							goto IL_54F;
						}
						list2.Add(item2);
					}
					result = new ArraySig(typeSig, num2, list, list2);
					goto IL_54F;
				}
				goto IL_54F;
			}
			case 21:
			{
				TypeSig typeSig = this.ReadType();
				uint num;
				if (this.reader.TryReadCompressedUInt32(out num))
				{
					GenericInstSig genericInstSig = new GenericInstSig(typeSig as ClassOrValueTypeSig, num);
					IList<TypeSig> genericArguments = genericInstSig.GenericArguments;
					for (uint num3 = 0U; num3 < num; num3 += 1U)
					{
						genericArguments.Add(this.ReadType());
					}
					result = genericInstSig;
					goto IL_54F;
				}
				goto IL_54F;
			}
			case 22:
				result = this.corLibTypes.TypedReference;
				goto IL_54F;
			case 23:
			{
				TypeSig typeSig = this.ReadType();
				uint num;
				if (this.reader.TryReadCompressedUInt32(out num))
				{
					result = new ValueArraySig(typeSig, num);
					goto IL_54F;
				}
				goto IL_54F;
			}
			case 24:
				result = this.corLibTypes.IntPtr;
				goto IL_54F;
			case 25:
				result = this.corLibTypes.UIntPtr;
				goto IL_54F;
			case 27:
				result = new FnPtrSig(this.ReadSig());
				goto IL_54F;
			case 28:
				result = this.corLibTypes.Object;
				goto IL_54F;
			case 29:
				result = new SZArraySig(this.ReadType());
				goto IL_54F;
			case 30:
			{
				uint num;
				if (this.reader.TryReadCompressedUInt32(out num))
				{
					result = new GenericMVar(num, this.gpContext.Method);
					goto IL_54F;
				}
				goto IL_54F;
			}
			case 31:
				result = new CModReqdSig(this.ReadTypeDefOrRef(true), this.ReadType());
				goto IL_54F;
			case 32:
				result = new CModOptSig(this.ReadTypeDefOrRef(true), this.ReadType());
				goto IL_54F;
			case 33:
			{
				IntPtr address;
				if (IntPtr.Size == 4)
				{
					address = new IntPtr(this.reader.ReadInt32());
				}
				else
				{
					address = new IntPtr(this.reader.ReadInt64());
				}
				result = this.helper.ConvertRTInternalAddress(address);
				goto IL_54F;
			}
			case 63:
			{
				uint num;
				if (this.reader.TryReadCompressedUInt32(out num))
				{
					result = new ModuleSig(num, this.ReadType());
					goto IL_54F;
				}
				goto IL_54F;
			}
			case 65:
				result = new SentinelSig();
				goto IL_54F;
			case 69:
				result = new PinnedSig(this.ReadType());
				goto IL_54F;
			}
			result = null;
			IL_54F:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0018F930 File Offset: 0x0018F930
		private ITypeDefOrRef ReadTypeDefOrRef(bool allowTypeSpec)
		{
			uint codedToken;
			if (!this.reader.TryReadCompressedUInt32(out codedToken))
			{
				return null;
			}
			if (!allowTypeSpec && CodedToken.TypeDefOrRef.Decode2(codedToken).Table == Table.TypeSpec)
			{
				return null;
			}
			return this.helper.ResolveTypeDefOrRef(codedToken, default(GenericParamContext));
		}

		// Token: 0x0400274A RID: 10058
		private const uint MaxArrayRank = 64U;

		// Token: 0x0400274B RID: 10059
		private readonly ISignatureReaderHelper helper;

		// Token: 0x0400274C RID: 10060
		private readonly ICorLibTypes corLibTypes;

		// Token: 0x0400274D RID: 10061
		private DataReader reader;

		// Token: 0x0400274E RID: 10062
		private readonly GenericParamContext gpContext;

		// Token: 0x0400274F RID: 10063
		private RecursionCounter recursionCounter;
	}
}
