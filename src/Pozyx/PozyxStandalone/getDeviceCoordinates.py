from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates, DeviceCoordinates
from configparser import ConfigParser

#read setup file
cParser = ConfigParser()
cParser.read('config.ini')

#Change the remote ID for getting coordinates from another device from config file
r_id = int(cParser.get('default','remote_id'),16)

#change the ID's for checking coordinates of another device
anchor1 = int(cParser.get('default','anchor1_id'),16)
anchor2 = int(cParser.get('default','anchor2_id'),16)
anchor3 = int(cParser.get('default','anchor3_id'),16)
anchor4 = int(cParser.get('default','anchor4_id'),16)

#Setup variables
coordinatesAnchor1 = Coordinates()
coordinatesAnchor2 = Coordinates()
coordinatesAnchor3 = Coordinates()
coordinatesAnchor4 = Coordinates()

#Check if connected
serial_port = get_first_pozyx_serial_port()
if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
    exit()

try:
    #Obtain the device coordinates from the device list
    pozyx.getDeviceCoordinates(anchor1,coordinatesAnchor1,r_id)
    print(hex(anchor1) + ": " + str(coordinatesAnchor1))
    pozyx.getDeviceCoordinates(anchor2,coordinatesAnchor2,r_id)
    print(hex(anchor2) + ": " + str(coordinatesAnchor2))
    pozyx.getDeviceCoordinates(anchor3,coordinatesAnchor3,r_id)
    print(hex(anchor3) + ": " + str(coordinatesAnchor3))
    pozyx.getDeviceCoordinates(anchor4,coordinatesAnchor4,r_id)
    print(hex(anchor4) + ": " + str(coordinatesAnchor4))

#Exception exit
except:
    print("")
    print("Exit program")
