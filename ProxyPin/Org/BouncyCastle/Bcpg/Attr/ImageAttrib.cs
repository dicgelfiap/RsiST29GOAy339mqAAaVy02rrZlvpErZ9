using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg.Attr
{
	// Token: 0x02000281 RID: 641
	public class ImageAttrib : UserAttributeSubpacket
	{
		// Token: 0x0600144C RID: 5196 RVA: 0x0006D33C File Offset: 0x0006D33C
		public ImageAttrib(byte[] data) : this(false, data)
		{
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0006D348 File Offset: 0x0006D348
		public ImageAttrib(bool forceLongLength, byte[] data) : base(UserAttributeSubpacketTag.ImageAttribute, forceLongLength, data)
		{
			this.hdrLength = ((int)(data[1] & byte.MaxValue) << 8 | (int)(data[0] & byte.MaxValue));
			this._version = (int)(data[2] & byte.MaxValue);
			this._encoding = (int)(data[3] & byte.MaxValue);
			this.imageData = new byte[data.Length - this.hdrLength];
			Array.Copy(data, this.hdrLength, this.imageData, 0, this.imageData.Length);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0006D3CC File Offset: 0x0006D3CC
		public ImageAttrib(ImageAttrib.Format imageType, byte[] imageData) : this(ImageAttrib.ToByteArray(imageType, imageData))
		{
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0006D3DC File Offset: 0x0006D3DC
		private static byte[] ToByteArray(ImageAttrib.Format imageType, byte[] imageData)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(16);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(1);
			memoryStream.WriteByte((byte)imageType);
			memoryStream.Write(ImageAttrib.Zeroes, 0, ImageAttrib.Zeroes.Length);
			memoryStream.Write(imageData, 0, imageData.Length);
			return memoryStream.ToArray();
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0006D434 File Offset: 0x0006D434
		public virtual int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0006D43C File Offset: 0x0006D43C
		public virtual int Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0006D444 File Offset: 0x0006D444
		public virtual byte[] GetImageData()
		{
			return this.imageData;
		}

		// Token: 0x04000DCD RID: 3533
		private static readonly byte[] Zeroes = new byte[12];

		// Token: 0x04000DCE RID: 3534
		private int hdrLength;

		// Token: 0x04000DCF RID: 3535
		private int _version;

		// Token: 0x04000DD0 RID: 3536
		private int _encoding;

		// Token: 0x04000DD1 RID: 3537
		private byte[] imageData;

		// Token: 0x02000DD7 RID: 3543
		public enum Format : byte
		{
			// Token: 0x04004067 RID: 16487
			Jpeg = 1
		}
	}
}
