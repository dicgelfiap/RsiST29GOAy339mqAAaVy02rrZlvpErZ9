using System;
using System.Globalization;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000277 RID: 631
	public class DerUtcTime : Asn1Object
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x0006C64C File Offset: 0x0006C64C
		public static DerUtcTime GetInstance(object obj)
		{
			if (obj == null || obj is DerUtcTime)
			{
				return (DerUtcTime)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0006C67C File Offset: 0x0006C67C
		public static DerUtcTime GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerUtcTime)
			{
				return DerUtcTime.GetInstance(@object);
			}
			return new DerUtcTime(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0006C6BC File Offset: 0x0006C6BC
		public DerUtcTime(string time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			this.time = time;
			try
			{
				this.ToDateTime();
			}
			catch (FormatException ex)
			{
				throw new ArgumentException("invalid date string: " + ex.Message);
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0006C71C File Offset: 0x0006C71C
		public DerUtcTime(DateTime time)
		{
			this.time = time.ToString("yyMMddHHmmss", CultureInfo.InvariantCulture) + "Z";
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0006C748 File Offset: 0x0006C748
		internal DerUtcTime(byte[] bytes)
		{
			this.time = Strings.FromAsciiByteArray(bytes);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0006C75C File Offset: 0x0006C75C
		public DateTime ToDateTime()
		{
			return this.ParseDateString(this.TimeString, "yyMMddHHmmss'GMT'zzz");
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0006C770 File Offset: 0x0006C770
		public DateTime ToAdjustedDateTime()
		{
			return this.ParseDateString(this.AdjustedTimeString, "yyyyMMddHHmmss'GMT'zzz");
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0006C784 File Offset: 0x0006C784
		private DateTime ParseDateString(string dateStr, string formatStr)
		{
			return DateTime.ParseExact(dateStr, formatStr, DateTimeFormatInfo.InvariantInfo).ToUniversalTime();
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0006C7AC File Offset: 0x0006C7AC
		public string TimeString
		{
			get
			{
				if (this.time.IndexOf('-') < 0 && this.time.IndexOf('+') < 0)
				{
					if (this.time.Length == 11)
					{
						return this.time.Substring(0, 10) + "00GMT+00:00";
					}
					return this.time.Substring(0, 12) + "GMT+00:00";
				}
				else
				{
					int num = this.time.IndexOf('-');
					if (num < 0)
					{
						num = this.time.IndexOf('+');
					}
					string text = this.time;
					if (num == this.time.Length - 3)
					{
						text += "00";
					}
					if (num == 10)
					{
						return string.Concat(new string[]
						{
							text.Substring(0, 10),
							"00GMT",
							text.Substring(10, 3),
							":",
							text.Substring(13, 2)
						});
					}
					return string.Concat(new string[]
					{
						text.Substring(0, 12),
						"GMT",
						text.Substring(12, 3),
						":",
						text.Substring(15, 2)
					});
				}
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0006C924 File Offset: 0x0006C924
		[Obsolete("Use 'AdjustedTimeString' property instead")]
		public string AdjustedTime
		{
			get
			{
				return this.AdjustedTimeString;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0006C92C File Offset: 0x0006C92C
		public string AdjustedTimeString
		{
			get
			{
				string timeString = this.TimeString;
				string str = (timeString[0] < '5') ? "20" : "19";
				return str + timeString;
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0006C96C File Offset: 0x0006C96C
		private byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.time);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0006C97C File Offset: 0x0006C97C
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(23, this.GetOctets());
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0006C98C File Offset: 0x0006C98C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUtcTime derUtcTime = asn1Object as DerUtcTime;
			return derUtcTime != null && this.time.Equals(derUtcTime.time);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0006C9C0 File Offset: 0x0006C9C0
		protected override int Asn1GetHashCode()
		{
			return this.time.GetHashCode();
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0006C9D0 File Offset: 0x0006C9D0
		public override string ToString()
		{
			return this.time;
		}

		// Token: 0x04000DC0 RID: 3520
		private readonly string time;
	}
}
