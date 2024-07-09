using System;
using System.IO;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200051B RID: 1307
	public class ServerSrpParams
	{
		// Token: 0x060027D4 RID: 10196 RVA: 0x000D6CC4 File Offset: 0x000D6CC4
		public ServerSrpParams(BigInteger N, BigInteger g, byte[] s, BigInteger B)
		{
			this.m_N = N;
			this.m_g = g;
			this.m_s = Arrays.Clone(s);
			this.m_B = B;
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000D6CF0 File Offset: 0x000D6CF0
		public virtual BigInteger B
		{
			get
			{
				return this.m_B;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x000D6CF8 File Offset: 0x000D6CF8
		public virtual BigInteger G
		{
			get
			{
				return this.m_g;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x000D6D00 File Offset: 0x000D6D00
		public virtual BigInteger N
		{
			get
			{
				return this.m_N;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000D6D08 File Offset: 0x000D6D08
		public virtual byte[] S
		{
			get
			{
				return this.m_s;
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000D6D10 File Offset: 0x000D6D10
		public virtual void Encode(Stream output)
		{
			TlsSrpUtilities.WriteSrpParameter(this.m_N, output);
			TlsSrpUtilities.WriteSrpParameter(this.m_g, output);
			TlsUtilities.WriteOpaque8(this.m_s, output);
			TlsSrpUtilities.WriteSrpParameter(this.m_B, output);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000D6D44 File Offset: 0x000D6D44
		public static ServerSrpParams Parse(Stream input)
		{
			BigInteger n = TlsSrpUtilities.ReadSrpParameter(input);
			BigInteger g = TlsSrpUtilities.ReadSrpParameter(input);
			byte[] s = TlsUtilities.ReadOpaque8(input);
			BigInteger b = TlsSrpUtilities.ReadSrpParameter(input);
			return new ServerSrpParams(n, g, s, b);
		}

		// Token: 0x04001A40 RID: 6720
		protected BigInteger m_N;

		// Token: 0x04001A41 RID: 6721
		protected BigInteger m_g;

		// Token: 0x04001A42 RID: 6722
		protected BigInteger m_B;

		// Token: 0x04001A43 RID: 6723
		protected byte[] m_s;
	}
}
