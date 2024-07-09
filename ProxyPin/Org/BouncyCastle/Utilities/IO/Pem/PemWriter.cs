using System;
using System.IO;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x02000679 RID: 1657
	public class PemWriter
	{
		// Token: 0x060039E7 RID: 14823 RVA: 0x0013719C File Offset: 0x0013719C
		public PemWriter(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.nlLength = Platform.NewLine.Length;
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060039E8 RID: 14824 RVA: 0x001371DC File Offset: 0x001371DC
		public TextWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x001371E4 File Offset: 0x001371E4
		public int GetOutputSize(PemObject obj)
		{
			int num = 2 * (obj.Type.Length + 10 + this.nlLength) + 6 + 4;
			if (obj.Headers.Count > 0)
			{
				foreach (object obj2 in obj.Headers)
				{
					PemHeader pemHeader = (PemHeader)obj2;
					num += pemHeader.Name.Length + ": ".Length + pemHeader.Value.Length + this.nlLength;
				}
				num += this.nlLength;
			}
			int num2 = (obj.Content.Length + 2) / 3 * 4;
			num += num2 + (num2 + 64 - 1) / 64 * this.nlLength;
			return num;
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x001372CC File Offset: 0x001372CC
		public void WriteObject(PemObjectGenerator objGen)
		{
			PemObject pemObject = objGen.Generate();
			this.WritePreEncapsulationBoundary(pemObject.Type);
			if (pemObject.Headers.Count > 0)
			{
				foreach (object obj in pemObject.Headers)
				{
					PemHeader pemHeader = (PemHeader)obj;
					this.writer.Write(pemHeader.Name);
					this.writer.Write(": ");
					this.writer.WriteLine(pemHeader.Value);
				}
				this.writer.WriteLine();
			}
			this.WriteEncoded(pemObject.Content);
			this.WritePostEncapsulationBoundary(pemObject.Type);
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x001373A0 File Offset: 0x001373A0
		private void WriteEncoded(byte[] bytes)
		{
			bytes = Base64.Encode(bytes);
			for (int i = 0; i < bytes.Length; i += this.buf.Length)
			{
				int num = 0;
				while (num != this.buf.Length && i + num < bytes.Length)
				{
					this.buf[num] = (char)bytes[i + num];
					num++;
				}
				this.writer.WriteLine(this.buf, 0, num);
			}
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x00137414 File Offset: 0x00137414
		private void WritePreEncapsulationBoundary(string type)
		{
			this.writer.WriteLine("-----BEGIN " + type + "-----");
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x00137434 File Offset: 0x00137434
		private void WritePostEncapsulationBoundary(string type)
		{
			this.writer.WriteLine("-----END " + type + "-----");
		}

		// Token: 0x04001E20 RID: 7712
		private const int LineLength = 64;

		// Token: 0x04001E21 RID: 7713
		private readonly TextWriter writer;

		// Token: 0x04001E22 RID: 7714
		private readonly int nlLength;

		// Token: 0x04001E23 RID: 7715
		private char[] buf = new char[64];
	}
}
