using System;

namespace Org.BouncyCastle.Math.Field
{
	// Token: 0x02000620 RID: 1568
	internal class PrimeField : IFiniteField
	{
		// Token: 0x06003538 RID: 13624 RVA: 0x00118368 File Offset: 0x00118368
		internal PrimeField(BigInteger characteristic)
		{
			this.characteristic = characteristic;
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x00118378 File Offset: 0x00118378
		public virtual BigInteger Characteristic
		{
			get
			{
				return this.characteristic;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x00118380 File Offset: 0x00118380
		public virtual int Dimension
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x00118384 File Offset: 0x00118384
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			PrimeField primeField = obj as PrimeField;
			return primeField != null && this.characteristic.Equals(primeField.characteristic);
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x001183C0 File Offset: 0x001183C0
		public override int GetHashCode()
		{
			return this.characteristic.GetHashCode();
		}

		// Token: 0x04001D1C RID: 7452
		protected readonly BigInteger characteristic;
	}
}
