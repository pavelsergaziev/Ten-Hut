using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerVolumeChanger
{
    private AudioMixer _mixer;
    private float _tempVolumeVariable, _maxVolume;

    //Для более равномерного изменения громкости используем перевод в логарифмическую шкалу.
    // см. https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
    //Оттуда
    //формула Mathf.Log10(linearValue) * 20
    //Соответственно, перевод обратно для отображения и хранения будет производиться по
    //формуле Mathf.Pow(10, (logarithmicValue / 20))
    private const float LogZeroVolume = 0.0001f;//результат 0.0001f при переводе в нашу логарифм. шкалу равен -80, а это в юнити нулевая громкость.
    private const int LogBase = 10;
    private const int LogMultiplier = 20;

    public AudioMixerVolumeChanger(AudioMixer mixer, float maxVolume)
    {
        _mixer = mixer;
        _maxVolume = maxVolume;
    }

    /// <summary>
    /// Установить общую громкость микшера в нужное значение. Значение принимается линейное, в микшере громкость ставится логарифмическая.
    /// </summary>
    /// <param name="volume"></param>
    public void SetMainMixerVolume(float volume)
    {
        _mixer.SetFloat("MasterVolume", GetLogarithmicVolumeFromLinear(volume));
    }

    public float GetAudioMixerGroupVolume(AudioMixerGroup mixerGroup)
    {
        GetVolume(mixerGroup, out _tempVolumeVariable);
        return GetLinearVolumeFromLogarithmic(_tempVolumeVariable);
    }

    /// <summary>
    /// Меняет громкость, сдвигая ее на целевое значение в пределах от нуля до максимума.
    /// Возвращает получившееся значение громкости.
    /// Громкость в микшере меняется логарифмически, но метод принимает и возвращает линейные числа (для вывода и хранения).
    /// </summary>
    /// <param name="mixerGroup"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public float ChangeVolume(AudioMixerGroup audioMixerGroup, float amount)
    {
        GetVolume(audioMixerGroup, out _tempVolumeVariable);

        _tempVolumeVariable = GetLinearVolumeFromLogarithmic(_tempVolumeVariable);

        _tempVolumeVariable += amount;

        if (_tempVolumeVariable > _maxVolume)
        {
            _tempVolumeVariable = _maxVolume;
        }
        else if (_tempVolumeVariable < LogZeroVolume)
        {
            _tempVolumeVariable = LogZeroVolume;
        }

        SetVolume(audioMixerGroup, _tempVolumeVariable);

        return _tempVolumeVariable;
    }

    private float GetLogarithmicVolumeFromLinear(float value)
    {
        return Mathf.Log10(value) * LogMultiplier;
    }

    private float GetLinearVolumeFromLogarithmic(float value)
    {
        return Mathf.Pow(LogBase, (_tempVolumeVariable / LogMultiplier));
    }

    private void GetVolume(AudioMixerGroup audioMixerGroup, out float volume)
    {
        _mixer.GetFloat(string.Concat(audioMixerGroup.name,"Volume"),out volume);
    }

    /// <summary>
    /// Установить громкость в нужное значение. Значение принимается линейное, громкость ставится логарифмическая.
    /// </summary>
    /// <param name="audioMixerGroup"></param>
    /// <param name="volume"></param>
    public void SetVolume(AudioMixerGroup audioMixerGroup, float volume)
    {
        _mixer.SetFloat(string.Concat(audioMixerGroup.name,"Volume"),GetLogarithmicVolumeFromLinear(volume));
    }
}
