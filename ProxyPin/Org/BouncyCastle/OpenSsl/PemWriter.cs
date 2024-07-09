using System;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO.Pem;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x0200067A RID: 1658
	public class PemWriter : PemWriter
	{
		// Token: 0x060039EE RID: 14830 RVA: 0x00137454 File Offset: 0x00137454
		public PemWriter(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x00137460 File Offset: 0x00137460
		public void WriteObject(object obj)
		{
			try
			{
				base.WriteObject(new MiscPemGenerator(obj));
			}
			catch (PemGenerationException ex)
			{
				if (ex.InnerException is IOException)
				{
					throw (IOException)ex.InnerException;
				}
				throw ex;
			}
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x001374B0 File Offset: 0x001374B0
		public void WriteObject(object obj, string algorithm, char[] password, SecureRandom random)
		{
			base.WriteObject(new MiscPemGenerator(obj, algorithm, password, random));
		}
	}
}
