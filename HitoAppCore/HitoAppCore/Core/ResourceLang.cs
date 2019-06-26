using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HitoAppCore.Core.Localization
{
    public class ResourceLang
    {
        IDictionary<CultureInfo, Dictionary<string, object>> resource;
        public ResourceLang()
        {
            resource = new Dictionary<CultureInfo, Dictionary<string, object>>();
        }
        public void Add(CultureInfo culture, string key, object val)
        {
            if (resource.ContainsKey(culture))
            {
                if (resource[culture].ContainsKey(key))
                {
                    resource[culture].SetItem(key, val);
                }
                else
                {
                    resource[culture].Add(key, val);
                }
            }
            else
            {
                resource.Add(culture, new Dictionary<string, object>() { { key, val } });

            }
        }

        internal object GetString(string text, CultureInfo ci)
        {
            throw new NotImplementedException();
        }
    }
}
