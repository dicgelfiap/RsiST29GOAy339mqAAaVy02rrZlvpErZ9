using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000B36 RID: 2870
	[NullableContext(1)]
	[Nullable(0)]
	internal class BooleanQueryExpression : QueryExpression
	{
		// Token: 0x0600742C RID: 29740 RVA: 0x0022EE50 File Offset: 0x0022EE50
		public BooleanQueryExpression(QueryOperator @operator, object left, [Nullable(2)] object right) : base(@operator)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x0600742D RID: 29741 RVA: 0x0022EE68 File Offset: 0x0022EE68
		private IEnumerable<JToken> GetResult(JToken root, JToken t, [Nullable(2)] object o)
		{
			JToken jtoken = o as JToken;
			if (jtoken != null)
			{
				return new JToken[]
				{
					jtoken
				};
			}
			List<PathFilter> list = o as List<PathFilter>;
			if (list != null)
			{
				return JPath.Evaluate(list, root, t, false);
			}
			return CollectionUtils.ArrayEmpty<JToken>();
		}

		// Token: 0x0600742E RID: 29742 RVA: 0x0022EEB0 File Offset: 0x0022EEB0
		public override bool IsMatch(JToken root, JToken t)
		{
			if (this.Operator == QueryOperator.Exists)
			{
				return this.GetResult(root, t, this.Left).Any<JToken>();
			}
			using (IEnumerator<JToken> enumerator = this.GetResult(root, t, this.Left).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IEnumerable<JToken> result = this.GetResult(root, t, this.Right);
					ICollection<JToken> collection = (result as ICollection<JToken>) ?? result.ToList<JToken>();
					do
					{
						JToken leftResult = enumerator.Current;
						foreach (JToken rightResult in collection)
						{
							if (this.MatchTokens(leftResult, rightResult))
							{
								return true;
							}
						}
					}
					while (enumerator.MoveNext());
				}
			}
			return false;
		}

		// Token: 0x0600742F RID: 29743 RVA: 0x0022EFAC File Offset: 0x0022EFAC
		private bool MatchTokens(JToken leftResult, JToken rightResult)
		{
			JValue jvalue = leftResult as JValue;
			if (jvalue != null)
			{
				JValue jvalue2 = rightResult as JValue;
				if (jvalue2 != null)
				{
					switch (this.Operator)
					{
					case QueryOperator.Equals:
						if (BooleanQueryExpression.EqualsWithStringCoercion(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					case QueryOperator.NotEquals:
						if (!BooleanQueryExpression.EqualsWithStringCoercion(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					case QueryOperator.Exists:
						return true;
					case QueryOperator.LessThan:
						if (jvalue.CompareTo(jvalue2) < 0)
						{
							return true;
						}
						return false;
					case QueryOperator.LessThanOrEquals:
						if (jvalue.CompareTo(jvalue2) <= 0)
						{
							return true;
						}
						return false;
					case QueryOperator.GreaterThan:
						if (jvalue.CompareTo(jvalue2) > 0)
						{
							return true;
						}
						return false;
					case QueryOperator.GreaterThanOrEquals:
						if (jvalue.CompareTo(jvalue2) >= 0)
						{
							return true;
						}
						return false;
					case QueryOperator.And:
					case QueryOperator.Or:
						return false;
					case QueryOperator.RegexEquals:
						if (BooleanQueryExpression.RegexEquals(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					case QueryOperator.StrictEquals:
						if (BooleanQueryExpression.EqualsWithStrictMatch(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					case QueryOperator.StrictNotEquals:
						if (!BooleanQueryExpression.EqualsWithStrictMatch(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					default:
						return false;
					}
				}
			}
			QueryOperator @operator = this.Operator;
			if (@operator - QueryOperator.NotEquals <= 1)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06007430 RID: 29744 RVA: 0x0022F0B4 File Offset: 0x0022F0B4
		private static bool RegexEquals(JValue input, JValue pattern)
		{
			if (input.Type != JTokenType.String || pattern.Type != JTokenType.String)
			{
				return false;
			}
			string text = (string)pattern.Value;
			int num = text.LastIndexOf('/');
			string pattern2 = text.Substring(1, num - 1);
			string optionsText = text.Substring(num + 1);
			return Regex.IsMatch((string)input.Value, pattern2, MiscellaneousUtils.GetRegexOptions(optionsText));
		}

		// Token: 0x06007431 RID: 29745 RVA: 0x0022F120 File Offset: 0x0022F120
		internal static bool EqualsWithStringCoercion(JValue value, JValue queryValue)
		{
			if (value.Equals(queryValue))
			{
				return true;
			}
			if ((value.Type == JTokenType.Integer && queryValue.Type == JTokenType.Float) || (value.Type == JTokenType.Float && queryValue.Type == JTokenType.Integer))
			{
				return JValue.Compare(value.Type, value.Value, queryValue.Value) == 0;
			}
			if (queryValue.Type != JTokenType.String)
			{
				return false;
			}
			string b = (string)queryValue.Value;
			string a;
			switch (value.Type)
			{
			case JTokenType.Date:
				using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
				{
					object value2 = value.Value;
					if (value2 is DateTimeOffset)
					{
						DateTimeOffset value3 = (DateTimeOffset)value2;
						DateTimeUtils.WriteDateTimeOffsetString(stringWriter, value3, DateFormatHandling.IsoDateFormat, null, CultureInfo.InvariantCulture);
					}
					else
					{
						DateTimeUtils.WriteDateTimeString(stringWriter, (DateTime)value.Value, DateFormatHandling.IsoDateFormat, null, CultureInfo.InvariantCulture);
					}
					a = stringWriter.ToString();
					goto IL_14A;
				}
				break;
			case JTokenType.Raw:
				return false;
			case JTokenType.Bytes:
				break;
			case JTokenType.Guid:
			case JTokenType.TimeSpan:
				a = value.Value.ToString();
				goto IL_14A;
			case JTokenType.Uri:
				a = ((Uri)value.Value).OriginalString;
				goto IL_14A;
			default:
				return false;
			}
			a = Convert.ToBase64String((byte[])value.Value);
			IL_14A:
			return string.Equals(a, b, StringComparison.Ordinal);
		}

		// Token: 0x06007432 RID: 29746 RVA: 0x0022F290 File Offset: 0x0022F290
		internal static bool EqualsWithStrictMatch(JValue value, JValue queryValue)
		{
			if ((value.Type == JTokenType.Integer && queryValue.Type == JTokenType.Float) || (value.Type == JTokenType.Float && queryValue.Type == JTokenType.Integer))
			{
				return JValue.Compare(value.Type, value.Value, queryValue.Value) == 0;
			}
			return value.Type == queryValue.Type && value.Equals(queryValue);
		}

		// Token: 0x040038C6 RID: 14534
		public readonly object Left;

		// Token: 0x040038C7 RID: 14535
		[Nullable(2)]
		public readonly object Right;
	}
}
