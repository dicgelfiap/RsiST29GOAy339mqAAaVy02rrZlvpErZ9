using System;

namespace Org.BouncyCastle.Asn1.GM
{
	// Token: 0x0200016C RID: 364
	public abstract class GMObjectIdentifiers
	{
		// Token: 0x04000840 RID: 2112
		public static readonly DerObjectIdentifier sm_scheme = new DerObjectIdentifier("1.2.156.10197.1");

		// Token: 0x04000841 RID: 2113
		public static readonly DerObjectIdentifier sm6_ecb = GMObjectIdentifiers.sm_scheme.Branch("101.1");

		// Token: 0x04000842 RID: 2114
		public static readonly DerObjectIdentifier sm6_cbc = GMObjectIdentifiers.sm_scheme.Branch("101.2");

		// Token: 0x04000843 RID: 2115
		public static readonly DerObjectIdentifier sm6_ofb128 = GMObjectIdentifiers.sm_scheme.Branch("101.3");

		// Token: 0x04000844 RID: 2116
		public static readonly DerObjectIdentifier sm6_cfb128 = GMObjectIdentifiers.sm_scheme.Branch("101.4");

		// Token: 0x04000845 RID: 2117
		public static readonly DerObjectIdentifier sm1_ecb = GMObjectIdentifiers.sm_scheme.Branch("102.1");

		// Token: 0x04000846 RID: 2118
		public static readonly DerObjectIdentifier sm1_cbc = GMObjectIdentifiers.sm_scheme.Branch("102.2");

		// Token: 0x04000847 RID: 2119
		public static readonly DerObjectIdentifier sm1_ofb128 = GMObjectIdentifiers.sm_scheme.Branch("102.3");

		// Token: 0x04000848 RID: 2120
		public static readonly DerObjectIdentifier sm1_cfb128 = GMObjectIdentifiers.sm_scheme.Branch("102.4");

		// Token: 0x04000849 RID: 2121
		public static readonly DerObjectIdentifier sm1_cfb1 = GMObjectIdentifiers.sm_scheme.Branch("102.5");

		// Token: 0x0400084A RID: 2122
		public static readonly DerObjectIdentifier sm1_cfb8 = GMObjectIdentifiers.sm_scheme.Branch("102.6");

		// Token: 0x0400084B RID: 2123
		public static readonly DerObjectIdentifier ssf33_ecb = GMObjectIdentifiers.sm_scheme.Branch("103.1");

		// Token: 0x0400084C RID: 2124
		public static readonly DerObjectIdentifier ssf33_cbc = GMObjectIdentifiers.sm_scheme.Branch("103.2");

		// Token: 0x0400084D RID: 2125
		public static readonly DerObjectIdentifier ssf33_ofb128 = GMObjectIdentifiers.sm_scheme.Branch("103.3");

		// Token: 0x0400084E RID: 2126
		public static readonly DerObjectIdentifier ssf33_cfb128 = GMObjectIdentifiers.sm_scheme.Branch("103.4");

		// Token: 0x0400084F RID: 2127
		public static readonly DerObjectIdentifier ssf33_cfb1 = GMObjectIdentifiers.sm_scheme.Branch("103.5");

		// Token: 0x04000850 RID: 2128
		public static readonly DerObjectIdentifier ssf33_cfb8 = GMObjectIdentifiers.sm_scheme.Branch("103.6");

		// Token: 0x04000851 RID: 2129
		public static readonly DerObjectIdentifier sms4_ecb = GMObjectIdentifiers.sm_scheme.Branch("104.1");

		// Token: 0x04000852 RID: 2130
		public static readonly DerObjectIdentifier sms4_cbc = GMObjectIdentifiers.sm_scheme.Branch("104.2");

		// Token: 0x04000853 RID: 2131
		public static readonly DerObjectIdentifier sms4_ofb128 = GMObjectIdentifiers.sm_scheme.Branch("104.3");

		// Token: 0x04000854 RID: 2132
		public static readonly DerObjectIdentifier sms4_cfb128 = GMObjectIdentifiers.sm_scheme.Branch("104.4");

