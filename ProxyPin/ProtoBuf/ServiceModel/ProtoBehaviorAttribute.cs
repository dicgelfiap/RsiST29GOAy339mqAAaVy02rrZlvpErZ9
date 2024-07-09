using System;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ProtoBuf.ServiceModel
{
	// Token: 0x02000C41 RID: 3137
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[ComVisible(true)]
	public sealed class ProtoBehaviorAttribute : Attribute, IOperationBehavior
	{
		// Token: 0x06007CC0 RID: 31936 RVA: 0x0024B1EC File Offset: 0x0024B1EC
		void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06007CC1 RID: 31937 RVA: 0x0024B1F0 File Offset: 0x0024B1F0
		void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
			IOperationBehavior operationBehavior = new ProtoOperationBehavior(operationDescription);
			operationBehavior.ApplyClientBehavior(operationDescription, clientOperation);
		}

		// Token: 0x06007CC2 RID: 31938 RVA: 0x0024B210 File Offset: 0x0024B210
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			IOperationBehavior operationBehavior = new ProtoOperationBehavior(operationDescription);
			operationBehavior.ApplyDispatchBehavior(operationDescription, dispatchOperation);
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x0024B230 File Offset: 0x0024B230
		void IOperationBehavior.Validate(OperationDescription operationDescription)
		{
		}
	}
}
