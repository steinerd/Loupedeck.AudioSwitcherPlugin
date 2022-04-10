# AudioSwitcher Loupedeck Plugin
[![License](http://img.shields.io/:license-MIT-blue.svg?style=flat)](LICENSE)
![forks](https://img.shields.io/github/forks/Steinerd/Loupedeck.AudioSwitcherPlugin?style=flat)
![stars](https://img.shields.io/github/stars/Steinerd/Loupedeck.AudioSwitcherPlugin?style=flat)
![issues](https://img.shields.io/github/issues/Steinerd/Loupedeck.AudioSwitcherPlugin?style=flat)
[![downloads](https://img.shields.io/github/downloads/Steinerd/Loupedeck.AudioSwitcherPlugin/latest/total?style=plastic)(https://github.com/Steinerd/Loupedeck.AudioSwitcherPlugin/compare)
In short, this is a dynamically generated _folder_ accessible via "Audio Devices" button. 

## Credits

This plugin makes use of [Sean Chapman's/xenolightning](https://github.com/xenolightning) [AudioSwitcher](https://github.com/xenolightning/AudioSwitcher) project/NuGet Package. 

--------

# Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Support](#support)
- [Contributing](#contributing)
- [License](#license)

# Installation

<details><summary><b>Loupedeck Installation</b></summary>
  
  
  1. Go to [latest release](https://github.com/Steinerd/Loupedeck.AudioSwitcherPlugin/releases/latest), and download the `lplug4` file to you computer
  1. Open (normally double-click) to install, the Loupedeck software should take care of the rest
  1. Restart Loupedeck (if not handled by the installer)
  1. In the Loupedeck interface, enable **AudioSwitcher** by clicking <ins>Manage plugins</ins>
  1. Check the AudioSwitcher box on to enable
  1. Drag the desired control onto your layout

Once click it will bring you to a dynamic playback device selection page. 
</details>

<details><summary><b>IDE Installation</b></summary>
  Made with Visual Studio 2022, C# will likely only compile in VS2019 or greater. 

  Assuming Loupedeck is already installed on your machine, make sure you've stopped it before you debug the project. 

  Debugging _should_ build the solution, which will then output the DLL, config, and pdb into your `%LocalAppData%\Loupedeck\Plugins` directory.

  If all goes well, Loupedeck will then open and you can then debug. 

</details>

# Usage

Follow the __Loupedeck Installation__ instructions above, 
once completed you will then be able to drag the ***Audio Devices*** control onto any layout page or button in your Loupedeck.

As of writing this (Apr 9th, 2022), there is only the one control. 
I'm hoping to add more for recording devices, and an auxiliary control that allows you to chance both recording and playback simultaneously. 

# Support

[Submit an issue](https://github.com/Steinerd/Loupedeck.AudioSwitcherPlugin/issues/new)

Fill out the template to the best of your abilities and send it through. 

# Contribute

Easily done. Just [open a pull request](https://github.com/Steinerd/Loupedeck.AudioSwitcherPlugin/compare). 

Don't worry about specifics, I'll handle the minutia. 

# License
The MIT-License for this plugin can be reviewed at [LICENSE](LICENSE) attached to this repo.
