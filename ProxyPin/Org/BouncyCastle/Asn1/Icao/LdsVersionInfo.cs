using System;

namespace Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x02000173 RID: 371
	public class LdsVersionInfo : Asn1Encodable
	{
		// Token: 0x06000C84 RID: 3204 RVA: 0x000506A4 File Offset: 0x000506A4
		public LdsVersionInfo(string ldsVersion, string unicodeVersion)
		{
			this.ldsVersion = new DerPrintableString(ldsVersion);
			this.unicodeVersion = new DerPrintableString(unicodeVersion);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000506C4 File Offset: 0x000506C4
		private LdsVersionInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("sequence wrong size for LDSVersionInfo", "seq");
			}
			this.ldsVersion = DerPrintableString.GetInstance(seq[0]);
			this.unicodeVersion = DerPrintableString.GetInstance(seq[1]);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0005071C File Offset: 0x0005071C
		public static LdsVersionInfo GetInstance(object obj)
		{
			if (obj is LdsVersionInfo)
			{
				return (LdsVersionInfo)obj;
			}
			if (obj != null)
			{
				return new LdsVersionInfo(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00050744 File Offset: 0x00050744
		public virtual string GetLdsVersion()
		{
			return this.ldsVersion.GetString();
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00050754 File Offset: 0x00050754
		public virtual string GetUnicodeVersion()
		{
			return this.unicodeVersion.GetString();
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00050764 File Offset: 0x00050764
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.ldsVersion,
				this.unicodeVersion
			});
		}

		// Token: 0x040008B2 RID: 2226
		private DerPrintableString ldsVersion;

		// Token: 0x040008B3 RID: 2227
		private DerPrintableString unicodeVersion;
	}
}
