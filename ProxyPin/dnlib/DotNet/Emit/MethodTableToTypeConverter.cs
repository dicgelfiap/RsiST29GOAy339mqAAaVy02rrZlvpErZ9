using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009EB RID: 2539
	internal static class MethodTableToTypeConverter
	{
		// Token: 0x0600618E RID: 24974 RVA: 0x001D0380 File Offset: 0x001D0380
		static MethodTableToTypeConverter()
		{
			if (MethodTableToTypeConverter.ptrFieldInfo == null)
			{
				MethodTableToTypeConverter.moduleBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("DynAsm"), AssemblyBuilderAccess.Run).DefineDynamicModule("DynMod");
			}
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x001D0478 File Offset: 0x001D0478
		public static Type Convert(IntPtr address)
		{
			object obj = MethodTableToTypeConverter.lockObj;
			Type result;
			lock (obj)
			{
				Type type;
				if (MethodTableToTypeConverter.addrToType.TryGetValue(address, out type))
				{
					result = type;
				}
				else
				{
					type = (MethodTableToTypeConverter.GetTypeNET20(address) ?? MethodTableToTypeConverter.GetTypeUsingTypeBuilder(address));
					MethodTableToTypeConverter.addrToType[address] = type;
					result = type;
				}
			}
			return result;
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x001D04F4 File Offset: 0x001D04F4
		private static Type GetTypeUsingTypeBuilder(IntPtr address)
		{
			if (MethodTableToTypeConverter.moduleBuilder == null)
			{
				return null;
			}
			TypeBuilder typeBuilder = MethodTableToTypeConverter.moduleBuilder.DefineType(MethodTableToTypeConverter.GetNextTypeName());
			MethodBuilder mb = typeBuilder.DefineMethod("m", MethodAttributes.Static, typeof(void), Array2.Empty<Type>());
			Type result;
			try
			{
				if (MethodTableToTypeConverter.setMethodBodyMethodInfo != null)
				{
					result = MethodTableToTypeConverter.GetTypeNET45(typeBuilder, mb, address);
				}
				else
				{
					result = MethodTableToTypeConverter.GetTypeNET40(typeBuilder, mb, address);
				}
			}
			catch
			{
				MethodTableToTypeConverter.moduleBuilder = null;
				result = null;
			}
			return result;
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x001D0580 File Offset: 0x001D0580
		private static Type GetTypeNET45(TypeBuilder tb, MethodBuilder mb, IntPtr address)
		{
			byte[] array = new byte[]
			{
				42
			};
			int num = 8;
			byte[] localSignature = MethodTableToTypeConverter.GetLocalSignature(address);
			MethodBase methodBase = MethodTableToTypeConverter.setMethodBodyMethodInfo;
			object[] array2 = new object[5];
			array2[0] = array;
			array2[1] = num;
			array2[2] = localSignature;
			methodBase.Invoke(mb, array2);
			return tb.CreateType().GetMethod("m", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).GetMethodBody().LocalVariables[0].LocalType;
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x001D05F0 File Offset: 0x001D05F0
		private static Type GetTypeNET40(TypeBuilder tb, MethodBuilder mb, IntPtr address)
		{
			ILGenerator ilgenerator = mb.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ret);
			ilgenerator.DeclareLocal(typeof(int));
			byte[] localSignature = MethodTableToTypeConverter.GetLocalSignature(address);
			SignatureHelper obj = (SignatureHelper)MethodTableToTypeConverter.localSignatureFieldInfo.GetValue(ilgenerator);
			MethodTableToTypeConverter.sigDoneFieldInfo.SetValue(obj, true);
			MethodTableToTypeConverter.currSigFieldInfo.SetValue(obj, localSignature.Length);
			MethodTableToTypeConverter.signatureFieldInfo.SetValue(obj, localSignature);
			return tb.CreateType().GetMethod("m", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).GetMethodBody().LocalVariables[0].LocalType;
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x001D0694 File Offset: 0x001D0694
		private static Type GetTypeNET20(IntPtr address)
		{
			if (MethodTableToTypeConverter.ptrFieldInfo == null)
			{
				return null;
			}
			object obj = default(RuntimeTypeHandle);
			MethodTableToTypeConverter.ptrFieldInfo.SetValue(obj, address);
			return Type.GetTypeFromHandle((RuntimeTypeHandle)obj);
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x001D06DC File Offset: 0x001D06DC
		private static string GetNextTypeName()
		{
			return "Type" + MethodTableToTypeConverter.numNewTypes++.ToString();
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x001D070C File Offset: 0x001D070C
		private static byte[] GetLocalSignature(IntPtr mtAddr)
		{
			ulong num = (ulong)mtAddr.ToInt64();
			if (IntPtr.Size == 4)
			{
				return new byte[]
				{
					7,
					1,
					33,
					0,
					0,
					0,
					0
				};
			}
			return new byte[]
			{
				7,
				1,
				33,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
		}

		// Token: 0x040030C4 RID: 12484
		private const string METHOD_NAME = "m";

		// Token: 0x040030C5 RID: 12485
		private static readonly MethodInfo setMethodBodyMethodInfo = typeof(MethodBuilder).GetMethod("SetMethodBody", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040030C6 RID: 12486
		private static readonly FieldInfo localSignatureFieldInfo = typeof(ILGenerator).GetField("m_localSignature", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040030C7 RID: 12487
		private static readonly FieldInfo sigDoneFieldInfo = typeof(SignatureHelper).GetField("m_sigDone", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040030C8 RID: 12488
		private static readonly FieldInfo currSigFieldInfo = typeof(SignatureHelper).GetField("m_currSig", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040030C9 RID: 12489
		private static readonly FieldInfo signatureFieldInfo = typeof(SignatureHelper).GetField("m_signature", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040030CA RID: 12490
		private static readonly FieldInfo ptrFieldInfo = typeof(RuntimeTypeHandle).GetField("m_ptr", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040030CB RID: 12491
		private static readonly Dictionary<IntPtr, Type> addrToType = new Dictionary<IntPtr, Type>();

		// Token: 0x040030CC RID: 12492
		private static ModuleBuilder moduleBuilder;

		// Token: 0x040030CD RID: 12493
		private static int numNewTypes;

		// Token: 0x040030CE RID: 12494
		private static object lockObj = new object();
	}
}
