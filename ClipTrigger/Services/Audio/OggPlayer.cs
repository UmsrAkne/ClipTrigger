using System;
using NAudio.Vorbis;
using NAudio.Wave;

namespace ClipTrigger.Services.Audio;

/// <summary>
/// OGG player implementation backed by NAudio and NAudio.Vorbis.
/// </summary>
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class OggPlayer : IPlayer, IDisposable
{
    private WaveOutEvent outputDevice;
    private WaveStream waveStream; // VorbisWaveReader derives from WaveStream
    private WaveChannel32 volumeChannel; // for volume control
    private float volume = 1.0f;

    public void Play(string filePath)
    {
        Stop();

        if (!filePath.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("OggPlayer can only play .ogg files.", nameof(filePath));
        }

        // Initialize reader and output
        waveStream = new VorbisWaveReader(filePath);
        volumeChannel = new WaveChannel32(waveStream) { Volume = volume, };
        outputDevice = new WaveOutEvent();
        outputDevice.Init(volumeChannel);
        outputDevice.Play();
    }

    public void Stop()
    {
        try
        {
            outputDevice?.Stop();
        }
        catch
        {
            // ignore stop exceptions
        }
        finally
        {
            outputDevice?.Dispose();
            outputDevice = null;

            volumeChannel?.Dispose();
            volumeChannel = null;

            waveStream?.Dispose();
            waveStream = null;
        }
    }

    public void SetVolume(float newVolume)
    {
        // Clamp to [0,1]
        volume = Math.Clamp(newVolume, 0.0f, 1.0f);
        if (volumeChannel != null)
        {
            volumeChannel.Volume = volume;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        Stop();
    }
}