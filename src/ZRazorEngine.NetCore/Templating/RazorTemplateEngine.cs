using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorEngine.Templating
{
	/// <summary>
	/// Entry point to parse Razor files and generate code.
	/// </summary>
	// Token: 0x02000089 RID: 137
	public class RazorTemplateEngine
	{
		/// <summary>
		/// Initializes a new instance of <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorTemplateEngine" />.
		/// </summary>
		/// <param name="engine">The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorEngine" />.</param>
		/// <param name="project">The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProject" />.</param>
		// Token: 0x0600045F RID: 1119 RVA: 0x00009D6B File Offset: 0x00007F6B
		public RazorTemplateEngine(Microsoft.AspNetCore.Razor.Language.RazorEngine engine, Microsoft.AspNetCore.Razor.Language.RazorProject project)
		{
			if (engine == null)
			{
				throw new ArgumentNullException("engine");
			}
			if (project == null)
			{
				throw new ArgumentNullException("project");
			}
			this.Engine = engine;
			this.Project = project;
			this._options = new RazorTemplateEngineOptions();
		}

		/// <summary>
		/// Gets the <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorEngine" />.
		/// </summary>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public Microsoft.AspNetCore.Razor.Language.RazorEngine Engine { get; }

		/// <summary>
		/// Gets the <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProject" />.
		/// </summary>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00009DB0 File Offset: 0x00007FB0
		public Microsoft.AspNetCore.Razor.Language.RazorProject Project { get; }

		/// <summary>
		/// Options to configure <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorTemplateEngine" />.
		/// </summary>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00009DB8 File Offset: 0x00007FB8
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00009DC0 File Offset: 0x00007FC0
		public RazorTemplateEngineOptions Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._options = value;
			}
		}

		/// <summary>
		/// Parses the template specified by the project item <paramref name="path" />.
		/// </summary>
		/// <param name="path">The template path.</param>
		/// <returns>The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorCSharpDocument" />.</returns>
		// Token: 0x06000464 RID: 1124 RVA: 0x00009DD8 File Offset: 0x00007FD8
		public Microsoft.AspNetCore.Razor.Language.RazorCSharpDocument GenerateCode(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("Argument path Cannot Be Null Or Empty");
			}
			Microsoft.AspNetCore.Razor.Language.RazorProjectItem item = this.Project.GetItem(path);
			return this.GenerateCode(item);
		}

		/// <summary>
		/// Parses the template specified by <paramref name="projectItem" />.
		/// </summary>
		/// <param name="projectItem">The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProjectItem" />.</param>
		/// <returns>The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorCSharpDocument" />.</returns>
		// Token: 0x06000465 RID: 1125 RVA: 0x00009E14 File Offset: 0x00008014
		public Microsoft.AspNetCore.Razor.Language.RazorCSharpDocument GenerateCode(Microsoft.AspNetCore.Razor.Language.RazorProjectItem projectItem)
		{
			if (projectItem == null)
			{
				throw new ArgumentNullException("projectItem");
			}
			if (!projectItem.Exists)
			{
				throw new InvalidOperationException($"FormatRazorTemplateEngine_ItemCouldNotBeFound {projectItem.FilePath}");
			}
			Microsoft.AspNetCore.Razor.Language.RazorCodeDocument codeDocument = this.CreateCodeDocument(projectItem);
			return this.GenerateCode(codeDocument);
		}

		/// <summary>
		/// Parses the template specified by <paramref name="codeDocument" />.
		/// </summary>
		/// <param name="codeDocument">The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProjectItem" />.</param>
		/// <returns>The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorCSharpDocument" />.</returns>
		// Token: 0x06000466 RID: 1126 RVA: 0x00009E57 File Offset: 0x00008057
		public virtual Microsoft.AspNetCore.Razor.Language.RazorCSharpDocument GenerateCode(Microsoft.AspNetCore.Razor.Language.RazorCodeDocument codeDocument)
		{
			if (codeDocument == null)
			{
				throw new ArgumentNullException("codeDocument");
			}
			this.Engine.Process(codeDocument);
			return codeDocument.GetCSharpDocument();
		}

		/// <summary>
		/// Generates a <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorCodeDocument" /> for the specified <paramref name="path" />.
		/// </summary>
		/// <param name="path">The template path.</param>
		/// <returns>The created <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorCodeDocument" />.</returns>
		// Token: 0x06000467 RID: 1127 RVA: 0x00009E7C File Offset: 0x0000807C
		public virtual Microsoft.AspNetCore.Razor.Language.RazorCodeDocument CreateCodeDocument(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("Argument path Cannot Be Null Or Empty");
			}
			Microsoft.AspNetCore.Razor.Language.RazorProjectItem item = this.Project.GetItem(path);
			return this.CreateCodeDocument(item);
		}

		/// <summary>
		/// Generates a <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorCodeDocument" /> for the specified <paramref name="projectItem" />.
		/// </summary>
		/// <param name="projectItem">The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProjectItem" />.</param>
		/// <returns>The created <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorCodeDocument" />.</returns>
		// Token: 0x06000468 RID: 1128 RVA: 0x00009EB8 File Offset: 0x000080B8
		public virtual Microsoft.AspNetCore.Razor.Language.RazorCodeDocument CreateCodeDocument(Microsoft.AspNetCore.Razor.Language.RazorProjectItem projectItem)
		{
			if (projectItem == null)
			{
				throw new ArgumentNullException("projectItem");
			}
			if (!projectItem.Exists)
			{
				throw new InvalidOperationException($"FormatRazorTemplateEngine_ItemCouldNotBeFound {projectItem.FilePath}");
			}
			Microsoft.AspNetCore.Razor.Language.RazorSourceDocument source = Microsoft.AspNetCore.Razor.Language.RazorSourceDocument.ReadFrom(projectItem);
			IEnumerable<Microsoft.AspNetCore.Razor.Language.RazorSourceDocument> imports = this.GetImports(projectItem);
			return Microsoft.AspNetCore.Razor.Language.RazorCodeDocument.Create(source, imports);
		}

		/// <summary>
		/// Gets <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorSourceDocument" /> that are applicable to the specified <paramref name="path" />.
		/// </summary>
		/// <param name="path">The template path.</param>
		/// <returns>The sequence of applicable <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorSourceDocument" />.</returns>
		// Token: 0x06000469 RID: 1129 RVA: 0x00009F00 File Offset: 0x00008100
		public IEnumerable<Microsoft.AspNetCore.Razor.Language.RazorSourceDocument> GetImports(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("Argument path Cannot Be Null Or Empty");
			}
			Microsoft.AspNetCore.Razor.Language.RazorProjectItem item = this.Project.GetItem(path);
			return this.GetImports(item);
		}

		/// <summary>
		/// Gets <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorSourceDocument" /> that are applicable to the specified <paramref name="projectItem" />.
		/// </summary>
		/// <param name="projectItem">The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProjectItem" />.</param>
		/// <returns>The sequence of applicable <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorSourceDocument" />.</returns>
		// Token: 0x0600046A RID: 1130 RVA: 0x00009F3C File Offset: 0x0000813C
		public virtual IEnumerable<Microsoft.AspNetCore.Razor.Language.RazorSourceDocument> GetImports(Microsoft.AspNetCore.Razor.Language.RazorProjectItem projectItem)
		{
			if (projectItem == null)
			{
				throw new ArgumentNullException("projectItem");
			}
			List<Microsoft.AspNetCore.Razor.Language.RazorSourceDocument> list = new List<Microsoft.AspNetCore.Razor.Language.RazorSourceDocument>();
			foreach (Microsoft.AspNetCore.Razor.Language.RazorProjectItem razorProjectItem in this.GetImportItems(projectItem))
			{
				if (razorProjectItem.Exists)
				{
					list.Insert(0, Microsoft.AspNetCore.Razor.Language.RazorSourceDocument.ReadFrom(razorProjectItem));
				}
			}
			if (this.Options.DefaultImports != null)
			{
				list.Insert(0, this.Options.DefaultImports);
			}
			return list;
		}

		/// <summary>
		/// Gets the sequence of imports with the name specified by <see cref="P:Microsoft.AspNetCore.Razor.Language.RazorTemplateEngineOptions.ImportsFileName" />
		/// that apply to <paramref name="path" />.
		/// </summary>
		/// <param name="path">The path to look up import items for.</param>
		/// <returns>A sequence of <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProjectItem" /> instances that apply to the
		/// <paramref name="path" />.</returns>
		// Token: 0x0600046B RID: 1131 RVA: 0x00009FCC File Offset: 0x000081CC
		public IEnumerable<Microsoft.AspNetCore.Razor.Language.RazorProjectItem> GetImportItems(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("Argument path Cannot Be Null Or Empty");
			}
			Microsoft.AspNetCore.Razor.Language.RazorProjectItem item = this.Project.GetItem(path);
			return this.GetImportItems(item);
		}

		/// <summary>
		/// Gets the sequence of imports with the name specified by <see cref="P:Microsoft.AspNetCore.Razor.Language.RazorTemplateEngineOptions.ImportsFileName" />
		/// that apply to <paramref name="projectItem" />.
		/// </summary>
		/// <param name="projectItem">The <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProjectItem" /> to look up import items for.</param>
		/// <returns>A sequence of <see cref="T:Microsoft.AspNetCore.Razor.Language.RazorProjectItem" /> instances that apply to the
		/// <paramref name="projectItem" />.</returns>
		// Token: 0x0600046C RID: 1132 RVA: 0x0000A008 File Offset: 0x00008208
		public virtual IEnumerable<Microsoft.AspNetCore.Razor.Language.RazorProjectItem> GetImportItems(Microsoft.AspNetCore.Razor.Language.RazorProjectItem projectItem)
		{
			string importsFileName = this.Options.ImportsFileName;
			if (!string.IsNullOrEmpty(importsFileName))
			{
				return this.Project.FindHierarchicalItems(projectItem.FilePath, importsFileName);
			}
			return Enumerable.Empty<Microsoft.AspNetCore.Razor.Language.RazorProjectItem>();
		}

		// Token: 0x04000114 RID: 276
		private RazorTemplateEngineOptions _options;
	}
}
