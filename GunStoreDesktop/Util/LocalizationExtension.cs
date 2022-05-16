using System.Windows.Data;

namespace GunStoreDesktop.Util;

public class LocalizationExtension : Binding
{
    public LocalizationExtension(string name) : base("[" + name + "]")
    {
        Mode = BindingMode.OneWay;
        Source = TranslationSource.Instance;
    }
}