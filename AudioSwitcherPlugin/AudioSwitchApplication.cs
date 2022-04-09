namespace Loupedeck.AudioSwitcherPlugin
{
    using System;

    public class AudioSwitchApplication : ClientApplication
    {
        public AudioSwitchApplication() { }

        protected override String GetProcessName() => String.Empty;

        protected override String GetBundleName() => String.Empty;
    }
}