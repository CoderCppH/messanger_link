using ClientForPostGrammMessanger.lib.Config.Patterns;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientForPostGrammMessanger.lib.Config
{
    public class Config
    {
        private string _path_to_config = "config.json";
        p_config _p_config = default;
        public Config() 
        {
            if (File.Exists(_path_to_config))
            {
                byte[] config_out_bytes = File.ReadAllBytes(_path_to_config);
                if (config_out_bytes.Length > 1) 
                {
                    _p_config = Convert.ConvertJson.ConvertJson.buffer_in_string_json<p_config>(config_out_bytes);
                    if (_p_config == default || _p_config == null)
                        throw new Exception("config file equels null");
                }
            }
            else
            {
                _p_config = new p_config();
                _p_config.port = 0;
                _p_config.host = "localhost";
                byte[] config_in_bytes = Convert.ConvertJson.ConvertJson.Object_in_json_in_buffer<p_config>(_p_config);
                var file = File.Create(_path_to_config);
                file.Write(config_in_bytes);
                file.Close();
            }
        }
        public p_config GetConfig() 
        {
            return _p_config;
        }
    }
}
