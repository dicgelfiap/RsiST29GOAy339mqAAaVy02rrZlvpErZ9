using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000A84 RID: 2692
	[NullableContext(1)]
	[Nullable(0)]
	internal struct JsonPosition
	{
		// Token: 0x060068D1 RID: 26833 RVA: 0x001FC358 File Offset: 0x001FC358
		public JsonPosition(JsonContainerType type)
		{
			this.Type = type;
			this.HasIndex = JsonPosition.TypeHasIndex(type);
			this.Position = -1;
			this.PropertyName = null;
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x001FC37C File Offset: 0x001FC37C
		internal int CalculateLength()
		{
			JsonContainerType type = this.Type;
			if (type == JsonContainerType.Object)
			{
				return this.PropertyName.Length + 5;
			}
			if (type - JsonContainerType.Array > 1)
			{
				throw new ArgumentOutOfRangeException("Type");
			}
			return MathUtils.IntLength((ulong)((long)this.Position)) + 2;
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x001FC3D0 File Offset: 0x001FC3D0
		[NullableContext(2)]
		internal void WriteTo([Nullable(1)] StringBuilder sb, ref StringWriter writer, ref char[] buffer)
		{
			JsonContainerType type = this.Type;
			if (type != JsonContainerType.Object)
			{
				if (type - JsonContainerType.Array > 1)
				{
					return;
				}
				sb.Append('[');
				sb.Append(this.Position);
				sb.Append(']');
				return;
			}
			else
			{
				string propertyName = this.PropertyName;
				if (propertyName.IndexOfAny(JsonPosition.SpecialCharacters) != -1)
				{
					sb.Append("['");
					if (writer == null)
					{
						writer = new StringWriter(sb);
					}
					JavaScriptUtils.WriteEscapedJavaScriptString(writer, propertyName, '\'', false, JavaScriptUtils.SingleQuoteCharEscapeFlags, StringEscapeHandling.Default, null, ref buffer);
					sb.Append("']");
					return;
				}
				if (sb.Length > 0)
				{
					sb.Append('.');
				}
				sb.Append(propertyName);
				return;
			}
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x001FC48C File Offset: 0x001FC48C
		internal static bool TypeHasIndex(JsonContainerType type)
		{
			return type == JsonContainerType.Array || type == JsonContainerType.Constructor;
		}

		// Token: 0x060068D5 RID: 26837 RVA: 0x001FC49C File Offset: 0x001FC49C
		internal static string BuildPath(List<JsonPosition> positions, JsonPosition? currentPosition)
		{
			int num = 0;
			if (positions != null)
			{
				for (int i = 0; i < positions.Count; i++)
				{
					num += positions[i].CalculateLength();
				}
			}
			if (currentPosition != null)
			{
				num += currentPosition.GetValueOrDefault().CalculateLength();
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			StringWriter stringWriter = null;
			char[] array = null;
			if (positions != null)
			{
				foreach (JsonPosition jsonPosition in positions)
				{
					jsonPosition.WriteTo(stringBuilder, ref stringWriter, ref array);
				}
			}
			if (currentPosition != null)
			{
				currentPosition.GetValueOrDefault().WriteTo(stringBuilder, ref stringWriter, ref array);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x001FC57C File Offset: 0x001FC57C
		internal static string FormatMessage([Nullable(2)] IJsonLineInfo lineInfo, string path, string message)
		{
			if (!message.EndsWith(Environment.NewLine, StringComparison.Ordinal))
			{
				message = message.Trim();
				if (!message.EndsWith('.'))
				{
					message += ".";
				}
				message += " ";
			}
			message += "Path '{0}'".FormatWith(CultureInfo.InvariantCulture, path);
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				message += ", line {0}, position {1}".FormatWith(CultureInfo.InvariantCulture, lineInfo.LineNumber, lineInfo.LinePosition);
			}
			message += ".";
			return message;
		}

		// Token: 0x04003549 RID: 13641
		private static readonly char[] SpecialCharacters = new char[]
		{
			'.',
			' ',
			'\'',
			'/',
			'"',
			'[',
			']',
			'(',
			')',
			'\t',
			'\n',
			'\r',
			'\f',
			'\b',
			'\\',
			'\u0085',
			'\u2028',
			'\u2029'
		};

		// Token: 0x0400354A RID: 13642
		internal JsonContainerType Type;

		// Token: 0x0400354B RID: 13643
		internal int Position;

		// Token: 0x0400354C RID: 13644
		[Nullable(2)]
		internal string PropertyName;

		// Token: 0x0400354D RID: 13645
		internal bool HasIndex;
	}
}
