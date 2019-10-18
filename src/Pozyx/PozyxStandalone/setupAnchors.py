from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates, DeviceCoordinates
from configparser import ConfigParser

#read setup file
parser = ConfigParser()
parser.read('config.ini')

#Change the remote ID for set-up from another device from config file
r_id = parser.get('default','remote_id')

#Setup variables
anchor1 = parser.get('default','anchor1_id')
anchor2 = parser.get('default','anchor2_id')
anchor3 = parser.get('default','anchor3_id')
anchor4 = parser.get('default','anchor4_id')

#Check if connected
serial_port = get_first_pozyx_serial_port()
if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
    exit()

try:
    ##Setup anchors
    #Clear previous setup
    pozyx.clearDevices(remote_id=r_id)
    print("List cleared.")

    #Anchor variables
    anchor1 = DeviceCoordinates(anchor1,1,Coordinates(parser.get('default','anchor1_coordinates')))
    anchor2 = DeviceCoordinates(anchor2,1,Coordinates(parser.get('default','anchor2_coordinates')))
    anchor3 = DeviceCoordinates(anchor3,1,Coordinates(parser.get('default','anchor3_coordinates')))
    anchor4 = DeviceCoordinates(anchor4,1,Coordinates(parser.get('default','anchor4_coordinates')))
    print("Coordinates set.")

    #Add Anchors to the device list
    pozyx.addDevice(anchor1, remote_id=r_id)
    pozyx.addDevice(anchor2, remote_id=r_id)
    pozyx.addDevice(anchor3, remote_id=r_id)
    pozyx.addDevice(anchor4, remote_id=r_id)
    print("Devices added.")

    #Don't abuse function, flash will fail
    #Save configuration of the anchors
    #pozyx.saveNetwork(remote_id=tagId)

#Exception exit
except:
    print("")
    print("Exit program")
