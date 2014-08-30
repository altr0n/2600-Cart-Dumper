
#include "pins_arduino.h" 




int CE = 52; // Low to send high to app;y
int OE = 50;
int D1 = 2;
int D2 = 3;
int D3 = 4;
int D4 = 5;
int D5 = 6;
int D6 = 7;
int D7 = 9;
int D8 = 10;
int ROM_SIZE = 0x2000;
int Ad0 = 53;
int Ad1 = 51;
int Ad2 = 49;
int Ad3 = 47;
int Ad4 = 45;
int Ad5 = 43;
int Ad6 = 41;
int Ad7 = 39;
int Ad8 = 37;
int Ad9 = 35;
int Ad10 = 33;




int data = 0;
int addy = 0x000;


void setup() 
{                
  Serial.begin(9600);
  pinMode(OE,OUTPUT);
  pinMode(CE,OUTPUT);
  pinMode(Ad0,OUTPUT);
  pinMode(Ad1,OUTPUT);
  pinMode(Ad2,OUTPUT);
  pinMode(Ad3,OUTPUT);
  pinMode(Ad4,OUTPUT);
  pinMode(Ad5,OUTPUT);
  pinMode(Ad6,OUTPUT);
  pinMode(Ad7,OUTPUT);
  pinMode(Ad8,OUTPUT);
  pinMode(Ad9,OUTPUT);
  pinMode(Ad10,OUTPUT);
  
  
  pinMode(2,INPUT);
  pinMode(3,INPUT);
  pinMode(4,INPUT);
  pinMode(5,INPUT);
  pinMode(6,INPUT);
  pinMode(7,INPUT);
  pinMode(9,INPUT);
  pinMode(10,INPUT);


      
}
byte myBytes[4096];
void WriteEm()
{
 for(int t = 0;t<4096;t++)
  {
   Serial.println(myBytes[t]);
  } 
}
boolean start = false;
byte myByte = 0x00;

int foo = 5;
boolean readq = false;
void loop()
{
  if(!readq)
  {
   int testing = Serial.read();
    if(testing>0)
     {
        readq = true;
     } 
  }else
  {
      digitalWrite(CE,LOW);
      digitalWrite(OE,LOW);
      delay(foo);
      while(addy<4096)
      {
       FetchLocation();
       addy++; 
      }
      
    readq = false;
  }
}

void FetchLocation()
{ 
    DropItLow();
    digitalWrite(Ad0,(addy&0x01));
    digitalWrite(Ad1,((addy>>1)&0x01));
    digitalWrite(Ad2,((addy>>2)&0x01));
    digitalWrite(Ad3,((addy>>3)&0x01));
    digitalWrite(Ad4,((addy>>4)&0x01));
    digitalWrite(Ad5,((addy>>5)&0x01));
    digitalWrite(Ad6,((addy>>6)&0x01));
    digitalWrite(Ad7,((addy>>7)&0x01));
    digitalWrite(Ad8,((addy>>8)&0x01));
    digitalWrite(Ad9,((addy>>9)&0x01));
    digitalWrite(Ad10,((addy>>10)&0x01));
    digitalWrite(OE,((addy>>11)&0x01));
    digitalWrite(CE,LOW);
    delay(foo);  
    myByte = digitalRead(2)
      | digitalRead(3) << 1
      | digitalRead(4) << 2
      | digitalRead(5) << 3
      | digitalRead(6) << 4
      | digitalRead(7) << 5
      | digitalRead(9) << 6
      | digitalRead(10) << 7;
 
    Serial.print(addy, HEX);
      Serial.print(":");
      Serial.println(myByte, HEX);  
      DropItLikeItsHot();
     
}

void DropItLow()
{
    
    digitalWrite(Ad0,LOW);
    digitalWrite(Ad1,LOW);
    digitalWrite(Ad2,LOW);
    digitalWrite(Ad3,LOW);
    digitalWrite(Ad4,LOW);
    digitalWrite(Ad5,LOW);
    digitalWrite(Ad6,LOW);
    digitalWrite(Ad7,LOW);
    digitalWrite(Ad8,LOW);
    digitalWrite(Ad9,LOW);
    digitalWrite(Ad10,LOW);
    digitalWrite(OE,LOW);
    delay(2);
}

void DropItLikeItsHot()
{
    digitalWrite(CE,HIGH);
    delay(2);
}


