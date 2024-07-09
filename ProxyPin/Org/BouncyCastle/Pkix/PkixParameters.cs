using System;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200068E RID: 1678
	public class PkixParameters
	{
		// Token: 0x06003A6D RID: 14957 RVA: 0x0013AFC0 File Offset: 0x0013AFC0
		public PkixParameters(ISet trustAnchors)
		{
			this.SetTrustAnchors(trustAnchors);
			this.initialPolicies = new HashSet();
			this.certPathCheckers = Platform.CreateArrayList();
			this.stores = Platform.CreateArrayList();
			this.additionalStores = Platform.CreateArrayList();
			this.trustedACIssuers = new HashSet();
			this.necessaryACAttributes = new HashSet();
			this.prohibitedACAttributes = new HashSet();
			this.attrCertCheckers = new HashSet();
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x0013B068 File Offset: 0x0013B068
		// (set) Token: 0x06003A6F RID: 14959 RVA: 0x0013B070 File Offset: 0x0013B070
		public virtual bool IsRevocationEnabled
		{
			get
			{
				return this.revocationEnabled;
			}
			set
			{
				this.revocationEnabled = value;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x0013B07C File Offset: 0x0013B07C
		// (set) Token: 0x06003A71 RID: 14961 RVA: 0x0013B084 File Offset: 0x0013B084
		public virtual bool IsExplicitPolicyRequired
		{
			get
			{
				return this.explicitPolicyRequired;
			}
			set
			{
				this.explicitPolicyRequired = value;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x0013B090 File Offset: 0x0013B090
		// (set) Token: 0x06003A73 RID: 14963 RVA: 0x0013B098 File Offset: 0x0013B098
		public virtual bool IsAnyPolicyInhibited
		{
			get
			{
				return this.anyPolicyInhibited;
			}
			set
			{
				this.anyPolicyInhibited = value;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x0013B0A4 File Offset: 0x0013B0A4
		// (set) Token: 0x06003A75 RID: 14965 RVA: 0x0013B0AC File Offset: 0x0013B0AC
		public virtual bool IsPolicyMappingInhibited
		{
			get
			{
				return this.policyMappingInhibited;
			}
			set
			{
				this.policyMappingInhibited = value;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x0013B0B8 File Offset: 0x0013B0B8
		// (set) Token: 0x06003A77 RID: 14967 RVA: 0x0013B0C0 File Offset: 0x0013B0C0
		public virtual bool IsPolicyQualifiersRejected
		{
			get
			{
				return this.policyQualifiersRejected;
			}
			set
			{
				this.policyQualifiersRejected = value;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x0013B0CC File Offset: 0x0013B0CC
		// (set) Token: 0x06003A79 RID: 14969 RVA: 0x0013B0D4 File Offset: 0x0013B0D4
		public virtual DateTimeObject Date
		{
			get
			{
				return this.date;
			}
			set
			{
				this.date = value;
			}
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x0013B0E0 File Offset: 0x0013B0E0
		public virtual ISet GetTrustAnchors()
		{
			return new HashSet(this.trustAnchors);
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x0013B0F0 File Offset: 0x0013B0F0
		public virtual void SetTrustAnchors(ISet tas)
		{
			if (tas == null)
			{
				throw new ArgumentNullException("value");
			}
			if (tas.IsEmpty)
			{
				throw new ArgumentException("non-empty set required", "value");
			}
			this.trustAnchors = new HashSet();
			foreach (object obj in tas)
			{
				TrustAnchor trustAnchor = (TrustAnchor)obj;
				if (trustAnchor != null)
				{
					this.trustAnchors.Add(trustAnchor);
				}
			}
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0013B190 File Offset: 0x0013B190
		public virtual X509CertStoreSelector GetTargetCertConstraints()
		{
			if (this.certSelector == null)
			{
				return null;
			}
			return (X509CertStoreSelector)this.certSelector.Clone();
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0013B1B0 File Offset: 0x0013B1B0
		public virtual void SetTargetCertConstraints(IX509Selector selector)
		{
			if (selector == null)
			{
				this.certSelector = null;
				return;
			}
			this.certSelector = (IX509Selector)selector.Clone();
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x0013B1D4 File Offset: 0x0013B1D4
		public virtual ISet GetInitialPolicies()
		{
			ISet s = this.initialPolicies;
			if (this.initialPolicies == null)
			{
				s = new HashSet();
			}
			return new HashSet(s);
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x0013B204 File Offset: 0x0013B204
		public virtual void SetInitialPolicies(ISet initialPolicies)
		{
			this.initialPolicies = new HashSet();
			if (initialPolicies != null)
			{
				foreach (object obj in initialPolicies)
				{
					string text = (string)obj;
					if (text != null)
					{
						this.initialPolicies.Add(text);
					}
				}
			}
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x0013B280 File Offset: 0x0013B280
		public virtual void SetCertPathCheckers(IList checkers)
		{
			this.certPathCheckers = Platform.CreateArrayList();
			if (checkers != null)
			{
				foreach (object obj in checkers)
				{
					PkixCertPathChecker pkixCertPathChecker = (PkixCertPathChecker)obj;
					this.certPathCheckers.Add(pkixCertPathChecker.Clone());
				}
			}
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x0013B2FC File Offset: 0x0013B2FC
		public virtual IList GetCertPathCheckers()
		{
			IList list = Platform.CreateArrayList();
			foreach (object obj in this.certPathCheckers)
			{
				PkixCertPathChecker pkixCertPathChecker = (PkixCertPathChecker)obj;
				list.Add(pkixCertPathChecker.Clone());
			}
			return list;
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x0013B36C File Offset: 0x0013B36C
		public virtual void AddCertPathChecker(PkixCertPathChecker checker)
		{
			if (checker != null)
			{
				this.certPathCheckers.Add(checker.Clone());
			}
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x0013B388 File Offset: 0x0013B388
		public virtual object Clone()
		{
			PkixParameters pkixParameters = new PkixParameters(this.GetTrustAnchors());
			pkixParameters.SetParams(this);
			return pkixParameters;
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x0013B3B0 File Offset: 0x0013B3B0
		protected virtual void SetParams(PkixParameters parameters)
		{
			this.Date = parameters.Date;
			this.SetCertPathCheckers(parameters.GetCertPathCheckers());
			this.IsAnyPolicyInhibited = parameters.IsAnyPolicyInhibited;
			this.IsExplicitPolicyRequired = parameters.IsExplicitPolicyRequired;
			this.IsPolicyMappingInhibited = parameters.IsPolicyMappingInhibited;
			this.IsRevocationEnabled = parameters.IsRevocationEnabled;
			this.SetInitialPolicies(parameters.GetInitialPolicies());
			this.IsPolicyQualifiersRejected = parameters.IsPolicyQualifiersRejected;
			this.SetTargetCertConstraints(parameters.GetTargetCertConstraints());
			this.SetTrustAnchors(parameters.GetTrustAnchors());
			this.validityModel = parameters.validityModel;
			this.useDeltas = parameters.useDeltas;
			this.additionalLocationsEnabled = parameters.additionalLocationsEnabled;
			this.selector = ((parameters.selector == null) ? null : ((IX509Selector)parameters.selector.Clone()));
			this.stores = Platform.CreateArrayList(parameters.stores);
			this.additionalStores = Platform.CreateArrayList(parameters.additionalStores);
			this.trustedACIssuers = new HashSet(parameters.trustedACIssuers);
			this.prohibitedACAttributes = new HashSet(parameters.prohibitedACAttributes);
			this.necessaryACAttributes = new HashSet(parameters.necessaryACAttributes);
			this.attrCertCheckers = new HashSet(parameters.attrCertCheckers);
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06003A85 RID: 14981 RVA: 0x0013B4EC File Offset: 0x0013B4EC
		// (set) Token: 0x06003A86 RID: 14982 RVA: 0x0013B4F4 File Offset: 0x0013B4F4
		public virtual bool IsUseDeltasEnabled
		{
			get
			{
				return this.useDeltas;
			}
			set
			{
				this.useDeltas = value;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003A87 RID: 14983 RVA: 0x0013B500 File Offset: 0x0013B500
		// (set) Token: 0x06003A88 RID: 14984 RVA: 0x0013B508 File Offset: 0x0013B508
		public virtual int ValidityModel
		{
			get
			{
				return this.validityModel;
			}
			set
			{
				this.validityModel = value;
			}
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x0013B514 File Offset: 0x0013B514
		public virtual void SetStores(IList stores)
		{
			if (stores == null)
			{
				this.stores = Platform.CreateArrayList();
				return;
			}
			foreach (object obj in stores)
			{
				if (!(obj is IX509Store))
				{
					throw new InvalidCastException("All elements of list must be of type " + typeof(IX509Store).FullName);
				}
			}
			this.stores = Platform.CreateArrayList(stores);
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x0013B5B0 File Offset: 0x0013B5B0
		public virtual void AddStore(IX509Store store)
		{
			if (store != null)
			{
				this.stores.Add(store);
			}
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x0013B5C8 File Offset: 0x0013B5C8
		public virtual void AddAdditionalStore(IX509Store store)
		{
			if (store != null)
			{
				this.additionalStores.Add(store);
			}
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x0013B5E0 File Offset: 0x0013B5E0
		public virtual IList GetAdditionalStores()
		{
			return Platform.CreateArrayList(this.additionalStores);
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x0013B5F0 File Offset: 0x0013B5F0
		public virtual IList GetStores()
		{
			return Platform.CreateArrayList(this.stores);
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x0013B600 File Offset: 0x0013B600
		public virtual bool IsAdditionalLocationsEnabled
		{
			get
			{
				return this.additionalLocationsEnabled;
			}
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x0013B608 File Offset: 0x0013B608
		public virtual void SetAdditionalLocationsEnabled(bool enabled)
		{
			this.additionalLocationsEnabled = enabled;
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x0013B614 File Offset: 0x0013B614
		public virtual IX509Selector GetTargetConstraints()
		{
			if (this.selector != null)
			{
				return (IX509Selector)this.selector.Clone();
			}
			return null;
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x0013B634 File Offset: 0x0013B634
		public virtual void SetTargetConstraints(IX509Selector selector)
		{
			if (selector != null)
			{
				this.selector = (IX509Selector)selector.Clone();
				return;
			}
			this.selector = null;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x0013B658 File Offset: 0x0013B658
		public virtual ISet GetTrustedACIssuers()
		{
			return new HashSet(this.trustedACIssuers);
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x0013B668 File Offset: 0x0013B668
		public virtual void SetTrustedACIssuers(ISet trustedACIssuers)
		{
			if (trustedACIssuers == null)
			{
				this.trustedACIssuers = new HashSet();
				return;
			}
			foreach (object obj in trustedACIssuers)
			{
				if (!(obj is TrustAnchor))
				{
					throw new InvalidCastException("All elements of set must be of type " + typeof(TrustAnchor).FullName + ".");
				}
			}
			this.trustedACIssuers = new HashSet(trustedACIssuers);
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x0013B708 File Offset: 0x0013B708
		public virtual ISet GetNecessaryACAttributes()
		{
			return new HashSet(this.necessaryACAttributes);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x0013B718 File Offset: 0x0013B718
		public virtual void SetNecessaryACAttributes(ISet necessaryACAttributes)
		{
			if (necessaryACAttributes == null)
			{
				this.necessaryACAttributes = new HashSet();
				return;
			}
			foreach (object obj in necessaryACAttributes)
			{
				if (!(obj is string))
				{
					throw new InvalidCastException("All elements of set must be of type string.");
				}
			}
			this.necessaryACAttributes = new HashSet(necessaryACAttributes);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x0013B7A0 File Offset: 0x0013B7A0
		public virtual ISet GetProhibitedACAttributes()
		{
			return new HashSet(this.prohibitedACAttributes);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x0013B7B0 File Offset: 0x0013B7B0
		public virtual void SetProhibitedACAttributes(ISet prohibitedACAttributes)
		{
			if (prohibitedACAttributes == null)
			{
				this.prohibitedACAttributes = new HashSet();
				return;
			}
			foreach (object obj in prohibitedACAttributes)
			{
				if (!(obj is string))
				{
					throw new InvalidCastException("All elements of set must be of type string.");
				}
			}
			this.prohibitedACAttributes = new HashSet(prohibitedACAttributes);
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x0013B838 File Offset: 0x0013B838
		public virtual ISet GetAttrCertCheckers()
		{
			return new HashSet(this.attrCertCheckers);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x0013B848 File Offset: 0x0013B848
		public virtual void SetAttrCertCheckers(ISet attrCertCheckers)
		{
			if (attrCertCheckers == null)
			{
				this.attrCertCheckers = new HashSet();
				return;
			}
			foreach (object obj in attrCertCheckers)
			{
				if (!(obj is PkixAttrCertChecker))
				{
					throw new InvalidCastException("All elements of set must be of type " + typeof(PkixAttrCertChecker).FullName + ".");
				}
			}
			this.attrCertCheckers = new HashSet(attrCertCheckers);
		}

		// Token: 0x04001E4D RID: 7757
		public const int PkixValidityModel = 0;

		// Token: 0x04001E4E RID: 7758
		public const int ChainValidityModel = 1;

		// Token: 0x04001E4F RID: 7759
		private ISet trustAnchors;

		// Token: 0x04001E50 RID: 7760
		private DateTimeObject date;

		// Token: 0x04001E51 RID: 7761
		private IList certPathCheckers;

		// Token: 0x04001E52 RID: 7762
		private bool revocationEnabled = true;

		// Token: 0x04001E53 RID: 7763
		private ISet initialPolicies;

		// Token: 0x04001E54 RID: 7764
		private bool explicitPolicyRequired = false;

		// Token: 0x04001E55 RID: 7765
		private bool anyPolicyInhibited = false;

		// Token: 0x04001E56 RID: 7766
		private bool policyMappingInhibited = false;

		// Token: 0x04001E57 RID: 7767
		private bool policyQualifiersRejected = true;

		// Token: 0x04001E58 RID: 7768
		private IX509Selector certSelector;

		// Token: 0x04001E59 RID: 7769
		private IList stores;

		// Token: 0x04001E5A RID: 7770
		private IX509Selector selector;

		// Token: 0x04001E5B RID: 7771
		private bool additionalLocationsEnabled;

		// Token: 0x04001E5C RID: 7772
		private IList additionalStores;

		// Token: 0x04001E5D RID: 7773
		private ISet trustedACIssuers;

		// Token: 0x04001E5E RID: 7774
		private ISet necessaryACAttributes;

		// Token: 0x04001E5F RID: 7775
		private ISet prohibitedACAttributes;

		// Token: 0x04001E60 RID: 7776
		private ISet attrCertCheckers;

		// Token: 0x04001E61 RID: 7777
		private int validityModel = 0;

		// Token: 0x04001E62 RID: 7778
		private bool useDeltas = false;
	}
}
