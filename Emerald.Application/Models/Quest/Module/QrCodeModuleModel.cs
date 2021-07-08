using Emerald.Application.Models.Quest.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Module
{
    public class QrCodeModuleModel : ModuleModel
    {
        public QrCodeModuleModel()
        {
        }

        public QrCodeModuleModel(string moduleId, string objective, List<ComponentModel> components) : base(moduleId, objective, ModuleType.QrCode, components)
        {
        }
    }
}
