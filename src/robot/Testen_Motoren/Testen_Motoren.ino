int In1 = A1; // Stuurt moter A aan
int In2 = A2;
int In3 = A3;
int In4 = A4;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(In1, OUTPUT);
  pinMode(In2, OUTPUT);
  pinMode(In3, OUTPUT);
  pinMode(In4, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  DriveForward(5000);         // 5 seconden naar voor rijden
  Turn90DegreesLeft(2103);     // 2103/90 = 23,367 ms per °
  Turn90DegreesRight(2103);   
  DriveBackwards(5000);         // 5 seconden naar achter rijden
}
void Turn90DegreesRight(int timeDriving) {
  digitalWrite(In1, LOW);
  digitalWrite(In2, HIGH);
  digitalWrite(In3, LOW);
  digitalWrite(In4, HIGH);
  Serial.println("Turning Left");
  delay(timeDriving);
}
void Turn90DegreesLeft(int timeDriving) {
 digitalWrite(In1, HIGH);
  digitalWrite(In2, LOW);
  digitalWrite(In3, HIGH);
  digitalWrite(In4, LOW);
  Serial.println("Turning Right");
  delay(timeDriving);
}
void DriveForward(int timeDriving) {
  digitalWrite(In1, HIGH);
  digitalWrite(In2, LOW);
  digitalWrite(In3, LOW);
  digitalWrite(In4, HIGH);
  Serial.println("Driving forward");
  delay(timeDriving);
}
void DriveBackwards(int timeDriving) {
  digitalWrite(In1, LOW);
  digitalWrite(In2, HIGH);
  digitalWrite(In3, HIGH);
  digitalWrite(In4, LOW);
  Serial.println("Driving backwards");
  delay(timeDriving);
}
