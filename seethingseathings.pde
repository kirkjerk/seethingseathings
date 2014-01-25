ArrayList<Fish> fishes;
void setup(){
   size(800,800); 
   fishes = new ArrayList<Fish>();
  Fish frontFish = new Fish();
  fishes.add(frontFish); 
  for(int i = 0; i < 10; i++){
    Fish f = new Fish();
    int index = int(random(fishes.size()));
     f.setTarget(fishes.get(index)); 
     fishes.add(f); 
  }
   
}

void draw(){
  background(0,0,200);

  for(Fish f : fishes){
    f.move();
    f.draw();
  }
}

final int FISHSIZE = 10;
final int MAXSPEEDTOTRIGGERKICK = 1;
//final float MINDISTTOTRIGGERKICK = 50;
final float PERCENTCHANCETOKICK = 2;
final float KICKSTRENGTH = 10;

class Fish{
   float x=400, y=400;
   float xs,ys;
   float sz = FISHSIZE;
   
   float tx = 400,ty = 400;
   
   color c = color(random(128,255),random(64,200),random(0,64));
   
   Fish targetFish = null;
   
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
           if(x > tx) xs -= random(KICKSTRENGTH);
           else xs += random(KICKSTRENGTH);
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
       translate(x,y);
       float dir = 1;
       if(xs < 0) dir = -1;
     
      line(sz*dir,0,0,-sz);
      line(0,-sz,-sz*dir,+sz);
      line(0,sz,-sz*dir,-sz);
      line(0,sz,sz*dir,0);
      popMatrix();
      strokeWeight(1);
      if(targetFish != null) line(x,y,targetFish.x,targetFish.y);
      
   }
  

    
     
}
