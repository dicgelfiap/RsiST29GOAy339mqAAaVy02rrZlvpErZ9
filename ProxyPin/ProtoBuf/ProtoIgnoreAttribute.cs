using System;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C34 RID: 3124
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	[ComVisible(true)]
	public class ProtoIgnoreAttribute : Attribute
	{
	}
}
