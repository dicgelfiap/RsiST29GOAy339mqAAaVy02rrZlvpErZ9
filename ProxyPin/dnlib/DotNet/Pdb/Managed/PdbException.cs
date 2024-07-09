using System;
using System.Runtime.Serialization;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x02000953 RID: 2387
	[Serializable]
	internal sealed class PdbException : Exception
	{
		// Token: 0x06005BA6 RID: 23462 RVA: 0x001BF010 File Offset: 0x001BF010
		public PdbException()
		{
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x001BF018 File Offset: 0x001BF018
		public PdbException(string message) : base("Failed to read PDB: " + message)
		{
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x001BF02C File Offset: 0x001BF02C
		public PdbException(Exception innerException) : base("Failed to read PDB: " + innerException.Message, innerException)
		{
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x001BF048 File Offset: 0x001BF048
		public PdbException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
