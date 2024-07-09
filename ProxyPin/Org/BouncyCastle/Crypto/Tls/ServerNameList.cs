using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000518 RID: 1304
	public class ServerNameList
	{
		// Token: 0x060027CA RID: 10186 RVA: 0x000D6B34 File Offset: 0x000D6B34
		public ServerNameList(IList serverNameList)
		{
			if (serverNameList == null)
			{
				throw new ArgumentNullException("serverNameList");
			}
			this.mServerNameList = serverNameList;
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000D6B54 File Offset: 0x000D6B54
		public virtual IList ServerNames
		{
			get
			{
				return this.mServerNameList;
			}
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000D6B5C File Offset: 0x000D6B5C
		public virtual void Encode(Stream output)
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = TlsUtilities.EmptyBytes;
			foreach (object obj in this.ServerNames)
			{
				ServerName serverName = (ServerName)obj;
				array = ServerNameList.CheckNameType(array, serverName.NameType);
				if (array == null)
				{
					throw new TlsFatalAlert(80);
				}
				serverName.Encode(memoryStream);
			}
			TlsUtilities.CheckUint16(memoryStream.Length);
			TlsUtilities.WriteUint16((int)memoryStream.Length, output);
			Streams.WriteBufTo(memoryStream, output);
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000D6C08 File Offset: 0x000D6C08
		public static ServerNameList Parse(Stream input)
		{
			int num = TlsUtilities.ReadUint16(input);
			if (num < 1)
			{
				throw new TlsFatalAlert(50);
			}
			byte[] buffer = TlsUtilities.ReadFully(num, input);
			MemoryStream memoryStream = new MemoryStream(buffer, false);
			byte[] array = TlsUtilities.EmptyBytes;
			IList list = Platform.CreateArrayList();
			while (memoryStream.Position < memoryStream.Length)
			{
				ServerName serverName = ServerName.Parse(memoryStream);
				array = ServerNameList.CheckNameType(array, serverName.NameType);
				if (array == null)
				{
					throw new TlsFatalAlert(47);
				}
				list.Add(serverName);
			}
			return new ServerNameList(list);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000D6C94 File Offset: 0x000D6C94
		private static byte[] CheckNameType(byte[] nameTypesSeen, byte nameType)
		{
			if (!NameType.IsValid(nameType) || Arrays.Contains(nameTypesSeen, nameType))
			{
				return null;
			}
			return Arrays.Append(nameTypesSeen, nameType);
		}

		// Token: 0x04001A3F RID: 6719
		protected readonly IList mServerNameList;
	}
}
