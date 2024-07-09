using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009DB RID: 2523
	[ComVisible(true)]
	public class DynamicMethodBodyReader : MethodBodyReaderBase, ISignatureReaderHelper
	{
		// Token: 0x0600609F RID: 24735 RVA: 0x001CC900 File Offset: 0x001CC900
		public DynamicMethodBodyReader(ModuleDef module, object obj) : this(module, obj, default(GenericParamContext))
		{
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x001CC924 File Offset: 0x001CC924
		public DynamicMethodBodyReader(ModuleDef module, object obj, GenericParamContext gpContext) : this(module, obj, new Importer(module, ImporterOptions.TryToUseDefs, gpContext), DynamicMethodBodyReaderOptions.None)
		{
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x001CC938 File Offset: 0x001CC938
		public DynamicMethodBodyReader(ModuleDef module, object obj, Importer importer) : this(module, obj, importer, DynamicMethodBodyReaderOptions.None)
		{
		}

		// Token: 0x060060A2 RID: 24738 RVA: 0x001CC944 File Offset: 0x001CC944
		public DynamicMethodBodyReader(ModuleDef module, object obj, Importer importer, DynamicMethodBodyReaderOptions options)
		{
			this.module = module;
			this.importer = importer;
			this.options = options;
			this.gpContext = importer.gpContext;
			this.methodName = null;
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Delegate @delegate = obj as Delegate;
			if (@delegate != null)
			{
				obj = @delegate.Method;
				if (obj == null)
				{
					throw new Exception("Delegate.Method is null");
				}
			}
			if (obj.GetType().ToString() == "System.Reflection.Emit.DynamicMethod+RTDynamicMethod")
			{
				obj = (DynamicMethodBodyReader.rtdmOwnerFieldInfo.Read(obj) as DynamicMethod);
				if (obj == null)
				{
					throw new Exception("RTDynamicMethod.m_owner is null or invalid");
				}
			}
			if (obj is DynamicMethod)
			{
				this.methodName = ((DynamicMethod)obj).Name;
				obj = DynamicMethodBodyReader.dmResolverFieldInfo.Read(obj);
				if (obj == null)
				{
					throw new Exception("No resolver found");
				}
			}
			if (obj.GetType().ToString() != "System.Reflection.Emit.DynamicResolver")
			{
				throw new Exception("Couldn't find DynamicResolver");
			}
			byte[] array = DynamicMethodBodyReader.rslvCodeFieldInfo.Read(obj) as byte[];
			if (array == null)
			{
				throw new Exception("No code");
			}
			this.codeSize = array.Length;
			MethodBase methodBase = DynamicMethodBodyReader.rslvMethodFieldInfo.Read(obj) as MethodBase;
			if (methodBase == null)
			{
				throw new Exception("No method");
			}
			this.maxStack = (int)DynamicMethodBodyReader.rslvMaxStackFieldInfo.Read(obj);
			object obj2 = DynamicMethodBodyReader.rslvDynamicScopeFieldInfo.Read(obj);
			if (obj2 == null)
			{
				throw new Exception("No scope");
			}
			IList list = DynamicMethodBodyReader.scopeTokensFieldInfo.Read(obj2) as IList;
			if (list == null)
			{
				throw new Exception("No tokens");
			}
			this.tokens = new List<object>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				this.tokens.Add(list[i]);
			}
			this.ehInfos = (IList<object>)DynamicMethodBodyReader.rslvExceptionsFieldInfo.Read(obj);
			this.ehHeader = (DynamicMethodBodyReader.rslvExceptionHeaderFieldInfo.Read(obj) as byte[]);
			this.UpdateLocals(DynamicMethodBodyReader.rslvLocalsFieldInfo.Read(obj) as byte[]);
			this.reader = ByteArrayDataReaderFactory.CreateReader(array);
			this.method = this.CreateMethodDef(methodBase);
			this.parameters = this.method.Parameters;
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x001CCBA8 File Offset: 0x001CCBA8
		private static List<DynamicMethodBodyReader.ExceptionInfo> CreateExceptionInfos(IList<object> ehInfos)
		{
			if (ehInfos == null)
			{
				return new List<DynamicMethodBodyReader.ExceptionInfo>();
			}
			List<DynamicMethodBodyReader.ExceptionInfo> list = new List<DynamicMethodBodyReader.ExceptionInfo>(ehInfos.Count);
			int count = ehInfos.Count;
			for (int i = 0; i < count; i++)
			{
				object instance = ehInfos[i];
				DynamicMethodBodyReader.ExceptionInfo item = new DynamicMethodBodyReader.ExceptionInfo
				{
					CatchAddr = (int[])DynamicMethodBodyReader.ehCatchAddrFieldInfo.Read(instance),
					CatchClass = (Type[])DynamicMethodBodyReader.ehCatchClassFieldInfo.Read(instance),
					CatchEndAddr = (int[])DynamicMethodBodyReader.ehCatchEndAddrFieldInfo.Read(instance),
					CurrentCatch = (int)DynamicMethodBodyReader.ehCurrentCatchFieldInfo.Read(instance),
					Type = (int[])DynamicMethodBodyReader.ehTypeFieldInfo.Read(instance),
					StartAddr = (int)DynamicMethodBodyReader.ehStartAddrFieldInfo.Read(instance),
					EndAddr = (int)DynamicMethodBodyReader.ehEndAddrFieldInfo.Read(instance),
					EndFinally = (int)DynamicMethodBodyReader.ehEndFinallyFieldInfo.Read(instance)
				};
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x001CCCB4 File Offset: 0x001CCCB4
		private void UpdateLocals(byte[] localsSig)
		{
			if (localsSig == null || localsSig.Length == 0)
			{
				return;
			}
			LocalSig localSig = SignatureReader.ReadSig(this, this.module.CorLibTypes, localsSig, this.gpContext) as LocalSig;
			if (localSig == null)
			{
				return;
			}
			IList<TypeSig> locals = localSig.Locals;
			int count = locals.Count;
			for (int i = 0; i < count; i++)
			{
				this.locals.Add(new Local(locals[i]));
			}
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x001CCD2C File Offset: 0x001CCD2C
		private MethodDef CreateMethodDef(MethodBase delMethod)
		{
			int num = 1;
			MethodDefUser methodDefUser = new MethodDefUser();
			TypeSig returnType = this.GetReturnType(delMethod);
			List<TypeSig> parameters = this.GetParameters(delMethod);
			if (num != 0)
			{
				methodDefUser.Signature = MethodSig.CreateStatic(returnType, parameters.ToArray());
			}
			else
			{
				methodDefUser.Signature = MethodSig.CreateInstance(returnType, parameters.ToArray());
			}
			methodDefUser.Parameters.UpdateParameterTypes();
			methodDefUser.ImplAttributes = MethodImplAttributes.IL;
			methodDefUser.Attributes = MethodAttributes.PrivateScope;
			if (num != 0)
			{
				methodDefUser.Attributes |= MethodAttributes.Static;
			}
			return this.module.UpdateRowId<MethodDefUser>(methodDefUser);
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x001CCDBC File Offset: 0x001CCDBC
		private TypeSig GetReturnType(MethodBase mb)
		{
			MethodInfo methodInfo = mb as MethodInfo;
			if (methodInfo != null)
			{
				return this.importer.ImportAsTypeSig(methodInfo.ReturnType);
			}
			return this.module.CorLibTypes.Void;
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x001CCE00 File Offset: 0x001CCE00
		private List<TypeSig> GetParameters(MethodBase delMethod)
		{
			List<TypeSig> list = new List<TypeSig>();
			foreach (ParameterInfo parameterInfo in delMethod.GetParameters())
			{
				list.Add(this.importer.ImportAsTypeSig(parameterInfo.ParameterType));
			}
			return list;
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x001CCE54 File Offset: 0x001CCE54
		public bool Read()
		{
			base.ReadInstructionsNumBytes((uint)this.codeSize);
			this.CreateExceptionHandlers();
			return true;
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x001CCE6C File Offset: 0x001CCE6C
		private void CreateExceptionHandlers()
		{
			if (this.ehHeader != null)
			{
				if (this.ehHeader.Length < 4)
				{
					return;
				}
				BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.ehHeader));
				if ((binaryReader.ReadByte() & 64) == 0)
				{
					int num = (int)((ushort)((binaryReader.ReadByte() - 2) / 12));
					binaryReader.ReadUInt16();
					for (int i = 0; i < num; i++)
					{
						if (binaryReader.BaseStream.Position + 12L > binaryReader.BaseStream.Length)
						{
							return;
						}
						ExceptionHandler exceptionHandler = new ExceptionHandler();
						exceptionHandler.HandlerType = (ExceptionHandlerType)binaryReader.ReadUInt16();
						int num2 = (int)binaryReader.ReadUInt16();
						exceptionHandler.TryStart = base.GetInstructionThrow((uint)num2);
						exceptionHandler.TryEnd = base.GetInstruction((uint)((int)binaryReader.ReadByte() + num2));
						num2 = (int)binaryReader.ReadUInt16();
						exceptionHandler.HandlerStart = base.GetInstructionThrow((uint)num2);
						exceptionHandler.HandlerEnd = base.GetInstruction((uint)((int)binaryReader.ReadByte() + num2));
						if (exceptionHandler.HandlerType == ExceptionHandlerType.Catch)
						{
							exceptionHandler.CatchType = (this.ReadToken(binaryReader.ReadUInt32()) as ITypeDefOrRef);
						}
						else if (exceptionHandler.HandlerType == ExceptionHandlerType.Filter)
						{
							exceptionHandler.FilterStart = base.GetInstruction(binaryReader.ReadUInt32());
						}
						else
						{
							binaryReader.ReadUInt32();
						}
						this.exceptionHandlers.Add(exceptionHandler);
					}
					return;
				}
				Stream baseStream = binaryReader.BaseStream;
				long position = baseStream.Position;
				baseStream.Position = position - 1L;
				int num3 = (int)((ushort)(((binaryReader.ReadUInt32() >> 8) - 4U) / 24U));
				for (int j = 0; j < num3; j++)
				{
					if (binaryReader.BaseStream.Position + 24L > binaryReader.BaseStream.Length)
					{
						return;
					}
					ExceptionHandler exceptionHandler2 = new ExceptionHandler();
					exceptionHandler2.HandlerType = (ExceptionHandlerType)binaryReader.ReadUInt32();
					uint num4 = binaryReader.ReadUInt32();
					exceptionHandler2.TryStart = base.GetInstructionThrow(num4);
					exceptionHandler2.TryEnd = base.GetInstruction(binaryReader.ReadUInt32() + num4);
					num4 = binaryReader.ReadUInt32();
					exceptionHandler2.HandlerStart = base.GetInstructionThrow(num4);
					exceptionHandler2.HandlerEnd = base.GetInstruction(binaryReader.ReadUInt32() + num4);
					if (exceptionHandler2.HandlerType == ExceptionHandlerType.Catch)
					{
						exceptionHandler2.CatchType = (this.ReadToken(binaryReader.ReadUInt32()) as ITypeDefOrRef);
					}
					else if (exceptionHandler2.HandlerType == ExceptionHandlerType.Filter)
					{
						exceptionHandler2.FilterStart = base.GetInstruction(binaryReader.ReadUInt32());
					}
					else
					{
						binaryReader.ReadUInt32();
					}
					this.exceptionHandlers.Add(exceptionHandler2);
				}
				return;
			}
			else if (this.ehInfos != null)
			{
				foreach (DynamicMethodBodyReader.ExceptionInfo exceptionInfo in DynamicMethodBodyReader.CreateExceptionInfos(this.ehInfos))
				{
					Instruction instructionThrow = base.GetInstructionThrow((uint)exceptionInfo.StartAddr);
					Instruction instruction = base.GetInstruction((uint)exceptionInfo.EndAddr);
					Instruction instruction2 = (exceptionInfo.EndFinally < 0) ? null : base.GetInstruction((uint)exceptionInfo.EndFinally);
					for (int k = 0; k < exceptionInfo.CurrentCatch; k++)
					{
						ExceptionHandler exceptionHandler3 = new ExceptionHandler();
						exceptionHandler3.HandlerType = (ExceptionHandlerType)exceptionInfo.Type[k];
						exceptionHandler3.TryStart = instructionThrow;
						exceptionHandler3.TryEnd = ((exceptionHandler3.HandlerType == ExceptionHandlerType.Finally) ? instruction2 : instruction);
						exceptionHandler3.FilterStart = null;
						exceptionHandler3.HandlerStart = base.GetInstructionThrow((uint)exceptionInfo.CatchAddr[k]);
						exceptionHandler3.HandlerEnd = base.GetInstruction((uint)exceptionInfo.CatchEndAddr[k]);
						exceptionHandler3.CatchType = this.importer.Import(exceptionInfo.CatchClass[k]);
						this.exceptionHandlers.Add(exceptionHandler3);
					}
				}
			}
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x001CD254 File Offset: 0x001CD254
		public MethodDef GetMethod()
		{
			CilBody cilBody = new CilBody(true, this.instructions, this.exceptionHandlers, this.locals);
			cilBody.MaxStack = (ushort)Math.Min(this.maxStack, 65535);
			this.instructions = null;
			this.exceptionHandlers = null;
			this.locals = null;
			this.method.Body = cilBody;
			this.method.Name = this.methodName;
			return this.method;
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x001CD2D4 File Offset: 0x001CD2D4
		protected override IField ReadInlineField(Instruction instr)
		{
			return this.ReadToken(this.reader.ReadUInt32()) as IField;
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x001CD2EC File Offset: 0x001CD2EC
		protected override IMethod ReadInlineMethod(Instruction instr)
		{
			return this.ReadToken(this.reader.ReadUInt32()) as IMethod;
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x001CD304 File Offset: 0x001CD304
		protected override MethodSig ReadInlineSig(Instruction instr)
		{
			return this.ReadToken(this.reader.ReadUInt32()) as MethodSig;
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x001CD31C File Offset: 0x001CD31C
		protected override string ReadInlineString(Instruction instr)
		{
			return (this.ReadToken(this.reader.ReadUInt32()) as string) ?? string.Empty;
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x001CD340 File Offset: 0x001CD340
		protected override ITokenOperand ReadInlineTok(Instruction instr)
		{
			return this.ReadToken(this.reader.ReadUInt32()) as ITokenOperand;
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x001CD358 File Offset: 0x001CD358
		protected override ITypeDefOrRef ReadInlineType(Instruction instr)
		{
			return this.ReadToken(this.reader.ReadUInt32()) as ITypeDefOrRef;
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x001CD370 File Offset: 0x001CD370
		private object ReadToken(uint token)
		{
			uint num = token & 16777215U;
			uint num2 = token >> 24;
			if (num2 <= 10U)
			{
				switch (num2)
				{
				case 2U:
					return this.ImportType(num);
				case 3U:
				case 5U:
					goto IL_80;
				case 4U:
					return this.ImportField(num);
				case 6U:
					break;
				default:
					if (num2 != 10U)
					{
						goto IL_80;
					}
					break;
				}
				return this.ImportMethod(num);
			}
			if (num2 == 17U)
			{
				return this.ImportSignature(num);
			}
			if (num2 == 112U)
			{
				return this.Resolve(num) as string;
			}
			IL_80:
			return null;
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x001CD404 File Offset: 0x001CD404
		private IMethod ImportMethod(uint rid)
		{
			object obj = this.Resolve(rid);
			if (obj == null)
			{
				return null;
			}
			if (obj is RuntimeMethodHandle)
			{
				if ((this.options & DynamicMethodBodyReaderOptions.UnknownDeclaringType) != DynamicMethodBodyReaderOptions.None)
				{
					return this.importer.Import(MethodBase.GetMethodFromHandle((RuntimeMethodHandle)obj, default(RuntimeTypeHandle)));
				}
				return this.importer.Import(MethodBase.GetMethodFromHandle((RuntimeMethodHandle)obj));
			}
			else
			{
				if (obj.GetType().ToString() == "System.Reflection.Emit.GenericMethodInfo")
				{
					RuntimeTypeHandle declaringType = (RuntimeTypeHandle)DynamicMethodBodyReader.gmiContextFieldInfo.Read(obj);
					MethodBase methodFromHandle = MethodBase.GetMethodFromHandle((RuntimeMethodHandle)DynamicMethodBodyReader.gmiMethodHandleFieldInfo.Read(obj), declaringType);
					return this.importer.Import(methodFromHandle);
				}
				if (obj.GetType().ToString() == "System.Reflection.Emit.VarArgMethod")
				{
					MethodInfo varArgMethod = this.GetVarArgMethod(obj);
					if (!(varArgMethod is DynamicMethod))
					{
						return this.importer.Import(varArgMethod);
					}
					obj = varArgMethod;
				}
				if (obj is DynamicMethod)
				{
					throw new Exception("DynamicMethod calls another DynamicMethod");
				}
				return null;
			}
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x001CD528 File Offset: 0x001CD528
		private MethodInfo GetVarArgMethod(object obj)
		{
			if (DynamicMethodBodyReader.vamDynamicMethodFieldInfo.Exists(obj))
			{
				MethodInfo methodInfo = DynamicMethodBodyReader.vamMethodFieldInfo.Read(obj) as MethodInfo;
				return (DynamicMethodBodyReader.vamDynamicMethodFieldInfo.Read(obj) as DynamicMethod) ?? methodInfo;
			}
			return DynamicMethodBodyReader.vamMethodFieldInfo.Read(obj) as MethodInfo;
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x001CD584 File Offset: 0x001CD584
		private IField ImportField(uint rid)
		{
			object obj = this.Resolve(rid);
			if (obj == null)
			{
				return null;
			}
			if (obj is RuntimeFieldHandle)
			{
				if ((this.options & DynamicMethodBodyReaderOptions.UnknownDeclaringType) != DynamicMethodBodyReaderOptions.None)
				{
					return this.importer.Import(FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)obj, default(RuntimeTypeHandle)));
				}
				return this.importer.Import(FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)obj));
			}
			else
			{
				if (obj.GetType().ToString() == "System.Reflection.Emit.GenericFieldInfo")
				{
					RuntimeTypeHandle declaringType = (RuntimeTypeHandle)DynamicMethodBodyReader.gfiContextFieldInfo.Read(obj);
					FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)DynamicMethodBodyReader.gfiFieldHandleFieldInfo.Read(obj), declaringType);
					return this.importer.Import(fieldFromHandle);
				}
				return null;
			}
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x001CD650 File Offset: 0x001CD650
		private ITypeDefOrRef ImportType(uint rid)
		{
			object obj = this.Resolve(rid);
			if (obj is RuntimeTypeHandle)
			{
				return this.importer.Import(Type.GetTypeFromHandle((RuntimeTypeHandle)obj));
			}
			return null;
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x001CD690 File Offset: 0x001CD690
		private CallingConventionSig ImportSignature(uint rid)
		{
			byte[] array = this.Resolve(rid) as byte[];
			if (array == null)
			{
				return null;
			}
			return SignatureReader.ReadSig(this, this.module.CorLibTypes, array, this.gpContext);
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x001CD6D0 File Offset: 0x001CD6D0
		private object Resolve(uint index)
		{
			if (index >= (uint)this.tokens.Count)
			{
				return null;
			}
			return this.tokens[(int)index];
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x001CD6F4 File Offset: 0x001CD6F4
		ITypeDefOrRef ISignatureReaderHelper.ResolveTypeDefOrRef(uint codedToken, GenericParamContext gpContext)
		{
			uint token;
			if (!CodedToken.TypeDefOrRef.Decode(codedToken, out token))
			{
				return null;
			}
			Table table = MDToken.ToTable(token);
			if (table - Table.TypeRef <= 1 || table == Table.TypeSpec)
			{
				return this.module.ResolveToken(token) as ITypeDefOrRef;
			}
			return null;
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x001CD744 File Offset: 0x001CD744
		TypeSig ISignatureReaderHelper.ConvertRTInternalAddress(IntPtr address)
		{
			return this.importer.ImportAsTypeSig(MethodTableToTypeConverter.Convert(address));
		}

		// Token: 0x04003062 RID: 12386
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo rtdmOwnerFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_owner");

		// Token: 0x04003063 RID: 12387
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo dmResolverFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_resolver");

		// Token: 0x04003064 RID: 12388
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo rslvCodeFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_code");

		// Token: 0x04003065 RID: 12389
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo rslvDynamicScopeFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_scope");

		// Token: 0x04003066 RID: 12390
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo rslvMethodFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_method");

		// Token: 0x04003067 RID: 12391
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo rslvLocalsFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_localSignature");

		// Token: 0x04003068 RID: 12392
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo rslvMaxStackFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_stackSize");

		// Token: 0x04003069 RID: 12393
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo rslvExceptionsFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_exceptions");

		// Token: 0x0400306A RID: 12394
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo rslvExceptionHeaderFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_exceptionHeader");

		// Token: 0x0400306B RID: 12395
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo scopeTokensFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_tokens");

		// Token: 0x0400306C RID: 12396
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo gfiFieldHandleFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_field", "m_fieldHandle");

		// Token: 0x0400306D RID: 12397
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo gfiContextFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_context");

		// Token: 0x0400306E RID: 12398
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo gmiMethodHandleFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_method", "m_methodHandle");

		// Token: 0x0400306F RID: 12399
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo gmiContextFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_context");

		// Token: 0x04003070 RID: 12400
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo ehCatchAddrFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_catchAddr");

		// Token: 0x04003071 RID: 12401
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo ehCatchClassFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_catchClass");

		// Token: 0x04003072 RID: 12402
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo ehCatchEndAddrFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_catchEndAddr");

		// Token: 0x04003073 RID: 12403
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo ehCurrentCatchFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_currentCatch");

		// Token: 0x04003074 RID: 12404
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo ehTypeFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_type");

		// Token: 0x04003075 RID: 12405
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo ehStartAddrFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_startAddr");

		// Token: 0x04003076 RID: 12406
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo ehEndAddrFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_endAddr");

		// Token: 0x04003077 RID: 12407
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo ehEndFinallyFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_endFinally");

		// Token: 0x04003078 RID: 12408
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo vamMethodFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_method");

		// Token: 0x04003079 RID: 12409
		private static readonly DynamicMethodBodyReader.ReflectionFieldInfo vamDynamicMethodFieldInfo = new DynamicMethodBodyReader.ReflectionFieldInfo("m_dynamicMethod");

		// Token: 0x0400307A RID: 12410
		private readonly ModuleDef module;

		// Token: 0x0400307B RID: 12411
		private readonly Importer importer;

		// Token: 0x0400307C RID: 12412
		private readonly GenericParamContext gpContext;

		// Token: 0x0400307D RID: 12413
		private readonly MethodDef method;

		// Token: 0x0400307E RID: 12414
		private readonly int codeSize;

		// Token: 0x0400307F RID: 12415
		private readonly int maxStack;

		// Token: 0x04003080 RID: 12416
		private readonly List<object> tokens;

		// Token: 0x04003081 RID: 12417
		private readonly IList<object> ehInfos;

		// Token: 0x04003082 RID: 12418
		private readonly byte[] ehHeader;

		// Token: 0x04003083 RID: 12419
		private readonly string methodName;

		// Token: 0x04003084 RID: 12420
		private readonly DynamicMethodBodyReaderOptions options;

		// Token: 0x02001041 RID: 4161
		private class ReflectionFieldInfo
		{
			// Token: 0x06008FCE RID: 36814 RVA: 0x002AD768 File Offset: 0x002AD768
			public ReflectionFieldInfo(string fieldName)
			{
				this.fieldName1 = fieldName;
			}

			// Token: 0x06008FCF RID: 36815 RVA: 0x002AD778 File Offset: 0x002AD778
			public ReflectionFieldInfo(string fieldName1, string fieldName2)
			{
				this.fieldName1 = fieldName1;
				this.fieldName2 = fieldName2;
			}

			// Token: 0x06008FD0 RID: 36816 RVA: 0x002AD790 File Offset: 0x002AD790
			public object Read(object instance)
			{
				if (this.fieldInfo == null)
				{
					this.InitializeField(instance.GetType());
				}
				if (this.fieldInfo == null)
				{
					throw new Exception(string.Concat(new string[]
					{
						"Couldn't find field '",
						this.fieldName1,
						"' or '",
						this.fieldName2,
						"'"
					}));
				}
				return this.fieldInfo.GetValue(instance);
			}

			// Token: 0x06008FD1 RID: 36817 RVA: 0x002AD80C File Offset: 0x002AD80C
			public bool Exists(object instance)
			{
				this.InitializeField(instance.GetType());
				return this.fieldInfo != null;
			}

			// Token: 0x06008FD2 RID: 36818 RVA: 0x002AD828 File Offset: 0x002AD828
			private void InitializeField(Type type)
			{
				if (this.fieldInfo != null)
				{
					return;
				}
				BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
				this.fieldInfo = type.GetField(this.fieldName1, bindingAttr);
				if (this.fieldInfo == null && this.fieldName2 != null)
				{
					this.fieldInfo = type.GetField(this.fieldName2, bindingAttr);
				}
			}

			// Token: 0x04004535 RID: 17717
			private FieldInfo fieldInfo;

			// Token: 0x04004536 RID: 17718
			private readonly string fieldName1;

			// Token: 0x04004537 RID: 17719
			private readonly string fieldName2;
		}

		// Token: 0x02001042 RID: 4162
		private class ExceptionInfo
		{
			// Token: 0x04004538 RID: 17720
			public int[] CatchAddr;

			// Token: 0x04004539 RID: 17721
			public Type[] CatchClass;

			// Token: 0x0400453A RID: 17722
			public int[] CatchEndAddr;

			// Token: 0x0400453B RID: 17723
			public int CurrentCatch;

			// Token: 0x0400453C RID: 17724
			public int[] Type;

			// Token: 0x0400453D RID: 17725
			public int StartAddr;

			// Token: 0x0400453E RID: 17726
			public int EndAddr;

			// Token: 0x0400453F RID: 17727
			public int EndFinally;
		}
	}
}
