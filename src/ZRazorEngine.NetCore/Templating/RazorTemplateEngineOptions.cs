using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazorEngine.Templating
{
	/// <summary>
	/// Options for code generation in the <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorTemplateEngine" />.
	/// </summary>
	// Token: 0x0200008A RID: 138
	public sealed class RazorTemplateEngineOptions
	{
		/// <summary>
		/// Gets or sets the file name of the imports file (e.g. _ViewImports.cshtml).
		/// </summary>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000A041 File Offset: 0x00008241
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000A049 File Offset: 0x00008249
		public string ImportsFileName { get; set; }

		/// <summary>
		/// Gets or sets the default set of imports.
		/// </summary>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000A052 File Offset: 0x00008252
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000A05A File Offset: 0x0000825A
		public RazorSourceDocument DefaultImports { get; set; }
	}
}
