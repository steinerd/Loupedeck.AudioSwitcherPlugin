namespace Loupedeck.Steinerd.AudioSwitcherPlugin
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection.Metadata;

    using AudioSwitcher.AudioApi.CoreAudio;

    // This class contains the plugin-level logic of the Loupedeck plugin.

    public class AudioSwitcherPlugin : Plugin
    {
        // Gets a value indicating whether this is an API-only plugin.
        public override Boolean UsesApplicationApiOnly => true;

        // Gets a value indicating whether this is a Universal plugin or an Application plugin.
        public override Boolean HasNoApplication => true;

        internal CoreAudioController AudioController { get; private set; }
        internal ConcurrentStack<CoreAudioDevice> ActiveDevices { get; private set; }

        public static AudioSwitcherPlugin Instance { get; private set; }

        // Initializes a new instance of the plugin class.
        public AudioSwitcherPlugin()
        {
            // Initialize the plugin log.
            PluginLog.Init(this.Log);

            // Initialize the plugin resources.
            PluginResources.Init(this.Assembly);

            Instance ??= this;

            this.ActiveDevices ??= new ConcurrentStack<CoreAudioDevice>();

            if (this.AudioController == null)
            {
                // Without this delay the Audio Knob (if used) stops working....
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(15));
                    this.AudioController = new CoreAudioController();
                    this.RefreshDevices();
                });
            }
        }

        public override void RunCommand(String commandName, String parameter)
        {
            if (parameter == "Refresh")
            {
                this.RefreshDevices();
            }

            base.RunCommand(commandName, parameter);
        }

        // This method is called when the plugin is loaded.
        public override void Load() => this.LoadPluginIcons();

        // This method is called when the plugin is unloaded.
        public override void Unload()
        {
        }
        private void LoadPluginIcons()
        {
            this.Info.Icon16x16 = EmbeddedResources.ReadImage(EmbeddedResources.FindFile("16.png"));
            this.Info.Icon32x32 = EmbeddedResources.ReadImage(EmbeddedResources.FindFile("32.png"));
            this.Info.Icon48x48 = EmbeddedResources.ReadImage(EmbeddedResources.FindFile("48.png"));
            this.Info.Icon256x256 = EmbeddedResources.ReadImage(EmbeddedResources.FindFile("256.png"));
        }

        internal void RefreshDevices()
        {
            this.ActiveDevices.Clear();
            this.ActiveDevices.PushRange(this.AudioController.GetPlaybackDevices(AudioSwitcher.AudioApi.DeviceState.Active).ToArray());
        }
    }
}
