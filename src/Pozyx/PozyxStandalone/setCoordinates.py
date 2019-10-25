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
anchor1X = int(cParser.get('default','anchor1_X'))
anchor1Y = int(cParser.get('default','anchor1_Y'))
anchor1Z = int(cParser.get('default','anchor1_Z'))
anchor2X = int(cParser.get('default','anchor2_X'))
anchor2Y = int(cParser.get('default','anchor2_Y'))
anchor2Z = int(cParser.get('default','anchor2_Z'))
anchor3X = int(cParser.get('default','anchor3_X'))
anchor3Y = int(cParser.get('default','anchor3_Y'))
anchor3Z = int(cParser.get('default','anchor3_Z'))
anchor4X = int(cParser.get('default','anchor4_X'))
anchor4Y = int(cParser.get('default','anchor4_Y'))
anchor4Z = int(cParser.get('default','anchor4_Z'))

#change the ID's for setting coordinates of devices from config file
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
    #Set the coordinates in the device
    pozyx.setCoordinates(Coordinates(anchor1X,anchor1Y,anchor1Z), anchor1)
    pozyx.setCoordinates(Coordinates(anchor2X,anchor2Y,anchor2Z), anchor2)
    pozyx.setCoordinates(Coordinates(anchor3X,anchor3Y,anchor3Z), anchor3)
    pozyx.setCoordinates(Coordinates(anchor4X,anchor4Y,anchor4Z), anchor4)
    print("Coordinates set.")

    #get the coordinates of the device
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
