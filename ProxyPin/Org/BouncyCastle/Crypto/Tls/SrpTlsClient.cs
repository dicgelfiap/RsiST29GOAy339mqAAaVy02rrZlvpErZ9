using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000522 RID: 1314
	public class SrpTlsClient : AbstractTlsClient
	{
		// Token: 0x060027F5 RID: 10229 RVA: 0x000D7124 File Offset: 0x000D7124
		public SrpTlsClient(byte[] identity, byte[] password) : this(new DefaultTlsCipherFactory(), new DefaultTlsSrpGroupVerifier(), identity, password)
		{
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000D7138 File Offset: 0x000D7138
		public SrpTlsClient(TlsCipherFactory cipherFactory, byte[] identity, byte[] password) : this(cipherFactory, new DefaultTlsSrpGroupVerifier(), identity, password)
		{
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000D7148 File Offset: 0x000D7148
		public SrpTlsClient(TlsCipherFactory cipherFactory, TlsSrpGroupVerifier groupVerifier, byte[] identity, byte[] password) : base(cipherFactory)
		{
			this.mGroupVerifier = groupVerifier;
			this.mIdentity = Arrays.Clone(identity);
			this.mPassword = Arrays.Clone(password);
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x000D7174 File Offset: 0x000D7174
		protected virtual bool RequireSrpServerExtension
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000D7178 File Offset: 0x000D7178
		public override int[] GetCipherSuites()
		{
			return new int[]
			{
				49182
			};
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000D719C File Offset: 0x000D719C
		public override IDictionary GetClientExtensions()
		{
			IDictionary dictionary = TlsExtensionsUtilities.EnsureExtensionsInitialised(base.GetClientExtensions());
			TlsSrpUtilities.AddSrpExtension(dictionary, this.mIdentity);
			return dictionary;
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000D71C8 File Offset: 0x000D71C8
		public override void ProcessServerExtensions(IDictionary serverExtensions)
		{
			if (!TlsUtilities.HasExpectedEmptyExtensionData(serverExtensions, 12, 47) && this.RequireSrpServerExtension)
			{
				throw new TlsFatalAlert(47);
			}
			base.ProcessServerExtensions(serverExtensions);
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000D71F4 File Offset: 0x000D71F4
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			switch (keyExchangeAlgorithm)
			{
			case 21:
			case 22:
			case 23:
				return this.CreateSrpKeyExchange(keyExchangeAlgorithm);
			default:
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000D723C File Offset: 0x000D723C
		public override TlsAuthentication GetAuthentication()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000D7248 File Offset: 0x000D7248
		protected virtual TlsKeyExchange CreateSrpKeyExchange(int keyExchange)
		{
			return new TlsSrpKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mGroupVerifier, this.mIdentity, this.mPassword);
		}

		// Token: 0x04001A57 RID: 6743
		protected TlsSrpGroupVerifier mGroupVerifier;

		// Token: 0x04001A58 RID: 6744
		protected byte[] mIdentity;

		// Token: 0x04001A59 RID: 6745
		protected byte[] mPassword;
	}
}
