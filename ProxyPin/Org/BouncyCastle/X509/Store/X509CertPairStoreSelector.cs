using System;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x0200070B RID: 1803
	public class X509CertPairStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06003F0B RID: 16139 RVA: 0x0015A760 File Offset: 0x0015A760
		private static X509CertStoreSelector CloneSelector(X509CertStoreSelector s)
		{
			if (s != null)
			{
				return (X509CertStoreSelector)s.Clone();
			}
			return null;
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x0015A778 File Offset: 0x0015A778
		public X509CertPairStoreSelector()
		{
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x0015A780 File Offset: 0x0015A780
		private X509CertPairStoreSelector(X509CertPairStoreSelector o)
		{
			this.certPair = o.CertPair;
			this.forwardSelector = o.ForwardSelector;
			this.reverseSelector = o.ReverseSelector;
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x0015A7BC File Offset: 0x0015A7BC
		// (set) Token: 0x06003F0F RID: 16143 RVA: 0x0015A7C4 File Offset: 0x0015A7C4
		public X509CertificatePair CertPair
		{
			get
			{
				return this.certPair;
			}
			set
			{
				this.certPair = value;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06003F10 RID: 16144 RVA: 0x0015A7D0 File Offset: 0x0015A7D0
		// (set) Token: 0x06003F11 RID: 16145 RVA: 0x0015A7E0 File Offset: 0x0015A7E0
		public X509CertStoreSelector ForwardSelector
		{
			get
			{
				return X509CertPairStoreSelector.CloneSelector(this.forwardSelector);
			}
			set
			{
				this.forwardSelector = X509CertPairStoreSelector.CloneSelector(value);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x0015A7F0 File Offset: 0x0015A7F0
		// (set) Token: 0x06003F13 RID: 16147 RVA: 0x0015A800 File Offset: 0x0015A800
		public X509CertStoreSelector ReverseSelector
		{
			get
			{
				return X509CertPairStoreSelector.CloneSelector(this.reverseSelector);
			}
			set
			{
				this.reverseSelector = X509CertPairStoreSelector.CloneSelector(value);
			}
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x0015A810 File Offset: 0x0015A810
		public bool Match(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			X509CertificatePair x509CertificatePair = obj as X509CertificatePair;
			return x509CertificatePair != null && (this.certPair == null || this.certPair.Equals(x509CertificatePair)) && (this.forwardSelector == null || this.forwardSelector.Match(x509CertificatePair.Forward)) && (this.reverseSelector == null || this.reverseSelector.Match(x509CertificatePair.Reverse));
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x0015A8A8 File Offset: 0x0015A8A8
		public object Clone()
		{
			return new X509CertPairStoreSelector(this);
		}

		// Token: 0x04002085 RID: 8325
		private X509CertificatePair certPair;

		// Token: 0x04002086 RID: 8326
		private X509CertStoreSelector forwardSelector;

		// Token: 0x04002087 RID: 8327
		private X509CertStoreSelector reverseSelector;
	}
}
