using Godot;
using System;

namespace SecondOrder;
public class SecondOrderSysVec2
{
    private float k1, k2, k3;
    Vector2 previousPos;
    Vector2 nextPos, velocity;
    
    public SecondOrderSysVec2(float f, float C, float r, Vector2 initPos){
        float pi = (float) Math.PI;
        k1 = (C / pi);
        k2 = (float) (1 / ((2 * pi * f) * (2 * pi * f)));
        k3 = (r * C) / (2 * pi * f);

        previousPos = initPos;
        nextPos = initPos;
        velocity = Vector2.Zero;
    }

    public SecondOrderSysVec2(){
        new SecondOrderSysVec2(1,3,0, Vector2.Zero);
    }
    public Vector2 update(float delta, Vector2 x){
        Vector2 xd = (x - previousPos);
        previousPos = x;
        return this.update(delta, x, xd);
    }
    public Vector2 update(float delta, Vector2 x, Vector2 xd){
        nextPos = nextPos + delta * velocity;
        velocity = velocity + delta * (previousPos + k3*xd - nextPos - k1 * velocity) / k2;
        return nextPos;
    }
}