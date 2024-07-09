using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000263 RID: 611
	internal class ConstructedOctetStream : BaseInputStream
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x0006A92C File Offset: 0x0006A92C
		internal ConstructedOctetStream(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0006A944 File Offset: 0x0006A944
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._currentStream == null)
			{
				if (!this._first)
				{
					return 0;
				}
				Asn1OctetStringParser nextParser = this.GetNextParser();
				if (nextParser == null)
				{
					return 0;
				}
				this._first = false;
				this._currentStream = nextParser.GetOctetStream();
			}
			int num = 0;
			for (;;)
			{
				int num2 = this._currentStream.Read(buffer, offset + num, count - num);
				if (num2 > 0)
				{
					num += num2;
					if (num == count)
					{
						break;
					}
				}
				else
				{
					Asn1OctetStringParser nextParser2 = this.GetNextParser();
					if (nextParser2 == null)
					{
						goto Block_6;
					}
					this._currentStream = nextParser2.GetOctetStream();
				}
			}
			return num;
			Block_6:
			this._currentStream = null;
			return num;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0006A9D8 File Offset: 0x0006A9D8
		public override int ReadByte()
		{
			if (this._currentStream == null)
			{
				if (!this._first)
				{
					return 0;
				}
				Asn1OctetStringParser nextParser = this.GetNextParser();
				if (nextParser == null)
				{
					return 0;
				}
				this._first = false;
				this._currentStream = nextParser.GetOctetStream();
			}
			int num;
			for (;;)
			{
				num = this._currentStream.ReadByte();
				if (num >= 0)
				{
					break;
				}
				Asn1OctetStringParser nextParser2 = this.GetNextParser();
				if (nextParser2 == null)
				{
					goto Block_5;
				}
				this._currentStream = nextParser2.GetOctetStream();
			}
			return num;
			Block_5:
			this._currentStream = null;
			return -1;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0006AA5C File Offset: 0x0006AA5C
		private Asn1OctetStringParser GetNextParser()
		{
			IAsn1Convertible asn1Convertible = this._parser.ReadObject();
			if (asn1Convertible == null)
			{
				return null;
			}
			if (asn1Convertible is Asn1OctetStringParser)
			{
				return (Asn1OctetStringParser)asn1Convertible;
			}
			throw new IOException("unknown object encountered: " + Platform.GetTypeName(asn1Convertible));
		}

		// Token: 0x04000D9E RID: 3486
		private readonly Asn1StreamParser _parser;

		// Token: 0x04000D9F RID: 3487
		private bool _first = true;

		// Token: 0x04000DA0 RID: 3488
		private Stream _currentStream;
	}
}
