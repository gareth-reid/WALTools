
namespace WALTools.Extension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Parse any object and return double or default vale
        /// </summary>
        /// <param name="doubleValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this object doubleValue, double defaultValue)
        {
            try
            {
                double dValue;
                if (double.TryParse(doubleValue.ToString(), out dValue))
                {
                    return dValue;
                }
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
