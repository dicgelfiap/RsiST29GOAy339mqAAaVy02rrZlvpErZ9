using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AE9 RID: 2793
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonContract
	{
		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06006ED6 RID: 28374 RVA: 0x00218888 File Offset: 0x00218888
		public Type UnderlyingType { get; }

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06006ED7 RID: 28375 RVA: 0x00218890 File Offset: 0x00218890
		// (set) Token: 0x06006ED8 RID: 28376 RVA: 0x00218898 File Offset: 0x00218898
		public Type CreatedType
		{
			get
			{
				return this._createdType;
			}
			set
			{
				ValidationUtils.ArgumentNotNull(value, "value");
				this._createdType = value;
				this.IsSealed = this._createdType.IsSealed();
				this.IsInstantiable = (!this._createdType.IsInterface() && !this._createdType.IsAbstract());
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06006ED9 RID: 28377 RVA: 0x002188F8 File Offset: 0x002188F8
		// (set) Token: 0x06006EDA RID: 28378 RVA: 0x00218900 File Offset: 0x00218900
		public bool? IsReference { get; set; }

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06006EDB RID: 28379 RVA: 0x0021890C File Offset: 0x0021890C
		// (set) Token: 0x06006EDC RID: 28380 RVA: 0x00218914 File Offset: 0x00218914
		[Nullable(2)]
		public JsonConverter Converter { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06006EDD RID: 28381 RVA: 0x00218920 File Offset: 0x00218920
		// (set) Token: 0x06006EDE RID: 28382 RVA: 0x00218928 File Offset: 0x00218928
		[Nullable(2)]
		public JsonConverter InternalConverter { [NullableContext(2)] get; [NullableContext(2)] internal set; }

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06006EDF RID: 28383 RVA: 0x00218934 File Offset: 0x00218934
		public IList<SerializationCallback> OnDeserializedCallbacks
		{
			get
			{
				if (this._onDeserializedCallbacks == null)
				{
					this._onDeserializedCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializedCallbacks;
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06006EE0 RID: 28384 RVA: 0x00218954 File Offset: 0x00218954
		public IList<SerializationCallback> OnDeserializingCallbacks
		{
			get
			{
				if (this._onDeserializingCallbacks == null)
				{
					this._onDeserializingCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializingCallbacks;
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06006EE1 RID: 28385 RVA: 0x00218974 File Offset: 0x00218974
		public IList<SerializationCallback> OnSerializedCallbacks
		{
			get
			{
				if (this._onSerializedCallbacks == null)
				{
					this._onSerializedCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializedCallbacks;
			}
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06006EE2 RID: 28386 RVA: 0x00218994 File Offset: 0x00218994
		public IList<SerializationCallback> OnSerializingCallbacks
		{
			get
			{
				if (this._onSerializingCallbacks == null)
				{
					this._onSerializingCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializingCallbacks;
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06006EE3 RID: 28387 RVA: 0x002189B4 File Offset: 0x002189B4
		public IList<SerializationErrorCallback> OnErrorCallbacks
		{
			get
			{
				if (this._onErrorCallbacks == null)
				{
					this._onErrorCallbacks = new List<SerializationErrorCallback>();
				}
				return this._onErrorCallbacks;
			}
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x002189D4 File Offset: 0x002189D4
		// (set) Token: 0x06006EE5 RID: 28389 RVA: 0x002189DC File Offset: 0x002189DC
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Func<object> DefaultCreator { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06006EE6 RID: 28390 RVA: 0x002189E8 File Offset: 0x002189E8
		// (set) Token: 0x06006EE7 RID: 28391 RVA: 0x002189F0 File Offset: 0x002189F0
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x06006EE8 RID: 28392 RVA: 0x002189FC File Offset: 0x002189FC
		internal JsonContract(Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			this.UnderlyingType = underlyingType;
			underlyingType = ReflectionUtils.EnsureNotByRefType(underlyingType);
			this.IsNullable = ReflectionUtils.IsNullable(underlyingType);
			this.NonNullableUnderlyingType = ((this.IsNullable && ReflectionUtils.IsNullableType(underlyingType)) ? Nullable.GetUnderlyingType(underlyingType) : underlyingType);
			this._createdType = (this.CreatedType = this.NonNullableUnderlyingType);
			this.IsConvertable = ConvertUtils.IsConvertible(this.NonNullableUnderlyingType);
			this.IsEnum = this.NonNullableUnderlyingType.IsEnum();
			this.InternalReadType = ReadType.Read;
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x00218AA0 File Offset: 0x00218AA0
		internal void InvokeOnSerializing(object o, StreamingContext context)
		{
			if (this._onSerializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x00218B04 File Offset: 0x00218B04
		internal void InvokeOnSerialized(object o, StreamingContext context)
		{
			if (this._onSerializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x00218B68 File Offset: 0x00218B68
		internal void InvokeOnDeserializing(object o, StreamingContext context)
		{
			if (this._onDeserializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x06006EEC RID: 28396 RVA: 0x00218BCC File Offset: 0x00218BCC
		internal void InvokeOnDeserialized(object o, StreamingContext context)
		{
			if (this._onDeserializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x06006EED RID: 28397 RVA: 0x00218C30 File Offset: 0x00218C30
		internal void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
		{
			if (this._onErrorCallbacks != null)
			{
				foreach (SerializationErrorCallback serializationErrorCallback in this._onErrorCallbacks)
				{
					serializationErrorCallback(o, context, errorContext);
				}
			}
		}

		// Token: 0x06006EEE RID: 28398 RVA: 0x00218C94 File Offset: 0x00218C94
		internal static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[]
				{
					context
				});
			};
		}

		// Token: 0x06006EEF RID: 28399 RVA: 0x00218CB0 File Offset: 0x00218CB0
		internal static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context, ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[]
				{
					context,
					econtext
				});
			};
		}

		// Token: 0x0400374D RID: 14157
		internal bool IsNullable;

		// Token: 0x0400374E RID: 14158
		internal bool IsConvertable;

		// Token: 0x0400374F RID: 14159
		internal bool IsEnum;

		// Token: 0x04003750 RID: 14160
		internal Type NonNullableUnderlyingType;

		// Token: 0x04003751 RID: 14161
		internal ReadType InternalReadType;

		// Token: 0x04003752 RID: 14162
		internal JsonContractType ContractType;

		// Token: 0x04003753 RID: 14163
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x04003754 RID: 14164
		internal bool IsSealed;

		// Token: 0x04003755 RID: 14165
		internal bool IsInstantiable;

		// Token: 0x04003756 RID: 14166
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x04003757 RID: 14167
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x04003758 RID: 14168
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x04003759 RID: 14169
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x0400375A RID: 14170
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationErrorCallback> _onErrorCallbacks;

		// Token: 0x0400375B RID: 14171
		private Type _createdType;
	}
}
