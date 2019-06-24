using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Util.WebControl
{
    /// <summary>
    /// 自动补全，实体
    /// </summary>
    public class AutocompleteEntity
    {
        public string data { get; set; }
        public string value { get; set; }
    }
    public class AutocompleteResult
    {
        public string query { get; set; }
        public List<AutocompleteEntity> suggestions { get; set; }
        public AutocompleteResult()
        {

            suggestions = new List<AutocompleteEntity>();
        }


    }
}
