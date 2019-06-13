using System.Globalization;

namespace HitoAppCore.Core.Localization
{
    internal interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
}