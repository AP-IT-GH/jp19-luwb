from pypozyx import PozyxSerial, get_first_pozyx_serial_port, PozyxConstants, POZYX_SUCCESS
from configparser import ConfigParser

#read setup file
cParser = ConfigParser()
cParser.read('config.ini')

#Get the connected device
connectedDevice = int(cParser.get('default','connected_id'),16)

#Check if connected
serial_port = get_first_pozyx_serial_port()
if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
    exit()

try:
    #Does discovery on the device and print the found devices
    if pozyx.doDiscovery(discovery_type=PozyxConstants.DISCOVERY_ALL_DEVICES) == POZYX_SUCCESS:
        print("Connected Device: " + hex(connectedDevice))
        print("Found devices:")
        pozyx.printDeviceList(include_coordinates=False)

#Exception exit
except:
    print("")
    print("Exit program")
