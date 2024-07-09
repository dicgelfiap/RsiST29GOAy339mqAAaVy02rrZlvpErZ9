using System;
using System.Collections;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200036C RID: 876
	public class SkeinEngine : IMemoable
	{
		// Token: 0x06001AE9 RID: 6889 RVA: 0x00091324 File Offset: 0x00091324
		static SkeinEngine()
		{
			SkeinEngine.InitialState(256, 128, new ulong[]
			{
				16217771249220022880UL,
				9817190399063458076UL,
				1155188648486244218UL,
				14769517481627992514UL
			});
			SkeinEngine.InitialState(256, 160, new ulong[]
			{
				1450197650740764312UL,
				3081844928540042640UL,
				15310647011875280446UL,
				3301952811952417661UL
			});
			SkeinEngine.InitialState(256, 224, new ulong[]
			{
				14270089230798940683UL,
				9758551101254474012UL,
				11082101768697755780UL,
				4056579644589979102UL
			});
			SkeinEngine.InitialState(256, 256, new ulong[]
			{
				18202890402666165321UL,
				3443677322885453875UL,
				12915131351309911055UL,
				7662005193972177513UL
			});
			SkeinEngine.InitialState(512, 128, new ulong[]
			{
				12158729379475595090UL,
				2204638249859346602UL,
				3502419045458743507UL,
				13617680570268287068UL,
				983504137758028059UL,
				1880512238245786339UL,
				11730851291495443074UL,
				7602827311880509485UL
			});
			SkeinEngine.InitialState(512, 160, new ulong[]
			{
				2934123928682216849UL,
				14047033351726823311UL,
				1684584802963255058UL,
				5744138295201861711UL,
				2444857010922934358UL,
				15638910433986703544UL,
				13325156239043941114UL,
				118355523173251694UL
			});
			SkeinEngine.InitialState(512, 224, new ulong[]
			{
				14758403053642543652UL,
				14674518637417806319UL,
				10145881904771976036UL,
				4146387520469897396UL,
				1106145742801415120UL,
				7455425944880474941UL,
				11095680972475339753UL,
				11397762726744039159UL
			});
			SkeinEngine.InitialState(512, 384, new ulong[]
			{
				11814849197074935647UL,
				12753905853581818532UL,
				11346781217370868990UL,
				15535391162178797018UL,
				2000907093792408677UL,
				9140007292425499655UL,
				6093301768906360022UL,
				2769176472213098488UL
			});
			SkeinEngine.InitialState(512, 512, new ulong[]
			{
				5261240102383538638UL,
				978932832955457283UL,
				10363226125605772238UL,
				11107378794354519217UL,
				6752626034097301424UL,
				16915020251879818228UL,
				11029617608758768931UL,
				12544957130904423475UL
			});
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00091460 File Offset: 0x00091460
		private static void InitialState(int blockSize, int outputSize, ulong[] state)
		{
			SkeinEngine.INITIAL_STATES.Add(SkeinEngine.VariantIdentifier(blockSize / 8, outputSize / 8), state);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00091480 File Offset: 0x00091480
		private static int VariantIdentifier(int blockSizeBytes, int outputSizeBytes)
		{
			return outputSizeBytes << 16 | blockSizeBytes;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00091488 File Offset: 0x00091488
		public SkeinEngine(int blockSizeBits, int outputSizeBits)
		{
			if (outputSizeBits % 8 != 0)
			{
				throw new ArgumentException("Output size must be a multiple of 8 bits. :" + outputSizeBits);
			}
			this.outputSizeBytes = outputSizeBits / 8;
			this.threefish = new ThreefishEngine(blockSizeBits);
			this.ubi = new SkeinEngine.UBI(this, this.threefish.GetBlockSize());
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000914F8 File Offset: 0x000914F8
		public SkeinEngine(SkeinEngine engine) : this(engine.BlockSize * 8, engine.OutputSize * 8)
		{
			this.CopyIn(engine);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00091528 File Offset: 0x00091528
		private void CopyIn(SkeinEngine engine)
		{
			this.ubi.Reset(engine.ubi);
			this.chain = Arrays.Clone(engine.chain, this.chain);
			this.initialState = Arrays.Clone(engine.initialState, this.initialState);
			this.key = Arrays.Clone(engine.key, this.key);
			this.preMessageParameters = SkeinEngine.Clone(engine.preMessageParameters, this.preMessageParameters);
			this.postMessageParameters = SkeinEngine.Clone(engine.postMessageParameters, this.postMessageParameters);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000915C0 File Offset: 0x000915C0
		private static SkeinEngine.Parameter[] Clone(SkeinEngine.Parameter[] data, SkeinEngine.Parameter[] existing)
		{
			if (data == null)
			{
				return null;
			}
			if (existing == null || existing.Length != data.Length)
			{
				existing = new SkeinEngine.Parameter[data.Length];
			}
			Array.Copy(data, 0, existing, 0, existing.Length);
			return existing;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000915F4 File Offset: 0x000915F4
		public IMemoable Copy()
		{
			return new SkeinEngine(this);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000915FC File Offset: 0x000915FC
		public void Reset(IMemoable other)
		{
			SkeinEngine skeinEngine = (SkeinEngine)other;
			if (this.BlockSize != skeinEngine.BlockSize || this.outputSizeBytes != skeinEngine.outputSizeBytes)
			{
				throw new MemoableResetException("Incompatible parameters in provided SkeinEngine.");
			}
			this.CopyIn(skeinEngine);
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00091648 File Offset: 0x00091648
		public int OutputSize
		{
			get
			{
				return this.outputSizeBytes;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00091650 File Offset: 0x00091650
		public int BlockSize
		{
			get
			{
				return this.threefish.GetBlockSize();
			}
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00091660 File Offset: 0x00091660
		public void Init(SkeinParameters parameters)
		{
			this.chain = null;
			this.key = null;
			this.preMessageParameters = null;
			this.postMessageParameters = null;
			if (parameters != null)
			{
				byte[] array = parameters.GetKey();
				if (array.Length < 16)
				{
					throw new ArgumentException("Skein key must be at least 128 bits.");
				}
				this.InitParams(parameters.GetParameters());
			}
			this.CreateInitialState();
			this.UbiInit(48);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000916CC File Offset: 0x000916CC
		private void InitParams(IDictionary parameters)
		{
			IEnumerator enumerator = parameters.Keys.GetEnumerator();
			IList list = Platform.CreateArrayList();
			IList list2 = Platform.CreateArrayList();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				int num = (int)obj;
				byte[] value = (byte[])parameters[num];
				if (num == 0)
				{
					this.key = value;
				}
				else if (num < 48)
				{
					list.Add(new SkeinEngine.Parameter(num, value));
				}
				else
				{
					list2.Add(new SkeinEngine.Parameter(num, value));
				}
			}
			this.preMessageParameters = new SkeinEngine.Parameter[list.Count];
			list.CopyTo(this.preMessageParameters, 0);
			Array.Sort(this.preMessageParameters);
			this.postMessageParameters = new SkeinEngine.Parameter[list2.Count];
			list2.CopyTo(this.postMessageParameters, 0);
			Array.Sort(this.postMessageParameters);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000917B4 File Offset: 0x000917B4
		private void CreateInitialState()
		{
			ulong[] array = (ulong[])SkeinEngine.INITIAL_STATES[SkeinEngine.VariantIdentifier(this.BlockSize, this.OutputSize)];
			if (this.key == null && array != null)
			{
				this.chain = Arrays.Clone(array);
			}
			else
			{
				this.chain = new ulong[this.BlockSize / 8];
				if (this.key != null)
				{
					this.UbiComplete(0, this.key);
				}
				this.UbiComplete(4, new SkeinEngine.Configuration((long)(this.outputSizeBytes * 8)).Bytes);
			}
			if (this.preMessageParameters != null)
			{
				for (int i = 0; i < this.preMessageParameters.Length; i++)
				{
					SkeinEngine.Parameter parameter = this.preMessageParameters[i];
					this.UbiComplete(parameter.Type, parameter.Value);
				}
			}
			this.initialState = Arrays.Clone(this.chain);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000918A4 File Offset: 0x000918A4
		public void Reset()
		{
			Array.Copy(this.initialState, 0, this.chain, 0, this.chain.Length);
			this.UbiInit(48);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000918CC File Offset: 0x000918CC
		private void UbiComplete(int type, byte[] value)
		{
			this.UbiInit(type);
			this.ubi.Update(value, 0, value.Length, this.chain);
			this.UbiFinal();
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00091900 File Offset: 0x00091900
		private void UbiInit(int type)
		{
			this.ubi.Reset(type);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00091910 File Offset: 0x00091910
		private void UbiFinal()
		{
			this.ubi.DoFinal(this.chain);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00091924 File Offset: 0x00091924
		private void CheckInitialised()
		{
			if (this.ubi == null)
			{
				throw new ArgumentException("Skein engine is not initialised.");
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0009193C File Offset: 0x0009193C
		public void Update(byte inByte)
		{
			this.singleByte[0] = inByte;
			this.Update(this.singleByte, 0, 1);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00091958 File Offset: 0x00091958
		public void Update(byte[] inBytes, int inOff, int len)
		{
			this.CheckInitialised();
			this.ubi.Update(inBytes, inOff, len, this.chain);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00091974 File Offset: 0x00091974
		public int DoFinal(byte[] outBytes, int outOff)
		{
			this.CheckInitialised();
			if (outBytes.Length < outOff + this.outputSizeBytes)
			{
				throw new DataLengthException("Output buffer is too short to hold output");
			}
			this.UbiFinal();
			if (this.postMessageParameters != null)
			{
				for (int i = 0; i < this.postMessageParameters.Length; i++)
				{
					SkeinEngine.Parameter parameter = this.postMessageParameters[i];
					this.UbiComplete(parameter.Type, parameter.Value);
				}
			}
			int blockSize = this.BlockSize;
			int num = (this.outputSizeBytes + blockSize - 1) / blockSize;
			for (int j = 0; j < num; j++)
			{
				int outputBytes = Math.Min(blockSize, this.outputSizeBytes - j * blockSize);
				this.Output((ulong)((long)j), outBytes, outOff + j * blockSize, outputBytes);
			}
			this.Reset();
			return this.outputSizeBytes;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00091A44 File Offset: 0x00091A44
		private void Output(ulong outputSequence, byte[] outBytes, int outOff, int outputBytes)
		{
			byte[] array = new byte[8];
			ThreefishEngine.WordToBytes(outputSequence, array, 0);
			ulong[] array2 = new ulong[this.chain.Length];
			this.UbiInit(63);
			this.ubi.Update(array, 0, array.Length, array2);
			this.ubi.DoFinal(array2);
			int num = (outputBytes + 8 - 1) / 8;
			for (int i = 0; i < num; i++)
			{
				int num2 = Math.Min(8, outputBytes - i * 8);
				if (num2 == 8)
				{
					ThreefishEngine.WordToBytes(array2[i], outBytes, outOff + i * 8);
				}
				else
				{
					ThreefishEngine.WordToBytes(array2[i], array, 0);
					Array.Copy(array, 0, outBytes, outOff + i * 8, num2);
				}
			}
		}

		// Token: 0x040011DA RID: 4570
		public const int SKEIN_256 = 256;

		// Token: 0x040011DB RID: 4571
		public const int SKEIN_512 = 512;

		// Token: 0x040011DC RID: 4572
		public const int SKEIN_1024 = 1024;

		// Token: 0x040011DD RID: 4573
		private const int PARAM_TYPE_KEY = 0;

		// Token: 0x040011DE RID: 4574
		private const int PARAM_TYPE_CONFIG = 4;

		// Token: 0x040011DF RID: 4575
		private const int PARAM_TYPE_MESSAGE = 48;

		// Token: 0x040011E0 RID: 4576
		private const int PARAM_TYPE_OUTPUT = 63;

		// Token: 0x040011E1 RID: 4577
		private static readonly IDictionary INITIAL_STATES = Platform.CreateHashtable();

		// Token: 0x040011E2 RID: 4578
		private readonly ThreefishEngine threefish;

		// Token: 0x040011E3 RID: 4579
		private readonly int outputSizeBytes;

		// Token: 0x040011E4 RID: 4580
		private ulong[] chain;

		// Token: 0x040011E5 RID: 4581
		private ulong[] initialState;

		// Token: 0x040011E6 RID: 4582
		private byte[] key;

		// Token: 0x040011E7 RID: 4583
		private SkeinEngine.Parameter[] preMessageParameters;

		// Token: 0x040011E8 RID: 4584
		private SkeinEngine.Parameter[] postMessageParameters;

		// Token: 0x040011E9 RID: 4585
		private readonly SkeinEngine.UBI ubi;

		// Token: 0x040011EA RID: 4586
		private readonly byte[] singleByte = new byte[1];

		// Token: 0x02000DE4 RID: 3556
		private class Configuration
		{
			// Token: 0x06008B92 RID: 35730 RVA: 0x0029E7E8 File Offset: 0x0029E7E8
			public Configuration(long outputSizeBits)
			{
				this.bytes[0] = 83;
				this.bytes[1] = 72;
				this.bytes[2] = 65;
				this.bytes[3] = 51;
				this.bytes[4] = 1;
				this.bytes[5] = 0;
				ThreefishEngine.WordToBytes((ulong)outputSizeBits, this.bytes, 8);
			}

			// Token: 0x17001D71 RID: 7537
			// (get) Token: 0x06008B93 RID: 35731 RVA: 0x0029E854 File Offset: 0x0029E854
			public byte[] Bytes
			{
				get
				{
					return this.bytes;
				}
			}

			// Token: 0x04004098 RID: 16536
			private byte[] bytes = new byte[32];
		}

		// Token: 0x02000DE5 RID: 3557
		public class Parameter
		{
			// Token: 0x06008B94 RID: 35732 RVA: 0x0029E85C File Offset: 0x0029E85C
			public Parameter(int type, byte[] value)
			{
				this.type = type;
				this.value = value;
			}

			// Token: 0x17001D72 RID: 7538
			// (get) Token: 0x06008B95 RID: 35733 RVA: 0x0029E874 File Offset: 0x0029E874
			public int Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17001D73 RID: 7539
			// (get) Token: 0x06008B96 RID: 35734 RVA: 0x0029E87C File Offset: 0x0029E87C
			public byte[] Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04004099 RID: 16537
			private int type;

			// Token: 0x0400409A RID: 16538
			private byte[] value;
		}

		// Token: 0x02000DE6 RID: 3558
		private class UbiTweak
		{
			// Token: 0x06008B97 RID: 35735 RVA: 0x0029E884 File Offset: 0x0029E884
			public UbiTweak()
			{
				this.Reset();
			}

			// Token: 0x06008B98 RID: 35736 RVA: 0x0029E8A0 File Offset: 0x0029E8A0
			public void Reset(SkeinEngine.UbiTweak tweak)
			{
				this.tweak = Arrays.Clone(tweak.tweak, this.tweak);
				this.extendedPosition = tweak.extendedPosition;
			}

			// Token: 0x06008B99 RID: 35737 RVA: 0x0029E8C8 File Offset: 0x0029E8C8
			public void Reset()
			{
				this.tweak[0] = 0UL;
				this.tweak[1] = 0UL;
				this.extendedPosition = false;
				this.First = true;
			}

			// Token: 0x17001D74 RID: 7540
			// (get) Token: 0x06008B9A RID: 35738 RVA: 0x0029E8EC File Offset: 0x0029E8EC
			// (set) Token: 0x06008B9B RID: 35739 RVA: 0x0029E900 File Offset: 0x0029E900
			public uint Type
			{
				get
				{
					return (uint)(this.tweak[1] >> 56 & 63UL);
				}
				set
				{
					this.tweak[1] = ((this.tweak[1] & 18446743798831644672UL) | ((ulong)value & 63UL) << 56);
				}
			}

			// Token: 0x17001D75 RID: 7541
			// (get) Token: 0x06008B9C RID: 35740 RVA: 0x0029E928 File Offset: 0x0029E928
			// (set) Token: 0x06008B9D RID: 35741 RVA: 0x0029E944 File Offset: 0x0029E944
			public bool First
			{
				get
				{
					return (this.tweak[1] & 4611686018427387904UL) != 0UL;
				}
				set
				{
					ulong[] array;
					if (value)
					{
						(array = this.tweak)[1] = (array[1] | 4611686018427387904UL);
						return;
					}
					(array = this.tweak)[1] = (array[1] & 13835058055282163711UL);
				}
			}

			// Token: 0x17001D76 RID: 7542
			// (get) Token: 0x06008B9E RID: 35742 RVA: 0x0029E98C File Offset: 0x0029E98C
			// (set) Token: 0x06008B9F RID: 35743 RVA: 0x0029E9A8 File Offset: 0x0029E9A8
			public bool Final
			{
				get
				{
					return (this.tweak[1] & 9223372036854775808UL) != 0UL;
				}
				set
				{
					ulong[] array;
					if (value)
					{
						(array = this.tweak)[1] = (array[1] | 9223372036854775808UL);
						return;
					}
					(array = this.tweak)[1] = (array[1] & 9223372036854775807UL);
				}
			}

			// Token: 0x06008BA0 RID: 35744 RVA: 0x0029E9F0 File Offset: 0x0029E9F0
			public void AdvancePosition(int advance)
			{
				if (this.extendedPosition)
				{
					ulong[] array = new ulong[]
					{
						this.tweak[0] & (ulong)-1,
						this.tweak[0] >> 32 & (ulong)-1,
						this.tweak[1] & (ulong)-1
					};
					ulong num = (ulong)((long)advance);
					for (int i = 0; i < array.Length; i++)
					{
						num += array[i];
						array[i] = num;
						num >>= 32;
					}
					this.tweak[0] = ((array[1] & (ulong)-1) << 32 | (array[0] & (ulong)-1));
					this.tweak[1] = ((this.tweak[1] & 18446744069414584320UL) | (array[2] & (ulong)-1));
					return;
				}
				ulong num2 = this.tweak[0];
				num2 += (ulong)advance;
				this.tweak[0] = num2;
				if (num2 > 18446744069414584320UL)
				{
					this.extendedPosition = true;
				}
			}

			// Token: 0x06008BA1 RID: 35745 RVA: 0x0029EACC File Offset: 0x0029EACC
			public ulong[] GetWords()
			{
				return this.tweak;
			}

			// Token: 0x06008BA2 RID: 35746 RVA: 0x0029EAD4 File Offset: 0x0029EAD4
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					this.Type,
					" first: ",
					this.First,
					", final: ",
					this.Final
				});
			}

			// Token: 0x0400409B RID: 16539
			private const ulong LOW_RANGE = 18446744069414584320UL;

			// Token: 0x0400409C RID: 16540
			private const ulong T1_FINAL = 9223372036854775808UL;

			// Token: 0x0400409D RID: 16541
			private const ulong T1_FIRST = 4611686018427387904UL;

			// Token: 0x0400409E RID: 16542
			private ulong[] tweak = new ulong[2];

			// Token: 0x0400409F RID: 16543
			private bool extendedPosition;
		}

		// Token: 0x02000DE7 RID: 3559
		private class UBI
		{
			// Token: 0x06008BA3 RID: 35747 RVA: 0x0029EB2C File Offset: 0x0029EB2C
			public UBI(SkeinEngine engine, int blockSize)
			{
				this.engine = engine;
				this.currentBlock = new byte[blockSize];
				this.message = new ulong[this.currentBlock.Length / 8];
			}

			// Token: 0x06008BA4 RID: 35748 RVA: 0x0029EB68 File Offset: 0x0029EB68
			public void Reset(SkeinEngine.UBI ubi)
			{
				this.currentBlock = Arrays.Clone(ubi.currentBlock, this.currentBlock);
				this.currentOffset = ubi.currentOffset;
				this.message = Arrays.Clone(ubi.message, this.message);
				this.tweak.Reset(ubi.tweak);
			}

			// Token: 0x06008BA5 RID: 35749 RVA: 0x0029EBC4 File Offset: 0x0029EBC4
			public void Reset(int type)
			{
				this.tweak.Reset();
				this.tweak.Type = (uint)type;
				this.currentOffset = 0;
			}

			// Token: 0x06008BA6 RID: 35750 RVA: 0x0029EBE4 File Offset: 0x0029EBE4
			public void Update(byte[] value, int offset, int len, ulong[] output)
			{
				int num = 0;
				while (len > num)
				{
					if (this.currentOffset == this.currentBlock.Length)
					{
						this.ProcessBlock(output);
						this.tweak.First = false;
						this.currentOffset = 0;
					}
					int num2 = Math.Min(len - num, this.currentBlock.Length - this.currentOffset);
					Array.Copy(value, offset + num, this.currentBlock, this.currentOffset, num2);
					num += num2;
					this.currentOffset += num2;
					this.tweak.AdvancePosition(num2);
				}
			}

			// Token: 0x06008BA7 RID: 35751 RVA: 0x0029EC7C File Offset: 0x0029EC7C
			private void ProcessBlock(ulong[] output)
			{
				this.engine.threefish.Init(true, this.engine.chain, this.tweak.GetWords());
				for (int i = 0; i < this.message.Length; i++)
				{
					this.message[i] = ThreefishEngine.BytesToWord(this.currentBlock, i * 8);
				}
				this.engine.threefish.ProcessBlock(this.message, output);
				for (int j = 0; j < output.Length; j++)
				{
					IntPtr intPtr;
					output[(int)(intPtr = (IntPtr)j)] = (output[(int)intPtr] ^ this.message[j]);
				}
			}

			// Token: 0x06008BA8 RID: 35752 RVA: 0x0029ED1C File Offset: 0x0029ED1C
			public void DoFinal(ulong[] output)
			{
				for (int i = this.currentOffset; i < this.currentBlock.Length; i++)
				{
					this.currentBlock[i] = 0;
				}
				this.tweak.Final = true;
				this.ProcessBlock(output);
			}

			// Token: 0x040040A0 RID: 16544
			private readonly SkeinEngine.UbiTweak tweak = new SkeinEngine.UbiTweak();

			// Token: 0x040040A1 RID: 16545
			private readonly SkeinEngine engine;

			// Token: 0x040040A2 RID: 16546
			private byte[] currentBlock;

			// Token: 0x040040A3 RID: 16547
			private int currentOffset;

			// Token: 0x040040A4 RID: 16548
			private ulong[] message;
		}
	}
}
