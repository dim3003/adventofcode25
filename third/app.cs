int total = 0;
foreach (var line in File.ReadLines("data.txt"))
{
  int l = line.Length;
  int[] digits = new int[l];
  for(int i = 0; i < l; i++){
    digits[i] = line[i] - '0';
  }

  var digitsMinusLast = digits.Take(digits.Count() - 1).ToArray(); 
  int maxValueFirst = digitsMinusLast.Max(); 
  int maxIndexFirst = digitsMinusLast.ToList().IndexOf(maxValueFirst);
  var digitsTruncated = digits.Skip(maxIndexFirst + 1).Take(digits.Count()).ToArray();
  int maxValueSecond = digitsTruncated.Max();
  total += maxValueFirst * 10 + maxValueSecond;
}
Console.WriteLine(total);
