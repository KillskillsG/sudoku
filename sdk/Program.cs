using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sdk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SUDOKU");
            
            casilla[,] canva = new casilla[9,9];

            
            resetearcanva(canva,0,0);
            dibujarcanva(canva,0,0);
            int opc = 0;
            do
            {
                Console.WriteLine("seleccione una opcion");
                Console.WriteLine("1) ingresar manualmente los datos");
                Console.WriteLine("2) revisar canva");
                Console.WriteLine("3) solucionar");
                Console.WriteLine("4) salir");
                opc = Convert.ToInt32(Console.ReadLine());
                switch (opc)
                {
                    case 1:

                        Console.Write("x:");
                        int x = Convert.ToInt32(Console.ReadLine());
                        Console.Write("y:");
                        int y = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("insertar");
                        int v = Convert.ToInt32(Console.ReadLine());
                        canva[x, y] = new casilla { valor = v, establecido = true };

                        break;
                    case 2:
                        dibujarcanva(canva, 0, 0);
                        break;
                    case 3:
                        algoritmosolucion(canva);
                        break;
                    case 4:
                        Console.WriteLine("adios");
                        break;
                }



            } while (opc != 4);

            
        }




        static casilla[,] algoritmosolucion(casilla[,] canvabase)
        {
            //1 establecer el numero de casillas establecidas


            int[] pilasusables = new int[9];

            LinkedList<int> listaLigada = new LinkedList<int>();

            LinkedList<int> listaOrdenada = new LinkedList<int>();

            for (int i = 0; i < 9; i++)
            {
                pilasusables[i] = 9 - leervalores(canvabase, i, 0, 0, 0);
                
            }

            for (int i = 0; i < 9; i++)
            {
                listaLigada.AddLast(pilasusables[i]);
            }


            

            if (listaLigada.Count > 0)
            {
                int i = listaLigada.First();
                listaOrdenada.AddFirst(i);
                foreach (var item in listaLigada)
                {

                    if (item <= listaOrdenada.First())
                    {
                        listaOrdenada.AddFirst(item);
                    }
                    else if (item >= listaOrdenada.Last())
                    {
                        listaOrdenada.AddLast(item);
                    }
                    else
                    {
                        int prim = listaOrdenada.First();
                        LinkedListNode<int> primero = listaOrdenada.Find(prim); ;
                        LinkedListNode<int> segundo = primero.Next;


                        listaOrdenada = ordenarlista(listaOrdenada, item, primero, segundo, false);
                    }
                }

            }
            Stack<int> descartados = new Stack<int>();
            // recorre los numero de mas a menos
            foreach (var item in listaOrdenada)
            {
                

                for(int j = 1; j<pilasusables.Length; j++)
                {
                    if (item == pilasusables[j] && descartados.Contains(j) == false)
                    {
                        //aqui se llamara al metodo de llenado


                        Console.WriteLine($"se encontro el numero {item} especificado en el valor global:"+ j);
                        descartados.Push(j);
                        break;
                    }
                }
            }

            return canvabase;
        }


        static bool[] llenado(casilla[,] canvab, bool[] cuadrante, int x, int y, int num)
        {
            if (y >= canvab.GetLength(1))
            {
                return cuadrante;
            }
            else
            {

                if (canvab[x, y].valor == num)
                {
                    
                }
                return llenado(canvab, cuadrante, x, y, num);
            }
           
        }



        static LinkedList<int> ordenarlista(LinkedList<int> listaOrdenada, int valor, LinkedListNode<int> min, LinkedListNode<int> max, bool insercion)
        {

            if (insercion == true)
            {
                
                return listaOrdenada;
            }
            else
            {

                if (valor > min.Value && valor < max.Value)
                {
                    listaOrdenada.AddAfter(min, valor);
                    insercion = true;
                    
                }
                else if (valor == min.Value || valor == max.Value)
                {
                    listaOrdenada.AddAfter(min, valor);
                    insercion = true;
                    
                }
                else
                {
                    min = max;
                    max = max.Next;

                    
                }
                return ordenarlista(listaOrdenada, valor, min, max, insercion);

            }
        }



        //METODO QUE ESTABLECE RESTABLECE EL CANVA A 0
        static casilla[,] resetearcanva(casilla[,] canva, int x, int y)

            
        {
            if (y >= canva.GetLength(1))
            {
                Console.WriteLine("bienvenido");
                return canva;
            }
            else
            {
                
                canva[x, y] = new casilla { };
                if (x >= canva.GetLength(0)-1)
                {
                    x = 0;
                    y++;
                }
                else
                {

                    x++;
                }

                return resetearcanva(canva, x, y);
            }
        }

        //METODO QUE INGRESA MANUALMENTE LOS ELEMENTOS DEL CANVA
        static casilla[,] ingresarcanva(casilla[,] canva, int x, int y)
        {
            if (y >= canva.GetLength(1))
            {
                Console.WriteLine("bienvenido");
                return canva;
            }
            else
            {
                Console.Write($" dato en posicion x:{x} y:{y}:");
                int dato = Convert.ToInt32(Console.ReadLine());

                if(dato > 0 && dato < 10)
                {
                    canva[x, y] = new casilla {valor = dato, establecido = true};
                }
                else
                {
                    canva[x, y] = new casilla { };
                }

                if (x >= canva.GetLength(0) - 1)
                {
                    x = 0;
                    y++;
                }
                else
                {

                    x++;
                }

                return ingresarcanva(canva, x, y);
            }
        }

        //METODO QUE DIBUJA LOS ELEMENTOS DEL CANVAS
        static void dibujarcanva(casilla[,] canva, int x, int y)
        {
            if (y >= canva.GetLength(1))
            {
                return;
            }
            else
            {
                
                if (canva[x, y].valor != 0)
                {
                    Console.Write(canva[x, y].valor + " ");
                }
                else
                {
                    Console.Write("* ");
                }
                
                if (x >= canva.GetLength(0) - 1)
                {
                    Console.WriteLine();
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }

                dibujarcanva(canva, x, y);
                
            }
        }


        static int leervalores(casilla[,] canva,int valor, int x, int y, int totalreal)
        {
            
            if (y >= canva.GetLength(1))
            {
                return totalreal;
            }
            else
            {
                if(canva[x, y].valor == valor && canva[x, y].establecido)
                {
                    totalreal = totalreal + 1;
                    
                }
                if (x >= canva.GetLength(0) - 1)
                {
                    x = 0;
                    y++;
                }
                else
                {

                    x++;
                }


                return leervalores(canva,valor, x, y,totalreal);
            }

            
            
        }

        // objeto casilla
        public class casilla
        {
            public int valor { get; set; }
            public bool establecido{ get; set; }

            public casilla()
            {
                valor = 0;
                establecido = false;
            }

        }
    }
}
