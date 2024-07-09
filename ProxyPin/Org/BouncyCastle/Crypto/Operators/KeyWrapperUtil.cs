using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Operators
{
	// Token: 0x02000412 RID: 1042
	internal class KeyWrapperUtil
	{
		// Token: 0x06002168 RID: 8552 RVA: 0x000C2004 File Offset: 0x000C2004
		static KeyWrapperUtil()
		{
			KeyWrapperUtil.providerMap.Add("RSA/NONE/OAEPWITHSHA1ANDMGF1PADDING", new RsaOaepWrapperProvider(OiwObjectIdentifiers.IdSha1));
			KeyWrapperUtil.providerMap.Add("RSA/NONE/OAEPWITHSHA224ANDMGF1PADDING", new RsaOaepWrapperProvider(NistObjectIdentifiers.IdSha224));
			KeyWrapperUtil.providerMap.Add("RSA/NONE/OAEPWITHSHA256ANDMGF1PADDING", new RsaOaepWrapperProvider(NistObjectIdentifiers.IdSha256));
			KeyWrapperUtil.providerMap.Add("RSA/NONE/OAEPWITHSHA384ANDMGF1PADDING", new RsaOaepWrapperProvider(NistObjectIdentifiers.IdSha384));
			KeyWrapperUtil.providerMap.Add("RSA/NONE/OAEPWITHSHA512ANDMGF1PADDING", new RsaOaepWrapperProvider(NistObjectIdentifiers.IdSha512));
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000C209C File Offset: 0x000C209C
		public static IKeyWrapper WrapperForName(string algorithm, ICipherParameters parameters)
		{
			WrapperProvider wrapperProvider = (WrapperProvider)KeyWrapperUtil.providerMap[Strings.ToUpperCase(algorithm)];
			if (wrapperProvider == null)
			{
				throw new ArgumentException("could not resolve " + algorithm + " to a KeyWrapper");
			}
			return (IKeyWrapper)wrapperProvider.CreateWrapper(true, parameters);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000C20EC File Offset: 0x000C20EC
		public static IKeyUnwrapper UnwrapperForName(string algorithm, ICipherParameters parameters)
		{
			WrapperProvider wrapperProvider = (WrapperProvider)KeyWrapperUtil.providerMap[Strings.ToUpperCase(algorithm)];
			if (wrapperProvider == null)
			{
				throw new ArgumentException("could not resolve " + algorithm + " to a KeyUnwrapper");
			}
			return (IKeyUnwrapper)wrapperProvider.CreateWrapper(false, parameters);
		}

		// Token: 0x040015B6 RID: 5558
		private static readonly IDictionary providerMap = Platform.CreateHashtable();
	}
}
