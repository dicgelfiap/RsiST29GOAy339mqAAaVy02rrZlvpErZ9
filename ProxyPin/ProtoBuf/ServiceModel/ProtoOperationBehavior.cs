using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Xml;
using ProtoBuf.Meta;

namespace ProtoBuf.ServiceModel
{
	// Token: 0x02000C44 RID: 3140
	[ComVisible(true)]
	public sealed class ProtoOperationBehavior : DataContractSerializerOperationBehavior
	{
		// Token: 0x06007CCF RID: 31951 RVA: 0x0024B324 File Offset: 0x0024B324
		public ProtoOperationBehavior(OperationDescription operation) : base(operation)
		{
			this.model = RuntimeTypeModel.Default;
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x06007CD0 RID: 31952 RVA: 0x0024B338 File Offset: 0x0024B338
		// (set) Token: 0x06007CD1 RID: 31953 RVA: 0x0024B340 File Offset: 0x0024B340
		public TypeModel Model
		{
			get
			{
				return this.model;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.model = value;
			}
		}

		// Token: 0x06007CD2 RID: 31954 RVA: 0x0024B35C File Offset: 0x0024B35C
		public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
		{
			if (this.model == null)
			{
				throw new InvalidOperationException("No Model instance has been assigned to the ProtoOperationBehavior");
			}
			return XmlProtoSerializer.TryCreate(this.model, type) ?? base.CreateSerializer(type, name, ns, knownTypes);
		}

		// Token: 0x04003C2C RID: 15404
		private TypeModel model;
	}
}
