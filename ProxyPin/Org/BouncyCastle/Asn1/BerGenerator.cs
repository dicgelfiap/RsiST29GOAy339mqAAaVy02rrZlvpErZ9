using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200024E RID: 590
	public class BerGenerator : Asn1Generator
	{
		// Token: 0x060012EC RID: 4844 RVA: 0x000699D8 File Offset: 0x000699D8
		protected BerGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000699E8 File Offset: 0x000699E8
		public BerGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream)
		{
			this._tagged = true;
			this._isExplicit = isExplicit;
			this._tagNo = tagNo;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00069A10 File Offset: 0x00069A10
		public override void AddObject(Asn1Encodable obj)
		{
			new BerOutputStream(base.Out).WriteObject(obj);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00069A24 File Offset: 0x00069A24
		public override Stream GetRawOutputStream()
		{
			return base.Out;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00069A2C File Offset: 0x00069A2C
		public override void Close()
		{
			this.WriteBerEnd();
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00069A34 File Offset: 0x00069A34
		private void WriteHdr(int tag)
		{
			base.Out.WriteByte((byte)tag);
			base.Out.WriteByte(128);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00069A64 File Offset: 0x00069A64
		protected void WriteBerHeader(int tag)
		{
			if (!this._tagged)
			{
				this.WriteHdr(tag);
				return;
			}
			int num = this._tagNo | 128;
			if (this._isExplicit)
			{
				this.WriteHdr(num | 32);
				this.WriteHdr(tag);
				return;
			}
			if ((tag & 32) != 0)
			{
				this.WriteHdr(num | 32);
				return;
			}
			this.WriteHdr(num);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00069AD0 File Offset: 0x00069AD0
		protected void WriteBerBody(Stream contentStream)
		{
			Streams.PipeAll(contentStream, base.Out);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00069AE0 File Offset: 0x00069AE0
		protected void WriteBerEnd()
		{
			base.Out.WriteByte(0);
			base.Out.WriteByte(0);
			if (this._tagged && this._isExplicit)
			{
				base.Out.WriteByte(0);
				base.Out.WriteByte(0);
			}
		}

		// Token: 0x04000D89 RID: 3465
		private bool _tagged = false;

		// Token: 0x04000D8A RID: 3466
		private bool _isExplicit;

		// Token: 0x04000D8B RID: 3467
		private int _tagNo;
	}
}
