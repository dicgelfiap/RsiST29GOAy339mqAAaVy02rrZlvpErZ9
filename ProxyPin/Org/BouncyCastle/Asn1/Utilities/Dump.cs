using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Utilities
{
	// Token: 0x020001D1 RID: 465
	public sealed class Dump
	{
		// Token: 0x06000F03 RID: 3843 RVA: 0x0005B0BC File Offset: 0x0005B0BC
		private Dump()
		{
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0005B0C4 File Offset: 0x0005B0C4
		public static void Main(string[] args)
		{
			FileStream inputStream = File.OpenRead(args[0]);
			Asn1InputStream asn1InputStream = new Asn1InputStream(inputStream);
			Asn1Object obj;
			while ((obj = asn1InputStream.ReadObject()) != null)
			{
				Console.WriteLine(Asn1Dump.DumpAsString(obj));
			}
			Platform.Dispose(asn1InputStream);
		}
	}
}