		// Token: 0x04000855 RID: 2133
		public static readonly DerObjectIdentifier sms4_cfb1 = GMObjectIdentifiers.sm_scheme.Branch("104.5");

		// Token: 0x04000856 RID: 2134
		public static readonly DerObjectIdentifier sms4_cfb8 = GMObjectIdentifiers.sm_scheme.Branch("104.6");

		// Token: 0x04000857 RID: 2135
		public static readonly DerObjectIdentifier sms4_ctr = GMObjectIdentifiers.sm_scheme.Branch("104.7");

		// Token: 0x04000858 RID: 2136
		public static readonly DerObjectIdentifier sms4_gcm = GMObjectIdentifiers.sm_scheme.Branch("104.8");

		// Token: 0x04000859 RID: 2137
		public static readonly DerObjectIdentifier sms4_ccm = GMObjectIdentifiers.sm_scheme.Branch("104.9");

		// Token: 0x0400085A RID: 2138
		public static readonly DerObjectIdentifier sms4_xts = GMObjectIdentifiers.sm_scheme.Branch("104.10");

		// Token: 0x0400085B RID: 2139
		public static readonly DerObjectIdentifier sms4_wrap = GMObjectIdentifiers.sm_scheme.Branch("104.11");

		// Token: 0x0400085C RID: 2140
		public static readonly DerObjectIdentifier sms4_wrap_pad = GMObjectIdentifiers.sm_scheme.Branch("104.12");

		// Token: 0x0400085D RID: 2141
		public static readonly DerObjectIdentifier sms4_ocb = GMObjectIdentifiers.sm_scheme.Branch("104.100");

		// Token: 0x0400085E RID: 2142
		public static readonly DerObjectIdentifier sm5 = GMObjectIdentifiers.sm_scheme.Branch("201");

		// Token: 0x0400085F RID: 2143
		public static readonly DerObjectIdentifier sm2p256v1 = GMObjectIdentifiers.sm_scheme.Branch("301");

		// Token: 0x04000860 RID: 2144
		public static readonly DerObjectIdentifier sm2sign = GMObjectIdentifiers.sm_scheme.Branch("301.1");

		// Token: 0x04000861 RID: 2145
		public static readonly DerObjectIdentifier sm2exchange = GMObjectIdentifiers.sm_scheme.Branch("301.2");

		// Token: 0x04000862 RID: 2146
		public static readonly DerObjectIdentifier sm2encrypt = GMObjectIdentifiers.sm_scheme.Branch("301.3");

		// Token: 0x04000863 RID: 2147
		public static readonly DerObjectIdentifier wapip192v1 = GMObjectIdentifiers.sm_scheme.Branch("301.101");

		// Token: 0x04000864 RID: 2148
		public static readonly DerObjectIdentifier sm2encrypt_recommendedParameters = GMObjectIdentifiers.sm2encrypt.Branch("1");

		// Token: 0x04000865 RID: 2149
		public static readonly DerObjectIdentifier sm2encrypt_specifiedParameters = GMObjectIdentifiers.sm2encrypt.Branch("2");

		// Token: 0x04000866 RID: 2150
		public static readonly DerObjectIdentifier sm2encrypt_with_sm3 = GMObjectIdentifiers.sm2encrypt.Branch("2.1");

		// Token: 0x04000867 RID: 2151
		public static readonly DerObjectIdentifier sm2encrypt_with_sha1 = GMObjectIdentifiers.sm2encrypt.Branch("2.2");

		// Token: 0x04000868 RID: 2152
		public static readonly DerObjectIdentifier sm2encrypt_with_sha224 = GMObjectIdentifiers.sm2encrypt.Branch("2.3");

		// Token: 0x04000869 RID: 2153
		public static readonly DerObjectIdentifier sm2encrypt_with_sha256 = GMObjectIdentifiers.sm2encrypt.Branch("2.4");

		// Token: 0x0400086A RID: 2154
		public static readonly DerObjectIdentifier sm2encrypt_with_sha384 = GMObjectIdentifiers.sm2encrypt.Branch("2.5");

