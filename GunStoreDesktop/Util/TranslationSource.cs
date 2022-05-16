using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using GunStoreDesktop.Properties;

namespace GunStoreDesktop.Util;

public class TranslationSource : INotifyPropertyChanged
{
    public static TranslationSource Instance { get; } = new();

    private readonly ResourceManager _resourceManager = Resources.Localization.Strings.ResourceManager;
    private CultureInfo _cultureInfo = null;

    public string? this[string key]
    {
        get { return this._resourceManager.GetString(key, this._cultureInfo); }
    }

    public CultureInfo CultureInfo
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
    
    public event PropertyChangedEventHandler? PropertyChanged;
}