using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F9 RID: 1273
	internal class DtlsReliableHandshake
	{
		// Token: 0x0600271C RID: 10012 RVA: 0x000D3FA0 File Offset: 0x000D3FA0
		internal DtlsReliableHandshake(TlsContext context, DtlsRecordLayer transport)
		{
			this.mRecordLayer = transport;
			this.mHandshakeHash = new DeferredHash();
			this.mHandshakeHash.Init(context);
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000D4008 File Offset: 0x000D4008
		internal void NotifyHelloComplete()
		{
			this.mHandshakeHash = this.mHandshakeHash.NotifyPrfDetermined();
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x000D401C File Offset: 0x000D401C
		internal TlsHandshakeHash HandshakeHash
		{
			get
			{
				return this.mHandshakeHash;
			}
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000D4024 File Offset: 0x000D4024
		internal TlsHandshakeHash PrepareToFinish()
		{
			TlsHandshakeHash result = this.mHandshakeHash;
			this.mHandshakeHash = this.mHandshakeHash.StopTracking();
			return result;
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000D4050 File Offset: 0x000D4050
		internal void SendMessage(byte msg_type, byte[] body)
		{
			TlsUtilities.CheckUint24(body.Length);
			if (!this.mSending)
			{
				this.CheckInboundFlight();
				this.mSending = true;
				this.mOutboundFlight.Clear();
			}
			DtlsReliableHandshake.Message message = new DtlsReliableHandshake.Message(this.mMessageSeq++, msg_type, body);
			this.mOutboundFlight.Add(message);
			this.WriteMessage(message);
			this.UpdateHandshakeMessagesDigest(message);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000D40C4 File Offset: 0x000D40C4
		internal byte[] ReceiveMessageBody(byte msg_type)
		{
			DtlsReliableHandshake.Message message = this.ReceiveMessage();
			if (message.Type != msg_type)
			{
				throw new TlsFatalAlert(10);
			}
			return message.Body;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000D40F8 File Offset: 0x000D40F8
		internal DtlsReliableHandshake.Message ReceiveMessage()
		{
			if (this.mSending)
			{
				this.mSending = false;
				this.PrepareInboundFlight(Platform.CreateHashtable());
			}
			byte[] array = null;
			int num = 1000;
			for (;;)
			{
				try
				{
					while (!this.mRecordLayer.IsClosed)
					{
						DtlsReliableHandshake.Message pendingMessage = this.GetPendingMessage();
						if (pendingMessage != null)
						{
							return pendingMessage;
						}
						int receiveLimit = this.mRecordLayer.GetReceiveLimit();
						if (array == null || array.Length < receiveLimit)
						{
							array = new byte[receiveLimit];
						}
						int num2 = this.mRecordLayer.Receive(array, 0, receiveLimit, num);
						if (num2 < 0)
						{
							goto IL_C8;
						}
						bool flag = this.ProcessRecord(16, this.mRecordLayer.ReadEpoch, array, 0, num2);
						if (flag)
						{
							num = this.BackOff(num);
						}
					}
					throw new TlsFatalAlert(90);
				}
				catch (IOException)
				{
				}
				IL_C8:
				this.ResendOutboundFlight();
				num = this.BackOff(num);
			}
			DtlsReliableHandshake.Message result;
			return result;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000D41F4 File Offset: 0x000D41F4
		internal void Finish()
		{
			DtlsHandshakeRetransmit retransmit = null;
			if (!this.mSending)
			{
				this.CheckInboundFlight();
			}
			else
			{
				this.PrepareInboundFlight(null);
				if (this.mPreviousInboundFlight != null)
				{
					retransmit = new DtlsReliableHandshake.Retransmit(this);
				}
			}
			this.mRecordLayer.HandshakeSuccessful(retransmit);
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000D4244 File Offset: 0x000D4244
		internal void ResetHandshakeMessagesDigest()
		{
			this.mHandshakeHash.Reset();
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000D4254 File Offset: 0x000D4254
		private int BackOff(int timeoutMillis)
		{
			return Math.Min(timeoutMillis * 2, 60000);
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000D4264 File Offset: 0x000D4264
		private void CheckInboundFlight()
		{
			foreach (object obj in this.mCurrentInboundFlight.Keys)
			{
				int num = (int)obj;
				int num2 = this.mNextReceiveSeq;
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000D42D0 File Offset: 0x000D42D0
		private DtlsReliableHandshake.Message GetPendingMessage()
		{
			DtlsReassembler dtlsReassembler = (DtlsReassembler)this.mCurrentInboundFlight[this.mNextReceiveSeq];
			if (dtlsReassembler != null)
			{
				byte[] bodyIfComplete = dtlsReassembler.GetBodyIfComplete();
				if (bodyIfComplete != null)
				{
					this.mPreviousInboundFlight = null;
					return this.UpdateHandshakeMessagesDigest(new DtlsReliableHandshake.Message(this.mNextReceiveSeq++, dtlsReassembler.MsgType, bodyIfComplete));
				}
			}
			return null;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000D433C File Offset: 0x000D433C
		private void PrepareInboundFlight(IDictionary nextFlight)
		{
			DtlsReliableHandshake.ResetAll(this.mCurrentInboundFlight);
			this.mPreviousInboundFlight = this.mCurrentInboundFlight;
			this.mCurrentInboundFlight = nextFlight;
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000D435C File Offset: 0x000D435C
		private bool ProcessRecord(int windowSize, int epoch, byte[] buf, int off, int len)
		{
			bool flag = false;
			while (len >= 12)
			{
				int num = TlsUtilities.ReadUint24(buf, off + 9);
				int num2 = num + 12;
				if (len < num2)
				{
					break;
				}
				int num3 = TlsUtilities.ReadUint24(buf, off + 1);
				int num4 = TlsUtilities.ReadUint24(buf, off + 6);
				if (num4 + num > num3)
				{
					break;
				}
				byte b = TlsUtilities.ReadUint8(buf, off);
				int num5 = (b == 20) ? 1 : 0;
				if (epoch != num5)
				{
					break;
				}
				int num6 = TlsUtilities.ReadUint16(buf, off + 4);
				if (num6 < this.mNextReceiveSeq + windowSize)
				{
					if (num6 >= this.mNextReceiveSeq)
					{
						DtlsReassembler dtlsReassembler = (DtlsReassembler)this.mCurrentInboundFlight[num6];
						if (dtlsReassembler == null)
						{
							dtlsReassembler = new DtlsReassembler(b, num3);
							this.mCurrentInboundFlight[num6] = dtlsReassembler;
						}
						dtlsReassembler.ContributeFragment(b, num3, buf, off + 12, num4, num);
					}
					else if (this.mPreviousInboundFlight != null)
					{
						DtlsReassembler dtlsReassembler2 = (DtlsReassembler)this.mPreviousInboundFlight[num6];
						if (dtlsReassembler2 != null)
						{
							dtlsReassembler2.ContributeFragment(b, num3, buf, off + 12, num4, num);
							flag = true;
						}
					}
				}
				off += num2;
				len -= num2;
			}
			bool flag2 = flag && DtlsReliableHandshake.CheckAll(this.mPreviousInboundFlight);
			if (flag2)
			{
				this.ResendOutboundFlight();
				DtlsReliableHandshake.ResetAll(this.mPreviousInboundFlight);
			}
			return flag2;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000D44D8 File Offset: 0x000D44D8
		private void ResendOutboundFlight()
		{
			this.mRecordLayer.ResetWriteEpoch();
			for (int i = 0; i < this.mOutboundFlight.Count; i++)
			{
				this.WriteMessage((DtlsReliableHandshake.Message)this.mOutboundFlight[i]);
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000D4524 File Offset: 0x000D4524
		private DtlsReliableHandshake.Message UpdateHandshakeMessagesDigest(DtlsReliableHandshake.Message message)
		{
			if (message.Type != 0)
			{
				byte[] body = message.Body;
				byte[] array = new byte[12];
				TlsUtilities.WriteUint8(message.Type, array, 0);
				TlsUtilities.WriteUint24(body.Length, array, 1);
				TlsUtilities.WriteUint16(message.Seq, array, 4);
				TlsUtilities.WriteUint24(0, array, 6);
				TlsUtilities.WriteUint24(body.Length, array, 9);
				this.mHandshakeHash.BlockUpdate(array, 0, array.Length);
				this.mHandshakeHash.BlockUpdate(body, 0, body.Length);
			}
			return message;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000D45A8 File Offset: 0x000D45A8
		private void WriteMessage(DtlsReliableHandshake.Message message)
		{
			int sendLimit = this.mRecordLayer.GetSendLimit();
			int num = sendLimit - 12;
			if (num < 1)
			{
				throw new TlsFatalAlert(80);
			}
			int num2 = message.Body.Length;
			int num3 = 0;
			do
			{
				int num4 = Math.Min(num2 - num3, num);
				this.WriteHandshakeFragment(message, num3, num4);
				num3 += num4;
			}
			while (num3 < num2);
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000D4604 File Offset: 0x000D4604
		private void WriteHandshakeFragment(DtlsReliableHandshake.Message message, int fragment_offset, int fragment_length)
		{
			DtlsReliableHandshake.RecordLayerBuffer recordLayerBuffer = new DtlsReliableHandshake.RecordLayerBuffer(12 + fragment_length);
			TlsUtilities.WriteUint8(message.Type, recordLayerBuffer);
			TlsUtilities.WriteUint24(message.Body.Length, recordLayerBuffer);
			TlsUtilities.WriteUint16(message.Seq, recordLayerBuffer);
			TlsUtilities.WriteUint24(fragment_offset, recordLayerBuffer);
			TlsUtilities.WriteUint24(fragment_length, recordLayerBuffer);
			recordLayerBuffer.Write(message.Body, fragment_offset, fragment_length);
			recordLayerBuffer.SendToRecordLayer(this.mRecordLayer);
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000D4670 File Offset: 0x000D4670
		private static bool CheckAll(IDictionary inboundFlight)
		{
			foreach (object obj in inboundFlight.Values)
			{
				DtlsReassembler dtlsReassembler = (DtlsReassembler)obj;
				if (dtlsReassembler.GetBodyIfComplete() == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000D46E0 File Offset: 0x000D46E0
		private static void ResetAll(IDictionary inboundFlight)
		{
			foreach (object obj in inboundFlight.Values)
			{
				DtlsReassembler dtlsReassembler = (DtlsReassembler)obj;
				dtlsReassembler.Reset();
			}
		}

		// Token: 0x04001945 RID: 6469
		private const int MaxReceiveAhead = 16;

		// Token: 0x04001946 RID: 6470
		private const int MessageHeaderLength = 12;

		// Token: 0x04001947 RID: 6471
		private readonly DtlsRecordLayer mRecordLayer;

		// Token: 0x04001948 RID: 6472
		private TlsHandshakeHash mHandshakeHash;

		// Token: 0x04001949 RID: 6473
		private IDictionary mCurrentInboundFlight = Platform.CreateHashtable();

		// Token: 0x0400194A RID: 6474
		private IDictionary mPreviousInboundFlight = null;

		// Token: 0x0400194B RID: 6475
		private IList mOutboundFlight = Platform.CreateArrayList();

		// Token: 0x0400194C RID: 6476
		private bool mSending = true;

		// Token: 0x0400194D RID: 6477
		private int mMessageSeq = 0;

		// Token: 0x0400194E RID: 6478
		private int mNextReceiveSeq = 0;

		// Token: 0x02000E1C RID: 3612
		internal class Message
		{
			// Token: 0x06008C49 RID: 35913 RVA: 0x002A22EC File Offset: 0x002A22EC
			internal Message(int message_seq, byte msg_type, byte[] body)
			{
				this.mMessageSeq = message_seq;
				this.mMsgType = msg_type;
				this.mBody = body;
			}

			// Token: 0x17001D7D RID: 7549
			// (get) Token: 0x06008C4A RID: 35914 RVA: 0x002A230C File Offset: 0x002A230C
			public int Seq
			{
				get
				{
					return this.mMessageSeq;
				}
			}

			// Token: 0x17001D7E RID: 7550
			// (get) Token: 0x06008C4B RID: 35915 RVA: 0x002A2314 File Offset: 0x002A2314
			public byte Type
			{
				get
				{
					return this.mMsgType;
				}
			}

			// Token: 0x17001D7F RID: 7551
			// (get) Token: 0x06008C4C RID: 35916 RVA: 0x002A231C File Offset: 0x002A231C
			public byte[] Body
			{
				get
				{
					return this.mBody;
				}
			}

			// Token: 0x04004170 RID: 16752
			private readonly int mMessageSeq;

			// Token: 0x04004171 RID: 16753
			private readonly byte mMsgType;

			// Token: 0x04004172 RID: 16754
			private readonly byte[] mBody;
		}

		// Token: 0x02000E1D RID: 3613
		internal class RecordLayerBuffer : MemoryStream
		{
			// Token: 0x06008C4D RID: 35917 RVA: 0x002A2324 File Offset: 0x002A2324
			internal RecordLayerBuffer(int size) : base(size)
			{
			}

			// Token: 0x06008C4E RID: 35918 RVA: 0x002A2330 File Offset: 0x002A2330
			internal void SendToRecordLayer(DtlsRecordLayer recordLayer)
			{
				byte[] buffer = this.GetBuffer();
				int len = (int)this.Length;
				recordLayer.Send(buffer, 0, len);
				Platform.Dispose(this);
			}
		}

		// Token: 0x02000E1E RID: 3614
		internal class Retransmit : DtlsHandshakeRetransmit
		{
			// Token: 0x06008C4F RID: 35919 RVA: 0x002A2360 File Offset: 0x002A2360
			internal Retransmit(DtlsReliableHandshake outer)
			{
				this.mOuter = outer;
			}

			// Token: 0x06008C50 RID: 35920 RVA: 0x002A2370 File Offset: 0x002A2370
			public void ReceivedHandshakeRecord(int epoch, byte[] buf, int off, int len)
			{
				this.mOuter.ProcessRecord(0, epoch, buf, off, len);
			}

			// Token: 0x04004173 RID: 16755
			private readonly DtlsReliableHandshake mOuter;
		}
	}
}
