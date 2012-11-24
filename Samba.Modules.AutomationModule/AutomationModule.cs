﻿using System.ComponentModel.Composition;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Samba.Domain.Models.Automation;
using Samba.Localization.Properties;
using Samba.Presentation.Common;
using Samba.Presentation.Common.ModelBase;
using Samba.Presentation.Services.Common;

namespace Samba.Modules.AutomationModule
{
    [ModuleExport(typeof(AutomationModule))]
    class AutomationModule : ModuleBase
    {
        [ImportingConstructor]
        public AutomationModule()
        {
            AddDashboardCommand<EntityCollectionViewModelBase<RuleActionViewModel, AppAction>>(Resources.RuleActions, Resources.Automation, 45);
            AddDashboardCommand<EntityCollectionViewModelBase<RuleViewModel, AppRule>>(Resources.Rules, Resources.Automation, 45);
            AddDashboardCommand<TriggerListViewModel>(Resources.Trigger.ToPlural(), Resources.Automation, 45);
            AddDashboardCommand<EntityCollectionViewModelBase<AutomationCommandViewModel, AutomationCommand>>(Resources.AutomationCommand.ToPlural(), Resources.Automation, 45);
            AddDashboardCommand<EntityCollectionViewModelBase<ScriptViewModel, Script>>(Resources.Script.ToPlural(), Resources.Automation, 45);

            HighlightingManager.Instance.RegisterHighlighting("SambaDSL", null, () => LoadHighlightingDefinition("SambaDSL.xshd"));
        }

        public static IHighlightingDefinition LoadHighlightingDefinition(string resourceName)
        {
            var type = typeof(AutomationModule);
            var fullName = type.Namespace + "." + resourceName;
            using (var stream = type.Assembly.GetManifestResourceStream(fullName))
            using (var reader = new XmlTextReader(stream))
                return HighlightingLoader.Load(reader, HighlightingManager.Instance);
        }
    }
}
