using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C19 RID: 3097
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	[ComVisible(true)]
	public sealed class ProtoAfterDeserializationAttribute : Attribute
	{
	}
}
