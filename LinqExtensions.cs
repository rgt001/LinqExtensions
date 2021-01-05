public static class LinqExtensions
{
  public static void MultipleGroupBy<T>(IEnumerable<T> source, params PropertyInfo[] properties)
  {
      try
      {
          Dictionary<string, List<T>> temp = new Dictionary<string, List<T>>();
          foreach (IGrouping<object, T> item in source.GroupBy(p => properties[0].GetValue(p)))
          {
              temp.Add(item.Key.ToString(), item.ToList());
          }
          
          Dictionary<string, List<T>> tempTwo = new Dictionary<string, List<T>>();
          for (int i = 1; i < properties.Length; i++)
          {
              foreach (KeyValuePair<string, List<T>> keyValue in temp)
              {
                  foreach (IGrouping<object, T> item in keyValue.Value.GroupBy(p => properties[i].GetValue(p)))
                  {
                      tempTwo.Add(item.Key.ToString() + "," + keyValue.Key, item.ToList());
                  }
              }
              temp.Clear();
              temp = new Dictionary<string, List<T>>(tempTwo);
          }
      }
      catch (Exception ex)
      {
          throw;
      }
  }
}
