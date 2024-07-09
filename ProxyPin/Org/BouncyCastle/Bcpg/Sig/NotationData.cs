using System;
using System.IO;
using System.Text;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000289 RID: 649
	public class NotationData : SignatureSubpacket
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x0006D9F0 File Offset: 0x0006D9F0
		public NotationData(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.NotationData, critical, isLongLength, data)
		{
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0006DA00 File Offset: 0x0006DA00
		public NotationData(bool critical, bool humanReadable, string notationName, string notationValue) : base(SignatureSubpacketTag.NotationData, critical, false, NotationData.CreateData(humanReadable, notationName, notationValue))
		{
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0006DA18 File Offset: 0x0006DA18
		private static byte[] CreateData(bool humanReadable, string notationName, string notationValue)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(humanReadable ? 128 : 0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			byte[] bytes = Encoding.UTF8.GetBytes(notationName);
			int num = Math.Min(bytes.Length, 255);
			byte[] bytes2 = Encoding.UTF8.GetBytes(notationValue);
			int num2 = Math.Min(bytes2.Length, 255);
			memoryStream.WriteByte((byte)(num >> 8));
			memoryStream.WriteByte((byte)num);
			memoryStream.WriteByte((byte)(num2 >> 8));
			memoryStream.WriteByte((byte)num2);
			memoryStream.Write(bytes, 0, num);
			memoryStream.Write(bytes2, 0, num2);
			return memoryStream.ToArray();
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0006DAD4 File Offset: 0x0006DAD4
		public bool IsHumanReadable
		{
			get
			{
				return this.data[0] == 128;
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0006DAE8 File Offset: 0x0006DAE8
		public string GetNotationName()
		{
			int count = ((int)this.data[4] << 8) + (int)this.data[5];
			int index = 8;
			return Encoding.UTF8.GetString(this.data, index, count);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0006DB24 File Offset: 0x0006DB24
		public string GetNotationValue()
		{
			int num = ((int)this.data[4] << 8) + (int)this.data[5];
			int count = ((int)this.data[6] << 8) + (int)this.data[7];
			int index = 8 + num;
			return Encoding.UTF8.GetString(this.data, index, count);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0006DB74 File Offset: 0x0006DB74
		public byte[] GetNotationValueBytes()
		{
			int num = ((int)this.data[4] << 8) + (int)this.data[5];
			int num2 = ((int)this.data[6] << 8) + (int)this.data[7];
			int sourceIndex = 8 + num;
			byte[] array = new byte[num2];
			Array.Copy(this.data, sourceIndex, array, 0, num2);
			return array;
		}

		// Token: 0x04000DDE RID: 3550
		public const int HeaderFlagLength = 4;

		// Token: 0x04000DDF RID: 3551
		public const int HeaderNameLength = 2;

		// Token: 0x04000DE0 RID: 3552
		public const int HeaderValueLength = 2;
	}
}
