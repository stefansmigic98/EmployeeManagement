using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoboAspNET1.Helpers
{
    public class ProtectionHelper
    {
        private IConfiguration _configuration;
        private IDataProtector _protector;

        private static readonly ProtectionHelper singleton;

        public ProtectionHelper()
        {

        }

        static ProtectionHelper()
        {
            singleton = new ProtectionHelper();
        }

        public static ProtectionHelper Singletion
        {
            get { return singleton; }
        }

        public void SetConfig(IConfiguration configuration)
        {
            singleton._configuration = configuration;
        }
        public void SetProvider(IDataProtectionProvider provider, string protectionKey)
        {
            _protector = provider.CreateProtector(protectionKey);
        }

        public string GetSectionValue(string key)
        {
            string[] keys = key.Split(":");
            string value = _configuration[key + ":Value"];

            bool isProtected = bool.Parse(_configuration[key + ":Protected"]);

            if (!isProtected)
            {
                string appSettingsPath = System.IO.Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                string json = System.IO.File.ReadAllText(appSettingsPath);

                dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

                jsonObject[keys[0]][keys[1]]["Value"] = _protector.Protect(value);
                jsonObject[keys[0]][keys[1]]["Protected"] = true;


                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(appSettingsPath, output);

                return value;
            }
            return _protector.Unprotect(value);
        }
    }
}
