using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000A9F RID: 2719
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AsyncUtils
	{
		// Token: 0x06006C41 RID: 27713 RVA: 0x0020ACB0 File Offset: 0x0020ACB0
		internal static Task<bool> ToAsync(this bool value)
		{
			if (!value)
			{
				return AsyncUtils.False;
			}
			return AsyncUtils.True;
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x0020ACC4 File Offset: 0x0020ACC4
		[NullableContext(2)]
		public static Task CancelIfRequestedAsync(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06006C43 RID: 27715 RVA: 0x0020ACDC File Offset: 0x0020ACDC
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static Task<T> CancelIfRequestedAsync<T>(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled<T>();
		}

		// Token: 0x06006C44 RID: 27716 RVA: 0x0020ACF4 File Offset: 0x0020ACF4
		public static Task FromCanceled(this CancellationToken cancellationToken)
		{
			return new Task(delegate()
			{
			}, cancellationToken);
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x0020AD20 File Offset: 0x0020AD20
		public static Task<T> FromCanceled<[Nullable(2)] T>(this CancellationToken cancellationToken)
		{
			Func<T> function;
			if ((function = AsyncUtils.<>c__6<T>.<>9__6_0) == null)
			{
				Func<T> func = AsyncUtils.<>c__6<T>.<>9__6_0 = (() => default(T));
				function = func;
			}
			return new Task<T>(function, cancellationToken);
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x0020AD5C File Offset: 0x0020AD5C
		public static Task WriteAsync(this TextWriter writer, char value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06006C47 RID: 27719 RVA: 0x0020AD78 File Offset: 0x0020AD78
		public static Task WriteAsync(this TextWriter writer, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x0020AD94 File Offset: 0x0020AD94
		public static Task WriteAsync(this TextWriter writer, char[] value, int start, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value, start, count);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06006C49 RID: 27721 RVA: 0x0020ADB4 File Offset: 0x0020ADB4
		public static Task<int> ReadAsync(this TextReader reader, char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return reader.ReadAsync(buffer, index, count);
			}
			return cancellationToken.FromCanceled<int>();
		}

		// Token: 0x06006C4A RID: 27722 RVA: 0x0020ADD4 File Offset: 0x0020ADD4
		public static bool IsCompletedSucessfully(this Task task)
		{
			return task.Status == TaskStatus.RanToCompletion;
		}

		// Token: 0x0400364A RID: 13898
		public static readonly Task<bool> False = Task.FromResult<bool>(false);

		// Token: 0x0400364B RID: 13899
		public static readonly Task<bool> True = Task.FromResult<bool>(true);

		// Token: 0x0400364C RID: 13900
		internal static readonly Task CompletedTask = Task.Delay(0);
	}
}
