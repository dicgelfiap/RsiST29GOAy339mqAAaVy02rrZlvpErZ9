using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Numerics.Vectors;

namespace System
{
	// Token: 0x02000CFC RID: 3324
	internal static class SR
	{
		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x060086B5 RID: 34485 RVA: 0x0027B52C File Offset: 0x0027B52C
		private static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager result;
				if ((result = System.Numerics.Vectors2618085.SR.s_resourceManager) == null)
				{
					result = (System.Numerics.Vectors2618085.SR.s_resourceManager = new ResourceManager(System.Numerics.Vectors2618085.SR.ResourceType));
				}
				return result;
			}
		}

		// Token: 0x060086B6 RID: 34486 RVA: 0x0027B54C File Offset: 0x0027B54C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x060086B7 RID: 34487 RVA: 0x0027B550 File Offset: 0x0027B550
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.Numerics.Vectors2618085.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text, StringComparison.Ordinal))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x060086B8 RID: 34488 RVA: 0x0027B59C File Offset: 0x0027B59C
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.Numerics.Vectors2618085.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x060086B9 RID: 34489 RVA: 0x0027B5CC File Offset: 0x0027B5CC
		internal static string Format(string resourceFormat, object p1)
		{
			if (System.Numerics.Vectors2618085.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x060086BA RID: 34490 RVA: 0x0027B5F8 File Offset: 0x0027B5F8
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.Numerics.Vectors2618085.SR.UsingResourceKeys())
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

		// Token: 0x060086BB RID: 34491 RVA: 0x0027B62C File Offset: 0x0027B62C
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.Numerics.Vectors2618085.SR.UsingResourceKeys())
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

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x060086BC RID: 34492 RVA: 0x0027B664 File Offset: 0x0027B664
		internal static Type ResourceType { get; } = typeof(FxResources.System.Numerics.Vectors.SR);

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x060086BD RID: 34493 RVA: 0x0027B66C File Offset: 0x0027B66C
		internal static string Arg_ArgumentOutOfRangeException
		{
			get
			{
				return System.Numerics.Vectors2618085.SR.GetResourceString("Arg_ArgumentOutOfRangeException", null);
			}
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x060086BE RID: 34494 RVA: 0x0027B67C File Offset: 0x0027B67C
		internal static string Arg_ElementsInSourceIsGreaterThanDestination
		{
			get
			{
				return System.Numerics.Vectors2618085.SR.GetResourceString("Arg_ElementsInSourceIsGreaterThanDestination", null);
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x060086BF RID: 34495 RVA: 0x0027B68C File Offset: 0x0027B68C
		internal static string Arg_NullArgumentNullRef
		{
			get
			{
				return System.Numerics.Vectors2618085.SR.GetResourceString("Arg_NullArgumentNullRef", null);
			}
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x060086C0 RID: 34496 RVA: 0x0027B69C File Offset: 0x0027B69C
		internal static string Arg_TypeNotSupported
		{
			get
			{
				return System.Numerics.Vectors2618085.SR.GetResourceString("Arg_TypeNotSupported", null);
			}
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x060086C1 RID: 34497 RVA: 0x0027B6AC File Offset: 0x0027B6AC
		internal static string Arg_InsufficientNumberOfElements
		{
			get
			{
				return System.Numerics.Vectors2618085.SR.GetResourceString("Arg_InsufficientNumberOfElements", null);
			}
		}

		// Token: 0x04003E32 RID: 15922
		private static ResourceManager s_resourceManager;
	}
}
