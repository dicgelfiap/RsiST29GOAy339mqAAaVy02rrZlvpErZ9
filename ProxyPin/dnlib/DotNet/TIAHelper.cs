using System;

namespace dnlib.DotNet
{
	// Token: 0x02000858 RID: 2136
	internal static class TIAHelper
	{
		// Token: 0x0600509E RID: 20638 RVA: 0x00190768 File Offset: 0x00190768
		private static TIAHelper.Info? GetInfo(TypeDef td)
		{
			if (td == null)
			{
				return null;
			}
			if (td.IsWindowsRuntime)
			{
				return null;
			}
			UTF8String scope = null;
			UTF8String utf8String = null;
			CustomAttribute customAttribute = td.CustomAttributes.Find("System.Runtime.InteropServices.TypeIdentifierAttribute");
			if (customAttribute != null)
			{
				if (customAttribute.ConstructorArguments.Count >= 2)
				{
					if (customAttribute.ConstructorArguments[0].Type.GetElementType() != ElementType.String)
					{
						return null;
					}
					if (customAttribute.ConstructorArguments[1].Type.GetElementType() != ElementType.String)
					{
						return null;
					}
					scope = ((customAttribute.ConstructorArguments[0].Value as UTF8String) ?? (customAttribute.ConstructorArguments[0].Value as string));
					utf8String = ((customAttribute.ConstructorArguments[1].Value as UTF8String) ?? (customAttribute.ConstructorArguments[1].Value as string));
				}
			}
			else
			{
				ModuleDef module = td.Module;
				AssemblyDef assemblyDef = (module != null) ? module.Assembly : null;
				if (assemblyDef == null)
				{
					return null;
				}
				if (!assemblyDef.CustomAttributes.IsDefined("System.Runtime.InteropServices.ImportedFromTypeLibAttribute") && !assemblyDef.CustomAttributes.IsDefined("System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute"))
				{
					return null;
				}
			}
			if (UTF8String.IsNull(utf8String))
			{
				CustomAttribute customAttribute2;
				if (td.IsInterface && td.IsImport)
				{
					customAttribute2 = td.CustomAttributes.Find("System.Runtime.InteropServices.GuidAttribute");
				}
				else
				{
					ModuleDef module2 = td.Module;
					AssemblyDef assemblyDef2 = (module2 != null) ? module2.Assembly : null;
					if (assemblyDef2 == null)
					{
						return null;
					}
					customAttribute2 = assemblyDef2.CustomAttributes.Find("System.Runtime.InteropServices.GuidAttribute");
				}
				if (customAttribute2 == null)
				{
					return null;
				}
				if (customAttribute2.ConstructorArguments.Count < 1)
				{
					return null;
				}
				if (customAttribute2.ConstructorArguments[0].Type.GetElementType() != ElementType.String)
				{
					return null;
				}
				scope = ((customAttribute2.ConstructorArguments[0].Value as UTF8String) ?? (customAttribute2.ConstructorArguments[0].Value as string));
				UTF8String @namespace = td.Namespace;
				UTF8String name = td.Name;
				if (UTF8String.IsNullOrEmpty(@namespace))
				{
					utf8String = name;
				}
				else if (UTF8String.IsNullOrEmpty(name))
				{
					utf8String = new UTF8String(TIAHelper.Concat(@namespace.Data, 46, Array2.Empty<byte>()));
				}
				else
				{
					utf8String = new UTF8String(TIAHelper.Concat(@namespace.Data, 46, name.Data));
				}
			}
			return new TIAHelper.Info?(new TIAHelper.Info(scope, utf8String));
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x00190A98 File Offset: 0x00190A98
		private static byte[] Concat(byte[] a, byte b, byte[] c)
		{
			byte[] array = new byte[a.Length + 1 + c.Length];
			for (int i = 0; i < a.Length; i++)
			{
				array[i] = a[i];
			}
			array[a.Length] = b;
			int j = 0;
			int num = a.Length + 1;
			while (j < c.Length)
			{
				array[num] = c[j];
				j++;
				num++;
			}
			return array;
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x00190AF8 File Offset: 0x00190AF8
		internal static bool IsTypeDefEquivalent(TypeDef td)
		{
			return TIAHelper.GetInfo(td) != null && TIAHelper.CheckEquivalent(td);
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x00190B24 File Offset: 0x00190B24
		private static bool CheckEquivalent(TypeDef td)
		{
			int num = 0;
			while (td != null && num < 1000)
			{
				if (num != 0 && TIAHelper.GetInfo(td) == null)
				{
					return false;
				}
				bool flag;
				if (td.IsInterface)
				{
					flag = (td.IsImport || td.CustomAttributes.IsDefined("System.Runtime.InteropServices.ComEventInterfaceAttribute"));
				}
				else
				{
					flag = (td.IsValueType || td.IsDelegate);
				}
				if (!flag)
				{
					return false;
				}
				if (td.GenericParameters.Count > 0)
				{
					return false;
				}
				TypeDef declaringType = td.DeclaringType;
				if (declaringType == null)
				{
					return td.IsPublic;
				}
				if (!td.IsNestedPublic)
				{
					return false;
				}
				td = declaringType;
				num++;
			}
			return false;
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x00190BF8 File Offset: 0x00190BF8
		public static bool Equivalent(TypeDef td1, TypeDef td2)
		{
			TIAHelper.Info? info = TIAHelper.GetInfo(td1);
			if (info == null)
			{
				return false;
			}
			TIAHelper.Info? info2 = TIAHelper.GetInfo(td2);
			if (info2 == null)
			{
				return false;
			}
			if (!TIAHelper.CheckEquivalent(td1) || !TIAHelper.CheckEquivalent(td2))
			{
				return false;
			}
			if (!info.Value.Equals(info2.Value))
			{
				return false;
			}
			for (int i = 0; i < 1000; i++)
			{
				if (td1.IsInterface)
				{
					if (!td2.IsInterface)
					{
						return false;
					}
				}
				else
				{
					bool baseType = td1.BaseType != null;
					ITypeDefOrRef baseType2 = td2.BaseType;
					if (!baseType || baseType2 == null)
					{
						return false;
					}
					if (td1.IsDelegate)
					{
						if (!td2.IsDelegate)
						{
							return false;
						}
						if (!TIAHelper.DelegateEquals(td1, td2))
						{
							return false;
						}
					}
					else
					{
						if (!td1.IsValueType)
						{
							return false;
						}
						if (td1.IsEnum != td2.IsEnum)
						{
							return false;
						}
						if (!td2.IsValueType)
						{
							return false;
						}
						if (!TIAHelper.ValueTypeEquals(td1, td2, td1.IsEnum))
						{
							return false;
						}
					}
				}
				td1 = td1.DeclaringType;
				td2 = td2.DeclaringType;
				if (td1 == null && td2 == null)
				{
					break;
				}
				if (td1 == null || td2 == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x00190D44 File Offset: 0x00190D44
		private static bool DelegateEquals(TypeDef td1, TypeDef td2)
		{
			bool flag = td1.FindMethod(TIAHelper.InvokeString) != null;
			MethodDef methodDef = td2.FindMethod(TIAHelper.InvokeString);
			return flag && methodDef != null;
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x00190D7C File Offset: 0x00190D7C
		private static bool ValueTypeEquals(TypeDef td1, TypeDef td2, bool isEnum)
		{
			return td1.Methods.Count == 0 && td2.Methods.Count == 0;
		}

		// Token: 0x0400276B RID: 10091
		private static readonly UTF8String InvokeString = new UTF8String("Invoke");

		// Token: 0x02000FF9 RID: 4089
		private readonly struct Info : IEquatable<TIAHelper.Info>
		{
			// Token: 0x06008EAF RID: 36527 RVA: 0x002AA630 File Offset: 0x002AA630
			public Info(UTF8String scope, UTF8String identifier)
			{
				this.Scope = scope;
				this.Identifier = identifier;
			}

			// Token: 0x06008EB0 RID: 36528 RVA: 0x002AA640 File Offset: 0x002AA640
			public bool Equals(TIAHelper.Info other)
			{
				return TIAHelper.Info.stricmp(this.Scope, other.Scope) && UTF8String.Equals(this.Identifier, other.Identifier);
			}

			// Token: 0x06008EB1 RID: 36529 RVA: 0x002AA66C File Offset: 0x002AA66C
			private static bool stricmp(UTF8String a, UTF8String b)
			{
				byte[] array = (a != null) ? a.Data : null;
				byte[] array2 = (b != null) ? b.Data : null;
				if (array == array2)
				{
					return true;
				}
				if (array == null || array2 == null)
				{
					return false;
				}
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					byte b2 = array[i];
					byte b3 = array2[i];
					if (65 <= b2 && b2 <= 90)
					{
						b2 = b2 - 65 + 97;
					}
					if (65 <= b3 && b3 <= 90)
					{
						b3 = b3 - 65 + 97;
					}
					if (b2 != b3)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04004411 RID: 17425
			public readonly UTF8String Scope;

			// Token: 0x04004412 RID: 17426
			public readonly UTF8String Identifier;
		}
	}
}
