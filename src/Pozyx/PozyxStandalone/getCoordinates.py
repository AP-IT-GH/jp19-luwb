from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates
from configparser import ConfigParser

#read setup file
cParser = ConfigParser()
cParser.read('config.ini')
connectedDevice = int(cParser.get('default','connected_id'),16)
anchor1ID = int(cParser.get('default','anchor1_id'),16)
anchor2ID = int(cParser.get('default','anchor2_id'),16) 
anchor3ID = int(cParser.get('default','anchor3_id'),16) 
anchor4ID = int(cParser.get('default','anchor4_id'),16)  

#Change the remote ID's for getting coordinates of another device from config file
anchor1 = anchor1ID if connectedDevice != anchor1ID else None
anchor2 = anchor2ID if connectedDevice != anchor2ID else None
anchor3 = anchor3ID if connectedDevice != anchor3ID else None
anchor4 = anchor4ID if connectedDevice != anchor4ID else None

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
    #Obtain the device Coordinates
    pozyx.getCoordinates(coordinatesAnchor1, anchor1)
    print(hex(anchor1ID) + ": " + str(coordinatesAnchor1))
    pozyx.getCoordinates(coordinatesAnchor2, anchor2)
    print(hex(anchor2ID) + ": " + str(coordinatesAnchor2))
    pozyx.getCoordinates(coordinatesAnchor3, anchor3)
    print(hex(anchor3ID) + ": " + str(coordinatesAnchor3))
    pozyx.getCoordinates(coordinatesAnchor4, anchor4)
    print(hex(anchor4ID) + ": " + str(coordinatesAnchor4))

#Exception exit
except:
    print("")
    print("Exit program")
