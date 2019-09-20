from pypozyx import PozyxSerial, get_first_pozyx_serial_port, Coordinates

#change the ID's for setting coordinates of another device
device1 = 0x6734
device2 = 0x671f
device3 = 0x6e13
device4 = None

#Setup variables
coordinatesDevice1 = Coordinates()
coordinatesDevice2 = Coordinates()
coordinatesDevice3 = Coordinates()
coordinatesDevice4 = Coordinates()

#Check if connected
serial_port = get_first_pozyx_serial_port()
if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
    exit()

#Set the coordinates in the device
pozyx.setCoordinates(Coordinates(9929,100,2533), device1)
pozyx.setCoordinates(Coordinates(3875,100,2424), device2)
pozyx.setCoordinates(Coordinates(0,2212,2912), device3)
pozyx.setCoordinates(Coordinates(9878,4497,1981), device4)

#get the coordinates of the device
pozyx.getCoordinates(coordinatesDevice1, device1)
print(coordinatesDevice1)
pozyx.getCoordinates(coordinatesDevice2, device2)
print(coordinatesDevice2)
pozyx.getCoordinates(coordinatesDevice3, device3)
print(coordinatesDevice3)
pozyx.getCoordinates(coordinatesDevice4, device4)
print(coordinatesDevice4)

#Exception exit
except:
    print("")
    print("Exit program")
