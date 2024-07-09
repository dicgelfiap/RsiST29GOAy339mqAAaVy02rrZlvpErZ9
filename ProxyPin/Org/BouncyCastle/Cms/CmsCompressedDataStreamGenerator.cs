using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002E3 RID: 739
	public class CmsCompressedDataStreamGenerator
	{
		// Token: 0x06001643 RID: 5699 RVA: 0x000742CC File Offset: 0x000742CC
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000742D8 File Offset: 0x000742D8
		public Stream Open(Stream outStream, string compressionOID)
		{
			return this.Open(outStream, CmsObjectIdentifiers.Data.Id, compressionOID);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000742EC File Offset: 0x000742EC
		public Stream Open(Stream outStream, string contentOID, string compressionOID)
		{
			BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
			berSequenceGenerator.AddObject(CmsObjectIdentifiers.CompressedData);
			BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
			berSequenceGenerator2.AddObject(new DerInteger(0));
			berSequenceGenerator2.AddObject(new AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.9.16.3.8")));
			BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(berSequenceGenerator2.GetRawOutputStream());
			berSequenceGenerator3.AddObject(new DerObjectIdentifier(contentOID));
			Stream output = CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, true, this._bufferSize);
			return new CmsCompressedDataStreamGenerator.CmsCompressedOutputStream(new ZOutputStream(output, -1), berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
		}

		// Token: 0x04000F2D RID: 3885
		public const string ZLib = "1.2.840.113549.1.9.16.3.8";

		// Token: 0x04000F2E RID: 3886
		private int _bufferSize;

		// Token: 0x02000DDB RID: 3547
		private class CmsCompressedOutputStream : BaseOutputStream
		{
			// Token: 0x06008B70 RID: 35696 RVA: 0x0029D9FC File Offset: 0x0029D9FC
			internal CmsCompressedOutputStream(ZOutputStream outStream, BerSequenceGenerator sGen, BerSequenceGenerator cGen, BerSequenceGenerator eiGen)
			{
				this._out = outStream;
				this._sGen = sGen;
				this._cGen = cGen;
				this._eiGen = eiGen;
			}

			// Token: 0x06008B71 RID: 35697 RVA: 0x0029DA24 File Offset: 0x0029DA24
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x06008B72 RID: 35698 RVA: 0x0029DA34 File Offset: 0x0029DA34
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x06008B73 RID: 35699 RVA: 0x0029DA44 File Offset: 0x0029DA44
			public override void Close()
			{
				Platform.Dispose(this._out);
				this._eiGen.Close();
				this._cGen.Close();
				this._sGen.Close();
				base.Close();
			}

			// Token: 0x04004071 RID: 16497
			private ZOutputStream _out;

			// Token: 0x04004072 RID: 16498
			private BerSequenceGenerator _sGen;

			// Token: 0x04004073 RID: 16499
			private BerSequenceGenerator _cGen;

			// Token: 0x04004074 RID: 16500
			private BerSequenceGenerator _eiGen;
		}
	}
}
