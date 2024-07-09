using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C2C RID: 3116
	[ComVisible(true)]
	public interface IMeasuredProtoOutput<TOutput> : IProtoOutput<TOutput>
	{
		// Token: 0x06007BB3 RID: 31667
		MeasureState<T> Measure<T>(T value, object userState = null);

		// Token: 0x06007BB4 RID: 31668
		void Serialize<T>(MeasureState<T> measured, TOutput destination);
	}
}
