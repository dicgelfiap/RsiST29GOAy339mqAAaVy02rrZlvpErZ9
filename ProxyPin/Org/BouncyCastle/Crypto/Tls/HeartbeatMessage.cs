using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000507 RID: 1287
	public class HeartbeatMessage
	{
		// Token: 0x06002761 RID: 10081 RVA: 0x000D587C File Offset: 0x000D587C
		public HeartbeatMessage(byte type, byte[] payload, int paddingLength)
		{
			if (!HeartbeatMessageType.IsValid(type))
			{
				throw new ArgumentException("not a valid HeartbeatMessageType value", "type");
			}
			if (payload == null || payload.Length >= 65536)
			{
				throw new ArgumentException("must have length < 2^16", "payload");
			}
			if (paddingLength < 16)
			{
				throw new ArgumentException("must be at least 16", "paddingLength");
			}
			this.mType = type;
			this.mPayload = payload;
			this.mPaddingLength = paddingLength;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000D5900 File Offset: 0x000D5900
		public virtual void Encode(TlsContext context, Stream output)
		{
			TlsUtilities.WriteUint8(this.mType, output);
			TlsUtilities.CheckUint16(this.mPayload.Length);
			TlsUtilities.WriteUint16(this.mPayload.Length, output);
			output.Write(this.mPayload, 0, this.mPayload.Length);
			byte[] array = new byte[this.mPaddingLength];
			context.NonceRandomGenerator.NextBytes(array);
			output.Write(array, 0, array.Length);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000D5970 File Offset: 0x000D5970
		public static HeartbeatMessage Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (!HeartbeatMessageType.IsValid(b))
			{
				throw new TlsFatalAlert(47);
			}
			int payloadLength = TlsUtilities.ReadUint16(input);
			HeartbeatMessage.PayloadBuffer payloadBuffer = new HeartbeatMessage.PayloadBuffer();
			Streams.PipeAll(input, payloadBuffer);
			byte[] array = payloadBuffer.ToTruncatedByteArray(payloadLength);
			if (array == null)
			{
				return null;
			}
			TlsUtilities.CheckUint16(payloadBuffer.Length);
			int paddingLength = (int)payloadBuffer.Length - array.Length;
			return new HeartbeatMessage(b, array, paddingLength);
		}

		// Token: 0x040019B8 RID: 6584
		protected readonly byte mType;

		// Token: 0x040019B9 RID: 6585
		protected readonly byte[] mPayload;

		// Token: 0x040019BA RID: 6586
		protected readonly int mPaddingLength;

		// Token: 0x02000E20 RID: 3616
		internal class PayloadBuffer : MemoryStream
		{
			// Token: 0x06008C52 RID: 35922 RVA: 0x002A241C File Offset: 0x002A241C
			internal byte[] ToTruncatedByteArray(int payloadLength)
			{
				int num = payloadLength + 16;
				if (this.Length < (long)num)
				{
					return null;
				}
				byte[] buffer = this.GetBuffer();
				return Arrays.CopyOf(buffer, payloadLength);
			}
		}
	}
}
