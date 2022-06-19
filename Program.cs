using System;

namespace Interpreter
{
    class Program
    {
        
        static string instructorRegister = "0000";
        static bool run_bit = true; //um bit que pode ser desligado para parar a máquina

        #region Registers
        static int programCounter = 0;
        static string registrador_A = "0000";
        static string registrador_B = "0000";
        #endregion

        #region OP Code
        const string ADD = "0001";
        const string SUB = "0010";
        const string AND = "0011";
        const string OR  = "0100";
        const string XOR = "0101";
        const string INCREMENT_A = "0110";
        const string CLEAR = "0111";
        const string HALT = "1111";
        #endregion

        #region Flags
        static bool flag_transbordo = false;
        static bool flag_negativo = false;
        static bool flag_zero = false;
        #endregion

        //4 Bits Instructions

        //ADD Reg_A = 5 + Reg_B = 7
        static string[] M1 = 
        {
            "0001",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1000",
            "0011"
        };

        //ADD Reg_A = 5 - Reg_B = 7
        static string[] M2 =
        {
            "0010",
            "0110",
            "0111",
            "0111",
            "1111",
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
                instructorRegister = GetMemoryCell(memory, programCounter);
                SelectOpCode(instructorRegister);
            }

            PrintMemory(memory);
        }

        static void SelectOpCode(string opCode)
        {
            switch (opCode)
            {
                case SUB:
                    Console.WriteLine("SUBTRAIR A - B");
                    programCounter++;
                    Sub();
                    programCounter++;
                    break;
                case ADD:
                    Console.WriteLine("ADICIONAR A + B");
                    programCounter++;
                    Add();
                    programCounter++;
                    break;
                case HALT:
                    Console.WriteLine("HALT");
                    Halt();
                    break;
            }
        }

        static void Add()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoA);
            registrador_A = GetMemoryCell(memory, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            //Obter valor registrador B
            var enderecoB = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoB);
            registrador_B = GetMemoryCell(memory, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) + Convert.ToInt16(registrador_B, 2);

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4, '0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            programCounter++;

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(memory, enderecoGuardar, registrador_A);
        }

        static void Sub()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            registrador_A = GetMemoryCell(memory, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            //Obter valor registrador B
            var enderecoB = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            registrador_B = GetMemoryCell(memory, enderecoB);
            Console.WriteLine("Reg_B: " + registrador_B);

            var resultado = Convert.ToInt16(registrador_A, 2) - Convert.ToInt16(registrador_B, 2);

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4,'0');
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            programCounter++;

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(memory, enderecoGuardar, registrador_A);
        }

        static void Jump()
        {
            
        }

        static void Halt()
        {
            run_bit = false;
        }

        //Utilizado para pegar a célula de memória desejada
        static string GetMemoryCell(string[] memory, int rowNumber)
        {            
            return memory[rowNumber];
        }

        //Utilizado para guardar valor a célula de memória desejada
        static void SetMemoryCell(string[] memory, int rowNumber, string valor)
        {
            memory[rowNumber] = valor;
        }

        private static void ClassificarFlag(int valor)
        {   
            //Verificar se é zero
            if (valor == 0)
            {
                flag_zero = true;
            }
            else
            {
                flag_zero = false;
            }

            //Verificar se valor é menor que zero
            if (valor < 0)
            {
                flag_negativo = true;
            }
            else
            {
                flag_negativo = false;
            }

            //Verificar se valor é maior que o permitido do registrador
            try
            {
                Convert.ToInt16(valor);
                flag_transbordo = false;
            }
            catch (OverflowException)
            {
                flag_transbordo = true;
            }
        }

        private static void PrintMemory(string[] memory)
        {
            Console.WriteLine();
            for(int i = 0; i < memory.Length; i++)
            {
                Console.WriteLine($"MEM[{i}] {memory[i]}");
            }
            Console.WriteLine();
            Console.WriteLine($"Transbordo (Overflow): {flag_transbordo}");
            Console.WriteLine($"Negativo (Negative): {flag_negativo}");
            Console.WriteLine($"Zero (Zero): {flag_zero}");
        }
    }
}