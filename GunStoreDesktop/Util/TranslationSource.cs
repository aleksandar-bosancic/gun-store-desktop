using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace GunStoreDesktop.Util;

public class TranslationSource : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public static TranslationSource Instance { get; } = new();

    private readonly ResourceManager _resourceManager = Resources.Localization.Strings.ResourceManager;
    private CultureInfo? _cultureInfo;

    public string? this[string key] => _resourceManager.GetString(key, _cultureInfo);

    public CultureInfo? CultureInfo
    {
        get => _cultureInfo;
        set
        {
            if (Equals(_cultureInfo, value)) return;
            _cultureInfo = value;
            PropertyChangedEventHandler? @event = PropertyChanged;
            @event?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}