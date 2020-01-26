# Keep Talking or Suffer a Windows Update
### Submitted for Boilermake VII, January 26th, 2020
#### Description:

In this 2 player co-operative game, players work together to prevent a deadline's worst enemy--an untimely Windows
update. This game is asymmetric in the sense that one player will play using a VR headset while the other will be
playing from a  typical PC client. The player in the VR headset will be thrust into a dimly lit room surrounded by a few
physical interfaces as well as a laptop and a monitor. During the course of the game, the monitor will display clues as
to what physical interfaces need to be pressed to delay the update which is counting down on the laptop. In order to
actually use these clues, the player in the VR headset will need to work together with the PC player because they both
have information privy to the other. Together they will have to figure out how to delay the inevitable through
cooperation or risk hastening the most irritating of inconveniences by using the wrong interfaces!  
  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;_"Death, Taxes, and Inopportune Windows Updates"_ -Benjamin Franklin
  
#### Implementation Overview:


<ul>
<li> VR Game:
    <ul> Written in C# within the Unity engine </ul>
    <ul> APK built using Android Studio </ul>
</li>
<li> PC Game:
    <ul> Written in C# within the Unity engine </ul>
</li>
<li> Backend Server:
    <ul> Concurrent server written in Java </ul>
</li>
</ul>

#### Instructions:
VR Player:  
  
**tl;dr:** Tell the PC player the color of the font displayed on the monitor. Then press the button they tell you

  1. A color will display on the monitor. Tell the PC player the color **of the font**
  2. The PC player will use that information to tell you what physical interface to use
  3. When you press the button the monitor will change, repeat 1-2
  4. If you successfully interact with three correct interfaces in a row you win. If time runs out on your update, you
  lose.
  
  PC Player:
    
  While a seemingly less glorious role, you're the brains of this operation--the VR player is just your monkey to
  command. In a more complete version of the game, the puzzles you need to solve would be more challenging.
  