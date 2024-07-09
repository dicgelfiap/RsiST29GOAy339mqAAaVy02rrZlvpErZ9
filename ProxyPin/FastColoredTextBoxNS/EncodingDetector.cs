using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
	// Token: 0x02000A01 RID: 2561
	public static class EncodingDetector
	{
		// Token: 0x0600628C RID: 25228 RVA: 0x001D61C4 File Offset: 0x001D61C4
		public static Encoding DetectTextFileEncoding(string InputFilename)
		{
			Encoding result;
			using (FileStream fileStream = File.OpenRead(InputFilename))
			{
				result = EncodingDetector.DetectTextFileEncoding(fileStream, 65536L);
			}
			return result;
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x001D620C File Offset: 0x001D620C
		public static Encoding DetectTextFileEncoding(FileStream InputFileStream, long HeuristicSampleSize)
		{
			bool flag = false;
			return EncodingDetector.DetectTextFileEncoding(InputFileStream, 65536L, out flag);
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x001D6238 File Offset: 0x001D6238
		public static Encoding DetectTextFileEncoding(FileStream InputFileStream, long HeuristicSampleSize, out bool HasBOM)
		{
			long position = InputFileStream.Position;
			InputFileStream.Position = 0L;
			byte[] array = new byte[(InputFileStream.Length > 4L) ? 4L : InputFileStream.Length];
			InputFileStream.Read(array, 0, array.Length);
			Encoding encoding = EncodingDetector.DetectBOMBytes(array);
			bool flag = encoding != null;
			Encoding result;
			if (flag)
			{
				InputFileStream.Position = position;
				HasBOM = true;
				result = encoding;
			}
			else
			{
				byte[] array2 = new byte[(HeuristicSampleSize > InputFileStream.Length) ? InputFileStream.Length : HeuristicSampleSize];
				Array.Copy(array, array2, array.Length);
				bool flag2 = InputFileStream.Length > (long)array.Length;
				if (flag2)
				{
					InputFileStream.Read(array2, array.Length, array2.Length - array.Length);
				}
				InputFileStream.Position = position;
				encoding = EncodingDetector.DetectUnicodeInByteSampleByHeuristics(array2);
				HasBOM = false;
				result = encoding;
			}
			return result;
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x001D6324 File Offset: 0x001D6324
		public static Encoding DetectBOMBytes(byte[] BOMBytes)
		{
			bool flag = BOMBytes.Length < 2;
			Encoding result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = BOMBytes[0] == byte.MaxValue && BOMBytes[1] == 254 && (BOMBytes.Length < 4 || BOMBytes[2] != 0 || BOMBytes[3] > 0);
				if (flag2)
				{
					result = Encoding.Unicode;
				}
				else
				{
					bool flag3 = BOMBytes[0] == 254 && BOMBytes[1] == byte.MaxValue;
					if (flag3)
					{
						result = Encoding.BigEndianUnicode;
					}
					else
					{
						bool flag4 = BOMBytes.Length < 3;
						if (flag4)
						{
							result = null;
						}
						else
						{
							bool flag5 = BOMBytes[0] == 239 && BOMBytes[1] == 187 && BOMBytes[2] == 191;
							if (flag5)
							{
								result = Encoding.UTF8;
							}
							else
							{
								bool flag6 = BOMBytes[0] == 43 && BOMBytes[1] == 47 && BOMBytes[2] == 118;
								if (flag6)
								{
									result = Encoding.UTF7;
								}
								else
								{
									bool flag7 = BOMBytes.Length < 4;
									if (flag7)
									{
										result = null;
									}
									else
									{
										bool flag8 = BOMBytes[0] == byte.MaxValue && BOMBytes[1] == 254 && BOMBytes[2] == 0 && BOMBytes[3] == 0;
										if (flag8)
										{
											result = Encoding.UTF32;
										}
										else
										{
											bool flag9 = BOMBytes[0] == 0 && BOMBytes[1] == 0 && BOMBytes[2] == 254 && BOMBytes[3] == byte.MaxValue;
											if (flag9)
											{
												result = Encoding.GetEncoding(12001);
											}
											else
											{
												result = null;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x001D64F8 File Offset: 0x001D64F8
		public static Encoding DetectUnicodeInByteSampleByHeuristics(byte[] SampleBytes)
		{
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			long num4 = 0L;
			long num5 = 0L;
			long num6 = 0L;
			int num7 = 0;
			while (num6 < (long)SampleBytes.Length)
			{
				bool flag = SampleBytes[(int)(checked((IntPtr)num6))] == 0;
				if (flag)
				{
					bool flag2 = num6 % 2L == 0L;
					if (flag2)
					{
						num2 += 1L;
					}
					else
					{
						num += 1L;
					}
				}
				bool flag3 = EncodingDetector.IsCommonUSASCIIByte(SampleBytes[(int)(checked((IntPtr)num6))]);
				if (flag3)
				{
					num5 += 1L;
				}
				bool flag4 = num7 == 0;
				if (flag4)
				{
					int num8 = EncodingDetector.DetectSuspiciousUTF8SequenceLength(SampleBytes, num6);
					bool flag5 = num8 > 0;
					if (flag5)
					{
						num3 += 1L;
						num4 += (long)num8;
						num7 = num8 - 1;
					}
				}
				else
				{
					num7--;
				}
				num6 += 1L;
			}
			bool flag6 = (double)num2 * 2.0 / (double)SampleBytes.Length < 0.2 && (double)num * 2.0 / (double)SampleBytes.Length > 0.6;
			Encoding result;
			if (flag6)
			{
				result = Encoding.Unicode;
			}
			else
			{
				bool flag7 = (double)num * 2.0 / (double)SampleBytes.Length < 0.2 && (double)num2 * 2.0 / (double)SampleBytes.Length > 0.6;
				if (flag7)
				{
					result = Encoding.BigEndianUnicode;
				}
				else
				{
					string @string = Encoding.ASCII.GetString(SampleBytes);
					Regex regex = new Regex("\\A([\\x09\\x0A\\x0D\\x20-\\x7E]|[\\xC2-\\xDF][\\x80-\\xBF]|\\xE0[\\xA0-\\xBF][\\x80-\\xBF]|[\\xE1-\\xEC\\xEE\\xEF][\\x80-\\xBF]{2}|\\xED[\\x80-\\x9F][\\x80-\\xBF]|\\xF0[\\x90-\\xBF][\\x80-\\xBF]{2}|[\\xF1-\\xF3][\\x80-\\xBF]{3}|\\xF4[\\x80-\\x8F][\\x80-\\xBF]{2})*\\z");
					bool flag8 = regex.IsMatch(@string);
					if (flag8)
					{
						bool flag9 = (double)num3 * 500000.0 / (double)SampleBytes.Length >= 1.0 && ((long)SampleBytes.Length - num4 == 0L || (double)num5 * 1.0 / (double)((long)SampleBytes.Length - num4) >= 0.8);
						if (flag9)
						{
							return Encoding.UTF8;
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x001D6728 File Offset: 0x001D6728
		private static bool IsCommonUSASCIIByte(byte testByte)
		{
			return testByte == 10 || testByte == 13 || testByte == 9 || (testByte >= 32 && testByte <= 47) || (testByte >= 48 && testByte <= 57) || (testByte >= 58 && testByte <= 64) || (testByte >= 65 && testByte <= 90) || (testByte >= 91 && testByte <= 96) || (testByte >= 97 && testByte <= 122) || (testByte >= 123 && testByte <= 126);
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x001D67E4 File Offset: 0x001D67E4
		private static int DetectSuspiciousUTF8SequenceLength(byte[] SampleBytes, long currentPos)
		{
			int result = 0;
			bool flag = (long)SampleBytes.Length >= currentPos + 1L && SampleBytes[(int)(checked((IntPtr)currentPos))] == 194;
			checked
			{
				if (flag)
				{
					bool flag2 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 129 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 141 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 143;
					if (flag2)
					{
						result = 2;
					}
					else
					{
						bool flag3 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 144 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 157;
						if (flag3)
						{
							result = 2;
						}
						else
						{
							bool flag4 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] >= 160 && SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] <= 191;
							if (flag4)
							{
								result = 2;
							}
						}
					}
				}
				else
				{
					bool flag5 = unchecked((long)SampleBytes.Length >= currentPos + 1L) && SampleBytes[(int)((IntPtr)currentPos)] == 195;
					if (flag5)
					{
						bool flag6 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] >= 128 && SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] <= 191;
						if (flag6)
						{
							result = 2;
						}
					}
					else
					{
						bool flag7 = unchecked((long)SampleBytes.Length >= currentPos + 1L) && SampleBytes[(int)((IntPtr)currentPos)] == 197;
						if (flag7)
						{
							bool flag8 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 146 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 147;
							if (flag8)
							{
								result = 2;
							}
							else
							{
								bool flag9 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 160 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 161;
								if (flag9)
								{
									result = 2;
								}
								else
								{
									bool flag10 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 184 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 189 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 190;
									if (flag10)
									{
										result = 2;
									}
								}
							}
						}
						else
						{
							bool flag11 = unchecked((long)SampleBytes.Length >= currentPos + 1L) && SampleBytes[(int)((IntPtr)currentPos)] == 198;
							if (flag11)
							{
								bool flag12 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 146;
								if (flag12)
								{
									result = 2;
								}
							}
							else
							{
								bool flag13 = unchecked((long)SampleBytes.Length >= currentPos + 1L) && SampleBytes[(int)((IntPtr)currentPos)] == 203;
								if (flag13)
								{
									bool flag14 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 134 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 156;
									if (flag14)
									{
										result = 2;
									}
								}
								else
								{
									bool flag15 = unchecked((long)SampleBytes.Length >= currentPos + 2L) && SampleBytes[(int)((IntPtr)currentPos)] == 226;
									if (flag15)
									{
										bool flag16 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 128;
										if (flag16)
										{
											bool flag17 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 147 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 148;
											if (flag17)
											{
												result = 3;
											}
											bool flag18 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 152 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 153 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 154;
											if (flag18)
											{
												result = 3;
											}
											bool flag19 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 156 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 157 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 158;
											if (flag19)
											{
												result = 3;
											}
											bool flag20 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 160 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 161 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 162;
											if (flag20)
											{
												result = 3;
											}
											bool flag21 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 166;
											if (flag21)
											{
												result = 3;
											}
											bool flag22 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 176;
											if (flag22)
											{
												result = 3;
											}
											bool flag23 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 185 || SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 186;
											if (flag23)
											{
												result = 3;
											}
										}
										else
										{
											bool flag24 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 130 && SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 172;
											if (flag24)
											{
												result = 3;
											}
											else
											{
												bool flag25 = SampleBytes[(int)((IntPtr)(unchecked(currentPos + 1L)))] == 132 && SampleBytes[(int)((IntPtr)(unchecked(currentPos + 2L)))] == 162;
												if (flag25)
												{
													result = 3;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x0400323F RID: 12863
		private const long _defaultHeuristicSampleSize = 65536L;
	}
}
