using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Memory;

namespace System
{
	// Token: 0x02000CE0 RID: 3296
	internal static class SR
	{
		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06008557 RID: 34135 RVA: 0x002715A8 File Offset: 0x002715A8
		private static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager result;
				if ((result = System.Memory2565708.SR.s_resourceManager) == null)
				{
					result = (System.Memory2565708.SR.s_resourceManager = new ResourceManager(System.Memory2565708.SR.ResourceType));
				}
				return result;
			}
		}

		// Token: 0x06008558 RID: 34136 RVA: 0x002715C8 File Offset: 0x002715C8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06008559 RID: 34137 RVA: 0x002715CC File Offset: 0x002715CC
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.Memory2565708.SR.ResourceManager.GetString(resourceKey);
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

		// Token: 0x0600855A RID: 34138 RVA: 0x00271618 File Offset: 0x00271618
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.Memory2565708.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x0600855B RID: 34139 RVA: 0x00271648 File Offset: 0x00271648
		internal static string Format(string resourceFormat, object p1)
		{
			if (System.Memory2565708.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x0600855C RID: 34140 RVA: 0x00271674 File Offset: 0x00271674
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.Memory2565708.SR.UsingResourceKeys())
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

		// Token: 0x0600855D RID: 34141 RVA: 0x002716A8 File Offset: 0x002716A8
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.Memory2565708.SR.UsingResourceKeys())
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

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x0600855E RID: 34142 RVA: 0x002716E0 File Offset: 0x002716E0
		internal static Type ResourceType { get; } = typeof(FxResources.System.Memory.SR);

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x0600855F RID: 34143 RVA: 0x002716E8 File Offset: 0x002716E8
		internal static string NotSupported_CannotCallEqualsOnSpan
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("NotSupported_CannotCallEqualsOnSpan", null);
			}
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x06008560 RID: 34144 RVA: 0x002716F8 File Offset: 0x002716F8
		internal static string NotSupported_CannotCallGetHashCodeOnSpan
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("NotSupported_CannotCallGetHashCodeOnSpan", null);
			}
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x06008561 RID: 34145 RVA: 0x00271708 File Offset: 0x00271708
		internal static string Argument_InvalidTypeWithPointersNotSupported
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("Argument_InvalidTypeWithPointersNotSupported", null);
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x06008562 RID: 34146 RVA: 0x00271718 File Offset: 0x00271718
		internal static string Argument_DestinationTooShort
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("Argument_DestinationTooShort", null);
			}
		}

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x06008563 RID: 34147 RVA: 0x00271728 File Offset: 0x00271728
		internal static string MemoryDisposed
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("MemoryDisposed", null);
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x06008564 RID: 34148 RVA: 0x00271738 File Offset: 0x00271738
		internal static string OutstandingReferences
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("OutstandingReferences", null);
			}
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x06008565 RID: 34149 RVA: 0x00271748 File Offset: 0x00271748
		internal static string Argument_BadFormatSpecifier
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("Argument_BadFormatSpecifier", null);
			}
		}

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x06008566 RID: 34150 RVA: 0x00271758 File Offset: 0x00271758
		internal static string Argument_GWithPrecisionNotSupported
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("Argument_GWithPrecisionNotSupported", null);
			}
		}

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06008567 RID: 34151 RVA: 0x00271768 File Offset: 0x00271768
		internal static string Argument_CannotParsePrecision
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("Argument_CannotParsePrecision", null);
			}
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06008568 RID: 34152 RVA: 0x00271778 File Offset: 0x00271778
		internal static string Argument_PrecisionTooLarge
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("Argument_PrecisionTooLarge", null);
			}
		}

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06008569 RID: 34153 RVA: 0x00271788 File Offset: 0x00271788
		internal static string Argument_OverlapAlignmentMismatch
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("Argument_OverlapAlignmentMismatch", null);
			}
		}

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x0600856A RID: 34154 RVA: 0x00271798 File Offset: 0x00271798
		internal static string EndPositionNotReached
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("EndPositionNotReached", null);
			}
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x0600856B RID: 34155 RVA: 0x002717A8 File Offset: 0x002717A8
		internal static string UnexpectedSegmentType
		{
			get
			{
				return System.Memory2565708.SR.GetResourceString("UnexpectedSegmentType", null);
			}
		}

		// Token: 0x04003DC5 RID: 15813
		private static ResourceManager s_resourceManager;
	}
}
