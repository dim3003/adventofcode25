var grid = File.ReadAllLines("data.txt");
int rows = grid.Length;
int cols = grid[0].Length;

int total = 0;

// 8 directions
int[] dr = { -1, -1, -1,  0, 0,  1, 1, 1 };
int[] dc = { -1,  0,  1, -1, 1, -1, 0, 1 };

for (int r = 0; r < rows; r++)
{
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

        if (adjacent < 4)
            total++;
    }
}

Console.WriteLine(total);
