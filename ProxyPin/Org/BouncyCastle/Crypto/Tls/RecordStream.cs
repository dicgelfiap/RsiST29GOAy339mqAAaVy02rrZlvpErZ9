using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000515 RID: 1301
	internal class RecordStream
	{
		// Token: 0x0600279C RID: 10140 RVA: 0x000D61EC File Offset: 0x000D61EC
		internal RecordStream(TlsProtocol handler, Stream input, Stream output)
		{
			this.mHandler = handler;
			this.mInput = input;
			this.mOutput = output;
			this.mReadCompression = new TlsNullCompression();
			this.mWriteCompression = this.mReadCompression;
			this.mHandshakeHashUpdater = new RecordStream.HandshakeHashUpdateStream(this);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000D62A4 File Offset: 0x000D62A4
		internal virtual void Init(TlsContext context)
		{
			this.mReadCipher = new TlsNullCipher(context);
			this.mWriteCipher = this.mReadCipher;
			this.mHandshakeHash = new DeferredHash();
			this.mHandshakeHash.Init(context);
			this.SetPlaintextLimit(16384);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000D62E0 File Offset: 0x000D62E0
		internal virtual int GetPlaintextLimit()
		{
			return this.mPlaintextLimit;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000D62E8 File Offset: 0x000D62E8
		internal virtual void SetPlaintextLimit(int plaintextLimit)
		{
			this.mPlaintextLimit = plaintextLimit;
			this.mCompressedLimit = this.mPlaintextLimit + 1024;
			this.mCiphertextLimit = this.mCompressedLimit + 1024;
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000D6318 File Offset: 0x000D6318
		// (set) Token: 0x060027A1 RID: 10145 RVA: 0x000D6320 File Offset: 0x000D6320
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

		// Token: 0x060027A2 RID: 10146 RVA: 0x000D632C File Offset: 0x000D632C
		internal virtual void SetWriteVersion(ProtocolVersion writeVersion)
		{
			this.mWriteVersion = writeVersion;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000D6338 File Offset: 0x000D6338
		internal virtual void SetRestrictReadVersion(bool enabled)
		{
			this.mRestrictReadVersion = enabled;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000D6344 File Offset: 0x000D6344
		internal virtual void SetPendingConnectionState(TlsCompression tlsCompression, TlsCipher tlsCipher)
		{
			this.mPendingCompression = tlsCompression;
			this.mPendingCipher = tlsCipher;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000D6354 File Offset: 0x000D6354
		internal virtual void SentWriteCipherSpec()
		{
			if (this.mPendingCompression == null || this.mPendingCipher == null)
			{
				throw new TlsFatalAlert(40);
			}
			this.mWriteCompression = this.mPendingCompression;
			this.mWriteCipher = this.mPendingCipher;
			this.mWriteSeqNo = new RecordStream.SequenceNumber();
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000D63A8 File Offset: 0x000D63A8
		internal virtual void ReceivedReadCipherSpec()
		{
			if (this.mPendingCompression == null || this.mPendingCipher == null)
			{
				throw new TlsFatalAlert(40);
			}
			this.mReadCompression = this.mPendingCompression;
			this.mReadCipher = this.mPendingCipher;
			this.mReadSeqNo = new RecordStream.SequenceNumber();
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000D63FC File Offset: 0x000D63FC
		internal virtual void FinaliseHandshake()
		{
			if (this.mReadCompression != this.mPendingCompression || this.mWriteCompression != this.mPendingCompression || this.mReadCipher != this.mPendingCipher || this.mWriteCipher != this.mPendingCipher)
			{
				throw new TlsFatalAlert(40);
			}
			this.mPendingCompression = null;
			this.mPendingCipher = null;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000D6468 File Offset: 0x000D6468
		internal virtual void CheckRecordHeader(byte[] recordHeader)
		{
			byte type = TlsUtilities.ReadUint8(recordHeader, 0);
			RecordStream.CheckType(type, 10);
			if (!this.mRestrictReadVersion)
			{
				int num = TlsUtilities.ReadVersionRaw(recordHeader, 1);
				if (((long)num & (long)((ulong)-256)) != 768L)
				{
					throw new TlsFatalAlert(47);
				}
			}
			else
			{
				ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(recordHeader, 1);
				if (this.mReadVersion != null && !protocolVersion.Equals(this.mReadVersion))
				{
					throw new TlsFatalAlert(47);
				}
			}
			int length = TlsUtilities.ReadUint16(recordHeader, 3);
			RecordStream.CheckLength(length, this.mCiphertextLimit, 22);
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000D64FC File Offset: 0x000D64FC
		internal virtual bool ReadRecord()
		{
			byte[] array = TlsUtilities.ReadAllOrNothing(5, this.mInput);
			if (array == null)
			{
				return false;
			}
			byte b = TlsUtilities.ReadUint8(array, 0);
			RecordStream.CheckType(b, 10);
			if (!this.mRestrictReadVersion)
			{
				int num = TlsUtilities.ReadVersionRaw(array, 1);
				if (((long)num & (long)((ulong)-256)) != 768L)
				{
					throw new TlsFatalAlert(47);
				}
			}
			else
			{
				ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(array, 1);
				if (this.mReadVersion == null)
				{
					this.mReadVersion = protocolVersion;
				}
				else if (!protocolVersion.Equals(this.mReadVersion))
				{
					throw new TlsFatalAlert(47);
				}
			}
			int num2 = TlsUtilities.ReadUint16(array, 3);
			RecordStream.CheckLength(num2, this.mCiphertextLimit, 22);
			byte[] array2 = this.DecodeAndVerify(b, this.mInput, num2);
			this.mHandler.ProcessRecord(b, array2, 0, array2.Length);
			return true;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000D65D8 File Offset: 0x000D65D8
		internal virtual byte[] DecodeAndVerify(byte type, Stream input, int len)
		{
			byte[] array = TlsUtilities.ReadFully(len, input);
			long seqNo = this.mReadSeqNo.NextValue(10);
			byte[] array2 = this.mReadCipher.DecodeCiphertext(seqNo, type, array, 0, array.Length);
			RecordStream.CheckLength(array2.Length, this.mCompressedLimit, 22);
			Stream stream = this.mReadCompression.Decompress(this.mBuffer);
			if (stream != this.mBuffer)
			{
				stream.Write(array2, 0, array2.Length);
				stream.Flush();
				array2 = this.GetBufferContents();
			}
			RecordStream.CheckLength(array2.Length, this.mPlaintextLimit, 30);
			if (array2.Length < 1 && type != 23)
			{
				throw new TlsFatalAlert(47);
			}
			return array2;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000D6684 File Offset: 0x000D6684
		internal virtual void WriteRecord(byte type, byte[] plaintext, int plaintextOffset, int plaintextLength)
		{
			if (this.mWriteVersion == null)
			{
				return;
			}
			RecordStream.CheckType(type, 80);
			RecordStream.CheckLength(plaintextLength, this.mPlaintextLimit, 80);
			if (plaintextLength < 1 && type != 23)
			{
				throw new TlsFatalAlert(80);
			}
			Stream stream = this.mWriteCompression.Compress(this.mBuffer);
			long seqNo = this.mWriteSeqNo.NextValue(80);
			byte[] array;
			if (stream == this.mBuffer)
			{
				array = this.mWriteCipher.EncodePlaintext(seqNo, type, plaintext, plaintextOffset, plaintextLength);
			}
			else
			{
				stream.Write(plaintext, plaintextOffset, plaintextLength);
				stream.Flush();
				byte[] bufferContents = this.GetBufferContents();
				RecordStream.CheckLength(bufferContents.Length, plaintextLength + 1024, 80);
				array = this.mWriteCipher.EncodePlaintext(seqNo, type, bufferContents, 0, bufferContents.Length);
			}
			RecordStream.CheckLength(array.Length, this.mCiphertextLimit, 80);
			byte[] array2 = new byte[array.Length + 5];
			TlsUtilities.WriteUint8(type, array2, 0);
			TlsUtilities.WriteVersion(this.mWriteVersion, array2, 1);
			TlsUtilities.WriteUint16(array.Length, array2, 3);
			Array.Copy(array, 0, array2, 5, array.Length);
			this.mOutput.Write(array2, 0, array2.Length);
			this.mOutput.Flush();
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000D67B8 File Offset: 0x000D67B8
		internal virtual void NotifyHelloComplete()
		{
			this.mHandshakeHash = this.mHandshakeHash.NotifyPrfDetermined();
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x000D67CC File Offset: 0x000D67CC
		internal virtual TlsHandshakeHash HandshakeHash
		{
			get
			{
				return this.mHandshakeHash;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x000D67D4 File Offset: 0x000D67D4
		internal virtual Stream HandshakeHashUpdater
		{
			get
			{
				return this.mHandshakeHashUpdater;
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000D67DC File Offset: 0x000D67DC
		internal virtual TlsHandshakeHash PrepareToFinish()
		{
			TlsHandshakeHash result = this.mHandshakeHash;
			this.mHandshakeHash = this.mHandshakeHash.StopTracking();
			return result;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000D6808 File Offset: 0x000D6808
		internal virtual void SafeClose()
		{
			try
			{
				Platform.Dispose(this.mInput);
			}
			catch (IOException)
			{
			}
			try
			{
				Platform.Dispose(this.mOutput);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000D6860 File Offset: 0x000D6860
		internal virtual void Flush()
		{
			this.mOutput.Flush();
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000D6870 File Offset: 0x000D6870
		private byte[] GetBufferContents()
		{
			byte[] result = this.mBuffer.ToArray();
			this.mBuffer.SetLength(0L);
			return result;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000D689C File Offset: 0x000D689C
		private static void CheckType(byte type, byte alertDescription)
		{
			switch (type)
			{
			case 20:
			case 21:
			case 22:
			case 23:
				return;
			default:
				throw new TlsFatalAlert(alertDescription);
			}
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000D68D0 File Offset: 0x000D68D0
		private static void CheckLength(int length, int limit, byte alertDescription)
		{
			if (length > limit)
			{
				throw new TlsFatalAlert(alertDescription);
			}
		}

		// Token: 0x04001A15 RID: 6677
		private const int DEFAULT_PLAINTEXT_LIMIT = 16384;

		// Token: 0x04001A16 RID: 6678
		internal const int TLS_HEADER_SIZE = 5;

		// Token: 0x04001A17 RID: 6679
		internal const int TLS_HEADER_TYPE_OFFSET = 0;

		// Token: 0x04001A18 RID: 6680
		internal const int TLS_HEADER_VERSION_OFFSET = 1;

		// Token: 0x04001A19 RID: 6681
		internal const int TLS_HEADER_LENGTH_OFFSET = 3;

		// Token: 0x04001A1A RID: 6682
		private TlsProtocol mHandler;

		// Token: 0x04001A1B RID: 6683
		private Stream mInput;

		// Token: 0x04001A1C RID: 6684
		private Stream mOutput;

		// Token: 0x04001A1D RID: 6685
		private TlsCompression mPendingCompression = null;

		// Token: 0x04001A1E RID: 6686
		private TlsCompression mReadCompression = null;

		// Token: 0x04001A1F RID: 6687
		private TlsCompression mWriteCompression = null;

		// Token: 0x04001A20 RID: 6688
		private TlsCipher mPendingCipher = null;

		// Token: 0x04001A21 RID: 6689
		private TlsCipher mReadCipher = null;

		// Token: 0x04001A22 RID: 6690
		private TlsCipher mWriteCipher = null;

		// Token: 0x04001A23 RID: 6691
		private RecordStream.SequenceNumber mReadSeqNo = new RecordStream.SequenceNumber();

		// Token: 0x04001A24 RID: 6692
		private RecordStream.SequenceNumber mWriteSeqNo = new RecordStream.SequenceNumber();

		// Token: 0x04001A25 RID: 6693
		private MemoryStream mBuffer = new MemoryStream();

		// Token: 0x04001A26 RID: 6694
		private TlsHandshakeHash mHandshakeHash = null;

		// Token: 0x04001A27 RID: 6695
		private readonly BaseOutputStream mHandshakeHashUpdater;

		// Token: 0x04001A28 RID: 6696
		private ProtocolVersion mReadVersion = null;

		// Token: 0x04001A29 RID: 6697
		private ProtocolVersion mWriteVersion = null;

		// Token: 0x04001A2A RID: 6698
		private bool mRestrictReadVersion = true;

		// Token: 0x04001A2B RID: 6699
		private int mPlaintextLimit;

		// Token: 0x04001A2C RID: 6700
		private int mCompressedLimit;

		// Token: 0x04001A2D RID: 6701
		private int mCiphertextLimit;

		// Token: 0x02000E21 RID: 3617
		private class HandshakeHashUpdateStream : BaseOutputStream
		{
			// Token: 0x06008C54 RID: 35924 RVA: 0x002A2458 File Offset: 0x002A2458
			public HandshakeHashUpdateStream(RecordStream mOuter)
			{
				this.mOuter = mOuter;
			}

			// Token: 0x06008C55 RID: 35925 RVA: 0x002A2468 File Offset: 0x002A2468
			public override void Write(byte[] buf, int off, int len)
			{
				this.mOuter.mHandshakeHash.BlockUpdate(buf, off, len);
			}

			// Token: 0x04004186 RID: 16774
			private readonly RecordStream mOuter;
		}

		// Token: 0x02000E22 RID: 3618
		private class SequenceNumber
		{
			// Token: 0x06008C56 RID: 35926 RVA: 0x002A2480 File Offset: 0x002A2480
			internal long NextValue(byte alertDescription)
			{
				if (this.exhausted)
				{
					throw new TlsFatalAlert(alertDescription);
				}
				long result = this.value;
				if ((this.value += 1L) == 0L)
				{
					this.exhausted = true;
				}
				return result;
			}

			// Token: 0x04004187 RID: 16775
			private long value = 0L;

			// Token: 0x04004188 RID: 16776
			private bool exhausted = false;
		}
	}
}
