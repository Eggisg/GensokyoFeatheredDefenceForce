using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColour 
{

    public TimerScript timerScript = new TimerScript(0);
    public bool curved = true;
    public Color color1, color2;
    public SpriteRenderer spriteRenderer;
    public AnimationCurve curve;
    public bool active;
    public float delay;
    public bool Loop;
    public bool done;


    public void Update()
    {
        timerScript.Update();
        if (active)
        {
            if (curved)
            {
                spriteRenderer.color = Color.Lerp
                    (
                        color1,
                        color2,
                        curve.Evaluate(timerScript.Progress())
                    );
            }

            if (timerScript.Check())
            {
                done = true;
                spriteRenderer.color = color2;
                if (Loop)
                {
                    SwitchPoints();
                    timerScript.Restart();
                }

            }
        }
    }

    public void LerpColorEndless(Color color1, Color color2, SpriteRenderer spriteRenderer, AnimationCurve curve, float delay)
    {
        active = true;
        this.color1 = color1;
        this.color2 = color2;
        this.spriteRenderer = spriteRenderer;
        this.curve = curve;
        this.delay = delay;
        Loop = true;
        this.spriteRenderer.color = color1;
        timerScript.Start(delay);
    }

    public void LerpColor(Color color, SpriteRenderer spriteRenderer, AnimationCurve curve, float delay)
    {
        if (done)
        {
            active = true;
            this.spriteRenderer = spriteRenderer;
            this.color1 = spriteRenderer.color;
            this.color2 = color;
            this.curve = curve;
            this.delay = delay;
            Loop = false;
            timerScript.Start(delay);
            done = false;
        }

    }

    void SwitchPoints()
    {
        Color temp = this.color1;
        this.color1 = this.color2;
        this.color2 = temp;
    }

}
