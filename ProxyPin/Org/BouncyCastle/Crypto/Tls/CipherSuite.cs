using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004DB RID: 1243
	public abstract class CipherSuite
	{
		// Token: 0x06002652 RID: 9810 RVA: 0x000D0AB8 File Offset: 0x000D0AB8
		public static bool IsScsv(int cipherSuite)
		{
			return cipherSuite == 255 || cipherSuite == 22016;
		}

		// Token: 0x040017F0 RID: 6128
		public const int TLS_NULL_WITH_NULL_NULL = 0;

		// Token: 0x040017F1 RID: 6129
		public const int TLS_RSA_WITH_NULL_MD5 = 1;

		// Token: 0x040017F2 RID: 6130
		public const int TLS_RSA_WITH_NULL_SHA = 2;

		// Token: 0x040017F3 RID: 6131
		public const int TLS_RSA_EXPORT_WITH_RC4_40_MD5 = 3;

		// Token: 0x040017F4 RID: 6132
		public const int TLS_RSA_WITH_RC4_128_MD5 = 4;

		// Token: 0x040017F5 RID: 6133
		public const int TLS_RSA_WITH_RC4_128_SHA = 5;

		// Token: 0x040017F6 RID: 6134
		public const int TLS_RSA_EXPORT_WITH_RC2_CBC_40_MD5 = 6;

		// Token: 0x040017F7 RID: 6135
		public const int TLS_RSA_WITH_IDEA_CBC_SHA = 7;

		// Token: 0x040017F8 RID: 6136
		public const int TLS_RSA_EXPORT_WITH_DES40_CBC_SHA = 8;

		// Token: 0x040017F9 RID: 6137
		public const int TLS_RSA_WITH_DES_CBC_SHA = 9;

		// Token: 0x040017FA RID: 6138
		public const int TLS_RSA_WITH_3DES_EDE_CBC_SHA = 10;

		// Token: 0x040017FB RID: 6139
		public const int TLS_DH_DSS_EXPORT_WITH_DES40_CBC_SHA = 11;

		// Token: 0x040017FC RID: 6140
		public const int TLS_DH_DSS_WITH_DES_CBC_SHA = 12;

		// Token: 0x040017FD RID: 6141
		public const int TLS_DH_DSS_WITH_3DES_EDE_CBC_SHA = 13;

		// Token: 0x040017FE RID: 6142
		public const int TLS_DH_RSA_EXPORT_WITH_DES40_CBC_SHA = 14;

		// Token: 0x040017FF RID: 6143
		public const int TLS_DH_RSA_WITH_DES_CBC_SHA = 15;

		// Token: 0x04001800 RID: 6144
		public const int TLS_DH_RSA_WITH_3DES_EDE_CBC_SHA = 16;

		// Token: 0x04001801 RID: 6145
		public const int TLS_DHE_DSS_EXPORT_WITH_DES40_CBC_SHA = 17;

		// Token: 0x04001802 RID: 6146
		public const int TLS_DHE_DSS_WITH_DES_CBC_SHA = 18;

		// Token: 0x04001803 RID: 6147
		public const int TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA = 19;

		// Token: 0x04001804 RID: 6148
		public const int TLS_DHE_RSA_EXPORT_WITH_DES40_CBC_SHA = 20;

		// Token: 0x04001805 RID: 6149
		public const int TLS_DHE_RSA_WITH_DES_CBC_SHA = 21;

		// Token: 0x04001806 RID: 6150
		public const int TLS_DHE_RSA_WITH_3DES_EDE_CBC_SHA = 22;

		// Token: 0x04001807 RID: 6151
		public const int TLS_DH_anon_EXPORT_WITH_RC4_40_MD5 = 23;

		// Token: 0x04001808 RID: 6152
		public const int TLS_DH_anon_WITH_RC4_128_MD5 = 24;

		// Token: 0x04001809 RID: 6153
		public const int TLS_DH_anon_EXPORT_WITH_DES40_CBC_SHA = 25;

		// Token: 0x0400180A RID: 6154
		public const int TLS_DH_anon_WITH_DES_CBC_SHA = 26;

		// Token: 0x0400180B RID: 6155
		public const int TLS_DH_anon_WITH_3DES_EDE_CBC_SHA = 27;

		// Token: 0x0400180C RID: 6156
		public const int TLS_RSA_WITH_AES_128_CBC_SHA = 47;

		// Token: 0x0400180D RID: 6157
		public const int TLS_DH_DSS_WITH_AES_128_CBC_SHA = 48;

		// Token: 0x0400180E RID: 6158
		public const int TLS_DH_RSA_WITH_AES_128_CBC_SHA = 49;

		// Token: 0x0400180F RID: 6159
		public const int TLS_DHE_DSS_WITH_AES_128_CBC_SHA = 50;

		// Token: 0x04001810 RID: 6160
		public const int TLS_DHE_RSA_WITH_AES_128_CBC_SHA = 51;

		// Token: 0x04001811 RID: 6161
		public const int TLS_DH_anon_WITH_AES_128_CBC_SHA = 52;

		// Token: 0x04001812 RID: 6162
		public const int TLS_RSA_WITH_AES_256_CBC_SHA = 53;

		// Token: 0x04001813 RID: 6163
		public const int TLS_DH_DSS_WITH_AES_256_CBC_SHA = 54;

		// Token: 0x04001814 RID: 6164
		public const int TLS_DH_RSA_WITH_AES_256_CBC_SHA = 55;

		// Token: 0x04001815 RID: 6165
		public const int TLS_DHE_DSS_WITH_AES_256_CBC_SHA = 56;

		// Token: 0x04001816 RID: 6166
		public const int TLS_DHE_RSA_WITH_AES_256_CBC_SHA = 57;

		// Token: 0x04001817 RID: 6167
		public const int TLS_DH_anon_WITH_AES_256_CBC_SHA = 58;

		// Token: 0x04001818 RID: 6168
		public const int TLS_RSA_WITH_CAMELLIA_128_CBC_SHA = 65;

		// Token: 0x04001819 RID: 6169
		public const int TLS_DH_DSS_WITH_CAMELLIA_128_CBC_SHA = 66;

		// Token: 0x0400181A RID: 6170
		public const int TLS_DH_RSA_WITH_CAMELLIA_128_CBC_SHA = 67;

		// Token: 0x0400181B RID: 6171
		public const int TLS_DHE_DSS_WITH_CAMELLIA_128_CBC_SHA = 68;

		// Token: 0x0400181C RID: 6172
		public const int TLS_DHE_RSA_WITH_CAMELLIA_128_CBC_SHA = 69;

		// Token: 0x0400181D RID: 6173
		public const int TLS_DH_anon_WITH_CAMELLIA_128_CBC_SHA = 70;

		// Token: 0x0400181E RID: 6174
		public const int TLS_RSA_WITH_CAMELLIA_256_CBC_SHA = 132;

		// Token: 0x0400181F RID: 6175
		public const int TLS_DH_DSS_WITH_CAMELLIA_256_CBC_SHA = 133;

		// Token: 0x04001820 RID: 6176
		public const int TLS_DH_RSA_WITH_CAMELLIA_256_CBC_SHA = 134;

		// Token: 0x04001821 RID: 6177
		public const int TLS_DHE_DSS_WITH_CAMELLIA_256_CBC_SHA = 135;

		// Token: 0x04001822 RID: 6178
		public const int TLS_DHE_RSA_WITH_CAMELLIA_256_CBC_SHA = 136;

		// Token: 0x04001823 RID: 6179
		public const int TLS_DH_anon_WITH_CAMELLIA_256_CBC_SHA = 137;

		// Token: 0x04001824 RID: 6180
		public const int TLS_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 186;

		// Token: 0x04001825 RID: 6181
		public const int TLS_DH_DSS_WITH_CAMELLIA_128_CBC_SHA256 = 187;

		// Token: 0x04001826 RID: 6182
		public const int TLS_DH_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 188;

		// Token: 0x04001827 RID: 6183
		public const int TLS_DHE_DSS_WITH_CAMELLIA_128_CBC_SHA256 = 189;

		// Token: 0x04001828 RID: 6184
		public const int TLS_DHE_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 190;

		// Token: 0x04001829 RID: 6185
		public const int TLS_DH_anon_WITH_CAMELLIA_128_CBC_SHA256 = 191;

		// Token: 0x0400182A RID: 6186
		public const int TLS_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 192;

		// Token: 0x0400182B RID: 6187
		public const int TLS_DH_DSS_WITH_CAMELLIA_256_CBC_SHA256 = 193;

		// Token: 0x0400182C RID: 6188
		public const int TLS_DH_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 194;

		// Token: 0x0400182D RID: 6189
		public const int TLS_DHE_DSS_WITH_CAMELLIA_256_CBC_SHA256 = 195;

		// Token: 0x0400182E RID: 6190
		public const int TLS_DHE_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 196;

		// Token: 0x0400182F RID: 6191
		public const int TLS_DH_anon_WITH_CAMELLIA_256_CBC_SHA256 = 197;

		// Token: 0x04001830 RID: 6192
		public const int TLS_RSA_WITH_SEED_CBC_SHA = 150;

		// Token: 0x04001831 RID: 6193
		public const int TLS_DH_DSS_WITH_SEED_CBC_SHA = 151;

		// Token: 0x04001832 RID: 6194
		public const int TLS_DH_RSA_WITH_SEED_CBC_SHA = 152;

		// Token: 0x04001833 RID: 6195
		public const int TLS_DHE_DSS_WITH_SEED_CBC_SHA = 153;

		// Token: 0x04001834 RID: 6196
		public const int TLS_DHE_RSA_WITH_SEED_CBC_SHA = 154;

		// Token: 0x04001835 RID: 6197
		public const int TLS_DH_anon_WITH_SEED_CBC_SHA = 155;

		// Token: 0x04001836 RID: 6198
		public const int TLS_PSK_WITH_RC4_128_SHA = 138;

		// Token: 0x04001837 RID: 6199
		public const int TLS_PSK_WITH_3DES_EDE_CBC_SHA = 139;

		// Token: 0x04001838 RID: 6200
		public const int TLS_PSK_WITH_AES_128_CBC_SHA = 140;

		// Token: 0x04001839 RID: 6201
		public const int TLS_PSK_WITH_AES_256_CBC_SHA = 141;

		// Token: 0x0400183A RID: 6202
		public const int TLS_DHE_PSK_WITH_RC4_128_SHA = 142;

		// Token: 0x0400183B RID: 6203
		public const int TLS_DHE_PSK_WITH_3DES_EDE_CBC_SHA = 143;

		// Token: 0x0400183C RID: 6204
		public const int TLS_DHE_PSK_WITH_AES_128_CBC_SHA = 144;

		// Token: 0x0400183D RID: 6205
		public const int TLS_DHE_PSK_WITH_AES_256_CBC_SHA = 145;

		// Token: 0x0400183E RID: 6206
		public const int TLS_RSA_PSK_WITH_RC4_128_SHA = 146;

		// Token: 0x0400183F RID: 6207
		public const int TLS_RSA_PSK_WITH_3DES_EDE_CBC_SHA = 147;

		// Token: 0x04001840 RID: 6208
		public const int TLS_RSA_PSK_WITH_AES_128_CBC_SHA = 148;

		// Token: 0x04001841 RID: 6209
		public const int TLS_RSA_PSK_WITH_AES_256_CBC_SHA = 149;

		// Token: 0x04001842 RID: 6210
		public const int TLS_ECDH_ECDSA_WITH_NULL_SHA = 49153;

		// Token: 0x04001843 RID: 6211
		public const int TLS_ECDH_ECDSA_WITH_RC4_128_SHA = 49154;

		// Token: 0x04001844 RID: 6212
		public const int TLS_ECDH_ECDSA_WITH_3DES_EDE_CBC_SHA = 49155;

		// Token: 0x04001845 RID: 6213
		public const int TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA = 49156;

		// Token: 0x04001846 RID: 6214
		public const int TLS_ECDH_ECDSA_WITH_AES_256_CBC_SHA = 49157;

		// Token: 0x04001847 RID: 6215
		public const int TLS_ECDHE_ECDSA_WITH_NULL_SHA = 49158;

		// Token: 0x04001848 RID: 6216
		public const int TLS_ECDHE_ECDSA_WITH_RC4_128_SHA = 49159;

		// Token: 0x04001849 RID: 6217
		public const int TLS_ECDHE_ECDSA_WITH_3DES_EDE_CBC_SHA = 49160;

		// Token: 0x0400184A RID: 6218
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA = 49161;

		// Token: 0x0400184B RID: 6219
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA = 49162;

		// Token: 0x0400184C RID: 6220
		public const int TLS_ECDH_RSA_WITH_NULL_SHA = 49163;

		// Token: 0x0400184D RID: 6221
		public const int TLS_ECDH_RSA_WITH_RC4_128_SHA = 49164;

		// Token: 0x0400184E RID: 6222
		public const int TLS_ECDH_RSA_WITH_3DES_EDE_CBC_SHA = 49165;

		// Token: 0x0400184F RID: 6223
		public const int TLS_ECDH_RSA_WITH_AES_128_CBC_SHA = 49166;

		// Token: 0x04001850 RID: 6224
		public const int TLS_ECDH_RSA_WITH_AES_256_CBC_SHA = 49167;

		// Token: 0x04001851 RID: 6225
		public const int TLS_ECDHE_RSA_WITH_NULL_SHA = 49168;

		// Token: 0x04001852 RID: 6226
		public const int TLS_ECDHE_RSA_WITH_RC4_128_SHA = 49169;

		// Token: 0x04001853 RID: 6227
		public const int TLS_ECDHE_RSA_WITH_3DES_EDE_CBC_SHA = 49170;

		// Token: 0x04001854 RID: 6228
		public const int TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA = 49171;

		// Token: 0x04001855 RID: 6229
		public const int TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA = 49172;

		// Token: 0x04001856 RID: 6230
		public const int TLS_ECDH_anon_WITH_NULL_SHA = 49173;

		// Token: 0x04001857 RID: 6231
		public const int TLS_ECDH_anon_WITH_RC4_128_SHA = 49174;

		// Token: 0x04001858 RID: 6232
		public const int TLS_ECDH_anon_WITH_3DES_EDE_CBC_SHA = 49175;

		// Token: 0x04001859 RID: 6233
		public const int TLS_ECDH_anon_WITH_AES_128_CBC_SHA = 49176;

		// Token: 0x0400185A RID: 6234
		public const int TLS_ECDH_anon_WITH_AES_256_CBC_SHA = 49177;

		// Token: 0x0400185B RID: 6235
		public const int TLS_PSK_WITH_NULL_SHA = 44;

		// Token: 0x0400185C RID: 6236
		public const int TLS_DHE_PSK_WITH_NULL_SHA = 45;

		// Token: 0x0400185D RID: 6237
		public const int TLS_RSA_PSK_WITH_NULL_SHA = 46;

		// Token: 0x0400185E RID: 6238
		public const int TLS_SRP_SHA_WITH_3DES_EDE_CBC_SHA = 49178;

		// Token: 0x0400185F RID: 6239
		public const int TLS_SRP_SHA_RSA_WITH_3DES_EDE_CBC_SHA = 49179;

		// Token: 0x04001860 RID: 6240
		public const int TLS_SRP_SHA_DSS_WITH_3DES_EDE_CBC_SHA = 49180;

		// Token: 0x04001861 RID: 6241
		public const int TLS_SRP_SHA_WITH_AES_128_CBC_SHA = 49181;

		// Token: 0x04001862 RID: 6242
		public const int TLS_SRP_SHA_RSA_WITH_AES_128_CBC_SHA = 49182;

		// Token: 0x04001863 RID: 6243
		public const int TLS_SRP_SHA_DSS_WITH_AES_128_CBC_SHA = 49183;

		// Token: 0x04001864 RID: 6244
		public const int TLS_SRP_SHA_WITH_AES_256_CBC_SHA = 49184;

		// Token: 0x04001865 RID: 6245
		public const int TLS_SRP_SHA_RSA_WITH_AES_256_CBC_SHA = 49185;

		// Token: 0x04001866 RID: 6246
		public const int TLS_SRP_SHA_DSS_WITH_AES_256_CBC_SHA = 49186;

		// Token: 0x04001867 RID: 6247
		public const int TLS_RSA_WITH_NULL_SHA256 = 59;

		// Token: 0x04001868 RID: 6248
		public const int TLS_RSA_WITH_AES_128_CBC_SHA256 = 60;

		// Token: 0x04001869 RID: 6249
		public const int TLS_RSA_WITH_AES_256_CBC_SHA256 = 61;

		// Token: 0x0400186A RID: 6250
		public const int TLS_DH_DSS_WITH_AES_128_CBC_SHA256 = 62;

		// Token: 0x0400186B RID: 6251
		public const int TLS_DH_RSA_WITH_AES_128_CBC_SHA256 = 63;

		// Token: 0x0400186C RID: 6252
		public const int TLS_DHE_DSS_WITH_AES_128_CBC_SHA256 = 64;

		// Token: 0x0400186D RID: 6253
		public const int TLS_DHE_RSA_WITH_AES_128_CBC_SHA256 = 103;

		// Token: 0x0400186E RID: 6254
		public const int TLS_DH_DSS_WITH_AES_256_CBC_SHA256 = 104;

		// Token: 0x0400186F RID: 6255
		public const int TLS_DH_RSA_WITH_AES_256_CBC_SHA256 = 105;

		// Token: 0x04001870 RID: 6256
		public const int TLS_DHE_DSS_WITH_AES_256_CBC_SHA256 = 106;

		// Token: 0x04001871 RID: 6257
		public const int TLS_DHE_RSA_WITH_AES_256_CBC_SHA256 = 107;

		// Token: 0x04001872 RID: 6258
		public const int TLS_DH_anon_WITH_AES_128_CBC_SHA256 = 108;

		// Token: 0x04001873 RID: 6259
		public const int TLS_DH_anon_WITH_AES_256_CBC_SHA256 = 109;

		// Token: 0x04001874 RID: 6260
		public const int TLS_RSA_WITH_AES_128_GCM_SHA256 = 156;

		// Token: 0x04001875 RID: 6261
		public const int TLS_RSA_WITH_AES_256_GCM_SHA384 = 157;

		// Token: 0x04001876 RID: 6262
		public const int TLS_DHE_RSA_WITH_AES_128_GCM_SHA256 = 158;

		// Token: 0x04001877 RID: 6263
		public const int TLS_DHE_RSA_WITH_AES_256_GCM_SHA384 = 159;

		// Token: 0x04001878 RID: 6264
		public const int TLS_DH_RSA_WITH_AES_128_GCM_SHA256 = 160;

		// Token: 0x04001879 RID: 6265
		public const int TLS_DH_RSA_WITH_AES_256_GCM_SHA384 = 161;

		// Token: 0x0400187A RID: 6266
		public const int TLS_DHE_DSS_WITH_AES_128_GCM_SHA256 = 162;

		// Token: 0x0400187B RID: 6267
		public const int TLS_DHE_DSS_WITH_AES_256_GCM_SHA384 = 163;

		// Token: 0x0400187C RID: 6268
		public const int TLS_DH_DSS_WITH_AES_128_GCM_SHA256 = 164;

		// Token: 0x0400187D RID: 6269
		public const int TLS_DH_DSS_WITH_AES_256_GCM_SHA384 = 165;

		// Token: 0x0400187E RID: 6270
		public const int TLS_DH_anon_WITH_AES_128_GCM_SHA256 = 166;

		// Token: 0x0400187F RID: 6271
		public const int TLS_DH_anon_WITH_AES_256_GCM_SHA384 = 167;

		// Token: 0x04001880 RID: 6272
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256 = 49187;

		// Token: 0x04001881 RID: 6273
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA384 = 49188;

		// Token: 0x04001882 RID: 6274
		public const int TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA256 = 49189;

		// Token: 0x04001883 RID: 6275
		public const int TLS_ECDH_ECDSA_WITH_AES_256_CBC_SHA384 = 49190;

		// Token: 0x04001884 RID: 6276
		public const int TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256 = 49191;

		// Token: 0x04001885 RID: 6277
		public const int TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA384 = 49192;

		// Token: 0x04001886 RID: 6278
		public const int TLS_ECDH_RSA_WITH_AES_128_CBC_SHA256 = 49193;

		// Token: 0x04001887 RID: 6279
		public const int TLS_ECDH_RSA_WITH_AES_256_CBC_SHA384 = 49194;

		// Token: 0x04001888 RID: 6280
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256 = 49195;

		// Token: 0x04001889 RID: 6281
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384 = 49196;

		// Token: 0x0400188A RID: 6282
		public const int TLS_ECDH_ECDSA_WITH_AES_128_GCM_SHA256 = 49197;

		// Token: 0x0400188B RID: 6283
		public const int TLS_ECDH_ECDSA_WITH_AES_256_GCM_SHA384 = 49198;

		// Token: 0x0400188C RID: 6284
		public const int TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256 = 49199;

		// Token: 0x0400188D RID: 6285
		public const int TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384 = 49200;

		// Token: 0x0400188E RID: 6286
		public const int TLS_ECDH_RSA_WITH_AES_128_GCM_SHA256 = 49201;

		// Token: 0x0400188F RID: 6287
		public const int TLS_ECDH_RSA_WITH_AES_256_GCM_SHA384 = 49202;

		// Token: 0x04001890 RID: 6288
		public const int TLS_PSK_WITH_AES_128_GCM_SHA256 = 168;

		// Token: 0x04001891 RID: 6289
		public const int TLS_PSK_WITH_AES_256_GCM_SHA384 = 169;

		// Token: 0x04001892 RID: 6290
		public const int TLS_DHE_PSK_WITH_AES_128_GCM_SHA256 = 170;

		// Token: 0x04001893 RID: 6291
		public const int TLS_DHE_PSK_WITH_AES_256_GCM_SHA384 = 171;

		// Token: 0x04001894 RID: 6292
		public const int TLS_RSA_PSK_WITH_AES_128_GCM_SHA256 = 172;

		// Token: 0x04001895 RID: 6293
		public const int TLS_RSA_PSK_WITH_AES_256_GCM_SHA384 = 173;

		// Token: 0x04001896 RID: 6294
		public const int TLS_PSK_WITH_AES_128_CBC_SHA256 = 174;

		// Token: 0x04001897 RID: 6295
		public const int TLS_PSK_WITH_AES_256_CBC_SHA384 = 175;

		// Token: 0x04001898 RID: 6296
		public const int TLS_PSK_WITH_NULL_SHA256 = 176;

		// Token: 0x04001899 RID: 6297
		public const int TLS_PSK_WITH_NULL_SHA384 = 177;

		// Token: 0x0400189A RID: 6298
		public const int TLS_DHE_PSK_WITH_AES_128_CBC_SHA256 = 178;

		// Token: 0x0400189B RID: 6299
		public const int TLS_DHE_PSK_WITH_AES_256_CBC_SHA384 = 179;

		// Token: 0x0400189C RID: 6300
		public const int TLS_DHE_PSK_WITH_NULL_SHA256 = 180;

		// Token: 0x0400189D RID: 6301
		public const int TLS_DHE_PSK_WITH_NULL_SHA384 = 181;

		// Token: 0x0400189E RID: 6302
		public const int TLS_RSA_PSK_WITH_AES_128_CBC_SHA256 = 182;

		// Token: 0x0400189F RID: 6303
		public const int TLS_RSA_PSK_WITH_AES_256_CBC_SHA384 = 183;

		// Token: 0x040018A0 RID: 6304
		public const int TLS_RSA_PSK_WITH_NULL_SHA256 = 184;

		// Token: 0x040018A1 RID: 6305
		public const int TLS_RSA_PSK_WITH_NULL_SHA384 = 185;

		// Token: 0x040018A2 RID: 6306
		public const int TLS_ECDHE_PSK_WITH_RC4_128_SHA = 49203;

		// Token: 0x040018A3 RID: 6307
		public const int TLS_ECDHE_PSK_WITH_3DES_EDE_CBC_SHA = 49204;

		// Token: 0x040018A4 RID: 6308
		public const int TLS_ECDHE_PSK_WITH_AES_128_CBC_SHA = 49205;

		// Token: 0x040018A5 RID: 6309
		public const int TLS_ECDHE_PSK_WITH_AES_256_CBC_SHA = 49206;

		// Token: 0x040018A6 RID: 6310
		public const int TLS_ECDHE_PSK_WITH_AES_128_CBC_SHA256 = 49207;

		// Token: 0x040018A7 RID: 6311
		public const int TLS_ECDHE_PSK_WITH_AES_256_CBC_SHA384 = 49208;

		// Token: 0x040018A8 RID: 6312
		public const int TLS_ECDHE_PSK_WITH_NULL_SHA = 49209;

		// Token: 0x040018A9 RID: 6313
		public const int TLS_ECDHE_PSK_WITH_NULL_SHA256 = 49210;

		// Token: 0x040018AA RID: 6314
		public const int TLS_ECDHE_PSK_WITH_NULL_SHA384 = 49211;

		// Token: 0x040018AB RID: 6315
		public const int TLS_EMPTY_RENEGOTIATION_INFO_SCSV = 255;

		// Token: 0x040018AC RID: 6316
		public const int TLS_ECDHE_ECDSA_WITH_CAMELLIA_128_CBC_SHA256 = 49266;

		// Token: 0x040018AD RID: 6317
		public const int TLS_ECDHE_ECDSA_WITH_CAMELLIA_256_CBC_SHA384 = 49267;

		// Token: 0x040018AE RID: 6318
		public const int TLS_ECDH_ECDSA_WITH_CAMELLIA_128_CBC_SHA256 = 49268;

		// Token: 0x040018AF RID: 6319
		public const int TLS_ECDH_ECDSA_WITH_CAMELLIA_256_CBC_SHA384 = 49269;

		// Token: 0x040018B0 RID: 6320
		public const int TLS_ECDHE_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 49270;

		// Token: 0x040018B1 RID: 6321
		public const int TLS_ECDHE_RSA_WITH_CAMELLIA_256_CBC_SHA384 = 49271;

		// Token: 0x040018B2 RID: 6322
		public const int TLS_ECDH_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 49272;

		// Token: 0x040018B3 RID: 6323
		public const int TLS_ECDH_RSA_WITH_CAMELLIA_256_CBC_SHA384 = 49273;

		// Token: 0x040018B4 RID: 6324
		public const int TLS_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49274;

		// Token: 0x040018B5 RID: 6325
		public const int TLS_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49275;

		// Token: 0x040018B6 RID: 6326
		public const int TLS_DHE_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49276;

		// Token: 0x040018B7 RID: 6327
		public const int TLS_DHE_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49277;

		// Token: 0x040018B8 RID: 6328
		public const int TLS_DH_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49278;

		// Token: 0x040018B9 RID: 6329
		public const int TLS_DH_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49279;

		// Token: 0x040018BA RID: 6330
		public const int TLS_DHE_DSS_WITH_CAMELLIA_128_GCM_SHA256 = 49280;

		// Token: 0x040018BB RID: 6331
		public const int TLS_DHE_DSS_WITH_CAMELLIA_256_GCM_SHA384 = 49281;

		// Token: 0x040018BC RID: 6332
		public const int TLS_DH_DSS_WITH_CAMELLIA_128_GCM_SHA256 = 49282;

		// Token: 0x040018BD RID: 6333
		public const int TLS_DH_DSS_WITH_CAMELLIA_256_GCM_SHA384 = 49283;

		// Token: 0x040018BE RID: 6334
		public const int TLS_DH_anon_WITH_CAMELLIA_128_GCM_SHA256 = 49284;

		// Token: 0x040018BF RID: 6335
		public const int TLS_DH_anon_WITH_CAMELLIA_256_GCM_SHA384 = 49285;

		// Token: 0x040018C0 RID: 6336
		public const int TLS_ECDHE_ECDSA_WITH_CAMELLIA_128_GCM_SHA256 = 49286;

		// Token: 0x040018C1 RID: 6337
		public const int TLS_ECDHE_ECDSA_WITH_CAMELLIA_256_GCM_SHA384 = 49287;

		// Token: 0x040018C2 RID: 6338
		public const int TLS_ECDH_ECDSA_WITH_CAMELLIA_128_GCM_SHA256 = 49288;

		// Token: 0x040018C3 RID: 6339
		public const int TLS_ECDH_ECDSA_WITH_CAMELLIA_256_GCM_SHA384 = 49289;

		// Token: 0x040018C4 RID: 6340
		public const int TLS_ECDHE_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49290;

		// Token: 0x040018C5 RID: 6341
		public const int TLS_ECDHE_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49291;

		// Token: 0x040018C6 RID: 6342
		public const int TLS_ECDH_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49292;

		// Token: 0x040018C7 RID: 6343
		public const int TLS_ECDH_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49293;

		// Token: 0x040018C8 RID: 6344
		public const int TLS_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49294;

		// Token: 0x040018C9 RID: 6345
		public const int TLS_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49295;

		// Token: 0x040018CA RID: 6346
		public const int TLS_DHE_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49296;

		// Token: 0x040018CB RID: 6347
		public const int TLS_DHE_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49297;

		// Token: 0x040018CC RID: 6348
		public const int TLS_RSA_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49298;

		// Token: 0x040018CD RID: 6349
		public const int TLS_RSA_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49299;

		// Token: 0x040018CE RID: 6350
		public const int TLS_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49300;

		// Token: 0x040018CF RID: 6351
		public const int TLS_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49301;

		// Token: 0x040018D0 RID: 6352
		public const int TLS_DHE_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49302;

		// Token: 0x040018D1 RID: 6353
		public const int TLS_DHE_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49303;

		// Token: 0x040018D2 RID: 6354
		public const int TLS_RSA_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49304;

		// Token: 0x040018D3 RID: 6355
		public const int TLS_RSA_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49305;

		// Token: 0x040018D4 RID: 6356
		public const int TLS_ECDHE_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49306;

		// Token: 0x040018D5 RID: 6357
		public const int TLS_ECDHE_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49307;

		// Token: 0x040018D6 RID: 6358
		public const int TLS_RSA_WITH_AES_128_CCM = 49308;

		// Token: 0x040018D7 RID: 6359
		public const int TLS_RSA_WITH_AES_256_CCM = 49309;

		// Token: 0x040018D8 RID: 6360
		public const int TLS_DHE_RSA_WITH_AES_128_CCM = 49310;

		// Token: 0x040018D9 RID: 6361
		public const int TLS_DHE_RSA_WITH_AES_256_CCM = 49311;

		// Token: 0x040018DA RID: 6362
		public const int TLS_RSA_WITH_AES_128_CCM_8 = 49312;

		// Token: 0x040018DB RID: 6363
		public const int TLS_RSA_WITH_AES_256_CCM_8 = 49313;

		// Token: 0x040018DC RID: 6364
		public const int TLS_DHE_RSA_WITH_AES_128_CCM_8 = 49314;

		// Token: 0x040018DD RID: 6365
		public const int TLS_DHE_RSA_WITH_AES_256_CCM_8 = 49315;

		// Token: 0x040018DE RID: 6366
		public const int TLS_PSK_WITH_AES_128_CCM = 49316;

		// Token: 0x040018DF RID: 6367
		public const int TLS_PSK_WITH_AES_256_CCM = 49317;

		// Token: 0x040018E0 RID: 6368
		public const int TLS_DHE_PSK_WITH_AES_128_CCM = 49318;

		// Token: 0x040018E1 RID: 6369
		public const int TLS_DHE_PSK_WITH_AES_256_CCM = 49319;

		// Token: 0x040018E2 RID: 6370
		public const int TLS_PSK_WITH_AES_128_CCM_8 = 49320;

		// Token: 0x040018E3 RID: 6371
		public const int TLS_PSK_WITH_AES_256_CCM_8 = 49321;

		// Token: 0x040018E4 RID: 6372
		public const int TLS_PSK_DHE_WITH_AES_128_CCM_8 = 49322;

		// Token: 0x040018E5 RID: 6373
		public const int TLS_PSK_DHE_WITH_AES_256_CCM_8 = 49323;

		// Token: 0x040018E6 RID: 6374
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_CCM = 49324;

		// Token: 0x040018E7 RID: 6375
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_CCM = 49325;

		// Token: 0x040018E8 RID: 6376
		public const int TLS_ECDHE_ECDSA_WITH_AES_128_CCM_8 = 49326;

		// Token: 0x040018E9 RID: 6377
		public const int TLS_ECDHE_ECDSA_WITH_AES_256_CCM_8 = 49327;

		// Token: 0x040018EA RID: 6378
		public const int TLS_FALLBACK_SCSV = 22016;

		// Token: 0x040018EB RID: 6379
		public const int DRAFT_TLS_ECDHE_RSA_WITH_CHACHA20_POLY1305_SHA256 = 52392;

		// Token: 0x040018EC RID: 6380
		public const int DRAFT_TLS_ECDHE_ECDSA_WITH_CHACHA20_POLY1305_SHA256 = 52393;

		// Token: 0x040018ED RID: 6381
		public const int DRAFT_TLS_DHE_RSA_WITH_CHACHA20_POLY1305_SHA256 = 52394;

		// Token: 0x040018EE RID: 6382
		public const int DRAFT_TLS_PSK_WITH_CHACHA20_POLY1305_SHA256 = 52395;

		// Token: 0x040018EF RID: 6383
		public const int DRAFT_TLS_ECDHE_PSK_WITH_CHACHA20_POLY1305_SHA256 = 52396;

		// Token: 0x040018F0 RID: 6384
		public const int DRAFT_TLS_DHE_PSK_WITH_CHACHA20_POLY1305_SHA256 = 52397;

		// Token: 0x040018F1 RID: 6385
		public const int DRAFT_TLS_RSA_PSK_WITH_CHACHA20_POLY1305_SHA256 = 52398;
	}
}
