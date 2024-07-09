using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200064B RID: 1611
	public abstract class PgpEncryptedData
	{
		// Token: 0x06003801 RID: 14337 RVA: 0x0012D5C0 File Offset: 0x0012D5C0
		internal PgpEncryptedData(InputStreamPacket encData)
		{
			this.encData = encData;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x0012D5D0 File Offset: 0x0012D5D0
		public virtual Stream GetInputStream()
		{
			return this.encData.GetInputStream();
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x0012D5E0 File Offset: 0x0012D5E0
		public bool IsIntegrityProtected()
		{
			return this.encData is SymmetricEncIntegrityPacket;
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x0012D5F0 File Offset: 0x0012D5F0
		public bool Verify()
		{
			if (!this.IsIntegrityProtected())
			{
				throw new PgpException("data not integrity protected.");
			}
			DigestStream digestStream = (DigestStream)this.encStream;
			while (this.encStream.ReadByte() >= 0)
			{
			}
			byte[] lookAhead = this.truncStream.GetLookAhead();
			IDigest digest = digestStream.ReadDigest();
			digest.BlockUpdate(lookAhead, 0, 2);
			byte[] array = DigestUtilities.DoFinal(digest);
			byte[] array2 = new byte[array.Length];
			Array.Copy(lookAhead, 2, array2, 0, array2.Length);
			return Arrays.ConstantTimeAreEqual(array, array2);
		}

		// Token: 0x04001D9F RID: 7583
		internal InputStreamPacket encData;

		// Token: 0x04001DA0 RID: 7584
		internal Stream encStream;

		// Token: 0x04001DA1 RID: 7585
		internal PgpEncryptedData.TruncatedStream truncStream;

		// Token: 0x02000E60 RID: 3680
		internal class TruncatedStream : BaseInputStream
		{
			// Token: 0x06008D4D RID: 36173 RVA: 0x002A60A8 File Offset: 0x002A60A8
			internal TruncatedStream(Stream inStr)
			{
				int num = Streams.ReadFully(inStr, this.lookAhead, 0, this.lookAhead.Length);
				if (num < 22)
				{
					throw new EndOfStreamException();
				}
				this.inStr = inStr;
				this.bufStart = 0;
				this.bufEnd = num - 22;
			}

			// Token: 0x06008D4E RID: 36174 RVA: 0x002A610C File Offset: 0x002A610C
			private int FillBuffer()
			{
				if (this.bufEnd < 490)
				{
					return 0;
				}
				Array.Copy(this.lookAhead, 490, this.lookAhead, 0, 22);
				this.bufEnd = Streams.ReadFully(this.inStr, this.lookAhead, 22, 490);
				this.bufStart = 0;
				return this.bufEnd;
			}

			// Token: 0x06008D4F RID: 36175 RVA: 0x002A6174 File Offset: 0x002A6174
			public override int ReadByte()
			{
				if (this.bufStart < this.bufEnd)
				{
					return (int)this.lookAhead[this.bufStart++];
				}
				if (this.FillBuffer() < 1)
				{
					return -1;
				}
				return (int)this.lookAhead[this.bufStart++];
			}

			// Token: 0x06008D50 RID: 36176 RVA: 0x002A61D8 File Offset: 0x002A61D8
			public override int Read(byte[] buf, int off, int len)
			{
				int num = this.bufEnd - this.bufStart;
				int num2 = off;
				while (len > num)
				{
					Array.Copy(this.lookAhead, this.bufStart, buf, num2, num);
					this.bufStart += num;
					num2 += num;
					len -= num;
					if ((num = this.FillBuffer()) < 1)
					{
						return num2 - off;
					}
				}
				Array.Copy(this.lookAhead, this.bufStart, buf, num2, len);
				this.bufStart += len;
				return num2 + len - off;
			}

			// Token: 0x06008D51 RID: 36177 RVA: 0x002A6268 File Offset: 0x002A6268
			internal byte[] GetLookAhead()
			{
				byte[] array = new byte[22];
				Array.Copy(this.lookAhead, this.bufStart, array, 0, 22);
				return array;
			}

			// Token: 0x04004237 RID: 16951
			private const int LookAheadSize = 22;

			// Token: 0x04004238 RID: 16952
			private const int LookAheadBufSize = 512;

			// Token: 0x04004239 RID: 16953
			private const int LookAheadBufLimit = 490;

			// Token: 0x0400423A RID: 16954
			private readonly Stream inStr;

			// Token: 0x0400423B RID: 16955
			private readonly byte[] lookAhead = new byte[512];

			// Token: 0x0400423C RID: 16956
			private int bufStart;

			// Token: 0x0400423D RID: 16957
			private int bufEnd;
		}
	}
}
