using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009D8 RID: 2520
	[ComVisible(true)]
	public enum Code : ushort
	{
		// Token: 0x04002F7B RID: 12155
		UNKNOWN1 = 256,
		// Token: 0x04002F7C RID: 12156
		UNKNOWN2,
		// Token: 0x04002F7D RID: 12157
		Add = 88,
		// Token: 0x04002F7E RID: 12158
		Add_Ovf = 214,
		// Token: 0x04002F7F RID: 12159
		Add_Ovf_Un,
		// Token: 0x04002F80 RID: 12160
		And = 95,
		// Token: 0x04002F81 RID: 12161
		Arglist = 65024,
		// Token: 0x04002F82 RID: 12162
		Beq = 59,
		// Token: 0x04002F83 RID: 12163
		Beq_S = 46,
		// Token: 0x04002F84 RID: 12164
		Bge = 60,
		// Token: 0x04002F85 RID: 12165
		Bge_S = 47,
		// Token: 0x04002F86 RID: 12166
		Bge_Un = 65,
		// Token: 0x04002F87 RID: 12167
		Bge_Un_S = 52,
		// Token: 0x04002F88 RID: 12168
		Bgt = 61,
		// Token: 0x04002F89 RID: 12169
		Bgt_S = 48,
		// Token: 0x04002F8A RID: 12170
		Bgt_Un = 66,
		// Token: 0x04002F8B RID: 12171
		Bgt_Un_S = 53,
		// Token: 0x04002F8C RID: 12172
		Ble = 62,
		// Token: 0x04002F8D RID: 12173
		Ble_S = 49,
		// Token: 0x04002F8E RID: 12174
		Ble_Un = 67,
		// Token: 0x04002F8F RID: 12175
		Ble_Un_S = 54,
		// Token: 0x04002F90 RID: 12176
		Blt = 63,
		// Token: 0x04002F91 RID: 12177
		Blt_S = 50,
		// Token: 0x04002F92 RID: 12178
		Blt_Un = 68,
		// Token: 0x04002F93 RID: 12179
		Blt_Un_S = 55,
		// Token: 0x04002F94 RID: 12180
		Bne_Un = 64,
		// Token: 0x04002F95 RID: 12181
		Bne_Un_S = 51,
		// Token: 0x04002F96 RID: 12182
		Box = 140,
		// Token: 0x04002F97 RID: 12183
		Br = 56,
		// Token: 0x04002F98 RID: 12184
		Br_S = 43,
		// Token: 0x04002F99 RID: 12185
		Break = 1,
		// Token: 0x04002F9A RID: 12186
		Brfalse = 57,
		// Token: 0x04002F9B RID: 12187
		Brfalse_S = 44,
		// Token: 0x04002F9C RID: 12188
		Brtrue = 58,
		// Token: 0x04002F9D RID: 12189
		Brtrue_S = 45,
		// Token: 0x04002F9E RID: 12190
		Call = 40,
		// Token: 0x04002F9F RID: 12191
		Calli,
		// Token: 0x04002FA0 RID: 12192
		Callvirt = 111,
		// Token: 0x04002FA1 RID: 12193
		Castclass = 116,
		// Token: 0x04002FA2 RID: 12194
		Ceq = 65025,
		// Token: 0x04002FA3 RID: 12195
		Cgt,
		// Token: 0x04002FA4 RID: 12196
		Cgt_Un,
		// Token: 0x04002FA5 RID: 12197
		Ckfinite = 195,
		// Token: 0x04002FA6 RID: 12198
		Clt = 65028,
		// Token: 0x04002FA7 RID: 12199
		Clt_Un,
		// Token: 0x04002FA8 RID: 12200
		Constrained = 65046,
		// Token: 0x04002FA9 RID: 12201
		Conv_I = 211,
		// Token: 0x04002FAA RID: 12202
		Conv_I1 = 103,
		// Token: 0x04002FAB RID: 12203
		Conv_I2,
		// Token: 0x04002FAC RID: 12204
		Conv_I4,
		// Token: 0x04002FAD RID: 12205
		Conv_I8,
		// Token: 0x04002FAE RID: 12206
		Conv_Ovf_I = 212,
		// Token: 0x04002FAF RID: 12207
		Conv_Ovf_I_Un = 138,
		// Token: 0x04002FB0 RID: 12208
		Conv_Ovf_I1 = 179,
		// Token: 0x04002FB1 RID: 12209
		Conv_Ovf_I1_Un = 130,
		// Token: 0x04002FB2 RID: 12210
		Conv_Ovf_I2 = 181,
		// Token: 0x04002FB3 RID: 12211
		Conv_Ovf_I2_Un = 131,
		// Token: 0x04002FB4 RID: 12212
		Conv_Ovf_I4 = 183,
		// Token: 0x04002FB5 RID: 12213
		Conv_Ovf_I4_Un = 132,
		// Token: 0x04002FB6 RID: 12214
		Conv_Ovf_I8 = 185,
		// Token: 0x04002FB7 RID: 12215
		Conv_Ovf_I8_Un = 133,
		// Token: 0x04002FB8 RID: 12216
		Conv_Ovf_U = 213,
		// Token: 0x04002FB9 RID: 12217
		Conv_Ovf_U_Un = 139,
		// Token: 0x04002FBA RID: 12218
		Conv_Ovf_U1 = 180,
		// Token: 0x04002FBB RID: 12219
		Conv_Ovf_U1_Un = 134,
		// Token: 0x04002FBC RID: 12220
		Conv_Ovf_U2 = 182,
		// Token: 0x04002FBD RID: 12221
		Conv_Ovf_U2_Un = 135,
		// Token: 0x04002FBE RID: 12222
		Conv_Ovf_U4 = 184,
		// Token: 0x04002FBF RID: 12223
		Conv_Ovf_U4_Un = 136,
		// Token: 0x04002FC0 RID: 12224
		Conv_Ovf_U8 = 186,
		// Token: 0x04002FC1 RID: 12225
		Conv_Ovf_U8_Un = 137,
		// Token: 0x04002FC2 RID: 12226
		Conv_R_Un = 118,
		// Token: 0x04002FC3 RID: 12227
		Conv_R4 = 107,
		// Token: 0x04002FC4 RID: 12228
		Conv_R8,
		// Token: 0x04002FC5 RID: 12229
		Conv_U = 224,
		// Token: 0x04002FC6 RID: 12230
		Conv_U1 = 210,
		// Token: 0x04002FC7 RID: 12231
		Conv_U2 = 209,
		// Token: 0x04002FC8 RID: 12232
		Conv_U4 = 109,
		// Token: 0x04002FC9 RID: 12233
		Conv_U8,
		// Token: 0x04002FCA RID: 12234
		Cpblk = 65047,
		// Token: 0x04002FCB RID: 12235
		Cpobj = 112,
		// Token: 0x04002FCC RID: 12236
		Div = 91,
		// Token: 0x04002FCD RID: 12237
		Div_Un,
		// Token: 0x04002FCE RID: 12238
		Dup = 37,
		// Token: 0x04002FCF RID: 12239
		Endfilter = 65041,
		// Token: 0x04002FD0 RID: 12240
		Endfinally = 220,
		// Token: 0x04002FD1 RID: 12241
		Initblk = 65048,
		// Token: 0x04002FD2 RID: 12242
		Initobj = 65045,
		// Token: 0x04002FD3 RID: 12243
		Isinst = 117,
		// Token: 0x04002FD4 RID: 12244
		Jmp = 39,
		// Token: 0x04002FD5 RID: 12245
		Ldarg = 65033,
		// Token: 0x04002FD6 RID: 12246
		Ldarg_0 = 2,
		// Token: 0x04002FD7 RID: 12247
		Ldarg_1,
		// Token: 0x04002FD8 RID: 12248
		Ldarg_2,
		// Token: 0x04002FD9 RID: 12249
		Ldarg_3,
		// Token: 0x04002FDA RID: 12250
		Ldarg_S = 14,
		// Token: 0x04002FDB RID: 12251
		Ldarga = 65034,
		// Token: 0x04002FDC RID: 12252
		Ldarga_S = 15,
		// Token: 0x04002FDD RID: 12253
		Ldc_I4 = 32,
		// Token: 0x04002FDE RID: 12254
		Ldc_I4_0 = 22,
		// Token: 0x04002FDF RID: 12255
		Ldc_I4_1,
		// Token: 0x04002FE0 RID: 12256
		Ldc_I4_2,
		// Token: 0x04002FE1 RID: 12257
		Ldc_I4_3,
		// Token: 0x04002FE2 RID: 12258
		Ldc_I4_4,
		// Token: 0x04002FE3 RID: 12259
		Ldc_I4_5,
		// Token: 0x04002FE4 RID: 12260
		Ldc_I4_6,
		// Token: 0x04002FE5 RID: 12261
		Ldc_I4_7,
		// Token: 0x04002FE6 RID: 12262
		Ldc_I4_8,
		// Token: 0x04002FE7 RID: 12263
		Ldc_I4_M1 = 21,
		// Token: 0x04002FE8 RID: 12264
		Ldc_I4_S = 31,
		// Token: 0x04002FE9 RID: 12265
		Ldc_I8 = 33,
		// Token: 0x04002FEA RID: 12266
		Ldc_R4,
		// Token: 0x04002FEB RID: 12267
		Ldc_R8,
		// Token: 0x04002FEC RID: 12268
		Ldelem = 163,
		// Token: 0x04002FED RID: 12269
		Ldelem_I = 151,
		// Token: 0x04002FEE RID: 12270
		Ldelem_I1 = 144,
		// Token: 0x04002FEF RID: 12271
		Ldelem_I2 = 146,
		// Token: 0x04002FF0 RID: 12272
		Ldelem_I4 = 148,
		// Token: 0x04002FF1 RID: 12273
		Ldelem_I8 = 150,
		// Token: 0x04002FF2 RID: 12274
		Ldelem_R4 = 152,
		// Token: 0x04002FF3 RID: 12275
		Ldelem_R8,
		// Token: 0x04002FF4 RID: 12276
		Ldelem_Ref,
		// Token: 0x04002FF5 RID: 12277
		Ldelem_U1 = 145,
		// Token: 0x04002FF6 RID: 12278
		Ldelem_U2 = 147,
		// Token: 0x04002FF7 RID: 12279
		Ldelem_U4 = 149,
		// Token: 0x04002FF8 RID: 12280
		Ldelema = 143,
		// Token: 0x04002FF9 RID: 12281
		Ldfld = 123,
		// Token: 0x04002FFA RID: 12282
		Ldflda,
		// Token: 0x04002FFB RID: 12283
		Ldftn = 65030,
		// Token: 0x04002FFC RID: 12284
		Ldind_I = 77,
		// Token: 0x04002FFD RID: 12285
		Ldind_I1 = 70,
		// Token: 0x04002FFE RID: 12286
		Ldind_I2 = 72,
		// Token: 0x04002FFF RID: 12287
		Ldind_I4 = 74,
		// Token: 0x04003000 RID: 12288
		Ldind_I8 = 76,
		// Token: 0x04003001 RID: 12289
		Ldind_R4 = 78,
		// Token: 0x04003002 RID: 12290
		Ldind_R8,
		// Token: 0x04003003 RID: 12291
		Ldind_Ref,
		// Token: 0x04003004 RID: 12292
		Ldind_U1 = 71,
		// Token: 0x04003005 RID: 12293
		Ldind_U2 = 73,
		// Token: 0x04003006 RID: 12294
		Ldind_U4 = 75,
		// Token: 0x04003007 RID: 12295
		Ldlen = 142,
		// Token: 0x04003008 RID: 12296
		Ldloc = 65036,
		// Token: 0x04003009 RID: 12297
		Ldloc_0 = 6,
		// Token: 0x0400300A RID: 12298
		Ldloc_1,
		// Token: 0x0400300B RID: 12299
		Ldloc_2,
		// Token: 0x0400300C RID: 12300
		Ldloc_3,
		// Token: 0x0400300D RID: 12301
		Ldloc_S = 17,
		// Token: 0x0400300E RID: 12302
		Ldloca = 65037,
		// Token: 0x0400300F RID: 12303
		Ldloca_S = 18,
		// Token: 0x04003010 RID: 12304
		Ldnull = 20,
		// Token: 0x04003011 RID: 12305
		Ldobj = 113,
		// Token: 0x04003012 RID: 12306
		Ldsfld = 126,
		// Token: 0x04003013 RID: 12307
		Ldsflda,
		// Token: 0x04003014 RID: 12308
		Ldstr = 114,
		// Token: 0x04003015 RID: 12309
		Ldtoken = 208,
		// Token: 0x04003016 RID: 12310
		Ldvirtftn = 65031,
		// Token: 0x04003017 RID: 12311
		Leave = 221,
		// Token: 0x04003018 RID: 12312
		Leave_S,
		// Token: 0x04003019 RID: 12313
		Localloc = 65039,
		// Token: 0x0400301A RID: 12314
		Mkrefany = 198,
		// Token: 0x0400301B RID: 12315
		Mul = 90,
		// Token: 0x0400301C RID: 12316
		Mul_Ovf = 216,
		// Token: 0x0400301D RID: 12317
		Mul_Ovf_Un,
		// Token: 0x0400301E RID: 12318
		Neg = 101,
		// Token: 0x0400301F RID: 12319
		Newarr = 141,
		// Token: 0x04003020 RID: 12320
		Newobj = 115,
		// Token: 0x04003021 RID: 12321
		Nop = 0,
		// Token: 0x04003022 RID: 12322
		Not = 102,
		// Token: 0x04003023 RID: 12323
		Or = 96,
		// Token: 0x04003024 RID: 12324
		Pop = 38,
		// Token: 0x04003025 RID: 12325
		Prefix1 = 254,
		// Token: 0x04003026 RID: 12326
		Prefix2 = 253,
		// Token: 0x04003027 RID: 12327
		Prefix3 = 252,
		// Token: 0x04003028 RID: 12328
		Prefix4 = 251,
		// Token: 0x04003029 RID: 12329
		Prefix5 = 250,
		// Token: 0x0400302A RID: 12330
		Prefix6 = 249,
		// Token: 0x0400302B RID: 12331
		Prefix7 = 248,
		// Token: 0x0400302C RID: 12332
		Prefixref = 255,
		// Token: 0x0400302D RID: 12333
		Readonly = 65054,
		// Token: 0x0400302E RID: 12334
		Refanytype = 65053,
		// Token: 0x0400302F RID: 12335
		Refanyval = 194,
		// Token: 0x04003030 RID: 12336
		Rem = 93,
		// Token: 0x04003031 RID: 12337
		Rem_Un,
		// Token: 0x04003032 RID: 12338
		Ret = 42,
		// Token: 0x04003033 RID: 12339
		Rethrow = 65050,
		// Token: 0x04003034 RID: 12340
		Shl = 98,
		// Token: 0x04003035 RID: 12341
		Shr,
		// Token: 0x04003036 RID: 12342
		Shr_Un,
		// Token: 0x04003037 RID: 12343
		Sizeof = 65052,
		// Token: 0x04003038 RID: 12344
		Starg = 65035,
		// Token: 0x04003039 RID: 12345
		Starg_S = 16,
		// Token: 0x0400303A RID: 12346
		Stelem = 164,
		// Token: 0x0400303B RID: 12347
		Stelem_I = 155,
		// Token: 0x0400303C RID: 12348
		Stelem_I1,
		// Token: 0x0400303D RID: 12349
		Stelem_I2,
		// Token: 0x0400303E RID: 12350
		Stelem_I4,
		// Token: 0x0400303F RID: 12351
		Stelem_I8,
		// Token: 0x04003040 RID: 12352
		Stelem_R4,
		// Token: 0x04003041 RID: 12353
		Stelem_R8,
		// Token: 0x04003042 RID: 12354
		Stelem_Ref,
		// Token: 0x04003043 RID: 12355
		Stfld = 125,
		// Token: 0x04003044 RID: 12356
		Stind_I = 223,
		// Token: 0x04003045 RID: 12357
		Stind_I1 = 82,
		// Token: 0x04003046 RID: 12358
		Stind_I2,
		// Token: 0x04003047 RID: 12359
		Stind_I4,
		// Token: 0x04003048 RID: 12360
		Stind_I8,
		// Token: 0x04003049 RID: 12361
		Stind_R4,
		// Token: 0x0400304A RID: 12362
		Stind_R8,
		// Token: 0x0400304B RID: 12363
		Stind_Ref = 81,
		// Token: 0x0400304C RID: 12364
		Stloc = 65038,
		// Token: 0x0400304D RID: 12365
		Stloc_0 = 10,
		// Token: 0x0400304E RID: 12366
		Stloc_1,
		// Token: 0x0400304F RID: 12367
		Stloc_2,
		// Token: 0x04003050 RID: 12368
		Stloc_3,
		// Token: 0x04003051 RID: 12369
		Stloc_S = 19,
		// Token: 0x04003052 RID: 12370
		Stobj = 129,
		// Token: 0x04003053 RID: 12371
		Stsfld = 128,
		// Token: 0x04003054 RID: 12372
		Sub = 89,
		// Token: 0x04003055 RID: 12373
		Sub_Ovf = 218,
		// Token: 0x04003056 RID: 12374
		Sub_Ovf_Un,
		// Token: 0x04003057 RID: 12375
		Switch = 69,
		// Token: 0x04003058 RID: 12376
		Tailcall = 65044,
		// Token: 0x04003059 RID: 12377
		Throw = 122,
		// Token: 0x0400305A RID: 12378
		Unaligned = 65042,
		// Token: 0x0400305B RID: 12379
		Unbox = 121,
		// Token: 0x0400305C RID: 12380
		Unbox_Any = 165,
		// Token: 0x0400305D RID: 12381
		Volatile = 65043,
		// Token: 0x0400305E RID: 12382
		Xor = 97
	}
}
