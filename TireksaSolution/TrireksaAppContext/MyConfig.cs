using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrireksaAppContext
{
    public class MyConfig
    {
      


        internal string GetUserName()
        {
            return GetStringValue("UserName");
        }


        internal async void UpdateUserName(string value)
        {
            await Task.Delay(2000);
            UpdateKey("UserName", value);
        }



        private Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private static string address;




        //
        private int GetIntValue(string KeyName)
        {
            int vResult = 0;

            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (int)asr.GetValue(KeyName, typeof(int));
            }
            return vResult;
        }

        public string GetStringValue(string KeyName)
        {
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }

        public void UpdateKey(string strKey, string newValue)
        {
            if (!KeyExists(strKey))
            {
                // Add an Application Setting.
                config.AppSettings.Settings.Add(strKey, newValue);
                config.Save(ConfigurationSaveMode.Modified, true);
                // Save the configuration file.
                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                // Add an Application Setting.

                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                foreach (XmlElement element in xmlDoc.DocumentElement)
                {
                    if (element.Name.Equals("appSettings"))
                    {
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            if (node.Attributes[0].Value.Equals(strKey))
                            {
                                node.Attributes[1].Value = newValue;
                            }
                        }
                    }
                }

                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public bool KeyExists(string strKey)
        {
            bool IsExists = false;
            foreach (string item in config.AppSettings.Settings.AllKeys)
            {
                if (item == strKey)
                {
                    IsExists = true;
                }
            }
            return IsExists;
        }


    }
}