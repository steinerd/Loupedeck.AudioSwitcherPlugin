namespace Loupedeck.Steinerd.AudioSwitcherPlugin.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Loupedeck;
    using Loupedeck.Steinerd.AudioSwitcherPlugin;

    public class AudioDevicesFolder : PluginDynamicFolder
    {
        public static AudioDevicesFolder Instance { get; private set; }

        public AudioDevicesFolder()
        {
            Instance = this;
            this.DisplayName = "Audio\nDevices";
            this.GroupName = "Controls";

            var navArea = this.GetNavigationArea(DeviceType.All);

        }

        public override IEnumerable<String> GetEncoderPressActionNames(DeviceType deviceType)
        {
            return
            [
                null, null, null,
                this.CreateAdjustmentName("Refresh")
            ];
        }

        public override IEnumerable<String> GetButtonPressActionNames(DeviceType deviceType)
        {

            var systemButtons = new[]
            {
                NavigateUpActionName,
            };

            return !AudioSwitcherPlugin.Instance.ActiveDevices.IsEmpty
                ? systemButtons.Concat(AudioSwitcherPlugin.Instance.ActiveDevices.Select(s => this.CreateCommandName(s.Id.ToString("n"))))
                : systemButtons.Concat([this.CreateCommandName("Please\nWait...")]);
        }


        public override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            using var builder = new BitmapBuilder(imageSize);
                        
            builder.Clear(new BitmapColor(0, 0, 0));

            var device = AudioSwitcherPlugin.Instance.ActiveDevices.FirstOrDefault(f => f.Id.ToString("n") == actionParameter);

            if (device != null)
            {
                if (device.IsDefaultDevice)
                {
                    builder.Clear(new BitmapColor(0, 100, 0));
                }               
            }

            builder.DrawText(device.InterfaceName, BitmapColor.White);

            
            return builder.ToImage();
        }

        public override void RunCommand(String actionParameter)
        {
            this.EncoderActionNamesChanged();

            AudioSwitcherPlugin.Instance.ActiveDevices.FirstOrDefault(f => f.Id.ToString("n") == actionParameter)?.SetAsDefault();
            this.CommandImageChanged(actionParameter);

        }


    }
}
