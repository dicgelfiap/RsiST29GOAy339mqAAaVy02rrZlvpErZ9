﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C16 RID: 3094
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[ImmutableObject(true)]
	[ComVisible(true)]
	public sealed class ProtoBeforeSerializationAttribute : Attribute
	{
	}
}