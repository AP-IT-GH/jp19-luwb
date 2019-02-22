Pin1 = A1;
Pin2 = A2;
Pin3 = A3;
Pin4 = A4;

void setup() {
  // put your setup code here, to run once:
  pinMode(Pin1,OUTPUT);
  pinMode(Pin2,OUTPUT);
  pinMode(Pin3,OUTPUT);
  pinMode(Pin4,OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(Pin1,HIGH);
  digitalWrite(Pin2,LOW);
  digitalWrite(Pin3,LOW);
  digitalWrite(Pin4,HIGH);
  delay(5000);
  digitalWrite(Pin1,LOW);
  digitalWrite(Pin2,HIGH);
  digitalWrite(Pin3,HIGH);
  digitalWrite(Pin4,LOW);
  delay(5000);
}
