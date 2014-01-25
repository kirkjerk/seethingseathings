ArrayList<Fish> fishes;
void setup(){
  frameRate(60);
   size(800,800,P3D); 
   ortho();
   fishes = new ArrayList<Fish>();
  Fish frontFish = new Fish();
  fishes.add(frontFish); 
  for(int i = 0; i < 2500; i++){
    Fish f = new Fish();
    int index = int(random(fishes.size()));
     f.setTarget(fishes.get(index)); 
     fishes.add(f); 
  }
   
}
float bigangle;
void draw(){
  bigangle += .01;
  background(0,0,200);

  pushMatrix();
  translate(400,400);
  translate(0,0,-1200);
  rotateY(bigangle);
  translate(-400,-400);
  strokeWeight(1); stroke(0); noFill();
  rect(0,0,800,800);

  for(Fish f : fishes){
    f.move();
    f.draw();
  }
  
  fill(255);
  text(frameRate,740,780);
  popMatrix();
}

final int FISHSIZE = 10;
final int MAXSPEEDTOTRIGGERKICK = 1;
//final float MINDISTTOTRIGGERKICK = 50;
final float PERCENTCHANCETOKICK = 10;
final float KICKSTRENGTH = 10;

class Fish{
   float x=random(800), y=random(800);
   float xs,ys;
   float sz = FISHSIZE;
   
   float tx = 400,ty = 400;
   
   color c = color(random(128,255),random(64,200),random(0,64));
   
   Fish targetFish = null;
   
   float depth = random(-200,200);
   
   void setTarget(Fish t){
      targetFish = t; 
   }
   void setTargetXY(){
      if(targetFish == null){
         tx = mouseX;
         ty = mouseY;
      }   else {
         tx = targetFish.x;
         ty = targetFish.y;
        
      }
   }
   
   void checkKick(){
     if(abs(xs) > MAXSPEEDTOTRIGGERKICK) return;
     //if(dist(x,y,tx,ty) > MINDISTTOTRIGGERKICK){
        if(random(100) < PERCENTCHANCETOKICK){
           if(x > tx) xs -= random(dist(x,y,tx,ty))/25;
           else xs += random(dist(x,y,tx,ty))/25;
        } 
    // }
   }
   
   void move(){
     setTargetXY();
     checkKick();
      xs *= .95;
     x += xs; 
     if(y < ty) y+= abs(xs/10);
     if(y > ty) y-= abs(xs/10);
   }
   
   void draw(){
     strokeWeight(3);
       //stroke(255,128,0);
      stroke(c);
       pushMatrix();
       translate(x,y,depth);
       float dir = 1;
       if(xs < 0) dir = -1;
     
      line(sz*dir,0,0,-sz);
      line(0,-sz,-sz*dir,+sz);
      line(0,sz,-sz*dir,-sz);
      line(0,sz,sz*dir,0);
      popMatrix();
      strokeWeight(1);
     // if(targetFish != null) line(x,y,targetFish.x,targetFish.y);
      
   }
  

    
     
}
