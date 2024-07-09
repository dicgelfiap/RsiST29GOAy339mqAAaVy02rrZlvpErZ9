using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Buffers;

namespace System
{
	// Token: 0x02000C85 RID: 3205
	internal static class SR
	{
		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x06008060 RID: 32864 RVA: 0x00260274 File Offset: 0x00260274
		private static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager result;
				if ((result = System.SR.s_resourceManager) == null)
				{
					result = (System.SR.s_resourceManager = new ResourceManager(System.SR.ResourceType));
				}
				return result;
			}
		}

		// Token: 0x06008061 RID: 32865 RVA: 0x00260294 File Offset: 0x00260294
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x00260298 File Offset: 0x00260298
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.SR.ResourceManager.GetString(resourceKey);
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

		// Token: 0x06008063 RID: 32867 RVA: 0x002602E4 File Offset: 0x002602E4
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x06008064 RID: 32868 RVA: 0x00260314 File Offset: 0x00260314
		internal static string Format(string resourceFormat, object p1)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06008065 RID: 32869 RVA: 0x00260340 File Offset: 0x00260340
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.SR.UsingResourceKeys())
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

		// Token: 0x06008066 RID: 32870 RVA: 0x00260374 File Offset: 0x00260374
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.SR.UsingResourceKeys())
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

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x06008067 RID: 32871 RVA: 0x002603AC File Offset: 0x002603AC
		internal static Type ResourceType { get; } = typeof(FxResources.System.Buffers.SR);

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x06008068 RID: 32872 RVA: 0x002603B4 File Offset: 0x002603B4
		internal static string ArgumentException_BufferNotFromPool
		{
			get
			{
				return System.SR.GetResourceString("ArgumentException_BufferNotFromPool", null);
			}
		}

		// Token: 0x04003D12 RID: 15634
		private static ResourceManager s_resourceManager;
	}
}
