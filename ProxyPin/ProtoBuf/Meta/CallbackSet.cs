using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C73 RID: 3187
	[ComVisible(true)]
	public class CallbackSet
	{
		// Token: 0x06007EA6 RID: 32422 RVA: 0x00254014 File Offset: 0x00254014
		internal CallbackSet(MetaType metaType)
		{
			if (metaType == null)
			{
				throw new ArgumentNullException("metaType");
			}
			this.metaType = metaType;
		}

		// Token: 0x17001B85 RID: 7045
		internal MethodInfo this[TypeModel.CallbackType callbackType]
		{
			get
			{
				switch (callbackType)
				{
				case TypeModel.CallbackType.BeforeSerialize:
					return this.beforeSerialize;
				case TypeModel.CallbackType.AfterSerialize:
					return this.afterSerialize;
				case TypeModel.CallbackType.BeforeDeserialize:
					return this.beforeDeserialize;
				case TypeModel.CallbackType.AfterDeserialize:
					return this.afterDeserialize;
				default:
					throw new ArgumentException("Callback type not supported: " + callbackType.ToString(), "callbackType");
				}
			}
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x002540A4 File Offset: 0x002540A4
		internal static bool CheckCallbackParameters(TypeModel model, MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				if (!(parameterType == model.MapType(typeof(SerializationContext))) && !(parameterType == model.MapType(typeof(Type))) && !(parameterType == model.MapType(typeof(StreamingContext))))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x00254130 File Offset: 0x00254130
		private MethodInfo SanityCheckCallback(TypeModel model, MethodInfo callback)
		{
			this.metaType.ThrowIfFrozen();
			if (callback == null)
			{
				return callback;
			}
			if (callback.IsStatic)
			{
				throw new ArgumentException("Callbacks cannot be static", "callback");
			}
			if (callback.ReturnType != model.MapType(typeof(void)) || !CallbackSet.CheckCallbackParameters(model, callback))
			{
				throw CallbackSet.CreateInvalidCallbackSignature(callback);
			}
			return callback;
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x002541AC File Offset: 0x002541AC
		internal static Exception CreateInvalidCallbackSignature(MethodInfo method)
		{
			return new NotSupportedException("Invalid callback signature in " + method.DeclaringType.FullName + "." + method.Name);
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x06007EAB RID: 32427 RVA: 0x002541E4 File Offset: 0x002541E4
		// (set) Token: 0x06007EAC RID: 32428 RVA: 0x002541EC File Offset: 0x002541EC
		public MethodInfo BeforeSerialize
		{
			get
			{
				return this.beforeSerialize;
			}
			set
			{
				this.beforeSerialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x06007EAD RID: 32429 RVA: 0x00254208 File Offset: 0x00254208
		// (set) Token: 0x06007EAE RID: 32430 RVA: 0x00254210 File Offset: 0x00254210
		public MethodInfo BeforeDeserialize
		{
			get
			{
				return this.beforeDeserialize;
			}
			set
			{
				this.beforeDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x06007EAF RID: 32431 RVA: 0x0025422C File Offset: 0x0025422C
		// (set) Token: 0x06007EB0 RID: 32432 RVA: 0x00254234 File Offset: 0x00254234
		public MethodInfo AfterSerialize
		{
			get
			{
				return this.afterSerialize;
			}
			set
			{
				this.afterSerialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x06007EB1 RID: 32433 RVA: 0x00254250 File Offset: 0x00254250
		// (set) Token: 0x06007EB2 RID: 32434 RVA: 0x00254258 File Offset: 0x00254258
		public MethodInfo AfterDeserialize
		{
			get
			{
				return this.afterDeserialize;
			}
			set
			{
				this.afterDeserialize = this.SanityCheckCallback(this.metaType.Model, value);
			}
		}

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06007EB3 RID: 32435 RVA: 0x00254274 File Offset: 0x00254274
		public bool NonTrivial
		{
			get
			{
				return this.beforeSerialize != null || this.beforeDeserialize != null || this.afterSerialize != null || this.afterDeserialize != null;
			}
		}

		// Token: 0x04003CA1 RID: 15521
		private readonly MetaType metaType;

		// Token: 0x04003CA2 RID: 15522
		private MethodInfo beforeSerialize;

		// Token: 0x04003CA3 RID: 15523
		private MethodInfo afterSerialize;

		// Token: 0x04003CA4 RID: 15524
		private MethodInfo beforeDeserialize;

		// Token: 0x04003CA5 RID: 15525
		private MethodInfo afterDeserialize;
	}
}
