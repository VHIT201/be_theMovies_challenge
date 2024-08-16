using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Common.Application.Settings
{
    public class ExternalServicesSetting
    {
        public List<HttpServiceSetting> HttpServiceSettings { get; set; } = [];
    }

    public class HttpServiceSetting
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Accept { get; set; } = "application/json";
        public string ContentType { get; set; } = "application/json";
        public string Authorization { get; set;} = string.Empty;
        public bool UseAuthenToken { get; set; } = false;
    }
}
