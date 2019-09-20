from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates, DeviceCoordinates

#Change the remote ID for checking coordinates from another device
r_id = None

#change the ID's for checking coordinates of another device
device1 = 0x6734
device2 = 0x671f
device3 = 0x6e13
device4 = 0x6642

#Setup variables
positionDevice1 = Coordinates()
positionDevice2 = Coordinates()
positionDevice3 = Coordinates()
positionDevice4 = Coordinates()

#Check if connected
serial_port = get_first_pozyx_serial_port()
if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
    exit()

#Obtain the device coordinates from the device list
pozyx.getDeviceCoordinates(device1,positionDevice1,r_id)
print(str(device1) + ": " + str(positionDevice1))
pozyx.getDeviceCoordinates(device2,positionDevice2,r_id)
print(str(device2) + ": " + str(positionDevice2))
pozyx.getDeviceCoordinates(device3,positionDevice3,r_id)
print(str(device3) + ": " + str(positionDevice3))
pozyx.getDeviceCoordinates(device4,positionDevice4,r_id)
print(str(device4) + ": " + str(positionDevice4))


#Exception exit
except:
    print("")
    print("Exit program")
