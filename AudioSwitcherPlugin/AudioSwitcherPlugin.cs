namespace Loupedeck.AudioSwitcherPlugin
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using System.Linq;

    using AudioSwitcher.AudioApi.CoreAudio;

    public class AudioSwitcherPlugin : Plugin
    {
        public override Boolean HasNoApplication => true;
        public override Boolean UsesApplicationApiOnly => true;

        public static AudioSwitcherPlugin Instance { get; private set; }
        internal CoreAudioController AudioController { get; private set; }
        internal ConcurrentStack<CoreAudioDevice> ActiveDevices { get; private set; }

        public AudioSwitcherPlugin()
        {
            if (Instance == null)
                Instance = this;

            if (this.AudioController == null)
            {
                // Without this delay the Audio Knob (if used) stops working....
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(10));
                    this.AudioController = new CoreAudioController();
                    this.RefreshDevices();
                });
            }

            if (this.ActiveDevices == null)
            {
                this.ActiveDevices = new ConcurrentStack<CoreAudioDevice>();
            }
        }

        public override void Load() => this.LoadPluginIcons();

        public override void Unload() => this.AudioController.Dispose();

        private void OnApplicationStarted(Object sender, EventArgs e) { }

        private void OnApplicationStopped(Object sender, EventArgs e) { }

        public override void RunCommand(String commandName, String parameter)
        {
            if (parameter == "Refresh")
            {
                this.RefreshDevices();
            }
        }

        public override void ApplyAdjustment(String adjustmentName, String parameter, Int32 diff) { }

        private void LoadPluginIcons()
        {
            this.Info.Icon16x16 = EmbeddedResources.ReadImage("Loupedeck.AudioSwitcherPlugin.Resources.16.png");
            this.Info.Icon32x32 = EmbeddedResources.ReadImage("Loupedeck.AudioSwitcherPlugin.Resources.32.png");
            this.Info.Icon48x48 = EmbeddedResources.ReadImage("Loupedeck.AudioSwitcherPlugin.Resources.48.png");
            this.Info.Icon256x256 = EmbeddedResources.ReadImage("Loupedeck.AudioSwitcherPlugin.Resources.256.png");
        }

        internal void RefreshDevices()
        {
            this.ActiveDevices.Clear();
            this.ActiveDevices.PushRange(this.AudioController.GetPlaybackDevices(AudioSwitcher.AudioApi.DeviceState.Active).ToArray());
        }
    }
}
