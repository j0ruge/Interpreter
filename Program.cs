using System;
using System.Linq;

namespace Interpreter
{
    class Program
    {
        static int programCounter = 0;
        static bool run_bit = true; //um bit que pode ser desligado para parar a máquina

        static string registrador_A = "0000";
        static string registrador_B = "0000";

        const string SUB = "1000";
        const string ADD = "1001";
        const string HALT = "1111";

        //4 Bits Instructions

        //ADD Reg_A = 5 + Reg_B = 7 
        static string[] M1 = 
        {
            "1001",
            "0110",
            "0111",
            "1111",
            "0000",
            "0000",
            "0101",
            "0111"
        };

        //ADD Reg_A = 5 + Reg_B = 7 
        static string[] M2 =
        {
            "1000",
            "0110",
            "0111",
            "1111",
            "0000",
            "0000",
            "1111",
            "1000"
        };

        static string[] memory = M2;

        static void Main(string[] args)
        {
            //INICIO CICLO
            while (run_bit)
            {
                var value = GetMemoryCell(memory, programCounter);
                programCounter++;
                SelectOpCode(value);
            }
        }

        static void SelectOpCode(string opCode)
        {
            switch (opCode)
            {
                case SUB:
                    Console.WriteLine("SUBTRAIR A - B");
                    Sub();
                    break;
                case ADD:
                    Console.WriteLine("ADICIONAR A + B");
                    Add();
                    break;
                case HALT:
                    Console.WriteLine("HALT");
                    Halt();
                    break;
            }
        }

        static void Add()
        {
            var enderecoA = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoA);
            registrador_A = GetMemoryCell(memory, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;
            var enderecoB = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoB);
            registrador_B = GetMemoryCell(memory, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) + Convert.ToInt16(registrador_B, 2);

            //ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4, '0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);
        }

        static void Sub()
        {
            var enderecoA = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            registrador_A = GetMemoryCell(memory, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;
            var enderecoB = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            registrador_B = GetMemoryCell(memory, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) - Convert.ToInt16(registrador_B, 2);

            //ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4,'0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);
        }

        static void Jump()
        {
            
        }

        static void Halt()
        {
            run_bit = false;
        }

        //static byte[] GetOpCode(byte[] memory)
        //{
        //    var opCode = new byte[memory.Length / 2];
        //    for(int i = 0; i < memory.Length / 2; i++)
        //    {
        //        opCode[i] = memory[i];
        //    }
        //    return opCode;
        //}

        //static byte[] GetValue(byte[] memory)
        //{
        //    var value = new byte[memory.Length / 2];
        //    for (int i = 0; i < memory.Length / 2; i++)
        //    {
        //        value[i] = memory[i + (memory.Length / 2)];
        //    }
        //    return value;
        //}

        static string GetMemoryCell(string[] memory, int rowNumber)
        {            
            return memory[rowNumber];
        }
    }
}