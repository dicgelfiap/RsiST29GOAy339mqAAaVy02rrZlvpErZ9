using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C17 RID: 3095
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	[ComVisible(true)]
	public sealed class ProtoAfterSerializationAttribute : Attribute
	{
	}
}
