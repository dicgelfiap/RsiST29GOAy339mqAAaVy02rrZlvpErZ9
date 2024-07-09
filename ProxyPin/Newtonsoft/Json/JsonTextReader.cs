using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000A8C RID: 2700
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonTextReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x060069FE RID: 27134 RVA: 0x001FFAB8 File Offset: 0x001FFAB8
		public override Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsync(cancellationToken);
			}
			return this.DoReadAsync(cancellationToken);
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x001FFAD4 File Offset: 0x001FFAD4
		internal Task<bool> DoReadAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			Task<bool> task;
			for (;;)
			{
				switch (this._currentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_4C;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_54;
				case JsonReader.State.PostValue:
					task = this.ParsePostValueAsync(false, cancellationToken);
					if (!task.IsCompletedSucessfully())
					{
						goto IL_7E;
					}
					if (task.Result)
					{
						goto Block_3;
					}
					continue;
				case JsonReader.State.Finished:
					goto IL_87;
				}
				break;
			}
			goto IL_8F;
			IL_4C:
			return this.ParseValueAsync(cancellationToken);
			IL_54:
			return this.ParseObjectAsync(cancellationToken);
			Block_3:
			return AsyncUtils.True;
			IL_7E:
			return this.DoReadAsync(task, cancellationToken);
			IL_87:
			return this.ReadFromFinishedAsync(cancellationToken);
			IL_8F:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A00 RID: 27136 RVA: 0x001FFB94 File Offset: 0x001FFB94
		private async Task<bool> DoReadAsync(Task<bool> task, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool result;
			if (configuredTaskAwaiter.GetResult())
			{
				result = true;
			}
			else
			{
				result = await this.DoReadAsync(cancellationToken).ConfigureAwait(false);
			}
			return result;
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x001FFBF0 File Offset: 0x001FFBF0
		private async Task<bool> ParsePostValueAsync(bool ignoreComments, CancellationToken cancellationToken)
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= ')')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_2EF;
							case '\r':
								await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
								continue;
							default:
								goto IL_2EF;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter.GetResult() == 0)
							{
								break;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_2EF;
						}
						goto IL_193;
					}
					this._charPos++;
					continue;
				}
				if (c <= '/')
				{
					if (c == ',')
					{
						goto IL_23E;
					}
					if (c == '/')
					{
						await this.ParseCommentAsync(!ignoreComments, cancellationToken).ConfigureAwait(false);
						if (!ignoreComments)
						{
							goto Block_14;
						}
						continue;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_175;
					}
					if (c == '}')
					{
						goto IL_157;
					}
				}
				IL_2EF:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_30D;
				}
				this._charPos++;
			}
			this._currentState = JsonReader.State.Finished;
			return false;
			IL_157:
			this._charPos++;
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_175:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_193:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			Block_14:
			return true;
			IL_23E:
			this._charPos++;
			base.SetStateBasedOnCurrent();
			return false;
			IL_30D:
			if (!base.SupportMultipleContent || this.Depth != 0)
			{
				char c;
				throw JsonReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
			}
			base.SetStateBasedOnCurrent();
			return false;
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x001FFC4C File Offset: 0x001FFC4C
		private async Task<bool> ReadFromFinishedAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool result;
			if (configuredTaskAwaiter.GetResult())
			{
				await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
				if (this._isEndOfFile)
				{
					base.SetToken(JsonToken.None);
					result = false;
				}
				else
				{
					if (this._chars[this._charPos] != '/')
					{
						throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
					}
					await this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false);
					result = true;
				}
			}
			else
			{
				base.SetToken(JsonToken.None);
				result = false;
			}
			return result;
		}

		// Token: 0x06006A03 RID: 27139 RVA: 0x001FFCA0 File Offset: 0x001FFCA0
		private Task<int> ReadDataAsync(bool append, CancellationToken cancellationToken)
		{
			return this.ReadDataAsync(append, 0, cancellationToken);
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x001FFCAC File Offset: 0x001FFCAC
		private async Task<int> ReadDataAsync(bool append, int charsRequired, CancellationToken cancellationToken)
		{
			int result;
			if (this._isEndOfFile)
			{
				result = 0;
			}
			else
			{
				this.PrepareBufferForReadData(append, charsRequired);
				int num = await this._reader.ReadAsync(this._chars, this._charsUsed, this._chars.Length - this._charsUsed - 1, cancellationToken).ConfigureAwait(false);
				this._charsUsed += num;
				if (num == 0)
				{
					this._isEndOfFile = true;
				}
				this._chars[this._charsUsed] = '\0';
				result = num;
			}
			return result;
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x001FFD10 File Offset: 0x001FFD10
		private async Task<bool> ParseValueAsync(CancellationToken cancellationToken)
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'N')
				{
					if (c <= ' ')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_9A1;
							case '\r':
								await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
								continue;
							default:
								if (c != ' ')
								{
									goto IL_9A1;
								}
								break;
							}
							this._charPos++;
							continue;
						}
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() == 0)
						{
							break;
						}
						continue;
					}
					else if (c <= '/')
					{
						if (c == '"')
						{
							goto IL_1F2;
						}
						switch (c)
						{
						case '\'':
							goto IL_1F2;
						case ')':
							goto IL_8ED;
						case ',':
							goto IL_8DD;
						case '-':
							goto IL_60D;
						case '/':
							goto IL_790;
						}
					}
					else
					{
						if (c == 'I')
						{
							goto IL_592;
						}
						if (c == 'N')
						{
							goto IL_517;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == '[')
					{
						goto IL_8A2;
					}
					if (c == ']')
					{
						goto IL_8BF;
					}
					if (c == 'f')
					{
						goto IL_2E6;
					}
				}
				else if (c <= 't')
				{
					if (c == 'n')
					{
						goto IL_35F;
					}
					if (c == 't')
					{
						goto IL_26D;
					}
				}
				else
				{
					if (c == 'u')
					{
						goto IL_80B;
					}
					if (c == '{')
					{
						goto IL_885;
					}
				}
				IL_9A1:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_9BF;
				}
				this._charPos++;
			}
			return false;
			IL_1F2:
			await this.ParseStringAsync(c, ReadType.Read, cancellationToken).ConfigureAwait(false);
			return true;
			IL_26D:
			await this.ParseTrueAsync(cancellationToken).ConfigureAwait(false);
			return true;
			IL_2E6:
			await this.ParseFalseAsync(cancellationToken).ConfigureAwait(false);
			return true;
			IL_35F:
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter3.GetResult())
			{
				char c2 = this._chars[this._charPos + 1];
				if (c2 != 'e')
				{
					if (c2 != 'u')
					{
						throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
					}
					await this.ParseNullAsync(cancellationToken).ConfigureAwait(false);
				}
				else
				{
					await this.ParseConstructorAsync(cancellationToken).ConfigureAwait(false);
				}
				return true;
			}
			this._charPos++;
			throw base.CreateUnexpectedEndException();
			IL_517:
			await this.ParseNumberNaNAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			return true;
			IL_592:
			await this.ParseNumberPositiveInfinityAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			return true;
			IL_60D:
			configuredTaskAwaiter3 = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter3.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				await this.ParseNumberNegativeInfinityAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				await this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			}
			return true;
			IL_790:
			await this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false);
			return true;
			IL_80B:
			await this.ParseUndefinedAsync(cancellationToken).ConfigureAwait(false);
			return true;
			IL_885:
			this._charPos++;
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_8A2:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_8BF:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_8DD:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_8ED:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_9BF:
			if (!char.IsNumber(c) && c != '-' && c != '.')
			{
				throw this.CreateUnexpectedCharacterException(c);
			}
			await this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			return true;
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x001FFD64 File Offset: 0x001FFD64
		private async Task ReadStringIntoBufferAsync(char quote, CancellationToken cancellationToken)
		{
			int charPos = this._charPos;
			int initialPosition = this._charPos;
			int lastWritePosition = this._charPos;
			this._stringBuffer.Position = 0;
			char c2;
			for (;;)
			{
				char[] chars = this._chars;
				int num = charPos;
				charPos = num + 1;
				char c = chars[num];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						if (c != '\n')
						{
							if (c == '\r')
							{
								this._charPos = charPos - 1;
								await this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false);
								charPos = this._charPos;
							}
						}
						else
						{
							this._charPos = charPos - 1;
							this.ProcessLineFeed();
							charPos = this._charPos;
						}
					}
					else if (this._charsUsed == charPos - 1)
					{
						num = charPos;
						charPos = num - 1;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() == 0)
						{
							break;
						}
					}
				}
				else if (c != '"' && c != '\'')
				{
					if (c == '\\')
					{
						this._charPos = charPos;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.EnsureCharsAsync(0, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (!configuredTaskAwaiter3.GetResult())
						{
							goto Block_12;
						}
						int escapeStartPos = charPos - 1;
						c2 = this._chars[charPos];
						num = charPos;
						charPos = num + 1;
						char writeChar;
						if (c2 <= '\\')
						{
							if (c2 <= '\'')
							{
								if (c2 != '"' && c2 != '\'')
								{
									goto Block_16;
								}
							}
							else if (c2 != '/')
							{
								if (c2 != '\\')
								{
									goto Block_18;
								}
								writeChar = '\\';
								goto IL_5E7;
							}
							writeChar = c2;
						}
						else if (c2 <= 'f')
						{
							if (c2 != 'b')
							{
								if (c2 != 'f')
								{
									goto Block_21;
								}
								writeChar = '\f';
							}
							else
							{
								writeChar = '\b';
							}
						}
						else
						{
							if (c2 != 'n')
							{
								switch (c2)
								{
								case 'r':
									writeChar = '\r';
									goto IL_5E7;
								case 't':
									writeChar = '\t';
									goto IL_5E7;
								case 'u':
									this._charPos = charPos;
									writeChar = await this.ParseUnicodeAsync(cancellationToken).ConfigureAwait(false);
									if (StringUtils.IsLowSurrogate(writeChar))
									{
										writeChar = '�';
									}
									else if (StringUtils.IsHighSurrogate(writeChar))
									{
										bool anotherHighSurrogate;
										do
										{
											anotherHighSurrogate = false;
											configuredTaskAwaiter3 = this.EnsureCharsAsync(2, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
											if (!configuredTaskAwaiter3.IsCompleted)
											{
												await configuredTaskAwaiter3;
												configuredTaskAwaiter3 = configuredTaskAwaiter4;
												configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
											}
											if (configuredTaskAwaiter3.GetResult() && this._chars[this._charPos] == '\\' && this._chars[this._charPos + 1] == 'u')
											{
												char highSurrogate = writeChar;
												this._charPos += 2;
												writeChar = await this.ParseUnicodeAsync(cancellationToken).ConfigureAwait(false);
												if (!StringUtils.IsLowSurrogate(writeChar))
												{
													if (StringUtils.IsHighSurrogate(writeChar))
													{
														highSurrogate = '�';
														anotherHighSurrogate = true;
													}
													else
													{
														highSurrogate = '�';
													}
												}
												this.EnsureBufferNotEmpty();
												this.WriteCharToBuffer(highSurrogate, lastWritePosition, escapeStartPos);
												lastWritePosition = this._charPos;
											}
											else
											{
												writeChar = '�';
											}
										}
										while (anotherHighSurrogate);
									}
									charPos = this._charPos;
									goto IL_5E7;
								}
								goto Block_23;
							}
							writeChar = '\n';
						}
						IL_5E7:
						this.EnsureBufferNotEmpty();
						this.WriteCharToBuffer(writeChar, lastWritePosition, escapeStartPos);
						lastWritePosition = charPos;
					}
				}
				else if (this._chars[charPos - 1] == quote)
				{
					goto Block_31;
				}
			}
			this._charPos = charPos;
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_12:
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_16:
			Block_18:
			Block_21:
			Block_23:
			this._charPos = charPos;
			throw JsonReaderException.Create(this, "Bad JSON escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, "\\" + c2.ToString()));
			Block_31:
			this.FinishReadStringIntoBuffer(charPos - 1, initialPosition, lastWritePosition);
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x001FFDC0 File Offset: 0x001FFDC0
		private Task ProcessCarriageReturnAsync(bool append, CancellationToken cancellationToken)
		{
			this._charPos++;
			Task<bool> task = this.EnsureCharsAsync(1, append, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				this.SetNewLine(task.Result);
				return AsyncUtils.CompletedTask;
			}
			return this.ProcessCarriageReturnAsync(task);
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x001FFE10 File Offset: 0x001FFE10
		private async Task ProcessCarriageReturnAsync(Task<bool> task)
		{
			this.SetNewLine(await task.ConfigureAwait(false));
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x001FFE64 File Offset: 0x001FFE64
		private async Task<char> ParseUnicodeAsync(CancellationToken cancellationToken)
		{
			return this.ConvertUnicode(await this.EnsureCharsAsync(4, true, cancellationToken).ConfigureAwait(false));
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x001FFEB8 File Offset: 0x001FFEB8
		private Task<bool> EnsureCharsAsync(int relativePosition, bool append, CancellationToken cancellationToken)
		{
			if (this._charPos + relativePosition < this._charsUsed)
			{
				return AsyncUtils.True;
			}
			if (this._isEndOfFile)
			{
				return AsyncUtils.False;
			}
			return this.ReadCharsAsync(relativePosition, append, cancellationToken);
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x001FFEF0 File Offset: 0x001FFEF0
		private async Task<bool> ReadCharsAsync(int relativePosition, bool append, CancellationToken cancellationToken)
		{
			int charsRequired = this._charPos + relativePosition - this._charsUsed + 1;
			for (;;)
			{
				int num = await this.ReadDataAsync(append, charsRequired, cancellationToken).ConfigureAwait(false);
				if (num == 0)
				{
					break;
				}
				charsRequired -= num;
				if (charsRequired <= 0)
				{
					goto Block_2;
				}
			}
			return false;
			Block_2:
			return true;
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x001FFF54 File Offset: 0x001FFF54
		private async Task<bool> ParseObjectAsync(CancellationToken cancellationToken)
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_24E;
						case '\r':
							await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
							continue;
						default:
							goto IL_24E;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != ' ')
				{
					if (c == '/')
					{
						goto IL_13F;
					}
					if (c != '}')
					{
						goto IL_24E;
					}
					goto IL_121;
				}
				this._charPos++;
				continue;
				IL_24E:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_26C;
				}
				this._charPos++;
			}
			return false;
			IL_121:
			base.SetToken(JsonToken.EndObject);
			this._charPos++;
			return true;
			IL_13F:
			await this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false);
			return true;
			IL_26C:
			return await this.ParsePropertyAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x001FFFA8 File Offset: 0x001FFFA8
		private async Task ParseCommentAsync(bool setToken, CancellationToken cancellationToken)
		{
			this._charPos++;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(1, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			bool singlelineComment;
			if (this._chars[this._charPos] == '*')
			{
				singlelineComment = false;
			}
			else
			{
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Error parsing comment. Expected: *, got {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				singlelineComment = true;
			}
			this._charPos++;
			int initialPosition = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							if (singlelineComment)
							{
								goto Block_19;
							}
							this.ProcessLineFeed();
							continue;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter3.GetResult() == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != '\r')
				{
					if (c == '*')
					{
						this._charPos++;
						if (singlelineComment)
						{
							continue;
						}
						configuredTaskAwaiter = this.EnsureCharsAsync(0, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos] == '/')
						{
							goto Block_17;
						}
						continue;
					}
				}
				else
				{
					if (singlelineComment)
					{
						goto Block_18;
					}
					await this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false);
					continue;
				}
				this._charPos++;
			}
			if (!singlelineComment)
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			this.EndComment(setToken, initialPosition, this._charPos);
			return;
			Block_17:
			this.EndComment(setToken, initialPosition, this._charPos - 1);
			this._charPos++;
			return;
			Block_18:
			this.EndComment(setToken, initialPosition, this._charPos);
			return;
			Block_19:
			this.EndComment(setToken, initialPosition, this._charPos);
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x00200004 File Offset: 0x00200004
		private async Task EatWhitespaceAsync(CancellationToken cancellationToken)
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c != '\0')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c != ' ' && !char.IsWhiteSpace(c))
							{
								break;
							}
							this._charPos++;
						}
						else
						{
							await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
						}
					}
					else
					{
						this.ProcessLineFeed();
					}
				}
				else if (this._charsUsed == this._charPos)
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					this._charPos++;
				}
			}
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x00200058 File Offset: 0x00200058
		private async Task ParseStringAsync(char quote, ReadType readType, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this._charPos++;
			this.ShiftBufferIfNeeded();
			await this.ReadStringIntoBufferAsync(quote, cancellationToken).ConfigureAwait(false);
			this.ParseReadString(quote, readType);
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x002000BC File Offset: 0x002000BC
		private async Task<bool> MatchValueAsync(string value, CancellationToken cancellationToken)
		{
			return this.MatchValue(await this.EnsureCharsAsync(value.Length - 1, true, cancellationToken).ConfigureAwait(false), value);
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x00200118 File Offset: 0x00200118
		private async Task<bool> MatchValueWithTrailingSeparatorAsync(string value, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool result;
			if (!configuredTaskAwaiter.GetResult())
			{
				result = false;
			}
			else
			{
				configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					result = true;
				}
				else
				{
					result = (this.IsSeparator(this._chars[this._charPos]) || this._chars[this._charPos] == '\0');
				}
			}
			return result;
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x00200174 File Offset: 0x00200174
		private async Task MatchAndSetAsync(string value, JsonToken newToken, [Nullable(2)] object tokenValue, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult())
			{
				base.SetToken(newToken, tokenValue);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing " + newToken.ToString().ToLowerInvariant() + " value.");
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x002001E0 File Offset: 0x002001E0
		private Task ParseTrueAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.True, JsonToken.Boolean, true, cancellationToken);
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x002001F8 File Offset: 0x002001F8
		private Task ParseFalseAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.False, JsonToken.Boolean, false, cancellationToken);
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x00200210 File Offset: 0x00200210
		private Task ParseNullAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.Null, JsonToken.Null, null, cancellationToken);
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x00200224 File Offset: 0x00200224
		private async Task ParseConstructorAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync("new", cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
			}
			await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
			int initialPosition = this._charPos;
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_1C6;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						await configuredTaskAwaiter3;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
						configuredTaskAwaiter3 = configuredTaskAwaiter4;
						configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter3.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						goto IL_204;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
			IL_1C6:
			int endPosition = this._charPos;
			this._charPos++;
			goto IL_312;
			IL_204:
			if (c == '\r')
			{
				endPosition = this._charPos;
				await this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false);
			}
			else if (c == '\n')
			{
				endPosition = this._charPos;
				this.ProcessLineFeed();
			}
			else if (char.IsWhiteSpace(c))
			{
				endPosition = this._charPos;
				this._charPos++;
			}
			else
			{
				if (c != '(')
				{
					throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
				}
				endPosition = this._charPos;
			}
			IL_312:
			this._stringReference = new StringReference(this._chars, initialPosition, endPosition - initialPosition);
			string constructorName = this._stringReference.ToString();
			await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
			if (this._chars[this._charPos] != '(')
			{
				throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			this.ClearRecentString();
			base.SetToken(JsonToken.StartConstructor, constructorName);
			constructorName = null;
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x00200278 File Offset: 0x00200278
		private async Task<object> ParseNumberNaNAsync(ReadType readType, CancellationToken cancellationToken)
		{
			return this.ParseNumberNaN(readType, await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.NaN, cancellationToken).ConfigureAwait(false));
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x002002D4 File Offset: 0x002002D4
		private async Task<object> ParseNumberPositiveInfinityAsync(ReadType readType, CancellationToken cancellationToken)
		{
			return this.ParseNumberPositiveInfinity(readType, await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.PositiveInfinity, cancellationToken).ConfigureAwait(false));
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x00200330 File Offset: 0x00200330
		private async Task<object> ParseNumberNegativeInfinityAsync(ReadType readType, CancellationToken cancellationToken)
		{
			return this.ParseNumberNegativeInfinity(readType, await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.NegativeInfinity, cancellationToken).ConfigureAwait(false));
		}

		// Token: 0x06006A1A RID: 27162 RVA: 0x0020038C File Offset: 0x0020038C
		private async Task ParseNumberAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.ShiftBufferIfNeeded();
			char firstChar = this._chars[this._charPos];
			int initialPosition = this._charPos;
			await this.ReadNumberIntoBufferAsync(cancellationToken).ConfigureAwait(false);
			this.ParseReadNumber(readType, firstChar, initialPosition);
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x002003E8 File Offset: 0x002003E8
		private Task ParseUndefinedAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.Undefined, JsonToken.Undefined, null, cancellationToken);
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x002003FC File Offset: 0x002003FC
		private async Task<bool> ParsePropertyAsync(CancellationToken cancellationToken)
		{
			char c = this._chars[this._charPos];
			char quoteChar;
			if (c == '"' || c == '\'')
			{
				this._charPos++;
				quoteChar = c;
				this.ShiftBufferIfNeeded();
				await this.ReadStringIntoBufferAsync(quoteChar, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				if (!this.ValidIdentifierChar(c))
				{
					throw JsonReaderException.Create(this, "Invalid property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				quoteChar = '\0';
				this.ShiftBufferIfNeeded();
				await this.ParseUnquotedPropertyAsync(cancellationToken).ConfigureAwait(false);
			}
			string propertyName;
			if (this.PropertyNameTable != null)
			{
				propertyName = (this.PropertyNameTable.Get(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length) ?? this._stringReference.ToString());
			}
			else
			{
				propertyName = this._stringReference.ToString();
			}
			await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
			if (this._chars[this._charPos] != ':')
			{
				throw JsonReaderException.Create(this, "Invalid character after parsing property name. Expected ':' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			base.SetToken(JsonToken.PropertyName, propertyName);
			this._quoteChar = quoteChar;
			this.ClearRecentString();
			return true;
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x00200450 File Offset: 0x00200450
		private async Task ReadNumberIntoBufferAsync(CancellationToken cancellationToken)
		{
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[charPos];
				if (c == '\0')
				{
					this._charPos = charPos;
					if (this._charsUsed != charPos)
					{
						break;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					if (this.ReadNumberCharIntoBuffer(c, charPos))
					{
						break;
					}
					charPos++;
				}
			}
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x002004A4 File Offset: 0x002004A4
		private async Task ParseUnquotedPropertyAsync(CancellationToken cancellationToken)
		{
			int initialPosition = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_C7;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() == 0)
					{
						break;
					}
				}
				else if (this.ReadUnquotedPropertyReportIfDone(c, initialPosition))
				{
					return;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
			IL_C7:
			this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x002004F8 File Offset: 0x002004F8
		private async Task<bool> ReadNullCharAsync(CancellationToken cancellationToken)
		{
			if (this._charsUsed == this._charPos)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult() == 0)
				{
					this._isEndOfFile = true;
					return true;
				}
			}
			else
			{
				this._charPos++;
			}
			return false;
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x0020054C File Offset: 0x0020054C
		private async Task HandleNullAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			if (this._chars[this._charPos + 1] == 'u')
			{
				await this.ParseNullAsync(cancellationToken).ConfigureAwait(false);
				return;
			}
			this._charPos += 2;
			throw this.CreateUnexpectedCharacterException(this._chars[this._charPos - 1]);
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x002005A0 File Offset: 0x002005A0
		private async Task ReadFinishedAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult())
			{
				await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
				if (this._isEndOfFile)
				{
					base.SetToken(JsonToken.None);
					return;
				}
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
			}
			base.SetToken(JsonToken.None);
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x002005F4 File Offset: 0x002005F4
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		private async Task<object> ReadStringValueAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_963;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				await this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false);
				return null;
			default:
				goto IL_963;
			}
			char c;
			string expected;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'I')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								goto IL_8DD;
							case '\v':
							case '\f':
								goto IL_8BA;
							case '\r':
								await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
								goto IL_8DD;
							default:
								goto IL_8BA;
							}
						}
						else
						{
							configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter.GetResult())
							{
								break;
							}
							goto IL_8DD;
						}
					}
					else
					{
						switch (c)
						{
						case ' ':
							break;
						case '!':
						case '#':
						case '$':
						case '%':
						case '&':
						case '(':
						case ')':
						case '*':
						case '+':
							goto IL_8BA;
						case '"':
						case '\'':
							goto IL_2A3;
						case ',':
							this.ProcessValueComma();
							goto IL_8DD;
						case '-':
							goto IL_32F;
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_453;
						case '/':
							await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
							goto IL_8DD;
						default:
							if (c != 'I')
							{
								goto IL_8BA;
							}
							goto IL_5E0;
						}
					}
					this._charPos++;
				}
				else if (c <= ']')
				{
					if (c == 'N')
					{
						goto IL_65E;
					}
					if (c != ']')
					{
						goto IL_8BA;
					}
					goto IL_7D9;
				}
				else
				{
					if (c == 'f')
					{
						goto IL_4F5;
					}
					if (c == 'n')
					{
						goto IL_6DC;
					}
					if (c != 't')
					{
						goto IL_8BA;
					}
					goto IL_4F5;
				}
				IL_8DD:
				expected = null;
				continue;
				IL_8BA:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_28;
				}
				goto IL_8DD;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_2A3:
			await this.ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(false);
			return this.FinishReadQuotedStringValue(readType);
			IL_32F:
			configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			await this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false);
			return this.Value;
			IL_453:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			await this.ParseNumberAsync(ReadType.ReadAsString, cancellationToken).ConfigureAwait(false);
			return this.Value;
			IL_4F5:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			expected = ((c == 't') ? JsonConvert.True : JsonConvert.False);
			configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(expected, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.String, expected);
			return expected;
			IL_5E0:
			return await this.ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(false);
			IL_65E:
			return await this.ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(false);
			IL_6DC:
			await this.HandleNullAsync(cancellationToken).ConfigureAwait(false);
			return null;
			IL_7D9:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_28:
			throw this.CreateUnexpectedCharacterException(c);
			IL_963:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x00200650 File Offset: 0x00200650
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		private async Task<object> ReadNumberValueAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_899;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				await this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false);
				return null;
			default:
				goto IL_899;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_7FC;
						case '\r':
							await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_7FC;
							case '"':
							case '\'':
								goto IL_283;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
								goto IL_484;
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_613;
							case '/':
								await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
								continue;
							default:
								goto IL_7FC;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult())
					{
						break;
					}
					continue;
				}
				else if (c <= 'N')
				{
					if (c == 'I')
					{
						goto IL_406;
					}
					if (c == 'N')
					{
						goto IL_388;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_71B;
					}
					if (c == 'n')
					{
						goto IL_30F;
					}
				}
				IL_7FC:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_20;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_283:
			await this.ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(false);
			return this.FinishReadQuotedNumber(readType);
			IL_30F:
			await this.HandleNullAsync(cancellationToken).ConfigureAwait(false);
			return null;
			IL_388:
			return await this.ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(false);
			IL_406:
			return await this.ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(false);
			IL_484:
			configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				return await this.ParseNumberNegativeInfinityAsync(readType, cancellationToken).ConfigureAwait(false);
			}
			await this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false);
			return this.Value;
			IL_613:
			await this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false);
			return this.Value;
			IL_71B:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_20:
			throw this.CreateUnexpectedCharacterException(c);
			IL_899:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x002006AC File Offset: 0x002006AC
		public override Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsBooleanAsync(cancellationToken);
			}
			return this.DoReadAsBooleanAsync(cancellationToken);
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x002006C8 File Offset: 0x002006C8
		internal async Task<bool?> DoReadAsBooleanAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_751;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				await this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false);
				return null;
			default:
				goto IL_751;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							goto IL_6C2;
						case '\v':
						case '\f':
							goto IL_69F;
						case '\r':
							await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
							goto IL_6C2;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_69F;
							case '"':
							case '\'':
								goto IL_27D;
							case ',':
								this.ProcessValueComma();
								goto IL_6C2;
							case '-':
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_38D;
							case '/':
								await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
								goto IL_6C2;
							default:
								goto IL_69F;
							}
							break;
						}
						this._charPos++;
					}
					else
					{
						configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult())
						{
							break;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == ']')
					{
						goto IL_5BA;
					}
					if (c != 'f')
					{
						goto IL_69F;
					}
					goto IL_464;
				}
				else
				{
					if (c == 'n')
					{
						goto IL_30F;
					}
					if (c != 't')
					{
						goto IL_69F;
					}
					goto IL_464;
				}
				IL_6C2:
				BigInteger i = default(BigInteger);
				continue;
				IL_69F:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_21;
				}
				goto IL_6C2;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_27D:
			await this.ParseStringAsync(c, ReadType.Read, cancellationToken).ConfigureAwait(false);
			return base.ReadBooleanString(this._stringReference.ToString());
			IL_30F:
			await this.HandleNullAsync(cancellationToken).ConfigureAwait(false);
			return null;
			IL_38D:
			await this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false);
			object value = this.Value;
			bool flag;
			if (value is BigInteger)
			{
				BigInteger i = (BigInteger)value;
				flag = (i != 0L);
			}
			else
			{
				flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
			}
			base.SetToken(JsonToken.Boolean, flag, false);
			return new bool?(flag);
			IL_464:
			bool isTrue = c == 't';
			configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(isTrue ? JsonConvert.True : JsonConvert.False, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.Boolean, isTrue);
			return new bool?(isTrue);
			IL_5BA:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_21:
			throw this.CreateUnexpectedCharacterException(c);
			IL_751:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x0020071C File Offset: 0x0020071C
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public override Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsBytesAsync(cancellationToken);
			}
			return this.DoReadAsBytesAsync(cancellationToken);
		}

		// Token: 0x06006A27 RID: 27175 RVA: 0x00200738 File Offset: 0x00200738
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		internal async Task<byte[]> DoReadAsBytesAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			bool isWrapped = false;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_735;
			case JsonReader.State.PostValue:
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			}
			case JsonReader.State.Finished:
				await this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false);
				return null;
			default:
				goto IL_735;
			}
			char c;
			byte[] data;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '\'')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								goto IL_6AF;
							case '\v':
							case '\f':
								goto IL_68C;
							case '\r':
								await this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false);
								goto IL_6AF;
							default:
								goto IL_68C;
							}
						}
						else
						{
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter.GetResult())
							{
								break;
							}
							goto IL_6AF;
						}
					}
					else if (c != ' ')
					{
						if (c != '"' && c != '\'')
						{
							goto IL_68C;
						}
						goto IL_247;
					}
					this._charPos++;
				}
				else if (c <= '[')
				{
					if (c != ',')
					{
						if (c != '/')
						{
							if (c != '[')
							{
								goto IL_68C;
							}
							goto IL_424;
						}
						else
						{
							await this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false);
						}
					}
					else
					{
						this.ProcessValueComma();
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_5AC;
					}
					if (c == 'n')
					{
						goto IL_4B1;
					}
					if (c != '{')
					{
						goto IL_68C;
					}
					this._charPos++;
					base.SetToken(JsonToken.StartObject);
					await this.ReadIntoWrappedTypeObjectAsync(cancellationToken).ConfigureAwait(false);
					isWrapped = true;
				}
				IL_6AF:
				data = null;
				continue;
				IL_68C:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_24;
				}
				goto IL_6AF;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_247:
			await this.ParseStringAsync(c, ReadType.ReadAsBytes, cancellationToken).ConfigureAwait(false);
			data = (byte[])this.Value;
			if (isWrapped)
			{
				await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
				if (this.TokenType != JsonToken.EndObject)
				{
					throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
				}
				base.SetToken(JsonToken.Bytes, data, false);
			}
			return data;
			IL_424:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return await base.ReadArrayIntoByteArrayAsync(cancellationToken).ConfigureAwait(false);
			IL_4B1:
			await this.HandleNullAsync(cancellationToken).ConfigureAwait(false);
			return null;
			IL_5AC:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_24:
			throw this.CreateUnexpectedCharacterException(c);
			IL_735:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x0020078C File Offset: 0x0020078C
		private async Task ReadIntoWrappedTypeObjectAsync(CancellationToken cancellationToken)
		{
			await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
			if (this.Value != null && this.Value.ToString() == "$type")
			{
				await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
				if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]", StringComparison.Ordinal))
				{
					await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
					if (this.Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x002007E0 File Offset: 0x002007E0
		public override Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDateTimeAsync(cancellationToken);
			}
			return this.DoReadAsDateTimeAsync(cancellationToken);
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x002007FC File Offset: 0x002007FC
		internal async Task<DateTime?> DoReadAsDateTimeAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsDateTime, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (DateTime?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06006A2B RID: 27179 RVA: 0x00200850 File Offset: 0x00200850
		public override Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDateTimeOffsetAsync(cancellationToken);
			}
			return this.DoReadAsDateTimeOffsetAsync(cancellationToken);
		}

		// Token: 0x06006A2C RID: 27180 RVA: 0x0020086C File Offset: 0x0020086C
		internal async Task<DateTimeOffset?> DoReadAsDateTimeOffsetAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsDateTimeOffset, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (DateTimeOffset?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06006A2D RID: 27181 RVA: 0x002008C0 File Offset: 0x002008C0
		public override Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDecimalAsync(cancellationToken);
			}
			return this.DoReadAsDecimalAsync(cancellationToken);
		}

		// Token: 0x06006A2E RID: 27182 RVA: 0x002008DC File Offset: 0x002008DC
		internal async Task<decimal?> DoReadAsDecimalAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsDecimal, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (decimal?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x00200930 File Offset: 0x00200930
		public override Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDoubleAsync(cancellationToken);
			}
			return this.DoReadAsDoubleAsync(cancellationToken);
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x0020094C File Offset: 0x0020094C
		internal async Task<double?> DoReadAsDoubleAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsDouble, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (double?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x002009A0 File Offset: 0x002009A0
		public override Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsInt32Async(cancellationToken);
			}
			return this.DoReadAsInt32Async(cancellationToken);
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x002009BC File Offset: 0x002009BC
		internal async Task<int?> DoReadAsInt32Async(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsInt32, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (int?)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06006A33 RID: 27187 RVA: 0x00200A10 File Offset: 0x00200A10
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public override Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsStringAsync(cancellationToken);
			}
			return this.DoReadAsStringAsync(cancellationToken);
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x00200A2C File Offset: 0x00200A2C
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		internal async Task<string> DoReadAsStringAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsString, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (string)configuredTaskAwaiter.GetResult();
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x00200A80 File Offset: 0x00200A80
		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this._reader = reader;
			this._lineNumber = 1;
			this._safeAsync = (base.GetType() == typeof(JsonTextReader));
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06006A36 RID: 27190 RVA: 0x00200AD4 File Offset: 0x00200AD4
		// (set) Token: 0x06006A37 RID: 27191 RVA: 0x00200ADC File Offset: 0x00200ADC
		[Nullable(2)]
		public JsonNameTable PropertyNameTable { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06006A38 RID: 27192 RVA: 0x00200AE8 File Offset: 0x00200AE8
		// (set) Token: 0x06006A39 RID: 27193 RVA: 0x00200AF0 File Offset: 0x00200AF0
		[Nullable(2)]
		public IArrayPool<char> ArrayPool
		{
			[NullableContext(2)]
			get
			{
				return this._arrayPool;
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._arrayPool = value;
			}
		}

		// Token: 0x06006A3A RID: 27194 RVA: 0x00200B0C File Offset: 0x00200B0C
		private void EnsureBufferNotEmpty()
		{
			if (this._stringBuffer.IsEmpty)
			{
				this._stringBuffer = new StringBuffer(this._arrayPool, 1024);
			}
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x00200B34 File Offset: 0x00200B34
		private void SetNewLine(bool hasNextChar)
		{
			if (hasNextChar && this._chars[this._charPos] == '\n')
			{
				this._charPos++;
			}
			this.OnNewLine(this._charPos);
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x00200B6C File Offset: 0x00200B6C
		private void OnNewLine(int pos)
		{
			this._lineNumber++;
			this._lineStartPos = pos;
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x00200B84 File Offset: 0x00200B84
		private void ParseString(char quote, ReadType readType)
		{
			this._charPos++;
			this.ShiftBufferIfNeeded();
			this.ReadStringIntoBuffer(quote);
			this.ParseReadString(quote, readType);
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x00200BAC File Offset: 0x00200BAC
		private void ParseReadString(char quote, ReadType readType)
		{
			base.SetPostValueState(true);
			switch (readType)
			{
			case ReadType.ReadAsInt32:
			case ReadType.ReadAsDecimal:
			case ReadType.ReadAsBoolean:
				return;
			case ReadType.ReadAsBytes:
			{
				byte[] value;
				Guid guid;
				if (this._stringReference.Length == 0)
				{
					value = CollectionUtils.ArrayEmpty<byte>();
				}
				else if (this._stringReference.Length == 36 && ConvertUtils.TryConvertGuid(this._stringReference.ToString(), out guid))
				{
					value = guid.ToByteArray();
				}
				else
				{
					value = Convert.FromBase64CharArray(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				}
				base.SetToken(JsonToken.Bytes, value, false);
				return;
			}
			case ReadType.ReadAsString:
			{
				string value2 = this._stringReference.ToString();
				base.SetToken(JsonToken.String, value2, false);
				this._quoteChar = quote;
				return;
			}
			}
			if (this._dateParseHandling != DateParseHandling.None)
			{
				DateParseHandling dateParseHandling;
				if (readType == ReadType.ReadAsDateTime)
				{
					dateParseHandling = DateParseHandling.DateTime;
				}
				else if (readType == ReadType.ReadAsDateTimeOffset)
				{
					dateParseHandling = DateParseHandling.DateTimeOffset;
				}
				else
				{
					dateParseHandling = this._dateParseHandling;
				}
				DateTimeOffset dateTimeOffset;
				if (dateParseHandling == DateParseHandling.DateTime)
				{
					DateTime dateTime;
					if (DateTimeUtils.TryParseDateTime(this._stringReference, base.DateTimeZoneHandling, base.DateFormatString, base.Culture, out dateTime))
					{
						base.SetToken(JsonToken.Date, dateTime, false);
						return;
					}
				}
				else if (DateTimeUtils.TryParseDateTimeOffset(this._stringReference, base.DateFormatString, base.Culture, out dateTimeOffset))
				{
					base.SetToken(JsonToken.Date, dateTimeOffset, false);
					return;
				}
			}
			base.SetToken(JsonToken.String, this._stringReference.ToString(), false);
			this._quoteChar = quote;
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x00200D5C File Offset: 0x00200D5C
		private static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		// Token: 0x06006A40 RID: 27200 RVA: 0x00200D70 File Offset: 0x00200D70
		private void ShiftBufferIfNeeded()
		{
			int num = this._chars.Length;
			if ((double)(num - this._charPos) <= (double)num * 0.1 || num >= 1073741823)
			{
				int num2 = this._charsUsed - this._charPos;
				if (num2 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num2;
				this._chars[this._charsUsed] = '\0';
			}
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x00200E0C File Offset: 0x00200E0C
		private int ReadData(bool append)
		{
			return this.ReadData(append, 0);
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x00200E18 File Offset: 0x00200E18
		private void PrepareBufferForReadData(bool append, int charsRequired)
		{
			if (this._charsUsed + charsRequired >= this._chars.Length - 1)
			{
				if (append)
				{
					int num = this._chars.Length * 2;
					int minSize = Math.Max((num < 0) ? int.MaxValue : num, this._charsUsed + charsRequired + 1);
					char[] array = BufferUtils.RentBuffer(this._arrayPool, minSize);
					JsonTextReader.BlockCopyChars(this._chars, 0, array, 0, this._chars.Length);
					BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
					this._chars = array;
					return;
				}
				int num2 = this._charsUsed - this._charPos;
				if (num2 + charsRequired + 1 >= this._chars.Length)
				{
					char[] array2 = BufferUtils.RentBuffer(this._arrayPool, num2 + charsRequired + 1);
					if (num2 > 0)
					{
						JsonTextReader.BlockCopyChars(this._chars, this._charPos, array2, 0, num2);
					}
					BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
					this._chars = array2;
				}
				else if (num2 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num2;
			}
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x00200F5C File Offset: 0x00200F5C
		private int ReadData(bool append, int charsRequired)
		{
			if (this._isEndOfFile)
			{
				return 0;
			}
			this.PrepareBufferForReadData(append, charsRequired);
			int count = this._chars.Length - this._charsUsed - 1;
			int num = this._reader.Read(this._chars, this._charsUsed, count);
			this._charsUsed += num;
			if (num == 0)
			{
				this._isEndOfFile = true;
			}
			this._chars[this._charsUsed] = '\0';
			return num;
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x00200FD8 File Offset: 0x00200FD8
		private bool EnsureChars(int relativePosition, bool append)
		{
			return this._charPos + relativePosition < this._charsUsed || this.ReadChars(relativePosition, append);
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x00200FF8 File Offset: 0x00200FF8
		private bool ReadChars(int relativePosition, bool append)
		{
			if (this._isEndOfFile)
			{
				return false;
			}
			int num = this._charPos + relativePosition - this._charsUsed + 1;
			int num2 = 0;
			do
			{
				int num3 = this.ReadData(append, num - num2);
				if (num3 == 0)
				{
					break;
				}
				num2 += num3;
			}
			while (num2 < num);
			return num2 >= num;
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x00201050 File Offset: 0x00201050
		public override bool Read()
		{
			this.EnsureBuffer();
			for (;;)
			{
				switch (this._currentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_4C;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_53;
				case JsonReader.State.PostValue:
					if (this.ParsePostValue(false))
					{
						return true;
					}
					continue;
				case JsonReader.State.Finished:
					goto IL_65;
				}
				break;
			}
			goto IL_DA;
			IL_4C:
			return this.ParseValue();
			IL_53:
			return this.ParseObject();
			IL_65:
			if (!this.EnsureChars(0, false))
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			this.EatWhitespace();
			if (this._isEndOfFile)
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			if (this._chars[this._charPos] == '/')
			{
				this.ParseComment(true);
				return true;
			}
			throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			IL_DA:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x0020115C File Offset: 0x0020115C
		public override int? ReadAsInt32()
		{
			return (int?)this.ReadNumberValue(ReadType.ReadAsInt32);
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x0020116C File Offset: 0x0020116C
		public override DateTime? ReadAsDateTime()
		{
			return (DateTime?)this.ReadStringValue(ReadType.ReadAsDateTime);
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x0020117C File Offset: 0x0020117C
		[NullableContext(2)]
		public override string ReadAsString()
		{
			return (string)this.ReadStringValue(ReadType.ReadAsString);
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x0020118C File Offset: 0x0020118C
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			this.EnsureBuffer();
			bool flag = false;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_265;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_265;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '\'')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_23C;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_23C;
							}
						}
						else
						{
							if (this.ReadNullChar())
							{
								break;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != '"' && c != '\'')
						{
							goto IL_23C;
						}
						goto IL_117;
					}
					this._charPos++;
					continue;
				}
				if (c <= '[')
				{
					if (c == ',')
					{
						this.ProcessValueComma();
						continue;
					}
					if (c == '/')
					{
						this.ParseComment(false);
						continue;
					}
					if (c == '[')
					{
						goto IL_193;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_1CE;
					}
					if (c == 'n')
					{
						goto IL_1AF;
					}
					if (c == '{')
					{
						this._charPos++;
						base.SetToken(JsonToken.StartObject);
						base.ReadIntoWrappedTypeObject();
						flag = true;
						continue;
					}
				}
				IL_23C:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_22;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_117:
			this.ParseString(c, ReadType.ReadAsBytes);
			byte[] array = (byte[])this.Value;
			if (flag)
			{
				base.ReaderReadAndAssert();
				if (this.TokenType != JsonToken.EndObject)
				{
					throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
				}
				base.SetToken(JsonToken.Bytes, array, false);
			}
			return array;
			IL_193:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return base.ReadArrayIntoByteArray();
			IL_1AF:
			this.HandleNull();
			return null;
			IL_1CE:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_22:
			throw this.CreateUnexpectedCharacterException(c);
			IL_265:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x00201424 File Offset: 0x00201424
		[NullableContext(2)]
		private object ReadStringValue(ReadType readType)
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_308;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_308;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'I')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_2DF;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_2DF;
							}
						}
						else
						{
							if (this.ReadNullChar())
							{
								break;
							}
							continue;
						}
					}
					else
					{
						switch (c)
						{
						case ' ':
							break;
						case '!':
						case '#':
						case '$':
						case '%':
						case '&':
						case '(':
						case ')':
						case '*':
						case '+':
							goto IL_2DF;
						case '"':
						case '\'':
							goto IL_16E;
						case ',':
							this.ProcessValueComma();
							continue;
						case '-':
							goto IL_17E;
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_1B7;
						case '/':
							this.ParseComment(false);
							continue;
						default:
							if (c != 'I')
							{
								goto IL_2DF;
							}
							goto IL_242;
						}
					}
					this._charPos++;
					continue;
				}
				if (c <= ']')
				{
					if (c == 'N')
					{
						goto IL_24A;
					}
					if (c == ']')
					{
						goto IL_271;
					}
				}
				else
				{
					if (c == 'f')
					{
						goto IL_1E2;
					}
					if (c == 'n')
					{
						goto IL_252;
					}
					if (c == 't')
					{
						goto IL_1E2;
					}
				}
				IL_2DF:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_24;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_16E:
			this.ParseString(c, readType);
			return this.FinishReadQuotedStringValue(readType);
			IL_17E:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			this.ParseNumber(readType);
			return this.Value;
			IL_1B7:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			this.ParseNumber(ReadType.ReadAsString);
			return this.Value;
			IL_1E2:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			string text = (c == 't') ? JsonConvert.True : JsonConvert.False;
			if (!this.MatchValueWithTrailingSeparator(text))
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.String, text);
			return text;
			IL_242:
			return this.ParseNumberPositiveInfinity(readType);
			IL_24A:
			return this.ParseNumberNaN(readType);
			IL_252:
			this.HandleNull();
			return null;
			IL_271:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_24:
			throw this.CreateUnexpectedCharacterException(c);
			IL_308:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x00201760 File Offset: 0x00201760
		[NullableContext(2)]
		private object FinishReadQuotedStringValue(ReadType readType)
		{
			switch (readType)
			{
			case ReadType.ReadAsBytes:
			case ReadType.ReadAsString:
				return this.Value;
			case ReadType.ReadAsDateTime:
			{
				object value = this.Value;
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					return dateTime;
				}
				return base.ReadDateTimeString((string)this.Value);
			}
			case ReadType.ReadAsDateTimeOffset:
			{
				object value = this.Value;
				if (value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					return dateTimeOffset;
				}
				return base.ReadDateTimeOffsetString((string)this.Value);
			}
			}
			throw new ArgumentOutOfRangeException("readType");
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x00201814 File Offset: 0x00201814
		private JsonReaderException CreateUnexpectedCharacterException(char c)
		{
			return JsonReaderException.Create(this, "Unexpected character encountered while parsing value: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x00201834 File Offset: 0x00201834
		public override bool? ReadAsBoolean()
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_301;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_301;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_2D0;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_2D0;
							case '"':
							case '\'':
								goto IL_161;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_191;
							case '/':
								this.ParseComment(false);
								continue;
							default:
								goto IL_2D0;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (this.ReadNullChar())
					{
						break;
					}
					continue;
				}
				else if (c <= 'f')
				{
					if (c == ']')
					{
						goto IL_25A;
					}
					if (c == 'f')
					{
						goto IL_1EC;
					}
				}
				else
				{
					if (c == 'n')
					{
						goto IL_181;
					}
					if (c == 't')
					{
						goto IL_1EC;
					}
				}
				IL_2D0:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_18;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_161:
			this.ParseString(c, ReadType.Read);
			return base.ReadBooleanString(this._stringReference.ToString());
			IL_181:
			this.HandleNull();
			return null;
			IL_191:
			this.ParseNumber(ReadType.Read);
			object value = this.Value;
			bool flag;
			if (value is BigInteger)
			{
				BigInteger left = (BigInteger)value;
				flag = (left != 0L);
			}
			else
			{
				flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
			}
			base.SetToken(JsonToken.Boolean, flag, false);
			return new bool?(flag);
			IL_1EC:
			bool flag2 = c == 't';
			string value2 = flag2 ? JsonConvert.True : JsonConvert.False;
			if (!this.MatchValueWithTrailingSeparator(value2))
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.Boolean, flag2);
			return new bool?(flag2);
			IL_25A:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_18:
			throw this.CreateUnexpectedCharacterException(c);
			IL_301:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x00201B68 File Offset: 0x00201B68
		private void ProcessValueComma()
		{
			this._charPos++;
			if (this._currentState != JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.Undefined);
				JsonReaderException ex = this.CreateUnexpectedCharacterException(',');
				this._charPos--;
				throw ex;
			}
			base.SetStateBasedOnCurrent();
		}

		// Token: 0x06006A50 RID: 27216 RVA: 0x00201BB8 File Offset: 0x00201BB8
		[NullableContext(2)]
		private object ReadNumberValue(ReadType readType)
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_26E;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_26E;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_245;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_245;
							case '"':
							case '\'':
								goto IL_151;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
								goto IL_179;
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_1B2;
							case '/':
								this.ParseComment(false);
								continue;
							default:
								goto IL_245;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (this.ReadNullChar())
					{
						break;
					}
					continue;
				}
				else if (c <= 'N')
				{
					if (c == 'I')
					{
						goto IL_171;
					}
					if (c == 'N')
					{
						goto IL_169;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_1D7;
					}
					if (c == 'n')
					{
						goto IL_161;
					}
				}
				IL_245:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_17;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_151:
			this.ParseString(c, readType);
			return this.FinishReadQuotedNumber(readType);
			IL_161:
			this.HandleNull();
			return null;
			IL_169:
			return this.ParseNumberNaN(readType);
			IL_171:
			return this.ParseNumberPositiveInfinity(readType);
			IL_179:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			this.ParseNumber(readType);
			return this.Value;
			IL_1B2:
			this.ParseNumber(readType);
			return this.Value;
			IL_1D7:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_17:
			throw this.CreateUnexpectedCharacterException(c);
			IL_26E:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06006A51 RID: 27217 RVA: 0x00201E58 File Offset: 0x00201E58
		[NullableContext(2)]
		private object FinishReadQuotedNumber(ReadType readType)
		{
			if (readType == ReadType.ReadAsInt32)
			{
				return base.ReadInt32String(this._stringReference.ToString());
			}
			if (readType == ReadType.ReadAsDecimal)
			{
				return base.ReadDecimalString(this._stringReference.ToString());
			}
			if (readType != ReadType.ReadAsDouble)
			{
				throw new ArgumentOutOfRangeException("readType");
			}
			return base.ReadDoubleString(this._stringReference.ToString());
		}

		// Token: 0x06006A52 RID: 27218 RVA: 0x00201EE4 File Offset: 0x00201EE4
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			return (DateTimeOffset?)this.ReadStringValue(ReadType.ReadAsDateTimeOffset);
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x00201EF4 File Offset: 0x00201EF4
		public override decimal? ReadAsDecimal()
		{
			return (decimal?)this.ReadNumberValue(ReadType.ReadAsDecimal);
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x00201F04 File Offset: 0x00201F04
		public override double? ReadAsDouble()
		{
			return (double?)this.ReadNumberValue(ReadType.ReadAsDouble);
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x00201F14 File Offset: 0x00201F14
		private void HandleNull()
		{
			if (!this.EnsureChars(1, true))
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			if (this._chars[this._charPos + 1] == 'u')
			{
				this.ParseNull();
				return;
			}
			this._charPos += 2;
			throw this.CreateUnexpectedCharacterException(this._chars[this._charPos - 1]);
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x00201F88 File Offset: 0x00201F88
		private void ReadFinished()
		{
			if (this.EnsureChars(0, false))
			{
				this.EatWhitespace();
				if (this._isEndOfFile)
				{
					return;
				}
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				this.ParseComment(false);
			}
			base.SetToken(JsonToken.None);
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x00202008 File Offset: 0x00202008
		private bool ReadNullChar()
		{
			if (this._charsUsed == this._charPos)
			{
				if (this.ReadData(false) == 0)
				{
					this._isEndOfFile = true;
					return true;
				}
			}
			else
			{
				this._charPos++;
			}
			return false;
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x00202040 File Offset: 0x00202040
		private void EnsureBuffer()
		{
			if (this._chars == null)
			{
				this._chars = BufferUtils.RentBuffer(this._arrayPool, 1024);
				this._chars[0] = '\0';
			}
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x0020206C File Offset: 0x0020206C
		private void ReadStringIntoBuffer(char quote)
		{
			int num = this._charPos;
			int charPos = this._charPos;
			int lastWritePosition = this._charPos;
			this._stringBuffer.Position = 0;
			char c2;
			for (;;)
			{
				char c = this._chars[num++];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						if (c != '\n')
						{
							if (c == '\r')
							{
								this._charPos = num - 1;
								this.ProcessCarriageReturn(true);
								num = this._charPos;
							}
						}
						else
						{
							this._charPos = num - 1;
							this.ProcessLineFeed();
							num = this._charPos;
						}
					}
					else if (this._charsUsed == num - 1)
					{
						num--;
						if (this.ReadData(true) == 0)
						{
							break;
						}
					}
				}
				else if (c != '"' && c != '\'')
				{
					if (c == '\\')
					{
						this._charPos = num;
						if (!this.EnsureChars(0, true))
						{
							goto Block_10;
						}
						int writeToPosition = num - 1;
						c2 = this._chars[num];
						num++;
						char c3;
						if (c2 <= '\\')
						{
							if (c2 <= '\'')
							{
								if (c2 != '"' && c2 != '\'')
								{
									goto Block_14;
								}
							}
							else if (c2 != '/')
							{
								if (c2 != '\\')
								{
									goto Block_16;
								}
								c3 = '\\';
								goto IL_2C6;
							}
							c3 = c2;
						}
						else if (c2 <= 'f')
						{
							if (c2 != 'b')
							{
								if (c2 != 'f')
								{
									goto Block_19;
								}
								c3 = '\f';
							}
							else
							{
								c3 = '\b';
							}
						}
						else
						{
							if (c2 != 'n')
							{
								switch (c2)
								{
								case 'r':
									c3 = '\r';
									goto IL_2C6;
								case 't':
									c3 = '\t';
									goto IL_2C6;
								case 'u':
									this._charPos = num;
									c3 = this.ParseUnicode();
									if (StringUtils.IsLowSurrogate(c3))
									{
										c3 = '�';
									}
									else if (StringUtils.IsHighSurrogate(c3))
									{
										bool flag;
										do
										{
											flag = false;
											if (this.EnsureChars(2, true) && this._chars[this._charPos] == '\\' && this._chars[this._charPos + 1] == 'u')
											{
												char writeChar = c3;
												this._charPos += 2;
												c3 = this.ParseUnicode();
												if (!StringUtils.IsLowSurrogate(c3))
												{
													if (StringUtils.IsHighSurrogate(c3))
													{
														writeChar = '�';
														flag = true;
													}
													else
													{
														writeChar = '�';
													}
												}
												this.EnsureBufferNotEmpty();
												this.WriteCharToBuffer(writeChar, lastWritePosition, writeToPosition);
												lastWritePosition = this._charPos;
											}
											else
											{
												c3 = '�';
											}
										}
										while (flag);
									}
									num = this._charPos;
									goto IL_2C6;
								}
								goto Block_21;
							}
							c3 = '\n';
						}
						IL_2C6:
						this.EnsureBufferNotEmpty();
						this.WriteCharToBuffer(c3, lastWritePosition, writeToPosition);
						lastWritePosition = num;
					}
				}
				else if (this._chars[num - 1] == quote)
				{
					goto Block_28;
				}
			}
			this._charPos = num;
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_10:
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_14:
			Block_16:
			Block_19:
			Block_21:
			this._charPos = num;
			throw JsonReaderException.Create(this, "Bad JSON escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, "\\" + c2.ToString()));
			Block_28:
			this.FinishReadStringIntoBuffer(num - 1, charPos, lastWritePosition);
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x002023B0 File Offset: 0x002023B0
		private void FinishReadStringIntoBuffer(int charPos, int initialPosition, int lastWritePosition)
		{
			if (initialPosition == lastWritePosition)
			{
				this._stringReference = new StringReference(this._chars, initialPosition, charPos - initialPosition);
			}
			else
			{
				this.EnsureBufferNotEmpty();
				if (charPos > lastWritePosition)
				{
					this._stringBuffer.Append(this._arrayPool, this._chars, lastWritePosition, charPos - lastWritePosition);
				}
				this._stringReference = new StringReference(this._stringBuffer.InternalBuffer, 0, this._stringBuffer.Position);
			}
			this._charPos = charPos + 1;
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x00202438 File Offset: 0x00202438
		private void WriteCharToBuffer(char writeChar, int lastWritePosition, int writeToPosition)
		{
			if (writeToPosition > lastWritePosition)
			{
				this._stringBuffer.Append(this._arrayPool, this._chars, lastWritePosition, writeToPosition - lastWritePosition);
			}
			this._stringBuffer.Append(this._arrayPool, writeChar);
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x00202470 File Offset: 0x00202470
		private char ConvertUnicode(bool enoughChars)
		{
			if (!enoughChars)
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing Unicode escape sequence.");
			}
			int value;
			if (ConvertUtils.TryHexTextToInt(this._chars, this._charPos, this._charPos + 4, out value))
			{
				char result = Convert.ToChar(value);
				this._charPos += 4;
				return result;
			}
			throw JsonReaderException.Create(this, "Invalid Unicode escape sequence: \\u{0}.".FormatWith(CultureInfo.InvariantCulture, new string(this._chars, this._charPos, 4)));
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x002024F0 File Offset: 0x002024F0
		private char ParseUnicode()
		{
			return this.ConvertUnicode(this.EnsureChars(4, true));
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x00202500 File Offset: 0x00202500
		private void ReadNumberIntoBuffer()
		{
			int num = this._charPos;
			for (;;)
			{
				char c = this._chars[num];
				if (c == '\0')
				{
					this._charPos = num;
					if (this._charsUsed != num)
					{
						return;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (this.ReadNumberCharIntoBuffer(c, num))
					{
						return;
					}
					num++;
				}
			}
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x00202558 File Offset: 0x00202558
		private bool ReadNumberCharIntoBuffer(char currentChar, int charPos)
		{
			if (currentChar <= 'X')
			{
				switch (currentChar)
				{
				case '+':
				case '-':
				case '.':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
					break;
				case ',':
				case '/':
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
					goto IL_B9;
				default:
					if (currentChar != 'X')
					{
						goto IL_B9;
					}
					break;
				}
			}
			else
			{
				switch (currentChar)
				{
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
					break;
				default:
					if (currentChar != 'x')
					{
						goto IL_B9;
					}
					break;
				}
			}
			return false;
			IL_B9:
			this._charPos = charPos;
			if (char.IsWhiteSpace(currentChar) || currentChar == ',' || currentChar == '}' || currentChar == ']' || currentChar == ')' || currentChar == '/')
			{
				return true;
			}
			throw JsonReaderException.Create(this, "Unexpected character encountered while parsing number: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
		}

		// Token: 0x06006A60 RID: 27232 RVA: 0x0020267C File Offset: 0x0020267C
		private void ClearRecentString()
		{
			this._stringBuffer.Position = 0;
			this._stringReference = default(StringReference);
		}

		// Token: 0x06006A61 RID: 27233 RVA: 0x00202698 File Offset: 0x00202698
		private bool ParsePostValue(bool ignoreComments)
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= ')')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_161;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_161;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							if (this.ReadData(false) == 0)
							{
								break;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_161;
						}
						goto IL_F7;
					}
					this._charPos++;
					continue;
				}
				if (c <= '/')
				{
					if (c == ',')
					{
						goto IL_121;
					}
					if (c == '/')
					{
						this.ParseComment(!ignoreComments);
						if (!ignoreComments)
						{
							return true;
						}
						continue;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_DF;
					}
					if (c == '}')
					{
						goto IL_C7;
					}
				}
				IL_161:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_17F;
				}
				this._charPos++;
			}
			this._currentState = JsonReader.State.Finished;
			return false;
			IL_C7:
			this._charPos++;
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_DF:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_F7:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_121:
			this._charPos++;
			base.SetStateBasedOnCurrent();
			return false;
			IL_17F:
			if (base.SupportMultipleContent && this.Depth == 0)
			{
				base.SetStateBasedOnCurrent();
				return false;
			}
			throw JsonReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x00202864 File Offset: 0x00202864
		private bool ParseObject()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_D5;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							goto IL_D5;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(false) == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != ' ')
				{
					if (c == '/')
					{
						goto IL_A2;
					}
					if (c != '}')
					{
						goto IL_D5;
					}
					goto IL_8A;
				}
				this._charPos++;
				continue;
				IL_D5:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_F3;
				}
				this._charPos++;
			}
			return false;
			IL_8A:
			base.SetToken(JsonToken.EndObject);
			this._charPos++;
			return true;
			IL_A2:
			this.ParseComment(true);
			return true;
			IL_F3:
			return this.ParseProperty();
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x00202970 File Offset: 0x00202970
		private bool ParseProperty()
		{
			char c = this._chars[this._charPos];
			char c2;
			if (c == '"' || c == '\'')
			{
				this._charPos++;
				c2 = c;
				this.ShiftBufferIfNeeded();
				this.ReadStringIntoBuffer(c2);
			}
			else
			{
				if (!this.ValidIdentifierChar(c))
				{
					throw JsonReaderException.Create(this, "Invalid property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				c2 = '\0';
				this.ShiftBufferIfNeeded();
				this.ParseUnquotedProperty();
			}
			string text;
			if (this.PropertyNameTable != null)
			{
				text = this.PropertyNameTable.Get(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				if (text == null)
				{
					text = this._stringReference.ToString();
				}
			}
			else
			{
				text = this._stringReference.ToString();
			}
			this.EatWhitespace();
			if (this._chars[this._charPos] != ':')
			{
				throw JsonReaderException.Create(this, "Invalid character after parsing property name. Expected ':' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			base.SetToken(JsonToken.PropertyName, text);
			this._quoteChar = c2;
			this.ClearRecentString();
			return true;
		}

		// Token: 0x06006A64 RID: 27236 RVA: 0x00202AD8 File Offset: 0x00202AD8
		private bool ValidIdentifierChar(char value)
		{
			return char.IsLetterOrDigit(value) || value == '_' || value == '$';
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x00202AF4 File Offset: 0x00202AF4
		private void ParseUnquotedProperty()
		{
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_41;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else if (this.ReadUnquotedPropertyReportIfDone(c, charPos))
				{
					return;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
			IL_41:
			this._stringReference = new StringReference(this._chars, charPos, this._charPos - charPos);
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x00202B6C File Offset: 0x00202B6C
		private bool ReadUnquotedPropertyReportIfDone(char currentChar, int initialPosition)
		{
			if (this.ValidIdentifierChar(currentChar))
			{
				this._charPos++;
				return false;
			}
			if (char.IsWhiteSpace(currentChar) || currentChar == ':')
			{
				this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
				return true;
			}
			throw JsonReaderException.Create(this, "Invalid JavaScript property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
		}

		// Token: 0x06006A67 RID: 27239 RVA: 0x00202BE4 File Offset: 0x00202BE4
		private bool ParseValue()
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'N')
				{
					if (c <= ' ')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_2A6;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								if (c != ' ')
								{
									goto IL_2A6;
								}
								break;
							}
							this._charPos++;
							continue;
						}
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(false) == 0)
						{
							break;
						}
						continue;
					}
					else if (c <= '/')
					{
						if (c == '"')
						{
							goto IL_12E;
						}
						switch (c)
						{
						case '\'':
							goto IL_12E;
						case ')':
							goto IL_264;
						case ',':
							goto IL_25A;
						case '-':
							goto IL_1CA;
						case '/':
							goto IL_203;
						}
					}
					else
					{
						if (c == 'I')
						{
							goto IL_1C0;
						}
						if (c == 'N')
						{
							goto IL_1B6;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == '[')
					{
						goto IL_22B;
					}
					if (c == ']')
					{
						goto IL_242;
					}
					if (c == 'f')
					{
						goto IL_140;
					}
				}
				else if (c <= 't')
				{
					if (c == 'n')
					{
						goto IL_148;
					}
					if (c == 't')
					{
						goto IL_138;
					}
				}
				else
				{
					if (c == 'u')
					{
						goto IL_20C;
					}
					if (c == '{')
					{
						goto IL_214;
					}
				}
				IL_2A6:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_2C4;
				}
				this._charPos++;
			}
			return false;
			IL_12E:
			this.ParseString(c, ReadType.Read);
			return true;
			IL_138:
			this.ParseTrue();
			return true;
			IL_140:
			this.ParseFalse();
			return true;
			IL_148:
			if (this.EnsureChars(1, true))
			{
				char c2 = this._chars[this._charPos + 1];
				if (c2 == 'u')
				{
					this.ParseNull();
				}
				else
				{
					if (c2 != 'e')
					{
						throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
					}
					this.ParseConstructor();
				}
				return true;
			}
			this._charPos++;
			throw base.CreateUnexpectedEndException();
			IL_1B6:
			this.ParseNumberNaN(ReadType.Read);
			return true;
			IL_1C0:
			this.ParseNumberPositiveInfinity(ReadType.Read);
			return true;
			IL_1CA:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				this.ParseNumberNegativeInfinity(ReadType.Read);
			}
			else
			{
				this.ParseNumber(ReadType.Read);
			}
			return true;
			IL_203:
			this.ParseComment(true);
			return true;
			IL_20C:
			this.ParseUndefined();
			return true;
			IL_214:
			this._charPos++;
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_22B:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_242:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_25A:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_264:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_2C4:
			if (char.IsNumber(c) || c == '-' || c == '.')
			{
				this.ParseNumber(ReadType.Read);
				return true;
			}
			throw this.CreateUnexpectedCharacterException(c);
		}

		// Token: 0x06006A68 RID: 27240 RVA: 0x00202EE4 File Offset: 0x00202EE4
		private void ProcessLineFeed()
		{
			this._charPos++;
			this.OnNewLine(this._charPos);
		}

		// Token: 0x06006A69 RID: 27241 RVA: 0x00202F00 File Offset: 0x00202F00
		private void ProcessCarriageReturn(bool append)
		{
			this._charPos++;
			this.SetNewLine(this.EnsureChars(1, append));
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x00202F20 File Offset: 0x00202F20
		private void EatWhitespace()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c != '\0')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c != ' ' && !char.IsWhiteSpace(c))
							{
								return;
							}
							this._charPos++;
						}
						else
						{
							this.ProcessCarriageReturn(false);
						}
					}
					else
					{
						this.ProcessLineFeed();
					}
				}
				else if (this._charsUsed == this._charPos)
				{
					if (this.ReadData(false) == 0)
					{
						break;
					}
				}
				else
				{
					this._charPos++;
				}
			}
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x00202FBC File Offset: 0x00202FBC
		private void ParseConstructor()
		{
			if (!this.MatchValueWithTrailingSeparator("new"))
			{
				throw JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
			}
			this.EatWhitespace();
			int charPos = this._charPos;
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_57;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						goto IL_8C;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
			IL_57:
			int charPos2 = this._charPos;
			this._charPos++;
			goto IL_116;
			IL_8C:
			if (c == '\r')
			{
				charPos2 = this._charPos;
				this.ProcessCarriageReturn(true);
			}
			else if (c == '\n')
			{
				charPos2 = this._charPos;
				this.ProcessLineFeed();
			}
			else if (char.IsWhiteSpace(c))
			{
				charPos2 = this._charPos;
				this._charPos++;
			}
			else
			{
				if (c != '(')
				{
					throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
				}
				charPos2 = this._charPos;
			}
			IL_116:
			this._stringReference = new StringReference(this._chars, charPos, charPos2 - charPos);
			string value = this._stringReference.ToString();
			this.EatWhitespace();
			if (this._chars[this._charPos] != '(')
			{
				throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			this.ClearRecentString();
			base.SetToken(JsonToken.StartConstructor, value);
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x00203174 File Offset: 0x00203174
		private void ParseNumber(ReadType readType)
		{
			this.ShiftBufferIfNeeded();
			char firstChar = this._chars[this._charPos];
			int charPos = this._charPos;
			this.ReadNumberIntoBuffer();
			this.ParseReadNumber(readType, firstChar, charPos);
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x002031B0 File Offset: 0x002031B0
		private void ParseReadNumber(ReadType readType, char firstChar, int initialPosition)
		{
			base.SetPostValueState(true);
			this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
			bool flag = char.IsDigit(firstChar) && this._stringReference.Length == 1;
			bool flag2 = firstChar == '0' && this._stringReference.Length > 1 && this._stringReference.Chars[this._stringReference.StartIndex + 1] != '.' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'e' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'E';
			object value;
			JsonToken newToken;
			switch (readType)
			{
			case ReadType.Read:
			case ReadType.ReadAsInt64:
			{
				if (flag)
				{
					value = (long)((ulong)firstChar - 48UL);
					newToken = JsonToken.Integer;
					goto IL_6BF;
				}
				if (flag2)
				{
					string text = this._stringReference.ToString();
					try
					{
						value = (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text, 16) : Convert.ToInt64(text, 8));
					}
					catch (Exception ex)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, text), ex);
					}
					newToken = JsonToken.Integer;
					goto IL_6BF;
				}
				long num;
				ParseResult parseResult = ConvertUtils.Int64TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num);
				if (parseResult == ParseResult.Success)
				{
					value = num;
					newToken = JsonToken.Integer;
					goto IL_6BF;
				}
				if (parseResult != ParseResult.Overflow)
				{
					if (this._floatParseHandling == FloatParseHandling.Decimal)
					{
						decimal num2;
						parseResult = ConvertUtils.DecimalTryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num2);
						if (parseResult != ParseResult.Success)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						value = num2;
					}
					else
					{
						double num3;
						if (!double.TryParse(this._stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num3))
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						value = num3;
					}
					newToken = JsonToken.Float;
					goto IL_6BF;
				}
				string text2 = this._stringReference.ToString();
				if (text2.Length > 380)
				{
					throw this.ThrowReaderError("JSON integer {0} is too large to parse.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
				}
				value = JsonTextReader.BigIntegerParse(text2, CultureInfo.InvariantCulture);
				newToken = JsonToken.Integer;
				goto IL_6BF;
			}
			case ReadType.ReadAsInt32:
				if (flag)
				{
					value = (int)(firstChar - '0');
				}
				else
				{
					if (flag2)
					{
						string text3 = this._stringReference.ToString();
						try
						{
							value = (text3.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt32(text3, 16) : Convert.ToInt32(text3, 8));
							goto IL_2AF;
						}
						catch (Exception ex2)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid integer.".FormatWith(CultureInfo.InvariantCulture, text3), ex2);
						}
					}
					int num4;
					ParseResult parseResult2 = ConvertUtils.Int32TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num4);
					if (parseResult2 == ParseResult.Success)
					{
						value = num4;
					}
					else
					{
						if (parseResult2 == ParseResult.Overflow)
						{
							throw this.ThrowReaderError("JSON integer {0} is too large or small for an Int32.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						throw this.ThrowReaderError("Input string '{0}' is not a valid integer.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
				}
				IL_2AF:
				newToken = JsonToken.Integer;
				goto IL_6BF;
			case ReadType.ReadAsString:
			{
				string text4 = this._stringReference.ToString();
				if (flag2)
				{
					try
					{
						if (text4.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
						{
							Convert.ToInt64(text4, 16);
						}
						else
						{
							Convert.ToInt64(text4, 8);
						}
						goto IL_18C;
					}
					catch (Exception ex3)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, text4), ex3);
					}
				}
				double num5;
				if (!double.TryParse(text4, NumberStyles.Float, CultureInfo.InvariantCulture, out num5))
				{
					throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
				}
				IL_18C:
				newToken = JsonToken.String;
				value = text4;
				goto IL_6BF;
			}
			case ReadType.ReadAsDecimal:
				if (flag)
				{
					value = firstChar - 48m;
				}
				else
				{
					if (flag2)
					{
						string text5 = this._stringReference.ToString();
						try
						{
							value = Convert.ToDecimal(text5.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text5, 16) : Convert.ToInt64(text5, 8));
							goto IL_3AD;
						}
						catch (Exception ex4)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, text5), ex4);
						}
					}
					decimal num6;
					if (ConvertUtils.DecimalTryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num6) != ParseResult.Success)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
					value = num6;
				}
				IL_3AD:
				newToken = JsonToken.Float;
				goto IL_6BF;
			case ReadType.ReadAsDouble:
				if (flag)
				{
					value = (double)firstChar - 48.0;
				}
				else
				{
					if (flag2)
					{
						string text6 = this._stringReference.ToString();
						try
						{
							value = Convert.ToDouble(text6.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text6, 16) : Convert.ToInt64(text6, 8));
							goto IL_49E;
						}
						catch (Exception ex5)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid double.".FormatWith(CultureInfo.InvariantCulture, text6), ex5);
						}
					}
					double num7;
					if (!double.TryParse(this._stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num7))
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid double.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
					value = num7;
				}
				IL_49E:
				newToken = JsonToken.Float;
				goto IL_6BF;
			}
			throw JsonReaderException.Create(this, "Cannot read number value as type.");
			IL_6BF:
			this.ClearRecentString();
			base.SetToken(newToken, value, false);
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x002038D0 File Offset: 0x002038D0
		private JsonReaderException ThrowReaderError(string message, [Nullable(2)] Exception ex = null)
		{
			base.SetToken(JsonToken.Undefined, null, false);
			return JsonReaderException.Create(this, message, ex);
		}

		// Token: 0x06006A6F RID: 27247 RVA: 0x002038E4 File Offset: 0x002038E4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static object BigIntegerParse(string number, CultureInfo culture)
		{
			return BigInteger.Parse(number, culture);
		}

		// Token: 0x06006A70 RID: 27248 RVA: 0x002038F4 File Offset: 0x002038F4
		private void ParseComment(bool setToken)
		{
			this._charPos++;
			if (!this.EnsureChars(1, false))
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			bool flag;
			if (this._chars[this._charPos] == '*')
			{
				flag = false;
			}
			else
			{
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Error parsing comment. Expected: *, got {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				flag = true;
			}
			this._charPos++;
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							if (flag)
							{
								goto Block_16;
							}
							this.ProcessLineFeed();
							continue;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(true) == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != '\r')
				{
					if (c == '*')
					{
						this._charPos++;
						if (!flag && this.EnsureChars(0, true) && this._chars[this._charPos] == '/')
						{
							goto Block_14;
						}
						continue;
					}
				}
				else
				{
					if (flag)
					{
						goto Block_15;
					}
					this.ProcessCarriageReturn(true);
					continue;
				}
				this._charPos++;
			}
			if (!flag)
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			this.EndComment(setToken, charPos, this._charPos);
			return;
			Block_14:
			this.EndComment(setToken, charPos, this._charPos - 1);
			this._charPos++;
			return;
			Block_15:
			this.EndComment(setToken, charPos, this._charPos);
			return;
			Block_16:
			this.EndComment(setToken, charPos, this._charPos);
		}

		// Token: 0x06006A71 RID: 27249 RVA: 0x00203AD4 File Offset: 0x00203AD4
		private void EndComment(bool setToken, int initialPosition, int endPosition)
		{
			if (setToken)
			{
				base.SetToken(JsonToken.Comment, new string(this._chars, initialPosition, endPosition - initialPosition));
			}
		}

		// Token: 0x06006A72 RID: 27250 RVA: 0x00203AF4 File Offset: 0x00203AF4
		private bool MatchValue(string value)
		{
			return this.MatchValue(this.EnsureChars(value.Length - 1, true), value);
		}

		// Token: 0x06006A73 RID: 27251 RVA: 0x00203B0C File Offset: 0x00203B0C
		private bool MatchValue(bool enoughChars, string value)
		{
			if (!enoughChars)
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (this._chars[this._charPos + i] != value[i])
				{
					this._charPos += i;
					return false;
				}
			}
			this._charPos += value.Length;
			return true;
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x00203B8C File Offset: 0x00203B8C
		private bool MatchValueWithTrailingSeparator(string value)
		{
			return this.MatchValue(value) && (!this.EnsureChars(0, false) || this.IsSeparator(this._chars[this._charPos]) || this._chars[this._charPos] == '\0');
		}

		// Token: 0x06006A75 RID: 27253 RVA: 0x00203BE4 File Offset: 0x00203BE4
		private bool IsSeparator(char c)
		{
			if (c <= ')')
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_B6;
				default:
					if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_B6;
						}
						if (base.CurrentState == JsonReader.State.Constructor || base.CurrentState == JsonReader.State.ConstructorStart)
						{
							return true;
						}
						return false;
					}
					break;
				}
				return true;
			}
			if (c <= '/')
			{
				if (c != ',')
				{
					if (c != '/')
					{
						goto IL_B6;
					}
					if (!this.EnsureChars(1, false))
					{
						return false;
					}
					char c2 = this._chars[this._charPos + 1];
					return c2 == '*' || c2 == '/';
				}
			}
			else if (c != ']' && c != '}')
			{
				goto IL_B6;
			}
			return true;
			IL_B6:
			if (char.IsWhiteSpace(c))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06006A76 RID: 27254 RVA: 0x00203CBC File Offset: 0x00203CBC
		private void ParseTrue()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.True))
			{
				base.SetToken(JsonToken.Boolean, true);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		// Token: 0x06006A77 RID: 27255 RVA: 0x00203CE8 File Offset: 0x00203CE8
		private void ParseNull()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.Null))
			{
				base.SetToken(JsonToken.Null);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing null value.");
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x00203D10 File Offset: 0x00203D10
		private void ParseUndefined()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.Undefined))
			{
				base.SetToken(JsonToken.Undefined);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing undefined value.");
		}

		// Token: 0x06006A79 RID: 27257 RVA: 0x00203D38 File Offset: 0x00203D38
		private void ParseFalse()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.False))
			{
				base.SetToken(JsonToken.Boolean, false);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		// Token: 0x06006A7A RID: 27258 RVA: 0x00203D64 File Offset: 0x00203D64
		private object ParseNumberNegativeInfinity(ReadType readType)
		{
			return this.ParseNumberNegativeInfinity(readType, this.MatchValueWithTrailingSeparator(JsonConvert.NegativeInfinity));
		}

		// Token: 0x06006A7B RID: 27259 RVA: 0x00203D78 File Offset: 0x00203D78
		private object ParseNumberNegativeInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.NegativeInfinity);
						return JsonConvert.NegativeInfinity;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_5C;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.NegativeInfinity);
					return double.NegativeInfinity;
				}
				IL_5C:
				throw JsonReaderException.Create(this, "Cannot read -Infinity value.");
			}
			throw JsonReaderException.Create(this, "Error parsing -Infinity value.");
		}

		// Token: 0x06006A7C RID: 27260 RVA: 0x00203DFC File Offset: 0x00203DFC
		private object ParseNumberPositiveInfinity(ReadType readType)
		{
			return this.ParseNumberPositiveInfinity(readType, this.MatchValueWithTrailingSeparator(JsonConvert.PositiveInfinity));
		}

		// Token: 0x06006A7D RID: 27261 RVA: 0x00203E10 File Offset: 0x00203E10
		private object ParseNumberPositiveInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.PositiveInfinity);
						return JsonConvert.PositiveInfinity;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_5C;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.PositiveInfinity);
					return double.PositiveInfinity;
				}
				IL_5C:
				throw JsonReaderException.Create(this, "Cannot read Infinity value.");
			}
			throw JsonReaderException.Create(this, "Error parsing Infinity value.");
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x00203E94 File Offset: 0x00203E94
		private object ParseNumberNaN(ReadType readType)
		{
			return this.ParseNumberNaN(readType, this.MatchValueWithTrailingSeparator(JsonConvert.NaN));
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x00203EA8 File Offset: 0x00203EA8
		private object ParseNumberNaN(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.NaN);
						return JsonConvert.NaN;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_5C;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.NaN);
					return double.NaN;
				}
				IL_5C:
				throw JsonReaderException.Create(this, "Cannot read NaN value.");
			}
			throw JsonReaderException.Create(this, "Error parsing NaN value.");
		}

		// Token: 0x06006A80 RID: 27264 RVA: 0x00203F2C File Offset: 0x00203F2C
		public override void Close()
		{
			base.Close();
			if (this._chars != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
				this._chars = null;
			}
			if (base.CloseInput)
			{
				TextReader reader = this._reader;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this._stringBuffer.Clear(this._arrayPool);
		}

		// Token: 0x06006A81 RID: 27265 RVA: 0x00203F9C File Offset: 0x00203F9C
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06006A82 RID: 27266 RVA: 0x00203FA0 File Offset: 0x00203FA0
		public int LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start && this.LinePosition == 0 && this.TokenType != JsonToken.Comment)
				{
					return 0;
				}
				return this._lineNumber;
			}
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06006A83 RID: 27267 RVA: 0x00203FCC File Offset: 0x00203FCC
		public int LinePosition
		{
			get
			{
				return this._charPos - this._lineStartPos;
			}
		}

		// Token: 0x040035C6 RID: 13766
		private readonly bool _safeAsync;

		// Token: 0x040035C7 RID: 13767
		private const char UnicodeReplacementChar = '�';

		// Token: 0x040035C8 RID: 13768
		private const int MaximumJavascriptIntegerCharacterLength = 380;

		// Token: 0x040035C9 RID: 13769
		private const int LargeBufferLength = 1073741823;

		// Token: 0x040035CA RID: 13770
		private readonly TextReader _reader;

		// Token: 0x040035CB RID: 13771
		[Nullable(2)]
		private char[] _chars;

		// Token: 0x040035CC RID: 13772
		private int _charsUsed;

		// Token: 0x040035CD RID: 13773
		private int _charPos;

		// Token: 0x040035CE RID: 13774
		private int _lineStartPos;

		// Token: 0x040035CF RID: 13775
		private int _lineNumber;

		// Token: 0x040035D0 RID: 13776
		private bool _isEndOfFile;

		// Token: 0x040035D1 RID: 13777
		private StringBuffer _stringBuffer;

		// Token: 0x040035D2 RID: 13778
		private StringReference _stringReference;

		// Token: 0x040035D3 RID: 13779
		[Nullable(2)]
		private IArrayPool<char> _arrayPool;
	}
}
