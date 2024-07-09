using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B21 RID: 2849
	public class JsonLoadSettings
	{
		// Token: 0x060072D3 RID: 29395 RVA: 0x00228AC0 File Offset: 0x00228AC0
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
			this._duplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace;
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x060072D4 RID: 29396 RVA: 0x00228AE0 File Offset: 0x00228AE0
		// (set) Token: 0x060072D5 RID: 29397 RVA: 0x00228AE8 File Offset: 0x00228AE8
		public CommentHandling CommentHandling
		{
			get
			{
				return this._commentHandling;
			}
			set
			{
				if (value < CommentHandling.Ignore || value > CommentHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._commentHandling = value;
			}
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x060072D6 RID: 29398 RVA: 0x00228B0C File Offset: 0x00228B0C
		// (set) Token: 0x060072D7 RID: 29399 RVA: 0x00228B14 File Offset: 0x00228B14
		public LineInfoHandling LineInfoHandling
		{
			get
			{
				return this._lineInfoHandling;
			}
			set
			{
				if (value < LineInfoHandling.Ignore || value > LineInfoHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lineInfoHandling = value;
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x060072D8 RID: 29400 RVA: 0x00228B38 File Offset: 0x00228B38
		// (set) Token: 0x060072D9 RID: 29401 RVA: 0x00228B40 File Offset: 0x00228B40
		public DuplicatePropertyNameHandling DuplicatePropertyNameHandling
		{
			get
			{
				return this._duplicatePropertyNameHandling;
			}
			set
			{
				if (value < DuplicatePropertyNameHandling.Replace || value > DuplicatePropertyNameHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._duplicatePropertyNameHandling = value;
			}
		}

		// Token: 0x0400386E RID: 14446
		private CommentHandling _commentHandling;

		// Token: 0x0400386F RID: 14447
		private LineInfoHandling _lineInfoHandling;

		// Token: 0x04003870 RID: 14448
		private DuplicatePropertyNameHandling _duplicatePropertyNameHandling;
	}
}
