using System;
using UnityEngine;

public interface IRecordable
{
    public void Play();
    public void Record();
    public void Stop();
    public void Reset();
}
