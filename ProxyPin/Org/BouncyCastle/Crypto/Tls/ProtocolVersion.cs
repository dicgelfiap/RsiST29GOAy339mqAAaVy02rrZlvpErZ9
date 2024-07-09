using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000512 RID: 1298
	public sealed class ProtocolVersion
	{
		// Token: 0x0600277C RID: 10108 RVA: 0x000D5CD0 File Offset: 0x000D5CD0
		private ProtocolVersion(int v, string name)
		{
			this.version = (v & 65535);
			this.name = name;
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x000D5CEC File Offset: 0x000D5CEC
		public int FullVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x000D5CF4 File Offset: 0x000D5CF4
		public int MajorVersion
		{
			get
			{
				return this.version >> 8;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x000D5D00 File Offset: 0x000D5D00
		public int MinorVersion
		{
			get
			{
				return this.version & 255;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x000D5D10 File Offset: 0x000D5D10
		public bool IsDtls
		{
			get
			{
				return this.MajorVersion == 254;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x000D5D20 File Offset: 0x000D5D20
		public bool IsSsl
		{
			get
			{
				return this == ProtocolVersion.SSLv3;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x000D5D2C File Offset: 0x000D5D2C
		public bool IsTls
		{
			get
			{
				return this.MajorVersion == 3;
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000D5D38 File Offset: 0x000D5D38
		public ProtocolVersion GetEquivalentTLSVersion()
		{
			if (!this.IsDtls)
			{
				return this;
			}
			if (this == ProtocolVersion.DTLSv10)
			{
				return ProtocolVersion.TLSv11;
			}
			return ProtocolVersion.TLSv12;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000D5D60 File Offset: 0x000D5D60
		public bool IsEqualOrEarlierVersionOf(ProtocolVersion version)
		{
			if (this.MajorVersion != version.MajorVersion)
			{
				return false;
			}
			int num = version.MinorVersion - this.MinorVersion;
			if (!this.IsDtls)
			{
				return num >= 0;
			}
			return num <= 0;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000D5DAC File Offset: 0x000D5DAC
		public bool IsLaterVersionOf(ProtocolVersion version)
		{
			if (this.MajorVersion != version.MajorVersion)
			{
				return false;
			}
			int num = version.MinorVersion - this.MinorVersion;
			if (!this.IsDtls)
			{
				return num < 0;
			}
			return num > 0;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000D5DF4 File Offset: 0x000D5DF4
		public override bool Equals(object other)
		{
			return this == other || (other is ProtocolVersion && this.Equals((ProtocolVersion)other));
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000D5E18 File Offset: 0x000D5E18
		public bool Equals(ProtocolVersion other)
		{
			return other != null && this.version == other.version;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000D5E30 File Offset: 0x000D5E30
		public override int GetHashCode()
		{
			return this.version;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000D5E38 File Offset: 0x000D5E38
		public static ProtocolVersion Get(int major, int minor)
		{
			if (major != 3)
			{
				if (major != 254)
				{
					throw new TlsFatalAlert(47);
				}
				switch (minor)
				{
				case 253:
					return ProtocolVersion.DTLSv12;
				case 254:
					throw new TlsFatalAlert(47);
				case 255:
					return ProtocolVersion.DTLSv10;
				default:
					return ProtocolVersion.GetUnknownVersion(major, minor, "DTLS");
				}
			}
			else
			{
				switch (minor)
				{
				case 0:
					return ProtocolVersion.SSLv3;
				case 1:
					return ProtocolVersion.TLSv10;
				case 2:
					return ProtocolVersion.TLSv11;
				case 3:
					return ProtocolVersion.TLSv12;
				default:
					return ProtocolVersion.GetUnknownVersion(major, minor, "TLS");
				}
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000D5EEC File Offset: 0x000D5EEC
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000D5EF4 File Offset: 0x000D5EF4
		private static ProtocolVersion GetUnknownVersion(int major, int minor, string prefix)
		{
			TlsUtilities.CheckUint8(major);
			TlsUtilities.CheckUint8(minor);
			int num = major << 8 | minor;
			string str = Platform.ToUpperInvariant(Convert.ToString(65536 | num, 16).Substring(1));
			return new ProtocolVersion(num, prefix + " 0x" + str);
		}

		// Token: 0x04001A0A RID: 6666
		public static readonly ProtocolVersion SSLv3 = new ProtocolVersion(768, "SSL 3.0");

		// Token: 0x04001A0B RID: 6667
		public static readonly ProtocolVersion TLSv10 = new ProtocolVersion(769, "TLS 1.0");

		// Token: 0x04001A0C RID: 6668
		public static readonly ProtocolVersion TLSv11 = new ProtocolVersion(770, "TLS 1.1");

		// Token: 0x04001A0D RID: 6669
		public static readonly ProtocolVersion TLSv12 = new ProtocolVersion(771, "TLS 1.2");

		// Token: 0x04001A0E RID: 6670
		public static readonly ProtocolVersion DTLSv10 = new ProtocolVersion(65279, "DTLS 1.0");

		// Token: 0x04001A0F RID: 6671
		public static readonly ProtocolVersion DTLSv12 = new ProtocolVersion(65277, "DTLS 1.2");

		// Token: 0x04001A10 RID: 6672
		private readonly int version;

		// Token: 0x04001A11 RID: 6673
		private readonly string name;
	}
}
