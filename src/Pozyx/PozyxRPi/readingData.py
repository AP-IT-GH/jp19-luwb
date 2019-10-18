from pypozyx import PozyxSerial, get_first_pozyx_serial_port, POZYX_SUCCESS, SingleRegister, EulerAngles, Acceleration
# initalize the Pozyx as above
serial_port = get_first_pozyx_serial_port()

if serial_port is not None:
    pozyx = PozyxSerial(serial_port)
    print("Connection success!")
else:
    print("No Pozyx port was found")
# initialize the data container
who_am_i = SingleRegister()
# get the data, passing along the container
status = pozyx.getWhoAmI(who_am_i)

# check the status to see if the read was successful. Handling failure is covered later.
if status == POZYX_SUCCESS:
    # print the container. Note how a SingleRegister will print as a hex string by default.
    print(who_am_i) # will print '0x43'

# and repeat
# initialize the data container
acceleration = Acceleration()
# get the data, passing along the container
pozyx.getAcceleration_mg(acceleration)

# initialize the data container
euler_angles = EulerAngles()
# get the data, passing along the container
pozyx.getEulerAngles_deg(euler_angles)
