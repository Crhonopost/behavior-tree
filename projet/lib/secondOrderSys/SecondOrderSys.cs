using Godot;
using System;

namespace SecondOrder;
public class SecondOrderSys
{
    private float k1, k2, k3;
    float previousValue;
    float nextValue, velocity;
    
    public SecondOrderSys(float f, float C, float r, float initPos){
        float pi = (float) Math.PI;
        k1 = (C / pi);
        k2 = (float) (1 / ((2 * pi * f) * (2 * pi * f)));
        k3 = (r * C) / (2 * pi * f);

        previousValue = initPos;
        nextValue = initPos;
        velocity = 0f;
    }

    public SecondOrderSys(){
        new SecondOrderSys(1,3,0, 0);
    }
    public float update(float delta, float x){
        float xd = (x - previousValue);
        previousValue = x;
        return this.update(delta, x, xd);
    }
    public float update(float delta, float x, float xd){
        nextValue = nextValue + delta * velocity;
        velocity = velocity + delta * (previousValue + k3*xd - nextValue - k1 * velocity) / k2;
        return nextValue;
    }
}
