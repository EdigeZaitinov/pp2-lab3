using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace far_manager
{
    class Program
    {
        static void draw( DirectoryInfo Folder ,int Index)//функция отрисовывания
        {
            Console.Clear();
            FileSystemInfo[] dir = Folder.GetFileSystemInfos();
            for (int i = 0; i < dir.Length; i++)
            {
                if (Index == i)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                if (dir[i].GetType() == typeof(DirectoryInfo))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else { Console.ForegroundColor = ConsoleColor.Yellow; }
                Console.WriteLine(dir[i]);
            }
        }
        static void Main()
        {
            int Index = 0;
          DirectoryInfo  Folder = new DirectoryInfo(@"D:\");//диск в котором будем шарится
            draw(Folder,Index);
            FileSystemInfo[] dir1 = Folder.GetFileSystemInfos();
            List<FileSystemInfo> names = new List<FileSystemInfo>();
            names.AddRange(dir1);
            bool open = true;
            bool alive = true;
            bool open2 = true;
            
            while (alive)
            {
                if (open) { draw(Folder, Index); }
                if (Index < 0) { Index = names.Count - 1; }
                ConsoleKeyInfo PressedKey = Console.ReadKey();

                if (PressedKey.Key == ConsoleKey.UpArrow)//поднимаемся вверх
                {
                    Index--;
                }
                if (PressedKey.Key == ConsoleKey.DownArrow)//опускаемся вниз
                {
                    Index++;
                }
                if (PressedKey.Key == ConsoleKey.Enter)//вход в папку или файл
                {
                    open = false;
                    
                        if (dir1[Index].GetType() == typeof(DirectoryInfo))
                        {
                            string ent = dir1[Index].FullName;
                            DirectoryInfo ente = new DirectoryInfo(ent);
                        int Index1 = Index;
                        Console.Clear();
                            draw(ente, Index1);
                            
                    
                        }
                        else if (dir1[Index].GetType() == typeof(FileInfo))
                        {
                            string fil = dir1[Index].FullName;
                            StreamReader b = new StreamReader(fil);
                            Console.Clear();
                            Console.WriteLine(b.ReadToEnd());
                            open = false;
                        }
                    

                }

                if (PressedKey.Key == ConsoleKey.Delete)//удаление папки или файла
                { 
                        if (dir1[Index].GetType() == typeof(DirectoryInfo))
                        {


                        Directory.Delete(dir1[Index].FullName, true);
                            Console.Clear();
                        
                        

                    }
                        else if (dir1[Index].GetType() == typeof(FileInfo))
                        {
                            try
                            {
                                File.Delete(dir1[Index].FullName);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.Clear();
                            
                           
                        }
                        
                }
                if (PressedKey.Key == ConsoleKey.Backspace)//выход из папки или файла
                {

                        Console.Clear();
                        draw(Folder, Index);
                }
                if (PressedKey.Key == ConsoleKey.F1)//переименовывание папки или файла
                {
                    if (dir1[Index].GetType() == typeof(DirectoryInfo))
                    {
                        string newname1 = Console.ReadLine();
                         string rnm = dir1[Index].FullName; //sourcefile
                        DirectoryInfo ente = new DirectoryInfo(rnm);
                        Console.WriteLine(@rnm);
                        string rnm2 = @ente.Parent.FullName + newname1;
                        Console.WriteLine(rnm2);

                        Directory.Move(rnm, rnm2);

                    }
                    else if (dir1[Index].GetType() == typeof(FileInfo))
                    {
                        string newname = Console.ReadLine();
                        string newn = Path.GetDirectoryName(dir1[Index].FullName);
                        File.Copy(dir1[Index].FullName,newn+newname,true);
                        File.Delete(dir1[Index].FullName);

                    }
                }
                if (Index == names.Count) { Index = 0; Console.BackgroundColor = ConsoleColor.Black; }
            }
        }
    }
}
