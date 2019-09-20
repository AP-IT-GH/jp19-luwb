from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates, DeviceCoordinates

#Change the remote ID for set-up from another device
r_id = None

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


##Setup anchors
#Clear previous setup
pozyx.clearDevices()

#Anchor variables
anchor0x6734 = DeviceCoordinates(0x6734,1,Coordinates(2212,0,2912))
anchor0x671f = DeviceCoordinates(0x671f,1,Coordinates(100,3875,2424))
anchor0x6e13 = DeviceCoordinates(0x6e13,1,Coordinates(4497,9878,1981))
anchor0x6642 = DeviceCoordinates(0x6642,1,Coordinates(100,9929,2533))

#Add Anchors to the tag device list
pozyx.addDevice(anchor0x6734, remote_id=tagId)
pozyx.addDevice(anchor0x671f, remote_id=tagId)
pozyx.addDevice(anchor0x6e13, remote_id=tagId)
pozyx.addDevice(anchor0x6642, remote_id=tagId)

#Don't abuse function, flash will fail
#Save configuration of the anchors
#pozyx.saveNetwork(remote_id=tagId)

#Exception exit
except:
    print("")
    print("Exit program")
