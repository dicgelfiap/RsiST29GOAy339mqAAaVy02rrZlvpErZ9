using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000495 RID: 1173
	public interface ISigner
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600241F RID: 9247
		string AlgorithmName { get; }

		// Token: 0x06002420 RID: 9248
		void Init(bool forSigning, ICipherParameters parameters);

		// Token: 0x06002421 RID: 9249
		void Update(byte input);

		// Token: 0x06002422 RID: 9250
		void BlockUpdate(byte[] input, int inOff, int length);

		// Token: 0x06002423 RID: 9251
		byte[] GenerateSignature();

		// Token: 0x06002424 RID: 9252
		bool VerifySignature(byte[] signature);

		// Token: 0x06002425 RID: 9253
		void Reset();
	}
}
