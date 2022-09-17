using ADO_Lesson_1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Lesson_1.MVVM.Models
{
    public class Options
    {

        #region Members

        public string ConnectionString { get; set; } = String.Empty;

        public bool RememberOptions { get; set; } = false;

        #endregion

        #region Methods

        public void ReadData(Options options)
        {
            if (options == null) options = new();
            this.ConnectionString = options.ConnectionString;
            this.RememberOptions = options.RememberOptions;
        }

        public void Save()
        {
            JSONService.Write("options.json", this);
        }

        public void Load()
        {

            Options loaded = this;

            try
            {
                loaded = JSONService.Read<Options>("options.json");
                ReadData(loaded);
            }
            catch (Exception) { }
        }


        #endregion

        public override string ToString() => ConnectionString.ToString();
    }
}
