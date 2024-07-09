using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B20 RID: 2848
	[NullableContext(1)]
	[Nullable(0)]
	public class JRaw : JValue
	{
		// Token: 0x060072CE RID: 29390 RVA: 0x002289DC File Offset: 0x002289DC
		public static async Task<JRaw> CreateAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			JRaw result;
			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
				{
					await jsonWriter.WriteTokenSyncReadingAsync(reader, cancellationToken).ConfigureAwait(false);
					result = new JRaw(sw.ToString());
				}
			}
			return result;
		}

		// Token: 0x060072CF RID: 29391 RVA: 0x00228A30 File Offset: 0x00228A30
		public JRaw(JRaw other) : base(other)
		{
		}

		// Token: 0x060072D0 RID: 29392 RVA: 0x00228A3C File Offset: 0x00228A3C
		[NullableContext(2)]
		public JRaw(object rawJson) : base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x060072D1 RID: 29393 RVA: 0x00228A48 File Offset: 0x00228A48
		public static JRaw Create(JsonReader reader)
		{
			JRaw result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					result = new JRaw(stringWriter.ToString());
				}
			}
			return result;
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x00228AB8 File Offset: 0x00228AB8
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
