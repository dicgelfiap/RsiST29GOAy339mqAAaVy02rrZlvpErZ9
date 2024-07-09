using System;
using System.IO;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004F8 RID: 1272
	internal class DtlsRecordLayer : DatagramTransport, TlsCloseable
	{
		// Token: 0x06002705 RID: 9989 RVA: 0x000D3648 File Offset: 0x000D3648
		internal DtlsRecordLayer(DatagramTransport transport, TlsContext context, TlsPeer peer, byte contentType)
		{
			this.mTransport = transport;
			this.mContext = context;
			this.mPeer = peer;
			this.mInHandshake = true;
			this.mCurrentEpoch = new DtlsEpoch(0, new TlsNullCipher(context));
			this.mPendingEpoch = null;
			this.mReadEpoch = this.mCurrentEpoch;
			this.mWriteEpoch = this.mCurrentEpoch;
			this.SetPlaintextLimit(16384);
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x000D3700 File Offset: 0x000D3700
		internal bool IsClosed
		{
			get
			{
				return this.mClosed;
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000D370C File Offset: 0x000D370C
		internal virtual void SetPlaintextLimit(int plaintextLimit)
		{
			this.mPlaintextLimit = plaintextLimit;
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x000D3718 File Offset: 0x000D3718
		internal virtual int ReadEpoch
		{
			get
			{
				return this.mReadEpoch.Epoch;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x000D3728 File Offset: 0x000D3728
		// (set) Token: 0x0600270A RID: 9994 RVA: 0x000D3734 File Offset: 0x000D3734
		internal virtual ProtocolVersion ReadVersion
		{
			get
			{
				return this.mReadVersion;
			}
			set
			{
				this.mReadVersion = value;
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000D3740 File Offset: 0x000D3740
		internal virtual void SetWriteVersion(ProtocolVersion writeVersion)
		{
			this.mWriteVersion = writeVersion;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000D374C File Offset: 0x000D374C
		internal virtual void InitPendingEpoch(TlsCipher pendingCipher)
		{
			if (this.mPendingEpoch != null)
			{
				throw new InvalidOperationException();
			}
			this.mPendingEpoch = new DtlsEpoch(this.mWriteEpoch.Epoch + 1, pendingCipher);
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000D3778 File Offset: 0x000D3778
		internal virtual void HandshakeSuccessful(DtlsHandshakeRetransmit retransmit)
		{
			if (this.mReadEpoch == this.mCurrentEpoch || this.mWriteEpoch == this.mCurrentEpoch)
			{
				throw new InvalidOperationException();
			}
			if (retransmit != null)
			{
				this.mRetransmit = retransmit;
				this.mRetransmitEpoch = this.mCurrentEpoch;
				this.mRetransmitExpiry = DateTimeUtilities.CurrentUnixMs() + 240000L;
			}
			this.mInHandshake = false;
			this.mCurrentEpoch = this.mPendingEpoch;
			this.mPendingEpoch = null;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000D37F8 File Offset: 0x000D37F8
		internal virtual void ResetWriteEpoch()
		{
			if (this.mRetransmitEpoch != null)
			{
				this.mWriteEpoch = this.mRetransmitEpoch;
				return;
			}
			this.mWriteEpoch = this.mCurrentEpoch;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000D3820 File Offset: 0x000D3820
		public virtual int GetReceiveLimit()
		{
			return Math.Min(this.mPlaintextLimit, this.mReadEpoch.Cipher.GetPlaintextLimit(this.mTransport.GetReceiveLimit() - 13));
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000D385C File Offset: 0x000D385C
		public virtual int GetSendLimit()
		{
			return Math.Min(this.mPlaintextLimit, this.mWriteEpoch.Cipher.GetPlaintextLimit(this.mTransport.GetSendLimit() - 13));
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000D3898 File Offset: 0x000D3898
		public virtual int Receive(byte[] buf, int off, int len, int waitMillis)
		{
			byte[] array = null;
			int result;
			for (;;)
			{
				int num = Math.Min(len, this.GetReceiveLimit()) + 13;
				if (array == null || array.Length < num)
				{
					array = new byte[num];
				}
				try
				{
					if (this.mRetransmit != null && DateTimeUtilities.CurrentUnixMs() > this.mRetransmitExpiry)
					{
						this.mRetransmit = null;
						this.mRetransmitEpoch = null;
					}
					int num2 = this.ReceiveRecord(array, 0, num, waitMillis);
					if (num2 < 0)
					{
						result = num2;
					}
					else
					{
						if (num2 < 13)
						{
							continue;
						}
						int num3 = TlsUtilities.ReadUint16(array, 11);
						if (num2 != num3 + 13)
						{
							continue;
						}
						byte b = TlsUtilities.ReadUint8(array, 0);
						switch (b)
						{
						case 20:
						case 21:
						case 22:
						case 23:
						case 24:
						{
							int num4 = TlsUtilities.ReadUint16(array, 3);
							DtlsEpoch dtlsEpoch = null;
							if (num4 == this.mReadEpoch.Epoch)
							{
								dtlsEpoch = this.mReadEpoch;
							}
							else if (b == 22 && this.mRetransmitEpoch != null && num4 == this.mRetransmitEpoch.Epoch)
							{
								dtlsEpoch = this.mRetransmitEpoch;
							}
							if (dtlsEpoch == null)
							{
								continue;
							}
							long num5 = TlsUtilities.ReadUint48(array, 5);
							if (dtlsEpoch.ReplayWindow.ShouldDiscard(num5))
							{
								continue;
							}
							ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(array, 1);
							if (!protocolVersion.IsDtls)
							{
								continue;
							}
							if (this.mReadVersion != null && !this.mReadVersion.Equals(protocolVersion))
							{
								continue;
							}
							byte[] array2 = dtlsEpoch.Cipher.DecodeCiphertext(DtlsRecordLayer.GetMacSequenceNumber(dtlsEpoch.Epoch, num5), b, array, 13, num2 - 13);
							dtlsEpoch.ReplayWindow.ReportAuthenticated(num5);
							if (array2.Length > this.mPlaintextLimit)
							{
								continue;
							}
							if (this.mReadVersion == null)
							{
								this.mReadVersion = protocolVersion;
							}
							switch (b)
							{
							case 20:
								for (int i = 0; i < array2.Length; i++)
								{
									byte b2 = TlsUtilities.ReadUint8(array2, i);
									if (b2 == 1 && this.mPendingEpoch != null)
									{
										this.mReadEpoch = this.mPendingEpoch;
									}
								}
								continue;
							case 21:
								if (array2.Length == 2)
								{
									byte b3 = array2[0];
									byte b4 = array2[1];
									this.mPeer.NotifyAlertReceived(b3, b4);
									if (b3 == 2)
									{
										this.Failed();
										throw new TlsFatalAlert(b4);
									}
									if (b4 == 0)
									{
										this.CloseTransport();
									}
								}
								continue;
							case 22:
								if (!this.mInHandshake)
								{
									if (this.mRetransmit != null)
									{
										this.mRetransmit.ReceivedHandshakeRecord(num4, array2, 0, array2.Length);
									}
									continue;
								}
								break;
							case 23:
								if (this.mInHandshake)
								{
									continue;
								}
								break;
							case 24:
								continue;
							}
							if (!this.mInHandshake && this.mRetransmit != null)
							{
								this.mRetransmit = null;
								this.mRetransmitEpoch = null;
							}
							Array.Copy(array2, 0, buf, off, array2.Length);
							result = array2.Length;
							break;
						}
						default:
							continue;
						}
					}
				}
				catch (IOException ex)
				{
					throw ex;
				}
				break;
			}
			return result;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000D3BEC File Offset: 0x000D3BEC
		public virtual void Send(byte[] buf, int off, int len)
		{
			byte contentType = 23;
			if (this.mInHandshake || this.mWriteEpoch == this.mRetransmitEpoch)
			{
				contentType = 22;
				byte b = TlsUtilities.ReadUint8(buf, off);
				if (b == 20)
				{
					DtlsEpoch dtlsEpoch = null;
					if (this.mInHandshake)
					{
						dtlsEpoch = this.mPendingEpoch;
					}
					else if (this.mWriteEpoch == this.mRetransmitEpoch)
					{
						dtlsEpoch = this.mCurrentEpoch;
					}
					if (dtlsEpoch == null)
					{
						throw new InvalidOperationException();
					}
					byte[] array = new byte[]
					{
						1
					};
					this.SendRecord(20, array, 0, array.Length);
					this.mWriteEpoch = dtlsEpoch;
				}
			}
			this.SendRecord(contentType, buf, off, len);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000D3CA0 File Offset: 0x000D3CA0
		public virtual void Close()
		{
			if (!this.mClosed)
			{
				if (this.mInHandshake)
				{
					this.Warn(90, "User canceled handshake");
				}
				this.CloseTransport();
			}
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000D3CD0 File Offset: 0x000D3CD0
		internal virtual void Failed()
		{
			if (!this.mClosed)
			{
				this.mFailed = true;
				this.CloseTransport();
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000D3CF0 File Offset: 0x000D3CF0
		internal virtual void Fail(byte alertDescription)
		{
			if (!this.mClosed)
			{
				try
				{
					this.RaiseAlert(2, alertDescription, null, null);
				}
				catch (Exception)
				{
				}
				this.mFailed = true;
				this.CloseTransport();
			}
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000D3D40 File Offset: 0x000D3D40
		internal virtual void Warn(byte alertDescription, string message)
		{
			this.RaiseAlert(1, alertDescription, message, null);
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000D3D4C File Offset: 0x000D3D4C
		private void CloseTransport()
		{
			if (!this.mClosed)
			{
				try
				{
					if (!this.mFailed)
					{
						this.Warn(0, null);
					}
					this.mTransport.Close();
				}
				catch (Exception)
				{
				}
				this.mClosed = true;
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000D3DAC File Offset: 0x000D3DAC
		private void RaiseAlert(byte alertLevel, byte alertDescription, string message, Exception cause)
		{
			this.mPeer.NotifyAlertRaised(alertLevel, alertDescription, message, cause);
			this.SendRecord(21, new byte[]
			{
				alertLevel,
				alertDescription
			}, 0, 2);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000D3DE8 File Offset: 0x000D3DE8
		private int ReceiveRecord(byte[] buf, int off, int len, int waitMillis)
		{
			if (this.mRecordQueue.Available > 0)
			{
				int num = 0;
				if (this.mRecordQueue.Available >= 13)
				{
					byte[] buf2 = new byte[2];
					this.mRecordQueue.Read(buf2, 0, 2, 11);
					num = TlsUtilities.ReadUint16(buf2, 0);
				}
				int num2 = Math.Min(this.mRecordQueue.Available, 13 + num);
				this.mRecordQueue.RemoveData(buf, off, num2, 0);
				return num2;
			}
			int num3 = this.mTransport.Receive(buf, off, len, waitMillis);
			if (num3 >= 13)
			{
				int num4 = TlsUtilities.ReadUint16(buf, off + 11);
				int num5 = 13 + num4;
				if (num3 > num5)
				{
					this.mRecordQueue.AddData(buf, off + num5, num3 - num5);
					num3 = num5;
				}
			}
			return num3;
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000D3EB0 File Offset: 0x000D3EB0
		private void SendRecord(byte contentType, byte[] buf, int off, int len)
		{
			if (this.mWriteVersion == null)
			{
				return;
			}
			if (len > this.mPlaintextLimit)
			{
				throw new TlsFatalAlert(80);
			}
			if (len < 1 && contentType != 23)
			{
				throw new TlsFatalAlert(80);
			}
			int epoch = this.mWriteEpoch.Epoch;
			long num = this.mWriteEpoch.AllocateSequenceNumber();
			byte[] array = this.mWriteEpoch.Cipher.EncodePlaintext(DtlsRecordLayer.GetMacSequenceNumber(epoch, num), contentType, buf, off, len);
			byte[] array2 = new byte[array.Length + 13];
			TlsUtilities.WriteUint8(contentType, array2, 0);
			ProtocolVersion version = this.mWriteVersion;
			TlsUtilities.WriteVersion(version, array2, 1);
			TlsUtilities.WriteUint16(epoch, array2, 3);
			TlsUtilities.WriteUint48(num, array2, 5);
			TlsUtilities.WriteUint16(array.Length, array2, 11);
			Array.Copy(array, 0, array2, 13, array.Length);
			this.mTransport.Send(array2, 0, array2.Length);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000D3F94 File Offset: 0x000D3F94
		private static long GetMacSequenceNumber(int epoch, long sequence_number)
		{
			return ((long)epoch & (long)((ulong)-1)) << 48 | sequence_number;
		}

		// Token: 0x04001930 RID: 6448
		private const int RECORD_HEADER_LENGTH = 13;

		// Token: 0x04001931 RID: 6449
		private const int MAX_FRAGMENT_LENGTH = 16384;

		// Token: 0x04001932 RID: 6450
		private const long TCP_MSL = 120000L;

		// Token: 0x04001933 RID: 6451
		private const long RETRANSMIT_TIMEOUT = 240000L;

		// Token: 0x04001934 RID: 6452
		private readonly DatagramTransport mTransport;

		// Token: 0x04001935 RID: 6453
		private readonly TlsContext mContext;

		// Token: 0x04001936 RID: 6454
		private readonly TlsPeer mPeer;

		// Token: 0x04001937 RID: 6455
		private readonly ByteQueue mRecordQueue = new ByteQueue();

		// Token: 0x04001938 RID: 6456
		private volatile bool mClosed = false;

		// Token: 0x04001939 RID: 6457
		private volatile bool mFailed = false;

		// Token: 0x0400193A RID: 6458
		private volatile ProtocolVersion mReadVersion = null;

		// Token: 0x0400193B RID: 6459
		private volatile ProtocolVersion mWriteVersion = null;

		// Token: 0x0400193C RID: 6460
		private volatile bool mInHandshake;

		// Token: 0x0400193D RID: 6461
		private volatile int mPlaintextLimit;

		// Token: 0x0400193E RID: 6462
		private DtlsEpoch mCurrentEpoch;

		// Token: 0x0400193F RID: 6463
		private DtlsEpoch mPendingEpoch;

		// Token: 0x04001940 RID: 6464
		private DtlsEpoch mReadEpoch;

		// Token: 0x04001941 RID: 6465
		private DtlsEpoch mWriteEpoch;

		// Token: 0x04001942 RID: 6466
		private DtlsHandshakeRetransmit mRetransmit = null;

		// Token: 0x04001943 RID: 6467
		private DtlsEpoch mRetransmitEpoch = null;

		// Token: 0x04001944 RID: 6468
		private long mRetransmitExpiry = 0L;
	}
}
