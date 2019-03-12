int LED = 13;
 
void setup()
{
  Serial.begin(9600);
  pinMode(LED, OUTPUT);
}
 
void loop()
{
  if (Serial.available())
  {
    int value = Serial.read();
    if (value == '1') digitalWrite(LED, HIGH);
    else if (value == '0') digitalWrite(LED, LOW);
    Serial.println(value);

    //String text = Serial.readString();
    //Serial.println(text);
  }
}
