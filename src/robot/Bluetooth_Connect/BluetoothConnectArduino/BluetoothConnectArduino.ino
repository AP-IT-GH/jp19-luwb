void setup()
{
  Serial.begin(9600);
  //Pin A1,A2,A3,A4 as output
  DDRC |= (1 << PORTC1) | (1 << PORTC2) | (1 << PORTC3) | (1 << PORTC4);
}
 
void loop()
{
  if (Serial.available())
  {
    //ASCII f=102 / b=98 / l=108 / r=114 / s=115
    int value = Serial.read();
    //Serial.println(value);
    switch(value){
      case 102:
        DriveForward();
        break;
      case 98:
        DriveBackwards();
        break;
      case 108:
        TurnLeft();
        break;
      case 114:
        TurnRight();
        break;
      case 115:
        PORTC &= 0x00;
        Serial.println("Stop Moving");
        break;
    }

    //String text = Serial.readString();
    //Serial.println(text);
  }
}

void TurnRight() {
  Serial.println("Turning Right");
  PORTC &= 0x00;
  PORTC |= (1 << PORTC2) | (1 << PORTC4);
}
void TurnLeft() {
  Serial.println("Turning Left");
  PORTC &= 0x00;
  PORTC |= (1 << PORTC1) | (1 << PORTC3);
}
void DriveForward() {
  Serial.println("Driving forward");
  PORTC &= 0x00;
  PORTC |= (1 << PORTC1) | (1 << PORTC4);
}
void DriveBackwards() {
  Serial.println("Driving backwards");
  PORTC &= 0x00;
  PORTC |= (1 << PORTC2) | (1 << PORTC3);
}
