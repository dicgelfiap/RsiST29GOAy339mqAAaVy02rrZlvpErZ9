using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000273 RID: 627
	public class DerSetGenerator : DerGenerator
	{
		// Token: 0x060013EC RID: 5100 RVA: 0x0006C350 File Offset: 0x0006C350
		public DerSetGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0006C364 File Offset: 0x0006C364
		public DerSetGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0006C37C File Offset: 0x0006C37C
		public override void AddObject(Asn1Encodable obj)
		{
			new DerOutputStream(this._bOut).WriteObject(obj);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0006C390 File Offset: 0x0006C390
		public override Stream GetRawOutputStream()
		{
			return this._bOut;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0006C398 File Offset: 0x0006C398
		public override void Close()
		{
			base.WriteDerEncoded(49, this._bOut.ToArray());
		}

		// Token: 0x04000DBB RID: 3515
		private readonly MemoryStream _bOut = new MemoryStream();
	}
}
