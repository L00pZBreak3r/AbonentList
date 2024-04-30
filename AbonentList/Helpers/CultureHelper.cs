using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace AbonentList.Helpers
{
    internal static class CultureHelper
    {
        public static readonly CultureInfo CurrentCultureDefault = CultureInfo.CurrentCulture;

        static CultureHelper()
        {
            Thread.CurrentThread.CurrentCulture = CurrentCultureDefault;
            Thread.CurrentThread.CurrentUICulture = CurrentCultureDefault;
            CultureInfo.DefaultThreadCurrentCulture = CurrentCultureDefault;
            CultureInfo.DefaultThreadCurrentUICulture = CurrentCultureDefault;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(                 
                XmlLanguage.GetLanguage(CurrentCultureDefault.IetfLanguageTag)));
        }
    }
}
