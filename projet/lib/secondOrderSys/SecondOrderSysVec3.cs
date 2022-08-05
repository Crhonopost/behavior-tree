using Godot;
using System;

namespace SecondOrder;
public class SecondOrderSysVec3
{
    private float k1, k2, k3;
    Vector3 previousPos;
    Vector3 nextPos, velocity;
    
    public SecondOrderSysVec3(float f, float C, float r, Vector3 initPos){
        float pi = (float) Math.PI;
        k1 = (C / pi);
        k2 = (float) (1 / ((2 * pi * f) * (2 * pi * f)));
        k3 = (r * C) / (2 * pi * f);

        previousPos = initPos;
        nextPos = initPos;
        velocity = Vector3.Zero;
    }

    public SecondOrderSysVec3(){
        new SecondOrderSysVec3(1,3,0, Vector3.Zero);
    }
    public Vector3 update(float delta, Vector3 x){
        Vector3 xd = (x - previousPos);
        previousPos = x;
        return this.update(delta, x, xd);
    }
    public Vector3 update(float delta, Vector3 x, Vector3 xd){
        nextPos = nextPos + delta * velocity;
        velocity = velocity + delta * (previousPos + k3*xd - nextPos - k1 * velocity) / k2;
        return nextPos;
    }
}