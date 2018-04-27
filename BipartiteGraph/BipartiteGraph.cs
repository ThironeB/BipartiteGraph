using System;

namespace BipartiteGraph
{
    class BipartiteGraph
    {
        static bool posicionLamina(string LaminaDetalle, string PosicionDetalle)
        {
            string[] Lamina;
            string[] Posicion;
            bool Presente = false;

            Lamina = LaminaDetalle.Split(' ');
            Posicion = PosicionDetalle.Split(' ');

            if (int.Parse(Lamina[1]) < int.Parse(Posicion[0]) && int.Parse(Posicion[0]) < int.Parse(Lamina[3]) && int.Parse(Lamina[2]) < int.Parse(Posicion[1]) && int.Parse(Posicion[1]) < int.Parse(Lamina[4]))
            {
                Presente = true;
            }

            return Presente;
        }

        static void resetBipartite(int row, int col, int tam, int[,] bip)
        {
            for (int fila = 0; fila < tam; fila++)
            {
                if (fila != row)
                {
                    bip[fila, col] = 0;
                }
            }
        }

        static bool visitedRow(int row,bool[] visited)
        {
            bool vis = false;

            if (visited[row] == true)
            {
                vis = true;
            }

            return vis;
        }

        static bool isMBipartite(int[,] Bip, int Tam)
        {
            bool Unico = false;
            for (int row = 0; row < Tam; row++)
            {
                Unico = false;
                for (int col = 0; col < Tam; col++)
                {
                    if (Bip[row, col] == 1 && Unico == false)
                    {
                        Unico = true;
                    }
                    else if (Bip[row, col] == 1 && Unico == true)
                    {
                        Unico = false;
                        return false;
                    }
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            int TamanoArrays = int.Parse(Console.ReadLine());
            string[] Laminas = new string[TamanoArrays];
            string[] Posicion = new string[TamanoArrays];

            int[,] Bipartite = new int[TamanoArrays, TamanoArrays];
            int iterator;

            for (iterator = 0; iterator < TamanoArrays; iterator += 1)
            {
                Laminas[iterator] = Console.ReadLine();
            }

            for (iterator = 0; iterator < TamanoArrays; iterator += 1)
            {
                Posicion[iterator] = Console.ReadLine();
            }

            for (int row = 0; row < TamanoArrays; row += 1)
            {
                for (int col = 0; col < TamanoArrays; col += 1)
                {
                    if (posicionLamina(Laminas[col], Posicion[row]))
                    {
                        Bipartite[row, col] = 1;
                    }
                    else
                    {
                        Bipartite[row, col] = 0;
                    }
                }
            }

            bool Unico = false;
            int pos = 0;
            bool[] visited = new bool[TamanoArrays];

            for (int i = 0; i < TamanoArrays; i++)
            {
                visited[i] = false;
            }

            for (int matriz = 0; matriz < TamanoArrays; matriz++) { 
                for (int row = 0; row < TamanoArrays; row++)
                {
                    if (!visitedRow(row, visited)) {
                        Unico = false;
                        for (int col = 0; col < TamanoArrays; col++)
                        {
                            if (Bipartite[row, col] == 1 && Unico == false)
                            {
                                pos = col;
                                Unico = true;
                            }
                            else if(Bipartite[row, col] == 1 && Unico == true)
                            {
                                Unico = false;
                            }
                        }

                        if (Unico)
                        {
                            resetBipartite(row, pos, TamanoArrays, Bipartite);
                            visited[row] = true;
                        }
                    }
                }
            }

            if (isMBipartite(Bipartite, TamanoArrays))
            {
                string[] Orden = new string[TamanoArrays];

                string[] Lamina;
                string[] Pos;

                for (int row = 0; row < TamanoArrays; row++)
                {
                    for (int col = 0; col < TamanoArrays; col++)
                    {
                        if (Bipartite[row, col] == 1)
                        {
                            Lamina = Laminas[col].Split(' ');
                            Pos = Posicion[row].Split(' ');
                            Orden[row] = Pos[2] + " " + Lamina[0];
                        }
                    }
                }

                Array.Sort(Orden);

                string[] OrdenAux = new string[TamanoArrays];
                string[] valor;

                Console.Write("\n");
                for (int i = 0; i < TamanoArrays; i++)
                {
                    valor = Orden[i].Split(' ');
                    OrdenAux[i] = valor[1];
                    Console.Write(OrdenAux[i] + " ");
                }
            }
            else
            {
                Console.WriteLine("\n" + "Imposible");
            }

            Console.ReadLine();
        }
    }
}
