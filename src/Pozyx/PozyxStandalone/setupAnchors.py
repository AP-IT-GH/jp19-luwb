from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates, DeviceCoordinates
from configparser import ConfigParser

#read setup file
cParser = ConfigParser()
cParser.read('config.ini')

#Change the remote ID for set-up from another device from config file
r_id = int(cParser.get('default','remote_id'),16)

#Setup variables
anchor1 = int(cParser.get('default','anchor1_id'),16)
anchor2 = int(cParser.get('default','anchor2_id'),16)
anchor3 = int(cParser.get('default','anchor3_id'),16)
anchor4 = int(cParser.get('default','anchor4_id'),16)
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
    anchor1 = DeviceCoordinates(anchor1,1,Coordinates(anchor1X,anchor1Y,anchor1Z))
    anchor2 = DeviceCoordinates(anchor2,1,Coordinates(anchor2X,anchor2Y,anchor2Z))
    anchor3 = DeviceCoordinates(anchor3,1,Coordinates(anchor3X,anchor3Y,anchor3Z))
    anchor4 = DeviceCoordinates(anchor4,1,Coordinates(anchor4X,anchor4Y,anchor4Z))
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
