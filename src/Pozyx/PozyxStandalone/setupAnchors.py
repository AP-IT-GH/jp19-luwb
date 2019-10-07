from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates, DeviceCoordinates

#Change the remote ID for set-up from another device
r_id = 0x673a

#Setup variables
positionAnchor = Coordinates()

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
    pozyx.clearDevices()
    print("List cleared.")

    #Anchor variables
    anchor0x6734 = DeviceCoordinates(0x6734,1,Coordinates(711,0,1954))
    anchor0x6642 = DeviceCoordinates(0x6642,1,Coordinates(10,5998,2504))
    anchor0x6e13 = DeviceCoordinates(0x6e13,1,Coordinates(4398,5930,2206))
    anchor0x671f = DeviceCoordinates(0x671f,1,Coordinates(4153,0,2524))
    print("Coordinates set.")

    #Add Anchors to the device list
    pozyx.addDevice(anchor0x6734, remote_id=r_id)
    pozyx.addDevice(anchor0x6642, remote_id=r_id)
    pozyx.addDevice(anchor0x6e13, remote_id=r_id)
    pozyx.addDevice(anchor0x671f, remote_id=r_id)
    print("Devices added.")

    #Don't abuse function, flash will fail
    #Save configuration of the anchors
    #pozyx.saveNetwork(remote_id=tagId)

#Exception exit
except:
    print("")
    print("Exit program")
