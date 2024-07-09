using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200052C RID: 1324
	public abstract class TlsProtocol : TlsCloseable
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x000D8264 File Offset: 0x000D8264
		public TlsProtocol(Stream stream, SecureRandom secureRandom) : this(stream, stream, secureRandom)
		{
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000D8270 File Offset: 0x000D8270
		public TlsProtocol(Stream input, Stream output, SecureRandom secureRandom)
		{
			this.mApplicationDataQueue = new ByteQueue(0);
			this.mAlertQueue = new ByteQueue(2);
			this.mHandshakeQueue = new ByteQueue(0);
			this.mTlsStream = null;
			this.mClosed = false;
			this.mFailedWithError = false;
			this.mAppDataReady = false;
			this.mAppDataSplitEnabled = true;
			this.mAppDataSplitMode = 0;
			this.mExpectedVerifyData = null;
			this.mTlsSession = null;
			this.mSessionParameters = null;
			this.mSecurityParameters = null;
			this.mPeerCertificate = null;
			this.mOfferedCipherSuites = null;
			this.mOfferedCompressionMethods = null;
			this.mClientExtensions = null;
			this.mServerExtensions = null;
			this.mConnectionState = 0;
			this.mResumedSession = false;
			this.mReceivedChangeCipherSpec = false;
			this.mSecureRenegotiation = false;
			this.mAllowCertificateStatus = false;
			this.mExpectSessionTicket = false;
			this.mBlocking = true;
			this.mInputBuffers = null;
			this.mOutputBuffer = null;
			base..ctor();
			this.mRecordStream = new RecordStream(this, input, output);
			this.mSecureRandom = secureRandom;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000D8374 File Offset: 0x000D8374
		public TlsProtocol(SecureRandom secureRandom)
		{
			this.mApplicationDataQueue = new ByteQueue(0);
			this.mAlertQueue = new ByteQueue(2);
			this.mHandshakeQueue = new ByteQueue(0);
			this.mTlsStream = null;
			this.mClosed = false;
			this.mFailedWithError = false;
			this.mAppDataReady = false;
			this.mAppDataSplitEnabled = true;
			this.mAppDataSplitMode = 0;
			this.mExpectedVerifyData = null;
			this.mTlsSession = null;
			this.mSessionParameters = null;
			this.mSecurityParameters = null;
			this.mPeerCertificate = null;
			this.mOfferedCipherSuites = null;
			this.mOfferedCompressionMethods = null;
			this.mClientExtensions = null;
			this.mServerExtensions = null;
			this.mConnectionState = 0;
			this.mResumedSession = false;
			this.mReceivedChangeCipherSpec = false;
			this.mSecureRenegotiation = false;
			this.mAllowCertificateStatus = false;
			this.mExpectSessionTicket = false;
			this.mBlocking = true;
			this.mInputBuffers = null;
			this.mOutputBuffer = null;
			base..ctor();
			this.mBlocking = false;
			this.mInputBuffers = new ByteQueueStream();
			this.mOutputBuffer = new ByteQueueStream();
			this.mRecordStream = new RecordStream(this, this.mInputBuffers, this.mOutputBuffer);
			this.mSecureRandom = secureRandom;
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600282C RID: 10284
		protected abstract TlsContext Context { get; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600282D RID: 10285
		internal abstract AbstractTlsContext ContextAdmin { get; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600282E RID: 10286
		protected abstract TlsPeer Peer { get; }

		// Token: 0x0600282F RID: 10287 RVA: 0x000D84A0 File Offset: 0x000D84A0
		protected virtual void HandleAlertMessage(byte alertLevel, byte alertDescription)
		{
			this.Peer.NotifyAlertReceived(alertLevel, alertDescription);
			if (alertLevel == 1)
			{
				this.HandleAlertWarningMessage(alertDescription);
				return;
			}
			this.HandleFailure();
			throw new TlsFatalAlertReceived(alertDescription);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000D84CC File Offset: 0x000D84CC
		protected virtual void HandleAlertWarningMessage(byte alertDescription)
		{
			if (alertDescription == 0)
			{
				if (!this.mAppDataReady)
				{
					throw new TlsFatalAlert(40);
				}
				this.HandleClose(false);
			}
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000D84F0 File Offset: 0x000D84F0
		protected virtual void HandleChangeCipherSpecMessage()
		{
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000D84F4 File Offset: 0x000D84F4
		protected virtual void HandleClose(bool user_canceled)
		{
			if (!this.mClosed)
			{
				this.mClosed = true;
				if (user_canceled && !this.mAppDataReady)
				{
					this.RaiseAlertWarning(90, "User canceled handshake");
				}
				this.RaiseAlertWarning(0, "Connection closed");
				this.mRecordStream.SafeClose();
				if (!this.mAppDataReady)
				{
					this.CleanupHandshake();
				}
			}
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000D8568 File Offset: 0x000D8568
		protected virtual void HandleException(byte alertDescription, string message, Exception cause)
		{
			if (!this.mClosed)
			{
				this.RaiseAlertFatal(alertDescription, message, cause);
				this.HandleFailure();
			}
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000D8588 File Offset: 0x000D8588
		protected virtual void HandleFailure()
		{
			this.mClosed = true;
			this.mFailedWithError = true;
			this.InvalidateSession();
			this.mRecordStream.SafeClose();
			if (!this.mAppDataReady)
			{
				this.CleanupHandshake();
			}
		}

		// Token: 0x06002835 RID: 10293
		protected abstract void HandleHandshakeMessage(byte type, MemoryStream buf);

		// Token: 0x06002836 RID: 10294 RVA: 0x000D85C0 File Offset: 0x000D85C0
		protected virtual void ApplyMaxFragmentLengthExtension()
		{
			if (this.mSecurityParameters.maxFragmentLength >= 0)
			{
				if (!MaxFragmentLength.IsValid((byte)this.mSecurityParameters.maxFragmentLength))
				{
					throw new TlsFatalAlert(80);
				}
				int plaintextLimit = 1 << (int)(8 + this.mSecurityParameters.maxFragmentLength);
				this.mRecordStream.SetPlaintextLimit(plaintextLimit);
			}
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000D8620 File Offset: 0x000D8620
		protected virtual void CheckReceivedChangeCipherSpec(bool expected)
		{
			if (expected != this.mReceivedChangeCipherSpec)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000D8638 File Offset: 0x000D8638
		protected virtual void CleanupHandshake()
		{
			if (this.mExpectedVerifyData != null)
			{
				Arrays.Fill(this.mExpectedVerifyData, 0);
				this.mExpectedVerifyData = null;
			}
			this.mSecurityParameters.Clear();
			this.mPeerCertificate = null;
			this.mOfferedCipherSuites = null;
			this.mOfferedCompressionMethods = null;
			this.mClientExtensions = null;
			this.mServerExtensions = null;
			this.mResumedSession = false;
			this.mReceivedChangeCipherSpec = false;
			this.mSecureRenegotiation = false;
			this.mAllowCertificateStatus = false;
			this.mExpectSessionTicket = false;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000D86B8 File Offset: 0x000D86B8
		protected virtual void BlockForHandshake()
		{
			if (this.mBlocking)
			{
				while (this.mConnectionState != 16)
				{
					if (this.mClosed)
					{
						throw new TlsFatalAlert(80);
					}
					this.SafeReadRecord();
				}
			}
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000D86F0 File Offset: 0x000D86F0
		protected virtual void CompleteHandshake()
		{
			try
			{
				this.mConnectionState = 16;
				this.mAlertQueue.Shrink();
				this.mHandshakeQueue.Shrink();
				this.mRecordStream.FinaliseHandshake();
				this.mAppDataSplitEnabled = !TlsUtilities.IsTlsV11(this.Context);
				if (!this.mAppDataReady)
				{
					this.mAppDataReady = true;
					if (this.mBlocking)
					{
						this.mTlsStream = new TlsStream(this);
					}
				}
				if (this.mTlsSession != null)
				{
					if (this.mSessionParameters == null)
					{
						this.mSessionParameters = new SessionParameters.Builder().SetCipherSuite(this.mSecurityParameters.CipherSuite).SetCompressionAlgorithm(this.mSecurityParameters.CompressionAlgorithm).SetExtendedMasterSecret(this.mSecurityParameters.IsExtendedMasterSecret).SetMasterSecret(this.mSecurityParameters.MasterSecret).SetPeerCertificate(this.mPeerCertificate).SetPskIdentity(this.mSecurityParameters.PskIdentity).SetSrpIdentity(this.mSecurityParameters.SrpIdentity).SetServerExtensions(this.mServerExtensions).Build();
						this.mTlsSession = new TlsSessionImpl(this.mTlsSession.SessionID, this.mSessionParameters);
					}
					this.ContextAdmin.SetResumableSession(this.mTlsSession);
				}
				this.Peer.NotifyHandshakeComplete();
			}
			finally
			{
				this.CleanupHandshake();
			}
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000D8868 File Offset: 0x000D8868
		protected internal void ProcessRecord(byte protocol, byte[] buf, int off, int len)
		{
			switch (protocol)
			{
			case 20:
				this.ProcessChangeCipherSpec(buf, off, len);
				return;
			case 21:
				this.mAlertQueue.AddData(buf, off, len);
				this.ProcessAlertQueue();
				return;
			case 22:
			{
				if (this.mHandshakeQueue.Available > 0)
				{
					this.mHandshakeQueue.AddData(buf, off, len);
					this.ProcessHandshakeQueue(this.mHandshakeQueue);
					return;
				}
				ByteQueue byteQueue = new ByteQueue(buf, off, len);
				this.ProcessHandshakeQueue(byteQueue);
				int available = byteQueue.Available;
				if (available > 0)
				{
					this.mHandshakeQueue.AddData(buf, off + len - available, available);
					return;
				}
				return;
			}
			case 23:
				if (!this.mAppDataReady)
				{
					throw new TlsFatalAlert(10);
				}
				this.mApplicationDataQueue.AddData(buf, off, len);
				this.ProcessApplicationDataQueue();
				return;
			default:
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000D8950 File Offset: 0x000D8950
		private void ProcessHandshakeQueue(ByteQueue queue)
		{
			while (queue.Available >= 4)
			{
				byte[] buf = new byte[4];
				queue.Read(buf, 0, 4, 0);
				byte b = TlsUtilities.ReadUint8(buf, 0);
				int num = TlsUtilities.ReadUint24(buf, 1);
				int num2 = 4 + num;
				if (queue.Available < num2)
				{
					return;
				}
				if (b != 0)
				{
					if (20 == b)
					{
						this.CheckReceivedChangeCipherSpec(true);
						TlsContext context = this.Context;
						if (this.mExpectedVerifyData == null && context.SecurityParameters.MasterSecret != null)
						{
							this.mExpectedVerifyData = this.CreateVerifyData(!context.IsServer);
						}
					}
					else
					{
						this.CheckReceivedChangeCipherSpec(this.mConnectionState == 16);
					}
					queue.CopyTo(this.mRecordStream.HandshakeHashUpdater, num2);
				}
				queue.RemoveData(4);
				MemoryStream buf2 = queue.ReadFrom(num);
				this.HandleHandshakeMessage(b, buf2);
			}
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000D8A34 File Offset: 0x000D8A34
		private void ProcessApplicationDataQueue()
		{
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000D8A38 File Offset: 0x000D8A38
		private void ProcessAlertQueue()
		{
			while (this.mAlertQueue.Available >= 2)
			{
				byte[] array = this.mAlertQueue.RemoveData(2, 0);
				byte alertLevel = array[0];
				byte alertDescription = array[1];
				this.HandleAlertMessage(alertLevel, alertDescription);
			}
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000D8A7C File Offset: 0x000D8A7C
		private void ProcessChangeCipherSpec(byte[] buf, int off, int len)
		{
			for (int i = 0; i < len; i++)
			{
				byte b = TlsUtilities.ReadUint8(buf, off + i);
				if (b != 1)
				{
					throw new TlsFatalAlert(50);
				}
				if (this.mReceivedChangeCipherSpec || this.mAlertQueue.Available > 0 || this.mHandshakeQueue.Available > 0)
				{
					throw new TlsFatalAlert(10);
				}
				this.mRecordStream.ReceivedReadCipherSpec();
				this.mReceivedChangeCipherSpec = true;
				this.HandleChangeCipherSpecMessage();
			}
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000D8B04 File Offset: 0x000D8B04
		protected internal virtual int ApplicationDataAvailable()
		{
			return this.mApplicationDataQueue.Available;
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000D8B14 File Offset: 0x000D8B14
		protected internal virtual int ReadApplicationData(byte[] buf, int offset, int len)
		{
			if (len < 1)
			{
				return 0;
			}
			while (this.mApplicationDataQueue.Available == 0)
			{
				if (this.mClosed)
				{
					if (this.mFailedWithError)
					{
						throw new IOException("Cannot read application data on failed TLS connection");
					}
					if (!this.mAppDataReady)
					{
						throw new InvalidOperationException("Cannot read application data until initial handshake completed.");
					}
					return 0;
				}
				else
				{
					this.SafeReadRecord();
				}
			}
			len = Math.Min(len, this.mApplicationDataQueue.Available);
			this.mApplicationDataQueue.RemoveData(buf, offset, len, 0);
			return len;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000D8BA4 File Offset: 0x000D8BA4
		protected virtual void SafeCheckRecordHeader(byte[] recordHeader)
		{
			try
			{
				this.mRecordStream.CheckRecordHeader(recordHeader);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.HandleException(tlsFatalAlert.AlertDescription, "Failed to read record", tlsFatalAlert);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.HandleException(80, "Failed to read record", ex);
				throw ex;
			}
			catch (Exception ex2)
			{
				this.HandleException(80, "Failed to read record", ex2);
				throw new TlsFatalAlert(80, ex2);
			}
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000D8C28 File Offset: 0x000D8C28
		protected virtual void SafeReadRecord()
		{
			try
			{
				if (this.mRecordStream.ReadRecord())
				{
					return;
				}
				if (!this.mAppDataReady)
				{
					throw new TlsFatalAlert(40);
				}
			}
			catch (TlsFatalAlertReceived tlsFatalAlertReceived)
			{
				throw tlsFatalAlertReceived;
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.HandleException(tlsFatalAlert.AlertDescription, "Failed to read record", tlsFatalAlert);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.HandleException(80, "Failed to read record", ex);
				throw ex;
			}
			catch (Exception ex2)
			{
				this.HandleException(80, "Failed to read record", ex2);
				throw new TlsFatalAlert(80, ex2);
			}
			this.HandleFailure();
			throw new TlsNoCloseNotifyException();
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000D8CE8 File Offset: 0x000D8CE8
		protected virtual void SafeWriteRecord(byte type, byte[] buf, int offset, int len)
		{
			try
			{
				this.mRecordStream.WriteRecord(type, buf, offset, len);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.HandleException(tlsFatalAlert.AlertDescription, "Failed to write record", tlsFatalAlert);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.HandleException(80, "Failed to write record", ex);
				throw ex;
			}
			catch (Exception ex2)
			{
				this.HandleException(80, "Failed to write record", ex2);
				throw new TlsFatalAlert(80, ex2);
			}
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000D8D70 File Offset: 0x000D8D70
		protected internal virtual void WriteData(byte[] buf, int offset, int len)
		{
			if (this.mClosed)
			{
				throw new IOException("Cannot write application data on closed/failed TLS connection");
			}
			while (len > 0)
			{
				if (this.mAppDataSplitEnabled)
				{
					switch (this.mAppDataSplitMode)
					{
					case 1:
						this.SafeWriteRecord(23, TlsUtilities.EmptyBytes, 0, 0);
						goto IL_8B;
					case 2:
						this.mAppDataSplitEnabled = false;
						this.SafeWriteRecord(23, TlsUtilities.EmptyBytes, 0, 0);
						goto IL_8B;
					}
					this.SafeWriteRecord(23, buf, offset, 1);
					offset++;
					len--;
				}
				IL_8B:
				if (len > 0)
				{
					int num = Math.Min(len, this.mRecordStream.GetPlaintextLimit());
					this.SafeWriteRecord(23, buf, offset, num);
					offset += num;
					len -= num;
				}
			}
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000D8E44 File Offset: 0x000D8E44
		protected virtual void SetAppDataSplitMode(int appDataSplitMode)
		{
			if (appDataSplitMode < 0 || appDataSplitMode > 2)
			{
				throw new ArgumentException("Illegal appDataSplitMode mode: " + appDataSplitMode, "appDataSplitMode");
			}
			this.mAppDataSplitMode = appDataSplitMode;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000D8E78 File Offset: 0x000D8E78
		protected virtual void WriteHandshakeMessage(byte[] buf, int off, int len)
		{
			if (len < 4)
			{
				throw new TlsFatalAlert(80);
			}
			byte b = TlsUtilities.ReadUint8(buf, off);
			if (b != 0)
			{
				this.mRecordStream.HandshakeHashUpdater.Write(buf, off, len);
			}
			int num = 0;
			do
			{
				int num2 = Math.Min(len - num, this.mRecordStream.GetPlaintextLimit());
				this.SafeWriteRecord(22, buf, off + num, num2);
				num += num2;
			}
			while (num < len);
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x000D8EE4 File Offset: 0x000D8EE4
		public virtual Stream Stream
		{
			get
			{
				if (!this.mBlocking)
				{
					throw new InvalidOperationException("Cannot use Stream in non-blocking mode! Use OfferInput()/OfferOutput() instead.");
				}
				return this.mTlsStream;
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000D8F04 File Offset: 0x000D8F04
		public virtual void CloseInput()
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use CloseInput() in blocking mode!");
			}
			if (this.mClosed)
			{
				return;
			}
			if (this.mInputBuffers.Available > 0)
			{
				throw new EndOfStreamException();
			}
			if (!this.mAppDataReady)
			{
				throw new TlsFatalAlert(40);
			}
			throw new TlsNoCloseNotifyException();
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000D8F6C File Offset: 0x000D8F6C
		public virtual void OfferInput(byte[] input)
		{
			this.OfferInput(input, 0, input.Length);
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000D8F7C File Offset: 0x000D8F7C
		public virtual void OfferInput(byte[] input, int inputOff, int inputLen)
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use OfferInput() in blocking mode! Use Stream instead.");
			}
			if (this.mClosed)
			{
				throw new IOException("Connection is closed, cannot accept any more input");
			}
			this.mInputBuffers.Write(input, inputOff, inputLen);
			while (this.mInputBuffers.Available >= 5)
			{
				byte[] array = new byte[5];
				this.mInputBuffers.Peek(array);
				int num = TlsUtilities.ReadUint16(array, 3) + 5;
				if (this.mInputBuffers.Available < num)
				{
					this.SafeCheckRecordHeader(array);
					return;
				}
				this.SafeReadRecord();
				if (this.mClosed)
				{
					if (this.mConnectionState != 16)
					{
						throw new TlsFatalAlert(80);
					}
					break;
				}
			}
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000D903C File Offset: 0x000D903C
		public virtual int GetAvailableInputBytes()
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use GetAvailableInputBytes() in blocking mode! Use ApplicationDataAvailable() instead.");
			}
			return this.ApplicationDataAvailable();
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000D905C File Offset: 0x000D905C
		public virtual int ReadInput(byte[] buffer, int offset, int length)
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use ReadInput() in blocking mode! Use Stream instead.");
			}
			return this.ReadApplicationData(buffer, offset, Math.Min(length, this.ApplicationDataAvailable()));
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000D9088 File Offset: 0x000D9088
		public virtual void OfferOutput(byte[] buffer, int offset, int length)
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use OfferOutput() in blocking mode! Use Stream instead.");
			}
			if (!this.mAppDataReady)
			{
				throw new IOException("Application data cannot be sent until the handshake is complete!");
			}
			this.WriteData(buffer, offset, length);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000D90C4 File Offset: 0x000D90C4
		public virtual int GetAvailableOutputBytes()
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use GetAvailableOutputBytes() in blocking mode! Use Stream instead.");
			}
			return this.mOutputBuffer.Available;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000D90E8 File Offset: 0x000D90E8
		public virtual int ReadOutput(byte[] buffer, int offset, int length)
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use ReadOutput() in blocking mode! Use Stream instead.");
			}
			return this.mOutputBuffer.Read(buffer, offset, length);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000D9110 File Offset: 0x000D9110
		protected virtual void InvalidateSession()
		{
			if (this.mSessionParameters != null)
			{
				this.mSessionParameters.Clear();
				this.mSessionParameters = null;
			}
			if (this.mTlsSession != null)
			{
				this.mTlsSession.Invalidate();
				this.mTlsSession = null;
			}
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000D914C File Offset: 0x000D914C
		protected virtual void ProcessFinishedMessage(MemoryStream buf)
		{
			if (this.mExpectedVerifyData == null)
			{
				throw new TlsFatalAlert(80);
			}
			byte[] b = TlsUtilities.ReadFully(this.mExpectedVerifyData.Length, buf);
			TlsProtocol.AssertEmpty(buf);
			if (!Arrays.ConstantTimeAreEqual(this.mExpectedVerifyData, b))
			{
				throw new TlsFatalAlert(51);
			}
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000D91A0 File Offset: 0x000D91A0
		protected virtual void RaiseAlertFatal(byte alertDescription, string message, Exception cause)
		{
			this.Peer.NotifyAlertRaised(2, alertDescription, message, cause);
			byte[] plaintext = new byte[]
			{
				2,
				alertDescription
			};
			try
			{
				this.mRecordStream.WriteRecord(21, plaintext, 0, 2);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000D91F8 File Offset: 0x000D91F8
		protected virtual void RaiseAlertWarning(byte alertDescription, string message)
		{
			this.Peer.NotifyAlertRaised(1, alertDescription, message, null);
			byte[] buf = new byte[]
			{
				1,
				alertDescription
			};
			this.SafeWriteRecord(21, buf, 0, 2);
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000D9234 File Offset: 0x000D9234
		protected virtual void SendCertificateMessage(Certificate certificate)
		{
			if (certificate == null)
			{
				certificate = Certificate.EmptyChain;
			}
			if (certificate.IsEmpty)
			{
				TlsContext context = this.Context;
				if (!context.IsServer)
				{
					ProtocolVersion serverVersion = this.Context.ServerVersion;
					if (serverVersion.IsSsl)
					{
						string message = serverVersion.ToString() + " client didn't provide credentials";
						this.RaiseAlertWarning(41, message);
						return;
					}
				}
			}
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(11);
			certificate.Encode(handshakeMessage);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000D92B8 File Offset: 0x000D92B8
		protected virtual void SendChangeCipherSpecMessage()
		{
			byte[] array = new byte[]
			{
				1
			};
			this.SafeWriteRecord(20, array, 0, array.Length);
			this.mRecordStream.SentWriteCipherSpec();
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000D92F0 File Offset: 0x000D92F0
		protected virtual void SendFinishedMessage()
		{
			byte[] array = this.CreateVerifyData(this.Context.IsServer);
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(20, array.Length);
			handshakeMessage.Write(array, 0, array.Length);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000D9330 File Offset: 0x000D9330
		protected virtual void SendSupplementalDataMessage(IList supplementalData)
		{
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(23);
			TlsProtocol.WriteSupplementalData(handshakeMessage, supplementalData);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000D9358 File Offset: 0x000D9358
		protected virtual byte[] CreateVerifyData(bool isServer)
		{
			TlsContext context = this.Context;
			string asciiLabel = isServer ? "server finished" : "client finished";
			byte[] sslSender = isServer ? TlsUtilities.SSL_SERVER : TlsUtilities.SSL_CLIENT;
			byte[] currentPrfHash = TlsProtocol.GetCurrentPrfHash(context, this.mRecordStream.HandshakeHash, sslSender);
			return TlsUtilities.CalculateVerifyData(context, asciiLabel, currentPrfHash);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000D93B8 File Offset: 0x000D93B8
		public virtual void Close()
		{
			this.HandleClose(true);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000D93C4 File Offset: 0x000D93C4
		protected internal virtual void Flush()
		{
			this.mRecordStream.Flush();
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x000D93D4 File Offset: 0x000D93D4
		public virtual bool IsClosed
		{
			get
			{
				return this.mClosed;
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000D93E0 File Offset: 0x000D93E0
		protected virtual short ProcessMaxFragmentLengthExtension(IDictionary clientExtensions, IDictionary serverExtensions, byte alertDescription)
		{
			short maxFragmentLengthExtension = TlsExtensionsUtilities.GetMaxFragmentLengthExtension(serverExtensions);
			if (maxFragmentLengthExtension >= 0 && (!MaxFragmentLength.IsValid((byte)maxFragmentLengthExtension) || (!this.mResumedSession && maxFragmentLengthExtension != TlsExtensionsUtilities.GetMaxFragmentLengthExtension(clientExtensions))))
			{
				throw new TlsFatalAlert(alertDescription);
			}
			return maxFragmentLengthExtension;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000D942C File Offset: 0x000D942C
		protected virtual void RefuseRenegotiation()
		{
			if (TlsUtilities.IsSsl(this.Context))
			{
				throw new TlsFatalAlert(40);
			}
			this.RaiseAlertWarning(100, "Renegotiation not supported");
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000D9454 File Offset: 0x000D9454
		protected internal static void AssertEmpty(MemoryStream buf)
		{
			if (buf.Position < buf.Length)
			{
				throw new TlsFatalAlert(50);
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000D9470 File Offset: 0x000D9470
		protected internal static byte[] CreateRandomBlock(bool useGmtUnixTime, IRandomGenerator randomGenerator)
		{
			byte[] array = new byte[32];
			randomGenerator.NextBytes(array);
			if (useGmtUnixTime)
			{
				TlsUtilities.WriteGmtUnixTime(array, 0);
			}
			return array;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000D94A0 File Offset: 0x000D94A0
		protected internal static byte[] CreateRenegotiationInfo(byte[] renegotiated_connection)
		{
			return TlsUtilities.EncodeOpaque8(renegotiated_connection);
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000D94A8 File Offset: 0x000D94A8
		protected internal static void EstablishMasterSecret(TlsContext context, TlsKeyExchange keyExchange)
		{
			byte[] array = keyExchange.GeneratePremasterSecret();
			try
			{
				context.SecurityParameters.masterSecret = TlsUtilities.CalculateMasterSecret(context, array);
			}
			finally
			{
				if (array != null)
				{
					Arrays.Fill(array, 0);
				}
			}
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000D94F4 File Offset: 0x000D94F4
		protected internal static byte[] GetCurrentPrfHash(TlsContext context, TlsHandshakeHash handshakeHash, byte[] sslSender)
		{
			IDigest digest = handshakeHash.ForkPrfHash();
			if (sslSender != null && TlsUtilities.IsSsl(context))
			{
				digest.BlockUpdate(sslSender, 0, sslSender.Length);
			}
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000D9530 File Offset: 0x000D9530
		protected internal static IDictionary ReadExtensions(MemoryStream input)
		{
			if (input.Position >= input.Length)
			{
				return null;
			}
			byte[] buffer = TlsUtilities.ReadOpaque16(input);
			TlsProtocol.AssertEmpty(input);
			MemoryStream memoryStream = new MemoryStream(buffer, false);
			IDictionary dictionary = Platform.CreateHashtable();
			while (memoryStream.Position < memoryStream.Length)
			{
				int num = TlsUtilities.ReadUint16(memoryStream);
				byte[] value = TlsUtilities.ReadOpaque16(memoryStream);
				if (dictionary.Contains(num))
				{
					throw new TlsFatalAlert(47);
				}
				dictionary.Add(num, value);
			}
			return dictionary;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000D95BC File Offset: 0x000D95BC
		protected internal static IList ReadSupplementalDataMessage(MemoryStream input)
		{
			byte[] buffer = TlsUtilities.ReadOpaque24(input);
			TlsProtocol.AssertEmpty(input);
			MemoryStream memoryStream = new MemoryStream(buffer, false);
			IList list = Platform.CreateArrayList();
			while (memoryStream.Position < memoryStream.Length)
			{
				int dataType = TlsUtilities.ReadUint16(memoryStream);
				byte[] data = TlsUtilities.ReadOpaque16(memoryStream);
				list.Add(new SupplementalDataEntry(dataType, data));
			}
			return list;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000D961C File Offset: 0x000D961C
		protected internal static void WriteExtensions(Stream output, IDictionary extensions)
		{
			MemoryStream memoryStream = new MemoryStream();
			TlsProtocol.WriteSelectedExtensions(memoryStream, extensions, true);
			TlsProtocol.WriteSelectedExtensions(memoryStream, extensions, false);
			byte[] buf = memoryStream.ToArray();
			TlsUtilities.WriteOpaque16(buf, output);
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000D9654 File Offset: 0x000D9654
		protected internal static void WriteSelectedExtensions(Stream output, IDictionary extensions, bool selectEmpty)
		{
			foreach (object obj in extensions.Keys)
			{
				int num = (int)obj;
				byte[] array = (byte[])extensions[num];
				if (selectEmpty == (array.Length == 0))
				{
					TlsUtilities.CheckUint16(num);
					TlsUtilities.WriteUint16(num, output);
					TlsUtilities.WriteOpaque16(array, output);
				}
			}
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000D96E4 File Offset: 0x000D96E4
		protected internal static void WriteSupplementalData(Stream output, IList supplementalData)
		{
			MemoryStream memoryStream = new MemoryStream();
			foreach (object obj in supplementalData)
			{
				SupplementalDataEntry supplementalDataEntry = (SupplementalDataEntry)obj;
				int dataType = supplementalDataEntry.DataType;
				TlsUtilities.CheckUint16(dataType);
				TlsUtilities.WriteUint16(dataType, memoryStream);
				TlsUtilities.WriteOpaque16(supplementalDataEntry.Data, memoryStream);
			}
			byte[] buf = memoryStream.ToArray();
			TlsUtilities.WriteOpaque24(buf, output);
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000D9774 File Offset: 0x000D9774
		protected internal static int GetPrfAlgorithm(TlsContext context, int ciphersuite)
		{
			bool flag = TlsUtilities.IsTlsV12(context);
			if (ciphersuite <= 109)
			{
				switch (ciphersuite)
				{
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
					break;
				default:
					switch (ciphersuite)
					{
					case 103:
					case 104:
					case 105:
					case 106:
					case 107:
					case 108:
					case 109:
						break;
					default:
						goto IL_3A7;
					}
					break;
				}
			}
			else
			{
				switch (ciphersuite)
				{
				case 156:
				case 158:
				case 160:
				case 162:
				case 164:
				case 166:
				case 168:
				case 170:
				case 172:
				case 186:
				case 187:
				case 188:
				case 189:
				case 190:
				case 191:
				case 192:
				case 193:
				case 194:
				case 195:
				case 196:
				case 197:
					goto IL_37D;
				case 157:
				case 159:
				case 161:
				case 163:
				case 165:
				case 167:
				case 169:
				case 171:
				case 173:
					break;
				case 174:
				case 176:
				case 178:
				case 180:
				case 182:
				case 184:
					goto IL_3A7;
				case 175:
				case 177:
				case 179:
				case 181:
				case 183:
				case 185:
					goto IL_39D;
				default:
					switch (ciphersuite)
					{
					case 49187:
					case 49189:
					case 49191:
					case 49193:
					case 49195:
					case 49197:
					case 49199:
					case 49201:
					case 49266:
					case 49268:
					case 49270:
					case 49272:
					case 49274:
					case 49276:
					case 49278:
					case 49280:
					case 49282:
					case 49284:
					case 49286:
					case 49288:
					case 49290:
					case 49292:
					case 49294:
					case 49296:
					case 49298:
					case 49308:
					case 49309:
					case 49310:
					case 49311:
					case 49312:
					case 49313:
					case 49314:
					case 49315:
					case 49316:
					case 49317:
					case 49318:
					case 49319:
					case 49320:
					case 49321:
					case 49322:
					case 49323:
					case 49324:
					case 49325:
					case 49326:
					case 49327:
						goto IL_37D;
					case 49188:
					case 49190:
					case 49192:
					case 49194:
					case 49196:
					case 49198:
					case 49200:
					case 49202:
					case 49267:
					case 49269:
					case 49271:
					case 49273:
					case 49275:
					case 49277:
					case 49279:
					case 49281:
					case 49283:
					case 49285:
					case 49287:
					case 49289:
					case 49291:
					case 49293:
					case 49295:
					case 49297:
					case 49299:
						break;
					case 49203:
					case 49204:
					case 49205:
					case 49206:
					case 49207:
					case 49209:
					case 49210:
					case 49212:
					case 49213:
					case 49214:
					case 49215:
					case 49216:
					case 49217:
					case 49218:
					case 49219:
					case 49220:
					case 49221:
					case 49222:
					case 49223:
					case 49224:
					case 49225:
					case 49226:
					case 49227:
					case 49228:
					case 49229:
					case 49230:
					case 49231:
					case 49232:
					case 49233:
					case 49234:
					case 49235:
					case 49236:
					case 49237:
					case 49238:
					case 49239:
					case 49240:
					case 49241:
					case 49242:
					case 49243:
					case 49244:
					case 49245:
					case 49246:
					case 49247:
					case 49248:
					case 49249:
					case 49250:
					case 49251:
					case 49252:
					case 49253:
					case 49254:
					case 49255:
					case 49256:
					case 49257:
					case 49258:
					case 49259:
					case 49260:
					case 49261:
					case 49262:
					case 49263:
					case 49264:
					case 49265:
					case 49300:
					case 49302:
					case 49304:
					case 49306:
						goto IL_3A7;
					case 49208:
					case 49211:
					case 49301:
					case 49303:
					case 49305:
					case 49307:
						goto IL_39D;
					default:
						switch (ciphersuite)
						{
						case 52392:
						case 52393:
						case 52395:
						case 52396:
						case 52397:
						case 52398:
							goto IL_37D;
						case 52394:
							goto IL_3A7;
						default:
							goto IL_3A7;
						}
						break;
					}
					break;
				}
				if (flag)
				{
					return 2;
				}
				throw new TlsFatalAlert(47);
				IL_39D:
				if (flag)
				{
					return 2;
				}
				return 0;
			}
			IL_37D:
			if (flag)
			{
				return 1;
			}
			throw new TlsFatalAlert(47);
			IL_3A7:
			if (flag)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04001A7F RID: 6783
		protected const short CS_START = 0;

		// Token: 0x04001A80 RID: 6784
		protected const short CS_CLIENT_HELLO = 1;

		// Token: 0x04001A81 RID: 6785
		protected const short CS_SERVER_HELLO = 2;

		// Token: 0x04001A82 RID: 6786
		protected const short CS_SERVER_SUPPLEMENTAL_DATA = 3;

		// Token: 0x04001A83 RID: 6787
		protected const short CS_SERVER_CERTIFICATE = 4;

		// Token: 0x04001A84 RID: 6788
		protected const short CS_CERTIFICATE_STATUS = 5;

		// Token: 0x04001A85 RID: 6789
		protected const short CS_SERVER_KEY_EXCHANGE = 6;

		// Token: 0x04001A86 RID: 6790
		protected const short CS_CERTIFICATE_REQUEST = 7;

		// Token: 0x04001A87 RID: 6791
		protected const short CS_SERVER_HELLO_DONE = 8;

		// Token: 0x04001A88 RID: 6792
		protected const short CS_CLIENT_SUPPLEMENTAL_DATA = 9;

		// Token: 0x04001A89 RID: 6793
		protected const short CS_CLIENT_CERTIFICATE = 10;

		// Token: 0x04001A8A RID: 6794
		protected const short CS_CLIENT_KEY_EXCHANGE = 11;

		// Token: 0x04001A8B RID: 6795
		protected const short CS_CERTIFICATE_VERIFY = 12;

		// Token: 0x04001A8C RID: 6796
		protected const short CS_CLIENT_FINISHED = 13;

		// Token: 0x04001A8D RID: 6797
		protected const short CS_SERVER_SESSION_TICKET = 14;

		// Token: 0x04001A8E RID: 6798
		protected const short CS_SERVER_FINISHED = 15;

		// Token: 0x04001A8F RID: 6799
		protected const short CS_END = 16;

		// Token: 0x04001A90 RID: 6800
		protected const short ADS_MODE_1_Nsub1 = 0;

		// Token: 0x04001A91 RID: 6801
		protected const short ADS_MODE_0_N = 1;

		// Token: 0x04001A92 RID: 6802
		protected const short ADS_MODE_0_N_FIRSTONLY = 2;

		// Token: 0x04001A93 RID: 6803
		private ByteQueue mApplicationDataQueue;

		// Token: 0x04001A94 RID: 6804
		private ByteQueue mAlertQueue;

		// Token: 0x04001A95 RID: 6805
		private ByteQueue mHandshakeQueue;

		// Token: 0x04001A96 RID: 6806
		internal RecordStream mRecordStream;

		// Token: 0x04001A97 RID: 6807
		protected SecureRandom mSecureRandom;

		// Token: 0x04001A98 RID: 6808
		private TlsStream mTlsStream;

		// Token: 0x04001A99 RID: 6809
		private volatile bool mClosed;

		// Token: 0x04001A9A RID: 6810
		private volatile bool mFailedWithError;

		// Token: 0x04001A9B RID: 6811
		private volatile bool mAppDataReady;

		// Token: 0x04001A9C RID: 6812
		private volatile bool mAppDataSplitEnabled;

		// Token: 0x04001A9D RID: 6813
		private volatile int mAppDataSplitMode;

		// Token: 0x04001A9E RID: 6814
		private byte[] mExpectedVerifyData;

		// Token: 0x04001A9F RID: 6815
		protected TlsSession mTlsSession;

		// Token: 0x04001AA0 RID: 6816
		protected SessionParameters mSessionParameters;

		// Token: 0x04001AA1 RID: 6817
		protected SecurityParameters mSecurityParameters;

		// Token: 0x04001AA2 RID: 6818
		protected Certificate mPeerCertificate;

		// Token: 0x04001AA3 RID: 6819
		protected int[] mOfferedCipherSuites;

		// Token: 0x04001AA4 RID: 6820
		protected byte[] mOfferedCompressionMethods;

		// Token: 0x04001AA5 RID: 6821
		protected IDictionary mClientExtensions;

		// Token: 0x04001AA6 RID: 6822
		protected IDictionary mServerExtensions;

		// Token: 0x04001AA7 RID: 6823
		protected short mConnectionState;

		// Token: 0x04001AA8 RID: 6824
		protected bool mResumedSession;

		// Token: 0x04001AA9 RID: 6825
		protected bool mReceivedChangeCipherSpec;

		// Token: 0x04001AAA RID: 6826
		protected bool mSecureRenegotiation;

		// Token: 0x04001AAB RID: 6827
		protected bool mAllowCertificateStatus;

		// Token: 0x04001AAC RID: 6828
		protected bool mExpectSessionTicket;

		// Token: 0x04001AAD RID: 6829
		protected bool mBlocking;

		// Token: 0x04001AAE RID: 6830
		protected ByteQueueStream mInputBuffers;

		// Token: 0x04001AAF RID: 6831
		protected ByteQueueStream mOutputBuffer;

		// Token: 0x02000E25 RID: 3621
		internal class HandshakeMessage : MemoryStream
		{
			// Token: 0x06008C66 RID: 35942 RVA: 0x002A2694 File Offset: 0x002A2694
			internal HandshakeMessage(byte handshakeType) : this(handshakeType, 60)
			{
			}

			// Token: 0x06008C67 RID: 35943 RVA: 0x002A26A0 File Offset: 0x002A26A0
			internal HandshakeMessage(byte handshakeType, int length) : base(length + 4)
			{
				TlsUtilities.WriteUint8(handshakeType, this);
				TlsUtilities.WriteUint24(0, this);
			}

			// Token: 0x06008C68 RID: 35944 RVA: 0x002A26BC File Offset: 0x002A26BC
			internal void Write(byte[] data)
			{
				this.Write(data, 0, data.Length);
			}

			// Token: 0x06008C69 RID: 35945 RVA: 0x002A26CC File Offset: 0x002A26CC
			internal void WriteToRecordStream(TlsProtocol protocol)
			{
				long num = this.Length - 4L;
				TlsUtilities.CheckUint24(num);
				this.Position = 1L;
				TlsUtilities.WriteUint24((int)num, this);
				byte[] buffer = this.GetBuffer();
				int len = (int)this.Length;
				protocol.WriteHandshakeMessage(buffer, 0, len);
				Platform.Dispose(this);
			}
		}
	}
}
