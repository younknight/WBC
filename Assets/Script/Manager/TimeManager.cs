
using System;
public class TimeManager
{

    public float CalculaterDate(DateTime date)
    {
        TimeSpan timeSpan;
        timeSpan = DateTime.Now - date;
        double totalTime = timeSpan.TotalSeconds; ;

        return ((float)totalTime);
    }
}
