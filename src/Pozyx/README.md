# Script Overview

## This is the documentation of the Pozyx scripts

* PozyxSoftware  
   These scripts need the Pozyx software to be running on a computer.
   * connectLocal.py  
      This script can connect to the software via an IP-address in the local network.  
      It prints the coordinates in the terminal and can export it to a txt-file.
   * connectCloud.py  
      This script connects to the software via the cloud using a topic, username and password.  
      It also prints the coordinates to the terminal.

* PozyxRPi  
   Those are mostly test scripts and are not used anymore.
   * pyTest.py  
      This script tests if there is a Pozyx device connected.  
      If it doesn't give an error things are fine and a device is connected.
   * readingData.py  
      It checks if it can succesfully read the data of the device.  
      If it prints "0x43" everything is fine.
   * UWBsettings.py  
      UWBsettings reads the uwb settings from the connected device and prints the settings.
   * positioning.py  
      This positions the directly connected tag and prints the result in the terminal.  
      It can export the position to a txt-file.

* PozyxStandalone  
   These are the scripts that are used as standalone that do not depend on the pozyx software.  
   Most of those scripts have a "r_id" variable which is used to choose on which device the commands are executed. If it is set to "None" then the commands will be preformed on the directly connected device. Otherwise they will be done from the device of which the ID is set.
   * setupAnchors.py  
      This setup clears the device list of the device.  
      It then adds the ID's and coordinates of the set devices to the device list of the device.  
      It can be saved to the flash but this is disabled due to testing purposes.
   * setCoordinates.py  
      This script changes the coordinates of the given devices in the memory of those devices.
      It also prints all the set coordinates as a check.
   * discoverDevices.py  
      It discovers the devices with the same UWB-settings which are in range.  
      And prints them to the terminal.
   * getCoordinates.py  
      This script checks the coordinates from the devices which ID's are given.  
      It gets the coordinates from the memory of the devices them self.  
      This can be used after the setCoordinates script.
   * getDeviceCoordinates.py  
      The script gets the coordinates of the asked devices out of the device list of the device where the script is executed.  
      It can be used after the setupAnchors script.
   * positioningTag.py  
      This is the script you will use the most.
      This does the positioning of the given device and prints it to the terminal.
      It is looped so you have to stop the script.
