namespace Loupedeck.AudioSwitcherPlugin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AudioSwitcher.AudioApi.CoreAudio;

    internal class PressToTalkCommand : PluginDynamicCommand, INotifyPropertyChanged
    {

        public bool PTTState
        {
            get => this._pttState;
            set
            {
                this._pttState = value;
                this.OnPropertyChange(nameof(this.PTTState));

            }
        }
        private bool _pttState = false;

        public CoreAudioDevice DefaultCommunicationDevice =>
            AudioSwitcherPlugin.Instance.AudioController.GetDefaultDevice(AudioSwitcher.AudioApi.DeviceType.Capture, AudioSwitcher.AudioApi.Role.Communications);
        public PressToTalkCommand() : base("PTT", "Hold to PTT", "Controls") { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(String propertyName)
        {
            if (propertyName == nameof(this.PTTState))
            {                
                this.DefaultCommunicationDevice.Mute(!this.PTTState);                
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected override Boolean ProcessButtonEvent(String actionParameter, DeviceButtonEvent buttonEvent)
        {
            if (buttonEvent.IsPressed)
            {
                this.PTTState = true;
                this.ActionImageChanged();
                return true;
            }
            else if (!buttonEvent.IsPressed)
            {
                this.PTTState = false;
                this.ActionImageChanged();
                return true;
            }

            return true;
        }

        protected override Boolean ProcessTouchEvent(String actionParameter, DeviceTouchEvent touchEvent)
        {

            if (touchEvent.EventType == DeviceTouchEventType.TouchDown)
            {
                this.PTTState = true;
                this.ActionImageChanged();
                return true;
            }
            else if (touchEvent.EventType == DeviceTouchEventType.TouchUp)
            {
                this.PTTState = false;
                this.ActionImageChanged();
                return true;
            }

            return true;
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var builder = new BitmapBuilder(imageSize);
            builder.Clear(this.PTTState ? new BitmapColor(0, 255, 0) : new BitmapColor(0, 0, 0));

            builder.DrawText("PTT", BitmapColor.White);


            return builder.ToImage();
        }
    }
}
