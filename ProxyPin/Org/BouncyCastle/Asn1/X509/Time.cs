using System;
using System.Globalization;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000216 RID: 534
	public class Time : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x00062490 File Offset: 0x00062490
		public static Time GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Time.GetInstance(obj.GetObject());
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000624A0 File Offset: 0x000624A0
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

		// Token: 0x0600112C RID: 4396 RVA: 0x000624F0 File Offset: 0x000624F0
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

		// Token: 0x0600112D RID: 4397 RVA: 0x00062568 File Offset: 0x00062568
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

		// Token: 0x0600112E RID: 4398 RVA: 0x000625DC File Offset: 0x000625DC
		public string GetTime()
		{
			if (this.time is DerUtcTime)
			{
				return ((DerUtcTime)this.time).AdjustedTimeString;
			}
			return ((DerGeneralizedTime)this.time).GetTime();
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00062610 File Offset: 0x00062610
		public DateTime ToDateTime()
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

		// Token: 0x06001130 RID: 4400 RVA: 0x00062684 File Offset: 0x00062684
		public override Asn1Object ToAsn1Object()
		{
			return this.time;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0006268C File Offset: 0x0006268C
		public override string ToString()
		{
			return this.GetTime();
		}

		// Token: 0x04000C59 RID: 3161
		private readonly Asn1Object time;
	}
}