		// Token: 0x0400086B RID: 2155
		public static readonly DerObjectIdentifier sm2encrypt_with_sha512 = GMObjectIdentifiers.sm2encrypt.Branch("2.6");

		// Token: 0x0400086C RID: 2156
		public static readonly DerObjectIdentifier sm2encrypt_with_rmd160 = GMObjectIdentifiers.sm2encrypt.Branch("2.7");

		// Token: 0x0400086D RID: 2157
		public static readonly DerObjectIdentifier sm2encrypt_with_whirlpool = GMObjectIdentifiers.sm2encrypt.Branch("2.8");

		// Token: 0x0400086E RID: 2158
		public static readonly DerObjectIdentifier sm2encrypt_with_blake2b512 = GMObjectIdentifiers.sm2encrypt.Branch("2.9");

		// Token: 0x0400086F RID: 2159
		public static readonly DerObjectIdentifier sm2encrypt_with_blake2s256 = GMObjectIdentifiers.sm2encrypt.Branch("2.10");

		// Token: 0x04000870 RID: 2160
		public static readonly DerObjectIdentifier sm2encrypt_with_md5 = GMObjectIdentifiers.sm2encrypt.Branch("2.11");

		// Token: 0x04000871 RID: 2161
		public static readonly DerObjectIdentifier id_sm9PublicKey = GMObjectIdentifiers.sm_scheme.Branch("302");

		// Token: 0x04000872 RID: 2162
		public static readonly DerObjectIdentifier sm9sign = GMObjectIdentifiers.sm_scheme.Branch("302.1");

		// Token: 0x04000873 RID: 2163
		public static readonly DerObjectIdentifier sm9keyagreement = GMObjectIdentifiers.sm_scheme.Branch("302.2");

		// Token: 0x04000874 RID: 2164
		public static readonly DerObjectIdentifier sm9encrypt = GMObjectIdentifiers.sm_scheme.Branch("302.3");

		// Token: 0x04000875 RID: 2165
		public static readonly DerObjectIdentifier sm3 = GMObjectIdentifiers.sm_scheme.Branch("401");

		// Token: 0x04000876 RID: 2166
		public static readonly DerObjectIdentifier hmac_sm3 = GMObjectIdentifiers.sm3.Branch("2");

		// Token: 0x04000877 RID: 2167
		public static readonly DerObjectIdentifier sm2sign_with_sm3 = GMObjectIdentifiers.sm_scheme.Branch("501");

		// Token: 0x04000878 RID: 2168
		public static readonly DerObjectIdentifier sm2sign_with_sha1 = GMObjectIdentifiers.sm_scheme.Branch("502");

		// Token: 0x04000879 RID: 2169
		public static readonly DerObjectIdentifier sm2sign_with_sha256 = GMObjectIdentifiers.sm_scheme.Branch("503");

		// Token: 0x0400087A RID: 2170
		public static readonly DerObjectIdentifier sm2sign_with_sha512 = GMObjectIdentifiers.sm_scheme.Branch("504");

		// Token: 0x0400087B RID: 2171
		public static readonly DerObjectIdentifier sm2sign_with_sha224 = GMObjectIdentifiers.sm_scheme.Branch("505");

		// Token: 0x0400087C RID: 2172
		public static readonly DerObjectIdentifier sm2sign_with_sha384 = GMObjectIdentifiers.sm_scheme.Branch("506");

		// Token: 0x0400087D RID: 2173
		public static readonly DerObjectIdentifier sm2sign_with_rmd160 = GMObjectIdentifiers.sm_scheme.Branch("507");

		// Token: 0x0400087E RID: 2174
		public static readonly DerObjectIdentifier sm2sign_with_whirlpool = GMObjectIdentifiers.sm_scheme.Branch("520");

		// Token: 0x0400087F RID: 2175
		public static readonly DerObjectIdentifier sm2sign_with_blake2b512 = GMObjectIdentifiers.sm_scheme.Branch("521");

		// Token: 0x04000880 RID: 2176
		public static readonly DerObjectIdentifier sm2sign_with_blake2s256 = GMObjectIdentifiers.sm_scheme.Branch("522");
	}
}
