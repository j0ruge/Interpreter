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

        //ADD Reg_A Reg_B
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

        //SUB Reg_A Reg_B
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

        //AND Reg_A Reg_B
        static string[] M3 =
        {
            "0011",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1001",
            "0011"
        };

        //OR Reg_A Reg_B
        static string[] M4 =
        {
            "0100",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1001",
            "0011"
        };

        //XOR Reg_A Reg_B
        static string[] M5 =
        {
            "0101",
            "0110",
            "0111",
            "0111",
            "1111",
            "0000",
            "1001",
            "0011"
        };

        //INCREMENT_A
        static string[] M6 =
        {
            "0110",
            "0110",
            "0111",
            "1111",
            "0000",
            "0000",
            "1111",
            "0000"
        };

        //SELECIONA O CONJUNTO DE MEMORIA QUE IRA SER EXECUTADO
        static string[] memory = M6;

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
                    Console.WriteLine("SUB A B");
                    programCounter++;
                    Sub();
                    programCounter++;
                    break;
                case ADD:
                    Console.WriteLine("ADD A B");
                    programCounter++;
                    Add();
                    programCounter++;
                    break;
                case AND:
                    Console.WriteLine("AND A B");
                    programCounter++;
                    And();
                    programCounter++;
                    break;
                case OR:
                    Console.WriteLine("OR A B");
                    programCounter++;
                    Or();
                    programCounter++;
                    break;
                case XOR:
                    Console.WriteLine("XOR A B");
                    programCounter++;
                    Xor();
                    programCounter++;
                    break;
                case INCREMENT_A:
                    Console.WriteLine("INCREMENT_A");
                    programCounter++;
                    IncrementA();
                    programCounter++;
                    break;
                case CLEAR:
                    Console.WriteLine("CLEAR");
                    programCounter++;
                    Clear();
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

        static void And()
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

            var resultado = Convert.ToInt16(registrador_A, 2) & Convert.ToInt16(registrador_B, 2);

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

        static void Or()
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

            var resultado = Convert.ToInt16(registrador_A, 2) | Convert.ToInt16(registrador_B, 2);

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

        static void Xor()
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

            var resultado = Convert.ToInt16(registrador_A, 2) ^ Convert.ToInt16(registrador_B, 2);

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

        static void IncrementA()
        {
            //Obter valor registrador A
            var enderecoA = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            Console.WriteLine("Endereco_Pos: " + enderecoA);
            registrador_A = GetMemoryCell(memory, enderecoA);
            Console.WriteLine("Reg_A: " + registrador_A);

            programCounter++;

            var resultado = Convert.ToInt16(registrador_A, 2) + 1;

            //Verificar valor obtido
            ClassificarFlag(resultado);

            //Atribuir valor ao ACC
            registrador_A = Convert.ToString(Math.Abs(resultado), 2).PadLeft(4, '0');
            registrador_A = registrador_A.Length > 4 ? "0000" : registrador_A;
            Console.WriteLine("Reg_A (Acc): " + registrador_A);

            //GUARDAR VALOR DO RESULTADO NA MEMORIA
            var enderecoGuardar = Convert.ToInt16(GetMemoryCell(memory, programCounter), 2); //Valor endereco em decimal
            SetMemoryCell(memory, enderecoGuardar, registrador_A);
        }

        static void Halt()
        {
            run_bit = false;
        }

        static void Clear()
        {
            ReturnOriginalState();
        }

        static void ReturnOriginalState()
        {
            registrador_A = "0000";
            registrador_B = "0000";
            instructorRegister = "0000";
            flag_transbordo = false;
            flag_negativo = false;
            flag_zero = false;
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
            if(valor > 15)
            {
                flag_transbordo = true;
            }
            else
            {
                flag_transbordo = false;
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