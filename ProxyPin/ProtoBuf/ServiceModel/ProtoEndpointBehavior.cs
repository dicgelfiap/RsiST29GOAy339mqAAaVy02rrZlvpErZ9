using System;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ProtoBuf.ServiceModel
{
	// Token: 0x02000C43 RID: 3139
	[ComVisible(true)]
	public class ProtoEndpointBehavior : IEndpointBehavior
	{
		// Token: 0x06007CC8 RID: 31944 RVA: 0x0024B258 File Offset: 0x0024B258
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06007CC9 RID: 31945 RVA: 0x0024B25C File Offset: 0x0024B25C
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			ProtoEndpointBehavior.ReplaceDataContractSerializerOperationBehavior(endpoint);
		}

		// Token: 0x06007CCA RID: 31946 RVA: 0x0024B264 File Offset: 0x0024B264
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			ProtoEndpointBehavior.ReplaceDataContractSerializerOperationBehavior(endpoint);
		}

		// Token: 0x06007CCB RID: 31947 RVA: 0x0024B26C File Offset: 0x0024B26C
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06007CCC RID: 31948 RVA: 0x0024B270 File Offset: 0x0024B270
		private static void ReplaceDataContractSerializerOperationBehavior(ServiceEndpoint serviceEndpoint)
		{
			foreach (OperationDescription description in serviceEndpoint.Contract.Operations)
			{
				ProtoEndpointBehavior.ReplaceDataContractSerializerOperationBehavior(description);
			}
		}

		// Token: 0x06007CCD RID: 31949 RVA: 0x0024B2CC File Offset: 0x0024B2CC
		private static void ReplaceDataContractSerializerOperationBehavior(OperationDescription description)
		{
			DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
			if (dataContractSerializerOperationBehavior != null)
			{
				description.Behaviors.Remove(dataContractSerializerOperationBehavior);
				ProtoOperationBehavior protoOperationBehavior = new ProtoOperationBehavior(description);
				protoOperationBehavior.MaxItemsInObjectGraph = dataContractSerializerOperationBehavior.MaxItemsInObjectGraph;
				description.Behaviors.Add(protoOperationBehavior);
			}
		}
	}
}
