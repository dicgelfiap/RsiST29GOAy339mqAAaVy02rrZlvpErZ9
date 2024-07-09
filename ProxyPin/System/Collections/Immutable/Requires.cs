using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CC6 RID: 3270
	internal static class Requires
	{
		// Token: 0x06008401 RID: 33793 RVA: 0x00268E40 File Offset: 0x00268E40
		[DebuggerStepThrough]
		public static void NotNull<T>([ValidatedNotNull] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x06008402 RID: 33794 RVA: 0x00268E54 File Offset: 0x00268E54
		[DebuggerStepThrough]
		public static T NotNullPassthrough<T>([ValidatedNotNull] T value, string parameterName) where T : class
		{
			Requires.NotNull<T>(value, parameterName);
			return value;
		}

		// Token: 0x06008403 RID: 33795 RVA: 0x00268E60 File Offset: 0x00268E60
		[DebuggerStepThrough]
		public static void NotNullAllowStructs<T>([ValidatedNotNull] T value, string parameterName)
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x06008404 RID: 33796 RVA: 0x00268E74 File Offset: 0x00268E74
		[DebuggerStepThrough]
		private static void FailArgumentNullException(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x06008405 RID: 33797 RVA: 0x00268E7C File Offset: 0x00268E7C
		[DebuggerStepThrough]
		public static void Range(bool condition, string parameterName, string message = null)
		{
			if (!condition)
			{
				Requires.FailRange(parameterName, message);
			}
		}

		// Token: 0x06008406 RID: 33798 RVA: 0x00268E8C File Offset: 0x00268E8C
		[DebuggerStepThrough]
		public static void FailRange(string parameterName, string message = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
			throw new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x06008407 RID: 33799 RVA: 0x00268EA8 File Offset: 0x00268EA8
		[DebuggerStepThrough]
		public static void Argument(bool condition, string parameterName, string message)
		{
			if (!condition)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x06008408 RID: 33800 RVA: 0x00268EB8 File Offset: 0x00268EB8
		[DebuggerStepThrough]
		public static void Argument(bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x06008409 RID: 33801 RVA: 0x00268EC8 File Offset: 0x00268EC8
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void FailObjectDisposed<TDisposed>(TDisposed disposed)
		{
			throw new ObjectDisposedException(disposed.GetType().FullName);
		}
	}
}
