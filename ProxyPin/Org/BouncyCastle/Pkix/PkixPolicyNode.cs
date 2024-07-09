using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200069D RID: 1693
	public class PkixPolicyNode
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x00140828 File Offset: 0x00140828
		public virtual int Depth
		{
			get
			{
				return this.mDepth;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x00140830 File Offset: 0x00140830
		public virtual IEnumerable Children
		{
			get
			{
				return new EnumerableProxy(this.mChildren);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x00140840 File Offset: 0x00140840
		// (set) Token: 0x06003B33 RID: 15155 RVA: 0x00140848 File Offset: 0x00140848
		public virtual bool IsCritical
		{
			get
			{
				return this.mCritical;
			}
			set
			{
				this.mCritical = value;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x00140854 File Offset: 0x00140854
		public virtual ISet PolicyQualifiers
		{
			get
			{
				return new HashSet(this.mPolicyQualifiers);
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003B35 RID: 15157 RVA: 0x00140864 File Offset: 0x00140864
		public virtual string ValidPolicy
		{
			get
			{
				return this.mValidPolicy;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x0014086C File Offset: 0x0014086C
		public virtual bool HasChildren
		{
			get
			{
				return this.mChildren.Count != 0;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003B37 RID: 15159 RVA: 0x00140880 File Offset: 0x00140880
		// (set) Token: 0x06003B38 RID: 15160 RVA: 0x00140890 File Offset: 0x00140890
		public virtual ISet ExpectedPolicies
		{
			get
			{
				return new HashSet(this.mExpectedPolicies);
			}
			set
			{
				this.mExpectedPolicies = new HashSet(value);
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x001408A0 File Offset: 0x001408A0
		// (set) Token: 0x06003B3A RID: 15162 RVA: 0x001408A8 File Offset: 0x001408A8
		public virtual PkixPolicyNode Parent
		{
			get
			{
				return this.mParent;
			}
			set
			{
				this.mParent = value;
			}
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x001408B4 File Offset: 0x001408B4
		public PkixPolicyNode(IList children, int depth, ISet expectedPolicies, PkixPolicyNode parent, ISet policyQualifiers, string validPolicy, bool critical)
		{
			if (children == null)
			{
				this.mChildren = Platform.CreateArrayList();
			}
			else
			{
				this.mChildren = Platform.CreateArrayList(children);
			}
			this.mDepth = depth;
			this.mExpectedPolicies = expectedPolicies;
			this.mParent = parent;
			this.mPolicyQualifiers = policyQualifiers;
			this.mValidPolicy = validPolicy;
			this.mCritical = critical;
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x0014091C File Offset: 0x0014091C
		public virtual void AddChild(PkixPolicyNode child)
		{
			child.Parent = this;
			this.mChildren.Add(child);
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x00140934 File Offset: 0x00140934
		public virtual void RemoveChild(PkixPolicyNode child)
		{
			this.mChildren.Remove(child);
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x00140944 File Offset: 0x00140944
		public override string ToString()
		{
			return this.ToString("");
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x00140954 File Offset: 0x00140954
		public virtual string ToString(string indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(indent);
			stringBuilder.Append(this.mValidPolicy);
			stringBuilder.Append(" {");
			stringBuilder.Append(Platform.NewLine);
			foreach (object obj in this.mChildren)
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)obj;
				stringBuilder.Append(pkixPolicyNode.ToString(indent + "    "));
			}
			stringBuilder.Append(indent);
			stringBuilder.Append("}");
			stringBuilder.Append(Platform.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x00140A20 File Offset: 0x00140A20
		public virtual object Clone()
		{
			return this.Copy();
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x00140A28 File Offset: 0x00140A28
		public virtual PkixPolicyNode Copy()
		{
			PkixPolicyNode pkixPolicyNode = new PkixPolicyNode(Platform.CreateArrayList(), this.mDepth, new HashSet(this.mExpectedPolicies), null, new HashSet(this.mPolicyQualifiers), this.mValidPolicy, this.mCritical);
			foreach (object obj in this.mChildren)
			{
				PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)obj;
				PkixPolicyNode pkixPolicyNode3 = pkixPolicyNode2.Copy();
				pkixPolicyNode3.Parent = pkixPolicyNode;
				pkixPolicyNode.AddChild(pkixPolicyNode3);
			}
			return pkixPolicyNode;
		}

		// Token: 0x04001E80 RID: 7808
		protected IList mChildren;

		// Token: 0x04001E81 RID: 7809
		protected int mDepth;

		// Token: 0x04001E82 RID: 7810
		protected ISet mExpectedPolicies;

		// Token: 0x04001E83 RID: 7811
		protected PkixPolicyNode mParent;

		// Token: 0x04001E84 RID: 7812
		protected ISet mPolicyQualifiers;

		// Token: 0x04001E85 RID: 7813
		protected string mValidPolicy;

		// Token: 0x04001E86 RID: 7814
		protected bool mCritical;
	}
}
