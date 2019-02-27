void setup() {
  Serial.begin(9600);
  //Pin A1,A2,A3,A4 als output
  DDRC |= (1 << PORTC1) | (1 << PORTC2) | (1 << PORTC3) | (1 << PORTC4);
}

void loop() {
  DriveForward(5000);         // 5 seconden naar voor rijden
  Turn90DegreesLeft(2103);     // 2103/90 = 23,367 ms per °
  Turn90DegreesRight(2103);   
  DriveBackwards(5000);         // 5 seconden naar achter rijden
}
void Turn90DegreesRight(int timeDriving) {
  Serial.println("Turning Left");
  PORTC &= 0x00;
  PORTC |= (1 << PORTC2) | (1 << PORTC4);
  delay(timeDriving);
}
void Turn90DegreesLeft(int timeDriving) {
  Serial.println("Turning Right");
  PORTC &= 0x00;
  PORTC |= (1 << PORTC1) | (1 << PORTC3);
  delay(timeDriving);
}
void DriveForward(int timeDriving) {
  Serial.println("Driving forward");
  PORTC &= 0x00;
  PORTC |= (1 << PORTC1) | (1 << PORTC4);
  delay(timeDriving);
}
void DriveBackwards(int timeDriving) {
  Serial.println("Driving backwards");
  PORTC &= 0x00;
  PORTC |= (1 << PORTC2) | (1 << PORTC3);
  delay(timeDriving);
}
