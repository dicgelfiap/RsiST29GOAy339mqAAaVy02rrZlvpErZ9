using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000517 RID: 1303
	public class ServerName
	{
		// Token: 0x060027C3 RID: 10179 RVA: 0x000D69E0 File Offset: 0x000D69E0
		public ServerName(byte nameType, object name)
		{
			if (!ServerName.IsCorrectType(nameType, name))
			{
				throw new ArgumentException("not an instance of the correct type", "name");
			}
			this.mNameType = nameType;
			this.mName = name;
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000D6A14 File Offset: 0x000D6A14
		public virtual byte NameType
		{
			get
			{
				return this.mNameType;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000D6A1C File Offset: 0x000D6A1C
		public virtual object Name
		{
			get
			{
				return this.mName;
			}
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000D6A24 File Offset: 0x000D6A24
		public virtual string GetHostName()
		{
			if (!ServerName.IsCorrectType(0, this.mName))
			{
				throw new InvalidOperationException("'name' is not a HostName string");
			}
			return (string)this.mName;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000D6A50 File Offset: 0x000D6A50
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.mNameType, output);
			byte b = this.mNameType;
			if (b != 0)
			{
				throw new TlsFatalAlert(80);
			}
			byte[] array = Strings.ToAsciiByteArray((string)this.mName);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(80);
			}
			TlsUtilities.WriteOpaque16(array, output);
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000D6AAC File Offset: 0x000D6AAC
		public static ServerName Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			byte b2 = b;
			if (b2 != 0)
			{
				throw new TlsFatalAlert(50);
			}
			byte[] array = TlsUtilities.ReadOpaque16(input);
			if (array.Length < 1)
			{
				throw new TlsFatalAlert(50);
			}
			object name = Strings.FromAsciiByteArray(array);
			return new ServerName(b, name);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000D6B00 File Offset: 0x000D6B00
		protected static bool IsCorrectType(byte nameType, object name)
		{
			if (nameType == 0)
			{
				return name is string;
			}
			throw new ArgumentException("unsupported NameType", "nameType");
		}

		// Token: 0x04001A3D RID: 6717
		protected readonly byte mNameType;

		// Token: 0x04001A3E RID: 6718
		protected readonly object mName;
	}
}
