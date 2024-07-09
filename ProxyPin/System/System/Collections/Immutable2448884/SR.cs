using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Collections.Immutable;

namespace System
{
	// Token: 0x02000C90 RID: 3216
	internal static class SR
	{
		// Token: 0x06008084 RID: 32900 RVA: 0x00260998 File Offset: 0x00260998
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06008085 RID: 32901 RVA: 0x0026099C File Offset: 0x0026099C
		[System.Collections.Immutable.NullableContext(1)]
		internal static string GetResourceString(string resourceKey, [System.Collections.Immutable.Nullable(2)] string defaultString = null)
		{
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return defaultString ?? resourceKey;
			}
			string text = null;
			try
			{
				text = System.Collections.Immutable2448884.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x06008086 RID: 32902 RVA: 0x002609FC File Offset: 0x002609FC
		[System.Collections.Immutable.NullableContext(1)]
		internal static string Format(string resourceFormat, [System.Collections.Immutable.Nullable(2)] object p1)
		{
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06008087 RID: 32903 RVA: 0x00260A28 File Offset: 0x00260A28
		[System.Collections.Immutable.NullableContext(1)]
		internal static string Format(string resourceFormat, [System.Collections.Immutable.Nullable(2)] object p1, [System.Collections.Immutable.Nullable(2)] object p2)
		{
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x06008088 RID: 32904 RVA: 0x00260A5C File Offset: 0x00260A5C
		[System.Collections.Immutable.NullableContext(2)]
		[return: System.Collections.Immutable.Nullable(1)]
		internal static string Format([System.Collections.Immutable.Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x06008089 RID: 32905 RVA: 0x00260A94 File Offset: 0x00260A94
		[System.Collections.Immutable.NullableContext(1)]
		internal static string Format(string resourceFormat, [System.Collections.Immutable.Nullable(2)] params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x0600808A RID: 32906 RVA: 0x00260AC8 File Offset: 0x00260AC8
		[System.Collections.Immutable.NullableContext(1)]
		internal static string Format([System.Collections.Immutable.Nullable(2)] IFormatProvider provider, string resourceFormat, [System.Collections.Immutable.Nullable(2)] object p1)
		{
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x0600808B RID: 32907 RVA: 0x00260AF8 File Offset: 0x00260AF8
		[System.Collections.Immutable.NullableContext(2)]
		[return: System.Collections.Immutable.Nullable(1)]
		internal static string Format(IFormatProvider provider, [System.Collections.Immutable.Nullable(1)] string resourceFormat, object p1, object p2)
		{
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x0600808C RID: 32908 RVA: 0x00260B2C File Offset: 0x00260B2C
		[System.Collections.Immutable.NullableContext(2)]
		[return: System.Collections.Immutable.Nullable(1)]
		internal static string Format(IFormatProvider provider, [System.Collections.Immutable.Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x00260B68 File Offset: 0x00260B68
		[System.Collections.Immutable.NullableContext(1)]
		internal static string Format([System.Collections.Immutable.Nullable(2)] IFormatProvider provider, string resourceFormat, [System.Collections.Immutable.Nullable(2)] params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.Collections.Immutable2448884.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(provider, resourceFormat, args);
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x0600808E RID: 32910 RVA: 0x00260B9C File Offset: 0x00260B9C
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager result;
				if ((result = System.Collections.Immutable2448884.SR.s_resourceManager) == null)
				{
					result = (System.Collections.Immutable2448884.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Collections.Immutable.SR)));
				}
				return result;
			}
		}

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x0600808F RID: 32911 RVA: 0x00260BC0 File Offset: 0x00260BC0
		internal static string Arg_KeyNotFoundWithKey
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("Arg_KeyNotFoundWithKey", null);
			}
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06008090 RID: 32912 RVA: 0x00260BD0 File Offset: 0x00260BD0
		internal static string ArrayInitializedStateNotEqual
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("ArrayInitializedStateNotEqual", null);
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x06008091 RID: 32913 RVA: 0x00260BE0 File Offset: 0x00260BE0
		internal static string ArrayLengthsNotEqual
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("ArrayLengthsNotEqual", null);
			}
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x06008092 RID: 32914 RVA: 0x00260BF0 File Offset: 0x00260BF0
		internal static string CannotFindOldValue
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("CannotFindOldValue", null);
			}
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x06008093 RID: 32915 RVA: 0x00260C00 File Offset: 0x00260C00
		internal static string CapacityMustBeGreaterThanOrEqualToCount
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("CapacityMustBeGreaterThanOrEqualToCount", null);
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x06008094 RID: 32916 RVA: 0x00260C10 File Offset: 0x00260C10
		internal static string CapacityMustEqualCountOnMove
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("CapacityMustEqualCountOnMove", null);
			}
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06008095 RID: 32917 RVA: 0x00260C20 File Offset: 0x00260C20
		internal static string CollectionModifiedDuringEnumeration
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("CollectionModifiedDuringEnumeration", null);
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06008096 RID: 32918 RVA: 0x00260C30 File Offset: 0x00260C30
		internal static string DuplicateKey
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("DuplicateKey", null);
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x06008097 RID: 32919 RVA: 0x00260C40 File Offset: 0x00260C40
		internal static string InvalidEmptyOperation
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("InvalidEmptyOperation", null);
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x06008098 RID: 32920 RVA: 0x00260C50 File Offset: 0x00260C50
		internal static string InvalidOperationOnDefaultArray
		{
			get
			{
				return System.Collections.Immutable2448884.SR.GetResourceString("InvalidOperationOnDefaultArray", null);
			}
		}

		// Token: 0x04003D1D RID: 15645
		private static ResourceManager s_resourceManager;
	}
}
