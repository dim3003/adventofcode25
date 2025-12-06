double total = 0;

foreach (var line in File.ReadLines("data.txt"))
{
  int l = line.Length;
  int[] digits = new int[l];
  for(int i = 0; i < l; i++){
    digits[i] = line[i] - '0';
  }

  int maxDigitIndex = -1; // First loop should not skip anything
  for(var n = 11; n >= 0; n--){
    digits = digits.Skip(maxDigitIndex + 1).ToArray(); 
    int maxDigit = digits.Take(digits.Count() - n).Max(); 
    maxDigitIndex = digits.ToList().IndexOf(maxDigit);
    total += maxDigit * Math.Pow(10, n);
  }
}
Console.WriteLine(total);
