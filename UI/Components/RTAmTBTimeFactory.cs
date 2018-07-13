using LiveSplit.Delta;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(RTAmTBTimeFactory))]

namespace LiveSplit.Delta
{
    public class RTAmTBTimeFactory : IComponentFactory
    {
        public string ComponentName => "RTA-TB";

        public string Description => "Allows to time in RTA minus time bonuses.";

        public ComponentCategory Category => ComponentCategory.Control;

        public IComponent Create(LiveSplitState state) => new RTAmTBTimeComponent(state);

        public string UpdateName => ComponentName;

        public string UpdateURL => "https://raw.githubusercontent.com/LakeIshikawa/LiveSplit.RTA-TB/master/";

        public string XMLURL => UpdateURL + "Components/update.LiveSplit.RTA-TB.xml";

        public Version Version => Version.Parse("0.0.1");
    }
}
