using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000271 RID: 625
	public class DerSequenceGenerator : DerGenerator
	{
		// Token: 0x060013E4 RID: 5092 RVA: 0x0006C2BC File Offset: 0x0006C2BC
		public DerSequenceGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0006C2D0 File Offset: 0x0006C2D0
		public DerSequenceGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0006C2E8 File Offset: 0x0006C2E8
		public override void AddObject(Asn1Encodable obj)
		{
			new DerOutputStream(this._bOut).WriteObject(obj);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0006C2FC File Offset: 0x0006C2FC
		public override Stream GetRawOutputStream()
		{
			return this._bOut;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0006C304 File Offset: 0x0006C304
		public override void Close()
		{
			base.WriteDerEncoded(48, this._bOut.ToArray());
		}

		// Token: 0x04000DB9 RID: 3513
		private readonly MemoryStream _bOut = new MemoryStream();
	}
}
