// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Themes
{
    using DevExpress.XamarinForms.Core.Themes;
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    
    internal class ThemeLoader : IThemeChangingHandler
    {
        private static ThemeLoader instance;
        
        private ThemeLoader()
        {
            ThemeManager.AddThemeChangedHandler(this);
        }
        
        void IThemeChangingHandler.OnThemeChanged()
        {
            this.LoadTheme();
        }
        
        public void LoadTheme()
        {
            try
            {
                ResourceDictionary dictionary = Activator.CreateInstance(Type.GetType("DevExpress.XamarinForms.DataGrid.Themes." + ThemeManager.ThemeName + "Theme")) as ResourceDictionary;
                if (dictionary != null)
                {
                    Application application1 = Application.Current;
                    if (application1 == null)
                    {
                        Application local1 = application1;
                    }
                    else
                    {
                        ResourceDictionary dictionary1 = application1.Resources;
                        if (dictionary1 == null)
                        {
                            ResourceDictionary local2 = dictionary1;
                        }
                        else
                        {
                            ICollection<ResourceDictionary> collection1 = dictionary1.MergedDictionaries;
                            if (collection1 == null)
                            {
                                ICollection<ResourceDictionary> local3 = collection1;
                            }
                            else
                            {
                                collection1.Add(dictionary);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        
        public static ThemeLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ThemeLoader();
                }
                return instance;
            }
        }
    }
}
