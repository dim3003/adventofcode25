var grid = File.ReadAllLines("data.txt").Select(l => l.ToCharArray()).ToArray();
int rows = grid.Length;
int cols = grid[0].Length;


// 8 directions
int[] dr = { -1, -1, -1,  0, 0,  1, 1, 1 };
int[] dc = { -1,  0,  1, -1, 1, -1, 0, 1 };

int FindRolls(){
  int subtotal = 0;
  var next = grid.Select(row => row.ToArray()).ToArray();
  
  for (int r = 0; r < rows; r++)
  for (int c = 0; c < cols; c++)
  {
      if (grid[r][c] != '@') continue;

      int adjacent = 0;
      for (int k = 0; k < 8; k++)
      {
          int nr = r + dr[k];
          int nc = c + dc[k];

          if (nr < 0 || nr >= rows || nc < 0 || nc >= cols)
              continue;

          if (grid[nr][nc] == '@')
              adjacent++;
      }

      if (adjacent < 4){
          next[r][c] = 'X';
          subtotal++;
      }
  }

  grid = next;
  return subtotal;
}

int total = 0;
int added;
do
{
  added = FindRolls();
  total += added;
} while (added > 0);

Console.WriteLine(total);
