# SBIG_CFW8
ASCOM Driver to Control SBIG CFW8 filter wheel

The Driver that allow you to control a SBIG CFW8 Filter Wheel through serial port. The app will send data (pulse) to the serial port to activate wheel.<br/>
Information on CFW8 pulses : http://archive.sbig.com/pdffiles/cfw8.hardware.pdf

# Install
You need .NET Framwork 3.5 at least : https://www.microsoft.com/fr-fr/download/details.aspx?id=21<br/>
Download the file CFW8 Setup in the repository and launch tt.<br/>
Launch you favorite Astro imaging software, you will see CFW8 FilterWheel in ASCOM Dialogbox

# Build by your own
Clone the repository<br/>
You need to download Ascom Dev Components : http://ascom-standards.org/Downloads/PlatDevComponents.htm<br/>
Install Visual C# express 2010 (or higher)<br/>
Launch Visual C# and build the driver.

