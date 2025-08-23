using System;
using CXE.CoreFx.Base;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
    [DataverseTable("template")]
    public partial class EmailTemplateModel : EntityBase
    {
        public EmailTemplateModel() : base("template") { }
        public EmailTemplateModel(Guid id) : base("template", id) { }

        [DataverseColumn("templateid")]
        public Guid TemplateId
        {
            get => this.GetValue<Guid>();
            set => this.SetValue(value);
        }

        [DataverseColumn("title")]
        public string Title
        {
            get => this.GetValue<string>();
            set => this.SetValue(value);
        }
    }
}





