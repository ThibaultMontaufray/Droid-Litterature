namespace Droid_litterature
{
    public class GroupVerbal : SentenseGroup
    {
        #region Attribute
        #endregion

        #region Properties
        public new string Text
        {
            get
            {
                string retVal = string.Empty;
                foreach (Word word in Words)
                {
                    if (!string.IsNullOrEmpty(retVal)) { retVal += " "; }
                    retVal += word.Text;
                }
                return retVal;
            }
        }
        #endregion

        #region Constructor
        public GroupVerbal()
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}