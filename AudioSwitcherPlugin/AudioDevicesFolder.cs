namespace Loupedeck.AudioSwitcherPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AudioSwitcher.AudioApi.CoreAudio;

    public class AudioDevicesFolder : PluginDynamicFolder
    {
        public static AudioDevicesFolder Instance { get; private set; }

        public AudioDevicesFolder()
        {
            Instance = this;
            this.DisplayName = "Audio\nDevices";
            this.GroupName = "Controls";
            this.Navigation = PluginDynamicFolderNavigation.ButtonArea;
        }

        public override IEnumerable<String> GetEncoderPressActionNames()
        {
            return new[]
            {
                null, null, null,
                this.CreateAdjustmentName("Refresh")
            };
        }

        public override IEnumerable<String> GetButtonPressActionNames()
        {

            var systemButtons = new[]
            {
                PluginDynamicFolder.NavigateUpActionName,
            };

            if (AudioSwitcherPlugin.Instance.ActiveDevices.Count > 0)
            {
                return systemButtons.Concat(AudioSwitcherPlugin.Instance.ActiveDevices.Select(s => this.CreateCommandName(s.Name)));
            }

            return systemButtons.Concat(new[] { this.CreateCommandName("Please\nWait...") });
        }


        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var builder = new BitmapBuilder(imageSize);

            builder.Clear(new BitmapColor(0, 0, 0));

            var device = AudioSwitcherPlugin.Instance.ActiveDevices.FirstOrDefault(f => f.Name == actionParameter);

            if (device != null)
            {
                if (device.IsDefaultDevice)
                {
                    builder.Clear(new BitmapColor(0, 100, 0));
                }
            }

            builder.DrawText(actionParameter, BitmapColor.White);

            return builder.ToImage();
        }

        public override void RunCommand(String actionParameter)
        {
            AudioSwitcherPlugin.Instance.ActiveDevices.FirstOrDefault(f => f.Name == actionParameter)?.SetAsDefault();
            this.CommandImageChanged(actionParameter);
        }


    }
}
