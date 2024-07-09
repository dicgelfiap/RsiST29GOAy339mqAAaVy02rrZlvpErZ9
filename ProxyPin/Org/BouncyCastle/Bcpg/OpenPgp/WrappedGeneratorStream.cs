using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200066F RID: 1647
	public class WrappedGeneratorStream : FilterStream
	{
		// Token: 0x060039C1 RID: 14785 RVA: 0x00135EE0 File Offset: 0x00135EE0
		public WrappedGeneratorStream(IStreamGenerator gen, Stream str) : base(str)
		{
			this.gen = gen;
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x00135EF0 File Offset: 0x00135EF0
		public override void Close()
		{
			this.gen.Close();
		}

		// Token: 0x04001E17 RID: 7703
		private readonly IStreamGenerator gen;
	}
}
