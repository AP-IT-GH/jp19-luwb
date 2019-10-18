from pypozyx import PozyxSerial, get_first_pozyx_serial_port, PozyxConstants, POZYX_SUCCESS
from configparser import ConfigParser

#read setup file
parser = ConfigParser()
parser.read('config.ini')

#Change the remote ID for discovering from another device from config file
r_id = parser.get('default','remote_id')

#Check if connected
serial_port = get_first_pozyx_serial_port()
if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
    exit()

try:
    #Clears the device list
    pozyx.clearDevices(r_id)

    #Does discovery on the device and print the found devices
    if pozyx.doDiscovery(discovery_type=PozyxConstants.DISCOVERY_ALL_DEVICES, remote_id=r_id) == POZYX_SUCCESS:
        print("Found devices:")
        pozyx.printDeviceList(r_id ,include_coordinates=False)

#Exception exit
except:
    print("")
    print("Exit program")
