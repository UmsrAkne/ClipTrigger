namespace ClipTrigger.Services.Audio;

/// <summary>
/// Simple audio player interface that supports play, stop, and volume control.
/// </summary>
public interface IPlayer
{
    /// <summary>
    /// Plays the specified audio file.
    /// </summary>
    /// <param name="filePath">Full path to an audio file.</param>
    void Play(string filePath);

    /// <summary>
    /// Stops playback and releases any resources.
    /// </summary>
    void Stop();

    /// <summary>
    /// Sets the output volume in the range [0.0, 1.0].
    /// </summary>
    /// <param name="volume">0.0 (mute) to 1.0 (max).</param>
    void SetVolume(float volume);
}