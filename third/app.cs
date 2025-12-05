double total = 0;


(int maxValue, int maxIndex) GetMaxValueAndIndex(int remainingNumberOfBatteries, Array digits, int lastMaxIndex){ 
  var digitsTruncated = digits.Skip(maxIndexFirst + 1).Take(digits.Count() - 10).ToArray();
  int maxValueSecond = digitsTruncated.Max();
  int maxIndexSecond = digitsTruncated.ToList().IndexOf(maxValueSecond);
  return (maxValue, maxIndex);
}

foreach (var line in File.ReadLines("data.txt"))
{
  int l = line.Length;
  int[] digits = new int[l];
  for(int i = 0; i < l; i++){
    digits[i] = line[i] - '0';
  }

  var digitsMinusLast = digits.Take(digits.Count() - 11).ToArray(); 
  int maxValueFirst = digitsMinusLast.Max(); 
  int maxIndexFirst = digitsMinusLast.ToList().IndexOf(maxValueFirst);
  var digitsTruncated = digits.Skip(maxIndexFirst + 1).Take(digits.Count() - 10).ToArray();
  int maxValueSecond = digitsTruncated.Max();
  int maxIndexSecond = digitsTruncated.ToList().IndexOf(maxValueSecond);

  for(var n = 11; n >= 0; n--){
    (int maxValue, int maxIndex) = GetMaxValueAndIndex();
    total += maxValueFirst * Math.Pow(10, n);
  }
  Console.WriteLine(total);
  break;
}
Console.WriteLine(total);
