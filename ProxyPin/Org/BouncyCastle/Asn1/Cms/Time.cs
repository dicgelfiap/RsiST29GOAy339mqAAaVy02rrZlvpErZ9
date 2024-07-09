using System;
using System.Globalization;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000124 RID: 292
	public class Time : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x0004874C File Offset: 0x0004874C
		public static Time GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Time.GetInstance(obj.GetObject());
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0004875C File Offset: 0x0004875C
		public Time(Asn1Object time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			if (!(time is DerUtcTime) && !(time is DerGeneralizedTime))
			{
				throw new ArgumentException("unknown object passed to Time");
			}
			this.time = time;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000487AC File Offset: 0x000487AC
		public Time(DateTime date)
		{
			string text = date.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + "Z";
			int num = int.Parse(text.Substring(0, 4));
			if (num < 1950 || num > 2049)
			{
				this.time = new DerGeneralizedTime(text);
				return;
			}
			this.time = new DerUtcTime(text.Substring(2));
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00048824 File Offset: 0x00048824
		public static Time GetInstance(object obj)
		{
			if (obj == null || obj is Time)
			{
				return (Time)obj;
			}
			if (obj is DerUtcTime)
			{
				return new Time((DerUtcTime)obj);
			}
			if (obj is DerGeneralizedTime)
			{
				return new Time((DerGeneralizedTime)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00048898 File Offset: 0x00048898
		public string TimeString
		{
			get
			{
				if (this.time is DerUtcTime)
				{
					return ((DerUtcTime)this.time).AdjustedTimeString;
				}
				return ((DerGeneralizedTime)this.time).GetTime();
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x000488CC File Offset: 0x000488CC
		public DateTime Date
		{
			get
			{
				DateTime result;
				try
				{
					if (this.time is DerUtcTime)
					{
						result = ((DerUtcTime)this.time).ToAdjustedDateTime();
					}
					else
					{
						result = ((DerGeneralizedTime)this.time).ToDateTime();
					}
				}
				catch (FormatException ex)
				{
					throw new InvalidOperationException("invalid date string: " + ex.Message);
				}
				return result;
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00048940 File Offset: 0x00048940
		public override Asn1Object ToAsn1Object()
		{
			return this.time;
		}

		// Token: 0x04000738 RID: 1848
		private readonly Asn1Object time;
	}
}
