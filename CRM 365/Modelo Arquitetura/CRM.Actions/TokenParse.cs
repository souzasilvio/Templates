using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Actions
{
    class TokenParse
    {
        private List<string> valoresToken;
        public TokenParse(string valor)
        {
            valoresToken = valor.Replace("{", "").Replace("}", "").Replace("\"", "").Split(',').ToList();
        }

        public string access_token
        {

            get
            {
                string[] result = valoresToken.Single(s => s.StartsWith("access_token")).Split(':');
                return result[1];
            }
        }

        public int expires_in
        {

            get
            {
                string[] result = valoresToken.Single(s => s.StartsWith("expires_in")).Split(':');
                return Convert.ToInt32(result[1]);
            }
        }

    }
}
