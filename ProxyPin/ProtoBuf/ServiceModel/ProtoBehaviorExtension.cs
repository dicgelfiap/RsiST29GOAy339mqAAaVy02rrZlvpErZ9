using System;
using System.Runtime.InteropServices;
using System.ServiceModel.Configuration;

namespace ProtoBuf.ServiceModel
{
	// Token: 0x02000C42 RID: 3138
	[ComVisible(true)]
	public class ProtoBehaviorExtension : BehaviorExtensionElement
	{
		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x06007CC6 RID: 31942 RVA: 0x0024B244 File Offset: 0x0024B244
		public override Type BehaviorType
		{
			get
			{
				return typeof(ProtoEndpointBehavior);
			}
		}

		// Token: 0x06007CC7 RID: 31943 RVA: 0x0024B250 File Offset: 0x0024B250
		protected override object CreateBehavior()
		{
			return new ProtoEndpointBehavior();
		}
	}
}
