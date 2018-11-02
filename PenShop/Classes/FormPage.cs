using System.Collections.Generic;

namespace PenShop {

    public class FormPage {

        #region Variables

        public List<Field> Fields;
        public int PageNumber;

        #endregion Variables

        public FormPage(int pagenum) {
            Fields = new List<Field>();
            PageNumber = pagenum;
        }
    }
}