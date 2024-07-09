using System;
using System.Globalization;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200026A RID: 618
	public class DerGeneralizedTime : Asn1Object
	{
		// Token: 0x060013A6 RID: 5030 RVA: 0x0006B5C4 File Offset: 0x0006B5C4
		public static DerGeneralizedTime GetInstance(object obj)
		{
			if (obj == null || obj is DerGeneralizedTime)
			{
				return (DerGeneralizedTime)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0006B5F8 File Offset: 0x0006B5F8
		public static DerGeneralizedTime GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerGeneralizedTime)
			{
				return DerGeneralizedTime.GetInstance(@object);
			}
			return new DerGeneralizedTime(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0006B638 File Offset: 0x0006B638
		public DerGeneralizedTime(string time)
		{
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

		// Token: 0x060013A9 RID: 5033 RVA: 0x0006B688 File Offset: 0x0006B688
		public DerGeneralizedTime(DateTime time)
		{
			this.time = time.ToString("yyyyMMddHHmmss\\Z");
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0006B6A4 File Offset: 0x0006B6A4
		internal DerGeneralizedTime(byte[] bytes)
		{
			this.time = Strings.FromAsciiByteArray(bytes);
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0006B6B8 File Offset: 0x0006B6B8
		public string TimeString
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0006B6C0 File Offset: 0x0006B6C0
		public string GetTime()
		{
			if (this.time[this.time.Length - 1] == 'Z')
			{
				return this.time.Substring(0, this.time.Length - 1) + "GMT+00:00";
			}
			int num = this.time.Length - 5;
			char c = this.time[num];
			if (c == '-' || c == '+')
			{
				return string.Concat(new string[]
				{
					this.time.Substring(0, num),
					"GMT",
					this.time.Substring(num, 3),
					":",
					this.time.Substring(num + 3)
				});
			}
			num = this.time.Length - 3;
			c = this.time[num];
			if (c == '-' || c == '+')
			{
				return this.time.Substring(0, num) + "GMT" + this.time.Substring(num) + ":00";
			}
			return this.time + this.CalculateGmtOffset();
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0006B808 File Offset: 0x0006B808
		private string CalculateGmtOffset()
		{
			char c = '+';
			DateTime dateTime = this.ToDateTime();
			TimeSpan timeSpan = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
			if (timeSpan.CompareTo(TimeSpan.Zero) < 0)
			{
				c = '-';
				timeSpan = timeSpan.Duration();
			}
			int hours = timeSpan.Hours;
			int minutes = timeSpan.Minutes;
			return string.Concat(new object[]
			{
				"GMT",
				c,
				DerGeneralizedTime.Convert(hours),
				":",
				DerGeneralizedTime.Convert(minutes)
			});
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0006B8A4 File Offset: 0x0006B8A4
		private static string Convert(int time)
		{
			if (time < 10)
			{
				return "0" + time;
			}
			return time.ToString();
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0006B8C8 File Offset: 0x0006B8C8
		public DateTime ToDateTime()
		{
			string text = this.time;
			bool makeUniversal = false;
			string format;
			if (Platform.EndsWith(text, "Z"))
			{
				if (this.HasFractionalSeconds)
				{
					int count = text.Length - text.IndexOf('.') - 2;
					format = "yyyyMMddHHmmss." + this.FString(count) + "\\Z";
				}
				else
				{
					format = "yyyyMMddHHmmss\\Z";
				}
			}
			else if (this.time.IndexOf('-') > 0 || this.time.IndexOf('+') > 0)
			{
				text = this.GetTime();
				makeUniversal = true;
				if (this.HasFractionalSeconds)
				{
					int count2 = Platform.IndexOf(text, "GMT") - 1 - text.IndexOf('.');
					format = "yyyyMMddHHmmss." + this.FString(count2) + "'GMT'zzz";
				}
				else
				{
					format = "yyyyMMddHHmmss'GMT'zzz";
				}
			}
			else if (this.HasFractionalSeconds)
			{
				int count3 = text.Length - 1 - text.IndexOf('.');
				format = "yyyyMMddHHmmss." + this.FString(count3);
			}
			else
			{
				format = "yyyyMMddHHmmss";
			}
			return this.ParseDateString(text, format, makeUniversal);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0006B9F8 File Offset: 0x0006B9F8
		private string FString(int count)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append('f');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0006BA30 File Offset: 0x0006BA30
		private DateTime ParseDateString(string s, string format, bool makeUniversal)
		{
			DateTimeStyles dateTimeStyles = DateTimeStyles.None;
			if (Platform.EndsWith(format, "Z"))
			{
				try
				{
					dateTimeStyles = (DateTimeStyles)Enums.GetEnumValue(typeof(DateTimeStyles), "AssumeUniversal");
				}
				catch (Exception)
				{
				}
				dateTimeStyles |= DateTimeStyles.AdjustToUniversal;
			}
			DateTime result = DateTime.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, dateTimeStyles);
			if (!makeUniversal)
			{
				return result;
			}
			return result.ToUniversalTime();
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0006BAA8 File Offset: 0x0006BAA8
		private bool HasFractionalSeconds
		{
			get
			{
				return this.time.IndexOf('.') == 14;
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0006BABC File Offset: 0x0006BABC
		private byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.time);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0006BACC File Offset: 0x0006BACC
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(24, this.GetOctets());
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0006BADC File Offset: 0x0006BADC
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerGeneralizedTime derGeneralizedTime = asn1Object as DerGeneralizedTime;
			return derGeneralizedTime != null && this.time.Equals(derGeneralizedTime.time);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0006BB10 File Offset: 0x0006BB10
		protected override int Asn1GetHashCode()
		{
			return this.time.GetHashCode();
		}

		// Token: 0x04000DB0 RID: 3504
		private readonly string time;
	}
}
