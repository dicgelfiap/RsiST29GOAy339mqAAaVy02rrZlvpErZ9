using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x020006F4 RID: 1780
	public sealed class JZlib
	{
		// Token: 0x06003E10 RID: 15888 RVA: 0x00157688 File Offset: 0x00157688
		public static string version()
		{
			return "1.0.7";
		}

		// Token: 0x04002003 RID: 8195
		private const string _version = "1.0.7";

		// Token: 0x04002004 RID: 8196
		public const int Z_NO_COMPRESSION = 0;

		// Token: 0x04002005 RID: 8197
		public const int Z_BEST_SPEED = 1;

		// Token: 0x04002006 RID: 8198
		public const int Z_BEST_COMPRESSION = 9;

		// Token: 0x04002007 RID: 8199
		public const int Z_DEFAULT_COMPRESSION = -1;

		// Token: 0x04002008 RID: 8200
		public const int Z_FILTERED = 1;

		// Token: 0x04002009 RID: 8201
		public const int Z_HUFFMAN_ONLY = 2;

		// Token: 0x0400200A RID: 8202
		public const int Z_DEFAULT_STRATEGY = 0;

		// Token: 0x0400200B RID: 8203
		public const int Z_NO_FLUSH = 0;

		// Token: 0x0400200C RID: 8204
		public const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x0400200D RID: 8205
		public const int Z_SYNC_FLUSH = 2;

		// Token: 0x0400200E RID: 8206
		public const int Z_FULL_FLUSH = 3;

		// Token: 0x0400200F RID: 8207
		public const int Z_FINISH = 4;

		// Token: 0x04002010 RID: 8208
		public const int Z_OK = 0;

		// Token: 0x04002011 RID: 8209
		public const int Z_STREAM_END = 1;

		// Token: 0x04002012 RID: 8210
		public const int Z_NEED_DICT = 2;

		// Token: 0x04002013 RID: 8211
		public const int Z_ERRNO = -1;

		// Token: 0x04002014 RID: 8212
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x04002015 RID: 8213
		public const int Z_DATA_ERROR = -3;

		// Token: 0x04002016 RID: 8214
		public const int Z_MEM_ERROR = -4;

		// Token: 0x04002017 RID: 8215
		public const int Z_BUF_ERROR = -5;

		// Token: 0x04002018 RID: 8216
		public const int Z_VERSION_ERROR = -6;
	}
}
