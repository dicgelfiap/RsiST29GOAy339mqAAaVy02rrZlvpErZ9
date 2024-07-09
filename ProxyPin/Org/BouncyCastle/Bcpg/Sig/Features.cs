using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000285 RID: 645
	public class Features : SignatureSubpacket
	{
		// Token: 0x0600145F RID: 5215 RVA: 0x0006D640 File Offset: 0x0006D640
		private static byte[] FeatureToByteArray(byte feature)
		{
			return new byte[]
			{
				feature
			};
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0006D660 File Offset: 0x0006D660
		public Features(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.Features, critical, isLongLength, data)
		{
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0006D670 File Offset: 0x0006D670
		public Features(bool critical, byte feature) : base(SignatureSubpacketTag.Features, critical, false, Features.FeatureToByteArray(feature))
		{
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0006D684 File Offset: 0x0006D684
		public bool SupportsModificationDetection
		{
			get
			{
				return this.SupportsFeature(Features.FEATURE_MODIFICATION_DETECTION);
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0006D694 File Offset: 0x0006D694
		public bool SupportsFeature(byte feature)
		{
			return Array.IndexOf(this.data, feature) >= 0;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0006D6B0 File Offset: 0x0006D6B0
		private void SetSupportsFeature(byte feature, bool support)
		{
			if (feature == 0)
			{
				throw new ArgumentException("cannot be 0", "feature");
			}
			int num = Array.IndexOf(this.data, feature);
			if (num >= 0 == support)
			{
				return;
			}
			if (support)
			{
				this.data = Arrays.Append(this.data, feature);
				return;
			}
			byte[] array = new byte[this.data.Length - 1];
			Array.Copy(this.data, 0, array, 0, num);
			Array.Copy(this.data, num + 1, array, num, array.Length - num);
			this.data = array;
		}

		// Token: 0x04000DD6 RID: 3542
		public static readonly byte FEATURE_MODIFICATION_DETECTION = 1;
	}
}
